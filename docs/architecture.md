# Arquitectura Clean / Ports and Adapters

## Diagrama General

         [ API / Controllers ]
                ↓
    [ Application (Services / DTOs) ]
                ↓
      [ Core (Entities / Interfaces) ]
                ↑
    [ Infrastructure (EF Core / Mongo) ]

## Estructura del Proyecto

AstraVentura_002_TransportSolutions.sln
│
├── AutoFleet.Core (El centro de todo, sin dependencias)
│   ├── Entities (Vehicle, VehicleStatus)
│   ├── Enums (RepositorySource)
│   ├── Interfaces (IVehicleRepository)
│   └── Models (InventoryItem)
│
├── AutoFleet.Application (Casos de uso y reglas de negocio)
│   ├── DTOs (CreateVehicleDto, FleetRequestDto, etc.)
│   ├── Interfaces (IVehicleService, IFleetOptimizerService)
│   └── Services
│       ├── FleetOptimizerService.cs (Algoritmos Greedy y Programación Dinámica)
│       └── VehicleService.cs (Lógica de Failover entre repositorios)
│
├── AutoFleet.Infrastructure (Acceso a datos y dependencias externas)
│   ├── Data (AutoFleetDbContext)
│   └── Repositories
│       ├── MongoVehicleRepository.cs (Lectura/Escritura secundaria)
│       └── VehicleRepository.cs (MSSQL - Principal)
│   
└── AutoFleet.API (Presentación y Configuración)
    ├── Controllers (FleetController, VehiclesController)
    ├── Extensions (Dependency Injection, Swagger, JWT)
    └── Middlewares (ExceptionMiddleware)
