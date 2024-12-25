using Microsoft.AspNetCore.Builder;
using Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

var appConfig = builder.Configuration;

builder.Services.AddServices(appConfig);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().
	WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.MapControllers();

app.Run();

public partial class Program { }