namespace Manatalol.Domain.Entities
{
    public class Education
    {
        public Guid Id { get; set; }
        public string? School { get; set; }
        public string? Degree { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }

        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
