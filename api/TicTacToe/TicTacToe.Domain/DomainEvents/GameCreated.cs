using TicTacToe.Domain.Shared;

namespace TicTacToe.Domain.DomainEvents;

public sealed record GameCreated(Guid GameId) : IDomainEvent;
