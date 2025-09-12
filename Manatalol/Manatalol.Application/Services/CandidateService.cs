using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.Helpers;
using Manatalol.Application.Interfaces;
using Manatalol.Application.Mappers;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ILinkedinService _linkedinService;

        public CandidateService(ICandidateRepository candidateRepository, ILinkedinService linkedinService)
        {
            _candidateRepository = candidateRepository;
            _linkedinService = linkedinService;
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

        public async Task<string> SaveCandidateViaUpload(byte[] pdfBytes, string createdBy)
        {
            var candidate = ExtractCandidateDataFromPdf(pdfBytes, createdBy);
            await _candidateRepository.CreateCandidate(candidate);
            return candidate.Reference;
        }

        public async Task<string> CreateCandidateViaLinkedinUrl(string url, string createdBy)
        {
            var profile = await _linkedinService.GetProfileAsync();
            return "";
        }

        private Candidate ExtractCandidateDataFromPdf(byte[] pdfBytes, string createdBy)
        {
            string textContent = PdfHelper.ExtractText(pdfBytes);
            var candidate = PdfParser.ExtractCandidate(textContent);
            candidate.Reference = candidate.FirstName.ToLower() + "-" + candidate.LastName.ToLower() + "-" + new Random().Next(0, 51);
            candidate.CreatedBy = createdBy;
            candidate.Source = Domain.Enums.SourceType.File;
            candidate.CreatedAt = DateTime.UtcNow;
            return candidate;
        }
    }
}