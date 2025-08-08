using LuckyRest.Database;
using LuckyRest.Database.Entities;
using LuckyRest.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var services = builder.Services;
services.AddEndpointsApiExplorer();

services.AddControllers();
services.AddCors();
services.AddSwaggerGen();

services.AddAuthorization();
services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme);

services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<LuckyDbContext>()
    .AddApiEndpoints();

services.AddDbContext<LuckyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.AddScopes();

var app = builder.Build();

app.UseCors(options => options.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod());

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

