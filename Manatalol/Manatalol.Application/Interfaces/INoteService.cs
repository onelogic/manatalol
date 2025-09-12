using Manatalol.Application.Common;
using Manatalol.Application.DTO.Notes;

namespace Manatalol.Application.Interfaces
{
    public interface INoteService
    {
        Task AddNote(CreateNote data);
    }
}
