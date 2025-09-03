using Manatalol.Application.DTO.Educations;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Mappers
{
    public static class EducationMapper
    {
        public static EducationDto ToDto(this Education request)
        {
            return new EducationDto
            {
                Degree = request.Degree,
                School = request.School,
                EndDate = request.EndDate,
                StartDate = request.StartDate
            };
        }
    }
}
