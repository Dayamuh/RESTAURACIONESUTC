
CREATE DATABASE REPARACIONESUTC
GO

USE ReparacionesUTC
GO

CREATE TABLE Tecnicos
(
	TecnicoID int identity(1,1) PRIMARY KEY,
    Nombre varchar(50),
	Especialidad varchar(50)
)

GO
CREATE TABLE Asignaciones
(
	AsignacionesID int identity(1,1) PRIMARY KEY,
	TecnicoID int,
	ReparacionID varchar(50),
	FechaAsignacion datetime CONSTRAINT df_FechaAsignacion DEFAULT GETDATE(),
	FOREIGN KEY (TecnicoID) REFERENCES Tecnicos(TecnicoID)
)

GO

CREATE TABLE Usuarios
(
    UsuarioID int identity(1,1) PRIMARY KEY,
    Nombre varchar(50) NOT NULL,
    CorreoElectronico varchar(50),
    Telefono varchar(15) CONSTRAINT UQ_Telefono UNIQUE (Telefono)
)

GO

CREATE TABLE Equipo
(
    EquipoID int identity(1,1) PRIMARY KEY,
    TipoEquipo varchar(50) NOT NULL,
    Modelo varchar(50),
    UsuarioID int,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
)

GO

CREATE TABLE Reparaciones
(
    ReparacionesID int identity(1,1) PRIMARY KEY,
    EquipoID int,
    FOREIGN KEY (EquipoID) REFERENCES Equipo(EquipoID),
    FechaSolicitud datetime CONSTRAINT df_FechaSolicitud DEFAULT GETDATE(),
    Estado char(1),
    AsignacionID int,
    FOREIGN KEY (AsignacionID) REFERENCES Asignaciones(AsignacionesID)
)

GO

 CREATE TABLE DetallesReparaciones
(
    DetallesReparacionID int identity(1,1) PRIMARY KEY,
    ReparacionID int,
    FOREIGN KEY (ReparacionID) REFERENCES Reparaciones(ReparacionesID),
    Descripcion varchar(50),
    FechaInicio datetime CONSTRAINT df_FechaInicio DEFAULT GETDATE(),
    FechaFin datetime CONSTRAINT df_FechaFin DEFAULT GETDATE()
)

GO


INSERT INTO Tecnicos(Nombre, Especialidad) VALUES 
('Alex', 'Soporte Técnico'), 
('Gerson', 'Seguridad Informática')
GO

INSERT INTO Usuarios(Nombre, CorreoElectronico, Telefono) VALUES 
('Diego', ' Diegocr@puravida.com', '89154236'), 
('Yuliana', 'Yulianacr@puravida.com', '65789412'), 
('Raquel', 'Raquelcr@puravida.com', '72459816')
GO

GO

INSERT INTO Equipo(TipoEquipo, Modelo, UsuarioID) VALUES
('Servidor', 'HP ProLiant', 1),
('Tablet', 'Samsung Galaxy Tab', 2),
('Monitor', 'LG UltraWide', 3)
GO

INSERT INTO Reparaciones(EquipoID, Estado) VALUES
(1, 'A'),
(2, 'B'),
(3, 'c')
GO

INSERT INTO Asignaciones(ReparacionID, TecnicoID) VALUES
(1, 3),
(2, 1),
(3, 2)
GO

INSERT INTO DetallesReparaciones (ReparacionID, Descripcion) VALUES
(1, 'Reemplazar la tarjeta madre en la laptop'),
(2, 'Reparación de píxeles muertos en el monitor'),
(3, 'Configuracion de software para mejorar el rendimiento en la computadora de escritorio')
GO

CREATE PROCEDURE CONSULTAR_USUARIOS
AS
	BEGIN
		SELECT * FROM Usuarios
	END
GO

CREATE PROCEDURE CONSULTAR_USUARIOS_ID
@ID INT
AS
	BEGIN
		SELECT * FROM Usuarios WHERE UsuarioID = @ID
	END
GO

CREATE PROCEDURE BORRAR_USUARIOS_ID
@ID INT
AS
	BEGIN
		DELETE Usuarios WHERE UsuarioID = @ID
	END
GO

CREATE PROCEDURE INSERTAR_USUARIO
@NOMBRE VARCHAR(50),
@CORREO VARCHAR(50),
@TELEFONO VARCHAR(15)
AS
	BEGIN
		INSERT INTO Usuarios(Nombre, CorreoElectronico, Telefono) VALUES (@NOMBRE, @CORREO, @TELEFONO)
	END
GO

CREATE PROCEDURE ACTUALIZAR_USUARIO_ID
@ID INT,
@NOMBRE VARCHAR(50),
@CORREO VARCHAR(50),
@TELEFONO VARCHAR(15)
AS
	BEGIN
		UPDATE Usuarios SET Nombre = @NOMBRE, CorreoElectronico = @CORREO, Telefono = @TELEFONO WHERE UsuarioID = @ID
	END
GO

CREATE PROCEDURE CONSULTAR_TECNICOS
AS
	BEGIN
		SELECT * FROM Tecnicos
	END
GO

CREATE PROCEDURE CONSULTAR_TECNICOS_ID
@ID INT
AS
	BEGIN
		SELECT * FROM Tecnicos WHERE TecnicoID = @ID
	END
GO

CREATE PROCEDURE BORRAR_TECNICOS_ID
@ID INT
AS
	BEGIN
		DELETE Tecnicos WHERE TecnicoID = @ID
	END
GO

CREATE PROCEDURE INSERTAR_TECNICO
@NOMBRE VARCHAR(50),
@ESPECIALIDAD VARCHAR(50)
AS
	BEGIN
		INSERT INTO Tecnicos(Nombre, Especialidad) VALUES (@NOMBRE, @ESPECIALIDAD)
	END
GO

CREATE PROCEDURE ACTUALIZAR_TECNICO_ID
@ID INT,
@NOMBRE VARCHAR(50),
@ESPECIALIDAD VARCHAR(50)
AS
	BEGIN
		UPDATE Tecnicos SET Nombre = @NOMBRE, Especialidad = @ESPECIALIDAD WHERE TecnicoID = @ID
	END
GO

CREATE PROCEDURE CONSULTAR_EQUIPO
AS
	BEGIN
		SELECT * FROM Equipo
	END
GO

CREATE PROCEDURE CONSULTAR_EQUIPO_ID
@ID INT
AS
	BEGIN
		SELECT * FROM Equipo WHERE EquipoID = @ID
	END
GO

CREATE PROCEDURE BORRAR_EQUIPO_ID
@ID INT
AS
	BEGIN
		DELETE Equipo WHERE EquipoID = @ID
	END
GO

CREATE PROCEDURE INSERTAR_EQUIPO
@TIPOEQUIPO VARCHAR(50),
@MODELO VARCHAR(50),
@USUARIOID INT
AS
	BEGIN
		INSERT INTO Equipo(TipoEquipo, Modelo, UsuarioID) VALUES (@TIPOEQUIPO, @MODELO, @USUARIOID)
	END
GO

CREATE PROCEDURE ACTUALIZAR_EQUIPO_ID
@ID INT,
@TIPOEQUIPO VARCHAR(50),
@MODELO VARCHAR(50),
@USUARIOID INT
AS
	BEGIN
		UPDATE Equipo SET TipoEquipo = @TIPOEQUIPO, Modelo = @MODELO, UsuarioID = @USUARIOID WHERE EquipoID = @ID
	END
GO

