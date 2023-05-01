using FootballNeighborhood.Domain.Dtos.Common;
using MediatR;

namespace FootballNeighborhood.Infrastructure.Cqrs;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, OperationResult>
    where TCommand : IRequest<OperationResult>
{
}