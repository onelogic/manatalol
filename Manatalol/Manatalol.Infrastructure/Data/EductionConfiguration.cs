using Manatalol.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manatalol.Infrastructure.Data
{
    public class EductionConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.School);
            builder.Property(n => n.Degree);
            builder.Property(n => n.Description);
            builder.Property(n => n.StartDate);
            builder.Property(n => n.EndDate);
        }
    }
}
