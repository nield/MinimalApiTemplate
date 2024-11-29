var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("Seq", 8002)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithOtlpExporter();

var redis = builder.AddRedis("Redis")
    .WithLifetime(ContainerLifetime.Persistent);

var sqlPassword = builder.AddParameter("sqlPassword");
var database = builder.AddSqlServer("Sql", sqlPassword, 8003)
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("SqlDatabase", "templateDb");

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
    .WithEnvironment("MassTransit__PublishEnabled", "true")
    .WithEnvironment("SEQ_SERVER_URL", "http://localhost:8002");

builder.AddProject<Projects.MinimalApiTemplate_Worker>("minimalapitemplate-worker")
    .WithReference(rabbit)
    .WaitFor(rabbit)
    .WaitFor(seq)
    .WithEnvironment("MassTransit__ConsumerEnabled", "true")
    .WithEnvironment("SEQ_SERVER_URL", "http://localhost:8002");

await builder.Build().RunAsync();
