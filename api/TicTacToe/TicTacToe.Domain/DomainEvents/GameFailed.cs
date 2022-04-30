using TicTacToe.Domain.Shared;

namespace TicTacToe.Domain.DomainEvents;

public sealed record GameFailed(Guid GameId, string Reason) : IDomainEvent;
