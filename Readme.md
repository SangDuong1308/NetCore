## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)

## Getting Started

*Clone the repository:

```bash
git clone https://github.com/SangDuong1308/ISD-HRMS.git

docker-compose up -d
```
Access **pgAdmin**
* Create **"HRMS"** database
```bash
http://localhost:5050/browser/
```
* Apply Migration
```bash
cd .\src\
dotnet ef migrations add {MigrationName}
dotnet ef database update
dotnet watch run
```
* Start application
```bash
dotnet watch run
```
