using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using FootballNeighborhood.Services.Repositories;

namespace FootballNeighborhood.Logic.ConfirmUsers.Queries
{
    public class CheckUserHasActiveConfirmationQueryHandler : IQueryHandler<CheckUserHasActiveConfirmationQuery, CheckUserHasActiveConfirmationQueryResult>
    {
        private readonly IUserConfirmationRepository _userConfirmationRepository;

        public CheckUserHasActiveConfirmationQueryHandler(IUserConfirmationRepository userConfirmationRepository)
        {
            _userConfirmationRepository = userConfirmationRepository;
        }

        public async Task<OperationResult<CheckUserHasActiveConfirmationQueryResult>> Handle(CheckUserHasActiveConfirmationQuery request, CancellationToken cancellationToken)
        {
            var isConfirmationActive = await _userConfirmationRepository
                .IsConfirmationActiveForUserId(request.UserId);

            return new OperationResult<CheckUserHasActiveConfirmationQueryResult>()
            {
                Result = new CheckUserHasActiveConfirmationQueryResult()
                {
                    IsConfirmationActive = isConfirmationActive
                }
            };
        }
    }
}
