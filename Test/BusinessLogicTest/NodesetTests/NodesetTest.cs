using BusinessLogic.Calculators.Interface;
using Test.BusinessLogicTest.Testdata;

namespace Test.BusinessLogicTest.NodesetTests;

public class NodesetTest
{
    private readonly StraightTestNodeset nodesetData;
    private readonly IPathCalculator pathCalculator;

    public NodesetTest(StraightTestNodeset nodesetData, IPathCalculator pathCalculator)
    {
        this.nodesetData = nodesetData;
        this.pathCalculator = pathCalculator;
    }

    [Fact]
    public void StraightTestNodesetNotImplemented() //TODO: Have actual testdata <3
    {
        Assert.Throws<NotImplementedException>(() => nodesetData.Set);
        Assert.Throws<NotImplementedException>(() => nodesetData.Path);
    }

    [Fact]
    public void AssertTrue()
    {
        Assert.True(true);
    }
}