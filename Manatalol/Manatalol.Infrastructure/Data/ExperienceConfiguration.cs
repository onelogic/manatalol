using Manatalol.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manatalol.Infrastructure.Data
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CompanyName).IsRequired();
            builder.Property(e => e.Position).IsRequired();
            builder.Property(e => e.Description);
        }
    }
}
