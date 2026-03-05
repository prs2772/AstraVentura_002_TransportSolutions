# Comandos Útiles

## 1. Entity Framework Core (Migraciones)

Si cambias la entidad `Vehicle` en el proyecto Core, debes actualizar la base de datos SQL:

### Crear la migración
Asegúrate de tener un servidor de SQL corriendo o el string apuntando a tu local/docker.

```bash
dotnet ef migrations add <NombreDeMigracion> --project AutoFleet.Infrastructure --startup-project AutoFleet.API
```

### Aplicar a la base de datos

```bash
dotnet ef database update --project AutoFleet.Infrastructure --startup-project AutoFleet.API
```

## 2. Docker Compose

Levantar la API junto con SQL Server y MongoDB en modo background (detached):
```bash
docker-compose up -d
```

Apagar los contenedores y removerlos:
```bash
docker-compose down
```

Borrar todo (incluyendo los volúmenes de datos donde se guardan las tablas de SQL y Mongo):
```bash
docker-compose down -v
```
