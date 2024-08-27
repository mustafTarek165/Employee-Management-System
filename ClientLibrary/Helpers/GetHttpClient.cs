using BaseLibrary.DTOs;
using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helpers
{
    public class GetHttpClient(IHttpClientFactory httpClientFactory,LocalStorageService localStorageService)
    {
        private const string HeaderKey = "Authorization";
        public async Task<HttpClient>GetPrivateHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            var stringtoken = await localStorageService.GetToken();

            if (string.IsNullOrEmpty(stringtoken)) return client;

            var deserializetoken = Serializations.DeserializeJsonString<UserSession>(stringtoken);
            if(deserializetoken == null) return client;
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializetoken.Token);
            return client;
        }
        public HttpClient GetPublicHttpClient()
        {
            var client = httpClientFactory.CreateClient("SystemApiClient");
            client.DefaultRequestHeaders.Remove(HeaderKey);
            return client;
        }
    }
}
