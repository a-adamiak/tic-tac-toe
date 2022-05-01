using TicTacToe.Domain.Constants;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Exceptions;
using TicTacToe.Domain.Helpers;

namespace TicTacToe.Domain.Entities;

public sealed class Game
{
    private Tag? _lastTag;
    private object _lockObj = new ();

    public Game(Guid id, GameStatus status, Tag initialTag, DateTime creationDate)
    {
        Id = id;
        InitialTag = initialTag;
        Status = status;
        Cells = new Tag?[CellSizes.RowsLength, CellSizes.ColumnsLength];
        CreationDate = creationDate;
    }

    public Guid Id { get; }
    public DateTime CreationDate { get; }

    // In ideal work it should be immutable but to simplify I don't do this
    public Tag?[,] Cells { get; init; }
    public GameStatus Status { get; private set; }

    public Tag InitialTag { get; }
    
    public void Delete()
    {
        if (Status == GameStatus.Deleted)
            throw new GameNotExist(Id.ToString());

        Status = GameStatus.Deleted;
    }

    public void MarkAsFailed()
    {
        Status = GameStatus.Failed;
    }

    public void MarkCell(Tag tag, int row, int cell)
    {
        lock (_lockObj)
        {
            if (_lastTag == tag)
                throw new InvalidTurn(tag);

            if (Status != GameStatus.InProgress)
                throw new GameAlreadyFinished(Id);

            var currentValue = Cells[row, cell];

            if (currentValue is not null)
                throw new CellAlreadyMarked(Id, row, cell, currentValue.Value);

            Cells[row, cell] = tag;

            Status = GetNewState(tag);

            _lastTag = tag;
        }
    }

    public static Game Create(Guid id, Tag firstTag) => new(id, GameStatus.InProgress, firstTag, DateTime.UtcNow);

    // It is a place for possible optimization 
    private GameStatus GetNewState(Tag lastMove) =>
        lastMove switch
        {
            var move when TicTacToeGame.IsWinner(Cells, move) => GetWinnerStatus(move),
            { } when TicTacToeGame.HasEmptyCell(Cells) => GameStatus.InProgress,
            _ => GameStatus.Draw
        };

    private static GameStatus GetWinnerStatus(Tag tag) => tag == Tag.O ? GameStatus.OWon : GameStatus.XWon;
}