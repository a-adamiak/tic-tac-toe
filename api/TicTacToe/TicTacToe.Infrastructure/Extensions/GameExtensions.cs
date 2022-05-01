using TicTacToe.Domain.Entities;
using TicTacToe.Infrastructure.Documents;

namespace TicTacToe.Infrastructure.Extensions
{
    internal static class GameExtensions
    {
        public static GameDocument ToDocument(this Game document) =>
            new GameDocument(document.Id, document.Status, document.InitialTag, document.Cells);
    }
}
