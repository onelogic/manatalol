using Manatalol.Application.Common;
using Manatalol.Application.DTO.Notes;

namespace Manatalol.Application.Interfaces
{
    public interface INoteService
    {
        Task<PageResult<NoteDto>> GetNotesByCandidatesAsync(
          string candidateReference,
          int pageNumber,
          int pageSize,
          string? sortBy = null,
          string? sortDirection = null);

        Task AddNote(CreateNote data);
    }
}
