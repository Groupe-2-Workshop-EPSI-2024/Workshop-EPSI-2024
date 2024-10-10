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

Install .NET SDK 8.0

Install the projects dependencies then migrate the database (with the variables in the .env file)
```bash
dotnet restore
dotnet ef database update -p Back_AddicTrack --connection "server=localhost;port=3306;database=${MYSQL_DATABASE};user=${MYSQL_USER};password=${MYSQL_PASSWORD}"
```
