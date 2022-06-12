using PowerOverInternet.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<OutletService, KasaOutletService>();
if (!builder.Environment.IsDevelopment()) {
    builder.Services.AddHttpsRedirection(options => { options.HttpsPort = 8444; });
}

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();