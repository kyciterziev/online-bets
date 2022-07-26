using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Infrastructure.EntityConfigurations
{
    public class BetEntityTypeConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> configuration)
        {
            configuration.ToTable("bets");

            configuration.HasKey(_ => _.Id);
            configuration.Property(_ => _.Name).HasColumnName("name").IsRequired();
            configuration.Property(_ => _.ExternalId).HasColumnName("external_id").IsRequired();
            configuration.Property(_ => _.MatchId).HasColumnName("match_id");
            configuration.Property(_ => _.IsLive).HasColumnName("is_live");
            configuration.Property(_ => _.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            configuration.Ignore(_ => _.DomainEvents);

            configuration.HasMany(_ => _.Odds).WithOne().HasForeignKey(_ => _.BetId);
        }
    }
}