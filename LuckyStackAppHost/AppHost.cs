
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("postgres-username");
var password = builder.AddParameter("postgres-password", secret: true);

var postgres = builder.AddPostgres("postgres", username, password)
    .WithHostPort(5432);
var database = postgres.AddDatabase("luckydb");

var migrationsWorker = builder.AddProject<LuckyRest_MigrationService>("luckyrest-migration")
    .WithReference(database)
    .WaitFor(database);

var restApi = builder.AddProject<LuckyRest>("luckyrest")
    .WithReference(database)
    .WaitFor(migrationsWorker)
    .WithExternalHttpEndpoints();




builder.AddNpmApp("vue-client", "../luckywheel-client", "dev")
    .WithReference(restApi)
    .WaitFor(restApi)
    .WithHttpEndpoint(env: "PORT")
    .WithEnvironment("VITE_IS_ASPIRE_HOST", "true")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();