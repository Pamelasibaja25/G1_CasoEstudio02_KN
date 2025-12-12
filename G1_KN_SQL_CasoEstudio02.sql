--CREACIÓN DE BASE DE DATOS
CREATE DATABASE CasoEstudioKN;
GO

USE CasoEstudioKN;
GO

--CREACIÓN DE TABLA: CasasSistema
CREATE TABLE CasasSistema (
    IdCasa BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    DescripcionCasa VARCHAR(30) NOT NULL,
    PrecioCasa DECIMAL(10,2) NOT NULL,
    UsuarioAlquiler VARCHAR(30) NULL,
    FechaAlquiler DATETIME NULL
);
GO

--INSERTS INICIALES
INSERT INTO CasasSistema (DescripcionCasa, PrecioCasa, UsuarioAlquiler, FechaAlquiler)
VALUES 
('Casa en San José',190000,NULL,NULL),
('Casa en Alajuela',145000,NULL,NULL),
('Casa en Cartago',115000,NULL,NULL),
('Casa en Heredia',122000,NULL,NULL),
('Casa en Guanacaste',105000,NULL,NULL);
GO

--PROCEDIMIENTOS
--Vista Consulta
CREATE PROCEDURE SP_ConsultarCasas
AS
BEGIN
    SELECT 
        IdCasa,
        DescripcionCasa,
        PrecioCasa,
        UsuarioAlquiler,
        Estado = CASE 
                    WHEN UsuarioAlquiler IS NULL THEN 'Disponible'
                    ELSE 'Reservada'
                 END,
        FechaAlquiler = FORMAT(FechaAlquiler, 'dd/MM/yyyy')
    FROM CasasSistema
    WHERE PrecioCasa BETWEEN 115000 AND 180000
    ORDER BY 
        CASE 
            WHEN UsuarioAlquiler IS NULL THEN 0 
            ELSE 1 
        END;
END;
GO

--DropdownList
CREATE PROCEDURE SP_CasasDisponibles
AS
BEGIN
    SELECT 
        IdCasa,
        DescripcionCasa,
        PrecioCasa
    FROM CasasSistema
    WHERE UsuarioAlquiler IS NULL;
END;
GO

--Obtener Precio por casa
CREATE PROCEDURE SP_ObtenerCasaPorId
    @IdCasa BIGINT
AS
BEGIN
    SELECT 
        IdCasa,
        DescripcionCasa,
        PrecioCasa,
        UsuarioAlquiler,
        FechaAlquiler
    FROM CasasSistema
    WHERE IdCasa = @IdCasa;
END;
GO

--Actualizar Alquiler
CREATE PROCEDURE SP_AlquilarCasa
    @IdCasa BIGINT,
    @UsuarioAlquiler VARCHAR(30)
AS
BEGIN
    UPDATE CasasSistema
    SET 
        UsuarioAlquiler = @UsuarioAlquiler,
        FechaAlquiler = GETDATE()
    WHERE IdCasa = @IdCasa
      AND UsuarioAlquiler IS NULL;
END;
GO

