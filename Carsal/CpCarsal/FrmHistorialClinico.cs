using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CpCarsal
{
    public partial class FrmHistorialClinico : Form
    {
        private int idPacienteActual = 0;
        private int idHistorialSeleccionado = 0;
        private bool modoEdicion = false;

        // Elemento para el combo de citas (permite la opción "sin cita")
        private class ItemCita
        {
            public int? IdCita { get; set; }
            public string Texto { get; set; }
        }

        public FrmHistorialClinico()
        {
            InitializeComponent();
        }

        private void FrmHistorialClinico_Load(object sender, EventArgs e)
        {
            gbAtencion.Enabled = false;
        }

        // ================= BUSCAR PACIENTE =================
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string ci = txtBuscarCi.Text.Trim();

                if (string.IsNullOrWhiteSpace(ci))
                {
                    MessageBox.Show("Ingrese el CI del paciente.");
                    return;
                }

                var lista = ClnCarsal.PacienteCln.BuscarPorCi(ci);

                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún paciente con ese CI.");
                    idPacienteActual = 0;
                    lblPaciente.Text = "Ningún paciente seleccionado";
                    gbAtencion.Enabled = false;
                    dgvHistorial.DataSource = null;
                    return;
                }

                var paciente = lista[0];
                idPacienteActual = paciente.IdPaciente;
                lblPaciente.Text = (paciente.Nombres + " " +
                    paciente.PrimerApellido + " " +
                    paciente.SegundoApellido).Trim() +
                    "   -   CI: " + paciente.Ci;

                gbAtencion.Enabled = true;
                SalirModoEdicion();
                CargarCitas();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= REGISTRAR ATENCIÓN =================
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idPacienteActual == 0)
                {
                    MessageBox.Show("Primero busque un paciente.");
                    return;
                }

                var cita = cboCita.SelectedItem as ItemCita;

                ClnCarsal.HistorialClinicoCln.Insertar(
                    idPacienteActual,
                    cita == null ? null : cita.IdCita,
                    txtDiagnostico.Text,
                    txtTratamiento.Text,
                    txtObservaciones.Text,
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Atención registrada correctamente.");

                LimpiarCampos();
                CargarCitas();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= MODIFICAR ATENCIÓN =================
        private void btnModificar_Click(object sender, EventArgs e)
        {
            // PRIMERA ETAPA: recuperar los datos de la atención seleccionada
            if (!modoEdicion)
            {
                if (dgvHistorial.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una atención de la lista.");
                    return;
                }

                DataGridViewRow fila = dgvHistorial.CurrentRow;

                idHistorialSeleccionado = Convert.ToInt32(fila.Cells["IdHistorial"].Value);
                txtDiagnostico.Text = Convert.ToString(fila.Cells["Diagnostico"].Value);
                txtTratamiento.Text = Convert.ToString(fila.Cells["Tratamiento"].Value);
                txtObservaciones.Text = Convert.ToString(fila.Cells["Observaciones"].Value);

                modoEdicion = true;
                cboCita.Enabled = false;
                btnRegistrar.Enabled = false;
                btnModificar.Text = "Guardar cambios";

                MessageBox.Show("Datos recuperados. Ahora puede corregir la atención.");
                return;
            }

            // SEGUNDA ETAPA: guardar los cambios
            try
            {
                ClnCarsal.HistorialClinicoCln.Modificar(
                    idHistorialSeleccionado,
                    txtDiagnostico.Text,
                    txtTratamiento.Text,
                    txtObservaciones.Text,
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Atención modificada correctamente.");

                SalirModoEdicion();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            SalirModoEdicion();
        }

        // ================= MÉTODOS DE APOYO =================
        private void CargarCitas()
        {
            var items = new List<ItemCita>
            {
                new ItemCita { IdCita = null, Texto = "(Sin cita - atención directa)" }
            };

            items.AddRange(
                ClnCarsal.HistorialClinicoCln.CitasDisponibles(idPacienteActual)
                    .Select(c => new ItemCita { IdCita = c.IdCita, Texto = c.Descripcion })
            );

            cboCita.DataSource = null;
            cboCita.DisplayMember = "Texto";
            cboCita.DataSource = items;
            cboCita.SelectedIndex = 0;
        }

        private void CargarHistorial()
        {
            dgvHistorial.DataSource =
                ClnCarsal.HistorialClinicoCln.ListarPorPaciente(idPacienteActual);

            if (dgvHistorial.Columns.Count == 0)
                return;

            dgvHistorial.Columns["IdHistorial"].Visible = false;
            dgvHistorial.Columns["IdPaciente"].Visible = false;
            dgvHistorial.Columns["IdCita"].Visible = false;

            dgvHistorial.Columns["FechaAtencion"].HeaderText = "Fecha";
            dgvHistorial.Columns["FechaAtencion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvHistorial.Columns["Diagnostico"].HeaderText = "Diagnóstico";
            dgvHistorial.Columns["RegistradoPor"].HeaderText = "Registrado por";
        }

        private void LimpiarCampos()
        {
            txtDiagnostico.Clear();
            txtTratamiento.Clear();
            txtObservaciones.Clear();
            if (cboCita.Items.Count > 0)
                cboCita.SelectedIndex = 0;
        }

        private void SalirModoEdicion()
        {
            modoEdicion = false;
            idHistorialSeleccionado = 0;
            cboCita.Enabled = true;
            btnRegistrar.Enabled = true;
            btnModificar.Text = "Modificar";
            LimpiarCampos();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(
                ex.InnerException != null
                    ? ex.InnerException.Message
                    : ex.Message,
                "Detalle del error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }
}