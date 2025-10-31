using System.Text.Json;

namespace UserContactRegistration.Application.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, IServiceProvider serviceProvider,
                                            ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            using var scope = _serviceProvider.CreateScope();

            string component = "GlobalExceptionMicroService";
            string place = context.Request.Path;

            _logger.LogError(exception, "{Component} {Place} - Exception: {Message}", component, place, exception.Message);

            int statusCode = exception switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                type = exception.GetType().FullName,
                error = exception.Message,
                inner = exception.InnerException?.Message,
                stack = _env.IsDevelopment() ? exception.StackTrace : null,
                errorOccurred = true
            }));
        }


    }

}
