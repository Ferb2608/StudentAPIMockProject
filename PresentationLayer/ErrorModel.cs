namespace WebAPISample
{
    public class ErrorModel
    {
        public string Destination { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string? Stacktrace { get; set; }

        public ErrorModel(string destination, string source, string message, string? stacktrace)
        {
            Destination = destination;
            Source = source;
            Message = message;
            Stacktrace = stacktrace;
        }
    }
}
