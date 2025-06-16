using Microsoft.AspNetCore.Mvc;
using PowerOverInternet.Services;

namespace PowerOverInternet.Controllers;

[ApiController]
[Route("[controller]")]
public class PowerController(ILogger<PowerController> logger, OutletService outletService): ControllerBase {

    [HttpPut]
    public IActionResult setOutletPower([FromQuery] string outletHostname, [FromQuery] OutletAction turnOn, [FromQuery] int? socketId = null, [FromQuery] int delaySec = 0) {
        delaySec = Math.Max(0, delaySec);
        string action = turnOn switch {
            OutletAction.FALSE  => "turn off",
            OutletAction.TRUE   => "turn on",
            OutletAction.TOGGLE => "toggle"
        };
        logger.LogInformation("{name} will {action} in {sec:N0} seconds", outletHostname, action, delaySec);

        Task.Delay(TimeSpan.FromSeconds(delaySec)).ContinueWith(async _ => {
            try {
                logger.LogInformation("{name} will {action} now", outletHostname, action);
                await outletService.setPowerState(outletHostname, turnOn, socketId);
            } catch (Exception e) when (e is not OutOfMemoryException) {
                logger.LogError(e, "Uncaught exception setting outlet {name} power state to {action} after delay", outletHostname, action);
            }
        });

        return NoContent();
    }

}

public enum OutletAction {

    FALSE,
    TRUE,
    TOGGLE

}