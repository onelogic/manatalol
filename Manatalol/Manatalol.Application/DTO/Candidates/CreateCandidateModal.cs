using Manatalol.Application.DTO.Educations;
using Manatalol.Application.DTO.Experiences;
using Manatalol.Application.DTO.Skills;

namespace Manatalol.Application.DTO.Candidates
{
    public record CandidateInputModel : CandidateDto
    {
        public string? SkillsTag { get; set; }
    }
}
