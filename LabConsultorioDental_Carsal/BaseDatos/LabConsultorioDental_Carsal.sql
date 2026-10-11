USE ConsultorioDental_Carsal;
GO

SELECT DB_NAME() AS BaseDeDatosActual;
GO


USE ConsultorioDental_Carsal;
GO

CREATE TABLE Doctor (
    IdDoctor INT IDENTITY(1,1) PRIMARY KEY,
    Ci VARCHAR(20) NOT NULL UNIQUE,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Celular VARCHAR(20),
    Especialidad VARCHAR(100) NOT NULL,
    Direccion VARCHAR(200),
    Estado BIT NOT NULL DEFAULT 1
);
GO
ALTER TABLE Doctor
ADD CONSTRAINT FK_Doctor_UsuarioRegistro
    FOREIGN KEY (IdUsuarioRegistro)
    REFERENCES Usuario(IdUsuario);
GO

ALTER TABLE Doctor
ADD CONSTRAINT FK_Doctor_UsuarioModificacion
    FOREIGN KEY (IdUsuarioModificacion)
    REFERENCES Usuario(IdUsuario);
GO

CREATE TABLE Paciente (
    IdPaciente INT IDENTITY(1,1) PRIMARY KEY,
    Ci VARCHAR(20) NOT NULL UNIQUE,
    Nombres VARCHAR(100) NOT NULL,
    PrimerApellido VARCHAR(100) NOT NULL,
    SegundoApellido VARCHAR(100),
    FechaNacimiento DATE,
    Sexo VARCHAR(20),
    Celular VARCHAR(20),
    Direccion VARCHAR(200),
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);
GO
ALTER TABLE Paciente
ADD CONSTRAINT FK_Paciente_UsuarioRegistro
    FOREIGN KEY (IdUsuarioRegistro)
    REFERENCES Usuario(IdUsuario);
GO

ALTER TABLE Paciente
ADD CONSTRAINT FK_Paciente_UsuarioModificacion
    FOREIGN KEY (IdUsuarioModificacion)
    REFERENCES Usuario(IdUsuario);
GO


CREATE TABLE Cita (
    IdCita INT IDENTITY(1,1) PRIMARY KEY,
    IdPaciente INT NOT NULL,
    IdDoctor INT NOT NULL,
    FechaHora DATETIME NOT NULL,
    Motivo VARCHAR(250) NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    Observaciones VARCHAR(500),

    CONSTRAINT FK_Cita_Paciente
        FOREIGN KEY (IdPaciente)
        REFERENCES Paciente(IdPaciente),

    CONSTRAINT FK_Cita_Doctor
        FOREIGN KEY (IdDoctor)
        REFERENCES Doctor(IdDoctor),

    CONSTRAINT CK_Cita_Estado
        CHECK (Estado IN (
            'Pendiente',
            'Confirmada',
            'Atendida',
            'Cancelada'
        ))
);
GO
ALTER TABLE Cita
ADD CONSTRAINT FK_Cita_UsuarioRegistro
    FOREIGN KEY (IdUsuarioRegistro)
    REFERENCES Usuario(IdUsuario);
GO

ALTER TABLE Cita
ADD CONSTRAINT FK_Cita_UsuarioModificacion
    FOREIGN KEY (IdUsuarioModificacion)
    REFERENCES Usuario(IdUsuario);
GO


CREATE TABLE HistorialClinico (
    IdHistorial INT IDENTITY(1,1) PRIMARY KEY,
    IdPaciente INT NOT NULL,
    IdCita INT NULL,
    FechaAtencion DATETIME NOT NULL DEFAULT GETDATE(),
    Diagnostico VARCHAR(500) NOT NULL,
    Tratamiento VARCHAR(500) NOT NULL,
    Observaciones VARCHAR(1000),

    CONSTRAINT FK_HistorialClinico_Paciente
        FOREIGN KEY (IdPaciente)
        REFERENCES Paciente(IdPaciente),

    CONSTRAINT FK_HistorialClinico_Cita
        FOREIGN KEY (IdCita)
        REFERENCES Cita(IdCita)
);
GO
ALTER TABLE HistorialClinico
ADD CONSTRAINT FK_Historial_UsuarioRegistro
    FOREIGN KEY (IdUsuarioRegistro)
    REFERENCES Usuario(IdUsuario);
GO

ALTER TABLE HistorialClinico
ADD CONSTRAINT FK_Historial_UsuarioModificacion
    FOREIGN KEY (IdUsuarioModificacion)
    REFERENCES Usuario(IdUsuario);
GO

CREATE TABLE Pago (
    IdPago INT IDENTITY(1,1) PRIMARY KEY,
    IdCita INT NOT NULL,
    FechaPago DATETIME NOT NULL DEFAULT GETDATE(),
    Monto DECIMAL(10,2) NOT NULL,
    MetodoPago VARCHAR(30) NOT NULL,
    Concepto VARCHAR(250) NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',

    CONSTRAINT FK_Pago_Cita
        FOREIGN KEY (IdCita)
        REFERENCES Cita(IdCita),

    CONSTRAINT CK_Pago_Monto
        CHECK (Monto > 0),

    CONSTRAINT CK_Pago_Metodo
        CHECK (MetodoPago IN (
            'Efectivo',
            'QR'
        )),

    CONSTRAINT CK_Pago_Estado
        CHECK (Estado IN (
            'Pendiente',
            'Pagado'
        ))
);
GO
ALTER TABLE Pago
ADD CONSTRAINT FK_Pago_UsuarioRegistro
    FOREIGN KEY (IdUsuarioRegistro)
    REFERENCES Usuario(IdUsuario);
GO

ALTER TABLE Pago
ADD CONSTRAINT FK_Pago_UsuarioModificacion
    FOREIGN KEY (IdUsuarioModificacion)
    REFERENCES Usuario(IdUsuario);
GO

CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    ClaveHash VARCHAR(255) NOT NULL,
    Rol VARCHAR(20) NOT NULL,
    IdDoctor INT NULL,
    Estado BIT NOT NULL DEFAULT 1,

    CONSTRAINT FK_Usuario_Doctor
        FOREIGN KEY (IdDoctor)
        REFERENCES Doctor(IdDoctor),

    CONSTRAINT CK_Usuario_Rol
        CHECK (Rol IN (
            'Administrador',
            'Doctor'
        ))
);
GO


USE ConsultorioDental_Carsal;
GO

-- =============================================
-- TRAZABILIDAD: TABLA DOCTOR
-- =============================================

ALTER TABLE Doctor
ADD
    IdUsuarioRegistro INT NULL,
    FechaRegistro DATETIME NOT NULL
        DEFAULT GETDATE(),
    IdUsuarioModificacion INT NULL,
    FechaModificacion DATETIME NULL;
GO

-- =============================================
-- TRAZABILIDAD: TABLA PACIENTE
-- =============================================

ALTER TABLE Paciente
ADD
    IdUsuarioRegistro INT NULL,
    IdUsuarioModificacion INT NULL,
    FechaModificacion DATETIME NULL;
GO

-- =============================================
-- TRAZABILIDAD: TABLA CITA
-- =============================================

ALTER TABLE Cita
ADD
    IdUsuarioRegistro INT NULL,
    FechaRegistro DATETIME NOT NULL
        DEFAULT GETDATE(),
    IdUsuarioModificacion INT NULL,
    FechaModificacion DATETIME NULL;
GO

-- =============================================
-- TRAZABILIDAD: TABLA HISTORIAL CLINICO
-- =============================================

ALTER TABLE HistorialClinico
ADD
    IdUsuarioRegistro INT NULL,
    IdUsuarioModificacion INT NULL,
    FechaModificacion DATETIME NULL;
GO

-- =============================================
-- TRAZABILIDAD: TABLA PAGO
-- =============================================

ALTER TABLE Pago
ADD
    IdUsuarioRegistro INT NULL,
    IdUsuarioModificacion INT NULL,
    FechaModificacion DATETIME NULL;
GO




USE ConsultorioDental_Carsal;
GO

SELECT
    fk.name AS NombreRelacion,
    OBJECT_NAME(fk.parent_object_id) AS TablaOrigen,
    COL_NAME(
        fkc.parent_object_id,
        fkc.parent_column_id
    ) AS ColumnaOrigen,
    OBJECT_NAME(fk.referenced_object_id) AS TablaDestino,
    COL_NAME(
        fkc.referenced_object_id,
        fkc.referenced_column_id
    ) AS ColumnaDestino
FROM sys.foreign_keys AS fk
INNER JOIN sys.foreign_key_columns AS fkc
    ON fk.object_id = fkc.constraint_object_id
ORDER BY TablaOrigen, NombreRelacion;
GO

