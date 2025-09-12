using Manatalol.Application.Common;
using Manatalol.Application.DTO.Notes;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Interfaces
{
    public interface INoteRepository
    {
        Task AddNote(CreateNote data);
    }
}
