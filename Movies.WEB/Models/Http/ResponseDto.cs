namespace Movies.WEB.Models.Http
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
