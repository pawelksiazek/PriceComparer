using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Common.Services
{
    public class RestRequestService : IRestRequestService
    {
        private readonly HttpClient _restClient;

        public RestRequestService()
        {
            _restClient = GetHttpClient();
        }

        private HttpClient GetHttpClient()
        {
            HttpClient host = new HttpClient();
            //host.BaseAddress = new Uri(_ebokConfiguration.WebApiUrl);

            host.DefaultRequestHeaders.Accept.Clear();
            host.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return host;
        }

        //public T Get<T>(string url)
        //{
        //    var response = _restClient.GetAsync(url).Result;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var result = response.Content.ReadAsAsync<T>().Result;
        //        return result;
        //    }

        //    throw new Exception(string.Format("{0} {1} {2}", (int)response.StatusCode, response.ReasonPhrase, url));
        //}

        public T Get<T>(string url)
        {
            HttpClient host = new HttpClient();
            //host.BaseAddress = new Uri(_ebokConfiguration.WebApiUrl);

            host.DefaultRequestHeaders.Accept.Clear();
            host.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpClient restClient = host;

            var response = restClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<T>().Result;
                return result;
            }

            throw new Exception(string.Format("{0} {1} {2}", (int)response.StatusCode, response.ReasonPhrase, url));
        }
    }
}