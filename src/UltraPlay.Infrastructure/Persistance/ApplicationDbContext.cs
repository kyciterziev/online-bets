using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UltraPlay.Application.Interfaces;
using UltraPlay.Domain.Common;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(IDateTimeProvider dateTimeProvider, IDomainEventService domainEventService, DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            this._dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            this._domainEventService = domainEventService ?? throw new ArgumentNullException(nameof(domainEventService));
        }

        public DbSet<Sport> Sports => Set<Sport>();
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Bet> Bets => Set<Bet>();
        public DbSet<Odd> Odds => Set<Odd>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = _dateTimeProvider.Now;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = _dateTimeProvider.Now;
                }
            }

            var events = ChangeTracker.Entries<IHasDomainEvent>()
                .SelectMany(x => x.Entity.DomainEvents)
                .Where(d => !d.IsPublished)
                .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            // await DispatchEvents(events);
            return result;
        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}