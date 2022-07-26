using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Infrastructure.EntityConfigurations
{
    public class OddEntityTypeConfiguration : IEntityTypeConfiguration<Odd>
    {
        public void Configure(EntityTypeBuilder<Odd> configuration)
        {
            configuration.ToTable("odds");

            configuration.HasKey(_ => _.Id);
            configuration.Property(_ => _.Name).HasColumnName("name").IsRequired();
            configuration.Property(_ => _.ExternalId).HasColumnName("external_id").IsRequired();
            configuration.Property(_ => _.Value).HasColumnName("value");
            configuration.Property(_ => _.SpecialBetValue).HasColumnName("special_bet_value").IsRequired(false);
            configuration.Property(_ => _.BetId).HasColumnName("bet_id");
            configuration.Property(_ => _.IsActive).HasColumnName("is_active").HasDefaultValue(true);

            configuration.Ignore(_ => _.DomainEvents);
        }
    }
}