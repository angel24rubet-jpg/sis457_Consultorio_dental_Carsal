using ClnCarsal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CpCarsal
{
    public partial class FrmCita : Form
    {
        private int idPacienteSeleccionado = 0;
        private int idCitaSeleccionada = 0;
        private int idUsuarioActual = 1;
        private bool modoEdicion = false;

        public FrmCita()
        {
            InitializeComponent();
        }

        private void FrmCita_Load(object sender, EventArgs e)
        {
            DarFormatoGrilla();

            dtpFecha.Format = DateTimePickerFormat.Short;
            dtpFecha.MinDate = DateTime.Today;

            cboEspecialidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHora.DropDownStyle = ComboBoxStyle.DropDownList;
            txtNombrePaciente.ReadOnly = true;

            CargarEspecialidades();
            ActualizarLista();
            ConfigurarEstadoInicial();
        }

        // ESTADO INICIAL Y LIMPIEZA
        private void ConfigurarEstadoInicial()
        {
            LimpiarCampos();

            pnlRegistro.Enabled = false;
            txtBuscarCi.Enabled = true;
            btnBuscar.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

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
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
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

        // FORMATO DE LA TABLA
        private void DarFormatoGrilla()
        {
            Color oscuro = Color.FromArgb(0, 64, 64);

            dgvLista.BackgroundColor = Color.White;
            dgvLista.BorderStyle = BorderStyle.None;
            dgvLista.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLista.GridColor = Color.FromArgb(227, 235, 235);
            dgvLista.RowHeadersVisible = false;
            dgvLista.AllowUserToAddRows = false;
            dgvLista.AllowUserToDeleteRows = false;
            dgvLista.AllowUserToResizeRows = false;
            dgvLista.ReadOnly = true;
            dgvLista.MultiSelect = false;
            dgvLista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvLista.EnableHeadersVisualStyles = false;
            dgvLista.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLista.ColumnHeadersHeight = 38;
            dgvLista.ColumnHeadersDefaultCellStyle.BackColor = oscuro;
            dgvLista.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLista.ColumnHeadersDefaultCellStyle.SelectionBackColor = oscuro;
            dgvLista.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvLista.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvLista.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgvLista.RowTemplate.Height = 34;
            dgvLista.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvLista.DefaultCellStyle.ForeColor = Color.FromArgb(31, 45, 45);
            dgvLista.DefaultCellStyle.SelectionBackColor = Color.FromArgb(203, 232, 229);
            dgvLista.DefaultCellStyle.SelectionForeColor = Color.FromArgb(31, 45, 45);
            dgvLista.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvLista.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 249, 249);
        }

        // LISTAR Y ORGANIZAR CITAS
        private void ActualizarLista()
        {
            dgvLista.DataSource = null;
            dgvLista.Columns.Clear();
            dgvLista.AutoGenerateColumns = true;
            dgvLista.DataSource = CitaCln.Listar();

            DarFormatoGrilla();

            string[] columnasNecesarias =
            {
                "IdCita", "IdPaciente", "IdDoctor", "Ci",
                "NombrePaciente", "PrimerApellido", "SegundoApellido",
                "FechaHora", "NombreDoctor", "ApellidosDoctor",
                "Especialidad", "Estado"
            };

            foreach (string nombre in columnasNecesarias)
            {
                if (!dgvLista.Columns.Contains(nombre))
                {
                    MessageBox.Show(
                        "No se encontró la columna '" + nombre +
                        "' en el resultado de paCitaListar(). Verifique el procedimiento almacenado.",
                        "ERROR EN LA LISTA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Ocultar identificadores y fecha original (se muestran dos columnas separadas).
            dgvLista.Columns["IdCita"].Visible = false;
            dgvLista.Columns["IdPaciente"].Visible = false;
            dgvLista.Columns["IdDoctor"].Visible = false;
            dgvLista.Columns["FechaHora"].Visible = false;

            // Encabezados
            dgvLista.Columns["Ci"].HeaderText = "CI";
            dgvLista.Columns["NombrePaciente"].HeaderText = "Nombres";
            dgvLista.Columns["PrimerApellido"].HeaderText = "Primer apellido";
            dgvLista.Columns["SegundoApellido"].HeaderText = "Segundo apellido";
            dgvLista.Columns["NombreDoctor"].HeaderText = "Nombre doctor";
            dgvLista.Columns["ApellidosDoctor"].HeaderText = "Apellidos doctor";
            dgvLista.Columns["Especialidad"].HeaderText = "Especialidad";
            dgvLista.Columns["Estado"].HeaderText = "Estado";

            // Fecha y hora se muestran por separado, usando el mismo dato FechaHora.
            DataGridViewTextBoxColumn columnaFecha = new DataGridViewTextBoxColumn();
            columnaFecha.Name = "Fecha";
            columnaFecha.HeaderText = "Fecha";
            columnaFecha.DataPropertyName = "FechaHora";
            columnaFecha.DefaultCellStyle.Format = "dd/MM/yyyy";
            columnaFecha.ReadOnly = true;
            dgvLista.Columns.Add(columnaFecha);

            DataGridViewTextBoxColumn columnaHora = new DataGridViewTextBoxColumn();
            columnaHora.Name = "Hora";
            columnaHora.HeaderText = "Hora";
            columnaHora.DataPropertyName = "FechaHora";
            columnaHora.DefaultCellStyle.Format = "HH:mm";
            columnaHora.ReadOnly = true;
            dgvLista.Columns.Add(columnaHora);

            // Orden solicitado: CI, paciente, fecha, hora, doctor, especialidad y estado.
            dgvLista.Columns["Ci"].DisplayIndex = 0;
            dgvLista.Columns["NombrePaciente"].DisplayIndex = 1;
            dgvLista.Columns["PrimerApellido"].DisplayIndex = 2;
            dgvLista.Columns["SegundoApellido"].DisplayIndex = 3;
            dgvLista.Columns["Fecha"].DisplayIndex = 4;
            dgvLista.Columns["Hora"].DisplayIndex = 5;
            dgvLista.Columns["NombreDoctor"].DisplayIndex = 6;
            dgvLista.Columns["ApellidosDoctor"].DisplayIndex = 7;
            dgvLista.Columns["Especialidad"].DisplayIndex = 8;
            dgvLista.Columns["Estado"].DisplayIndex = 9;

            dgvLista.Columns["Ci"].FillWeight = 65;
            dgvLista.Columns["NombrePaciente"].FillWeight = 100;
            dgvLista.Columns["PrimerApellido"].FillWeight = 100;
            dgvLista.Columns["SegundoApellido"].FillWeight = 100;
            dgvLista.Columns["Fecha"].FillWeight = 85;
            dgvLista.Columns["Hora"].FillWeight = 65;
            dgvLista.Columns["NombreDoctor"].FillWeight = 100;
            dgvLista.Columns["ApellidosDoctor"].FillWeight = 110;
            dgvLista.Columns["Especialidad"].FillWeight = 95;
            dgvLista.Columns["Estado"].FillWeight = 80;

            dgvLista.ClearSelection();
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            idCitaSeleccionada = 0;
        }

        // CARGAR ESPECIALIDADES
        private void CargarEspecialidades()
        {
            IEnumerable doctores = DoctorCln.Listar() as IEnumerable;
            List<string> especialidades = new List<string>();

            if (doctores != null)
            {
                foreach (var doctor in doctores)
                {
                    var propiedad = doctor.GetType().GetProperty("Especialidad");
                    if (propiedad == null) continue;

                    string especialidad = Convert.ToString(propiedad.GetValue(doctor, null));
                    if (!string.IsNullOrWhiteSpace(especialidad) &&
                        !especialidades.Contains(especialidad))
                        especialidades.Add(especialidad);
                }
            }

            especialidades.Sort();
            cboEspecialidad.DataSource = especialidades;
            cboEspecialidad.SelectedIndex = -1;
        }

        // CARGAR DOCTORES SEGUN ESPECIALIDAD
        private void CargarDoctores()
        {
            cboDoctor.DataSource = null;
            cboDoctor.Items.Clear();

            if (cboEspecialidad.SelectedIndex == -1) return;

            string especialidad = Convert.ToString(cboEspecialidad.SelectedItem);
            IEnumerable doctores = DoctorCln.Listar() as IEnumerable;
            List<object> lista = new List<object>();

            if (doctores != null)
            {
                foreach (var doctor in doctores)
                {
                    var tipo = doctor.GetType();
                    var propEspecialidad = tipo.GetProperty("Especialidad");
                    var propId = tipo.GetProperty("IdDoctor");
                    var propNombres = tipo.GetProperty("Nombres");
                    var propApellidos = tipo.GetProperty("Apellidos");

                    if (propEspecialidad == null || propId == null ||
                        propNombres == null || propApellidos == null)
                        continue;

                    string esp = Convert.ToString(propEspecialidad.GetValue(doctor, null));
                    if (esp == especialidad)
                    {
                        int id = Convert.ToInt32(propId.GetValue(doctor, null));
                        string nombres = Convert.ToString(propNombres.GetValue(doctor, null));
                        string apellidos = Convert.ToString(propApellidos.GetValue(doctor, null));

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

        // CARGAR HORARIOS DISPONIBLES Y MARCAR LOS OCUPADOS
        private void CargarHorariosDisponibles()
        {
            cboHora.Items.Clear();

            if (cboDoctor.SelectedIndex == -1 || cboDoctor.SelectedValue == null)
                return;

            int idDoctor = Convert.ToInt32(cboDoctor.SelectedValue);
            DateTime fecha = dtpFecha.Value.Date;
            IEnumerable resultado = CitaCln.HorariosOcupados(idDoctor, fecha) as IEnumerable;
            List<TimeSpan> horariosOcupados = new List<TimeSpan>();

            if (resultado != null)
            {
                foreach (var registro in resultado)
                {
                    if (registro == null) continue;

                    var propiedad = registro.GetType().GetProperty("HoraOcupada");
                    if (propiedad == null) continue;

                    object valor = propiedad.GetValue(registro, null);
                    TimeSpan horaOcupada;
                    if (valor != null && TimeSpan.TryParse(valor.ToString(), out horaOcupada))
                        horariosOcupados.Add(horaOcupada);
                }
            }

            for (int hora = 8; hora < 20; hora++)
            {
                for (int minuto = 0; minuto < 60; minuto += 30)
                {
                    TimeSpan turno = new TimeSpan(hora, minuto, 0);
                    DateTime fechaHora = fecha.Add(turno);
                    if (fechaHora <= DateTime.Now) continue;

                    string texto = fechaHora.ToString("HH:mm");
                    if (horariosOcupados.Any(h => h.Hours == turno.Hours && h.Minutes == turno.Minutes))
                        texto += " - OCUPADO";

                    cboHora.Items.Add(texto);
                }
            }

            cboHora.SelectedIndex = -1;
        }

        // EVENTOS DE LOS COMBOS
        private void cboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlRegistro.Enabled)
                CargarDoctores();
        }

        private void cboDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pnlRegistro.Enabled && cboDoctor.SelectedIndex != -1)
                CargarHorariosDisponibles();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (pnlRegistro.Enabled && cboDoctor.SelectedIndex != -1)
                CargarHorariosDisponibles();
        }

        private void cboHora_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboHora.Text.Contains("OCUPADO"))
            {
                MessageBox.Show(
                    "Esta hora ya está ocupada. Seleccione otro horario.",
                    "MENSAJE-CONSULTORIO CARSAL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cboHora.SelectedIndex = -1;
            }
        }

        // BUSCAR PACIENTE POR CI
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string ci = txtBuscarCi.Text.Trim();
                if (string.IsNullOrWhiteSpace(ci))
                {
                    MessageBox.Show("Ingrese el CI del paciente.", 
                        "MENSAJE CONSULTORIO CARSAL");
                    txtBuscarCi.Focus();
                    return;
                }

                var paciente = PacienteCln.BuscarPorCi(ci).FirstOrDefault();

                if (paciente == null)
                {
                    DialogResult respuesta = MessageBox.Show(
                        "El paciente con el CI " + ci + " no está registrado, ¿desea registrarlo?",
                        "MENSAJE-CONSULTORIO CARSAL",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (respuesta != DialogResult.Yes) return;

                    using (FrmPaciente formulario = new FrmPaciente())
                    {
                        formulario.RegistroDesdeCita = true;
                        formulario.CiDesdeCita = ci;

                        if (formulario.ShowDialog() != DialogResult.OK) return;

                        idPacienteSeleccionado = formulario.IdPacienteRegistrado;
                        txtNombrePaciente.Text = formulario.NombrePacienteRegistrado;
                    }

                    if (idPacienteSeleccionado == 0) return;
                    HabilitarRegistro();
                    return;
                }

                idPacienteSeleccionado = paciente.IdPaciente;
                txtNombrePaciente.Text = paciente.Nombres + " " +
                    paciente.PrimerApellido + " " + paciente.SegundoApellido;

                HabilitarRegistro();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.GetBaseException().Message,
                    "ERROR AL BUSCAR PACIENTE",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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

                if (cboDoctor.SelectedIndex == -1 || cboDoctor.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un doctor.");
                    return;
                }

                if (cboHora.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un horario.");
                    return;
                }

                string horarioSeleccionado = cboHora.SelectedItem.ToString();
                if (horarioSeleccionado.Contains("OCUPADO"))
                {
                    MessageBox.Show(
                        "El horario seleccionado ya tiene una cita registrada. Por favor, seleccione otra hora.",
                        "HORARIO OCUPADO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    cboHora.SelectedIndex = -1;
                    return;
                }

                int idDoctor = Convert.ToInt32(cboDoctor.SelectedValue);
                TimeSpan hora = TimeSpan.Parse(horarioSeleccionado);
                DateTime fechaHora = dtpFecha.Value.Date.Add(hora);

                if (!modoEdicion)
                {
                    CitaCln.Insertar(idPacienteSeleccionado, idDoctor, fechaHora, idUsuarioActual);
                    MessageBox.Show(
                        "Cita registrada correctamente.",
                        "CONSULTORIO CARSAL",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    CitaCln.Modificar(idCitaSeleccionada, idDoctor, fechaHora, idUsuarioActual);
                    MessageBox.Show(
                        "Cita modificada correctamente.",
                        "CONSULTORIO CARSAL",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                ActualizarLista();
                ConfigurarEstadoInicial();
                txtBuscarCi.Focus();
            }
            catch (Exception ex)
            {
                string mensaje = ex.GetBaseException().Message;
                if (mensaje.IndexOf("ocupad", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    mensaje.IndexOf("duplic", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    MessageBox.Show(
                        "El horario seleccionado ya está ocupado. Por favor, seleccione otro horario.",
                        "HORARIO OCUPADO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        "No se pudo guardar la cita.\n\n" + mensaje,
                        "ERROR AL GUARDAR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // CANCELAR REGISTRO O MODIFICACION
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ActualizarLista();
            ConfigurarEstadoInicial();
            txtBuscarCi.Focus();
        }

        // SELECCIONAR UNA CITA DE LA TABLA
        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || modoEdicion) return;

            DataGridViewRow fila = dgvLista.Rows[e.RowIndex];
            if (fila.Cells["IdCita"].Value == null || fila.Cells["Estado"].Value == null)
                return;

            idCitaSeleccionada = Convert.ToInt32(fila.Cells["IdCita"].Value);
            string estado = Convert.ToString(fila.Cells["Estado"].Value);

            btnModificar.Enabled = estado == "Pendiente";
            btnEliminar.Enabled = estado == "Pendiente";
        }

        // MODIFICAR CITA SELECCIONADA
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idCitaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una cita.", "MENSAJE-CONSULTORIO CARSAL");
                return;
            }

            try
            {
                DataGridViewRow fila = dgvLista.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => r.Cells["IdCita"].Value != null &&
                        Convert.ToInt32(r.Cells["IdCita"].Value) == idCitaSeleccionada);

                if (fila == null)
                {
                    MessageBox.Show("No se encontró la cita seleccionada.", "MENSAJE-CONSULTORIO CARSAL");
                    return;
                }

                idPacienteSeleccionado = Convert.ToInt32(fila.Cells["IdPaciente"].Value);
                txtNombrePaciente.Text =
                    Convert.ToString(fila.Cells["NombrePaciente"].Value) + " " +
                    Convert.ToString(fila.Cells["PrimerApellido"].Value) + " " +
                    Convert.ToString(fila.Cells["SegundoApellido"].Value);

                string especialidad = Convert.ToString(fila.Cells["Especialidad"].Value);
                int idDoctor = Convert.ToInt32(fila.Cells["IdDoctor"].Value);
                DateTime fechaHora = Convert.ToDateTime(fila.Cells["FechaHora"].Value);

                modoEdicion = true;
                HabilitarRegistro();

                txtBuscarCi.Enabled = false;
                btnBuscar.Enabled = false;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;

                cboEspecialidad.SelectedItem = especialidad;
                CargarDoctores();
                cboDoctor.SelectedValue = idDoctor;

                dtpFecha.Value = fechaHora.Date;
                CargarHorariosDisponibles();

                string horaActual = fechaHora.ToString("HH:mm");
                if (!cboHora.Items.Contains(horaActual))
                    cboHora.Items.Add(horaActual);
                cboHora.SelectedItem = horaActual;

                btnGuardar.Text = "GUARDAR CAMBIOS";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al recuperar los datos de la cita.\n\n" + ex.GetBaseException().Message,
                    "ERROR AL MODIFICAR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // CANCELAR CITA
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idCitaSeleccionada == 0)
            {
                MessageBox.Show("Seleccione una cita.", "MENSAJE-CONSULTORIO CARSAL");
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de cancelar esta cita?",
                "MENSAJE-CONSULTORIO CARSAL",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            try
            {
                CitaCln.Cancelar(idCitaSeleccionada, idUsuarioActual);
                MessageBox.Show(
                    "La cita fue cancelada correctamente.",
                    "CONSULTORIO CARSAL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ActualizarLista();
                ConfigurarEstadoInicial();
                txtBuscarCi.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cancelar la cita.\n\n" + ex.GetBaseException().Message,
                    "ERROR AL CANCELAR",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
