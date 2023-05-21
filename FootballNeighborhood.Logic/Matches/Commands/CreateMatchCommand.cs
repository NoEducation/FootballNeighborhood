using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;
using MediatR;

namespace FootballNeighborhood.Logic.Matches.Commands;

public record CreateMatchCommand : CreateUpdateMatchBaseCommand, ICommand<SuccessMessageAndObjectId>,
    IRequest<OperationResult<SuccessMessage>>
{
    protected CreateMatchCommand(string name, DateTimeOffset startDateTime, DateTimeOffset endDateTime, string city,
        string addressLine, short allowedPlayers, short minPlayers, bool showEmailAddress, bool showPhoneNumber)
    {
        Name = name;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        City = city;
        AddressLine = addressLine;
        AllowedPlayers = allowedPlayers;
        MinPlayers = minPlayers;
        ShowEmailAddress = showEmailAddress;
        ShowPhoneNumber = showPhoneNumber;
    }
}