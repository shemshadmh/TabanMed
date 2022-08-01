
namespace Application.Common
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, List<string> errors, string message)
        {
            IsSucceeded = true;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public Response(string message, List<string> errors, T data)
        {
            IsSucceeded = false;
            Message = message;
            Errors = errors;
            Data = data;
        }

        public Response<T> Succeeded(string message = "Successfully")
        {
            IsSucceeded = true;
            Message = message;
            return this;
        }

        public Response<T> Succeeded(T returnData, string message = "Successfully")
        {
            IsSucceeded = true;
            Message = message;
            Data = returnData;
            return this;
        }

        public Response<T> Failed(List<string> errors, string message)
        {
            IsSucceeded = false;
            Errors = errors;
            Message = message;
            return this;
        }

        public Response<T> Failed(string message)
        {
            IsSucceeded = false;
            Message = message;
            return this;
        }

        public bool IsSucceeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }
    }
}
