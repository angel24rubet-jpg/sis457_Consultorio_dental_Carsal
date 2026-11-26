-- =====================================================================
-- MIGRACIÓN: CONVENCIÓN DE NOMBRES (observación del ingeniero)
-- Base: LabConsultorioDental_Carsal
--
-- Convención aplicada (igual que el proyecto Minerva de clase):
--   * Llave primaria de cada tabla:   id
--   * Llaves foráneas:                idPaciente, idDoctor, idCita...
--   * Demás campos en camelCase:      primerApellido, fechaHora, metodoPago...
--   * Restricciones:                  fk_Tabla_Referencia, ck_Tabla_Campo
--
-- NO se pierden datos: solo se renombran columnas y restricciones,
-- y se actualizan los procedimientos almacenados.
-- Se puede ejecutar más de una vez sin error.
-- =====================================================================
USE LabConsultorioDental_Carsal;
GO

-- =====================================================================
-- PARTE 0: QUITAR LAS REGLAS CHECK
-- (SQL Server no deja renombrar una columna que tiene una regla CHECK;
--  se vuelven a crear en la PARTE 3 con los nombres nuevos)
-- =====================================================================
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql = @sql + N'ALTER TABLE dbo.' + QUOTENAME(OBJECT_NAME(parent_object_id))
            + N' DROP CONSTRAINT ' + QUOTENAME(name) + N';' + CHAR(10)
FROM sys.check_constraints
WHERE OBJECT_NAME(parent_object_id) IN
      ('Usuario','Especialidad','Doctor','Paciente','Cita',
       'HistorialClinico','Servicio','Pago','DetallePago');
EXEC sp_executesql @sql;
GO

-- =====================================================================
-- PARTE 1: RENOMBRAR COLUMNAS
-- =====================================================================
-- Usuario
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Usuario') AND name COLLATE Latin1_General_BIN = 'IdUsuario')
    EXEC sp_rename 'dbo.Usuario.IdUsuario', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Usuario') AND name COLLATE Latin1_General_BIN = 'NombreUsuario')
    EXEC sp_rename 'dbo.Usuario.NombreUsuario', 'nombreUsuario', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Usuario') AND name COLLATE Latin1_General_BIN = 'ClaveHash')
    EXEC sp_rename 'dbo.Usuario.ClaveHash', 'claveHash', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Usuario') AND name COLLATE Latin1_General_BIN = 'Rol')
    EXEC sp_rename 'dbo.Usuario.Rol', 'rol', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Usuario') AND name COLLATE Latin1_General_BIN = 'IdDoctor')
    EXEC sp_rename 'dbo.Usuario.IdDoctor', 'idDoctor', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Usuario') AND name COLLATE Latin1_General_BIN = 'Estado')
    EXEC sp_rename 'dbo.Usuario.Estado', 'estado', 'COLUMN';

-- Especialidad
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Especialidad') AND name COLLATE Latin1_General_BIN = 'IdEspecialidad')
    EXEC sp_rename 'dbo.Especialidad.IdEspecialidad', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Especialidad') AND name COLLATE Latin1_General_BIN = 'Nombre')
    EXEC sp_rename 'dbo.Especialidad.Nombre', 'nombre', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Especialidad') AND name COLLATE Latin1_General_BIN = 'Descripcion')
    EXEC sp_rename 'dbo.Especialidad.Descripcion', 'descripcion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Especialidad') AND name COLLATE Latin1_General_BIN = 'Estado')
    EXEC sp_rename 'dbo.Especialidad.Estado', 'estado', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Especialidad') AND name COLLATE Latin1_General_BIN = 'IdUsuarioRegistro')
    EXEC sp_rename 'dbo.Especialidad.IdUsuarioRegistro', 'idUsuarioRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Especialidad') AND name COLLATE Latin1_General_BIN = 'FechaRegistro')
    EXEC sp_rename 'dbo.Especialidad.FechaRegistro', 'fechaRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Especialidad') AND name COLLATE Latin1_General_BIN = 'IdUsuarioModificacion')
    EXEC sp_rename 'dbo.Especialidad.IdUsuarioModificacion', 'idUsuarioModificacion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Especialidad') AND name COLLATE Latin1_General_BIN = 'FechaModificacion')
    EXEC sp_rename 'dbo.Especialidad.FechaModificacion', 'fechaModificacion', 'COLUMN';

-- Doctor
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'IdDoctor')
    EXEC sp_rename 'dbo.Doctor.IdDoctor', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'Ci')
    EXEC sp_rename 'dbo.Doctor.Ci', 'ci', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'Nombres')
    EXEC sp_rename 'dbo.Doctor.Nombres', 'nombres', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'Apellidos')
    EXEC sp_rename 'dbo.Doctor.Apellidos', 'apellidos', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'Celular')
    EXEC sp_rename 'dbo.Doctor.Celular', 'celular', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'Direccion')
    EXEC sp_rename 'dbo.Doctor.Direccion', 'direccion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'Estado')
    EXEC sp_rename 'dbo.Doctor.Estado', 'estado', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'IdUsuarioRegistro')
    EXEC sp_rename 'dbo.Doctor.IdUsuarioRegistro', 'idUsuarioRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'FechaRegistro')
    EXEC sp_rename 'dbo.Doctor.FechaRegistro', 'fechaRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'IdUsuarioModificacion')
    EXEC sp_rename 'dbo.Doctor.IdUsuarioModificacion', 'idUsuarioModificacion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'FechaModificacion')
    EXEC sp_rename 'dbo.Doctor.FechaModificacion', 'fechaModificacion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Doctor') AND name COLLATE Latin1_General_BIN = 'IdEspecialidad')
    EXEC sp_rename 'dbo.Doctor.IdEspecialidad', 'idEspecialidad', 'COLUMN';

-- Paciente
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'IdPaciente')
    EXEC sp_rename 'dbo.Paciente.IdPaciente', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'Ci')
    EXEC sp_rename 'dbo.Paciente.Ci', 'ci', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'Nombres')
    EXEC sp_rename 'dbo.Paciente.Nombres', 'nombres', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'PrimerApellido')
    EXEC sp_rename 'dbo.Paciente.PrimerApellido', 'primerApellido', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'SegundoApellido')
    EXEC sp_rename 'dbo.Paciente.SegundoApellido', 'segundoApellido', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'FechaNacimiento')
    EXEC sp_rename 'dbo.Paciente.FechaNacimiento', 'fechaNacimiento', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'Sexo')
    EXEC sp_rename 'dbo.Paciente.Sexo', 'sexo', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'Celular')
    EXEC sp_rename 'dbo.Paciente.Celular', 'celular', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'Direccion')
    EXEC sp_rename 'dbo.Paciente.Direccion', 'direccion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'Estado')
    EXEC sp_rename 'dbo.Paciente.Estado', 'estado', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'FechaRegistro')
    EXEC sp_rename 'dbo.Paciente.FechaRegistro', 'fechaRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'IdUsuarioRegistro')
    EXEC sp_rename 'dbo.Paciente.IdUsuarioRegistro', 'idUsuarioRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'IdUsuarioModificacion')
    EXEC sp_rename 'dbo.Paciente.IdUsuarioModificacion', 'idUsuarioModificacion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Paciente') AND name COLLATE Latin1_General_BIN = 'FechaModificacion')
    EXEC sp_rename 'dbo.Paciente.FechaModificacion', 'fechaModificacion', 'COLUMN';

-- Cita
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'IdCita')
    EXEC sp_rename 'dbo.Cita.IdCita', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'IdPaciente')
    EXEC sp_rename 'dbo.Cita.IdPaciente', 'idPaciente', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'IdDoctor')
    EXEC sp_rename 'dbo.Cita.IdDoctor', 'idDoctor', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'FechaHora')
    EXEC sp_rename 'dbo.Cita.FechaHora', 'fechaHora', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'Estado')
    EXEC sp_rename 'dbo.Cita.Estado', 'estado', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'IdUsuarioRegistro')
    EXEC sp_rename 'dbo.Cita.IdUsuarioRegistro', 'idUsuarioRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'FechaRegistro')
    EXEC sp_rename 'dbo.Cita.FechaRegistro', 'fechaRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'IdUsuarioModificacion')
    EXEC sp_rename 'dbo.Cita.IdUsuarioModificacion', 'idUsuarioModificacion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Cita') AND name COLLATE Latin1_General_BIN = 'FechaModificacion')
    EXEC sp_rename 'dbo.Cita.FechaModificacion', 'fechaModificacion', 'COLUMN';

-- HistorialClinico
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'IdHistorial')
    EXEC sp_rename 'dbo.HistorialClinico.IdHistorial', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'IdPaciente')
    EXEC sp_rename 'dbo.HistorialClinico.IdPaciente', 'idPaciente', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'IdCita')
    EXEC sp_rename 'dbo.HistorialClinico.IdCita', 'idCita', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'FechaAtencion')
    EXEC sp_rename 'dbo.HistorialClinico.FechaAtencion', 'fechaAtencion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'Diagnostico')
    EXEC sp_rename 'dbo.HistorialClinico.Diagnostico', 'diagnostico', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'Tratamiento')
    EXEC sp_rename 'dbo.HistorialClinico.Tratamiento', 'tratamiento', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'Observaciones')
    EXEC sp_rename 'dbo.HistorialClinico.Observaciones', 'observaciones', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'IdUsuarioRegistro')
    EXEC sp_rename 'dbo.HistorialClinico.IdUsuarioRegistro', 'idUsuarioRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'IdUsuarioModificacion')
    EXEC sp_rename 'dbo.HistorialClinico.IdUsuarioModificacion', 'idUsuarioModificacion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.HistorialClinico') AND name COLLATE Latin1_General_BIN = 'FechaModificacion')
    EXEC sp_rename 'dbo.HistorialClinico.FechaModificacion', 'fechaModificacion', 'COLUMN';

-- Servicio
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'IdServicio')
    EXEC sp_rename 'dbo.Servicio.IdServicio', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'Nombre')
    EXEC sp_rename 'dbo.Servicio.Nombre', 'nombre', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'Descripcion')
    EXEC sp_rename 'dbo.Servicio.Descripcion', 'descripcion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'Precio')
    EXEC sp_rename 'dbo.Servicio.Precio', 'precio', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'Estado')
    EXEC sp_rename 'dbo.Servicio.Estado', 'estado', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'IdUsuarioRegistro')
    EXEC sp_rename 'dbo.Servicio.IdUsuarioRegistro', 'idUsuarioRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'FechaRegistro')
    EXEC sp_rename 'dbo.Servicio.FechaRegistro', 'fechaRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'IdUsuarioModificacion')
    EXEC sp_rename 'dbo.Servicio.IdUsuarioModificacion', 'idUsuarioModificacion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Servicio') AND name COLLATE Latin1_General_BIN = 'FechaModificacion')
    EXEC sp_rename 'dbo.Servicio.FechaModificacion', 'fechaModificacion', 'COLUMN';

-- Pago
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'IdPago')
    EXEC sp_rename 'dbo.Pago.IdPago', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'IdCita')
    EXEC sp_rename 'dbo.Pago.IdCita', 'idCita', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'FechaPago')
    EXEC sp_rename 'dbo.Pago.FechaPago', 'fechaPago', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'Monto')
    EXEC sp_rename 'dbo.Pago.Monto', 'monto', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'MetodoPago')
    EXEC sp_rename 'dbo.Pago.MetodoPago', 'metodoPago', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'Concepto')
    EXEC sp_rename 'dbo.Pago.Concepto', 'concepto', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'Estado')
    EXEC sp_rename 'dbo.Pago.Estado', 'estado', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'IdUsuarioRegistro')
    EXEC sp_rename 'dbo.Pago.IdUsuarioRegistro', 'idUsuarioRegistro', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'IdUsuarioModificacion')
    EXEC sp_rename 'dbo.Pago.IdUsuarioModificacion', 'idUsuarioModificacion', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.Pago') AND name COLLATE Latin1_General_BIN = 'FechaModificacion')
    EXEC sp_rename 'dbo.Pago.FechaModificacion', 'fechaModificacion', 'COLUMN';

-- DetallePago
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.DetallePago') AND name COLLATE Latin1_General_BIN = 'IdDetallePago')
    EXEC sp_rename 'dbo.DetallePago.IdDetallePago', 'id', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.DetallePago') AND name COLLATE Latin1_General_BIN = 'IdPago')
    EXEC sp_rename 'dbo.DetallePago.IdPago', 'idPago', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.DetallePago') AND name COLLATE Latin1_General_BIN = 'IdServicio')
    EXEC sp_rename 'dbo.DetallePago.IdServicio', 'idServicio', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.DetallePago') AND name COLLATE Latin1_General_BIN = 'Cantidad')
    EXEC sp_rename 'dbo.DetallePago.Cantidad', 'cantidad', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.DetallePago') AND name COLLATE Latin1_General_BIN = 'PrecioUnitario')
    EXEC sp_rename 'dbo.DetallePago.PrecioUnitario', 'precioUnitario', 'COLUMN';
IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.DetallePago') AND name COLLATE Latin1_General_BIN = 'Subtotal')
    EXEC sp_rename 'dbo.DetallePago.Subtotal', 'subtotal', 'COLUMN';
GO

-- Paciente: basta con un apellido (primero o segundo); la regla la valida PacienteCln
ALTER TABLE dbo.Paciente ALTER COLUMN primerApellido VARCHAR(100) NULL;
GO

-- =====================================================================
-- PARTE 2: RENOMBRAR LLAVES FORÁNEAS
-- =====================================================================
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Cita_Doctor')
    EXEC sp_rename 'dbo.FK_Cita_Doctor', 'fk_Cita_Doctor', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Cita_Paciente')
    EXEC sp_rename 'dbo.FK_Cita_Paciente', 'fk_Cita_Paciente', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Cita_UsuarioModificacion')
    EXEC sp_rename 'dbo.FK_Cita_UsuarioModificacion', 'fk_Cita_UsuarioModificacion', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Cita_UsuarioRegistro')
    EXEC sp_rename 'dbo.FK_Cita_UsuarioRegistro', 'fk_Cita_UsuarioRegistro', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_DetallePago_Pago')
    EXEC sp_rename 'dbo.FK_DetallePago_Pago', 'fk_DetallePago_Pago', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_DetallePago_Servicio')
    EXEC sp_rename 'dbo.FK_DetallePago_Servicio', 'fk_DetallePago_Servicio', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Doctor_Especialidad')
    EXEC sp_rename 'dbo.FK_Doctor_Especialidad', 'fk_Doctor_Especialidad', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Doctor_UsuarioModificacion')
    EXEC sp_rename 'dbo.FK_Doctor_UsuarioModificacion', 'fk_Doctor_UsuarioModificacion', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Doctor_UsuarioRegistro')
    EXEC sp_rename 'dbo.FK_Doctor_UsuarioRegistro', 'fk_Doctor_UsuarioRegistro', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Especialidad_UsuarioModificacion')
    EXEC sp_rename 'dbo.FK_Especialidad_UsuarioModificacion', 'fk_Especialidad_UsuarioModificacion', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Especialidad_UsuarioRegistro')
    EXEC sp_rename 'dbo.FK_Especialidad_UsuarioRegistro', 'fk_Especialidad_UsuarioRegistro', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Historial_UsuarioModificacion')
    EXEC sp_rename 'dbo.FK_Historial_UsuarioModificacion', 'fk_HistorialClinico_UsuarioModificacion', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Historial_UsuarioRegistro')
    EXEC sp_rename 'dbo.FK_Historial_UsuarioRegistro', 'fk_HistorialClinico_UsuarioRegistro', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_HistorialClinico_Cita')
    EXEC sp_rename 'dbo.FK_HistorialClinico_Cita', 'fk_HistorialClinico_Cita', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_HistorialClinico_Paciente')
    EXEC sp_rename 'dbo.FK_HistorialClinico_Paciente', 'fk_HistorialClinico_Paciente', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Paciente_UsuarioModificacion')
    EXEC sp_rename 'dbo.FK_Paciente_UsuarioModificacion', 'fk_Paciente_UsuarioModificacion', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Paciente_UsuarioRegistro')
    EXEC sp_rename 'dbo.FK_Paciente_UsuarioRegistro', 'fk_Paciente_UsuarioRegistro', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Pago_Cita')
    EXEC sp_rename 'dbo.FK_Pago_Cita', 'fk_Pago_Cita', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Pago_UsuarioModificacion')
    EXEC sp_rename 'dbo.FK_Pago_UsuarioModificacion', 'fk_Pago_UsuarioModificacion', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Pago_UsuarioRegistro')
    EXEC sp_rename 'dbo.FK_Pago_UsuarioRegistro', 'fk_Pago_UsuarioRegistro', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Servicio_UsuarioModificacion')
    EXEC sp_rename 'dbo.FK_Servicio_UsuarioModificacion', 'fk_Servicio_UsuarioModificacion', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Servicio_UsuarioRegistro')
    EXEC sp_rename 'dbo.FK_Servicio_UsuarioRegistro', 'fk_Servicio_UsuarioRegistro', 'OBJECT';
IF EXISTS (SELECT 1 FROM sys.objects WHERE name COLLATE Latin1_General_BIN = 'FK_Usuario_Doctor')
    EXEC sp_rename 'dbo.FK_Usuario_Doctor', 'fk_Usuario_Doctor', 'OBJECT';

GO

-- =====================================================================
-- PARTE 3: VOLVER A CREAR LAS REGLAS CHECK (con los nombres nuevos)
-- =====================================================================
IF OBJECT_ID('dbo.ck_Cita_Estado') IS NULL
    ALTER TABLE dbo.Cita ADD CONSTRAINT ck_Cita_Estado
        CHECK (estado IN ('Pendiente', 'Confirmada', 'Atendida', 'Cancelada'));

IF OBJECT_ID('dbo.ck_DetallePago_Cantidad') IS NULL
    ALTER TABLE dbo.DetallePago ADD CONSTRAINT ck_DetallePago_Cantidad
        CHECK (cantidad > 0);

IF OBJECT_ID('dbo.ck_DetallePago_Precio') IS NULL
    ALTER TABLE dbo.DetallePago ADD CONSTRAINT ck_DetallePago_Precio
        CHECK (precioUnitario > 0);

IF OBJECT_ID('dbo.ck_Pago_Estado') IS NULL
    ALTER TABLE dbo.Pago ADD CONSTRAINT ck_Pago_Estado
        CHECK (estado IN ('Pendiente', 'Pagado'));

IF OBJECT_ID('dbo.ck_Pago_Metodo') IS NULL
    ALTER TABLE dbo.Pago ADD CONSTRAINT ck_Pago_Metodo
        CHECK (metodoPago IN ('Efectivo', 'QR'));

IF OBJECT_ID('dbo.ck_Pago_Monto') IS NULL
    ALTER TABLE dbo.Pago ADD CONSTRAINT ck_Pago_Monto
        CHECK (monto > 0);

IF OBJECT_ID('dbo.ck_Servicio_Precio') IS NULL
    ALTER TABLE dbo.Servicio ADD CONSTRAINT ck_Servicio_Precio
        CHECK (precio > 0);

IF OBJECT_ID('dbo.ck_Usuario_Rol') IS NULL
    ALTER TABLE dbo.Usuario ADD CONSTRAINT ck_Usuario_Rol
        CHECK (rol IN ('Administrador', 'Doctor'));
GO

-- =====================================================================
-- PARTE 4: PROCEDIMIENTOS CON LOS NOMBRES NUEVOS
-- Las columnas usan la convención nueva (id, idPaciente, primerApellido...).
-- Los resultados se devuelven con los nombres de siempre (AS IdPaciente...)
-- para que la capa de presentación siga funcionando igual.
-- =====================================================================

-- ======================= CITAS =======================

CREATE OR ALTER PROCEDURE dbo.paCitaListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.id AS IdCita, C.idPaciente AS IdPaciente, P.ci AS Ci,
        P.nombres AS NombrePaciente, P.primerApellido AS PrimerApellido, P.segundoApellido AS SegundoApellido,
        C.idDoctor AS IdDoctor, D.nombres AS NombreDoctor, D.apellidos AS ApellidosDoctor,
        E.nombre AS Especialidad,
        C.fechaHora AS FechaHora, C.estado AS Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P     ON C.idPaciente = P.id
    INNER JOIN dbo.Doctor D       ON C.idDoctor = D.id
    INNER JOIN dbo.Especialidad E ON D.idEspecialidad = E.id
    ORDER BY
        CASE WHEN C.fechaHora >= GETDATE() AND C.estado = 'Pendiente'
             THEN 0 ELSE 1 END,
        CASE WHEN C.fechaHora >= GETDATE() AND C.estado = 'Pendiente'
             THEN C.fechaHora END ASC,
        C.fechaHora DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paCitaBuscarPorId
    @IdCita INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.id AS IdCita, C.idPaciente AS IdPaciente, P.ci AS Ci,
        P.nombres AS NombrePaciente, P.primerApellido AS PrimerApellido, P.segundoApellido AS SegundoApellido,
        C.idDoctor AS IdDoctor, D.nombres AS NombreDoctor, D.apellidos AS ApellidosDoctor,
        E.nombre AS Especialidad,
        C.fechaHora AS FechaHora, C.estado AS Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P     ON C.idPaciente = P.id
    INNER JOIN dbo.Doctor D       ON C.idDoctor = D.id
    INNER JOIN dbo.Especialidad E ON D.idEspecialidad = E.id
    WHERE C.id = @IdCita;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paCitaListarPorFecha
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.id AS IdCita, C.idPaciente AS IdPaciente, P.ci AS Ci,
        P.nombres AS NombrePaciente, P.primerApellido AS PrimerApellido, P.segundoApellido AS SegundoApellido,
        C.idDoctor AS IdDoctor, D.nombres AS NombreDoctor, D.apellidos AS ApellidosDoctor,
        E.nombre AS Especialidad,
        C.fechaHora AS FechaHora, C.estado AS Estado
    FROM dbo.Cita C
    INNER JOIN dbo.Paciente P     ON C.idPaciente = P.id
    INNER JOIN dbo.Doctor D       ON C.idDoctor = D.id
    INNER JOIN dbo.Especialidad E ON D.idEspecialidad = E.id
    WHERE CAST(C.fechaHora AS DATE) = @Fecha
    ORDER BY C.fechaHora ASC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paCitaHorariosOcupados
    @IdDoctor INT,
    @Fecha DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CAST(fechaHora AS TIME) AS HoraOcupada
    FROM dbo.Cita
    WHERE idDoctor = @IdDoctor
      AND CAST(fechaHora AS DATE) = @Fecha
      AND estado = 'Pendiente'
    ORDER BY fechaHora ASC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paCitaInsertar
    @IdPaciente INT,
    @IdDoctor INT,
    @FechaHora DATETIME,
    @IdUsuarioRegistro INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.Paciente
                       WHERE id = @IdPaciente AND estado = 1)
        BEGIN
            RAISERROR('El paciente no existe o está inactivo.', 16, 1);
            RETURN;
        END;

        IF NOT EXISTS (SELECT 1 FROM dbo.Doctor
                       WHERE id = @IdDoctor AND estado = 1)
        BEGIN
            RAISERROR('El doctor no existe o está inactivo.', 16, 1);
            RETURN;
        END;

        IF CAST(@FechaHora AS TIME) < '08:00'
           OR CAST(@FechaHora AS TIME) >= '20:00'
           OR DATEPART(MINUTE, @FechaHora) NOT IN (0, 30)
           OR DATEPART(SECOND, @FechaHora) <> 0
        BEGIN
            RAISERROR('El horario seleccionado no es válido.', 16, 1);
            RETURN;
        END;

        IF EXISTS (SELECT 1 FROM dbo.Cita
                   WHERE idDoctor = @IdDoctor
                     AND fechaHora = @FechaHora
                     AND estado = 'Pendiente')
        BEGIN
            RAISERROR('El doctor ya tiene una cita pendiente en ese horario.', 16, 1);
            RETURN;
        END;

        INSERT INTO dbo.Cita (idPaciente, idDoctor, fechaHora, estado, idUsuarioRegistro)
        VALUES (@IdPaciente, @IdDoctor, @FechaHora, 'Pendiente', @IdUsuarioRegistro);

        SELECT SCOPE_IDENTITY() AS IdCita;
    END TRY
    BEGIN CATCH
        DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Error, 16, 1);
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paCitaModificar
    @IdCita INT,
    @IdDoctor INT,
    @FechaHora DATETIME,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF NOT EXISTS (
            SELECT 1 FROM dbo.Cita
            WHERE id = @IdCita AND estado = 'Pendiente'
        )
        BEGIN
            RAISERROR('La cita no existe o no está pendiente.', 16, 1);
            RETURN;
        END;

        IF NOT EXISTS (
            SELECT 1 FROM dbo.Doctor
            WHERE id = @IdDoctor AND estado = 1
        )
        BEGIN
            RAISERROR('El doctor no existe o está inactivo.', 16, 1);
            RETURN;
        END;

        IF CAST(@FechaHora AS TIME) < '08:00'
           OR CAST(@FechaHora AS TIME) >= '20:00'
           OR DATEPART(MINUTE, @FechaHora) NOT IN (0, 30)
           OR DATEPART(SECOND, @FechaHora) <> 0
        BEGIN
            RAISERROR('El horario seleccionado no es válido.', 16, 1);
            RETURN;
        END;

        IF EXISTS (
            SELECT 1 FROM dbo.Cita
            WHERE idDoctor = @IdDoctor
              AND fechaHora = @FechaHora
              AND estado = 'Pendiente'
              AND id <> @IdCita
        )
        BEGIN
            RAISERROR('El doctor ya tiene otra cita en ese horario.', 16, 1);
            RETURN;
        END;

        UPDATE dbo.Cita
        SET
            idDoctor = @IdDoctor,
            fechaHora = @FechaHora,
            idUsuarioModificacion = @IdUsuarioModificacion,
            fechaModificacion = GETDATE()
        WHERE id = @IdCita;
    END TRY
    BEGIN CATCH
        DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Error, 16, 1);
    END CATCH;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paCitaCancelar
    @IdCita INT,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.Cita
        WHERE id = @IdCita AND estado = 'Pendiente'
    )
    BEGIN
        RAISERROR('La cita no existe o no está pendiente.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Cita
    SET
        estado = 'Cancelada',
        idUsuarioModificacion = @IdUsuarioModificacion,
        fechaModificacion = GETDATE()
    WHERE id = @IdCita
      AND estado = 'Pendiente';
END;
GO

-- ======================= DOCTORES =======================

CREATE OR ALTER PROCEDURE dbo.paDoctorBuscarPorCi
    @Ci VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        D.id AS IdDoctor,
        D.ci AS Ci,
        D.nombres AS Nombres,
        D.apellidos AS Apellidos,
        D.celular AS Celular,
        E.nombre AS Especialidad,
        D.direccion AS Direccion
    FROM dbo.Doctor D
    INNER JOIN dbo.Especialidad E ON D.idEspecialidad = E.id
    WHERE D.ci = @Ci
      AND D.estado = 1;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paDoctorInsertar
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

    IF EXISTS (SELECT 1 FROM dbo.Doctor WHERE ci = @Ci)
    BEGIN
        RAISERROR('Ya existe un doctor con ese CI.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Especialidad
                   WHERE id = @IdEspecialidad AND estado = 1)
    BEGIN
        RAISERROR('La especialidad no existe o está inactiva.', 16, 1);
        RETURN;
    END

    INSERT INTO dbo.Doctor
        (ci, nombres, apellidos, celular, idEspecialidad, direccion,
         estado, idUsuarioRegistro, fechaRegistro)
    VALUES
        (@Ci, @Nombres, @Apellidos, @Celular, @IdEspecialidad, @Direccion,
         1, @IdUsuarioRegistro, GETDATE());
END;
GO

CREATE OR ALTER PROCEDURE dbo.paDoctorModificar
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
               WHERE ci = @Ci AND id <> @IdDoctor)
    BEGIN
        RAISERROR('Ya existe otro doctor con ese CI.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.Especialidad
                   WHERE id = @IdEspecialidad AND estado = 1)
    BEGIN
        RAISERROR('La especialidad no existe o está inactiva.', 16, 1);
        RETURN;
    END

    UPDATE dbo.Doctor
    SET ci = @Ci,
        nombres = @Nombres,
        apellidos = @Apellidos,
        celular = @Celular,
        idEspecialidad = @IdEspecialidad,
        direccion = @Direccion,
        idUsuarioModificacion = @IdUsuarioModificacion,
        fechaModificacion = GETDATE()
    WHERE id = @IdDoctor
      AND estado = 1;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('No se encontró un doctor activo para modificar.', 16, 1);
        RETURN;
    END
END;
GO

CREATE OR ALTER PROCEDURE dbo.paDoctorEliminar
    @IdDoctor INT,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Doctor
    SET
        estado = 0,
        idUsuarioModificacion = @IdUsuarioModificacion,
        fechaModificacion = GETDATE()
    WHERE id = @IdDoctor
      AND estado = 1;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('No se encontró un doctor activo para desactivar.', 16, 1);
        RETURN;
    END
END;
GO

-- ======================= ESPECIALIDADES =======================

CREATE OR ALTER PROCEDURE dbo.paEspecialidadListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id AS IdEspecialidad,
        nombre AS Nombre,
        descripcion AS Descripcion,
        estado AS Estado,
        idUsuarioRegistro AS IdUsuarioRegistro,
        fechaRegistro AS FechaRegistro,
        idUsuarioModificacion AS IdUsuarioModificacion,
        fechaModificacion AS FechaModificacion
    FROM dbo.Especialidad
    ORDER BY id DESC;
END;
GO

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

    IF EXISTS (SELECT 1 FROM dbo.Especialidad WHERE nombre = @Nombre)
    BEGIN
        RAISERROR('Ya existe una especialidad con ese nombre.', 16, 1);
        RETURN;
    END;

    INSERT INTO dbo.Especialidad (nombre, descripcion, estado, idUsuarioRegistro, fechaRegistro)
    VALUES (@Nombre, @Descripcion, 1, @IdUsuarioRegistro, GETDATE());
END;
GO

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

    IF NOT EXISTS (SELECT 1 FROM dbo.Especialidad WHERE id = @IdEspecialidad)
    BEGIN
        RAISERROR('La especialidad no existe.', 16, 1);
        RETURN;
    END;

    IF EXISTS (SELECT 1 FROM dbo.Especialidad
               WHERE nombre = @Nombre AND id <> @IdEspecialidad)
    BEGIN
        RAISERROR('Ya existe otra especialidad con ese nombre.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Especialidad
    SET
        nombre = @Nombre,
        descripcion = @Descripcion,
        idUsuarioModificacion = @IdUsuarioModificacion,
        fechaModificacion = GETDATE()
    WHERE id = @IdEspecialidad;
END;
GO

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

    IF NOT EXISTS (SELECT 1 FROM dbo.Especialidad
                   WHERE id = @IdEspecialidad AND estado = 1)
    BEGIN
        RAISERROR('La especialidad no existe o ya está inactiva.', 16, 1);
        RETURN;
    END;

    IF EXISTS (SELECT 1 FROM dbo.Doctor
               WHERE idEspecialidad = @IdEspecialidad AND estado = 1)
    BEGIN
        RAISERROR('No se puede desactivar: existen doctores activos con esta especialidad.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Especialidad
    SET
        estado = 0,
        idUsuarioModificacion = @IdUsuarioModificacion,
        fechaModificacion = GETDATE()
    WHERE id = @IdEspecialidad;
END;
GO

-- ======================= PACIENTES =======================

CREATE OR ALTER PROCEDURE dbo.paPacienteListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id AS IdPaciente, ci AS Ci, nombres AS Nombres, primerApellido AS PrimerApellido,
        segundoApellido AS SegundoApellido, fechaNacimiento AS FechaNacimiento, sexo AS Sexo,
        celular AS Celular, direccion AS Direccion, fechaRegistro AS FechaRegistro, estado AS Estado,
        idUsuarioRegistro AS IdUsuarioRegistro, idUsuarioModificacion AS IdUsuarioModificacion,
        fechaModificacion AS FechaModificacion
    FROM dbo.Paciente
    WHERE estado = 1
    ORDER BY primerApellido, nombres;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paPacienteBuscarPorCi
    @Ci VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id AS IdPaciente, ci AS Ci, nombres AS Nombres, primerApellido AS PrimerApellido,
        segundoApellido AS SegundoApellido, fechaNacimiento AS FechaNacimiento, sexo AS Sexo,
        celular AS Celular, direccion AS Direccion, fechaRegistro AS FechaRegistro, estado AS Estado,
        idUsuarioRegistro AS IdUsuarioRegistro, idUsuarioModificacion AS IdUsuarioModificacion,
        fechaModificacion AS FechaModificacion
    FROM dbo.Paciente
    WHERE ci = @Ci AND estado = 1;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paPacienteInsertar
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

    IF EXISTS (SELECT 1 FROM dbo.Paciente WHERE ci = @Ci)
    BEGIN
        RAISERROR('Ya existe un paciente registrado con ese CI.', 16, 1);
        RETURN;
    END;

    INSERT INTO dbo.Paciente (
        ci, nombres, primerApellido, segundoApellido,
        fechaNacimiento, sexo, celular, direccion,
        idUsuarioRegistro, estado
    )
    VALUES (
        @Ci, @Nombres, @PrimerApellido, @SegundoApellido,
        @FechaNacimiento, @Sexo, @Celular, @Direccion,
        @IdUsuarioRegistro, 1
    );
END;
GO

CREATE OR ALTER PROCEDURE dbo.paPacienteModificar
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
        FROM dbo.Paciente
        WHERE ci = @Ci
          AND id <> @IdPaciente
          AND estado = 1
    )
    BEGIN
        RAISERROR('Ya existe otro paciente registrado con ese CI.', 16, 1);
        RETURN;
    END;

    UPDATE dbo.Paciente
    SET ci = @Ci,
        nombres = @Nombres,
        primerApellido = @PrimerApellido,
        segundoApellido = @SegundoApellido,
        fechaNacimiento = @FechaNacimiento,
        sexo = @Sexo,
        celular = @Celular,
        direccion = @Direccion,
        idUsuarioModificacion = @IdUsuarioModificacion,
        fechaModificacion = GETDATE()
    WHERE id = @IdPaciente
      AND estado = 1;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paPacienteEliminar
    @IdPaciente INT,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Paciente
    SET estado = 0,
        idUsuarioModificacion = @IdUsuarioModificacion,
        fechaModificacion = GETDATE()
    WHERE id = @IdPaciente
      AND estado = 1;
END;
GO

-- ======================= HISTORIAL CLÍNICO =======================

CREATE OR ALTER PROCEDURE dbo.paHistorialListarPorPaciente
    @IdPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        H.id AS IdHistorial,
        H.idPaciente AS IdPaciente,
        H.idCita AS IdCita,
        H.fechaAtencion AS FechaAtencion,
        H.diagnostico AS Diagnostico,
        H.tratamiento AS Tratamiento,
        H.observaciones AS Observaciones,
        COALESCE(
            DC.nombres + ' ' + DC.apellidos,
            DU.nombres + ' ' + DU.apellidos,
            'No registrado'
        ) AS Doctor,
        U.nombreUsuario AS RegistradoPor
    FROM dbo.HistorialClinico H
    LEFT JOIN dbo.Cita C    ON H.idCita = C.id
    LEFT JOIN dbo.Doctor DC ON C.idDoctor = DC.id
    LEFT JOIN dbo.Usuario U ON H.idUsuarioRegistro = U.id
    LEFT JOIN dbo.Doctor DU ON U.idDoctor = DU.id
    WHERE H.idPaciente = @IdPaciente
    ORDER BY H.fechaAtencion DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paHistorialCitasDisponibles
    @IdPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.id AS IdCita,
        C.fechaHora AS FechaHora,
        CONVERT(VARCHAR(16), C.fechaHora, 120)
            + ' - Dr(a). ' + D.nombres + ' ' + D.apellidos
            AS Descripcion
    FROM dbo.Cita C
    INNER JOIN dbo.Doctor D ON C.idDoctor = D.id
    WHERE C.idPaciente = @IdPaciente
      AND C.estado IN ('Pendiente', 'Confirmada')
      AND NOT EXISTS (
            SELECT 1
            FROM dbo.HistorialClinico H
            WHERE H.idCita = C.id
      )
    ORDER BY C.fechaHora DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paHistorialInsertar
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
        IF NOT EXISTS (
            SELECT 1 FROM dbo.Paciente
            WHERE id = @IdPaciente AND estado = 1
        )
        BEGIN
            RAISERROR('El paciente no existe o está inactivo.', 16, 1);
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

        IF @IdCita IS NOT NULL
        BEGIN
            -- La cita debe ser del mismo paciente y no estar cancelada ni atendida
            IF NOT EXISTS (
                SELECT 1 FROM dbo.Cita
                WHERE id = @IdCita
                  AND idPaciente = @IdPaciente
                  AND estado IN ('Pendiente', 'Confirmada')
            )
            BEGIN
                RAISERROR('La cita no pertenece al paciente o ya no está disponible.', 16, 1);
                RETURN;
            END;

            -- Una cita solo puede tener un registro de historial
            IF EXISTS (SELECT 1 FROM dbo.HistorialClinico WHERE idCita = @IdCita)
            BEGIN
                RAISERROR('Esta cita ya tiene una atención registrada.', 16, 1);
                RETURN;
            END;
        END;

        BEGIN TRANSACTION;

        INSERT INTO dbo.HistorialClinico (
            idPaciente, idCita, fechaAtencion,
            diagnostico, tratamiento, observaciones,
            idUsuarioRegistro
        )
        VALUES (
            @IdPaciente, @IdCita, GETDATE(),
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
            SET estado = 'Atendida',
                idUsuarioModificacion = @IdUsuarioRegistro,
                fechaModificacion = GETDATE()
            WHERE id = @IdCita;
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

CREATE OR ALTER PROCEDURE dbo.paHistorialModificar
    @IdHistorial INT,
    @Diagnostico VARCHAR(500),
    @Tratamiento VARCHAR(500),
    @Observaciones VARCHAR(1000) = NULL,
    @IdUsuarioModificacion INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.HistorialClinico WHERE id = @IdHistorial)
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
        SET diagnostico = LTRIM(RTRIM(@Diagnostico)),
            tratamiento = LTRIM(RTRIM(@Tratamiento)),
            observaciones = NULLIF(LTRIM(RTRIM(@Observaciones)), ''),
            idUsuarioModificacion = @IdUsuarioModificacion,
            fechaModificacion = GETDATE()
        WHERE id = @IdHistorial;
    END TRY
    BEGIN CATCH
        DECLARE @Error NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@Error, 16, 1);
    END CATCH;
END;
GO

-- ======================= COBRO (VENTA) =======================

CREATE OR ALTER PROCEDURE dbo.paServicioListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id AS IdServicio, nombre AS Nombre, descripcion AS Descripcion, precio AS Precio
    FROM dbo.Servicio
    WHERE estado = 1
    ORDER BY nombre;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paPagoCitasPorCobrar
    @IdPaciente INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        C.id AS IdCita,
        C.fechaHora AS FechaHora,
        CONVERT(VARCHAR(16), C.fechaHora, 120)
            + ' - Dr(a). ' + D.nombres + ' ' + D.apellidos AS Descripcion
    FROM dbo.Cita C
    INNER JOIN dbo.Doctor D ON C.idDoctor = D.id
    WHERE C.idPaciente = @IdPaciente
      AND C.estado = 'Atendida'
      AND NOT EXISTS (
            SELECT 1 FROM dbo.Pago P
            WHERE P.idCita = C.id AND P.estado = 'Pagado'
      )
    ORDER BY C.fechaHora DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paPagoListar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        P.id AS IdPago,
        P.fechaPago AS FechaPago,
        Pa.ci AS Ci,
        Pa.nombres + ' ' + Pa.primerApellido AS Paciente,
        C.fechaHora AS FechaCita,
        P.concepto AS Concepto,
        P.metodoPago AS MetodoPago,
        P.monto AS Monto,
        P.estado AS Estado
    FROM dbo.Pago P
    INNER JOIN dbo.Cita C      ON P.idCita = C.id
    INNER JOIN dbo.Paciente Pa ON C.idPaciente = Pa.id
    ORDER BY P.fechaPago DESC;
END;
GO

CREATE OR ALTER PROCEDURE dbo.paDetallePagoListar
    @IdPago INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        DP.id AS IdDetallePago,
        S.nombre AS Servicio,
        DP.cantidad AS Cantidad,
        DP.precioUnitario AS PrecioUnitario,
        DP.subtotal AS Subtotal
    FROM dbo.DetallePago DP
    INNER JOIN dbo.Servicio S ON DP.idServicio = S.id
    WHERE DP.idPago = @IdPago;
END;
GO

-- =====================================================================
-- COMPROBAR
-- =====================================================================
-- 1. Columnas con los nombres nuevos
SELECT TABLE_NAME AS Tabla, COLUMN_NAME AS Columna, DATA_TYPE AS Tipo
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
ORDER BY TABLE_NAME, ORDINAL_POSITION;

-- 2. Reglas CHECK y llaves foráneas
SELECT name AS Restriccion, type_desc AS Tipo
FROM sys.objects
WHERE type IN ('C', 'F')
ORDER BY type, name;

-- 3. Los procedimientos funcionan
EXEC dbo.paPacienteListar;
EXEC dbo.paCitaListar;
EXEC dbo.paPagoListar;
GO
