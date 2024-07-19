using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WaterJugAPI.Algorithms;
using WaterJugApi.Controllers;

namespace TestWaterJugAPI;

public class TestWaterJugController
{
    private readonly Mock<IWaterJugSolver> _mockWaterJugSolver;
    private readonly WaterJugController _controller;

    public TestWaterJugController()
    {
        _mockWaterJugSolver = new Mock<IWaterJugSolver>();
        _controller = new WaterJugController(_mockWaterJugSolver.Object);
    }

    [Theory]
    [InlineData(-1, 5, 4, "Invalid input value x <= 0.")]
    [InlineData(0, 5, 4, "Invalid input value x <= 0.")]
    [InlineData(3, -5, 4, "Invalid input value y <= 0.")]
    [InlineData(3, 0, 4, "Invalid input value y <= 0.")]
    [InlineData(3, 5, -1, "Invalid input value z < 0.")]
    public async void SolveWaterJug_InvalidInput(int xCapacity,
        int yCapacity, int zAmountWanted, string expectedMessage)
    {
        var request = new WaterJugController.WaterJugRequest
        {
            XCapacity = xCapacity,
            YCapacity = yCapacity,
            ZAmountWanted = zAmountWanted
        };

        var result = (await _controller.SolveWaterJug(request)).Result;

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(expectedMessage, badRequestResult.Value);
    }
}