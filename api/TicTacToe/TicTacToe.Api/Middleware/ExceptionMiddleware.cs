using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using TicTacToe.Api.Dto;
using TicTacToe.Application.Exceptions;
using TicTacToe.Domain.Exceptions;

namespace TicTacToe.Api.Middleware
{
    /// <summary>
    /// Exception middleware extensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Registers ExceptionMiddleware in application pipeline
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Request handler method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // we could add logging here

                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = ex switch
                {
                    CellAlreadyMarked => StatusCodes.Status400BadRequest,
                    GameAlreadyFinished => StatusCodes.Status400BadRequest,
                    InvalidCell => StatusCodes.Status400BadRequest,
                    GameNotExist => StatusCodes.Status404NotFound,
                    BotMoveFailed => StatusCodes.Status500InternalServerError,
                    InvalidTurn => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError,
                };

                var code = ex switch
                {
                    DomainException e => e.Code,
                    AppException e => e.Code,
                    _ => "InternalServerError"
                };
                
                var result = JsonSerializer.Serialize(new ErrorDto(code, ex.Message));
                await response.WriteAsync(result);
            }
        }
    }
}
