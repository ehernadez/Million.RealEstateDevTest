# Million Real Estate API

Este proyecto es una API de Bienes Raíces que proporciona funcionalidad para gestionar propiedades inmobiliarias.

## Tecnologías Utilizadas

- .NET 9
- Entity Framework Core
- SQL Server
- Docker & Docker Compose
- Autenticación JWT
- Swagger / OpenAPI
- Arquitectura Limpia (Clean Architecture)

## Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalado lo siguiente:

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

## Primeros Pasos

### Iniciar SQL Server con Docker Compose

1. Clona el repositorio:
```bash
git clone https://github.com/ehernadez/Million.RealEstateDevTest.git
cd Million.RealEstateDevTest
```

2. Inicia la instancia de SQL Server usando Docker Compose:
```bash
docker-compose up -d
```

Esto creará e iniciará una instancia de SQL Server con la base de datos configurada.

### Ejecutar la API

Tenemos dos opciones para ejecutar la API:

#### Opción 1: Usando Visual Studio

1. Asegúrate de que la instancia de SQL Server en Docker esté ejecutándose
2. Abre la solución en Visual Studio 2022
3. Establece Million.RealEstate.API como proyecto de inicio
4. Presiona F5 o haz clic en Iniciar Depuración

#### Opción 2: Usando la Línea de Comandos

1. Asegúrate de que la instancia de SQL Server en Docker esté ejecutándose
2. Navega hasta el directorio del proyecto API:
```bash
cd Million.RealEstate.API
```
3. Ejecuta el proyecto:
```bash
dotnet run
```

### URLs de Acceso

Una vez que la API esté en ejecución, podrás acceder a:

- API: `https://localhost:7041` o `http://localhost:5197`
- Documentación Swagger: `https://localhost:7041/index.html` o `http://localhost:5197/index.html`

Nota: Verifica que al ejecutar el proyecto sean los mismos puertos para acceder

## Autenticación

La API utiliza autenticación por token JWT Bearer. La mayoría de los endpoints requieren autenticación, excepto:
- Creación de Usuario (`/users`)
- Autenticación (`/auth/login`)

### Primeros Pasos

1. Crea un usuario usando el endpoint `/users`:
```json
{
    "username": "tu_usuario",
    "password": "tu_contraseña",
    "email": "tu_correo@ejemplo.com"
}
```

2. Autentícate usando el endpoint `/auth/login` para obtener tu token JWT:
```json
{
    "username": "tu_usuario",
    "password": "tu_contraseña"
}
```

3. Utiliza el token recibido en el encabezado de Autorización para las siguientes peticiones:
```
Authorization: Bearer tu_token_aquí
```

## Estructura del Proyecto

La solución sigue los principios de Arquitectura Limpia y está organizada en los siguientes proyectos:

- **Million.RealEstate.API**: Capa de API Web con controladores y configuración
- **Million.RealEstate.Application**: lógica de negocio
- **Million.RealEstate.Domain**: Entidades de dominio e interfaces
- **Million.RealEstate.Infrastructure**: Implementación de acceso a datos y servicios externos
- **Million.RealEstate.DependencyInjection**: Configuración de inyección de dependencias
- **Million.RealEstate.Tests**: Pruebas unitarias

## Configuración de la Base de Datos

La aplicación está configurada para usar SQL Server con la siguiente cadena de conexión predeterminada:
```
Server=localhost;Database=RealEstateDB;User Id=sa;Password=RealEstate123!;TrustServerCertificate=True
```

Esta configuración asume que SQL Server está ejecutándose en Docker. La base de datos se creará automáticamente en la primera ejecución.

## Notas Adicionales

- La API utiliza Swagger UI para facilitar las pruebas y documentación de endpoints
- Todos los endpoints excepto Creación de Usuario y Autenticación requieren un token JWT válido
- La base de datos se crea y migra automáticamente en la primera ejecución
- Docker Compose maneja tanto la configuración de la API como de la base de datos para una fácil implementación
