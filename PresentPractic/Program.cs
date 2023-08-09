using Microsoft.EntityFrameworkCore;
using PresentPractic;
using PresentPractic.Services;

var builder = WebApplication.CreateBuilder();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddScoped<IPresentService,DefaultPresentService>(); 
builder.Services.AddScoped<IUserServices,DefaultUserService>();
builder.Services.AddControllers();


var app = builder.Build();
app.MapControllers();


await app.RunAsync();