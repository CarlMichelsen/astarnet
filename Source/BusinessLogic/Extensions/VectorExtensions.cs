using Astar.Database.Entities;

namespace Astar.BusinessLogic.Extensions;

/// <summary>
/// Extension methods for Vector entity.
/// </summary>
public static class VectorExtensions
{
    /// <summary>
    /// Calculate the pythagorean distance between two vectors.
    /// </summary>
    /// <param name="current">From vector.</param>
    /// <param name="other">To vector.</param>
    /// <returns>Distance as float.</returns>
    public static float DistanceTo(this Vector current, Vector other)
    {
        var xDiff = other.X - current.X;
        var yDiff = other.Y - current.Y;
        var zDiff = other.Z - current.Z;
        double distance = Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff) + (zDiff * zDiff));
        return (float)distance;
    }
}