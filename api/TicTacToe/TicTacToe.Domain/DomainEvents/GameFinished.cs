using TicTacToe.Domain.Shared;

namespace TicTacToe.Domain.DomainEvents;

public sealed record GameFinished(Guid GameId) : IDomainEvent;
