namespace Manatalol.Domain.Entities
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
