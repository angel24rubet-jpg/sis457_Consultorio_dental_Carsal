using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpCarsal
{
    public partial class FrmDoctor : Form
    {
        private int idDoctorSeleccionado = 0;
        private bool modoEdicion = false;
        public FrmDoctor()
        {
            InitializeComponent();

            pnlRegistro.Enabled = false;
        }



        private void FrmDoctor_Load(object sender, EventArgs e)
        {
            // Cargar especialidades
            CargarEspecialidades();

            // Cargar doctores
            dgvLista.DataSource = ClnCarsal.DoctorCln.Listar();

            // Ocultar identificador
            if (dgvLista.Columns.Contains("IdDoctor"))
            {
                dgvLista.Columns["IdDoctor"].Visible = false;
            }

            // Aplicar formato visual
            DarFormatoGrilla();
        }


        // Llena el desplegable con las especialidades de la tabla
        private void CargarEspecialidades()
        {
            cboEspecialidad.DisplayMember = "Nombre";          // lo que se VE
            cboEspecialidad.ValueMember = "IdEspecialidad";    // lo que se GUARDA
            cboEspecialidad.DataSource = ClnCarsal.EspecialidadCln.Listar();
            cboEspecialidad.SelectedIndex = -1;
        }
        private void LimpiarCampos()
        {
            txtCi.Clear();
            txtNombres.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            cboEspecialidad.SelectedIndex = -1;
            txtDireccion.Clear();
            txtCelular.Clear();

            idDoctorSeleccionado = 0;
            modoEdicion = false;

            btnModificar.Text = "MODIFICAR DATOS";
        }
        private void ActualizarLista()
        {
            ActualizarLista();

            if (dgvLista.Columns.Contains("IdDoctor"))
                dgvLista.Columns["IdDoctor"].Visible = false;
        }

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

        private void ConfigurarEstadoBoton(Button boton)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.UseVisualStyleBackColor = false;

            if (boton.Enabled)
            {
                // Boton habilitado
                boton.BackColor = Color.FromArgb(6, 68, 90);
                boton.ForeColor = Color.White;
            }
            else
            {
                // Boton deshabilitado visualmente
                boton.BackColor = Color.FromArgb(75, 112, 128);
                boton.ForeColor = Color.White;
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCi.Text) ||
                    string.IsNullOrWhiteSpace(txtNombres.Text) ||
                    string.IsNullOrWhiteSpace(txtPrimerApellido.Text))
                {
                    MessageBox.Show(
                        "Complete el CI, los nombres y el primer apellido.",
                        "MENSAJE-CONSULTORIO CARSAL");
                    return;
                }
                if (cboEspecialidad.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione la especialidad del doctor.");
                    return;
                }
                ClnCarsal.DoctorCln.Insertar(
                       txtCi.Text.Trim(),
           txtNombres.Text.Trim(),
           txtPrimerApellido.Text.Trim(),
           string.IsNullOrWhiteSpace(txtSegundoApellido.Text)
               ? null : txtSegundoApellido.Text.Trim(),
           string.IsNullOrWhiteSpace(txtCelular.Text)
               ? null : txtCelular.Text.Trim(),
           (int)cboEspecialidad.SelectedValue,
           string.IsNullOrWhiteSpace(txtDireccion.Text)
               ? null : txtDireccion.Text.Trim(),
           ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
       ); ;

                MessageBox.Show("Doctor registrado correctamente.", 
                    "MENSAJE-CONSULTORIO CARSAL");

                dgvLista.DataSource = ClnCarsal.DoctorCln.Listar();

                LimpiarCampos();

                pnlRegistro.Enabled = false;

                btnRegistrar.Enabled = true;
                btnModificar.Enabled = true;


            }

            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message,
                    "Error al registrar doctor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // Primera pulsacion: recuperar datos del doctor
                if (!modoEdicion)
                {
                    if (idDoctorSeleccionado == 0)
                    {
                        MessageBox.Show("Seleccione un doctor de la lista.", 
                            "MENSAJE-CONSULTORIO CARSAL");
                        return;
                    }

                    var fila = dgvLista.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r =>
                            Convert.ToInt32(r.Cells["IdDoctor"].Value)
                            == idDoctorSeleccionado);

                    if (fila == null)
                    {
                        MessageBox.Show("No se encontró el doctor seleccionado.", 
                            "MENSAJE-CONSULTORIO CARSAL");
                        return;
                    }

                    txtCi.Text = fila.Cells["Ci"].Value?.ToString();
                    txtNombres.Text = fila.Cells["Nombres"].Value?.ToString();

                    string apellidos =
                        fila.Cells["Apellidos"].Value?.ToString() ?? "";

                    string[] partes = apellidos.Split(
                        new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    txtPrimerApellido.Text =
                        partes.Length > 0 ? partes[0] : "";

                    txtSegundoApellido.Text =
                        partes.Length > 1
                        ? string.Join(" ", partes, 1, partes.Length - 1)
                        : "";

                    txtCelular.Text = fila.Cells["Celular"].Value?.ToString();
                    cboEspecialidad.SelectedIndex = cboEspecialidad.FindStringExact(
    fila.Cells["Especialidad"].Value?.ToString());
                    txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();

                    pnlRegistro.Enabled = true;

                    btnRegistrar.Enabled = false;
                    btnModificar.Enabled = true;

                    modoEdicion = true;
                    btnModificar.Text = "GUARDAR CAMBIOS";

                    return;
                }

                // Segunda pulsacion: guardar modificaciones
                if (string.IsNullOrWhiteSpace(txtCi.Text) ||
                    string.IsNullOrWhiteSpace(txtNombres.Text) ||
                    string.IsNullOrWhiteSpace(txtPrimerApellido.Text))
                {
                    MessageBox.Show(
                        "Complete el CI, los nombres y el primer apellido.",
                        "MENSAJE-CONSULTORIO CARSAL");
                    return;
                }

                ClnCarsal.DoctorCln.Modificar(
                    idDoctorSeleccionado,
                    txtCi.Text.Trim(),
                    txtNombres.Text.Trim(),
                    txtPrimerApellido.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtSegundoApellido.Text)
                        ? null : txtSegundoApellido.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtCelular.Text)
                        ? null : txtCelular.Text.Trim(),
                     (int)cboEspecialidad.SelectedValue,
                    string.IsNullOrWhiteSpace(txtDireccion.Text)
                        ? null : txtDireccion.Text.Trim(),
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Doctor modificado correctamente.", 
                    "MENSAJE-CONSULTORIO CARSAL");

                dgvLista.DataSource = ClnCarsal.DoctorCln.Listar();

                LimpiarCampos();

                pnlRegistro.Enabled = false;

                btnRegistrar.Enabled = true;
                btnModificar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message,
                    "Error al modificar doctor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila = dgvLista.Rows[e.RowIndex];

            idDoctorSeleccionado = Convert.ToInt32(
                fila.Cells["IdDoctor"].Value);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLista.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un doctor de la lista.",
                        "MENSAJE-CONSULTORIO CARSAL");
                    return;
                }

                int idDoctor = Convert.ToInt32(
                    dgvLista.CurrentRow.Cells["IdDoctor"].Value);

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea aliminar este doctor?",
                    "MENSAJE-CONSULTORIO CARSAL",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                ClnCarsal.DoctorCln.Eliminar(
                    idDoctor,
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Doctor eliminado correctamente.", 
                    "MENSAJE-CONSULTORIO CARSAL");

                dgvLista.DataSource = ClnCarsal.DoctorCln.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message,
                    "Error al eliminar doctor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBuscarCi_Click(object sender, EventArgs e)
        {
            try
            {
                string ci = txtBuscarCi.Text.Trim();

                if (string.IsNullOrWhiteSpace(ci))
                {
                    MessageBox.Show("Ingrese el CI del doctor que desea buscar", 
                        "MENSAJE-CONSULTORIO CARSAL");
                    txtBuscarCi.Focus();
                    return;
                }

                var resultado = ClnCarsal.DoctorCln.BuscarPorCi(ci);
                dgvLista.DataSource = resultado;

                if (dgvLista.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró un doctor activo con ese CI.",
                        "MENSAJE-CONSULTORIO CARSAL");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message,
                    "Error al buscar doctor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMostrarLista_Click(object sender, EventArgs e)
        {
            try
            {
                txtBuscarCi.Clear();
                dgvLista.DataSource = ClnCarsal.DoctorCln.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message,
                    "Error al mostrar doctores",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnNuevaCita_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevoDoctor_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

            pnlRegistro.Enabled = true;

            btnRegistrar.Enabled = true;
            btnModificar.Enabled = false;

            ConfigurarEstadoBoton(btnRegistrar);
            ConfigurarEstadoBoton(btnModificar);

            txtCi.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

            pnlRegistro.Enabled = false;

            btnRegistrar.Enabled = true;
            btnModificar.Enabled = true;

            ConfigurarEstadoBoton(btnRegistrar);
            ConfigurarEstadoBoton(btnModificar);
        }
    }
}
