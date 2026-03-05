# Explicación del Código

## 1. API (Capa de Presentación)
Es la puerta de entrada HTTP. Contiene:
- **Program.cs**: Configura los servicios, el pipeline, CORS y Middlewares globales.
- **Middlewares**: `ExceptionMiddleware` atrapa cualquier error interno no controlado y devuelve una respuesta estándar `500 Internal Server Error` en formato JSON, evitando exponer stacktraces sensibles.
- **Controllers**: 
  - `VehiclesController`: Expone CRUD básico y actúa como proxy hacia la API externa de NHTSA.
  - `FleetController`: Expone los endpoints algorítmicos para calcular la flota ideal según los pasajeros.

## 2. Application (Casos de Uso)
Maneja el flujo de información entre la base de datos y la API:
- **VehicleService**: Implementa un patrón **Failover**; si la consulta falla en el repositorio principal (SQL Server), intenta en los secundarios (MongoDB).
- **FleetOptimizerService**: Contiene el motor lógico. Tiene dos enfoques:
  - `GetSimpleAllocationAsync`: Algoritmo Voraz (Greedy) para asignaciones rápidas.
  - `OptimizeAllocationAsync`: Algoritmo de **Programación Dinámica (Knapsack Problem 0/1)** para buscar la combinación más eficiente en consumo de combustible.

## 3. Infrastructure (Detalles Técnicos)
Implementa las interfaces definidas en el Core:
- Acceso a **Azure SQL / SQL Server** mediante Entity Framework Core.
- Acceso a **MongoDB** utilizando su Driver oficial BSON.

## 4. Core (Dominio)
El corazón de la aplicación. Contiene las entidades puras como `Vehicle`, que incluyen reglas de validación nativas (Data Annotations) para evitar el ingreso de datos basura (como años irreales o VINs incompletos).
