using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Infrastructure.EntityConfigurations
{
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> configuration)
        {
            configuration.ToTable("events");

            configuration.HasKey(_ => _.Id);
            configuration.Property(_ => _.Name).HasColumnName("name").IsRequired();
            configuration.Property(_ => _.ExternalId).HasColumnName("external_id").IsRequired();
            configuration.Property(_ => _.SportId).HasColumnName("sport_id").IsRequired();
            configuration.Property(_ => _.CategoryId).HasColumnName("category_id").IsRequired();
            configuration.Property(_ => _.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            configuration.Property(_ => _.IsLive).HasColumnName("is_live");

            configuration.HasMany(_ => _.Matches).WithOne().HasForeignKey(_ => _.EventId);
        }
    }
}