
using System;
using System.Collections.Generic;

using System.Linq;

using System.Windows.Forms;
using ClnCarsal;

namespace CpCarsal
{
    public partial class FrmCita : Form
    {
        private int idPacienteSeleccionado = 0;
        private int idCitaSeleccionada = 0;

        private int idUsuarioActual = 1;

        private bool pacienteEncontrado = false;
        private bool modoEdicion = false;

        public FrmCita()
        {
            InitializeComponent();

            ConfigurarEstadoInicial();
        }

        private void FrmCita_Load(object sender, EventArgs e)
        {

            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.MinDate = DateTime.Today;

            cboEspecialidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHora.DropDownStyle = ComboBoxStyle.DropDownList;

            txtNombrePaciente.ReadOnly = true;

            CargarEspecialidades();
            CargarListaCitas();

            ConfigurarEstadoInicial();
        }

        // ESTADOS DEL FORMULARIO

        private void ConfigurarEstadoInicial()
        {
            LimpiarCampos();

            pnlRegistro.Enabled = false;

            txtBuscarCi.Enabled = true;
            btnBuscar.Enabled = true;

            btnNuevaCita.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            pacienteEncontrado = false;
            modoEdicion = false;

            idPacienteSeleccionado = 0;
            idCitaSeleccionada = 0;

            btnGuardar.Text = "REGISTRAR CITA";
        }

        private void HabilitarRegistro()
        {
            pnlRegistro.Enabled = true;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;

            btnNuevaCita.Enabled = false;

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void DeshabilitarRegistro()
        {
            pnlRegistro.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void LimpiarCampos()
        {
            txtBuscarCi.Clear();
            txtNombrePaciente.Clear();

            cboEspecialidad.SelectedIndex = -1;
            cboDoctor.DataSource = null;
            cboHora.Items.Clear();

            dtpFecha.Value = DateTime.Today;
        }

        private void ActualizarLista()
        {
            dgvLista.DataSource = null;
            dgvLista.DataSource = CitaCln.Listar();

            dgvLista.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvLista.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLista.MultiSelect = false;
            dgvLista.ReadOnly = true;
            dgvLista.AllowUserToAddRows = false;

            dgvLista.ClearSelection();

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            idCitaSeleccionada = 0;
        }

        // CARGAR ESPECIALIDADES

        private void CargarEspecialidades()
        {
            var doctores = DoctorCln.Listar()
                as System.Collections.IEnumerable;

            var especialidades = new List<string>();

            if (doctores != null)
            {
                foreach (var doctor in doctores)
                {
                    string especialidad = (string)doctor
                        .GetType()
                        .GetProperty("Especialidad")
                        .GetValue(doctor, null);

                    if (!string.IsNullOrWhiteSpace(especialidad) &&
                        !especialidades.Contains(especialidad))
                    {
                        especialidades.Add(especialidad);
                    }
                }
            }

            especialidades.Sort();

            cboEspecialidad.DataSource = especialidades;
            cboEspecialidad.SelectedIndex = -1;
        }
        // CARGAR DOCTORES

        private void CargarDoctores()
        {
            cboDoctor.DataSource = null;
            cboDoctor.Items.Clear();

            if (cboEspecialidad.SelectedIndex == -1)
                return;

            string especialidad =
                cboEspecialidad.SelectedItem.ToString();

            var doctores = DoctorCln.Listar()
                as System.Collections.IEnumerable;

            var lista = new List<object>();

            if (doctores != null)
            {
                foreach (var doctor in doctores)
                {
                    string esp = (string)doctor.GetType()
                        .GetProperty("Especialidad")
                        .GetValue(doctor, null);

                    if (esp == especialidad)
                    {
                        int id = Convert.ToInt32(
                            doctor.GetType()
                            .GetProperty("IdDoctor")
                            .GetValue(doctor, null));

                        string nombres = (string)doctor.GetType()
                            .GetProperty("Nombres")
                            .GetValue(doctor, null);

                        string apellidos = (string)doctor.GetType()
                            .GetProperty("Apellidos")
                            .GetValue(doctor, null);

                        lista.Add(new
                        {
                            IdDoctor = id,
                            NombreCompleto = nombres + " " + apellidos
                        });
                    }
                }
            }

            cboDoctor.DisplayMember = "NombreCompleto";
            cboDoctor.ValueMember = "IdDoctor";
            cboDoctor.DataSource = lista;
            cboDoctor.SelectedIndex = -1;
        }

        // CARGAR HORARIOS DISPONIBLES
        private void CargarHorariosDisponibles()
        {
            cboHora.Items.Clear();

            if (cboDoctor.SelectedIndex == -1)
                return;

            int idDoctor =
                Convert.ToInt32(cboDoctor.SelectedValue);

            DateTime fecha = dtpFecha.Value.Date;

            var resultado = CitaCln.HorariosOcupados(
                idDoctor, fecha)
                as System.Collections.IEnumerable;

            var horariosOcupados = new List<TimeSpan>();

            if (resultado != null)
            {
                foreach (var registro in resultado)
                {
                    var propiedad = registro.GetType()
                        .GetProperty("HoraOcupada");

                    if (propiedad != null)
                    {
                        object valor =
                            propiedad.GetValue(registro, null);

                        if (valor is TimeSpan)
                            horariosOcupados.Add((TimeSpan)valor);
                    }
                }
            }

            for (int hora = 8; hora < 20; hora++)
            {
                for (int minuto = 0; minuto < 60; minuto += 30)
                {
                    TimeSpan turno =
                        new TimeSpan(hora, minuto, 0);

                    if (!horariosOcupados.Contains(turno))
                    {
                        DateTime fechaHora = fecha.Add(turno);

                        if (fechaHora > DateTime.Now)
                        {
                            cboHora.Items.Add(
                                fechaHora.ToString("HH:mm"));
                        }
                    }
                }
            }

            cboHora.SelectedIndex = -1;
        }

        // EVENTOS DE LOS COMBOS

        private void cboEspecialidad_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (!pnlRegistro.Enabled)
                return;

            CargarDoctores();
        }

        private void cboDoctor_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (!pnlRegistro.Enabled)
                return;

            if (cboDoctor.SelectedIndex != -1)
                CargarHorariosDisponibles();
        }

        private void dtpFecha_ValueChanged(
            object sender, EventArgs e)
        {
            if (!pnlRegistro.Enabled)
                return;


            if (cboDoctor.SelectedIndex != -1)

                CargarHorariosDisponibles();

        }


        // BUSCAR PACIENTE

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string ci = txtBuscarCi.Text.Trim();

                pacienteEncontrado = false;
                idPacienteSeleccionado = 0;

                txtNombrePaciente.Clear();
                btnNuevaCita.Enabled = false;

                if (string.IsNullOrWhiteSpace(ci))
                {
                    MessageBox.Show("Ingrese el CI del paciente.");
                    txtBuscarCi.Focus();
                    return;
                }

                var resultado = PacienteCln.BuscarPorCi(ci)
                    as System.Collections.IEnumerable;

                if (resultado == null ||
                    !resultado.Cast<object>().Any())
                {
                    MessageBox.Show(
                        "No se encontró un paciente activo con ese CI.");

                    return;
                }

                dynamic paciente =
                    resultado.Cast<object>().First();

                idPacienteSeleccionado =
                    Convert.ToInt32(paciente.IdPaciente);

                txtNombrePaciente.Text =
                    paciente.Nombres + " " +
                    paciente.PrimerApellido + " " +
                    paciente.SegundoApellido;

                pacienteEncontrado = true;
                btnNuevaCita.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar paciente: " + ex.Message);
            }
        }

        // NUEVA CITA

        private void btnNuevaCita_Click(object sender, EventArgs e)
        {
            if (!pacienteEncontrado ||
                idPacienteSeleccionado == 0)
            {
                MessageBox.Show("Primero busque un paciente.");
                return;
            }

            modoEdicion = false;
            idCitaSeleccionada = 0;

            btnGuardar.Text = "REGISTRAR CITA";
            cboEspecialidad.SelectedIndex = -1;
            cboDoctor.DataSource = null;
            cboHora.Items.Clear();

            HabilitarRegistro();

            btnNuevaCita.Enabled = false;
        }

        // REGISTRAR O MODIFICAR CITA

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idPacienteSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un paciente.");
                    return;
                }

                if (cboEspecialidad.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una especialidad.");
                    return;
                }

                if (cboDoctor.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un doctor.");
                    return;
                }

                if (cboHora.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un horario.");
                    return;
                }

                int idDoctor =
                    Convert.ToInt32(cboDoctor.SelectedValue);

                DateTime fechaHora = dtpFecha.Value.Date.Add(
                    TimeSpan.Parse(cboHora.SelectedItem.ToString()));

                if (!modoEdicion)
                {

                    CitaCln.Insertar(
                        idPacienteSeleccionado,
                        idDoctor,
                        fechaHora,
                        idUsuarioActual);

                    MessageBox.Show(
                        "Cita registrada correctamente.");
                }

                else

                {

                    CitaCln.Modificar(
                        idCitaSeleccionada,
                        idDoctor,
                        fechaHora,
                        idUsuarioActual);

                    MessageBox.Show(
                        "Cita modificada correctamente.");
                }

                ActualizarLista();

                // Volver al estado inicial
                ConfigurarEstadoInicial();

                txtBuscarCi.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar la cita: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // CANCELAR REGISTRO O MODIFICACION

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ActualizarLista();

            ConfigurarEstadoInicial();

            txtBuscarCi.Focus();
        }

        // SELECCIONAR CITA DE LA LISTA

        private void dgvLista_CellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || modoEdicion)
                return;

            DataGridViewRow fila =
                dgvLista.Rows[e.RowIndex];

            idCitaSeleccionada = Convert.ToInt32(
                fila.Cells["IdCita"].Value);

            string estado =
                fila.Cells["Estado"].Value.ToString();

            btnModificar.Enabled = estado == "Pendiente";
            btnEliminar.Enabled = estado == "Pendiente";
        }

        // MODIFICAR CITA

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idCitaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una cita.");
                return;
            }

            try
            {
                DataGridViewRow fila = dgvLista.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r =>
                        Convert.ToInt32(
                            r.Cells["IdCita"].Value) ==
                        idCitaSeleccionada);

                if (fila == null)
                {

                    MessageBox.Show(
                        "No se encontró la cita seleccionada.");
                    return;
                }

                idPacienteSeleccionado = Convert.ToInt32(

                    fila.Cells["IdPaciente"].Value);

                txtNombrePaciente.Text =

                    Convert.ToString(fila.Cells["NombrePaciente"].Value) +
                    " " +
                    Convert.ToString(fila.Cells["PrimerApellido"].Value) +
                    " " +
                    Convert.ToString(fila.Cells["SegundoApellido"].Value);

                string especialidad =

                    Convert.ToString(

                        fila.Cells["Especialidad"].Value);

                int idDoctor = Convert.ToInt32(
                    fila.Cells["IdDoctor"].Value);

                DateTime fechaHora = Convert.ToDateTime(
                    fila.Cells["FechaHora"].Value);

                modoEdicion = true;

                HabilitarRegistro();

                // Bloquear búsqueda durante la modificación
                txtBuscarCi.Enabled = false;
                btnBuscar.Enabled = false;
                btnNuevaCita.Enabled = false;

                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;

                // Recuperar especialidad y doctor

                cboEspecialidad.SelectedItem = especialidad;

                        CargarDoctores();

                cboDoctor.SelectedValue = idDoctor;

                // Recuperar fecha
                dtpFecha.Value = fechaHora.Date;

                CargarHorariosDisponibles();

                // Conservar el horario actual
                string horaActual =
                    fechaHora.ToString("HH:mm");

                if (!cboHora.Items.Contains(horaActual))
                    cboHora.Items.Add(horaActual);

                cboHora.SelectedItem = horaActual;

                btnGuardar.Text = "GUARDAR CAMBIOS";

            }

            catch (Exception ex)

            {
                MessageBox.Show(
                    "Error al recuperar los datos: " + ex.Message);
            }
        }

        // ELIMINAR / CANCELAR CITA

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCitaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una cita.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de anular esta cita?",
                "Confirmar anulación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                CitaCln.Cancelar(
                    idCitaSeleccionada,
                    idUsuarioActual);

                MessageBox.Show(
                    "La cita fue anulada correctamente.");

                ActualizarLista();

                ConfigurarEstadoInicial();

                txtBuscarCi.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al anular la cita: " + ex.Message);
            }
        }
       
        // CARGAR LISTA AL INGRESAR
       
        private void CargarListaCitas()
        {
            ActualizarLista();
        }
    }
}
