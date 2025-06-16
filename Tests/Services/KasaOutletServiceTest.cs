using Kasa;
using Microsoft.Extensions.Logging.Abstractions;
using PowerOverInternet.Services;
using static Kasa.IKasaOutletBase;

namespace Tests.Services;

public class KasaOutletServiceTest {

    private readonly ILogger<KasaOutletService> logger            = new NullLogger<KasaOutletService>();
    private readonly IKasaOutlet                outlet            = A.Fake<IKasaOutlet>();
    private readonly IMultiSocketKasaOutlet     multiSocketOutlet = A.Fake<IMultiSocketKasaOutlet>();
    private readonly KasaOutletService          kasaOutletService;

    public KasaOutletServiceTest() {
        A.CallTo(() => outlet.System).Returns(A.Fake<ISystemCommands.ISingleSocket>());
        kasaOutletService = new KasaOutletService(logger, _ => outlet, _ => multiSocketOutlet);
    }

    [Fact]
    public async Task turnOn() {
        await kasaOutletService.setPowerState("192.168.1.100", true, null);
        A.CallTo(() => outlet.System.SetSocketOn(true)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task turnOff() {
        await kasaOutletService.setPowerState("192.168.1.100", false, null);
        A.CallTo(() => outlet.System.SetSocketOn(false)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task turnOnMultiSocket() {
        await kasaOutletService.setPowerState("192.168.1.100", true, 0);
        A.CallTo(() => multiSocketOutlet.System.SetSocketOn(0, true)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task turnOffMultiSocket() {
        await kasaOutletService.setPowerState("192.168.1.100", false, 1);
        A.CallTo(() => multiSocketOutlet.System.SetSocketOn(1, false)).MustHaveHappenedOnceExactly();
    }

}