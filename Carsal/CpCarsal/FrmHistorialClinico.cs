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
            DarFormatoGrilla();
            pnlAtencion.Enabled = false;
            lblTotal.Text = "Busque un paciente para ver su historial";
            txtBuscarCi.Focus();
        }

        // ================= BUSCAR PACIENTE =================
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            erpCampos.Clear();
            string ci = txtBuscarCi.Text.Trim();

            if (string.IsNullOrWhiteSpace(ci))
            {
                erpCampos.SetError(txtBuscarCi, "Ingrese el CI del paciente");
                return;
            }

            try
            {
                var lista = ClnCarsal.PacienteCln.BuscarPorCi(ci);

                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún paciente con ese CI.", "Historial clínico",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idPacienteActual = 0;
                    lblPaciente.Text = "Ningún paciente seleccionado";
                    pnlAtencion.Enabled = false;
                    dgvHistorial.DataSource = null;
                    lblTotal.Text = "Busque un paciente para ver su historial";
                    return;
                }

                var paciente = lista[0];
                idPacienteActual = paciente.IdPaciente;
                lblPaciente.Text = (paciente.Nombres + " " +
                    paciente.PrimerApellido + " " +
                    paciente.SegundoApellido).Trim() +
                    "   ·   CI " + paciente.Ci;

                pnlAtencion.Enabled = true;
                SalirModoEdicion();
                CargarCitas();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= VALIDACIÓN DE CAMPOS VACÍOS =================
        private bool ValidarCampos()
        {
            bool esValido = true;
            erpCampos.Clear();

            if (string.IsNullOrWhiteSpace(txtDiagnostico.Text))
            {
                erpCampos.SetError(txtDiagnostico, "El diagnóstico es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrWhiteSpace(txtTratamiento.Text))
            {
                erpCampos.SetError(txtTratamiento, "El tratamiento es obligatorio");
                esValido = false;
            }

            return esValido;
        }

        // ================= REGISTRAR ATENCIÓN =================
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (idPacienteActual == 0)
            {
                MessageBox.Show("Primero busque un paciente.", "Historial clínico",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCampos())
                return;

            try
            {
                var cita = cboCita.SelectedItem as ItemCita;

                ClnCarsal.HistorialClinicoCln.Insertar(
                    idPacienteActual,
                    cita == null ? null : cita.IdCita,
                    txtDiagnostico.Text,
                    txtTratamiento.Text,
                    txtObservaciones.Text,
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Atención registrada correctamente.", "Historial clínico",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
                CargarCitas();
                CargarHistorial();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= MODIFICAR ATENCIÓN (en dos etapas) =================
        private void btnModificar_Click(object sender, EventArgs e)
        {
            // PRIMERA ETAPA: recuperar los datos de la atención seleccionada
            if (!modoEdicion)
            {
                if (dgvHistorial.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione una atención de la lista.", "Historial clínico",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                btnModificar.Text = "GUARDAR CAMBIOS";
                txtDiagnostico.Focus();
                return;
            }

            // SEGUNDA ETAPA: guardar los cambios
            if (!ValidarCampos())
                return;

            try
            {
                ClnCarsal.HistorialClinicoCln.Modificar(
                    idHistorialSeleccionado,
                    txtDiagnostico.Text,
                    txtTratamiento.Text,
                    txtObservaciones.Text,
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Atención modificada correctamente.", "Historial clínico",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

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
             ClnCarsal.HistorialClinicoCln.BuscarEnHistorial(idPacienteActual, txtFiltro.Text);
            FormatearGrilla();
        }

        // Formato visual de la tabla (colores, alto de filas, encabezado)
        private void DarFormatoGrilla()
        {
            System.Drawing.Color oscuro = System.Drawing.Color.FromArgb(0, 64, 64);

            dgvHistorial.BackgroundColor = System.Drawing.Color.White;
            dgvHistorial.BorderStyle = BorderStyle.None;
            dgvHistorial.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHistorial.GridColor = System.Drawing.Color.FromArgb(227, 235, 235);
            dgvHistorial.RowHeadersVisible = false;
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AllowUserToDeleteRows = false;
            dgvHistorial.AllowUserToResizeRows = false;
            dgvHistorial.ReadOnly = true;
            dgvHistorial.MultiSelect = false;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Encabezado
            dgvHistorial.EnableHeadersVisualStyles = false;
            dgvHistorial.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHistorial.ColumnHeadersHeight = 38;
            dgvHistorial.ColumnHeadersDefaultCellStyle.BackColor = oscuro;
            dgvHistorial.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvHistorial.ColumnHeadersDefaultCellStyle.SelectionBackColor = oscuro;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgvHistorial.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvHistorial.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Filas
            dgvHistorial.RowTemplate.Height = 34;
            dgvHistorial.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvHistorial.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 45, 45);
            dgvHistorial.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(203, 232, 229);
            dgvHistorial.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 45, 45);
            dgvHistorial.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvHistorial.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249);
        }

        // Títulos legibles y columnas ocultas
        private void FormatearGrilla()
        {
            string[] ocultas = { "IdHistorial", "IdPaciente", "IdCita" };
            foreach (string columna in ocultas)
                if (dgvHistorial.Columns.Contains(columna))
                    dgvHistorial.Columns[columna].Visible = false;

            if (dgvHistorial.Columns.Contains("FechaAtencion"))
            {
                dgvHistorial.Columns["FechaAtencion"].HeaderText = "Fecha";
                dgvHistorial.Columns["FechaAtencion"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvHistorial.Columns["FechaAtencion"].FillWeight = 70;
                dgvHistorial.Columns["Diagnostico"].HeaderText = "Diagnóstico";
                dgvHistorial.Columns["Diagnostico"].FillWeight = 130;
                dgvHistorial.Columns["Tratamiento"].FillWeight = 130;
                dgvHistorial.Columns["Observaciones"].FillWeight = 110;
                dgvHistorial.Columns["Doctor"].FillWeight = 90;
                dgvHistorial.Columns["RegistradoPor"].HeaderText = "Registrado por";
                dgvHistorial.Columns["RegistradoPor"].FillWeight = 70;
            }

            int total = dgvHistorial.Rows.Count;
            lblTotal.Text = total == 1 ? "1 atención registrada" : total + " atenciones registradas";
        }

        private void LimpiarCampos()
        {
            txtDiagnostico.Clear();
            txtTratamiento.Clear();
            txtObservaciones.Clear();
            erpCampos.Clear();
            if (cboCita.Items.Count > 0)
                cboCita.SelectedIndex = 0;
        }

        private void SalirModoEdicion()
        {
            modoEdicion = false;
            idHistorialSeleccionado = 0;
            cboCita.Enabled = true;
            btnRegistrar.Enabled = true;
            btnModificar.Text = "MODIFICAR";
            LimpiarCampos();
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(
                ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                "Detalle del error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {

        }
    }
}