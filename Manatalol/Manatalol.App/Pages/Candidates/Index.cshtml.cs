using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Manatalol.App.Pages.Candidates
{
    [Authorize]

    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
