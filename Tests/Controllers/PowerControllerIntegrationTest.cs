using System.Net;
using Kasa;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using PowerOverInternet.Services;

namespace Tests.Controllers;

public class PowerControllerIntegrationTest: IDisposable {

    private readonly OutletService                  outletService = A.Fake<OutletService>();
    private readonly HttpClient                     client;
    private readonly WebApplicationFactory<Program> webapp;

    public PowerControllerIntegrationTest() {
        webapp = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => {
                builder.UseEnvironment(Environments.Production);
                builder.ConfigureTestServices(collection => { collection.AddSingleton(outletService); });
            });
        client = webapp.CreateClient();
    }

    [Fact]
    public async Task putPower() {
        HttpResponseMessage response = await client.PutAsync("/power?outletHostname=192.168.1.100&turnOn=true", null);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        A.CallTo(() => outletService.setPowerState("192.168.1.100", true)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void outletFactory() {
        var factory = webapp.Services.GetService<Func<string, IKasaOutlet>>()!;

        using IKasaOutlet outlet = factory("192.168.1.100");
        outlet.Should().BeOfType<KasaOutlet>();
        outlet.Hostname.Should().Be("192.168.1.100");
        outlet.Options.LoggerFactory.Should().NotBeNull();
    }

    protected virtual void Dispose(bool disposing) {
        if (disposing) {
            client.Dispose();
            webapp.Dispose();
        }
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}