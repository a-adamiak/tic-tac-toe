using TicTacToe.Domain.Entities;
using TicTacToe.Infrastructure.Documents;

namespace TicTacToe.Infrastructure.Extensions
{
    internal static class GameDocumentExtensions
    {
        public static Game ToEntity(this GameDocument document) =>
            new(document.Id, document.Status, document.InitialTag)
            {
                Cells = document.Cells
            };
    }
}
