namespace Showdown_hub.Models
{
    public class ResponseDto<T>
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }

        public T Result  { get; set; }

        public List<string> ErrorMessage { get; set; }

    }
}