using ENINET.TransparentPortal.API.Controllers;

namespace ENINET.TransparentPortal.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        if (next is null)
        {
            throw new ArgumentNullException(nameof(next));
        }

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException exception)
        {
            _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);
            var problemDetails = new ApiResult<string>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"Client Error: Utente non autorizzato!",
                Data = ""

            };

            context.Response.StatusCode =
            StatusCodes.Status403Forbidden;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (BadHttpRequestException exception)
        {
            _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);
            var problemDetails = new ApiResult<string>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"Client Error: {exception.Message}",
                Data = ""

            };

            context.Response.StatusCode =
            StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception exception)
        {
            _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

            var problemDetails = new ApiResult<string>
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = $"Server Error: {exception.Message}",
                Data = ""

            };
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }


}
