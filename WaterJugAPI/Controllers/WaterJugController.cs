using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using WaterJugAPI.Algorithms;

namespace WaterJugApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WaterJugController(IWaterJugSolver waterJugSolver)
    : ControllerBase
{
    public struct WaterJugRequest
    {
        [JsonPropertyName("x_capacity")] public int XCapacity { get; set; }

        [JsonPropertyName("y_capacity")] public int YCapacity { get; set; }

        [JsonPropertyName("z_amount_wanted")]
        public int ZAmountWanted { get; set; }
    }

    [HttpPost("solve")]
    public async Task<ActionResult<List<Step>>> SolveWaterJug(
        [FromBody] WaterJugRequest request)
    {
        switch (request)
        {
            case { XCapacity: <= 0 }:
                return BadRequest("Invalid input value x <= 0.");
            case { YCapacity: <= 0 }:
                return BadRequest("Invalid input value y <= 0.");
            case { ZAmountWanted: < 0 }:
                return BadRequest("Invalid input value z < 0.");
            default:
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(1000),
                    HttpContext.RequestAborted);
                var solver = Task.Run(() => waterJugSolver.Solve(
                    request.XCapacity,
                    request.YCapacity, request.ZAmountWanted));

                var completedTask = await Task.WhenAny(solver, timeoutTask);

                return completedTask == timeoutTask
                    ? StatusCode(504, "Request timed out.")
                    : Ok(await solver);
        }
    }
}