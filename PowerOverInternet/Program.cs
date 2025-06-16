using Kasa;
using PowerOverInternet.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddSingleton<OutletService, KasaOutletService>()
    .AddSingleton<Func<string, IKasaOutlet>>(provider => hostname => new KasaOutlet(hostname, new Options {
        LoggerFactory = provider.GetService<ILoggerFactory>()
    }))
    .AddSingleton<Func<string, IMultiSocketKasaOutlet>>(provider => hostname => new MultiSocketKasaOutlet(hostname, new Options {
        LoggerFactory = provider.GetService<ILoggerFactory>()
    }));

if (!builder.Environment.IsDevelopment()) {
    builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 8444; });
}

await using WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();