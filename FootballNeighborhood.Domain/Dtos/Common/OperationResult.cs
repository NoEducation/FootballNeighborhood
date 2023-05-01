namespace FootballNeighborhood.Domain.Dtos.Common
{
    public class OperationResult<T> : OperationResult
    {
        public T? Result { get; set; }

        public OperationResult(T result)
        {
            Result = result;
        }

        public OperationResult() => Result = default;
    }

    public class OperationResult
    {
        public bool Success => Error is null;

        public OperationError? Error { get; private set; }

        public OperationResult()
        { }

        public OperationResult(string error, string? key = null)
        {
            AddError(error, key);
        }

        public OperationResult(OperationError error)
        {
            AddError(error);
        }

        public OperationResult(OperationResult operationResult)
        {
            AddErrors(operationResult);
        }

        public void AddError(string reason, string? key = null)
        {
            if(Error is not null)
                throw new ArgumentException("In OperationResult error is already defined",nameof(reason))

            Error = new OperationError(reason, key);
        }

        public void AddError(OperationError reason)
        {
            if (Error is not null)
                throw new ArgumentException("In OperationResult error is already defined", nameof(reason))

            Error = reason;
        }

        public void AddErrors(OperationResult operationResult)
        {
            if (operationResult.Error is null)
                throw new ArgumentException("Operation result has not defined error", nameof(operationResult));

            AddError(operationResult.Error);
        }

        public static OperationResult Yes() => new OperationResult();

        public static OperationResult No(string error, string? key = null) => new OperationResult(error, key);

        public static OperationResult No(OperationError error) => new OperationResult(error);

    };
}