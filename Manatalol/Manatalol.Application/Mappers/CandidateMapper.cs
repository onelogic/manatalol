using Manatalol.Application.DTO.Api;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Domain.Entities;
using Manatalol.Domain.Enums;

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
                ProfilePictureUrl = request.ProfilePictureUrl,
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
                Skills = (dto.Skills != null) ? dto.Skills.Select(s => s.ToEntity()).ToList() : new List<Skill>()
            };
        }
        public static Candidate ToCandidate(this LinkedinProfile profile, string createdBy)
        {
            return new Candidate
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Reference = profile.FirstName.ToLower() + "-" + profile.LastName.ToLower() + "-" + new Random().Next(0, 51),
                CurrentCompany = profile.Experiences?.FirstOrDefault()?.Company,
                Location = profile.LocationStr,
                Function = profile.Headline,
                Email = null,
                PhoneNumber = null,
                ProfilePictureUrl = profile.ProfilePictureUrl,
                Gender = Gender.Unknown,
                Source = SourceType.Linkedin,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow,

                Experiences = profile.Experiences?.Select(e => new Experience
                {
                    CompanyName = e.Company,
                    Position = e.Title,
                    Description = e.Description,
                    StartDate = e.StartsAt != null ?
                                new DateTime(e.StartsAt.Year ?? 1, e.StartsAt.Month ?? 1, e.StartsAt.Day ?? 1) : (DateTime?)null,
                    EndDate = e.EndsAt != null ?
                              new DateTime(e.EndsAt.Year ?? 1, e.EndsAt.Month ?? 1, e.EndsAt.Day ?? 1) : (DateTime?)null
                }).ToList() ?? new List<Experience>(),

                Educations = profile.Education?.Select(ed => new Education
                {
                    School = ed.School,
                    Degree = ed.DegreeName,
                    Description = ed.FieldOfStudy,
                    StartDate = ed.StartsAt != null ?
                                new DateTime(ed.StartsAt.Year ?? 1, ed.StartsAt.Month ?? 1, ed.StartsAt.Day ?? 1) : (DateTime?)null,
                    EndDate = ed.EndsAt != null ?
                              new DateTime(ed.EndsAt.Year ?? 1, ed.EndsAt.Month ?? 1, ed.EndsAt.Day ?? 1) : (DateTime?)null
                }).ToList() ?? new List<Education>(),

                Skills = profile.Skills?.Select(s => new Skill
                {
                    Name = s
                }).ToList() ?? new List<Skill>(),

                Notes = new List<Note>()
            };
        }
    }
}