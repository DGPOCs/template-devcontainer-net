# Plantilla .NET con DevContainers

Este repositorio proporciona una plantilla lista para usar que combina una API minimalista desarrollada con ASP.NET Core 8 y la configuración necesaria para ejecutarla dentro de un [Dev Container](https://containers.dev/). Está pensada como punto de partida para crear APIs modernas en .NET manteniendo un entorno de desarrollo reproducible.

## Características principales

- **ASP.NET Core 8** con SDK oficial de Microsoft.
- API REST sencilla con dos endpoints:
  - `GET /api/welcome`: devuelve el mensaje de bienvenida actual.
  - `POST /api/welcome`: actualiza el mensaje de bienvenida (requiere un cuerpo JSON con el campo `message`).
- El mensaje por defecto se define en `appsettings.json` y se mantiene en memoria tras las actualizaciones.
- Suite de pruebas automatizadas con `xUnit`, `FluentAssertions` y `Microsoft.AspNetCore.Mvc.Testing` para ejecutar pruebas de integración sobre la API.
- Configuración de Dev Container para trabajar de forma consistente en VS Code o GitHub Codespaces, con `dotnet restore` automático tras la creación del contenedor.

## Requisitos previos

- [VS Code](https://code.visualstudio.com/) con la extensión **Dev Containers** o acceso a **GitHub Codespaces**.
- Docker instalado y ejecutándose si se usa VS Code en local.

## Uso del Dev Container

1. Abre el repositorio en VS Code.
2. Cuando se te solicite, selecciona **Reopen in Container** (o usa el comando `Dev Containers: Reopen in Container`).
3. La imagen incluye el SDK de .NET 8. Tras la construcción del contenedor se ejecuta `dotnet restore DevcontainerWelcome.sln` para preparar las dependencias.

## Ejecución de la aplicación

Una vez dentro del Dev Container (o en tu entorno local con el SDK de .NET 8):

```bash
dotnet run --project src/WelcomeApi/WelcomeApi.csproj
```

La API estará disponible en `http://localhost:8080` gracias a la variable de entorno configurada en el contenedor.

### Ejemplo de peticiones

- Obtener el mensaje actual:

  ```bash
  curl http://localhost:8080/api/welcome
  ```

- Actualizar el mensaje:

  ```bash
  curl -X POST http://localhost:8080/api/welcome \
       -H "Content-Type: application/json" \
       -d '{"message": "Hola desde Dev Containers"}'

## Personalización

- Modifica el mensaje de bienvenida por defecto editando `src/WelcomeApi/appsettings.json` (`Welcome:WelcomeMessage`).
- Añade nuevas dependencias actualizando los ficheros `.csproj` en `src/` y `tests/`.
- Ajusta la configuración del contenedor en `.devcontainer/devcontainer.json` para incluir herramientas adicionales o cambiar los puertos expuestos.

## Licencia

Este proyecto se distribuye bajo la licencia [MIT](LICENSE).
