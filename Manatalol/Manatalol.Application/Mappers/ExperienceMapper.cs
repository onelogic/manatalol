using Manatalol.Application.DTO.Experiences;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Mappers
{
    public static class ExperienceMapper
    {
        public static ExperienceDto ToDto(this Experience request)
        {
            return new ExperienceDto
            {
                CompanyName = request.CompanyName,
                Position = request.Position,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsCurrent = request.IsCurrent
            };
        }
        public static Experience ToEntity(this ExperienceDto request)
        {
            return new Experience
            {
                CompanyName = request.CompanyName,
                Position = request.Position,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsCurrent = request.IsCurrent
            };
        }
    }
}
