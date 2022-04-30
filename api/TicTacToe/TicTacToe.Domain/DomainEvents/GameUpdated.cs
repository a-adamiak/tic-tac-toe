using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Shared;

namespace TicTacToe.Domain.DomainEvents;

public sealed record GameUpdated(Guid GameId, Tag Tag) : IDomainEvent;
