using System.Text.Json.Serialization;

namespace WaterJugAPI.Algorithms;

public interface IWaterJugSolver
{
    public List<Step> Solve(int x, int y, int z);
}

public struct Step(int bucketX, int bucketY, string action, string? status)
{
    public int BucketX { get; set; } = bucketX;
    public int BucketY { get; set; } = bucketY;
    public string Action { get; set; } = action;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; set; } = status;
}