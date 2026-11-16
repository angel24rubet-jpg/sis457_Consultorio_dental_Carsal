using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClnCarsal;

namespace CpCarsal
{
    public partial class FrmPago : Form
    {
        private int idPacienteActual = 0;

        // Servicios agregados al cobro (todavía no guardados)
        private List<ItemCobro> items = new List<ItemCobro>();

        // CI del paciente a cargar al abrir (se usa desde Historial -> "Cobrar")
        private string ciInicial = null;

        public FrmPago()
        {
            InitializeComponent();
        }

        public FrmPago(string ci) : this()
        {
            ciInicial = ci;
        }

        private void FrmPago_Load(object sender, EventArgs e)
        {
            DarFormatoGrilla(dgvDetalle);
            DarFormatoGrilla(dgvPagos);

            CargarServicios();
            ListarPagos();
            RefrescarDetalle();

            pnlCobro.Enabled = false;

            // Si se abrió desde el Historial, busca directamente al paciente
            if (!string.IsNullOrWhiteSpace(ciInicial))
            {
                txtBuscarCi.Text = ciInicial;
                btnBuscar_Click(this, EventArgs.Empty);
            }
        }

        // ================= 1. BUSCAR PACIENTE =================
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
                var lista = PacienteCln.BuscarPorCi(ci);

                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún paciente con ese CI.", "Cobro",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idPacienteActual = 0;
                    lblPaciente.Text = "Ningún paciente seleccionado";
                    pnlCobro.Enabled = false;
                    return;
                }

                var paciente = lista[0];
                idPacienteActual = paciente.IdPaciente;
                lblPaciente.Text = (paciente.Nombres + " " + paciente.PrimerApellido + " " +
                    paciente.SegundoApellido).Trim() + "   ·   CI " + paciente.Ci;

                LimpiarCobro();
                CargarCitas();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= 2. CITAS ATENDIDAS SIN PAGAR =================
        private void CargarCitas()
        {
            var citas = PagoCln.CitasPorCobrar(idPacienteActual);

            cboCita.DataSource = null;
            cboCita.DisplayMember = "Descripcion";
            cboCita.ValueMember = "IdCita";
            cboCita.DataSource = citas;

            if (citas.Count == 0)
            {
                pnlCobro.Enabled = false;
                txtAtencion.Clear();
                MessageBox.Show(
                    "El paciente no tiene citas atendidas pendientes de pago.\n\n" +
                    "Primero registre la atención en el Historial Clínico.",
                    "Cobro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            pnlCobro.Enabled = true;
            cboCita.SelectedIndex = 0;
        }

        // Al elegir una cita, muestra lo que se le hizo (desde el historial clínico)
        private void cboCita_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAtencion.Clear();

            if (cboCita.SelectedValue == null || !(cboCita.SelectedValue is int))
                return;

            int idCita = (int)cboCita.SelectedValue;

            var atencion = HistorialClinicoCln.ListarPorPaciente(idPacienteActual)
                .FirstOrDefault(h => h.IdCita == idCita);

            if (atencion == null)
            {
                txtAtencion.Text = "No hay atención registrada para esta cita.";
                return;
            }

            txtAtencion.Text =
                "Diagnóstico: " + atencion.Diagnostico + Environment.NewLine +
                "Tratamiento: " + atencion.Tratamiento +
                (string.IsNullOrWhiteSpace(atencion.Observaciones)
                    ? "" : Environment.NewLine + "Observaciones: " + atencion.Observaciones);
        }

        // ================= 3. SERVICIOS Y PRECIO =================
        private void CargarServicios()
        {
            cboServicio.DisplayMember = "Nombre";
            cboServicio.ValueMember = "IdServicio";
            cboServicio.DataSource = PagoCln.ListarServicios();
            DeseleccionarServicio();
        }

        // Con DataSource, a veces hay que poner -1 dos veces para que quede vacío
        private void DeseleccionarServicio()
        {
            cboServicio.SelectedIndex = -1;
            cboServicio.SelectedIndex = -1;
            nudPrecio.Value = 0;
        }

        // Al elegir un servicio, se sugiere su precio referencial (se puede cambiar)
        private void cboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            var servicio = cboServicio.SelectedItem as CadCarsal.paServicioListar_Result;
            nudPrecio.Value = servicio != null ? servicio.Precio : 0;
        }

        // ================= 4. AGREGAR / QUITAR DEL DETALLE =================
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            erpCampos.Clear();

            var servicio = cboServicio.SelectedItem as CadCarsal.paServicioListar_Result;
            if (servicio == null)
            {
                erpCampos.SetError(cboServicio, "Seleccione un servicio");
                return;
            }
            if (nudPrecio.Value <= 0)
            {
                erpCampos.SetError(nudPrecio, "Ingrese el precio según la complejidad");
                return;
            }

            int cantidad = (int)nudCantidad.Value;
            decimal precio = nudPrecio.Value;

            // Si el mismo servicio ya está con el mismo precio, solo suma la cantidad
            // Cada servicio se agrega una sola vez; para cambiar la cantidad se quita y se agrega de nuevo
            if (items.Any(i => i.IdServicio == servicio.IdServicio))
            {
                erpCampos.SetError(cboServicio, "Este servicio ya está en el detalle. Quítelo si quiere cambiar la cantidad.");
                return;
            }

            items.Add(new ItemCobro
            {
                IdServicio = servicio.IdServicio,
                Servicio = servicio.Nombre,
                Cantidad = cantidad,
                PrecioUnitario = precio
            });

            RefrescarDetalle();

            DeseleccionarServicio();
            nudCantidad.Value = 1;
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow == null || dgvDetalle.CurrentRow.Index < 0)
            {
                MessageBox.Show("Seleccione un servicio del detalle.", "Cobro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            items.RemoveAt(dgvDetalle.CurrentRow.Index);
            RefrescarDetalle();
        }

        // Muestra el detalle y recalcula el total
        private void RefrescarDetalle()
        {
            dgvDetalle.DataSource = null;
            dgvDetalle.DataSource = items.ToList();

            if (dgvDetalle.Columns.Contains("IdServicio"))
            {
                dgvDetalle.Columns["IdServicio"].Visible = false;
                dgvDetalle.Columns["Servicio"].FillWeight = 160;
                dgvDetalle.Columns["Cantidad"].HeaderText = "Cant.";
                dgvDetalle.Columns["Cantidad"].FillWeight = 50;
                dgvDetalle.Columns["PrecioUnitario"].HeaderText = "Precio Bs";
                dgvDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
                dgvDetalle.Columns["Subtotal"].HeaderText = "Subtotal Bs";
                dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            }

            lblTotal.Text = "TOTAL: Bs " + items.Sum(i => i.Subtotal).ToString("N2");
        }

        // ================= 5. REGISTRAR EL COBRO =================
        private bool ValidarCampos()
        {
            bool esValido = true;
            erpCampos.Clear();

            if (cboCita.SelectedIndex == -1)
            {
                erpCampos.SetError(cboCita, "Seleccione la cita a cobrar");
                esValido = false;
            }
            if (items.Count == 0)
            {
                erpCampos.SetError(dgvDetalle, "Agregue al menos un servicio");
                esValido = false;
            }
            if (cboMetodo.SelectedIndex == -1)
            {
                erpCampos.SetError(cboMetodo, "Seleccione el método de pago");
                esValido = false;
            }

            return esValido;
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            decimal total = items.Sum(i => i.Subtotal);

            DialogResult respuesta = MessageBox.Show(
                "¿Registrar el cobro de Bs " + total.ToString("N2") +
                " con " + cboMetodo.Text + "?",
                "Confirmar cobro",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                int idPago = PagoCln.Registrar(
                    (int)cboCita.SelectedValue,
                    cboMetodo.Text,
                    items,
                    UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show(
                    "Cobro registrado correctamente.\n\nN° de pago: " + idPago +
                    "\nTotal: Bs " + total.ToString("N2"),
                    "Cobro", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCobro();
                CargarCitas();
                ListarPagos();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCobro();
        }

        private void LimpiarCobro()
        {
            items.Clear();
            RefrescarDetalle();
            DeseleccionarServicio();
            nudCantidad.Value = 1;
            cboMetodo.SelectedIndex = -1;
            erpCampos.Clear();
        }

        // ================= 6. PAGOS REALIZADOS =================
        private void ListarPagos()
        {
            dgvPagos.DataSource = PagoCln.Listar();

            if (dgvPagos.Columns.Contains("IdPago"))
            {
                dgvPagos.Columns["IdPago"].HeaderText = "N°";
                dgvPagos.Columns["IdPago"].FillWeight = 40;
                dgvPagos.Columns["FechaPago"].HeaderText = "Fecha de pago";
                dgvPagos.Columns["FechaPago"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvPagos.Columns["Ci"].HeaderText = "CI";
                dgvPagos.Columns["FechaCita"].HeaderText = "Cita";
                dgvPagos.Columns["FechaCita"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvPagos.Columns["Concepto"].FillWeight = 180;
                dgvPagos.Columns["MetodoPago"].HeaderText = "Método";
                dgvPagos.Columns["Monto"].HeaderText = "Monto Bs";
                dgvPagos.Columns["Monto"].DefaultCellStyle.Format = "N2";
            }

            int total = dgvPagos.Rows.Count;
            lblTotalPagos.Text = total == 1 ? "1 pago registrado" : total + " pagos registrados";
        }

        // Doble clic: muestra los servicios cobrados en ese pago
        private void dgvPagos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            try
            {
                int idPago = Convert.ToInt32(dgvPagos.Rows[e.RowIndex].Cells["IdPago"].Value);
                var detalle = PagoCln.ListarDetalle(idPago);

                string texto = "";
                foreach (var d in detalle)
                    texto += d.Servicio + "   x" + d.Cantidad +
                             "   Bs " + d.PrecioUnitario.ToString("N2") +
                             "   =  Bs " + d.Subtotal.ToString("N2") + Environment.NewLine;

                texto += Environment.NewLine + "TOTAL: Bs " + detalle.Sum(d => d.Subtotal).ToString("N2");

                MessageBox.Show(texto, "Detalle del pago N° " + idPago,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= FORMATO DE TABLAS =================
        private void DarFormatoGrilla(DataGridView dgv)
        {
            System.Drawing.Color oscuro = System.Drawing.Color.FromArgb(0, 64, 64);

            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = System.Drawing.Color.FromArgb(227, 235, 235);
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = oscuro;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = oscuro;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            dgv.RowTemplate.Height = 32;
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgv.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 45, 45);
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(203, 232, 229);
            dgv.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 45, 45);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249);
        }

        private void MostrarError(Exception ex)
        {
            MessageBox.Show(
                ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                "Detalle del error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}