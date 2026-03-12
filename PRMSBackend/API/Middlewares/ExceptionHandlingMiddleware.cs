using Application.Validators;
using ERPClean.Helper;

namespace API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        readonly RequestDelegate _next;
        readonly ILogger<ExceptionHandlingMiddleware> _logger;
        readonly IHostEnvironment _env;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger
            , IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException oce) when (!context.Response.HasStarted)
            {
                //This is normal usecase(client cancelled the request), so that don't log it as error.
                if (context.RequestAborted.IsCancellationRequested)
                {
                    //_logger.LogInformation(oce, "Client cancelled. TraceId={TraceId}", context.TraceIdentifier);
                    _logger.LogInformation("Client cancelled.");
                    //context.Response.StatusCode = 499;//Client Closed Request
                }
                else
                {
                    //_logger.LogInformation(oce, "Timeout. TraceId={TraceId}", context.TraceIdentifier);
                    _logger.LogInformation("Timeout.");
                    //context.Response.StatusCode = StatusCodes.Status408RequestTimeout;
                }
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning("Business exception: {Message}", ex.Message);
                //context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var response = APIResponse<string>.Fail(ex.Message);
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (DataAccessException ex)
            {
                _logger.LogError("Data access exception: {Message}", _env.IsDevelopment() ? ex.InnerException : ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = APIResponse<string>.Fail(_env.IsDevelopment()
                    ? ex.InnerException?.ToString() ?? ex.Message
                    : ex.Message);
                await context.Response.WriteAsJsonAsync(response);
            }
            //catch (KeyNotFoundException ex)
            //{
            //    _logger.LogError("Data access exception: {Message}", _env.IsDevelopment() ? ex.InnerException : ex.Message);
            //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //    var response = APIResponse<string>.Fail(_env.IsDevelopment()
            //        ? ex.InnerException?.ToString() ?? ex.Message
            //        : ex.Message);
            //    await context.Response.WriteAsJsonAsync(response);
            //}
            catch (Exception ex)
            {
                _logger.LogError(_env.IsDevelopment() ? ex.Message : "Internal server error occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = APIResponse<string>.Fail(_env.IsDevelopment() 
                    ? ex.Message
                    : "Internal server error occurred.");

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
