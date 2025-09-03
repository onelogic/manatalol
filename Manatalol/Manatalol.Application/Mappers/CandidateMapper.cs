using Manatalol.Application.DTO.Candidates;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Mappers
{
    public static class CandidateMapper
    {
        public static CandidateDto ToDto(this Candidate request)
        {
            return new CandidateDto
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                Reference = request.Reference,
                Gender = request.Gender,
                CurrentCompany = request.CurrentCompany,
                Location = request.Location,
                Function = request.Function,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Source = request.Source,
                NotesCount = request.Notes?.Count ?? 0,
                CreatedBy = request.CreatedBy,
                Educations = (request.Educations != null) ? request.Educations?.Select(e => e.ToDto()).ToList() : new List<DTO.Educations.EducationDto>(),
                Experiences = (request.Experiences != null) ? request.Experiences?.Select(e => e.ToDto()).ToList() : new List<DTO.Experiences.ExperienceDto>(),
                Skills = (request.Skills != null) ? request.Skills?.Select(s => s.ToDto()).ToList() : new List<DTO.Skills.SkillDto>()
            };
        }
    }
}
