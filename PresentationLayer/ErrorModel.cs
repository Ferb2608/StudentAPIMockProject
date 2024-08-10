namespace WebAPISample
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        
        public ErrorModel()
        {
            
        }
        public ErrorModel(int statusCode, string source, string message)
        {
            StatusCode = statusCode;
            Source = source;
            Message = message;
        }
    }
}
