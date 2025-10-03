using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.DTO.Skills;
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
            return candidate?.ToDto();
        }

        public async Task<CandidateInputModel?> GetCandidateDetailsToUpdate(string reference)
        {
            var candidate = await _candidateRepository.GetCandidateDetails(reference);
            return candidate?.ToInputModal();
        }


        public async Task<string> CreateCandidatViaForm(CandidateInputModel request)
        {
            request.Source = Domain.Enums.SourceType.Form;
            request.Reference = request.FirstName.ToLower() + "-" + request.LastName.ToLower() + "-" + new Random().Next(0, 51);
            request.Skills = request.SkillsTag?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => new SkillDto { Name = s.Trim() })
                .ToList();
            var candidat = request.ToEntity();
            var existEmail = await _candidateRepository.CheckExistCandidat(candidat);
            if (existEmail == true)
            {
                throw new Exception("There already exists a candidat with this email address");
            }
            await _candidateRepository.CreateCandidate(candidat);
            return request.Reference;
        }

        public async Task<string> UpdateCandidate(CandidateInputModel request)
        {
            if (string.IsNullOrEmpty(request.Reference))
                throw new ApplicationException("Reference is required");

            var candidate = await _candidateRepository.GetCandidateDetails(request.Reference);
            if (candidate == null)
                throw new ApplicationException("Candidate not found");

            candidate = request.ToUpdateEntity(candidate);
            await _candidateRepository.UpdateCandidate(candidate);
            return request.Reference;
        }

        public async Task<string> SaveCandidateViaUpload(byte[] pdfBytes, string createdBy)
        {
            var candidat = ExtractCandidateDataFromPdf(pdfBytes, createdBy);
            var existEmail = await _candidateRepository.CheckExistCandidat(candidat);
            if (existEmail == true)
            {
                throw new Exception("There already exists a candidat with this email address");
            }
            await _candidateRepository.CreateCandidate(candidat);
            return candidat.Reference;
        }

        public async Task<string> CreateCandidateViaLinkedinUrl(string url, string createdBy)
        {
            try
            {
                var profile = await _linkedinService.GetProfileAsync(url);
                if (profile == null)
                {
                    throw new ApplicationException("Unable to fetch LinkedIn profile data.");
                }
                var candidat = profile.ToCandidate(createdBy);
                candidat.LinkedinUrl = url;
                var existEmail = await _candidateRepository.CheckExistCandidat(candidat);
                if (existEmail == true)
                {
                    throw new ApplicationException("There are already a candidat with this email address");
                }
                await _candidateRepository.CreateCandidate(candidat);
                return candidat.Reference;
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task DeleteCandidate(string reference)
        {
            var candidate = await _candidateRepository.GetCandidate(reference);
            if (candidate == null)
            {
                throw new ApplicationException("Candidat not found");
            }
            await _candidateRepository.DeleteCandidate(candidate);
        }

        private Candidate ExtractCandidateDataFromPdf(byte[] pdfBytes, string createdBy)
        {
            string textContent = PdfHelper.ExtractText(pdfBytes);
            var candidate = PdfParser.ExtractCandidate(textContent);
            candidate.Reference = candidate.FirstName.ToLower() + "-" + candidate.LastName.ToLower() + "-" + new Random().Next(0, 51);
            candidate.CreatedBy = createdBy;
            candidate.Source = Domain.Enums.SourceType.File;
            return candidate;
        }
    }
}