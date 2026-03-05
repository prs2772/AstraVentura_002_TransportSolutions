# 🚀 API Endpoints (Transport Solutions)

**Nota:** Todos los endpoints (excepto si se configuran como anónimos) requieren un token JWT válido (emitido por el servicio de Auth) en el header `Authorization: Bearer <token>`.

## 1. Consultar Modelos Externos (NHTSA Proxy)

**Endpoint:** `GET /api/vehicles/external-models/{make}`
**Propósito:** Trae datos oficiales del gobierno de USA.
**Ejemplo de ruta:** `/api/vehicles/external-models/tesla`

---

## 2. Crear Vehículo

**Endpoint:** `POST /api/vehicles`

**Body:**
```json
{
  "vin": "5YJ3E1EB1LF000001",
  "brand": "Tesla",
  "model": "Model 3",
  "year": 2024,
  "price": 45000.0,
  "passengerCapacity": 5,
  "kmPerLiter": 18.5
}
```

## 3. Optimización Simple (Voraz)

**Endpoint:** `POST /api/fleet/simple`
**Propósito:** Asigna pasajeros usando los vehículos más grandes primero.
**Body:**
```json
{
  "totalPassengers": 45
}
```

---

## 4. Optimización Avanzada (Programación Dinámica)

**Endpoint:** `POST /api/fleet/optimized`
**Propósito:** Asigna pasajeros usando el algoritmo de Programación Dinámica. Busca la combinación perfecta priorizando menos vehículos y mejor rendimiento de combustible.
**Body:**
```json
{
  "totalPassengers": 45
}
```

---

## 5. Consultar Vehículos

**Endpoint:** `GET /api/vehicles`
**Propósito:** Trae todos los vehículos.
