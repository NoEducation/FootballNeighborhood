using FootballNeighborhood.Domain.Dtos.Common;
using MediatR;

namespace FootballNeighborhood.Infrastructure.Cqrs;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, OperationResult>
    where TCommand : IRequest<OperationResult>
{
}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, OperationResult<TResult>>
    where TCommand : IRequest<OperationResult<TResult>>
    where TResult : SuccessMessage
{
}