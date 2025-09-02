using Manatalol.Domain.Enums;

namespace Manatalol.Application.DTO.Candidates
{
    public record CandidateDto
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string CurrentCompany { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Function { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public SourceType Source { get; set; }
        public int NotesCount { get; set; }
    }
}
