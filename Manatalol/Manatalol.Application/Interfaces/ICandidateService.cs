using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;

namespace Manatalol.Application.Interfaces
{
    public interface ICandidateService
    {
        Task<PageResult<CandidateDto>> GetCandidatesAsync(
         CandidateFilter candidateFilter,
         int pageNumber,
         int pageSize,
         string? sortBy = null,
         string? sortDirection = null);

        Task<CandidateDto?> GetCandidateDetails(string reference);
        Task<string> CreateCandidateViaLinkedinUrl(string url, string createdBy);
        Task<string> SaveCandidateViaUpload(byte[] pdfBytes, string createdBy);
        Task<string> CreateCandidatViaForm(CandidateInputModel request);
    }
}
