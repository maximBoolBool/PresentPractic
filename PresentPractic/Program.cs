using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PresentPractic;
using PresentPractic.AuthOptions;
using PresentPractic.Services;



var builder = WebApplication.CreateBuilder();
var connectionString = builder.Configuration.GetConnectionString("DefaulttConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddControllers();
builder.Services.AddScoped<IPresentService,DefaultPresentService>();
builder.Services.AddScoped<IUserServices,DefaultUserService>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.RunAsync();

    