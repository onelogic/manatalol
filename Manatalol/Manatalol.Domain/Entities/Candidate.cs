using Manatalol.Domain.Enums;

namespace Manatalol.Domain.Entities
{
    public class Candidate
    {
        public Guid Id { get; set; }

        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string CurrentCompany { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Function { get; set; } = string.Empty;
        public string AddedBy { get; set; } = string.Empty;
        public SourceType Source { get; set; }
        public string CreatedById { get; set; }

        public List<Experience> Experiences { get; set; }
        public List<Note> Notes { get; set; }
    }
}
