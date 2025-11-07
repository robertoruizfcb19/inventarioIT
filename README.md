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

## Crear y ejecutar un frontend React en una rama separada

Para mantener el backend y el frontend desacoplados puedes crear el cliente en otra rama y carpeta independiente:

1. Crea y cámbiate a una nueva rama dedicada al frontend:
   ```bash
   git checkout -b feature/frontend-react
   ```
2. Dentro del repositorio crea la carpeta para el frontend y genera un proyecto React (ejemplo con Vite y TypeScript):
   ```bash
   mkdir frontend
   cd frontend
   npm create vite@latest inventario-frontend -- --template react-ts
   cd inventario-frontend
   npm install
   ```
3. Configura una variable de entorno con la URL del API (por ejemplo en `frontend/inventario-frontend/.env`):
   ```env
   VITE_API_BASE_URL=https://localhost:5104
   ```
4. Implementa tus componentes consumiendo los endpoints del backend con `fetch` o Axios usando `import.meta.env.VITE_API_BASE_URL` como base.
5. Ejecuta el frontend en modo desarrollo de forma independiente al backend:
   ```bash
   # En una terminal
   dotnet run --project ApiInventario.csproj

   # En otra terminal
   cd frontend/inventario-frontend
   npm run dev -- --port 5173
   ```
6. Cuando el frontend esté listo, crea commits en la rama `feature/frontend-react` y, si lo deseas, ábrelo como Pull Request separado para mantener los cambios aislados del backend.

Con este flujo podrás trabajar en paralelo sobre el backend y el frontend, ejecutar cada uno en su propio servidor y realizar pruebas end-to-end utilizando la URL configurada.
