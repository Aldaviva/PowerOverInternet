using Microsoft.AspNetCore.Mvc;
using PowerOverInternet.Services;

namespace PowerOverInternet.Controllers;

[ApiController]
[Route("[controller]")]
public class PowerController: ControllerBase {

    private readonly ILogger<PowerController> logger;
    private readonly OutletService            outletService;

    public PowerController(ILogger<PowerController> logger, OutletService outletService) {
        this.logger        = logger;
        this.outletService = outletService;
    }

    [HttpPut]
    public IActionResult setOutletPower([FromQuery] string outletHostname, [FromQuery] bool turnOn, [FromQuery] int delaySec = 0) {
        logger.LogInformation("Will turn {name} {state} in {sec:N0} seconds", outletHostname, turnOn ? "on" : "off", delaySec);

        Task.Delay(TimeSpan.FromSeconds(delaySec))
            .ContinueWith(task => outletService.setPowerState(outletHostname, turnOn));

        return NoContent();
    }

}