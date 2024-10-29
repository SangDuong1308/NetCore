docker-compose up -d

cd src
dotnet ef migrations add {...}
dotnet ef database update