namespace _2.UsersManagement.Application.DTOs
{
    public class ResponseDto<TData>
    {
        public string Response { get; set; }

        public TData Data { get; set; } 

        public object Message { get; set; }
    }
}
