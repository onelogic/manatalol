namespace Manatalol.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<List<Dictionary<string, int>>> GetFunctionsData();
        Task<List<Dictionary<string, int>>> GetSkillsData();
    }
}
