using Kasa;
using Microsoft.Extensions.Logging.Abstractions;
using PowerOverInternet.Services;

namespace Tests.Services;

public class KasaOutletServiceTest {

    private readonly ILogger<KasaOutletService> logger = new NullLogger<KasaOutletService>();
    private readonly IKasaOutlet                outlet = A.Fake<IKasaOutlet>();
    private readonly KasaOutletService          kasaOutletService;

    public KasaOutletServiceTest() {
        kasaOutletService = new KasaOutletService(logger, _ => outlet);
    }

    [Fact]
    public async Task turnOn() {
        await kasaOutletService.setPowerState("192.168.1.100", true);
        A.CallTo(() => outlet.System.SetOutletOn(true)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task turnOff() {
        await kasaOutletService.setPowerState("192.168.1.100", false);
        A.CallTo(() => outlet.System.SetOutletOn(false)).MustHaveHappenedOnceExactly();
    }

}