using System.Net.Http.Json;

namespace VSSuppressInFileIssue
{
    // This code is just for demonstrating multiple code analysis warnings.
    public class Demo
    {
        public async Task<string> GetData(Uri apiAddress, string resource)
        {
            var httpMessageHandler = new HttpClientHandler { UseDefaultCredentials = true };
            var client = new HttpClient(httpMessageHandler) { BaseAddress = apiAddress };
            var result = await client.GetAsync(resource).ConfigureAwait(false);
            result.EnsureSuccessStatusCode();
            var response = (await result.Content.ReadFromJsonAsync<ApiResponse>().ConfigureAwait(false)) ?? throw new InvalidOperationException("Empty content");
            return response.Status;
        }

        private record ApiResponse(string Status);
    }
}