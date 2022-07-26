using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UltraPlay.Domain.Entities;

namespace UltraPlay.Infrastructure.EntityConfigurations
{
    public class SportEntityTypeConfiguration : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> configuration)
        {
            configuration.ToTable("sports");

            configuration.HasKey(_ => _.Id);
            configuration.Property(_ => _.ExternalId).HasColumnName("external_id").IsRequired();
            configuration.Property(_ => _.Name).HasColumnName("name").IsRequired();
            configuration.Property(_ => _.CreatedAt).HasColumnName("created_at");
            configuration.Property(_ => _.LastModifiedAt).HasColumnName("last_modified_at");

            configuration.HasMany(_ => _.Events).WithOne().HasForeignKey(_ => _.SportId);
        }
    }
}