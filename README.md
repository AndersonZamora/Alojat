# Alojat — alquiler de inmuebles

Aplicación web para publicar y buscar **inmuebles en alquiler**: cada inmueble tiene categoría, servicios incluidos, punto de referencia, datos de contacto y su ubicación en un mapa. Incluye un panel para que los usuarios con rol de administración gestionen las publicaciones.

Proyecto de 2023 hecho para practicar **ASP.NET Core MVC con una suite de pruebas completa** (unitarias de controladores e integración contra SQL Server).

## Qué hace

- **Inmuebles**: alta, edición, detalle y listado, con imagen, dirección, celular de contacto y coordenadas (latitud / longitud).
- **Mapa**: el punto del inmueble se elige y se muestra con la API JavaScript de Google Maps.
- **Catálogos**: categorías, servicios y puntos de referencia.
- **Búsqueda** de inmuebles.
- **Usuarios y roles**: registro, inicio de sesión con cookie y autorización por rol (`Admin` y `Propietario`).
- **Imágenes** subidas a Firebase Storage.

## Stack

| Capa | Tecnología |
|---|---|
| Web | ASP.NET Core 6 MVC (Razor) |
| Datos | Entity Framework Core 6 + SQL Server |
| Autenticación | Cookies con claims, roles propios |
| Almacenamiento de imágenes | Firebase Storage (`FirebaseStorage.net`) |
| Mapas | Google Maps JavaScript API |
| Pruebas | NUnit 3 + Moq (controladores) · NUnit + SQL Server real (integración) |

## Estructura

```
Alojat/              aplicación web
  Controllers/       Buscar, Categoria, Inmueble, Login, Punto, Servicio, Usuario
  Models/            entidades y view models
  service/ interfa/  servicios y sus interfaces (una pareja por dominio)
  Data/              AlquilerDbContext
  Views/             vistas Razor
TestNunit/           45 pruebas unitarias de controladores (Moq)
TestIntegration/     27 pruebas de integración de los servicios contra la base de datos
Script.sql           script de creación de la base de datos
```

## Pruebas

- **TestNunit**: cada controlador se prueba con sus servicios simulados (Moq).
- **TestIntegration**: los servicios se prueban contra una base SQL Server real a través de `AlquilerDbContext`, sin simulaciones.

## Cómo ejecutarlo

Requisitos: .NET 6 SDK y SQL Server.

1. Crea la base de datos ejecutando `Script.sql`.
2. Configura tu cadena de conexión a SQL Server para la aplicación y para `TestIntegration` (en `SharedDatabaseFixtureTest.cs`).
3. Para subir imágenes, completa tu configuración de Firebase (API key, dominio y usuario de servicio) en `Alojat/service/SSha.cs`.
4. Para ver el mapa, agrega tu clave de la API de Google Maps en las vistas `Punto` e `Inmueble`.
5. `dotnet run --project Alojat` y `dotnet test`.

> No subas credenciales reales al repositorio. Usa `dotnet user-secrets` o variables de entorno.

## Limitaciones conocidas

Es un proyecto de aprendizaje y conviene decirlo:

- Las contraseñas se guardan con **SHA-256 sin sal**. Para producción debería usarse un algoritmo de derivación de claves como Argon2id, bcrypt o PBKDF2 (o ASP.NET Core Identity).
- La configuración de Firebase y la cadena de conexión están escritas en el código en lugar de leerse de configuración o secretos.
- Las pruebas de integración necesitan una instancia de SQL Server accesible.
