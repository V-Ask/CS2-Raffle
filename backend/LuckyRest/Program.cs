using LuckyRest.Database;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var services = builder.Services;
services.AddEndpointsApiExplorer();

services.AddControllers();
services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy.SetIsOriginAllowed(origin =>
                    origin.StartsWith("http://localhost:") ||
                    origin.StartsWith("https://localhost:"))
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
        else
        {
            policy.WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    });
});
services.AddSwaggerGen();

services.AddAuthorization();
services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<LuckyDbContext>()
    .AddSignInManager()
    .AddApiEndpoints();

services.AddDbContext<LuckyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("luckydb"));
});


services.AddRateLimiter(options =>
{
    options.AddSlidingWindowLimiter("dathost", o =>
    {
        o.PermitLimit = 10;
        o.Window = TimeSpan.FromMinutes(1);
        o.SegmentsPerWindow = 6;
        o.QueueLimit = 0;
    });
    options.AddSlidingWindowLimiter("steam", o =>
    {
        o.PermitLimit = 10;
        o.Window = TimeSpan.FromMinutes(0.5);
        o.SegmentsPerWindow = 6;
        o.QueueLimit = 0;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.AddScopes();

var app = builder.Build();

app.UseCors();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<User>();
app.MapControllers();

app.Run();

