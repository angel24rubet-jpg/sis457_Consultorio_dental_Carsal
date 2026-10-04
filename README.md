
# SISTEMA DE ESCRITORIO PARA EL CONSULTORIO DENTAL “CARSAL”

DESCRIPCIÓN DEL PROYECTO DE DESARROLLO DEL SISTEMA

El presente proyecto consiste en el desarrollo de un sistema de escritorio para la gestión del consultorio dental “CARSAL”, diseñado para facilitar y organizar las principales actividades que se realizan dentro del consultorio.
El sistema permitirá al administrador registrar y gestionar la información de los doctores, pacientes, citas e historias clínicas, además de consultar y actualizar la información necesaria para la atención de los pacientes.
El objetivo principal es desarrollar una aplicación de escritorio funcional que permita llevar un mejor control de la información del consultorio, evitando el manejo desorganizado de los datos y facilitando el acceso a la información de pacientes y sus atenciones.

OBJETIVO GENERAL
Desarrollar un sistema de escritorio para la gestión de un consultorio dental que permita administrar la información de doctores, pacientes, citas e historias clínicas de manera organizada.

OBJETIVOS ESPECÍFICOS
•	Implementar un sistema para el registro y gestión de doctores.
•	Permitir el registro y actualización de los datos de los pacientes.
•	Registrar y administrar las citas del consultorio.
•	Permitir consultar las citas programadas.
•	Registrar las atenciones realizadas a los pacientes.
•	Mantener un historial clínico de cada paciente.
•	Permitir registrar diagnósticos y tratamientos realizados.
•	Facilitar la búsqueda y consulta de información.
•	Implementar una base de datos para almacenar y relacionar la información del sistema.
•	Mejorar la organización y control de la información del consultorio.

TIPOS DE USUARIOS
El sistema contará inicialmente con un usuario principal:

ADMINISTRADOR
El administrador tendrá acceso a las funciones principales del sistema:
•	Iniciar sesión.
•	Registrar nuevos doctores.
•	Modificar información de los doctores.
•	Eliminar o desactivar doctores.
•	Registrar nuevos pacientes.
•	Modificar información de los pacientes.
•	Registrar nuevas citas.
•	Modificar o cancelar citas.
•	Consultar las citas programadas.
•	Registrar información de las consultas.
•	Registrar diagnósticos.
•	Registrar tratamientos realizados.
•	Consultar el historial clínico de los pacientes.

TECNOLOGÍAS USADAS
Para el desarrollo del sistema de escritorio se utilizarán las herramientas y tecnologías establecidas para el proyecto de la materia.

Lenguaje de programación
•	C#
Se utilizará C# para desarrollar la lógica y funcionamiento principal del sistema.

Aplicación de escritorio
•	Visual Studio
Se utilizará Visual Studio para desarrollar, ejecutar y probar el sistema.

Base de datos
•	SQL Server
La base de datos almacenará la información de doctores, pacientes, citas e historias clínicas.

TABLAS TENTATIVAS DE LA BASE DE DATOS
A continuación, se presentan algunas de las tablas que se consideran necesarias para el funcionamiento del sistema.
Estas tablas son tentativas y podrán modificarse, agregarse o complementarse durante el desarrollo del proyecto según las necesidades del sistema.

TABLA DOCTOR
id
Nombre
Apellidos
Teléfono
Direccion 
Espeialidad 

TABLA PACIENTE
id
Nombre
Apellidos
Fecha_nacimiento
Genero
Teléfono
Direccion

TABLA CITA
Id
Id_paciente
Id_doctor
especialidad
fecha
hora

TABLA HISTORIAL_CLINICO 
Id
Id_paciente
Id_doctor
Fecha 
Motivo_consulta
Diagnostico
Tratamiento

TABLA USUARIO
Id
Nombre_usuario
password
Rol
Estado

TABLA PAGO
Id
Id_paciente
Id_cita
Fecha_pago
Monto 
Concepto




