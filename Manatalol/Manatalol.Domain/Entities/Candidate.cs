using Manatalol.Domain.Enums;

namespace Manatalol.Domain.Entities
{
    public class Candidate
    {
        public Guid Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string? CurrentCompany { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Function { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? LinkedinUrl { get; set; }
        public SourceType Source { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<Experience> Experiences { get; set; }
        public List<Note> Notes { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Education> Educations { get; set; }

    }
}