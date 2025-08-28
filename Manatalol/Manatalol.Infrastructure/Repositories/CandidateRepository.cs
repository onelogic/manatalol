using Manatalol.Application.Common;
using Manatalol.Application.DTO.Candidates;
using Manatalol.Application.Interfaces;
using Manatalol.Domain.Entities;
using Manatalol.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Manatalol.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _context;

        public CandidateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PageResult<Candidate>> GetCandidatesAsync(
           CandidateFilter candidateFilter,
           int pageNumber,
           int pageSize,
           string? sortBy = null,
           string? sortDirection = null)
        {
            var query = _context.Candidates.AsQueryable();
            query = SearchCandidatesAsync(query, candidateFilter);
            if (!string.IsNullOrEmpty(sortBy))
            {
                bool isDescending = sortDirection?.ToLower() == "desc";

                query = sortBy.ToLower() switch
                {
                    "firstname" => isDescending ? query.OrderByDescending(e => e.FirstName) : query.OrderBy(e => e.FirstName),
                    "lastname" => isDescending ? query.OrderByDescending(e => e.LastName) : query.OrderBy(e => e.LastName),
                    "location" => isDescending ? query.OrderByDescending(e => e.Location) : query.OrderBy(e => e.Location),
                    "function" => isDescending ? query.OrderByDescending(e => e.Function) : query.OrderBy(e => e.Function),
                    "currentCompany" => isDescending ? query.OrderByDescending(e => e.CurrentCompany) : query.OrderBy(e => e.CurrentCompany),
                    _ => query
                };
            }

            var totalItems = await query.CountAsync();

            var candidatePaged = await query
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<Candidate>(candidatePaged, totalItems, pageNumber, pageSize);
        }

        private IQueryable<Candidate> SearchCandidatesAsync(IQueryable<Candidate> query,
        CandidateFilter candidateFilter)
        {
            if (!string.IsNullOrEmpty(candidateFilter.Search))
            {
                var searchLower = candidateFilter.Search.ToLower();
                query = query.Where(c =>
                    c.FirstName.ToLower().Contains(searchLower) ||
                    c.LastName.ToLower().Contains(searchLower) ||
                    c.Location.ToLower().Contains(searchLower) ||
                    c.Function.ToLower().Contains(searchLower) ||
                    c.CurrentCompany.ToLower().Contains(searchLower)
                );
            }
            return query;
        }
    }
}
