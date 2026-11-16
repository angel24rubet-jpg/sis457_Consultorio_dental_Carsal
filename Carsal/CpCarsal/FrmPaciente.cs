using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;   // ← NUEVO: para usar funciones de Windows

namespace CpCarsal
{
    public partial class FrmPaciente : Form
    {
        private int idPacienteSeleccionado = 0;
        private bool modoEdicion = false;

        public bool RegistroDesdeCita { get; set; } = false;

        public string CiDesdeCita { get; set; }

        public int IdPacienteRegistrado { get; private set; }

        public string NombrePacienteRegistrado { get; private set; }


        public FrmPaciente()
        {
            InitializeComponent();
        }

        private void FrmPaciente_Load(object sender, EventArgs e)
        {
            DarFormatoGrilla();
            Limpiar();
            Listar();
            dtpFechaNacimiento.MaxDate = DateTime.Today.AddYears(-3);

            if (RegistroDesdeCita)
            {
                txtCi.Text = CiDesdeCita;
                txtCi.ReadOnly = true;
                pnlDatos.Enabled = true;
            }
        }

        // ================= LISTAR =================
        private void Listar()
        {
            dgvLista.DataSource = ClnCarsal.PacienteCln.Listar();
            FormatearGrilla();
        }

        // Formato visual de la tabla (colores, alto de filas, encabezado)
        private void DarFormatoGrilla()
        {
            System.Drawing.Color oscuro = System.Drawing.Color.FromArgb(0, 64, 64);

            dgvLista.BackgroundColor = System.Drawing.Color.White;
            dgvLista.BorderStyle = BorderStyle.None;
            dgvLista.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLista.GridColor = System.Drawing.Color.FromArgb(227, 235, 235);
            dgvLista.RowHeadersVisible = false;
            dgvLista.AllowUserToAddRows = false;
            dgvLista.AllowUserToDeleteRows = false;
            dgvLista.AllowUserToResizeRows = false;
            dgvLista.ReadOnly = true;
            dgvLista.MultiSelect = false;
            dgvLista.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLista.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Encabezado
            dgvLista.EnableHeadersVisualStyles = false;
            dgvLista.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvLista.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLista.ColumnHeadersHeight = 38;
            dgvLista.ColumnHeadersDefaultCellStyle.BackColor = oscuro;
            dgvLista.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dgvLista.ColumnHeadersDefaultCellStyle.SelectionBackColor = oscuro;
            dgvLista.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgvLista.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvLista.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Filas
            dgvLista.RowTemplate.Height = 34;
            dgvLista.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            dgvLista.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(31, 45, 45);
            dgvLista.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(203, 232, 229);
            dgvLista.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(31, 45, 45);
            dgvLista.DefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgvLista.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(244, 249, 249);
        }

        // Títulos legibles y columnas ocultas
        private void FormatearGrilla()
        {
            string[] ocultas = { "IdPaciente", "Estado", "IdUsuarioRegistro",
                                 "IdUsuarioModificacion", "FechaModificacion" };
            foreach (string columna in ocultas)
                if (dgvLista.Columns.Contains(columna))
                    dgvLista.Columns[columna].Visible = false;

            if (dgvLista.Columns.Contains("Ci"))
            {
                dgvLista.Columns["Ci"].HeaderText = "CI";
                dgvLista.Columns["PrimerApellido"].HeaderText = "Primer apellido";
                dgvLista.Columns["SegundoApellido"].HeaderText = "Segundo apellido";
                dgvLista.Columns["FechaNacimiento"].HeaderText = "Fecha nac.";
                dgvLista.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvLista.Columns["Direccion"].HeaderText = "Dirección";
                dgvLista.Columns["FechaRegistro"].HeaderText = "Registrado";
                dgvLista.Columns["FechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            int total = dgvLista.Rows.Count;
            lblTotal.Text = total == 1 ? "1 paciente activo" : total + " pacientes activos";
        }

        // ================= VALIDACIÓN DE CAMPOS VACÍOS =================
        private bool ValidarCampos()
        {
            bool esValido = true;
            erpCampos.Clear();

            if (string.IsNullOrWhiteSpace(txtCi.Text))
            {
                erpCampos.SetError(txtCi, "El CI es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrWhiteSpace(txtNombres.Text))
            {
                erpCampos.SetError(txtNombres, "Los nombres son obligatorios");
                esValido = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrimerApellido.Text))
            {
                erpCampos.SetError(txtPrimerApellido, "El primer apellido es obligatorio");
                esValido = false;
            }
            if (cboSexo.SelectedIndex == -1)
            {
                erpCampos.SetError(cboSexo, "Seleccione el sexo");
                esValido = false;
            }
            if (dtpFechaNacimiento.Value.Date > DateTime.Today)
            {
                erpCampos.SetError(dtpFechaNacimiento, "La fecha no puede ser futura");
                esValido = false;
            }
            // Validar fecha de nacimiento
            DateTime fechaNacimiento = dtpFechaNacimiento.Value.Date;
            DateTime hoy = DateTime.Today;

            if (fechaNacimiento > hoy)
            {
                MessageBox.Show(
                    "La fecha de nacimiento no puede ser futura.",
                    "CONSULTORIO CARSAL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                dtpFechaNacimiento.Focus();
                return false;
            }

            // Validar edad minima de 3 años
            DateTime fechaMinima = hoy.AddYears(-3);

            if (fechaNacimiento > fechaMinima)
            {
                MessageBox.Show(
                    "El paciente debe tener como minimo 3 años de edad.",
                    "CONSULTORIO CARSAL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                dtpFechaNacimiento.Focus();
                return false;
            }

            return esValido;
        }

        // Celular: solo números
        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        // ================= REGISTRAR =================
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                ClnCarsal.PacienteCln.Insertar(
                    txtCi.Text.Trim(),
                    txtNombres.Text.Trim(),
                    txtPrimerApellido.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtSegundoApellido.Text)
                        ? null : txtSegundoApellido.Text.Trim(),
                    dtpFechaNacimiento.Value.Date,
                    cboSexo.Text,
                    txtCelular.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show(
                    "Paciente registrado correctamente.",
                    "Pacientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Si se registro desde el formulario de citas
                if (RegistroDesdeCita)
                {
                    var paciente = ClnCarsal.PacienteCln.BuscarPorCi(
                        txtCi.Text.Trim()
                    ).FirstOrDefault();

                    if (paciente != null)
                    {
                        IdPacienteRegistrado = paciente.IdPaciente;

                        NombrePacienteRegistrado =
                            paciente.Nombres + " " +
                            paciente.PrimerApellido + " " +
                            paciente.SegundoApellido;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                }

                // Registro normal
                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= MODIFICAR (en dos etapas) =================
        private void btnModificar_Click(object sender, EventArgs e)
        {
            // PRIMERA ETAPA: recuperar los datos del paciente seleccionado
            if (!modoEdicion)
            {
                if (idPacienteSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un paciente de la lista.", "Pacientes",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow fila = dgvLista.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r =>
                        Convert.ToInt32(r.Cells["IdPaciente"].Value) == idPacienteSeleccionado);

                if (fila == null)
                    return;

                txtCi.Text = Convert.ToString(fila.Cells["Ci"].Value);
                txtNombres.Text = Convert.ToString(fila.Cells["Nombres"].Value);
                txtPrimerApellido.Text = Convert.ToString(fila.Cells["PrimerApellido"].Value);
                txtSegundoApellido.Text = Convert.ToString(fila.Cells["SegundoApellido"].Value);
                txtCelular.Text = Convert.ToString(fila.Cells["Celular"].Value);
                txtDireccion.Text = Convert.ToString(fila.Cells["Direccion"].Value);

                string sexo = Convert.ToString(fila.Cells["Sexo"].Value);
                cboSexo.SelectedIndex = cboSexo.Items.IndexOf(sexo);

                object fecha = fila.Cells["FechaNacimiento"].Value;
                dtpFechaNacimiento.Value = fecha is DateTime ? (DateTime)fecha : DateTime.Today;

                modoEdicion = true;
                btnModificar.Text = "GUARDAR CAMBIOS";
                btnRegistrar.Enabled = false;
                btnEliminar.Enabled = false;
                txtCi.Focus();
                return;
            }

            // SEGUNDA ETAPA: guardar las modificaciones
            if (!ValidarCampos())
                return;

            try
            {
                ClnCarsal.PacienteCln.Modificar(
                    idPacienteSeleccionado,
                    txtCi.Text.Trim(),
                    txtNombres.Text.Trim(),
                    txtPrimerApellido.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtSegundoApellido.Text)
                        ? null : txtSegundoApellido.Text.Trim(),
                    dtpFechaNacimiento.Value.Date,
                    cboSexo.Text,
                    txtCelular.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Paciente modificado correctamente.", "Pacientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= ELIMINAR (lógico) =================
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idPacienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un paciente de la lista.", "Pacientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de eliminar este paciente?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                ClnCarsal.PacienteCln.Eliminar(
                    idPacienteSeleccionado,
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value);

                MessageBox.Show("Paciente eliminado correctamente.", "Pacientes",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= LIMPIAR =================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtCi.Clear();
            txtNombres.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtCelular.Clear();
            txtDireccion.Clear();
            cboSexo.SelectedIndex = -1;
            dtpFechaNacimiento.Value = DateTime.Today;
            erpCampos.Clear();

            idPacienteSeleccionado = 0;
            modoEdicion = false;
            btnModificar.Text = "MODIFICAR";
            btnRegistrar.Enabled = true;
            btnEliminar.Enabled = true;
            txtCi.Focus();
        }

        // ================= SELECCIONAR FILA =================
        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || modoEdicion)
                return;

            idPacienteSeleccionado = Convert.ToInt32(
                dgvLista.Rows[e.RowIndex].Cells["IdPaciente"].Value);
        }

        // ================= BUSCAR =================
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string ci = txtBuscarCi.Text.Trim();

                // Si el buscador está vacío, se muestran todos
                if (string.IsNullOrWhiteSpace(ci))
                {
                    Listar();
                    return;
                }

                dgvLista.DataSource = ClnCarsal.PacienteCln.BuscarPorCi(ci);
                FormatearGrilla();

                if (dgvLista.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún paciente con ese CI.", "Pacientes",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
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