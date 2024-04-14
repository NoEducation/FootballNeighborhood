using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.Matches.Commands
{
    public class RemoveMatchCommandHandler : ICommandHandler<RemoveMatchCommand, SuccessMessage>
    {
        private readonly Context _context;

        public RemoveMatchCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<OperationResult<SuccessMessage>> Handle(RemoveMatchCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<SuccessMessage>();
            // Tutaj trzeba sprawdzić czy spotkanie sie jeszcze nie rozpoczeło

            var match = await _context.Matches
                .Include(match => match.MatchPlayers)
                .SingleOrDefaultAsync(match => match.Id == request.MatchId, cancellationToken);

            if(match is null)
            {
                result.AddError(MatchesResources.MatchDoesNotExists_ErrorMessage);
                return result;
            }

            _context.RemoveRange(match.MatchPlayers);
            _context.Remove(match);

            await _context.SaveChangesAsync();

            result.Result = new SuccessMessage()
            {
                Message = MatchesResources.MatchDeleted_Message
            };

            return result;
        }
    }
}
