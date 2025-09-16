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
                Skills = (request.Skills != null) ? request.Skills?.Select(s => s.ToDto()).ToList() : new List<DTO.Skills.SkillDto>(),
                Notes = (request.Notes != null) ? request.Notes?.Select(s => s.ToDto()).ToList() : new List<DTO.Notes.NoteDto>()
            };
        }
        public static Candidate ToEntity(this CandidateInputModel dto)
        {
            if (dto == null) return null;

            return new Candidate
            {
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                Reference = dto.Reference,
                Gender = dto.Gender,
                CurrentCompany = dto.CurrentCompany,
                Location = dto.Location,
                Function = dto.Function,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Source = dto.Source,
                CreatedBy = dto.CreatedBy,
                Educations = (dto.Educations != null) ? dto.Educations.Select(e => e.ToEntity()).ToList() : new List<Education>(),
                Experiences = (dto.Experiences != null) ? dto.Experiences.Select(e => e.ToEntity()).ToList() : new List<Experience>(),
                Skills = (dto.Skills != null) ? dto.Skills.Select(s => s.ToEntity()).ToList(): new List<Skill>()
            };
        }
    }
}