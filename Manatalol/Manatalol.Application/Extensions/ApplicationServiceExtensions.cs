using Manatalol.Application.Configurations;
using Manatalol.Application.Helpers;
using Manatalol.Application.Interfaces;
using Manatalol.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manatalol.Application.Extensions
{

    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EnrichLayerApi>(configuration.GetSection("EnrichLayerApi"));
            var enrichLayerApi = configuration.GetSection("EnrichLayerApi").Get<EnrichLayerApi>();
            services.AddSingleton(enrichLayerApi);

            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(emailSettings);


            services.AddHttpClient<ILinkedinService, LinkedinService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<RazorViewToStringRenderer>();

            return services;
        }
    }
}
