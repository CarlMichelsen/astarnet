using Database.Entities;

namespace Test.BusinessLogicTest.Testdata;

public abstract class BaseTestNodeset
{
    public abstract Nodeset Set { get; }
    public abstract LinkedList<Guid> Path { get; }
}