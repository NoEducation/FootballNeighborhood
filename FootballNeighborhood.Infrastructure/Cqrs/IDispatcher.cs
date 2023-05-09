using FootballNeighborhood.Domain.Dtos.Common;

namespace FootballNeighborhood.Infrastructure.Cqrs;

public interface IDispatcher
{
    Task<OperationResult<TResult>> SendAsync<TResult>(IQuery<TResult> query, CancellationToken token = default);

    Task<OperationResult> SendAsync(ICommand command, CancellationToken token = default);

    Task<OperationResult<SuccessMessage>> SendAsync(ICommand<SuccessMessage> command,
        CancellationToken token = default);

    Task<OperationResult<SuccessMessageAndObjectId>> SendAsync(
        ICommand<SuccessMessageAndObjectId> command, CancellationToken token = default);
}