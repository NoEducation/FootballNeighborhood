using FootballNeighborhood.Domain.Dtos.Common;
using MediatR;

namespace FootballNeighborhood.Infrastructure.Cqrs;

public interface IQuery<TResponse> : IRequest<OperationResult<TResponse>>
{
}