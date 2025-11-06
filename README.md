# Inventario IT

API REST construida con ASP.NET Core 8, Entity Framework Core y SQL Server para administrar el inventario de equipos de informática, sus mantenimientos y la trazabilidad de los componentes.

## Proyectos

- **ApiInventario**: capa de presentación con los controladores y configuración HTTP.
- **Inventario.Application**: lógica de negocio, DTOs, validaciones y servicios de aplicación.
- **Inventario.Domain**: entidades y enumeraciones del dominio.
- **Inventario.Infrastructure**: persistencia con Entity Framework Core, contexto y configuraciones.

## Requisitos

- .NET SDK 8.0
- SQL Server (local o remoto)

## Configuración

1. Actualiza la cadena de conexión `DefaultConnection` en `appsettings.json` o `appsettings.Development.json`.
2. Ejecuta las migraciones de Entity Framework Core cuando estén creadas:
   ```bash
   dotnet ef migrations add Inicial --project Inventario.Infrastructure --startup-project ApiInventario.csproj
   dotnet ef database update --project Inventario.Infrastructure --startup-project ApiInventario.csproj
   ```

## Ejecución

```bash
dotnet run --project ApiInventario.csproj
```

La API expone documentación Swagger en `https://localhost:5104/swagger` (puerto sujeto a tu configuración).

## Endpoints destacados

- `GET /api/equipos` – Listar equipos filtrando por estado o categoría.
- `GET /api/equipos/{id}` – Detalle de equipo con mantenimientos y componentes instalados.
- `POST /api/componentes/{id}/instalaciones` – Registrar la instalación de un componente en un equipo.
- `POST /api/mantenimientos` – Registrar un mantenimiento.
- `GET /api/movimientos/componentes/{id}` – Historial de movimientos de un componente.

Consulta el archivo [`ApiInventario.http`](ApiInventario.http) para ejemplos de peticiones.
