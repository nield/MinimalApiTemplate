<#
.SYNOPSIS
    Generates and trusts the local ASP.NET Core HTTPS development certificate
    used by the API container in docker-compose.

.DESCRIPTION
    The PFX is exported to %APPDATA%\ASP.NET\Https, which docker-compose.override.yml
    mounts into the container. The password must match the one referenced by
    Kestrel__Certificates__Default__Password in docker-compose.override.yml.

    Run this once after cloning the repo (or whenever the cert expires).
#>
param(
    [string]$Password = "devpassword",
    [string]$CertName = "MinimalApiTemplate.Api.pfx"
)

$ErrorActionPreference = "Stop"

$certDir = Join-Path $env:APPDATA "ASP.NET\Https"
$certPath = Join-Path $certDir $CertName

New-Item -ItemType Directory -Force -Path $certDir | Out-Null

Write-Host "Cleaning existing dev certificates..."
dotnet dev-certs https --clean

Write-Host "Exporting dev certificate to $certPath ..."
dotnet dev-certs https -ep $certPath -p $Password

Write-Host "Trusting dev certificate..."
dotnet dev-certs https --trust

Write-Host "Done. Dev HTTPS certificate ready at $certPath" -ForegroundColor Green
