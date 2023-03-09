using Astar.Database.Entities;

namespace Astar.BusinessLogicTest.Testdata;

public abstract class BaseTestNodeset
{
    public abstract Nodeset Set { get; }
    public abstract LinkedList<Guid> Path { get; }
}