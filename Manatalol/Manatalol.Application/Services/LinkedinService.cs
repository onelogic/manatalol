using Manatalol.Application.Configurations;
using Manatalol.Application.DTO.Api;
using Manatalol.Application.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;


namespace Manatalol.Application.Services
{
    public class LinkedinService : ILinkedinService
    {
        private readonly HttpClient _httpClient;
        private readonly EnrichLayerApi _enrichLayerApi;
         
        public LinkedinService(HttpClient httpClient, EnrichLayerApi enrichLayerApi)
        {
            _httpClient = httpClient;
            _enrichLayerApi = enrichLayerApi;

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _enrichLayerApi.Token);
        }

        public async Task<LinkedinProfile?> GetProfileAsync(string linkedinUrl)
        {
            try
            {
                var options = "fallback_to_cache=on-error&use_cache=if-present&skills=include" +
                             "&personal_email=include&personal_contact_number=include" +
                "&personal_email=include&personal_contact_number=include";


                string url = $"{_enrichLayerApi.BaseUrl}/api/v2/profile?" +
                         $"{options}" +
                         $"&url={linkedinUrl}";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var result= await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LinkedinProfile>(result);
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"Erreur API EnrichLayer : {e.Message}");
            }
        }
    }
}
