using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementation
{
    public class GenericServiceImplementation<T> : IGenericServiceInterface<T>
    {

        private readonly GetHttpClient _gethttpClient;

        public GenericServiceImplementation(GetHttpClient gethttpClient)
        {
            this._gethttpClient = gethttpClient;
        }

 
        //Delete
        public async Task<GeneralResponse> DeleteById(int id, string baseUrl)
        {
            var httpClient = await _gethttpClient.GetPrivateHttpClient();
            var response = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        // Get All
        public async Task<List<T>> GetAll(string baseUrl)
        {
            var httpClient = await _gethttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<T>>($"{baseUrl}/all");
            return results!;
        }

        //Get Id
        public async Task<T> GetById(int id, string baseUrl)
        {
            var httpClient = await _gethttpClient.GetPrivateHttpClient();
            var result = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
            return result!;
        }
        // Add 
        public async Task<GeneralResponse> Insert(T item, string baseUrl)
        {
            var httpClient =  await _gethttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/add", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        // Update

        public async Task<GeneralResponse> Update(T item, string baseUrl)
        {
            var httpClient = await _gethttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/update", item);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }
    }

}