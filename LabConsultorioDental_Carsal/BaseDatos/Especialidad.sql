USE LabConsultorioDental_Carsal;
GO

CREATE TABLE Especialidad (
    IdEspecialidad INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(250) NULL,
    Estado BIT NOT NULL DEFAULT 1,

    -- Trazabilidad
    IdUsuarioRegistro INT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuarioModificacion INT NULL,
    FechaModificacion DATETIME NULL,

    CONSTRAINT FK_Especialidad_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Especialidad_UsuarioModificacion
        FOREIGN KEY (IdUsuarioModificacion) REFERENCES Usuario(IdUsuario)
);
GO
-- PARTE 2.1: Especialidades iniciales
INSERT INTO Especialidad (Nombre, Descripcion) VALUES
('Odontología general', 'Diagnóstico, limpieza y tratamientos básicos'),
('Ortodoncia',          'Corrección de la posición de dientes y maxilares'),
('Endodoncia',          'Tratamiento de conductos y pulpa dental'),
('Periodoncia',         'Tratamiento de encías y tejidos de soporte'),
('Odontopediatría',     'Atención dental de niños'),
('Cirugía bucal',       'Extracciones complejas y cirugías de la boca'),
('Prótesis dental',     'Coronas, puentes y dentaduras'),
('Estética dental',     'Blanqueamiento y carillas');
GO

-- PARTE 2.2: Agregar las que ya escribieron en los doctores y no están en la lista
INSERT INTO Especialidad (Nombre)
SELECT DISTINCT LTRIM(RTRIM(D.Especialidad))
FROM Doctor D
WHERE LTRIM(RTRIM(ISNULL(D.Especialidad, ''))) <> ''
  AND NOT EXISTS (
        SELECT 1
        FROM Especialidad E
        WHERE E.Nombre = LTRIM(RTRIM(D.Especialidad))
  );
GO

-- Comprobar
SELECT * FROM Especialidad;

-- 3.1 Agregar la columna (primero permite NULL)
ALTER TABLE Doctor ADD IdEspecialidad INT NULL;
GO

-- 3.2 A cada doctor le asigna el Id de su especialidad (comparando el texto)
UPDATE D
SET D.IdEspecialidad = E.IdEspecialidad
FROM Doctor D
INNER JOIN Especialidad E
    ON E.Nombre = LTRIM(RTRIM(D.Especialidad));
GO

-- 3.3 Doctores sin especialidad -> Odontología general
UPDATE Doctor
SET IdEspecialidad = (SELECT IdEspecialidad FROM Especialidad
                      WHERE Nombre = 'Odontología general')
WHERE IdEspecialidad IS NULL;
GO

-- 3.4 Ahora sí: obligatoria y con llave foránea
ALTER TABLE Doctor ALTER COLUMN IdEspecialidad INT NOT NULL;
GO

ALTER TABLE Doctor ADD CONSTRAINT FK_Doctor_Especialidad
    FOREIGN KEY (IdEspecialidad) REFERENCES Especialidad(IdEspecialidad);
GO

-- Comprobar: cada doctor con su texto viejo y su Id nuevo
SELECT D.Nombres, D.Especialidad AS TextoViejo, D.IdEspecialidad, E.Nombre AS EspecialidadNueva
FROM Doctor D
INNER JOIN Especialidad E ON D.IdEspecialidad = E.IdEspecialidad;

-- Pasar a julio (y cualquier otro) de "protecis" a "Prótesis dental"
UPDATE Doctor
SET IdEspecialidad = (SELECT IdEspecialidad FROM Especialidad WHERE Nombre = 'Prótesis dental')
WHERE IdEspecialidad = (SELECT IdEspecialidad FROM Especialidad WHERE Nombre = 'protecis');
GO

-- Borrar la especialidad mal escrita (ya ningún doctor la usa)
DELETE FROM Especialidad WHERE Nombre = 'protecis';
GO

-- Comprobar
SELECT D.Nombres, D.IdEspecialidad, E.Nombre AS Especialidad
FROM Doctor D
INNER JOIN Especialidad E ON D.IdEspecialidad = E.IdEspecialidad;

-- 4.1 Registrar doctor: ahora recibe @IdEspecialidad (número) en vez del texto
ALTER PROCEDURE dbo.paDoctorInsertar
    @Ci VARCHAR(20),
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(200),
    @Celular VARCHAR(20) = NULL,
    @IdEspecialidad INT,
    @Direccion VARCHAR(200) = NULL,
    @IdUsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.Doctor WHERE Ci = @Ci)
    BEGIN
        RAISERROR('Ya existe un doctor con ese CI.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Especialidad
                   WHERE IdEspecialidad = @IdEspecialidad AND Estado = 1)
    BEGIN
        RAISERROR('La especialidad no existe o está inactiva.', 16, 1);
        RETURN;
    END

    INSERT INTO dbo.Doctor
        (Ci, Nombres, Apellidos, Celular, IdEspecialidad, Direccion,
         Estado, IdUsuarioRegistro, FechaRegistro)
    VALUES
        (@Ci, @Nombres, @Apellidos, @Celular, @IdEspecialidad, @Direccion,
         1, @IdUsuarioRegistro, GETDATE());
END;
GO

-- 4.2 Modificar doctor: igual, recibe @IdEspecialidad
ALTER PROCEDURE dbo.paDoctorModificar
    @IdDoctor INT,
    @Ci VARCHAR(20),
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(200),
    @Celular VARCHAR(20) = NULL,
    @IdEspecialidad INT,
    @Direccion VARCHAR(200) = NULL,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.Doctor
               WHERE Ci = @Ci AND IdDoctor <> @IdDoctor)
    BEGIN
        RAISERROR('Ya existe otro doctor con ese CI.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Especialidad
                   WHERE IdEspecialidad = @IdEspecialidad AND Estado = 1)
    BEGIN
        RAISERROR('La especialidad no existe o está inactiva.', 16, 1);
        RETURN;
    END

    UPDATE dbo.Doctor
    SET Ci = @Ci,
        Nombres = @Nombres,
        Apellidos = @Apellidos,
        Celular = @Celular,
        IdEspecialidad = @IdEspecialidad,
        Direccion = @Direccion,
        IdUsuarioModificacion = @IdUsuarioModificacion,
        FechaModificacion = GETDATE()
    WHERE IdDoctor = @IdDoctor
      AND Estado = 1;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('No se encontró un doctor activo para modificar.', 16, 1);
        RETURN;
    END
END;
GO

-- 4.3 Buscar doctor por CI: trae el NOMBRE de la especialidad con un JOIN
ALTER PROCEDURE dbo.paDoctorBuscarPorCi
    @Ci VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        D.IdDoctor,
        D.Ci,
        D.Nombres,
        D.Apellidos,
        D.Celular,
        E.Nombre AS Especialidad,
        D.Direccion
    FROM dbo.Doctor D
    INNER JOIN dbo.Especialidad E ON D.IdEspecialidad = E.IdEspecialidad
    WHERE D.Ci = @Ci
      AND D.Estado = 1;
END;
GO

-- 4.4 Listar citas
ALTER PROCEDURE dbo.paCitaListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.IdCita, C.IdPaciente, P.Ci,
        P.Nombres AS NombrePaciente, P.PrimerApellido, P.SegundoApellido,
        C.IdDoctor, D.Nombres AS NombreDoctor, D.Apellidos AS ApellidosDoctor,
        E.Nombre AS Especialidad,
        C.FechaHora, C.Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P     ON C.IdPaciente = P.IdPaciente
    INNER JOIN dbo.Doctor D       ON C.IdDoctor = D.IdDoctor
    INNER JOIN dbo.Especialidad E ON D.IdEspecialidad = E.IdEspecialidad
    ORDER BY
        CASE WHEN C.FechaHora >= GETDATE() AND C.Estado = 'Pendiente'
             THEN 0 ELSE 1 END,
        CASE WHEN C.FechaHora >= GETDATE() AND C.Estado = 'Pendiente'
             THEN C.FechaHora END ASC,
        C.FechaHora DESC;
END;
GO

-- 4.5 Buscar cita por Id
ALTER PROCEDURE dbo.paCitaBuscarPorId
    @IdCita INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.IdCita, C.IdPaciente, P.Ci,
        P.Nombres AS NombrePaciente, P.PrimerApellido, P.SegundoApellido,
        C.IdDoctor, D.Nombres AS NombreDoctor, D.Apellidos AS ApellidosDoctor,
        E.Nombre AS Especialidad,
        C.FechaHora, C.Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P     ON C.IdPaciente = P.IdPaciente
    INNER JOIN dbo.Doctor D       ON C.IdDoctor = D.IdDoctor
    INNER JOIN dbo.Especialidad E ON D.IdEspecialidad = E.IdEspecialidad
    WHERE C.IdCita = @IdCita;
END;
GO

-- 4.6 Listar citas por fecha
ALTER PROCEDURE dbo.paCitaListarPorFecha
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.IdCita, C.IdPaciente, P.Ci,
        P.Nombres AS NombrePaciente, P.PrimerApellido, P.SegundoApellido,
        C.IdDoctor, D.Nombres AS NombreDoctor, D.Apellidos AS ApellidosDoctor,
        E.Nombre AS Especialidad,
        C.FechaHora, C.Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P     ON C.IdPaciente = P.IdPaciente
    INNER JOIN dbo.Doctor D       ON C.IdDoctor = D.IdDoctor
    INNER JOIN dbo.Especialidad E ON D.IdEspecialidad = E.IdEspecialidad
    WHERE CAST(C.FechaHora AS DATE) = @Fecha
    ORDER BY C.FechaHora ASC;
END;
GO

-- Quitar la columna de texto vieja (ya nadie la usa)
ALTER TABLE Doctor DROP COLUMN Especialidad;
GO

-- Comprobar: columnas de Doctor
SELECT COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Doctor';

-- Comprobar: los procedimientos siguen funcionando
EXEC dbo.paCitaListar;