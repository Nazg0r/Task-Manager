using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
	opt.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
}, ServiceLifetime.Singleton);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }