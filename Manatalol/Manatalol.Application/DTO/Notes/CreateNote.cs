using Manatalol.Domain.Entities;

namespace Manatalol.Application.DTO.Notes
{
    public record CreateNote
    {
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CandidateReference { get; set; }
        public string CreatedBy { get; set; }
    }
}
