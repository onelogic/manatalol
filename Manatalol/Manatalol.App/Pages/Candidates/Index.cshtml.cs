using Manatalol.App.Data;
using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Manatalol.App.Pages.Candidates
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICandidateService _candidateService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ICandidateService candidateService, UserManager<ApplicationUser> userManager)
        {
            _candidateService = candidateService;
            _userManager = userManager;
        }

        public PageResult<CandidateDto> Candidates { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; } = "firstname";

        [BindProperty(SupportsGet = true)]
        public string SortDirection { get; set; } = "asc";

        public async Task OnGetAsync()
        {
            var filter = new CandidateFilter
            {
                Search = Search
            };

            Candidates = await _candidateService.GetCandidatesAsync(
                filter,
                PageNumber,
                PageSize,
                sortBy: SortBy,
                sortDirection: SortDirection
            );
        }

        public async Task<JsonResult> OnGetSearchAsync(string? search, int pageNumber = 0)
        {
            var filter = new CandidateFilter { Search = search };

            var candidates = await _candidateService.GetCandidatesAsync(
                filter,
                pageNumber,
                PageSize,
                sortBy: SortBy,
                sortDirection: SortDirection
            );

            return new JsonResult(candidates);
        }

        [BindProperty]
        public List<IFormFile> PdfFiles { get; set; }
        public async Task<IActionResult> OnPostUploadAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var reference = "";

            if (PdfFiles == null || PdfFiles.Count == 0)
            {
                ModelState.AddModelError("", "Veuillez sélectionner au moins un fichier PDF.");
                return Page();
            }

            foreach (var file in PdfFiles)
            {
                if (file.Length > 0)
                {
                    using var stream = new MemoryStream();
                    await file.CopyToAsync(stream);
                    var pdfBytes = stream.ToArray();

                    reference = await _candidateService.SaveCandidateViaUpload(pdfBytes, user.FullName);
                }
            }
            return RedirectToPage("/Candidates/Details", new { CandidateReference = reference });
        }

        [BindProperty]
        public string LinkedinUrl { get; set; }
        public async Task<IActionResult> OnPostLinkedinUrlAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (string.IsNullOrWhiteSpace(LinkedinUrl) || !LinkedinUrl.StartsWith("https://www.linkedin.com/in/"))
            {
                ModelState.AddModelError("", "Veuillez sélectionner un url de linkedin");
                return Page();
            }

            var reference = await _candidateService.CreateCandidateViaLinkedinUrl(LinkedinUrl, user.FullName);
            return RedirectToPage("/Candidates/Details", new { CandidateReference = reference });
        }

        public async Task<IActionResult> OnPostDeleteAsync(string reference)
        {
            if (string.IsNullOrEmpty(reference))
            {
                ModelState.AddModelError("", "Candidate Reference is empty");
                return Page();
            }
            await _candidateService.DeleteCandidate(reference);
            return RedirectToPage("/Candidates/Index");
        }
    }
}
