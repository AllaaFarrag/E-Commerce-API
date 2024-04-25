
namespace E_Commerce.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? errorMessage =null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetErrorMessageForStatusCode(statusCode);
        }

        private string? GetErrorMessageForStatusCode(int statusCode)
        => statusCode switch
        {
            500 => "Internal Server Error",
            404 => "Not Found",
            401 => "UnAuthorized",
            400 => "Bad Request",
            _ => ""
        };

        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }


    }
}
