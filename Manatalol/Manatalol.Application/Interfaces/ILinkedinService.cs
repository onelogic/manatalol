using Manatalol.Application.DTO.Api;
using Manatalol.Application.Services;

namespace Manatalol.Application.Interfaces
{
    public interface ILinkedinService
    {
        Task<LinkedinProfile?> GetProfileAsync(string linkedinUrl);
    }
}
