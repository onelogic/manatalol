using Manatalol.App.Data;
using Manatalol.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Manatalol.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkedinController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LinkedinController(ICandidateService candidateService, UserManager<ApplicationUser> userManager)
        {
            _candidateService = candidateService;
            _userManager = userManager;
        }

        public class LinkedinRequest
        {
            public string Url { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LinkedinRequest request)
        {
            if (string.IsNullOrEmpty(request.Url))
                return BadRequest(new { message = "Missing Linkedin Url" });
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var reference = await _candidateService.CreateCandidateViaLinkedinUrl(request.Url, user?.FullName ?? "System");

                return Ok(new { message = "This profile was successfully added to Manatalol app" });
            }
            catch (Exception e)
            {
               return BadRequest(new { e.Message });
            }
        }
    }
}
