using TicTacToe.Domain.Enums;

namespace TicTacToe.Infrastructure.Documents
{
    internal record GameDocument(Guid Id, GameStatus Status, Tag InitialTag, Tag?[,] Cells)
    {
    }
}
