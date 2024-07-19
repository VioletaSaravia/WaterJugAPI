using System.Diagnostics;
using WaterJugAPI.Algorithms;
using Xunit.Abstractions;

namespace TestWaterJugAPI;

using Xunit;

public class TestIWaterJugSolver
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IWaterJugSolver _solver = new WaterJugSolver();

    public TestIWaterJugSolver(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Theory]
    [InlineData(3, 5, 4)]
    [InlineData(2, 6, 4)]
    [InlineData(7, 9, 8)]
    [InlineData(1, 3, 2)]
    [InlineData(5, 11, 6)]
    [InlineData(3, 0, 3)]
    [InlineData(3, 3, 3)]
    [InlineData(1, 1, 0)]
    [InlineData(10, 15, 5)]
    [InlineData(7, 3, 1)]
    public void TestSolverWaterJug_CachedResult(int x, int y, int z)
    {
        var watch = Stopwatch.StartNew();
        _ = _solver.Solve(x, y, z);
        watch.Stop();

        var watch2 = Stopwatch.StartNew();
        _ = _solver.Solve(x, y, z);
        watch2.Stop();

        _testOutputHelper.WriteLine(watch.ElapsedTicks.ToString());
        _testOutputHelper.WriteLine(watch2.ElapsedTicks.ToString());
        Assert.True(watch.ElapsedTicks > watch2.ElapsedTicks);
    }

    [Theory]
    [InlineData(3, 5, 4)]
    [InlineData(2, 6, 4)]
    [InlineData(7, 9, 8)]
    [InlineData(1, 3, 2)]
    [InlineData(5, 11, 6)]
    [InlineData(3, 0, 3)]
    [InlineData(0, 0, 0)]
    [InlineData(3, 3, 3)]
    [InlineData(1, 1, 0)]
    [InlineData(10, 15, 5)]
    [InlineData(7, 3, 1)]
    public void TestSolveWaterJug_ValidInput(int x, int y, int z)
    {
        var result = _solver.Solve(x, y, z);

        Assert.NotNull(result);
        Assert.True(z == result.Last().BucketY || z == result.Last().BucketX);
    }

    [Theory]
    [InlineData(8, 4, 6)]
    [InlineData(9, 6, 5)]
    [InlineData(6, 10, 7)]
    [InlineData(14, 7, 5)]
    [InlineData(12, 8, 5)]
    [InlineData(15, 10, 7)]
    [InlineData(18, 12, 11)]
    [InlineData(21, 14, 10)]
    [InlineData(20, 15, 8)]
    [InlineData(25, 10, 14)]
    [InlineData(30, 18, 13)]
    [InlineData(35, 21, 20)]
    [InlineData(24, 16, 11)]
    [InlineData(27, 18, 8)]
    [InlineData(22, 14, 9)]
    public void TestSolveWaterJug_ImpossibleSolution(int x, int y, int z)
    {
        var result = _solver.Solve(x, y, z);

        Assert.NotNull(result);
        Assert.True(result[0].Status == "No Solution");
    }

    [Theory]
    [InlineData(3, 5, 9)]
    [InlineData(2, 6, 10)]
    [InlineData(7, 9, 20)]
    [InlineData(1, 3, 5)]
    [InlineData(5, 11, 20)]
    [InlineData(8, 4, 15)]
    [InlineData(10, 7, 18)]
    [InlineData(6, 9, 16)]
    [InlineData(4, 4, 9)]
    [InlineData(15, 10, 30)]
    public void TestSolveWaterJug_ZGreaterThanBothBuckets(int x, int y, int z)
    {
        var result = _solver.Solve(x, y, z);

        Assert.True(result[0].Status == "No Solution");
    }

    [Theory]
    [InlineData(3, 5, 0)]
    [InlineData(2, 6, 0)]
    [InlineData(7, 9, 0)]
    [InlineData(1, 3, 0)]
    [InlineData(5, 11, 0)]
    [InlineData(8, 4, 0)]
    [InlineData(10, 7, 0)]
    [InlineData(6, 9, 0)]
    [InlineData(4, 4, 0)]
    [InlineData(15, 10, 0)]
    public void TestSolveWaterJug_TargetAmountZero(int x, int y, int z)
    {
        var result = _solver.Solve(x, y, z);

        Assert.Single(result);
        Assert.Equal(0, result[0].BucketX);
        Assert.Equal(0, result[0].BucketY);
    }

    [Theory]
    [InlineData(3, 5, 3)]
    [InlineData(3, 5, 5)]
    [InlineData(2, 6, 2)]
    [InlineData(2, 6, 6)]
    [InlineData(7, 9, 7)]
    [InlineData(7, 9, 9)]
    [InlineData(1, 3, 1)]
    [InlineData(1, 3, 3)]
    [InlineData(5, 11, 5)]
    [InlineData(5, 11, 11)]
    [InlineData(15, 10, 15)]
    [InlineData(15, 10, 10)]
    public void
        TestSolveWaterJug_TargetAmountEqualToOneBucket(int x, int y, int z)
    {
        var result = _solver.Solve(x, y, z);

        Assert.Single(result);
        Assert.True(z == result.Last().BucketY || z == result.Last().BucketX);
    }
}