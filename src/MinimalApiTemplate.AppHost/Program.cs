const string baseContainerName = "minimalapitemplate";

var builder = DistributedApplication.CreateBuilder(args);

var seq = builder.AddSeq("Seq", 8002)
    .WithContainerName($"{baseContainerName}-seq")
    .WithLifetime(ContainerLifetime.Persistent);

var redis = builder.AddRedis("Redis", 8004)
    .WithContainerName($"{baseContainerName}-redis")
    .WithLifetime(ContainerLifetime.Persistent);

var sqlPassword = builder.AddParameter("sqlPassword", secret: true);
var database = builder.AddSqlServer("Sql", sqlPassword, 8003)
    .WithContainerName($"{baseContainerName}-sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("SqlDatabase", "templateDb");

var rabbitUsername = builder.AddParameter("rabbitUsername");
var rabbitPassword = builder.AddParameter("rabbitPassword", secret: true);

var rabbit = builder.AddRabbitMQ("RabbitMq", rabbitUsername, rabbitPassword)
    .WithContainerName($"{baseContainerName}-rabbit")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin(8001);

var keycloakUsername = builder.AddParameter("keycloakUsername");
var keycloakPassword = builder.AddParameter("keycloakPassword", secret: true);

var keycloak = builder.AddKeycloak("Keycloak", 8930, keycloakUsername, keycloakPassword)
    .WithContainerName($"{baseContainerName}-keycloak")
    .WithDataVolume()
    .WithRealmImport("../../scripts/keycloak/")
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Projects.MinimalApiTemplate_Api>("minimalapitemplate-api")    
    .WithReference(database)
    .WaitFor(database)
    .WithReference(rabbit)
    .WaitFor(rabbit)
    .WithReference(redis)
    .WaitFor(redis)
    .WaitFor(seq)
    .WithReference(keycloak)
    .WaitFor(keycloak)
    .WithEnvironment("MassTransit__PublishEnabled", "true")
    .WithEnvironment("SEQ_SERVER_URL", "http://localhost:8002")
    .WithUrls(context =>
    {
        foreach (var url in context.Urls)
        {
            url.DisplayLocation = UrlDisplayLocation.DetailsOnly;
        }
    
        context.Urls.Add(new ResourceUrlAnnotation
        {
            DisplayText = "Swagger UI",
            Url = "/swagger",
            Endpoint = context.GetEndpoint("https") is { Exists: true } https
                ? https
                : context.GetEndpoint("http")
        });
    });

builder.AddProject<Projects.MinimalApiTemplate_Worker>("minimalapitemplate-worker")
    .WithReference(rabbit)
    .WaitFor(rabbit)
    .WaitFor(seq)
    .WithEnvironment("MassTransit__ConsumerEnabled", "true")
    .WithEnvironment("SEQ_SERVER_URL", "http://localhost:8002");

await builder.Build().RunAsync();
