namespace LegalTrace.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public int Code { get; private set; }

        private ApiResponse(bool success, string message, T data, int code)
        {
            Success = success;
            Message = message;
            Data = data;
            Code = code;
        }

        public static ApiResponse<T> SuccessResponse(int code, T data, string message = "")
            => new ApiResponse<T>(true, message, data, code);

        public static ApiResponse<T> NotFoundResponse(int code, string message)
            => new ApiResponse<T>(false, message, default, code);

        public static ApiResponse<T> ErrorResponse(int code, string message)
            => new ApiResponse<T>(false, message, default, code);

        public static ApiResponse<T> BadRequest(int code, T data, string message)
            => new ApiResponse<T>(false, message, data, code);
    }

}

