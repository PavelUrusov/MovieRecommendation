using Microsoft.AspNetCore.Mvc;

namespace MovieRecommendation.API.Controllers;

/// <summary>
///     Controller for a simple "Hi" endpoint.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DevController : ControllerBase
{
    /// <summary>
    ///     Returns a "Hi!" message.
    /// </summary>
    /// <returns>An IActionResult representing the "Hi!" message.</returns>
    [HttpGet("[action]")]
    public IActionResult Hi()
    {
        return StatusCode(200, "Hi!");
    }
}