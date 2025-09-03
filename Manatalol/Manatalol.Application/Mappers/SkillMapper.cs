using Manatalol.Application.DTO.Skills;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Mappers
{
    public static class SkillMapper
    {
        public static SkillDto ToDto(this Skill request)
        {
            return new SkillDto
            {
                Name = request.Name
            };
        }
    }
}
