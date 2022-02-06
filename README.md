# GNB_Hiberus
Proyecto de prueba para Hiberus del uso de .Net Core.
El proyecto consta de tres capas:
 - Base de datos: GNB Creada en SQL Server.
 - Backend: Api de ASP.NetCore 6
 - Frontend: Web Angular 13

## Distribución de las carpetas
### Documentacion
Datos iniciales del requerimiento, enunciado y Json para poblar la base de datos.

 Además contienen una previsualizacion del grafo de conversión de divisas y un esquema visual de la base de datos.

 ### Base de datos
 Consta de un archivo ".bak" para poder restaurar la base de datos de pruebas

 Contiene tambien un usuario de pruebas "barney" con la base de datos GNB por defecto.

 ### GNBCoreWebAPI
 Proyecto backend desarrollado en .Net core 6. En este directorio tenemos el codigo fuente del proyecto.

 Ademas tiene una implementación de Swagger para porder realizar pruebas a la API.

### GNBWebPage
Codigo Fuente en Angular 13 para poder realizar una visualizacion de los datos mediante web.

## PROBAR PROYECTO
### 1. Restaurar Base de datos
Para poder probar la base de datos sera necesario restaurar la base de datos que se encuentra en la carpeta "Base de datos" llamanda "GNB.bak".

Para ello podemos usar el programa de gestión de bases de datos "SSMS" (Microsoft SQL Server Management Studio) y seguir el siguiente tutorial

https://sqlbackupandftp.com/blog/restore-database-backup

### 2. Ejecutar GNBCoreWebAPI
Si bien es cierto que lo mejor seria compilar la solución y compartirla en un docker, no se ha podido preparar el contenedor.

Para poder probar simplemente deberemos ejecutar la Solucion la cual esta configurada para usar la version Express de IIS en "http://localhost:7289"

Pagina para probar con Swagger:

http://localhost:7289/swagger/index.html

### 3. Ejecutar GNBWebPage
Igual que en el paso anterior lo propio seria generar una compilacion y encapsularla en un contenedor.

En este caso y solo para probar tendremos que abrir una consola en la raiz de la carpeta "GNBWebPage" justo donde se encuentra el archivo de "angular.json"

Una vez abierta la consola en esa carpeta ejecutaremos el siguiente comando:

    ng s -o

Este comando se usa para el depliegue de pruebas en "localhost:4200"

## TAREAS PENDIENTES
 - Creación de Log de errores en la API
 - Creación de proyecto de tipo "TestUnit" para integrar pruebas unitarias al deplieque
  - Añadir almacenamiento en "local storage" de la web o uso de ngrx para el almacenamiento sin conexion
  - Devolver total con "redondeo Banker's Rounding"
  - Integrar las tres capas en contenedores docker, orquestarlos y subirlos a docker hub
  - Implementar los TestUnit en angular de los .spec
 - Mejorar diseño visual de la web.


