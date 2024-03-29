﻿using Movies.WEB.Models.Http;
using Movies.WEB.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Movies.WEB.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.httpClient= httpClient;
            this.responseModel = new ResponseDto();
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MoviesAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                
                message.RequestUri = new Uri(apiRequest.Url!);
                client.DefaultRequestHeaders.Clear();

                if(apiRequest.Data is not null) 
                { 
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8,
                        "application/json");
                    
                }

                if(!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }

                switch(apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

                return apiResponseDto!;
            }
            catch(Exception ex)
            {
                var responseDto = new ResponseDto
                {
                    Success= false,
                    Message= ex.Message,
                };

                var res = JsonConvert.SerializeObject(responseDto);
                var apiResponseDto = JsonConvert.DeserializeObject<T> (res);
                return apiResponseDto!;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
