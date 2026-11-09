using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClnCarsal;

namespace CpCarsal
{
    public partial class FrmCita : Form
    {
        public FrmCita()

        {
            InitializeComponent();
        }
        private int idPacienteSeleccionado = 0;
        private int idCitaSeleccionada = 0;
        private int idUsuarioActual = 1;
        private void FrmCita_Load(object sender, EventArgs e)
        {
            CargarEspecialidades();
            CargarListaCitas();

            // Configurar fecha
            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.MinDate = DateTime.Today;


            // Configurar combos
            cboEspecialidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHora.DropDownStyle = ComboBoxStyle.DropDownList;

            // Bloquear campos hasta pulsar Nueva Cita
            cboEspecialidad.Enabled = false;
            cboDoctor.Enabled = false;
            dtpFecha.Enabled = false;
            cboHora.Enabled = false;

            txtNombrePaciente.ReadOnly = true;
        }

        // Cargar especialidades de doctores activos
        // Cargar especialidades de doctores activos
        private void CargarEspecialidades()
        {
            var doctores = DoctorCln.Listar() as System.Collections.IEnumerable;

            var especialidades = new List<string>();

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

            especialidades.Sort();

            cboEspecialidad.DataSource = especialidades;
            cboEspecialidad.SelectedIndex = -1;
        }
        // Cargar lista general de citas
        private void CargarListaCitas()
        {
            dgvLista.DataSource = CitaCln.Listar();

            dgvLista.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvLista.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLista.MultiSelect = false;

            dgvLista.ReadOnly = true;
            dgvLista.AllowUserToAddRows = false;
        }
        // Cargar doctores según la especialidad seleccionada

        private void CargarDoctores()
        {
            cboDoctor.DataSource = null;
            cboDoctor.Items.Clear();

            if (cboEspecialidad.SelectedIndex == -1)
                return;

            string especialidad = cboEspecialidad.SelectedItem.ToString();

            var doctores = DoctorCln.Listar()
                as System.Collections.IEnumerable;

            var lista = new List<object>();

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
                        .GetValue(doctor, null)
                    );

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

            cboDoctor.DisplayMember = "NombreCompleto";
            cboDoctor.ValueMember = "IdDoctor";
            cboDoctor.DataSource = lista;
            cboDoctor.SelectedIndex = -1;
        }


        // Cargar horarios disponibles según doctor y fecha
        private void CargarHorariosDisponibles()
        {
            cboHora.DataSource = null;
            cboHora.Items.Clear();

            if (cboDoctor.SelectedIndex == -1)
                return;

            int idDoctor = Convert.ToInt32(cboDoctor.SelectedValue);
            DateTime fecha = dtpFecha.Value.Date;

            // Consultar horarios ocupados
            var resultado = CitaCln.HorariosOcupados(idDoctor, fecha)
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
                        object valor = propiedad.GetValue(registro, null);

                        if (valor is TimeSpan)
                            horariosOcupados.Add((TimeSpan)valor);
                    }
                }
            }

            // Generar turnos desde las 08:00 hasta las 19:30
            for (int hora = 8; hora < 20; hora++)
            {
                for (int minuto = 0; minuto < 60; minuto += 30)
                {
                    TimeSpan turno = new TimeSpan(hora, minuto, 0);

                    if (!horariosOcupados.Contains(turno))
                    {
                        // No ofrecer horarios que ya pasaron
                        DateTime fechaHora = fecha.Add(turno);

                        if (fechaHora > DateTime.Now)
                            cboHora.Items.Add(
                                fechaHora.ToString("HH:mm"));
                    }
                }
            }

            cboHora.SelectedIndex = -1;
        }


        private void cboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDoctores();
        }

        private void cboDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDoctor.SelectedIndex != -1)
            {
                CargarHorariosDisponibles();
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (cboDoctor.SelectedIndex != -1)
            {
                CargarHorariosDisponibles();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string ci = txtBuscarCi.Text.Trim();

            if (string.IsNullOrWhiteSpace(ci))
            {
                MessageBox.Show("Ingrese el CI del paciente.");
                txtBuscarCi.Focus();
                return;
            }

            var resultado = PacienteCln.BuscarPorCi(ci) as System.Collections.IEnumerable;

            if (resultado == null || !resultado.Cast<object>().Any())
            {
                MessageBox.Show("No se encontró un paciente activo con ese CI.");
                idPacienteSeleccionado = 0;
                txtNombrePaciente.Clear();
                return;
            }

            dynamic paciente = resultado.Cast<object>().First();

            idPacienteSeleccionado = paciente.IdPaciente;

            txtNombrePaciente.Text =
                paciente.Nombres + " " +
                paciente.PrimerApellido + " " +
                paciente.SegundoApellido;
        }

        private void btnNuevaCita_Click(object sender, EventArgs e)
        {
            if (idPacienteSeleccionado == 0)
            {
                MessageBox.Show("Primero debe buscar y seleccionar un paciente.");
                txtBuscarCi.Focus();
                return;
            }

            cboEspecialidad.Enabled = true;
            cboDoctor.Enabled = true;
            dtpFecha.Enabled = true;
            cboHora.Enabled = true;

            cboEspecialidad.SelectedIndex = -1;
            cboDoctor.DataSource = null;
            cboHora.Items.Clear();

            idCitaSeleccionada = 0;

            btnGuardar.Enabled = true;
            btnModificar.Enabled = false;

            MessageBox.Show("Ahora puede seleccionar los datos de la cita.");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            idPacienteSeleccionado = 0;
            idCitaSeleccionada = 0;

            txtBuscarCi.Clear();
            txtNombrePaciente.Clear();

            cboEspecialidad.SelectedIndex = -1;
            cboDoctor.DataSource = null;
            cboHora.Items.Clear();

            cboEspecialidad.Enabled = false;
            cboDoctor.Enabled = false;
            dtpFecha.Enabled = false;
            cboHora.Enabled = false;

            btnGuardar.Enabled = false;
            btnModificar.Enabled = false;

            MessageBox.Show("Registro cancelado. Se limpiaron los campos.");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (idPacienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un paciente.");
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

            try
            {
                int idDoctor = Convert.ToInt32(cboDoctor.SelectedValue);

                DateTime fechaHora = dtpFecha.Value.Date.Add(
                    TimeSpan.Parse(cboHora.SelectedItem.ToString())
                );

                if (idCitaSeleccionada == 0)
                {
                    // Registrar nueva cita
                    CitaCln.Insertar(
                        idPacienteSeleccionado,
                        idDoctor,
                        fechaHora,
                        idUsuarioActual
                    );

                    MessageBox.Show("Cita registrada correctamente.");
                }
                else
                {
                    // Modificar cita existente
                    CitaCln.Modificar(
                        idCitaSeleccionada,
                        idDoctor,
                        fechaHora,
                        idUsuarioActual
                    );

                    MessageBox.Show("Cita modificada correctamente.");
                }

                // Actualizar lista
                CargarListaCitas();

                // Finalizar operación
                idCitaSeleccionada = 0;

                cboEspecialidad.SelectedIndex = -1;
                cboDoctor.DataSource = null;
                cboHora.Items.Clear();

                cboEspecialidad.Enabled = false;
                cboDoctor.Enabled = false;
                dtpFecha.Enabled = false;
                cboHora.Enabled = false;

                btnGuardar.Enabled = false;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo guardar la cita: " + ex.Message
                );
            }
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var fila = dgvLista.Rows[e.RowIndex];

            idCitaSeleccionada = Convert.ToInt32(
                fila.Cells["IdCita"].Value
            );

            string estado = fila.Cells["Estado"].Value.ToString();

            btnModificar.Enabled = estado == "Pendiente";
            btnEliminar.Enabled = estado == "Pendiente";

            // No recuperar datos todavía
            // Solo identificar la cita seleccionada
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idCitaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una cita de la lista.");
                return;
            }

            try
            {
                var fila = dgvLista.SelectedRows[0];

                // Recuperar datos del paciente
                idPacienteSeleccionado = Convert.ToInt32(
                    fila.Cells["IdPaciente"].Value
                );

                txtNombrePaciente.Text =
                    fila.Cells["NombrePaciente"].Value + " " +
                    fila.Cells["PrimerApellido"].Value + " " +
                    fila.Cells["SegundoApellido"].Value;

                // Recuperar doctor y especialidad
                string especialidad =
                    fila.Cells["Especialidad"].Value.ToString();

                int idDoctor = Convert.ToInt32(
                    fila.Cells["IdDoctor"].Value
                );

                DateTime fechaHora = Convert.ToDateTime(
                    fila.Cells["FechaHora"].Value
                );

                // Cargar especialidad
                cboEspecialidad.Enabled = true;
                cboDoctor.Enabled = true;
                dtpFecha.Enabled = true;
                cboHora.Enabled = true;

                cboEspecialidad.SelectedItem = especialidad;

                // Cargar doctores de esa especialidad
                CargarDoctores();

                cboDoctor.SelectedValue = idDoctor;

                // Cargar fecha
                dtpFecha.Value = fechaHora.Date;

                // Consultar horarios disponibles
                CargarHorariosDisponibles();

                // Mantener disponible el horario actual de esta cita
                string horaActual = fechaHora.ToString("HH:mm");

                if (!cboHora.Items.Contains(horaActual))
                {
                    cboHora.Items.Add(horaActual);
                }

                cboHora.SelectedItem = horaActual;

                // Preparar modo modificación
                btnGuardar.Enabled = true;
                btnModificar.Enabled = false;

                MessageBox.Show(
                    "Datos recuperados. Puede modificar la cita y presionar Guardar Cita."
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al recuperar los datos: " + ex.Message
                );
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCitaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una cita de la lista.");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de anular esta cita?",
                "Confirmar anulación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    CitaCln.Cancelar(
                        idCitaSeleccionada,
                        idUsuarioActual
                    );

                    MessageBox.Show("La cita fue anulada correctamente.");

                    CargarListaCitas();

                    idCitaSeleccionada = 0;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al anular la cita: " + ex.Message
                    );
                }
            }
        }
    }
}
