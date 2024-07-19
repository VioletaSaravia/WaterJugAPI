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

    [Fact]
    public void TestSolveWaterJug_ValidInput()
    {
        int x = 3;
        int y = 5;
        int z = 4;

        var result = _solver.Solve(x, y, z);

        Assert.NotNull(result);
        Assert.True(4 == result.Last().BucketY || 4 == result.Last().BucketX);
    }

    [Fact]
    public void TestSolverWaterJug_CachedResult()
    {
        int x = 2;
        int y = 96;
        int z = 4;

        var watch = Stopwatch.StartNew();
        _ = _solver.Solve(x, y, z);
        watch.Stop();
        
        var watch2 = Stopwatch.StartNew();
        _ = _solver.Solve(x, y, z);
        watch2.Stop();
        
        _testOutputHelper.WriteLine(watch.ElapsedMilliseconds.ToString());
        _testOutputHelper.WriteLine(watch2.ElapsedMilliseconds.ToString());
        Assert.True(watch.ElapsedMilliseconds > watch2.ElapsedMilliseconds);
    }

    [Fact]
    public void TestSolveWaterJug_ImpossibleSolution()
    {
        int x = 2;
        int y = 6;
        int z = 5;

        var result = _solver.Solve(x, y, z);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public void TestSolveWaterJug_InvalidInput()
    {
        int x = 0;
        int y = 5;
        int z = 4;

        var result = _solver.Solve(x, y, z);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public void TestSolveWaterJug_ZGreaterThanBothBuckets()
    {
        int x = 3;
        int y = 5;
        int z = 9;

        var result = _solver.Solve(x, y, z);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public void TestSolveWaterJug_TargetAmountZero()
    {
        int x = 3;
        int y = 5;
        int z = 0;

        var result = _solver.Solve(x, y, z);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public void
        TestSolveWaterJug_TargetAmountEqualToOneBucket()
    {
        int x = 3;
        int y = 5;
        int z = 3;

        var result = _solver.Solve(x, y, z);

        Assert.NotNull(result);
        Assert.True(3 == result.Last().BucketY || 3 == result.Last().BucketX);
    }
}