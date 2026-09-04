# MinimalApiTemplate
[![.NET](https://github.com/nield/MinimalApiTemplate/actions/workflows/dotnet.yml/badge.svg)](https://github.com/nield/MinimalApiTemplate/actions/workflows/dotnet.yml)
[![Quality gate status](https://sonarcloud.io/api/project_badges/measure?project=nield_MinimalApiTemplate&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=nield_MinimalApiTemplate)
![badge](https://gist.githubusercontent.com/nield/036191e91ff7da1f940618f701c0ad9f/raw/badge_combined.svg?)

An example .NET 10 Minimal Api application with OpenAPI, Swashbuckle, and API versioning using clean architecture.

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
2. **(Docker Compose only)** If you run the solution via Docker Compose, set up the local HTTPS
   development certificate. This step is **not** required when running via .NET Aspire (the AppHost),
   which manages certificates for you.
   The certificate is **not** committed to the repo (it contains a private key); each developer
   generates their own locally. From the root of the repo run:
   - Windows (PowerShell): `./scripts/certs/setup-dev-certs.ps1`
   - macOS/Linux: `./scripts/certs/setup-dev-certs.sh`

   This exports a dev cert into your user profile (`%APPDATA%\ASP.NET\Https` on Windows,
   `~/.aspnet/https` on macOS/Linux), which `docker-compose.override.yml` mounts into the API
   container. The password must match `Kestrel__Certificates__Default__Password` in
   `docker-compose.override.yml` (default: `devpassword`).
3. Open command prompt and set the current folder to the root of the repo
4. Execute 'dotnet new install .' command in the command prompt
5. While in command prompt create a folder in the location you want the template code to be created
6. Execute 'dotnet new ca-template -o "**New Micro Service Name here**"' command in the command prompt
7. If you need to uninstall the template
    - Open command prompt and set the current folder to the root of the repo
    - Execute 'dotnet new uninstall .' command in the command prompt

## Authentication

KeyCloak is setup as IDP with some defaults:

Groups
- Admin
- Standard

Roles
- standard-user-role
  - Can Create, Update and View
- admin-user-role
  - Can Delete
  
Users (Password is password)
- AdminUser
- StandardUser

Management UI (Password is admin)
- admin