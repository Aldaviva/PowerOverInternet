using Kasa;

namespace PowerOverInternet.Services;

public class KasaOutletService: OutletService {

    private readonly ILogger<KasaOutletService> logger;

    public KasaOutletService(ILogger<KasaOutletService> logger) {
        this.logger = logger;
    }

    public async Task setPowerState(string hostname, bool turnOn) {
        using IKasaOutlet outlet = new KasaOutlet(hostname);
        logger.LogInformation("Turning outlet {state} now", turnOn ? "on" : "off");
        await outlet.System.SetOutletOn(turnOn);
    }

}