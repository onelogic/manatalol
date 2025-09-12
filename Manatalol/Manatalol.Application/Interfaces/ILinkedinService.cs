using Manatalol.Application.Services;

namespace Manatalol.Application.Interfaces
{
    public interface ILinkedinService
    {
        Task<LinkedinProfile> GetProfileAsync();
    }
}
