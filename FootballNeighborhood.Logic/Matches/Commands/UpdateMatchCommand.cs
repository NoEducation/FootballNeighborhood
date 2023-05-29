using FootballNeighborhood.Domain.Dtos.Common;
using FootballNeighborhood.Infrastructure.Cqrs;

namespace FootballNeighborhood.Logic.Matches.Commands;

public record UpdateMatchCommand : CreateUpdateMatchBaseCommand, ICommand<SuccessMessage>
{
    public UpdateMatchCommand(string name, DateTimeOffset startDateTime, DateTimeOffset endDateTime, string city,
        string addressLine, short allowedPlayers, short minPlayers, bool showEmailAddress, bool showPhoneNumber,
        int matchId)
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
        MatchId = matchId;
    }

    public int MatchId { get; }
}