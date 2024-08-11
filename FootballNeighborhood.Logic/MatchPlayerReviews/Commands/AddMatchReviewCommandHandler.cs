using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Domain.Entities.Matches;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Resources;
using FootballNeighborhood.Services.Contexts;
using FootballNeighborhood.Services.UserContext;
using Microsoft.EntityFrameworkCore;

namespace FootballNeighborhood.Logic.MatchPlayerReviews.Commands;

public class AddMatchReviewCommandHandler : ICommandHandler<AddMatchReviewCommand, SuccessMessage>
{
    private readonly Context _context;
    private readonly IUserContext _userContext;

    public AddMatchReviewCommandHandler(Context context,
        IUserContext userContext)
    {
        _context = context;
        _userContext = userContext;
    }

    public async Task<OperationResult<SuccessMessage>> Handle(AddMatchReviewCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<SuccessMessage>();

        var playerMatch = await _context.MatchPlayers
                .Include(x => x.Match)
                .SingleOrDefaultAsync(matchPlayer => matchPlayer.MatchId == request.MatchId
                    && matchPlayer.UserId == _userContext.CurrentUserId, cancellationToken);

        if (playerMatch?.MatchReviewScore is not null)
        {
            result.AddError(MatchPlayerReviewsResources.ReviewAlreadyAdded_ErrorMessage);
            return result;
        }

        SaveReviews(request, playerMatch, cancellationToken);
        FinishMatchWhenRequested(request, result, playerMatch);

        if (!result.Success) return result;

        await _context.SaveChangesAsync(cancellationToken);

        result.Result = new SuccessMessage()
        {
            Message = MatchPlayerReviewsResources.ReviewAdded_SuccessMessage
        };

        return result;
    }

    private void FinishMatchWhenRequested(AddMatchReviewCommand request, OperationResult<SuccessMessage> result, MatchPlayer? playerMatch)
    {
        if (request.FinishMatch)
        {
            if (_userContext.CurrentUserId != playerMatch!.Match!.OwnerId)
            {
                result.AddError(MatchPlayerReviewsResources.OnlyMatchOwnerCanFinishMatch_ErrorMessage);
                return;
            }

            if (playerMatch.Match!.IsFinished)
            {
                result.AddError(MatchesResources.MatchAlreadyFinished_ErrorMessage);
                return;
            }

            playerMatch.Match.IsFinished = true;
        }
    }

    private void SaveReviews(AddMatchReviewCommand request, MatchPlayer? playerMatch, CancellationToken cancellationToken)
    {
        playerMatch!.MatchReviewScore = request.MatchReviewScore;
        playerMatch.MatchReviewDescription = request.MatchReviewDescription;
        playerMatch.MatchOwnerReviewScore = request.MatchOwnerReviewScore;

        if (request?.PlayerReviews is not null)
        {
            var reviews = request.PlayerReviews.Select(review =>
                new MatchPlayerReview()
                {
                    ReviewedPlayerId = review.UserId,
                    MatchPlayerId = playerMatch.Id,
                    Score = review.PlayerScore,
                    Description = review.PlayerScoreDescription
                }
            );

            _context.MatchPlayerReviews.AttachRange(reviews);
        }
    }
}

