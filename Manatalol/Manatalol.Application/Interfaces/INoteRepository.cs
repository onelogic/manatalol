using Manatalol.Application.Common;
using Manatalol.Application.DTO.Notes;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Interfaces
{
    public interface INoteRepository
    {
        Task<PageResult<Note>> GetNotesByCandidatesAsync(
           string candidateReference,
           int pageNumber,
           int pageSize,
           string? sortDirection = null);
        Task AddNote(CreateNote data);
    }
}
