using FootballNeighborhood.Domain.Dtos.Common;
using MediatR;

namespace FootballNeighborhood.Infrastructure.Cqrs;

public interface ICommand : IRequest<OperationResult>
{
}

public interface ICommand<TResponse> : IRequest<OperationResult<TResponse>>
    where TResponse : SuccessMessage
{
}