namespace Database.Entities;

public class Vector
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public float DistanceTo(Vector other)
    {
        var xDiff = other.X - X;
        var yDiff = other.Y - Y;
        var zDiff = other.Z - Z;
        double distance = Math.Sqrt(xDiff * xDiff + yDiff * yDiff + zDiff * zDiff);
        return (float)distance;
    }
}