using System.Collections.Concurrent;

namespace WaterJugAPI.Algorithms;

public class WaterJugSolver : IWaterJugSolver
{
    private static readonly ConcurrentDictionary<(int, int, int), List<Step>>
        CachedResults = [];

    private static int Gcd(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    public List<Step> Solve(int x, int y, int z)
    {
        if (CachedResults.ContainsKey((x, y, z)))
            return CachedResults[(x, y, z)];

        List<Step> result = [new Step(0, 0, "No Solution")];

        if (z > Math.Max(x, y) || z % Gcd(x, y) != 0) return result;

        var queue = new Queue<(int, int)>();
        var visited = new HashSet<(int, int)>();
        var steps = new Dictionary<(int, int), (int, int, WaterJugAction)>();

        queue.Enqueue((0, 0));
        visited.Add((0, 0));
        steps[(0, 0)] = (0, 0, WaterJugAction.Start);

        Span<(int, int, WaterJugAction)> nextStates =
            stackalloc (int, int, WaterJugAction)[6];

        while (queue.Count > 0)
        {
            var (a, b) = queue.Dequeue();

            if (a == z || b == z)
            {
                result = FormatSteps(steps, (a, b)).ToList();
                CachedResults.TryAdd((x, y, z), result);
                return result;
            }

            nextStates[0] = (x, b, WaterJugAction.Fill1);
            nextStates[1] = (a, y, WaterJugAction.Fill2);
            nextStates[2] = (0, b, WaterJugAction.Empty1);
            nextStates[3] = (a, 0, WaterJugAction.Empty2);
            nextStates[4] = (Math.Max(0, a - (y - b)), Math.Min(y, b + a),
                WaterJugAction.Pour1To2);
            nextStates[5] = (Math.Min(x, a + b), Math.Max(0, b - (x - a)),
                WaterJugAction.Pour2To1);

            foreach (var (nextA, nextB, action) in nextStates)
            {
                if (visited.Contains((nextA, nextB))) continue;

                queue.Enqueue((nextA, nextB));
                visited.Add((nextA, nextB));
                steps[(nextA, nextB)] = (a, b, action);
            }
        }

        CachedResults.TryAdd((x, y, z), result);
        return result;
    }

    private static List<Step> FormatSteps(
        Dictionary<(int, int), (int, int, WaterJugAction)> steps,
        (int, int) endState)
    {
        var result = new List<Step>();

        var currentState = endState;
        while ((steps[currentState].Item1, steps[currentState].Item2) !=
               currentState)
        {
            var (prevA, prevB, action) = steps[currentState];
            result.Add(new Step(currentState.Item1, currentState.Item2, ToString(action)));
            currentState = (prevA, prevB);
        }

        result.Reverse();
        return result.ToList();
    }

    private static string ToString(WaterJugAction act) => act switch
    {
        WaterJugAction.Start => "",
        WaterJugAction.Fill1 => "Fill Jug 1",
        WaterJugAction.Fill2 => "Fill Jug 2",
        WaterJugAction.Empty1 => "Empty Jug 1",
        WaterJugAction.Empty2 => "Empty Jug 2",
        WaterJugAction.Pour1To2 => "Pour Jug 1 to 2",
        WaterJugAction.Pour2To1 => "Pour Jug 2 to 1",
        _ => throw new ArgumentOutOfRangeException(nameof(act), act, null)
    };
}