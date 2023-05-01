using FootballNeighborhood.Domain.Dtos.Common;
using MediatR;

namespace FootballNeighborhood.Infrastructure.Cqrs;

public class Dispatcher : IDispatcher
{
    private readonly IMediator _mediator;

    public Dispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<TResult>> SendAsync<TResult>(IQuery<TResult> query,
        CancellationToken token = default)
    {
        var result = await _mediator.Send(query, token);

        return result;
    }

    public async Task<OperationResult> SendAsync(ICommand command, CancellationToken token = default)
    {
        var result = await _mediator.Send(command, token);

        return result;
    }
}