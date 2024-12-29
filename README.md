# Backend - Aplicativo de Reservaciones

Este proyecto es una aplicación backend para gestionar reservas de espacios. Permite a los usuarios crear, listar, y eliminar reservas para diferentes espacios disponibles. Está implementado con un sistema de bases de datos MySQL y tiene un entorno de contenedor Docker para facilitar su ejecución.

## Requisitos

Antes de comenzar con la configuración y ejecución del backend, asegúrate de tener los siguientes requisitos:

- **Docker**: Para ejecutar el contenedor de la base de datos.
- **MySQL**: Para la base de datos que almacena los usuarios, espacios y reservas.
- **ASP.NET/8 y NuGet**: Para ejecutar el servidor backend y gestionar dependencias si estás trabajando con un backend .Net.

## Configuración de la Base de Datos

### Creación de las tablas

En caso de tener problemas con las migraciones, puedes ejecutar los siguientes comandos SQL en tu codificador MySQL para crear las tablas necesarias para el funcionamiento del aplicativo.

```sql
-- Crear la tabla 'Users'
CREATE TABLE Users (
    Id INT NOT NULL AUTO_INCREMENT,
    Name LONGTEXT,
    Email LONGTEXT,
    CreatedAt DATETIME(6) NOT NULL,
    PRIMARY KEY (Id)
) CHARSET=utf8mb4;

-- Crear la tabla 'Spaces'
CREATE TABLE Spaces (
    Id INT NOT NULL AUTO_INCREMENT,
    Name LONGTEXT NOT NULL,
    Description LONGTEXT NOT NULL,
    CreatedAt DATETIME(6) NOT NULL,
    PRIMARY KEY (Id)
) CHARSET=utf8mb4;

-- Crear la tabla 'Reservations'
CREATE TABLE Reservations (
    Id INT NOT NULL AUTO_INCREMENT,
    UserId INT NOT NULL,
    SpaceId INT NOT NULL,
    StartDate DATETIME(6) NOT NULL,
    EndDate DATETIME(6) NOT NULL,
    CreatedAt DATETIME(6) NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT FK_Reservations_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_Reservations_Spaces FOREIGN KEY (SpaceId) REFERENCES Spaces(Id)
) CHARSET=utf8mb4;

-- Insertar datos de ejemplo en la tabla 'Spaces'
INSERT INTO Spaces (Name, Description, CreatedAt)
VALUES
    ('Sala de Conferencias', 'Espacio amplio para conferencias y reuniones grupales. Equipado con proyector y pizarras.', '2024-12-29 09:00:00'),
    ('Sala de Reuniones A', 'Sala de reuniones pequeña para grupos de hasta 6 personas. Equipado con TV y conexión Wi-Fi.', '2024-12-29 09:30:00'),
    ('Espacio de Coworking', 'Área compartida para trabajo individual o grupal. Conectividad Wi-Fi y escritorios ergonómicos.', '2024-12-29 10:00:00'),
    ('Auditorio Principal', 'Auditorio grande para eventos, conferencias y presentaciones. Equipado con sistema de sonido y luces.', '2024-12-29 10:30:00'),
    ('Sala de Capacitación', 'Espacio diseñado para capacitaciones y talleres, con equipo de proyección y espacios para trabajo grupal.', '2024-12-29 11:00:00');

-- Insertar un usuario de ejemplo en la tabla 'Users'
INSERT INTO Users (Name, Email, CreatedAt)
VALUES
    ('Daniel Yepes', 'daniel.yepes@gmail.com', '2024-12-29 12:00:00');
```

## Usando Docker para la Base de Datos

Se recomienda ejecutar MySQL en un contenedor Docker para facilitar la configuración y ejecución del servicio. Usa el siguiente comando para levantar el contenedor de MySQL con la base de datos preconfigurada:

```bash
docker run --hostname=85d9eb4fa6bc --mac-address=02:42:ac:11:00:03 --env=MYSQL_ROOT_PASSWORD=password --env=MYSQL_DATABASE=ReservacionesDB --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin --env=GOSU_VERSION=1.17 --env=MYSQL_MAJOR=innovation --env=MYSQL_VERSION=9.1.0-1.el9 --env=MYSQL_SHELL_VERSION=9.1.0-1.el9 --volume=/var/lib/mysql --network=bridge --workdir=/ -p 3306:3306 --restart=no --runtime=runc -d mysql:latest
```

## Migraciones

Si estás utilizando un sistema de migraciones en el backend con ASP.NET 8, debes ejecutar las migraciones correspondientes para crear las tablas y relaciones en la base de datos.

Para realizar las migraciones en ASP.NET, sigue estos pasos:

1. Abre una terminal en el directorio del proyecto.
2. Ejecuta el siguiente comando para aplicar las migraciones:

    ```bash
    dotnet ef database update
    ```
## Ejecutar el Backend

1. Ejecuta el contenedor de MySQL como se indicó anteriormente.
2. Levanta el servidor backend con el siguiente comando:

    ```bash
    dotnet run
    ```

3. Asegúrate de que la aplicación esté conectada a la base de datos MySQL y que las tablas `Users`, `Spaces`, y `Reservations` estén creadas.

