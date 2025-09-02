using Manatalol.Application.Common;
using Manatalol.Application.DTO.Notes;
using Manatalol.Application.Interfaces;
using Manatalol.Application.Mappers;

namespace Manatalol.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<PageResult<NoteDto>> GetNotesByCandidatesAsync(
          string candidateReference,
          int pageNumber,
          int pageSize,
          string? sortBy = null,
          string? sortDirection = null)
        {
            var pagedExpenses = await _noteRepository.GetNotesByCandidatesAsync(
          candidateReference, pageNumber, pageSize, sortDirection);

            return new PageResult<NoteDto>(
                pagedExpenses.Items.Select(e => e.ToDto()).ToList(),
                pagedExpenses.TotalItems,
                pagedExpenses.PageNumber,
                pagedExpenses.PageSize);
        }

        public async Task AddNote(CreateNote data)
        {
            await _noteRepository.AddNote(data);
        }
    }
}
