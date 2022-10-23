using Kasa;

namespace PowerOverInternet.Services;

public class KasaOutletService: OutletService {

    private readonly ILogger<KasaOutletService> logger;
    private readonly Func<string, IKasaOutlet>  outletFactory;

    public KasaOutletService(ILogger<KasaOutletService> logger, Func<string, IKasaOutlet> outletFactory) {
        this.logger        = logger;
        this.outletFactory = outletFactory;
    }

    public async Task setPowerState(string hostname, bool turnOn) {
        using IKasaOutlet outlet = outletFactory(hostname);
        logger.LogInformation("Turning outlet {hostname} {state} now", hostname, turnOn ? "on" : "off");
        await outlet.System.SetOutletOn(turnOn);
    }

}