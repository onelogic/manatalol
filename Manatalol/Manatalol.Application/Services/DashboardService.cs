using Manatalol.Application.Interfaces;

namespace Manatalol.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<List<Dictionary<string, int>>> GetFunctionsData()
            => await _dashboardRepository.GetFunctionsData();

        public async Task<List<Dictionary<string, int>>> GetSkillsData()
            => await _dashboardRepository.GetSkillssData(); 
    }
}
