namespace _2.Web.Gateway.Application.DTOs
{
    public class SendRequestDto
    {
        public string ControllerName { get; set; }
        public string Entity { get; set; }
        public string Reference { get; set; }
        public object Request { get; set; }
        public string UserId { get; set; }
        public string Host { get; set; }
        public string Language { get; set; }
        public string Url { get; set; }
    }
}