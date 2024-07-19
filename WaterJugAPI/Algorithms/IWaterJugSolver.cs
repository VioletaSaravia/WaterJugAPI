namespace WaterJugAPI.Algorithms;

public interface IWaterJugSolver
{
    public List<Step> Solve(int x, int y, int z);
}

public enum WaterJugAction
{
    Start,
    Fill1,
    Fill2,
    Empty1,
    Empty2,
    Pour1To2,
    Pour2To1,
}

public struct Step(int bucketX, int bucketY, string action)
{
    public int BucketX { get; set; } = bucketX;
    public int BucketY { get; set; } = bucketY;
    public string Action { get; set; } = action;
}