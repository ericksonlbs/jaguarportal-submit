using dotnet_jaguarportal.JaguarPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace System.Net.Http
{
    internal static class HttpClientExtensions
    {
        private const string tokenEndpoint = "api/token";
        private const string authenticationScheme = "Bearer";
        internal static void GenerateAccessToken(this HttpClient httpClient, string clientId, string clientSecret)
        {
            // Prepare the request parameters
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret)
            });

            // Send the token request
            HttpResponseMessage response = httpClient.PostAsync(tokenEndpoint, requestContent).GetAwaiter().GetResult();

            // Read the response content as JSON
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                // Parse the JSON response to get the access token
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                if (tokenResponse != null)
                {
                    string? accessToken = tokenResponse.access_token;
                    httpClient.DefaultRequestHeaders.Authorization = new Headers.AuthenticationHeaderValue(authenticationScheme, accessToken);
                }
            }
            else
            {
                Console.WriteLine($"Token request failed. Status code: {response.StatusCode}");
                Console.WriteLine($"Response content: {responseContent}");
            }
        }
    }
}
