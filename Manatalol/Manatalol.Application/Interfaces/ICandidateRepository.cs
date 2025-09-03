using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Interfaces
{
    public interface ICandidateRepository
    {
        Task<PageResult<Candidate>> GetCandidatesAsync(
            CandidateFilter candidateFilter,
            int pageNumber,
            int pageSize,
            string? sortBy = null,
            string? sortDirection = null);
        Task<Candidate?> GetCandidateDetails(string reference);
        Task<Candidate?> GetCandidateByReference(string reference);
    }
}
