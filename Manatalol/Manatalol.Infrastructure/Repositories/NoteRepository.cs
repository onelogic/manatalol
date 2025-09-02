using Manatalol.Application.Common;
using Manatalol.Application.DTO.Notes;
using Manatalol.Application.Interfaces;
using Manatalol.Domain.Entities;
using Manatalol.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PageResult<Note>> GetNotesByCandidatesAsync(
           string candidateReference,
           int pageNumber,
           int pageSize,
           string? sortDirection = null)
        {
            var candidate = await _candidateRepository.GetCandidateByReference(candidateReference);
            if (candidate == null)
            {
                return new PageResult<Note>(new List<Note>(), 0, pageNumber, pageSize);
            }
            var query = _context.Notes.Where(n => n.Candidate.Id == candidate.Id).OrderByDescending(c=>c.CreatedAt);
            var totalItems = await query.CountAsync();

            var notePaged = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<Note>(notePaged, totalItems, pageNumber, pageSize);
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