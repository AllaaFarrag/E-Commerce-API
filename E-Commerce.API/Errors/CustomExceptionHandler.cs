using System.Net;
using System.Text.Json;

namespace E_Commerce.API.Errors
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandler> _logger;
        private readonly IHostEnvironment _environment;

        public CustomExceptionHandler(RequestDelegate next, ILogger<CustomExceptionHandler> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //Content type = Application/Json
                //Naming Convention = CamelCase

                _logger.LogError(ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //context.Response.ContentType = "application/json";

                var response = _environment.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError
                    , ex.Message, ex.StackTrace) : new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
               
                //var json = JsonSerializer.Serialize(response , new JsonSerializerOptions
                //{
                //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                //});
                //await context.Response.WriteAsync(json);
                
                await context.Response.WriteAsJsonAsync(response);

            }
        }
    }
}
