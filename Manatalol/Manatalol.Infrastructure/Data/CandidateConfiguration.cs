using Manatalol.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manatalol.Infrastructure.Data
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).IsRequired();
            builder.Property(c => c.LastName).IsRequired();
            builder.Property(c => c.Reference).IsRequired();
            builder.Property(c => c.CurrentCompany);
            builder.Property(c => c.Location);
            builder.Property(c => c.Function);
            builder.Property(c => c.Email);
            builder.Property(c => c.PhoneNumber);
            builder.Property(c => c.Source);
            builder.Property(c => c.CreatedBy);
            builder.Property(c => c.LinkedinUrl);
            builder.HasMany(c => c.Experiences)
                .WithOne(e => e.Candidate)
                .HasForeignKey(e => e.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Notes)
                .WithOne(n => n.Candidate)
                .HasForeignKey(n => n.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Skills)
                .WithOne(n => n.Candidate)
                .HasForeignKey(n => n.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Educations)
                .WithOne(n => n.Candidate)
                .HasForeignKey(n => n.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Reference).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.LinkedinUrl).IsUnique();
        }
    }
}