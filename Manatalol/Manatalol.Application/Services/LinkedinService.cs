using Manatalol.Application.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Manatalol.Application.Services
{
    public class LinkedinService: ILinkedinService
    {
        private readonly HttpClient _httpClient;

        public LinkedinService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LinkedinProfile> GetProfileAsync()
        {
            var accessToken = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            // Récupérer les infos de base
            var meResponse = await _httpClient.GetAsync("https://api.linkedin.com/v2/me");
            meResponse.EnsureSuccessStatusCode();
            var meJson = await meResponse.Content.ReadAsStringAsync();

            var profile = JsonSerializer.Deserialize<LinkedinProfile>(meJson);

            // Récupérer l'email
            var emailResponse = await _httpClient.GetAsync(
                "https://api.linkedin.com/v2/emailAddress?q=members&projection=(elements*(handle~))");
            emailResponse.EnsureSuccessStatusCode();
            var emailJson = await emailResponse.Content.ReadAsStringAsync();

            var emailData = JsonDocument.Parse(emailJson);
            profile.Email = emailData.RootElement
                .GetProperty("elements")[0]
                .GetProperty("handle~")
                .GetProperty("emailAddress")
                .GetString();

            return profile;
        }


        private async Task<string> GetAccessTokenAsync()
        {
            using var client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "redirect_uri", "https://localhost:7290/" },
                { "client_id", "" },
                { "client_secret", "" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://www.linkedin.com/oauth/v2/accessToken", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
    public class LinkedinProfile
    {
        public string Id { get; set; }
        public string LocalizedFirstName { get; set; }
        public string LocalizedLastName { get; set; }
        public string Email { get; set; }
    }

}
