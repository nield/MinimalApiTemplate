#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ./NuGet.config /src

COPY ["src/MinimalApiTemplate.Worker/MinimalApiTemplate.Worker.csproj", "src/MinimalApiTemplate.Worker/"]
COPY ["src/MinimalApiTemplate.Messages/MinimalApiTemplate.Messages.csproj", "src/MinimalApiTemplate.Messages/"]

RUN dotnet restore "src/MinimalApiTemplate.Worker/MinimalApiTemplate.Worker.csproj" --configfile /src/NuGet.config
COPY . .
WORKDIR "/src/src/MinimalApiTemplate.Worker"
RUN dotnet build "MinimalApiTemplate.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalApiTemplate.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApiTemplate.Worker.dll"]