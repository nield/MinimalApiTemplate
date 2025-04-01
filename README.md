# MinimalApiTemplate
[![.NET](https://github.com/nield/MinimalApiTemplate/actions/workflows/dotnet.yml/badge.svg)](https://github.com/nield/MinimalApiTemplate/actions/workflows/dotnet.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nield_MinimalApiTemplate&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=nield_MinimalApiTemplate)
![badge](https://gist.githubusercontent.com/nield/036191e91ff7da1f940618f701c0ad9f/raw/badge_combined.svg?)

An example Minimal Api application with OpenAPI, Swashbuckle, and API versioning using clean architecture.

It also include the following:
- KeyCloak IDP 
- .NET Aspire
- .NET 8 ExceptionHandlers
- Redis OutputCaching
- CorrelationId handling using HeaderPropagation
- OpenTelemetry with custom metrics
- Integration tests using TestContainers
- Pub Sub using MassTransit
- API versioning
- Auditing using Audit.Net

This template was initially based on [Jason Tyler's Template](https://github.com/jasontaylordev/CleanArchitecture), but changed as I implement and review new .NET features

## Getting Started

1. Git Clone the repo to your device
2. Open command prompt and set the current folder to the root of the repo
3. Execute 'dotnet new install .' command in the command prompt
4. While in command prompt create a folder in the location you want the template code to be created
5. Execute 'dotnet new ca-template -o "**New Micro Service Name here**"' command in the command prompt
6. If you need to uninstall the template
    - Open command prompt and set the current folder to the root of the repo
    - Execute 'dotnet new uninstall .' command in the command prompt
