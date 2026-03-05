# 📘 Project Creation on .NET 8

Pasos principales que se siguieron para estructurar este servicio en formato de Arquitectura Limpia.

## 1.1 Solución y Capas

```bash
dotnet new sln -n AutoFleet
dotnet new classlib -n AutoFleet.Core
dotnet new classlib -n AutoFleet.Application
dotnet new classlib -n AutoFleet.Infrastructure
dotnet new webapi -n AutoFleet.API

dotnet sln add AutoFleet.Core AutoFleet.Application AutoFleet.Infrastructure AutoFleet.API
```

## 1.2 Dependencias (Referencias)

```bash
dotnet add AutoFleet.Application reference AutoFleet.Core
dotnet add AutoFleet.Infrastructure reference AutoFleet.Application
dotnet add AutoFleet.API reference AutoFleet.Application
dotnet add AutoFleet.API reference AutoFleet.Infrastructure
```

## 1.3 Paquetes Principales

**Application:**
```bash
dotnet add AutoFleet.Application package System.IdentityModel.Tokens.Jwt
```

**Infrastructure:**
```bash
dotnet add AutoFleet.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add AutoFleet.Infrastructure package MongoDB.Driver
```

**API:**
```bash
dotnet add AutoFleet.API package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add AutoFleet.API package Microsoft.EntityFrameworkCore.Design
```
