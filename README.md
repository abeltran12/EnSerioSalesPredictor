# EnSerioSalesPredictor Solution

Esta solución incluye varios proyectos que conforman el sistema de predicción de ventas y gestión de órdenes. Cada proyecto tiene su propósito y forma de ejecución.

1. Requisitos previos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js 20+](https://nodejs.org/)
- [Angular CLI 20+](https://angular.io/cli)
- SQL Server (o compatible) con la base de datos entregada ya creada.

> Todos los SPs deben ejecutarse antes de usar la API.

2. Base de datos y SPs

- Asegúrate de tener la base de datos **entregada** ya creada en tu entorno.
- Ejecuta los SPs que se encuentran en la carpeta `/Scripts/scripts_procedures.sql`.
- Cambia la **cadena de conexión** de la API (`appsettings.json`) para apuntar a tu servidor y base de datos.

> Sin esto, los endpoints de la API que dependen de los SPs no devolverán datos.

3. API (.NET 9)

Abre la terminal en la carpeta `EnSerioSalesPredictor.Api` y ejecuta:

dotnet restore
dotnet run

La API quedará escuchando en http://localhost:5152 por defecto.

Puedes probar los endpoints con Postman, Swagger, o desde los proyectos MVC/Angular.

4. Aplicación MVC

- Abrir la carpeta `EnSerioSalesPredictor.WebApp` en Visual Studio.

- Abre la terminal en la carpeta `EnSerioSalesPredictor.WebApp` y ejecuta:

dotnet restore
dotnet run

5. Proyecto de pruebas unitarias

- Abrir la carpeta `EnSerioSalesPredictor.Api.Tests`.
- Ejecutar las pruebas con:

dotnet test

> Cubre servicios de la API.

6. Aplicación Angular 20

Abre la carpeta `EnSerioSalesPredictor.Angular` y ejecuta:

npm install
ng serve

La aplicación correrá en http://localhost:4200.

Actualiza la **URL base de la API** en `environment.ts` para apuntar a http://localhost:5152.

> La aplicación está en desarrollo, pero ya puede consumir algunos endpoints de la API.

7. Clonación del repositorio

git clone https://github.com/tuusuario/EnSerioSalesPredictor.git
cd EnSerioSalesPredictor

Luego abre cada proyecto individualmente (API, MVC, Angular, Tests) y ejecútalos siguiendo los pasos anteriores.

Notas importantes

- Los SPs deben ejecutarse antes de iniciar la API.
- Cambia la **cadena de conexión** de la API a tu entorno.
- MVC genera gráficas con JS puro usando datos de la API.
- Angular requiere Node.js 20+ y Angular CLI 20+.
