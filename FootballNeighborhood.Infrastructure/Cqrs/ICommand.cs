using FootballNeighborhood.Domain.Dtos.Common;
using MediatR;

namespace FootballNeighborhood.Infrastructure.Cqrs;

public interface ICommand : IRequest<OperationResult>
{
}