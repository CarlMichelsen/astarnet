namespace Astar.Dto;

/// <summary>
/// Common response wrapper for the Astar Api.
/// </summary>
/// <typeparam name="T">Generic used for dto classes.</typeparam>
public class ServiceResponse<T>
{
    /// <summary>
    /// Gets a value indicating whether the state for the ServiceResponse is Ok.
    /// </summary>
    /// <returns>Boolean that tells if the request was succesful.</returns>
    public bool Ok { get => this.Data is not null && !this.Errors.Any(); }

    /// <summary>
    /// Gets or sets the data of the ServiceResponse.
    /// This data a generic type.
    /// </summary>
    /// <value>Generic type.</value>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets list of Errors that is relevant if the Ok property is true.
    /// </summary>
    /// <returns>A list of errors as strings.</returns>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Helper method for making oneliner ServiceResponse objects with the Ok property set to false and a single error.
    /// </summary>
    /// <param name="error">Used to add a single error to the Errors list.</param>
    /// <returns>Not Ok ServiceResponse with an error.</returns>
    public static ServiceResponse<T> NotOk(string error)
    {
        var res = new ServiceResponse<T>();
        res.Errors.Add(error);
        return res;
    }
}