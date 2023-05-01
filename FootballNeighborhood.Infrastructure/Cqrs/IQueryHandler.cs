using FootballNeighborhood.Domain.Dtos.Common;
using MediatR;

namespace FootballNeighborhood.Infrastructure.Cqrs;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, OperationResult<TResult>>
    where TQuery : IRequest<OperationResult<TResult>>
{
}