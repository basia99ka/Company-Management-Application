using SharedLibrary.DTOs;
using Blazored.LocalStorage;
using Microsoft.Extensions.Http;
using System.Net.Http;

namespace ClientLibrary.Helpers
{
    public class GetHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocalStorageService _localStorageService;
        private const string HeaderKey = "Authorization";

        public GetHttpClient(IHttpClientFactory httpClientFactory, LocalStorageService localStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _localStorageService = localStorageService;
        }

        public async Task<HttpClient> GetPrivateHttpClient()
        {
            var client = _httpClientFactory.CreateClient("SystemApiClient");

            // Pobierz token z LocalStorage
            var stringToken = await _localStorageService.GetToken();

            // Jeśli token nie istnieje, zwróć klienta bez nagłówka Authorization
            if (string.IsNullOrEmpty(stringToken)) return client;

            // Deserializuj token do obiektu UserSession
            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken);

            // Jeśli deserializacja się nie powiodła, zwróć klienta bez nagłówka Authorization
            if (deserializeToken == null) return client;

            // Debugowanie: Wyświetl token w konsoli
            Console.WriteLine($"Token: {deserializeToken.Token}");

            // Ustaw nagłówek Authorization z tokenem
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializeToken.Token);

            // Zwróć skonfigurowanego klienta
            return client;

        }

        public HttpClient GetPublicHttpClient()
        {
            var client = _httpClientFactory.CreateClient("SystemApiClient");
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }

    }
}



