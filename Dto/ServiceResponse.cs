namespace Dto;

public class ServiceResponse<T>
{
    public bool Ok { get => Data is not null && !Errors.Any(); }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();

    public static ServiceResponse<T> NotOk(string error)
    {
        var res = new ServiceResponse<T>();
        res.Errors.Add(error);
        return res;
    }
}