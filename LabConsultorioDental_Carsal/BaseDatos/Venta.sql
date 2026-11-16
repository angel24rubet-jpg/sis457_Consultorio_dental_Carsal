USE LabConsultorioDental_Carsal;
GO

-- =====================================================================
-- VENTA: COBRO DE SERVICIOS ODONTOLÓGICOS
-- Flujo: Cita -> Historial Clínico (la cita pasa a 'Atendida') -> Cobro
--
-- Tablas:
--   Servicio     : catálogo de servicios con PRECIO REFERENCIAL
--   Pago         : maestro del cobro (ya existía)
--   DetallePago  : servicios cobrados con el PRECIO REAL de cada paciente
-- =====================================================================

-- =============================================
-- TABLA: Servicio
-- =============================================
CREATE TABLE Servicio (
    IdServicio INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(250) NULL,
    Precio DECIMAL(10,2) NOT NULL,          -- precio referencial (sugerido)
    Estado BIT NOT NULL DEFAULT 1,

    IdUsuarioRegistro INT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuarioModificacion INT NULL,
    FechaModificacion DATETIME NULL,

    CONSTRAINT CK_Servicio_Precio CHECK (Precio > 0),
    CONSTRAINT FK_Servicio_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Servicio_UsuarioModificacion
        FOREIGN KEY (IdUsuarioModificacion) REFERENCES Usuario(IdUsuario)
);
GO

-- =============================================
-- TABLA: DetallePago
-- =============================================
CREATE TABLE DetallePago (
    IdDetallePago INT IDENTITY(1,1) PRIMARY KEY,
    IdPago INT NOT NULL,
    IdServicio INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,  -- precio real cobrado a este paciente
    Subtotal DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_DetallePago_Pago
        FOREIGN KEY (IdPago) REFERENCES Pago(IdPago),
    CONSTRAINT FK_DetallePago_Servicio
        FOREIGN KEY (IdServicio) REFERENCES Servicio(IdServicio),
    CONSTRAINT CK_DetallePago_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_DetallePago_Precio CHECK (PrecioUnitario > 0)
);
GO

-- =============================================
-- DATOS: Servicios con precio referencial
-- =============================================
INSERT INTO Servicio (Nombre, Descripcion, Precio) VALUES
('Consulta y revisión',  'Evaluación general de la salud bucal',      50.00),
('Limpieza dental',      'Profilaxis y eliminación de sarro',         150.00),
('Curación con resina',  'Restauración de pieza con caries',          200.00),
('Extracción simple',    'Extracción de pieza dental',                150.00),
('Endodoncia',           'Tratamiento de conducto',                   600.00),
('Blanqueamiento',       'Blanqueamiento dental en consultorio',      800.00);
GO

-- =============================================
-- PROCEDIMIENTO: paServicioListar
-- Servicios activos (para elegir qué cobrar)
-- =============================================
CREATE PROCEDURE dbo.paServicioListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdServicio, Nombre, Descripcion, Precio
    FROM dbo.Servicio
    WHERE Estado = 1
    ORDER BY Nombre;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paPagoCitasPorCobrar
-- Citas ATENDIDAS del paciente que todavía no se pagaron
-- =============================================
CREATE PROCEDURE dbo.paPagoCitasPorCobrar
    @IdPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.IdCita,
        C.FechaHora,
        CONVERT(VARCHAR(16), C.FechaHora, 120)
            + ' - Dr(a). ' + D.Nombres + ' ' + D.Apellidos AS Descripcion
    FROM dbo.Cita C
    INNER JOIN dbo.Doctor D ON C.IdDoctor = D.IdDoctor
    WHERE C.IdPaciente = @IdPaciente
      AND C.Estado = 'Atendida'
      AND NOT EXISTS (
            SELECT 1
            FROM dbo.Pago P
            WHERE P.IdCita = C.IdCita
              AND P.Estado = 'Pagado'
      )
    ORDER BY C.FechaHora DESC;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paPagoListar
-- Todos los pagos, el más reciente primero
-- =============================================
CREATE PROCEDURE dbo.paPagoListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        P.IdPago,
        P.FechaPago,
        Pa.Ci,
        Pa.Nombres + ' ' + Pa.PrimerApellido AS Paciente,
        C.FechaHora AS FechaCita,
        P.Concepto,
        P.MetodoPago,
        P.Monto,
        P.Estado
    FROM dbo.Pago P
    INNER JOIN dbo.Cita C      ON P.IdCita = C.IdCita
    INNER JOIN dbo.Paciente Pa ON C.IdPaciente = Pa.IdPaciente
    ORDER BY P.FechaPago DESC;
END;
GO

-- =============================================
-- PROCEDIMIENTO: paDetallePagoListar
-- Servicios cobrados en un pago
-- =============================================
CREATE PROCEDURE dbo.paDetallePagoListar
    @IdPago INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        DP.IdDetallePago,
        S.Nombre AS Servicio,
        DP.Cantidad,
        DP.PrecioUnitario,
        DP.Subtotal
    FROM dbo.DetallePago DP
    INNER JOIN dbo.Servicio S ON DP.IdServicio = S.IdServicio
    WHERE DP.IdPago = @IdPago;
END;
GO

-- NOTA: el registro del pago con su detalle se hace con Entity Framework
-- (ClnCarsal.PagoCln.Registrar): guarda Pago + DetallePago en una sola
-- transacción con SaveChanges().
