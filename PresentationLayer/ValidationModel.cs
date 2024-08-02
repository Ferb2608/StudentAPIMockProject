namespace WebAPISample
{
    public class ValidationModel
    {
        public string Destination { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }

        public ValidationModel(string destination, string source, string message)
        {
            Destination = destination;
            Source = source;
            Message = message;
        }
    }
}
