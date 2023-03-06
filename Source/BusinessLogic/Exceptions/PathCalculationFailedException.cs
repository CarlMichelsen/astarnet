namespace BusinessLogic.Exceptions;

/// <summary>
/// An exception for when a path calculation fails.
/// </summary>
public class PathCalculationFailedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PathCalculationFailedException"/> class.
    /// </summary>
    /// <param name="message">ExceptionMessage.</param>
    public PathCalculationFailedException(string message)
        : base(message)
    {
    }
}