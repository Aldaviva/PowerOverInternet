using Kasa;

namespace PowerOverInternet.Services;

public class KasaOutletService: OutletService {

    private readonly ILogger<KasaOutletService> logger;
    private readonly ILoggerFactory             loggerFactory;

    public KasaOutletService(ILogger<KasaOutletService> logger, ILoggerFactory loggerFactory) {
        this.logger        = logger;
        this.loggerFactory = loggerFactory;
    }

    public async Task setPowerState(string hostname, bool turnOn) {
        using IKasaOutlet outlet = new KasaOutlet(hostname) {
            Options = new Options {
                LoggerFactory = loggerFactory
            }
        };
        logger.LogInformation("Turning outlet {hostname} {state} now", hostname, turnOn ? "on" : "off");
        await outlet.System.SetOutletOn(turnOn);
    }

}