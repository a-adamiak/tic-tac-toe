using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Helpers
{
    public static class TicTacToeGame
    {
        public static readonly List<(int row, int column)>[] WinLines = {
            new() { (0,0), (0,1), (0,2)},
            new() { (1,0), (1,1), (1,2)},
            new() { (2,0), (2,1), (2,2)},
            new() { (0,0), (1,0), (2,0)},
            new() { (0,1), (1,1), (2,1)},
            new() { (0,2), (1,2), (2,2)},
            new() { (0,0), (1,1), (2,2)},
            new() { (0,2), (1,1), (2,0)},
        };

        public static bool IsWinner(Tag?[,] game, Tag tag)
        {
            var selectedRow = GetSelectedCells(game, tag);

            return WinLines.Any(winningLine => winningLine.Intersect(selectedRow).Count() == winningLine.Count);
        }

        public static bool HasEmptyCell(Tag?[,] game)
        {
            var rowsLength = game.GetUpperBound(0) + 1;
            var columnsLength = game.GetUpperBound(1) + 1;

            for (var i = 0; i < rowsLength; i++)
            for (var j = 0; j < columnsLength; j++)
            {
                var value = game[i, j];
                if (value is null)
                    return true;
            }

            return false;
        }

        public static (int row, int column)? GetWinningMove(Tag?[,] game, Tag nextMove)
        {
            var winLines = WinLines;
            var playerSelectedMoves = GetSelectedCells(game, nextMove);
            var allSelectedMoves = GetSelectedCells(game, Tag.O, Tag.X);

            var lineForWin = winLines
                .FirstOrDefault(winningLine =>
                    winningLine.Intersect(playerSelectedMoves).Count() == winningLine.Count - 1 &&
                    winningLine.Intersect(allSelectedMoves).Count() == winningLine.Count - 1);

            return lineForWin?.Except(playerSelectedMoves).FirstOrDefault();
        }

        public static IEnumerable<(int row, int column)> GetSelectedCells(Tag?[,] game, params Tag?[] tags)
        {
            var selectedRows = new List<(int row, int column)>();
            var rowsLength = game.GetUpperBound(0) + 1;
            var columnsLength = game.GetUpperBound(1) + 1;

            for (var i = 0; i < rowsLength; i++)
            for (var j = 0; j < columnsLength; j++)
            {
                var value = game[i, j];
                if(tags.Contains(value))
                    selectedRows.Add((i, j));
            }

            return selectedRows;
        }
    }
}
