using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Manatalol.App.Pages.Candidates
{
    [Authorize]

    public class IndexModel : PageModel
    {
        private readonly ICandidateService _candidateService;

        public IndexModel(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public PageResult<CandidateDto> Candidates { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 0;

        public int PageSize { get; set; } = 20;

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
                sortBy: "firstname",
                sortDirection: "asc"
            );
        }
        public async Task<JsonResult> OnGetSearchAsync(string? search, int pageNumber = 0)
        {
            var filter = new CandidateFilter { Search = search };

            var candidates = await _candidateService.GetCandidatesAsync(
                filter,
                pageNumber,
                PageSize,
                sortBy: "firstname",
                sortDirection: "asc"
            );

            return new JsonResult(candidates);
        }
    }
}
