using Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

var appConfig = builder.Configuration;

builder.Services.AddServices(appConfig);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }