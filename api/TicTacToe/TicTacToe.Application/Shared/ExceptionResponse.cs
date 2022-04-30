using System.Net;

namespace TicTacToe.Application.Shared;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);