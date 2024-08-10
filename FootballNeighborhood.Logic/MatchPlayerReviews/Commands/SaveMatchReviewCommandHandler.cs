using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Entities.Matches;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.MatchPlayerReviews.Commands;

public class SaveMatchReviewCommandHandler : ICommandHandler<AddMatchReviewCommand, SuccessMessage>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;

    public SaveMatchReviewCommandHandler(Context context,
        IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(AddMatchReviewCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<SuccessMessage>();

        var playerMatch = await _context.MatchPlayers
                .SingleOrDefaultAsync(matchPlayer => matchPlayer.MatchId == request.MatchId
                    && matchPlayer.UserId == _userContext.CurrentUserId, cancellationToken);

        if (playerMatch?.MatchReviewScore is not null)
        {
            result.AddError(MatchPlayerReviewsResources.ReviewAlreadyAdded_ErrorMessage);
            return result;
        }

        await Save(request, playerMatch, cancellationToken);

        result.Result = new SuccessMessage()
        {
            Message = MatchPlayerReviewsResources.ReviewAdded_SuccessMessage
        };

        return result;
    }

    private async Task Save(AddMatchReviewCommand request, MatchPlayer? playerMatch, CancellationToken cancellationToken)
    {
        playerMatch!.MatchReviewScore = request.MatchReviewScore;
        playerMatch.MatchReviewDescription = request.MatchReviewDescription;

        if (request?.PlayerReviews is not null)
        {
            foreach (var review in request.PlayerReviews)
            {
                var playerReview = new MatchPlayerReview()
                {
                    ReviewedUserId = review.PlayerId,
                    ReviewedByUserId = this._userContext.CurrentUserId,
                    MatchId = request.MatchId,
                    Score = review.PlayerScore,
                    Description = review.PlayerScoreDescription
                };

                _context.MatchPlayerReviews.Attach(playerReview);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}

