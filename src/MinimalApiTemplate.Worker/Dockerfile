#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./NuGet.config /src
COPY ["src/MinimalApiTemplate.Application/MinimalApiTemplate.Application.csproj", "src/MinimalApiTemplate.Application/"]
COPY ["src/MinimalApiTemplate.Domain/MinimalApiTemplate.Domain.csproj", "src/MinimalApiTemplate.Domain/"]
COPY ["src/MinimalApiTemplate.Infrastructure/MinimalApiTemplate.Infrastructure.csproj", "src/MinimalApiTemplate.Infrastructure/"]
COPY ["src/MinimalApiTemplate.Messages/MinimalApiTemplate.Messages.csproj", "src/MinimalApiTemplate.Messages/"]
COPY ["src/MinimalApiTemplate.Worker/MinimalApiTemplate.Worker.csproj", "src/MinimalApiTemplate.Worker/"]

RUN dotnet restore "src/MinimalApiTemplate.Worker/MinimalApiTemplate.Worker.csproj" --configfile /src/NuGet.config
COPY . .
WORKDIR "/src/src/MinimalApiTemplate.Worker"
RUN dotnet build "MinimalApiTemplate.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MinimalApiTemplate.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApiTemplate.Worker.dll"]