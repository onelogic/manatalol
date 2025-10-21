using Manatalol.App.Data;
using Manatalol.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smartadmin.Pages.Auth
{
    public class ForgetpasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgetpasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public string Email { get; set; }

        public bool EmailSent { get; set; } = false;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                EmailSent = true;
                return Page();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Page(
                "/Auth/ResetPassword",
                pageHandler: null,
                values: new { email = Email, token },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                Email,
                "ManatalOl : Reset your password",
               resetLink ?? "",
               user.FullName);

            EmailSent = true;
            return Page();
        }
    }
}

