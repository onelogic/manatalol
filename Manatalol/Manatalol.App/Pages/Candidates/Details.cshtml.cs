using Manatalol.App.Data;
using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.DTO.Notes;
using Manatalol.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Manatalol.App.Pages.Candidates
{
    public class DetailsModel : PageModel
    {
        private readonly ICandidateService _candidateService;
        private readonly INoteService _noteService;
        private readonly UserManager<ApplicationUser> _userManager;


        public DetailsModel(ICandidateService candidateService,
            INoteService noteService,
            UserManager<ApplicationUser> userManager)
        {
            _candidateService = candidateService;
            _noteService = noteService;
            _userManager = userManager;
        }

        public CandidateDto? CandidateDetails { get; set; }

        public PageResult<NoteDto> Notes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CandidateReference { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 0;

        public int PageSize { get; set; } = 5;

        public async Task<IActionResult> OnGetAsync()
        {
            CandidateDetails = await _candidateService.GetCandidateDetails(CandidateReference);
            if (CandidateDetails == null)
            {
                return RedirectToPage("/Error/Error404");
            }

            Notes = await _noteService.GetNotesByCandidatesAsync(
                CandidateReference,
                PageNumber,
                PageSize,
                sortDirection: "asc"
            );
            return Page();
        }

        [BindProperty]
        public CreateNote NewNote { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(NewNote.Content))
            {
                ModelState.AddModelError("", "Le contenu de la note est obligatoire.");
                return Page();
            }

            NewNote.CandidateReference = CandidateReference;
            var user = await _userManager.GetUserAsync(User);
            NewNote.CreatedBy = user?.FullName ?? "System";

            await _noteService.AddNote(NewNote);

            return RedirectToPage(new { CandidateReference });
        }
    }
}
