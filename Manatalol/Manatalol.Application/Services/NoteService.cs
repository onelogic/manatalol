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

        public async Task AddNote(CreateNote data)
        {
            await _noteRepository.AddNote(data);
        }
    }
}
