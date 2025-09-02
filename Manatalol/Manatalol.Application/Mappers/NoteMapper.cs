using Manatalol.Application.DTO.Notes;
using Manatalol.Domain.Entities;

namespace Manatalol.Application.Mappers
{
    public static class NoteMapper
    {
        public static NoteDto ToDto(this Note request)
        {
            return new NoteDto
            {
                Content = request.Content,
                CreatedAt = request.CreatedAt,
                CreatedBy = request.CreatedBy
            };
        }
    }
}
