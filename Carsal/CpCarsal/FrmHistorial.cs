using System;
using System.Windows.Forms;
using ClnCarsal;

namespace CpCarsal
{
    public partial class FrmHistorial : Form
    {
        // Lo que el usuario tiene seleccionado en este momento
        private int idPacienteActual = 0;
        private int idCitaSeleccionada = 0;       // cita que se va a atender
        private int idHistorialSeleccionado = 0;  // registro del historial que se va a corregir

        public FrmHistorial()
        {
            InitializeComponent();
        }

        private void FrmHistorial_Load(object sender, EventArgs e)
        {
            ConfigurarGrilla(dgvCitas);
            ConfigurarGrilla(dgvHistorial);
            LimpiarTodo();
        }

        // ------------------------------------------------------------
        //  Buscar paciente
        // ------------------------------------------------------------
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string ci = txtBuscarCi.Text.Trim();
                if (ci.Length == 0)
                {
                    MessageBox.Show("Ingrese el CI del paciente.");
                    txtBuscarCi.Focus();
                    return;
                }

                var pacientes = PacienteCln.BuscarPorCi(ci);
                if (pacientes.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún paciente con ese CI.");
                    LimpiarTodo();
                    return;
                }

                var p = pacientes[0];
                idPacienteActual = p.IdPaciente;
                lblPaciente.Text = "Paciente: " + p.Nombres + " " + p.PrimerApellido + " " +
                                   p.SegundoApellido + "   (CI " + p.Ci + ")";

                LimpiarFormularioAtencion();
                CargarCitas();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Error al buscar paciente");
            }
        }

        // ------------------------------------------------------------
        //  Cargar las dos tablas
        // ------------------------------------------------------------
        private void CargarCitas()
        {
            dgvCitas.DataSource = HistorialCln.ListarCitasPorAtender(idPacienteActual);

            ConfigurarColumna(dgvCitas, "IdCita", null, false);
            ConfigurarColumna(dgvCitas, "FechaHora", "Fecha y hora", true, "dd/MM/yyyy HH:mm");
            ConfigurarColumna(dgvCitas, "Doctor", "Doctor");
            ConfigurarColumna(dgvCitas, "Motivo", "Motivo");
            ConfigurarColumna(dgvCitas, "Estado", "Estado");

            dgvCitas.ClearSelection();   // que no quede ninguna fila marcada hasta que se haga clic
        }

        private void CargarHistorial()
        {
            dgvHistorial.DataSource = HistorialCln.ListarPorPaciente(idPacienteActual);

            ConfigurarColumna(dgvHistorial, "IdHistorial", null, false);
            ConfigurarColumna(dgvHistorial, "IdCita", null, false);
            ConfigurarColumna(dgvHistorial, "FechaAtencion", "Fecha de atención", true, "dd/MM/yyyy HH:mm");
            ConfigurarColumna(dgvHistorial, "Doctor", "Doctor");
            ConfigurarColumna(dgvHistorial, "Diagnostico", "Diagnóstico");
            ConfigurarColumna(dgvHistorial, "Tratamiento", "Tratamiento");
            ConfigurarColumna(dgvHistorial, "Observaciones", "Observaciones");

            dgvHistorial.ClearSelection();
        }

        // ------------------------------------------------------------
        //  Clic en una cita: se va a registrar una atención NUEVA
        // ------------------------------------------------------------
        private void dgvCitas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            idCitaSeleccionada = Convert.ToInt32(dgvCitas.Rows[e.RowIndex].Cells["IdCita"].Value);
            idHistorialSeleccionado = 0;

            txtDiagnostico.Clear();
            txtTratamiento.Clear();
            txtObservaciones.Clear();
            dgvHistorial.ClearSelection();

            ActualizarBotones();
            txtDiagnostico.Focus();
        }

        // ------------------------------------------------------------
        //  Clic en un registro del historial: se va a CORREGIR
        // ------------------------------------------------------------
        private void dgvHistorial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvHistorial.Rows[e.RowIndex];
            idHistorialSeleccionado = Convert.ToInt32(fila.Cells["IdHistorial"].Value);
            idCitaSeleccionada = 0;

            txtDiagnostico.Text = Convert.ToString(fila.Cells["Diagnostico"].Value);
            txtTratamiento.Text = Convert.ToString(fila.Cells["Tratamiento"].Value);
            txtObservaciones.Text = Convert.ToString(fila.Cells["Observaciones"].Value);
            dgvCitas.ClearSelection();

            ActualizarBotones();
        }

        // ------------------------------------------------------------
        //  Registrar atención (cita -> historial)
        // ------------------------------------------------------------
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idCitaSeleccionada == 0)
                {
                    MessageBox.Show("Primero seleccione, en la tabla de arriba, la cita que va a atender.");
                    return;
                }

                int idUsuario = UsuarioCln.ObtenerIdUsuarioActivo().Value;

                HistorialCln.Registrar(
                    idCitaSeleccionada,
                    txtDiagnostico.Text,
                    txtTratamiento.Text,
                    txtObservaciones.Text,
                    idUsuario);

                MessageBox.Show("Atención registrada. La cita quedó marcada como Atendida.");

                LimpiarFormularioAtencion();
                CargarCitas();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Error al registrar la atención");
            }
        }

        // ------------------------------------------------------------
        //  Guardar cambios de un registro existente
        // ------------------------------------------------------------
        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idHistorialSeleccionado == 0)
                {
                    MessageBox.Show("Primero seleccione, en la tabla de abajo, el registro que desea corregir.");
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Guardar los cambios en este registro del historial?",
                    "Confirmar modificación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (respuesta != DialogResult.Yes) return;

                int idUsuario = UsuarioCln.ObtenerIdUsuarioActivo().Value;

                HistorialCln.Modificar(
                    idHistorialSeleccionado,
                    txtDiagnostico.Text,
                    txtTratamiento.Text,
                    txtObservaciones.Text,
                    idUsuario);

                MessageBox.Show("Registro actualizado correctamente.");

                LimpiarFormularioAtencion();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MostrarError(ex, "Error al modificar el historial");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormularioAtencion();
            dgvCitas.ClearSelection();
            dgvHistorial.ClearSelection();
        }

        // ------------------------------------------------------------
        //  Utilidades
        // ------------------------------------------------------------
        private void LimpiarFormularioAtencion()
        {
            idCitaSeleccionada = 0;
            idHistorialSeleccionado = 0;
            txtDiagnostico.Clear();
            txtTratamiento.Clear();
            txtObservaciones.Clear();
            ActualizarBotones();
        }

        private void LimpiarTodo()
        {
            idPacienteActual = 0;
            lblPaciente.Text = "Paciente: (ninguno)";
            dgvCitas.DataSource = null;
            dgvHistorial.DataSource = null;
            LimpiarFormularioAtencion();
        }

        // "Registrar atención" solo con una cita elegida; "Guardar cambios" solo con un registro elegido
        private void ActualizarBotones()
        {
            btnGuardar.Enabled = idCitaSeleccionada > 0;
            btnModificar.Enabled = idHistorialSeleccionado > 0;
        }

        private void ConfigurarGrilla(DataGridView grilla)
        {
            grilla.ReadOnly = true;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.MultiSelect = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Cambia el título / visibilidad de una columna, solo si existe
        private void ConfigurarColumna(DataGridView grilla, string nombre, string titulo,
                                       bool visible = true, string formato = null)
        {
            if (!grilla.Columns.Contains(nombre)) return;

            DataGridViewColumn col = grilla.Columns[nombre];
            col.Visible = visible;
            if (titulo != null) col.HeaderText = titulo;
            if (formato != null) col.DefaultCellStyle.Format = formato;
        }

        private void MostrarError(Exception ex, string titulo)
        {
            MessageBox.Show(
                ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}