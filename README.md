# Astra Ventura Transport Solutions (Antes AutoFleet)

Repositorio GitHub: AstraVentura_002_TransportSolutions

## 📌 Propósito

Gestionar el inventario de la flota vehicular y proveer un motor algorítmico para optimizar la asignación de pasajeros a los vehículos, minimizando el uso de recursos y priorizando la eficiencia de combustible.

## 🎯 Alcance

Este microservicio forma parte del ecosistema de Astra Ventura Universal. Se encarga de la persistencia políglota de los vehículos (SQL Server y MongoDB) y expone endpoints protegidos por JWT (emitidos por `AstraVentura_001_Auth`) para consultar, crear e integrar datos vehiculares públicos (NHTSA vPIC API).

Se puede ver una explicación general del código en el archivo docs/explanation.md
→ [Explicación general](docs/explanation.md)

## 🏗 Arquitectura

El proyecto sigue los principios de **Clean Architecture** (Arquitectura Limpia), separando las responsabilidades en Core, Application, Infrastructure y API. 
Cuenta con un patrón de **Failover** para repositorios y algoritmos de **Programación Dinámica** y **Greedy** para la optimización.

→ [Arquitectura detallada](docs/architecture.md)

Para revisar cómo se fue creando el proyecto y sus dependencias, revisar el archivo project-creation.md
→ [Project Creation](docs/project-creation.md)

## 🚀 Ejecución

Ejecutar `docker-compose up -d` en la raíz. Esto levantará la API, una instancia de SQL Server (2022) y una de MongoDB. Para detener los servicios ejecutar `docker-compose down`.

## 🧪 Try me

Revisar el archivo try.md para ver cómo probar la API usando Swagger o REST Clients.
→ [Try me](docs/try.md)

## 🛠️ Utilerías

Revisar utils.md para ver comandos útiles sobre Entity Framework Core y Docker.
→ [Utilerías](docs/utils.md)

## 📖 Documentación

- [Arquitectura detallada](docs/architecture.md)
- [Project Creation](docs/project-creation.md)
- [Explicación general](docs/explanation.md)
- [Try me](docs/try.md)
- [Utilerías](docs/utils.md)
- [Docker](docs/docker-help.md)

