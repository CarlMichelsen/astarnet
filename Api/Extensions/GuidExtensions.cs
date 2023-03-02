namespace Api.Extensions;

public static class GuidExtensions
{
    public static bool TryParseGuid(this IEnumerable<string> values, out IEnumerable<Guid> guids)
    {
        var parsedGuids = new List<Guid>();
        foreach (var value in values)
        {
            if (Guid.TryParse(value, out var parsedGuid))
            {
                parsedGuids.Add(parsedGuid);
            }
        }

        if (parsedGuids.Any())
        {
            guids = parsedGuids;
            return true;
        }

        guids = Enumerable.Empty<Guid>();
        return false;
    }
}