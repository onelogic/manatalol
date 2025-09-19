using Manatalol.Application.Configurations;
using Manatalol.Application.Interfaces;
using Manatalol.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Manatalol.Application.Extensions
{

    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EnrichLayerApi>(configuration.GetSection("EnrichLayerApi"));
            var enrichLayerApi = configuration.GetSection("EnrichLayerApi").Get<EnrichLayerApi>();
            services.AddSingleton(enrichLayerApi);

            services.AddHttpClient<ILinkedinService, LinkedinService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<INoteService, NoteService>();
            return services;
        }
    }
}
