using Manatalol.Application.Interfaces;
using Manatalol.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Manatalol.Application.Extensions
{

    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<ILinkedinService, LinkedinService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<INoteService, NoteService>();
            return services;
        }
    }
}
