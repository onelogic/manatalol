using Manatalol.Application.DTO.Notes;
using Manatalol.Application.Interfaces;
using Manatalol.Domain.Entities;
using Manatalol.Infrastructure.Data;

namespace Manatalol.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;
        private readonly ICandidateRepository _candidateRepository;

        public NoteRepository(AppDbContext context,
            ICandidateRepository candidateRepository)
        {
            _context = context;
            _candidateRepository = candidateRepository;
        }

        public async Task AddNote(CreateNote data)
        {
            var candidate = await _candidateRepository.GetCandidateByReference(data.CandidateReference);
            if (candidate != null)
            {
                var note = new Note()
                {
                    CandidateId = candidate.Id,
                    Content = data.Content,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = data.CreatedBy,
                };

                _context.Add(note);
                _context.SaveChanges();
            }
        }
    }
}