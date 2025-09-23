namespace Manatalol.Application.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Dictionary<string, int>>> GetFunctionsData();
        Task<List<Dictionary<string, int>>> GetSkillssData();
    }
}
