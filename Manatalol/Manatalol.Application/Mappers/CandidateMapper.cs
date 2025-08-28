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
                Gender = request.Gender,
                CurrentCompany = request.CurrentCompany,
                Location = request.Location,
                Function = request.Function,
                Source = request.Source
            };
        }
    }
}
