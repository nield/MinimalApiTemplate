#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ./NuGet.config /src

COPY ["src/MinimalApiTemplate.Api/MinimalApiTemplate.Api.csproj", "src/MinimalApiTemplate.Api/"]
COPY ["src/MinimalApiTemplate.Application/MinimalApiTemplate.Application.csproj", "src/MinimalApiTemplate.Application/"]
COPY ["src/MinimalApiTemplate.Domain/MinimalApiTemplate.Domain.csproj", "src/MinimalApiTemplate.Domain/"]
COPY ["src/MinimalApiTemplate.Infrastructure/MinimalApiTemplate.Infrastructure.csproj", "src/MinimalApiTemplate.Infrastructure/"]
COPY ["src/MinimalApiTemplate.Messages/MinimalApiTemplate.Messages.csproj", "src/MinimalApiTemplate.Messages/"]
RUN dotnet restore "src/MinimalApiTemplate.Api/MinimalApiTemplate.Api.csproj" --configfile /src/NuGet.config
COPY . .
WORKDIR "/src/src/MinimalApiTemplate.Api"
RUN dotnet build "MinimalApiTemplate.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalApiTemplate.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApiTemplate.Api.dll"]