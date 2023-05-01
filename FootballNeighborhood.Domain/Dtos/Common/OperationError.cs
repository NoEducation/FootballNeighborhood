namespace FootballNeighborhood.Domain.Dtos.Common;

public class OperationError
{
    public OperationError(string error, string? key = null)
    {
        Error = error;
        Key = key;
    }

    public string Error { get; }
    public string? Key { get; }
}