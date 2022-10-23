using Kasa;
using PowerOverInternet.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<OutletService, KasaOutletService>();
builder.Services.AddScoped<Func<string, IKasaOutlet>>(provider => hostname => new KasaOutlet(hostname, new Options { LoggerFactory = provider.GetService<ILoggerFactory>() }));
if (!builder.Environment.IsDevelopment()) {
    builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 8444; });
}

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();