
var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("Seq", 8002)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithOtlpExporter();

var redis = builder.AddRedis("Redis")
    .WithLifetime(ContainerLifetime.Persistent);

var sqlPassword = builder.AddParameter("sqlPassword");
var database = builder.AddSqlServer("SqlDatabase", sqlPassword)
    .WithLifetime(ContainerLifetime.Persistent);

var rabbitUsername = builder.AddParameter("rabbitUsername");
var rabbitPassword = builder.AddParameter("rabbitPassword");

var rabbit = builder.AddRabbitMQ("RabbitMq", rabbitUsername, rabbitPassword)
                .WithLifetime(ContainerLifetime.Persistent)
                .WithManagementPlugin(8001);

builder.AddProject<Projects.MinimalApiTemplate_Api>("minimalapitemplate-api")
    .WithReference(database)
    .WaitFor(database)
    .WithReference(rabbit)
    .WaitFor(rabbit)
    .WithReference(redis)
    .WaitFor(redis)
    .WaitFor(seq)
    .WithEnvironment("MassTransit__PublishEnabled", "true");

builder.AddProject<Projects.MinimalApiTemplate_Worker>("minimalapitemplate-worker")
    .WithReference(rabbit)
    .WaitFor(rabbit)
    .WithEnvironment("MassTransit__ConsumerEnabled", "true");

await builder.Build().RunAsync();
