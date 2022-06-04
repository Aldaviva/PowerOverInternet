using Kasa.Log;
using PowerOverInternet;
using PowerOverInternet.Services;
using LoggerFactory = slf4net.LoggerFactory;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<OutletService, KasaOutletService>();
if (!builder.Environment.IsDevelopment()) {
    builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 8444; });
}

WebApplication app = builder.Build();

LoggerFactory.SetFactoryResolver(new LoggerFactoryResolver(new MicrosoftLoggerFactory(app.Services.GetRequiredService<ILoggerFactory>())));

app.UseHttpsRedirection();
app.MapControllers();
app.Run();