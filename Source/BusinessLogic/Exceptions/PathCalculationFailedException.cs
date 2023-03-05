namespace BusinessLogic.Exceptions;

public class PathCalculationFailedException : Exception
{
    public PathCalculationFailedException(string message) : base(message)
    {
    }
}