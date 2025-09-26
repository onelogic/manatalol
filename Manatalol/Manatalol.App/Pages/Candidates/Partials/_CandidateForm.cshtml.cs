using Manatalol.Application.DTO.Candidates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Manatalol.App.Pages.Candidates.Partials
{
    public class _CandidateFormModel : PageModel
    {
        [BindProperty]
        public CandidateInputModel Candidate { get; set; }
        public void OnGet()
        {
        }
    }
}
