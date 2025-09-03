using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.Interfaces;
using Manatalol.Application.Mappers;

namespace Manatalol.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<PageResult<CandidateDto>> GetCandidatesAsync(
           CandidateFilter candidateFilter,
           int pageNumber,
           int pageSize,
           string? sortBy = null,
           string? sortDirection = null)
        {
            var pagedExpenses = await _candidateRepository.GetCandidatesAsync(
          candidateFilter, pageNumber, pageSize, sortBy, sortDirection);

            return new PageResult<CandidateDto>(
                pagedExpenses.Items.Select(e => e.ToDto()).ToList(),
                pagedExpenses.TotalItems,
                pagedExpenses.PageNumber,
                pagedExpenses.PageSize);
        }

        public async Task<CandidateDto?> GetCandidateDetails(string reference)
        {
            var candidate = await _candidateRepository.GetCandidateDetails(reference);
            return candidate == null ? null : candidate.ToDto();
        }
    }
}
