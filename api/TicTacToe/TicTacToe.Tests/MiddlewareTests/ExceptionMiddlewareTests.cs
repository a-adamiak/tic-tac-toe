using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using TicTacToe.Api.Middleware;
using TicTacToe.Application.Exceptions;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Tests.Utils;
using Xunit;

namespace TicTacToe.Tests.MiddlewareTests
{
    public class ExceptionMiddlewareTests
    {

        [Theory, AutoMoqData]
        public async Task InvokeAsync_ShouldReturnBadRequest_WhenCellAlreadyMarked(CellAlreadyMarked ex)
        {
            var sut = new ExceptionMiddleware((context) => throw ex);

            var context = new DefaultHttpContext();

            await sut.InvokeAsync(context);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Theory, AutoMoqData]
        public async Task InvokeAsync_ShouldReturnBadRequest_WhenGameAlreadyFinished(GameAlreadyFinished ex)
        {
            var sut = new ExceptionMiddleware((context) => throw ex);

            var context = new DefaultHttpContext();

            await sut.InvokeAsync(context);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Theory, AutoMoqData]
        public async Task InvokeAsync_ShouldReturnBadRequest_WhenInvalidCell(InvalidCell ex)
        {
            var sut = new ExceptionMiddleware((context) => throw ex);

            var context = new DefaultHttpContext();

            await sut.InvokeAsync(context);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Theory, AutoMoqData]
        public async Task InvokeAsync_ShouldReturnNotFound_WhenGameNotExist(GameNotExist ex)
        {
            var sut = new ExceptionMiddleware((context) => throw ex);

            var context = new DefaultHttpContext();

            await sut.InvokeAsync(context);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Theory, AutoMoqData]
        public async Task InvokeAsync_ShouldReturnInternalServerError_WhenBotMoveFailed(BotMoveFailed ex)
        {
            var sut = new ExceptionMiddleware((context) => throw ex);

            var context = new DefaultHttpContext();

            await sut.InvokeAsync(context);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [Theory, AutoMoqData]
        public async Task InvokeAsync_ShouldReturnInternalServerError_WhenInvalidTurn(InvalidTurn ex)
        {
            var sut = new ExceptionMiddleware((context) => throw ex);

            var context = new DefaultHttpContext();

            await sut.InvokeAsync(context);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Theory, AutoMoqData]
        public async Task InvokeAsync_ShouldReturnInternalServerError_WhenGenericException(Exception ex)
        {
            var sut = new ExceptionMiddleware((context) => throw ex);

            var context = new DefaultHttpContext();

            await sut.InvokeAsync(context);

            context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }
    }
}