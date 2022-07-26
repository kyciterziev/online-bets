using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Infrastructure.EntityConfigurations
{
    public class MatchEntityTypeConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> configuration)
        {
            configuration.ToTable("matches");

            configuration.HasKey(_ => _.Id);
            configuration.Property(_ => _.Name).HasColumnName("name").IsRequired();
            configuration.Property(_ => _.ExternalId).HasColumnName("external_id").IsRequired();
            configuration.Property(_ => _.StartDate).HasColumnName("start_date");
            configuration.Property(_ => _.MatchType).HasColumnName("match_type");
            configuration.Property(_ => _.EventId).HasColumnName("event_id");
            configuration.Property(_ => _.CreatedAt).HasColumnName("created_at");
            configuration.Property(_ => _.LastModifiedAt).HasColumnName("last_modified_at");
            configuration.Property(_ => _.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            configuration.Ignore(_ => _.DomainEvents);

            configuration.HasMany(_ => _.Bets).WithOne().HasForeignKey(_ => _.MatchId);
        }
    }
}