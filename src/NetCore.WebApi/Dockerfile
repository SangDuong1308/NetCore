FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/NetCore.Domain/NetCore.Domain.csproj", "src/NetCore.Domain/"]
COPY ["src/NetCore.Application/NetCore.Application.csproj", "src/NetCore.Application/"]
COPY ["src/NetCore.Infrastructure/NetCore.Infrastructure.csproj", "src/NetCore.Infrastructure/"]
COPY ["src/NetCore.WebApi/NetCore.WebApi.csproj", "src/NetCore.WebApi/"]

RUN dotnet restore "./src/NetCore.WebApi/NetCore.WebApi.csproj" --disable-parallel
COPY . .
WORKDIR "src/NetCore.WebApi"

RUN dotnet publish "./NetCore.WebApi.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "NetCore.WebApi.dll"]