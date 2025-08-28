namespace Manatalol.Domain.Entities
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public string CreatedById { get; set; }
    }
}
