using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using TaskManager.Application;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Infrastructure;
using TaskManager.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// --- Logging ---
// builder.Host.UseSerilog((context, services, configuration) =>
//     configuration.ReadFrom.Configuration(context.Configuration));

// --- Add Services ---

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Manager API", Version = "v1" });
    // JWT Swagger config (optional, coming later)
});

// Infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


// JWT Authentication config (placeholder)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["JWT:Key"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });



Console.WriteLine("🟡 Application is building...");

var app = builder.Build();

Console.WriteLine("🟢 Application built. Starting middleware...");
// --- Middleware ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

Console.WriteLine("✅ App is running.");
