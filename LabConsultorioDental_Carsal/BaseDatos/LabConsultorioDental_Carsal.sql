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




USE LabConsultorioDental_Carsal;
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

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS: PACIENTES
-- =============================================

-- =============================================
-- 1. LISTAR PACIENTES ACTIVOS
-- =============================================
CREATE PROCEDURE paPacienteListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdPaciente, Ci, Nombres, PrimerApellido,
        SegundoApellido, FechaNacimiento, Sexo,
        Celular, Direccion, FechaRegistro, Estado,
        IdUsuarioRegistro, IdUsuarioModificacion,
        FechaModificacion
    FROM Paciente
    WHERE Estado = 1
    ORDER BY PrimerApellido, Nombres;
END;
GO

-- =============================================
-- 2. BUSCAR PACIENTE POR CI
-- =============================================
CREATE PROCEDURE paPacienteBuscarPorCi
    @Ci VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdPaciente, Ci, Nombres, PrimerApellido,
        SegundoApellido, FechaNacimiento, Sexo,
        Celular, Direccion, FechaRegistro, Estado,
        IdUsuarioRegistro, IdUsuarioModificacion,
        FechaModificacion
    FROM Paciente
    WHERE Ci = @Ci AND Estado = 1;
END;
GO

-- =============================================
-- 3. REGISTRAR PACIENTE
-- =============================================
CREATE PROCEDURE paPacienteInsertar
    @Ci VARCHAR(20),
    @Nombres VARCHAR(100),
    @PrimerApellido VARCHAR(100),
    @SegundoApellido VARCHAR(100) = NULL,
    @FechaNacimiento DATE = NULL,
    @Sexo VARCHAR(20) = NULL,
    @Celular VARCHAR(20) = NULL,
    @Direccion VARCHAR(200) = NULL,
    @IdUsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Paciente WHERE Ci = @Ci)
    BEGIN
        RAISERROR('Ya existe un paciente registrado con ese CI.', 16, 1);
        RETURN;
    END;

    INSERT INTO Paciente (
        Ci, Nombres, PrimerApellido, SegundoApellido,
        FechaNacimiento, Sexo, Celular, Direccion,
        IdUsuarioRegistro, Estado
    )
    VALUES (
        @Ci, @Nombres, @PrimerApellido, @SegundoApellido,
        @FechaNacimiento, @Sexo, @Celular, @Direccion,
        @IdUsuarioRegistro, 1
    );
END;
GO


USE LabConsultorioDental_Carsal;
GO

CREATE PROCEDURE dbo.paPacienteModificar
    @IdPaciente INT,
    @Ci VARCHAR(20),
    @Nombres VARCHAR(100),
    @PrimerApellido VARCHAR(100),
    @SegundoApellido VARCHAR(100) = NULL,
    @FechaNacimiento DATE = NULL,
    @Sexo VARCHAR(20) = NULL,
    @Celular VARCHAR(20) = NULL,
    @Direccion VARCHAR(200) = NULL,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Paciente
        WHERE Ci = @Ci
          AND IdPaciente <> @IdPaciente
          AND Estado = 1
    )
    BEGIN
        RAISERROR('Ya existe otro paciente registrado con ese CI.', 16, 1);
        RETURN;
    END;

    UPDATE Paciente
    SET Ci = @Ci,
        Nombres = @Nombres,
        PrimerApellido = @PrimerApellido,
        SegundoApellido = @SegundoApellido,
        FechaNacimiento = @FechaNacimiento,
        Sexo = @Sexo,
        Celular = @Celular,
        Direccion = @Direccion,
        IdUsuarioModificacion = @IdUsuarioModificacion,
        FechaModificacion = GETDATE()
    WHERE IdPaciente = @IdPaciente
      AND Estado = 1;
END;
GO


USE LabConsultorioDental_Carsal;
GO

CREATE PROCEDURE dbo.paPacienteEliminar
    @IdPaciente INT,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Paciente
    SET Estado = 0,
        IdUsuarioModificacion = @IdUsuarioModificacion,
        FechaModificacion = GETDATE()
    WHERE IdPaciente = @IdPaciente
      AND Estado = 1;
END;
GO


-- procedimientos almacenados DOCTOR --

USE LabConsultorioDental_Carsal;
GO

CREATE PROCEDURE dbo.paDoctorInsertar
    @Ci VARCHAR(20),
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(200),
    @Celular VARCHAR(20) = NULL,
    @Especialidad VARCHAR(100) = NULL,
    @Direccion VARCHAR(200) = NULL,
    @IdUsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM dbo.Doctor
        WHERE Ci = @Ci
    )
    BEGIN
        RAISERROR('Ya existe un doctor con ese CI.', 16, 1);
        RETURN;
    END

    INSERT INTO dbo.Doctor
    (
        Ci,
        Nombres,
        Apellidos,
        Celular,
        Especialidad,
        Direccion,
        Estado,
        IdUsuarioRegistro,
        FechaRegistro
    )
    VALUES
    (
        @Ci,
        @Nombres,
        @Apellidos,
        @Celular,
        @Especialidad,
        @Direccion,
        1,
        @IdUsuarioRegistro,
        GETDATE()
    );
END;
GO

-- procedimiento para modificar un DOCTOR --

USE LabConsultorioDental_Carsal;
GO

CREATE PROCEDURE dbo.paDoctorModificar
    @IdDoctor INT,
    @Ci VARCHAR(20),
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(200),
    @Celular VARCHAR(20) = NULL,
    @Especialidad VARCHAR(100) = NULL,
    @Direccion VARCHAR(200) = NULL,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM dbo.Doctor
        WHERE Ci = @Ci
          AND IdDoctor <> @IdDoctor
    )
    BEGIN
        RAISERROR('Ya existe otro doctor con ese CI.', 16, 1);
        RETURN;
    END

    UPDATE dbo.Doctor
    SET
        Ci = @Ci,
        Nombres = @Nombres,
        Apellidos = @Apellidos,
        Celular = @Celular,
        Especialidad = @Especialidad,
        Direccion = @Direccion,
        IdUsuarioModificacion = @IdUsuarioModificacion
    WHERE IdDoctor = @IdDoctor
      AND Estado = 1;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('No se encontró un doctor activo para modificar.', 16, 1);
        RETURN;
    END
END;
GO

-- procedimiento para eliminar un DOCTOR --

USE LabConsultorioDental_Carsal;
GO

CREATE PROCEDURE dbo.paDoctorEliminar
    @IdDoctor INT,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Doctor
    SET
        Estado = 0,
        IdUsuarioModificacion = @IdUsuarioModificacion
    WHERE IdDoctor = @IdDoctor
      AND Estado = 1;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('No se encontró un doctor activo para desactivar.', 16, 1);
        RETURN;
    END
END;
GO

-- procedimiento para Buscar un DOCTOR --

USE LabConsultorioDental_Carsal;
GO

CREATE PROCEDURE dbo.paDoctorBuscarPorCi
    @Ci VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdDoctor,
        Ci,
        Nombres,
        Apellidos,
        Celular,
        Especialidad,
        Direccion
    FROM dbo.Doctor
    WHERE Ci = @Ci
      AND Estado = 1;
END;
GO

-- procedimientos almacenados citas --

CREATE PROCEDURE dbo.paCitaListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.IdCita,
        C.IdPaciente,
        P.Ci,
        P.Nombres AS NombrePaciente,
        P.PrimerApellido,
        P.SegundoApellido,
        C.IdDoctor,
        D.Nombres AS NombreDoctor,
        D.Apellidos AS ApellidosDoctor,
        D.Especialidad,
        C.FechaHora,
        C.Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P ON C.IdPaciente = P.IdPaciente
    INNER JOIN dbo.Doctor D ON C.IdDoctor = D.IdDoctor
    ORDER BY
        CASE WHEN C.FechaHora >= GETDATE() AND C.Estado = 'Pendiente'
             THEN 0 ELSE 1 END,
        CASE WHEN C.FechaHora >= GETDATE() AND C.Estado = 'Pendiente'
             THEN C.FechaHora END ASC,
        C.FechaHora DESC;
END;
GO

-- procedimieto para buscar y recuperar los datos --

CREATE PROCEDURE dbo.paCitaBuscarPorId
    @IdCita INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.IdCita,
        C.IdPaciente,
        P.Ci,
        P.Nombres AS NombrePaciente,
        P.PrimerApellido,
        P.SegundoApellido,
        C.IdDoctor,
        D.Nombres AS NombreDoctor,
        D.Apellidos AS ApellidosDoctor,
        D.Especialidad,
        C.FechaHora,
        C.Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P
        ON C.IdPaciente = P.IdPaciente
    INNER JOIN dbo.Doctor D
        ON C.IdDoctor = D.IdDoctor
    WHERE C.IdCita = @IdCita;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paCitaInsertar
-- DESCRIPCIÓN: Registrar una nueva cita médica
-- =============================================

CREATE PROCEDURE dbo.paCitaInsertar
    @IdPaciente INT,
    @IdDoctor INT,
    @FechaHora DATETIME,
    @IdUsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        -- Validar que el paciente esté activo
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.Paciente
            WHERE IdPaciente = @IdPaciente
              AND Estado = 1
        )
        BEGIN
            RAISERROR('El paciente no existe o está inactivo.', 16, 1);
            RETURN;
        END;

        -- Validar que el doctor esté activo
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.Doctor
            WHERE IdDoctor = @IdDoctor
              AND Estado = 1
        )
        BEGIN
            RAISERROR('El doctor no existe o está inactivo.', 16, 1);
            RETURN;
        END;

        -- Validar horario de atención: 08:00 a 20:00
        IF CAST(@FechaHora AS TIME) < '08:00'
           OR CAST(@FechaHora AS TIME) >= '20:00'
           OR DATEPART(MINUTE, @FechaHora) NOT IN (0, 30)
           OR DATEPART(SECOND, @FechaHora) <> 0
        BEGIN
            RAISERROR('El horario seleccionado no es válido.', 16, 1);
            RETURN;
        END;

        -- Validar que no exista otra cita pendiente en ese horario
        IF EXISTS (
            SELECT 1
            FROM dbo.Cita
            WHERE IdDoctor = @IdDoctor
              AND FechaHora = @FechaHora
              AND Estado = 'Pendiente'
        )
        BEGIN
            RAISERROR('El doctor ya tiene una cita pendiente en ese horario.', 16, 1);
            RETURN;
        END;

        -- Registrar la cita
        INSERT INTO dbo.Cita (
            IdPaciente,
            IdDoctor,
            FechaHora,
            Estado,
            IdUsuarioRegistro
        )
        VALUES (
            @IdPaciente,
            @IdDoctor,
            @FechaHora,
            'Pendiente',
            @IdUsuarioRegistro
        );

        SELECT SCOPE_IDENTITY() AS IdCita;

    END TRY
    BEGIN CATCH
        DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Error, 16, 1);
    END CATCH;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paCitaModificar
-- DESCRIPCIÓN: Modificar una cita pendiente
-- =============================================

CREATE PROCEDURE dbo.paCitaModificar
    @IdCita INT,
    @IdDoctor INT,
    @FechaHora DATETIME,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        -- Verificar que la cita exista y esté pendiente
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.Cita
            WHERE IdCita = @IdCita
              AND Estado = 'Pendiente'
        )
        BEGIN
            RAISERROR('La cita no existe o no está pendiente.', 16, 1);
            RETURN;
        END;

        -- Verificar que el doctor esté activo
        IF NOT EXISTS (
            SELECT 1
            FROM dbo.Doctor
            WHERE IdDoctor = @IdDoctor
              AND Estado = 1
        )
        BEGIN
            RAISERROR('El doctor no existe o está inactivo.', 16, 1);
            RETURN;
        END;

        -- Validar horario de atención
        IF CAST(@FechaHora AS TIME) < '08:00'
           OR CAST(@FechaHora AS TIME) >= '20:00'
           OR DATEPART(MINUTE, @FechaHora) NOT IN (0, 30)
           OR DATEPART(SECOND, @FechaHora) <> 0
        BEGIN
            RAISERROR('El horario seleccionado no es válido.', 16, 1);
            RETURN;
        END;

        -- Verificar que el horario no esté ocupado por otra cita pendiente
        IF EXISTS (
            SELECT 1
            FROM dbo.Cita
            WHERE IdDoctor = @IdDoctor
              AND FechaHora = @FechaHora
              AND Estado = 'Pendiente'
              AND IdCita <> @IdCita
        )
        BEGIN
            RAISERROR('El doctor ya tiene otra cita en ese horario.', 16, 1);
            RETURN;
        END;

        -- Actualizar la cita
        UPDATE dbo.Cita
        SET
            IdDoctor = @IdDoctor,
            FechaHora = @FechaHora,
            IdUsuarioModificacion = @IdUsuarioModificacion,
            FechaModificacion = GETDATE()
        WHERE IdCita = @IdCita;

    END TRY
    BEGIN CATCH
        DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Error, 16, 1);
    END CATCH;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paCitaCancelar
-- DESCRIPCIÓN: Anular lógicamente una cita
-- =============================================

CREATE PROCEDURE dbo.paCitaCancelar
    @IdCita INT,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar que la cita exista y esté pendiente
    IF NOT EXISTS (
        SELECT 1
        FROM dbo.Cita
        WHERE IdCita = @IdCita
          AND Estado = 'Pendiente'
    )
    BEGIN
        RAISERROR('La cita no existe o no está pendiente.', 16, 1);
        RETURN;
    END;

    -- Anular lógicamente la cita
    UPDATE dbo.Cita
    SET
        Estado = 'Cancelada',
        IdUsuarioModificacion = @IdUsuarioModificacion,
        FechaModificacion = GETDATE()
    WHERE IdCita = @IdCita
      AND Estado = 'Pendiente';
END;
GO

-- =============================================
-- PROCEDIMIENTO: paCitaHorariosOcupados
-- DESCRIPCIÓN: Consultar horarios ocupados de un doctor
-- en una fecha determinada
-- =============================================

CREATE PROCEDURE dbo.paCitaHorariosOcupados
    @IdDoctor INT,
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CAST(FechaHora AS TIME) AS HoraOcupada
    FROM dbo.Cita
    WHERE IdDoctor = @IdDoctor
      AND CAST(FechaHora AS DATE) = @Fecha
      AND Estado = 'Pendiente'
    ORDER BY FechaHora ASC;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paCitaListarPorFecha
-- DESCRIPCIÓN: Listar todas las citas de una fecha
-- =============================================

CREATE PROCEDURE dbo.paCitaListarPorFecha
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.IdCita,
        C.IdPaciente,
        P.Ci,
        P.Nombres AS NombrePaciente,
        P.PrimerApellido,
        P.SegundoApellido,
        C.IdDoctor,
        D.Nombres AS NombreDoctor,
        D.Apellidos AS ApellidosDoctor,
        D.Especialidad,
        C.FechaHora,
        C.Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P
        ON C.IdPaciente = P.IdPaciente
    INNER JOIN dbo.Doctor D
        ON C.IdDoctor = D.IdDoctor
    WHERE CAST(C.FechaHora AS DATE) = @Fecha
    ORDER BY C.FechaHora ASC;
END;
GO
