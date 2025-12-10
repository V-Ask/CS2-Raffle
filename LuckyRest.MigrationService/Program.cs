using LuckyRest.Database;
using LuckyRest.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddNpgsqlDbContext<LuckyDbContext>("luckydb");

var host = builder.Build();
host.Run();