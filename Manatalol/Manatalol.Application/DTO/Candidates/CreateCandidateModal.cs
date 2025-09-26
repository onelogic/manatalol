namespace Manatalol.Application.DTO.Candidates
{
    public record CandidateInputModel : CandidateDto
    {
        public string? SkillsTag { get; set; }
    }
}
