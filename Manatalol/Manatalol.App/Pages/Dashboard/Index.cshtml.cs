using Manatalol.App.Data;
using Manatalol.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smartadmin.Pages.Dashboard
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public readonly IDashboardService _dashboardService;
        public IndexModel(SignInManager<ApplicationUser> signInManager,
            IDashboardService dashboardService)
        {
            _signInManager = signInManager;
            _dashboardService = dashboardService;
        }

        public List<Dictionary<string, int>> FunctionsData { get; set; }
        public List<Dictionary<string, int>> SkillsData { get; set; }

        public async Task OnGet()
        {
            FunctionsData =  await _dashboardService.GetFunctionsData();
            SkillsData = await _dashboardService.GetSkillsData();
        }
    }
}
