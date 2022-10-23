using System.Diagnostics;
using Microsoft.Extensions.Logging.Abstractions;
using PowerOverInternet.Controllers;
using PowerOverInternet.Services;

namespace Tests.Controllers;

public class PowerControllerUnitTest {

    private static readonly TimeSpan QUIESCENCE_TIME = TimeSpan.FromMilliseconds(500);

    private readonly OutletService        outletService = A.Fake<OutletService>();
    private readonly PowerController      powerController;
    private readonly ManualResetEventSlim calledSetPowerState = new();

    public PowerControllerUnitTest() {
        powerController = new PowerController(NullLogger<PowerController>.Instance, outletService);

        A.CallTo(() => outletService.setPowerState(A<string>._, A<bool>._)).Invokes(() => calledSetPowerState.Set());
    }

    [Fact]
    public void setOutletOn() {
        powerController.setOutletPower("192.168.1.100", true);

        calledSetPowerState.Wait(QUIESCENCE_TIME);

        A.CallTo(() => outletService.setPowerState("192.168.1.100", true)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void setOutletOff() {
        powerController.setOutletPower("192.168.1.100", false);

        calledSetPowerState.Wait(QUIESCENCE_TIME);

        A.CallTo(() => outletService.setPowerState("192.168.1.100", false)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void setOutletAfterDelay() {
        Stopwatch stopwatch = Stopwatch.StartNew();

        powerController.setOutletPower("192.168.1.100", false, 2);

        calledSetPowerState.Wait(TimeSpan.FromSeconds(2) + QUIESCENCE_TIME);
        stopwatch.Stop();

        A.CallTo(() => outletService.setPowerState("192.168.1.100", false)).MustHaveHappenedOnceExactly();
        stopwatch.Elapsed.Should().BeGreaterOrEqualTo(TimeSpan.FromSeconds(2) - QUIESCENCE_TIME);
        stopwatch.Elapsed.Should().BeLessOrEqualTo(TimeSpan.FromSeconds(2) + QUIESCENCE_TIME);
    }

    [Fact]
    public void setOutletClipsNegativeDelaysToZero() {
        Stopwatch stopwatch = Stopwatch.StartNew();
        powerController.setOutletPower("192.168.1.100", false, -1);

        calledSetPowerState.Wait(QUIESCENCE_TIME);
        stopwatch.Stop();

        A.CallTo(() => outletService.setPowerState("192.168.1.100", false)).MustHaveHappenedOnceExactly();
        stopwatch.Elapsed.Should().BeLessOrEqualTo(QUIESCENCE_TIME);
    }

}