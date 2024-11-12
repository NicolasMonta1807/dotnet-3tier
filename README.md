# Proyecto de Gestión de Espacios

Este proyecto es una aplicación de tres capas construida con .NET que proporciona una capa de datos gRPC, una API REST, y una interfaz de usuario en Blazor Pages para gestionar espacios y horarios.

## Instrucciones

### 1. Generar Tablas en la Base de Datos
Ejecuta el siguiente script SQL para crear las tablas necesarias en tu base de datos MySQL:

```sql
CREATE TABLE espacios (
    id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(64) NOT NULL,
    hora_apertura TIME NOT NULL,
    hora_cierre TIME NOT NULL
);

CREATE TABLE horarios (
    id INT PRIMARY KEY AUTO_INCREMENT,
    espacio_id INT NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL,
    capacidad INT NOT NULL,
    FOREIGN KEY (espacio_id) REFERENCES espacios(id)
);
```

El script se encuentra en el archivo `data.ddl` en la raíz del repositorio.

### 2. Asignar Secretos de Conexión a la Base de Datos
Sigue estos pasos para configurar los secretos de conexión a la base de datos:

1. Navega al proyecto de datos:
   ```bash
   cd EspaciosGrpcDatos
   ```
2. Inicializa el sistema de secretos de usuario:
   ```bash
   dotnet user-secrets init
   ```
3. Configura la cadena de conexión (cambia `<user>`, `<password>`, y `<database-name>` por tus credenciales):
   ```bash
   dotnet user-secrets set "ConnectionStrings:DatabaseConnection" "server=localhost;port=3306;user=<user>;password=<password>;database=<database-name>"
   ```

### 3. Compilar el Proyecto
En la raíz del proyecto (donde se encuentra el archivo `.sln`), ejecuta los siguientes comandos:

```bash
dotnet clean
dotnet build
```

### 4. Ejecutar los Proyectos
Ejecuta cada proyecto por separado desde la raíz del proyecto:

1. Ejecutar el proyecto gRPC (capa de datos):
   ```bash
   dotnet run --project EspaciosGrpcDatos
   ```
2. Ejecutar la API REST (capa lógica):
   ```bash
   dotnet run --project EspaciosLogicAPI
   ```
3. Ejecutar la aplicación Blazor Pages (interfaz de usuario):
   ```bash
   dotnet run --project EspaciosBlazorApp
   ```

### 5. Definiciones de la API REST
Después de ejecutar el proyecto `EspaciosLogicAPI`, puedes encontrar la documentación de la API REST en:

[http://localhost:5088/swagger/index.html](http://localhost:5088/swagger/index.html)

