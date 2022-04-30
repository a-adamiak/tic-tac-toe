using TicTacToe.Domain.Shared;

namespace TicTacToe.Domain.DomainEvents;

public sealed record GameDeleted(Guid GameId) : IDomainEvent;
