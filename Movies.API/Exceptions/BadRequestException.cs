namespace Movies.API.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string DefaultMessage = "Something went wrong";
        public BadRequestException() : base(DefaultMessage) { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(Exception innerException): base(DefaultMessage, innerException) { }
        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}
