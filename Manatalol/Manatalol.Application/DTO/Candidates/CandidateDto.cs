using Manatalol.Application.DTO.Educations;
using Manatalol.Application.DTO.Experiences;
using Manatalol.Application.DTO.Notes;
using Manatalol.Application.DTO.Skills;
using Manatalol.Domain.Enums;

namespace Manatalol.Application.DTO.Candidates
{
    public record CandidateDto
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? Reference { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public Gender Gender { get; set; }
        public string? CurrentCompany { get; set; }
        public string? Location { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Function { get; set; }
        public string? Summary { get; set; }

        public string? LinkedinUrl { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public SourceType Source { get; set; }
        public int NotesCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ExperienceDto>? Experiences { get; set; }
        public List<SkillDto>? Skills { get; set; }
        public List<EducationDto>? Educations { get; set; }
        public List<NoteDto>? Notes { get; set; }
    }
}
