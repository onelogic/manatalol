﻿namespace Manatalol.Domain.Entities
{
    public class Experience
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
