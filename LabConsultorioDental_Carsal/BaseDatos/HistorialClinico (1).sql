USE LabConsultorioDental_Carsal;
GO

-- =============================================
-- PROCEDIMIENTOS ALMACENADOS: HISTORIAL CLINICO
-- =============================================

-- =============================================
-- PROCEDIMIENTO: paHistorialListarPorPaciente
-- DESCRIPCIÓN: Listar todas las atenciones de un paciente
--              (la más reciente primero)
-- =============================================
CREATE PROCEDURE dbo.paHistorialListarPorPaciente
    @IdPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        H.IdHistorial,
        H.IdPaciente,
        H.IdCita,
        H.FechaAtencion,
        H.Diagnostico,
        H.Tratamiento,
        H.Observaciones,
        -- Doctor de la cita; si no hubo cita, el doctor que registró
        COALESCE(
            DC.Nombres + ' ' + DC.Apellidos,
            DU.Nombres + ' ' + DU.Apellidos,
            'No registrado'
        ) AS Doctor,
        U.NombreUsuario AS RegistradoPor
    FROM dbo.HistorialClinico H
    LEFT JOIN dbo.Cita C    ON H.IdCita = C.IdCita
    LEFT JOIN dbo.Doctor DC ON C.IdDoctor = DC.IdDoctor
    LEFT JOIN dbo.Usuario U ON H.IdUsuarioRegistro = U.IdUsuario
    LEFT JOIN dbo.Doctor DU ON U.IdDoctor = DU.IdDoctor
    WHERE H.IdPaciente = @IdPaciente
    ORDER BY H.FechaAtencion DESC;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paHistorialCitasDisponibles
-- DESCRIPCIÓN: Citas del paciente que todavía se pueden
--              registrar como atendidas (Pendiente o Confirmada
--              y sin historial registrado)
-- =============================================
CREATE PROCEDURE dbo.paHistorialCitasDisponibles
    @IdPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.IdCita,
        C.FechaHora,
        CONVERT(VARCHAR(16), C.FechaHora, 120)
            + ' - Dr(a). ' + D.Nombres + ' ' + D.Apellidos
            AS Descripcion
    FROM dbo.Cita C
    INNER JOIN dbo.Doctor D ON C.IdDoctor = D.IdDoctor
    WHERE C.IdPaciente = @IdPaciente
      AND C.Estado IN ('Pendiente', 'Confirmada')
      AND NOT EXISTS (
            SELECT 1
            FROM dbo.HistorialClinico H
            WHERE H.IdCita = C.IdCita
      )
    ORDER BY C.FechaHora DESC;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paHistorialInsertar
-- DESCRIPCIÓN: Registrar una atención en el historial.
--              Si se vincula a una cita, la cita pasa a 'Atendida'.
-- =============================================
CREATE PROCEDURE dbo.paHistorialInsertar
    @IdPaciente INT,
    @IdCita INT = NULL,
    @Diagnostico VARCHAR(500),
    @Tratamiento VARCHAR(500),
    @Observaciones VARCHAR(1000) = NULL,
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

        -- Validar diagnóstico y tratamiento
        IF LTRIM(RTRIM(ISNULL(@Diagnostico, ''))) = ''
        BEGIN
            RAISERROR('Debe ingresar el diagnóstico.', 16, 1);
            RETURN;
        END;

        IF LTRIM(RTRIM(ISNULL(@Tratamiento, ''))) = ''
        BEGIN
            RAISERROR('Debe ingresar el tratamiento.', 16, 1);
            RETURN;
        END;

        IF @IdCita IS NOT NULL
        BEGIN
            -- La cita debe ser del mismo paciente y no estar cancelada ni atendida
            IF NOT EXISTS (
                SELECT 1
                FROM dbo.Cita
                WHERE IdCita = @IdCita
                  AND IdPaciente = @IdPaciente
                  AND Estado IN ('Pendiente', 'Confirmada')
            )
            BEGIN
                RAISERROR('La cita no pertenece al paciente o ya no está disponible.', 16, 1);
                RETURN;
            END;

            -- Una cita solo puede tener un registro de historial
            IF EXISTS (
                SELECT 1
                FROM dbo.HistorialClinico
                WHERE IdCita = @IdCita
            )
            BEGIN
                RAISERROR('Esta cita ya tiene una atención registrada.', 16, 1);
                RETURN;
            END;
        END;

        BEGIN TRANSACTION;

        INSERT INTO dbo.HistorialClinico (
            IdPaciente,
            IdCita,
            FechaAtencion,
            Diagnostico,
            Tratamiento,
            Observaciones,
            IdUsuarioRegistro
        )
        VALUES (
            @IdPaciente,
            @IdCita,
            GETDATE(),
            LTRIM(RTRIM(@Diagnostico)),
            LTRIM(RTRIM(@Tratamiento)),
            NULLIF(LTRIM(RTRIM(@Observaciones)), ''),
            @IdUsuarioRegistro
        );

        DECLARE @IdHistorial INT = SCOPE_IDENTITY();

        -- Marcar la cita como atendida
        IF @IdCita IS NOT NULL
        BEGIN
            UPDATE dbo.Cita
            SET Estado = 'Atendida',
                IdUsuarioModificacion = @IdUsuarioRegistro,
                FechaModificacion = GETDATE()
            WHERE IdCita = @IdCita;
        END;

        COMMIT TRANSACTION;

        SELECT CAST(@IdHistorial AS DECIMAL(18,0)) AS IdHistorial;

    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Error, 16, 1);
    END CATCH;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paHistorialModificar
-- DESCRIPCIÓN: Corregir diagnóstico, tratamiento u observaciones
--              de una atención ya registrada.
--              (El historial clínico NO se elimina, solo se corrige)
-- =============================================
CREATE PROCEDURE dbo.paHistorialModificar
    @IdHistorial INT,
    @Diagnostico VARCHAR(500),
    @Tratamiento VARCHAR(500),
    @Observaciones VARCHAR(1000) = NULL,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        IF NOT EXISTS (
            SELECT 1
            FROM dbo.HistorialClinico
            WHERE IdHistorial = @IdHistorial
        )
        BEGIN
            RAISERROR('El registro de historial no existe.', 16, 1);
            RETURN;
        END;

        IF LTRIM(RTRIM(ISNULL(@Diagnostico, ''))) = ''
        BEGIN
            RAISERROR('Debe ingresar el diagnóstico.', 16, 1);
            RETURN;
        END;

        IF LTRIM(RTRIM(ISNULL(@Tratamiento, ''))) = ''
        BEGIN
            RAISERROR('Debe ingresar el tratamiento.', 16, 1);
            RETURN;
        END;

        UPDATE dbo.HistorialClinico
        SET Diagnostico = LTRIM(RTRIM(@Diagnostico)),
            Tratamiento = LTRIM(RTRIM(@Tratamiento)),
            Observaciones = NULLIF(LTRIM(RTRIM(@Observaciones)), ''),
            IdUsuarioModificacion = @IdUsuarioModificacion,
            FechaModificacion = GETDATE()
        WHERE IdHistorial = @IdHistorial;

    END TRY
    BEGIN CATCH
        DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Error, 16, 1);
    END CATCH;
END;
GO
