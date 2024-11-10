## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)

## Getting Started

### 1.1 Clone the repository:

```bash
git clone https://github.com/SangDuong1308/NetCore.git

docker-compose -f .\docker-compose.yml -p NetCore up -d
```
### 1.2 Access **pgAdmin**
```bash
http://localhost:5050/browser/
```
* Create **"CleanArchitectureAndDDD"** database
### 1.3 Apply Migration
```bash
cd .\src\

dotnet ef migrations add "Migration name" --context "AppDbContext" --project .\NetCore.Infrastructure\ --startup-project .\NetCore.WebApi\

dotnet ef database update --context "AppDbContext" --project .\NetCore.Infrastructure\ --startup-project .\NetCore.WebApi\
```
### 1.4 Start application
```bash
cd .\src\NetCore.WebApi\
dotnet watch run
```

## Build with
* [ASP.NET Core 8](https://github.com/dotnet/aspnetcore)
* [RabbitMQ](https://github.com/rabbitmq)
* [Redis](https://github.com/redis/redis)
* [MassTransit](https://github.com/MassTransit)
* [EF Core](https://github.com/dotnet/efcore)
* [Moq](https://github.com/moq)
* [xUnit](https://github.com/xunit/xunit)
* [HtmlSanitizer](https://github.com/mganss/HtmlSanitizer)
* [Open Telemetry](https://github.com/open-telemetry)
* [FluentValidation](https://github.com/FluentValidation/FluentValidation)
