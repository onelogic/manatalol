using Manatalol.App.Data;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Manatalol.App.Pages.Candidates
{
    public class CreateCandidateModalModel : PageModel
    {
        private readonly ICandidateService _candidateService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateCandidateModalModel(ICandidateService candidateService, UserManager<ApplicationUser> userManager)
        {
            _candidateService = candidateService;
            _userManager = userManager;
        }


        [BindProperty]
        public CandidateInputModel NewCandidate { get; set; }
        public async Task<IActionResult> OnPostCreateCandidateAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var user = await _userManager.GetUserAsync(User);
                NewCandidate.CreatedBy = user?.FullName ?? "System";
                var reference = await _candidateService.CreateCandidatViaForm(NewCandidate);
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