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
        public string Reference { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public Gender Gender { get; set; }
        public string? CurrentCompany { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Function { get; set; } = string.Empty;
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
