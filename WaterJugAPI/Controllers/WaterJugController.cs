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
        public int XCapacity { get; set; }
        public int YCapacity { get; set; }
        public int ZAmountWanted { get; set; }
    }

    [HttpPost("solve")]
    public ActionResult<List<Step>> SolveWaterJug(
        [FromBody] WaterJugRequest request) => request switch
    {
        { XCapacity: <= 0 } => BadRequest("Invalid input value x <= 0."),
        { YCapacity: <= 0 } => BadRequest("Invalid input value y <= 0."),
        { ZAmountWanted: < 0 } => BadRequest("Invalid input value z < 0."),
        _ => Ok(waterJugSolver.Solve(request.XCapacity,
            request.YCapacity, request.ZAmountWanted))
    };
}