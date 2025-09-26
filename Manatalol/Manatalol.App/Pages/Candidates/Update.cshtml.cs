using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Manatalol.App.Pages.Candidates
{
    public class UpdateModel : PageModel
    {
        private readonly ICandidateService _candidateService;

        public UpdateModel(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [BindProperty(SupportsGet = true)]
        public string CandidateReference { get; set; }
        [BindProperty]
        public CandidateInputModel Candidate { get; set; }
        public async Task OnGet()
        {
            var result = await _candidateService.GetCandidateDetailsToUpdate(CandidateReference);
            if (result == null)
            {
                RedirectToPage("/Error/Error404");
            }
            Candidate = result;
        }

        public async Task<IActionResult> OnPostUpdateCandidateAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var reference = await _candidateService.UpdateCandidate(Candidate);
                return RedirectToPage("/Candidates/Details", new { CandidateReference = reference });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
