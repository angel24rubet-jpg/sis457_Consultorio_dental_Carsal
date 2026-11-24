
USE LabConsultorioDental_Carsal;
GO

-- =============================================
-- 1. LISTAR ESPECIALIDADES
-- =============================================
CREATE OR ALTER PROCEDURE dbo.paEspecialidadListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdEspecialidad,
        Nombre,
        Descripcion,
        Estado,
        IdUsuarioRegistro,
        FechaRegistro,
        IdUsuarioModificacion,
        FechaModificacion
    FROM dbo.Especialidad
    ORDER BY IdEspecialidad DESC;
END;
GO

-- =============================================
-- 2. INSERTAR ESPECIALIDAD
-- =============================================
CREATE OR ALTER PROCEDURE dbo.paEspecialidadInsertar
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(250) = NULL,
    @IdUsuarioRegistro INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @Nombre = LTRIM(RTRIM(@Nombre));
    SET @Descripcion = NULLIF(LTRIM(RTRIM(@Descripcion)), '');

    IF @Nombre IS NULL OR @Nombre = ''
    BEGIN
        RAISERROR('El nombre de la especialidad es obligatorio.', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1
        FROM dbo.Especialidad
        WHERE Nombre = @Nombre
    )
    BEGIN
        RAISERROR('Ya existe una especialidad con ese nombre.', 16, 1);
        RETURN;
    END;

    INSERT INTO dbo.Especialidad
    (
        Nombre,
        Descripcion,
        Estado,
        IdUsuarioRegistro,
        FechaRegistro
    )
    VALUES
    (
        @Nombre,
        @Descripcion,
        1,
        @IdUsuarioRegistro,
        GETDATE()
    );
END;
GO

-- =============================================
-- 3. MODIFICAR ESPECIALIDAD
-- =============================================
CREATE OR ALTER PROCEDURE dbo.paEspecialidadModificar
    @IdEspecialidad INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(250) = NULL,
    @IdUsuarioModificacion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @Nombre = LTRIM(RTRIM(@Nombre));
    SET @Descripcion = NULLIF(LTRIM(RTRIM(@Descripcion)), '');

    IF @IdEspecialidad <= 0
    BEGIN
        RAISERROR('Seleccione una especialidad válida.', 16, 1);
        RETURN;
    END;

    IF @Nombre IS NULL OR @Nombre = ''
    BEGIN
        RAISERROR('El nombre de la especialidad es obligatorio.', 16, 1);
        RETURN;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.Especialidad
        WHERE IdEspecialidad = @IdEspecialidad
    )
    BEGIN
        RAISERROR('La especialidad no existe.', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1
        FROM dbo.Especialidad
        WHERE Nombre = @Nombre
          AND IdEspecialidad <> @IdEspecialidad
    )
    BEGIN
        RAISERROR('Ya existe otra especialidad con ese nombre.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Especialidad
    SET
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        IdUsuarioModificacion = @IdUsuarioModificacion,
        FechaModificacion = GETDATE()
    WHERE IdEspecialidad = @IdEspecialidad;
END;
GO

-- =============================================
-- 4. ELIMINAR (DESACTIVAR) ESPECIALIDAD
-- =============================================
CREATE OR ALTER PROCEDURE dbo.paEspecialidadEliminar
    @IdEspecialidad INT,
    @IdUsuarioModificacion INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdEspecialidad <= 0
    BEGIN
        RAISERROR('Seleccione una especialidad válida.', 16, 1);
        RETURN;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.Especialidad
        WHERE IdEspecialidad = @IdEspecialidad
          AND Estado = 1
    )
    BEGIN
        RAISERROR('La especialidad no existe o ya está inactiva.', 16, 1);
        RETURN;
    END;

    IF EXISTS (
        SELECT 1
        FROM dbo.Doctor
        WHERE IdEspecialidad = @IdEspecialidad
          AND Estado = 1
    )
    BEGIN
        RAISERROR('No se puede desactivar: existen doctores activos con esta especialidad.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Especialidad
    SET
        Estado = 0,
        IdUsuarioModificacion = @IdUsuarioModificacion,
        FechaModificacion = GETDATE()
    WHERE IdEspecialidad = @IdEspecialidad;
END;
GO

-- =============================================
-- 5. COMPROBAR PROCEDIMIENTOS
-- =============================================
EXEC dbo.paEspecialidadListar;
GO
