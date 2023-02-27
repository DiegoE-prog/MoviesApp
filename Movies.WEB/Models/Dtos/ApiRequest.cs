
namespace Movies.WEB.Models.Dtos
{
    public class ApiRequest
    {
        public SD.ApiType ApiType { get; set; }
        public string? Url { get; set; }
        public object? Data { get; set; }
        public string? AccessToken { get; set; }
    }
}
