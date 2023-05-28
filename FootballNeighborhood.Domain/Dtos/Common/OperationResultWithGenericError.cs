namespace FootballNeighborhood.Domain.Dtos.Common;

public class OperationResultWithGenericError : OperationResult
{
    public OperationResultWithGenericError(Guid code)
    {
        Code = code;
    }

    public OperationResultWithGenericError(string error,
        bool displayGenericException,
        Guid code) : base(error)
    {
        DisplayGenericException = displayGenericException;
        Code = code;
    }

    public bool UnexpectedErrorOccurred => true;
    public bool DisplayGenericException { get; set; }
    public Guid Code { get; }
}