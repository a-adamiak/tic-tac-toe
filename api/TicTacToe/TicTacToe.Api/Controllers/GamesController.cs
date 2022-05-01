using System.Net.Mime;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Dto;
using TicTacToe.Application.Commands.CreateGame;
using TicTacToe.Application.Commands.DeleteGame;
using TicTacToe.Application.Commands.MarkSpace;
using TicTacToe.Application.Queries.GetAllGames;
using TicTacToe.Application.Queries.GetGame;
using TicTacToe.Application.Shared;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Exceptions;

namespace TicTacToe.Api.Controllers
{
    /// <summary>
    /// Handler for the game
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/games")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class GamesController: ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public GamesController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        /// <summary>
        /// Get all existing games
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GameDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGames(CancellationToken cancellationToken = default)
        {
            var games = await _queryDispatcher.QueryAsync(new GetAllGames(), cancellationToken);

            return new OkObjectResult(games.ToDto());
        }

        /// <summary>
        /// Get game by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = nameof(GetGame))]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGame(string id, CancellationToken cancellationToken = default)
        {
            if(!Guid.TryParse(id, out var result))
            {
                throw new GameNotExist(id);
            };

            var game = await _queryDispatcher.QueryAsync(new GetGame(result), cancellationToken);

            return new OkObjectResult(game.ToDto());
        }

        /// <summary>
        /// Initialize a new game
        /// </summary>
        /// <param name="initialTag"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGame([FromBody] TagDto initialTag)
        {
            var guidForNewGame = Guid.NewGuid();
            await _commandDispatcher.SendAsync(new CreateGame(guidForNewGame, initialTag.ToValue()));

            return new AcceptedAtRouteResult(nameof(GetGame), new { id = guidForNewGame }, guidForNewGame);
        }

        /// <summary>
        /// Mark cell 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        [HttpPost("{id}/cells/{row}/{column}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarkCell(Guid id, int row, int column)
        {
            await _commandDispatcher.SendAsync(new MarkCell(id, Tag.X, row, column));

            var game = await _queryDispatcher.QueryAsync(new GetGame(id));

            return new OkObjectResult(game.ToDto());
        }

        /// <summary>
        /// Delete the game
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            await _commandDispatcher.SendAsync(new DeleteGame(id));

            return new OkObjectResult(id);
        }
    }
}
