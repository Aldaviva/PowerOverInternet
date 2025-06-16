using Kasa;
using PowerOverInternet.Controllers;

namespace PowerOverInternet.Services;

public interface OutletService {

    /// <exception cref="NetworkException"></exception>
    /// <exception cref="ResponseParsingException"></exception>
    Task setPowerState(string hostname, OutletAction turnOn, int? socketId);

}

public class KasaOutletService(ILogger<KasaOutletService> logger, Func<string, IKasaOutlet> outletFactory, Func<string, IMultiSocketKasaOutlet> multiSocketOutletFactory): OutletService {

    /// <inheritdoc />
    public async Task setPowerState(string hostname, OutletAction turnOn, int? socketId) {
        logger.LogInformation("{action} socket {hostname} now", turnOn switch {
            OutletAction.FALSE  => "Turning off",
            OutletAction.TRUE   => "Turning on",
            OutletAction.TOGGLE => "Toggling"
        }, hostname);

        bool newState;
        if (socketId == null) {
            using IKasaOutlet outlet = outletFactory(hostname);
            newState = await getNewOutletState(turnOn, async () => !await outlet.System.IsSocketOn());
            await outlet.System.SetSocketOn(newState);
        } else {
            using IMultiSocketKasaOutlet outlet = multiSocketOutletFactory(hostname);
            newState = await getNewOutletState(turnOn, async () => !await outlet.System.IsSocketOn(socketId.Value));
            await outlet.System.SetSocketOn(socketId.Value, newState);
        }

        static async Task<bool> getNewOutletState(OutletAction action, Func<Task<bool>> onToggle) => action switch {
            OutletAction.FALSE  => false,
            OutletAction.TRUE   => true,
            OutletAction.TOGGLE => await onToggle()
        };
    }

}