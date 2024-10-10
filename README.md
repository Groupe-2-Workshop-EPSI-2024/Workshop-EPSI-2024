# AddicTrack
An application to help people quit addictions  
Made for EPSI Workshop 2024

## Installation
Clone project
```bash
git clone https://github.com/Groupe-2-Workshop-EPSI-2024/Workshop-EPSI-2024/tree/main
```

Get .env file (for Docker flow) or setup environment variables

Use Docker
```bash
docker compose down
docker compose build
docker compose up -d
```

Migrate the database (with the variables in the .env file)
```bash
dotnet ef database update -p Back_AddicTrack --connection "server=localhost;port=3306;database=${MYSQL_DATABASE};user=${MYSQL_USER};password=${MYSQL_PASSWORD}"
```

The above commands needs to have .NET SDK 8.0 installed as well as dotnet-ef tool
```bash
dotnet tool install -g dotnet-ef
```
