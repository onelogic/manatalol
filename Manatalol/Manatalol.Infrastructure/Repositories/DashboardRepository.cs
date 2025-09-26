using Manatalol.Application.Interfaces;
using Manatalol.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Manatalol.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;

        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dictionary<string, int>>> GetFunctionsData()
        => await _context.Candidates
                .GroupBy(c => c.Function)
                .Select(g => new Dictionary<string, int> { { g.Key ?? "Unknown", g.Count() } })
                .ToListAsync();

        public async Task<List<Dictionary<string, int>>> GetSkillssData()
        => await _context.Skill
                .GroupBy(c => c.Name)
                .Select(g => new Dictionary<string, int> { { g.Key, g.Count() } })
                .ToListAsync();
    }
}
