namespace Manatalol.Domain.Entities
{
    public class Education
    {
        public Guid Id { get; set; }
        public string School { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
