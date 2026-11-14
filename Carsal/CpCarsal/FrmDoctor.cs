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
            ConfigurarDgvLista();

            dgvLista.DataSource = ClnCarsal.DoctorCln.Listar();

            dgvLista.Columns["IdDoctor"].Visible = false;
        }
        private void LimpiarCampos()
        {
            txtCi.Clear();
            txtNombres.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtEspecialidad.Clear();
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

        private void ConfigurarDgvLista()
        {
            dgvLista.EnableHeadersVisualStyles = false;

            dgvLista.BackgroundColor = Color.White;
            dgvLista.BorderStyle = BorderStyle.None;
            dgvLista.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgvLista.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(6, 68, 90);

            dgvLista.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLista.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvLista.ColumnHeadersHeight = 40;

            dgvLista.DefaultCellStyle.BackColor = Color.White;
            dgvLista.DefaultCellStyle.ForeColor =
                Color.FromArgb(24, 59, 80);

            dgvLista.DefaultCellStyle.Font =
                new Font("Segoe UI", 9, FontStyle.Regular);

            dgvLista.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(230, 244, 245);

            dgvLista.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 140, 150);

            dgvLista.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvLista.RowTemplate.Height = 34;

            dgvLista.RowHeadersVisible = false;
            dgvLista.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvLista.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLista.MultiSelect = false;
            dgvLista.ReadOnly = true;
            dgvLista.AllowUserToAddRows = false;
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
                        "Complete el CI, los nombres y el primer apellido.");
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
                    string.IsNullOrWhiteSpace(txtEspecialidad.Text)
                        ? null : txtEspecialidad.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtDireccion.Text)
                        ? null : txtDireccion.Text.Trim(),
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Doctor registrado correctamente.");

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
                        MessageBox.Show("Seleccione un doctor de la lista.");
                        return;
                    }

                    var fila = dgvLista.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r =>
                            Convert.ToInt32(r.Cells["IdDoctor"].Value)
                            == idDoctorSeleccionado);

                    if (fila == null)
                    {
                        MessageBox.Show("No se encontró el doctor seleccionado.");
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
                    txtEspecialidad.Text = fila.Cells["Especialidad"].Value?.ToString();
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
                        "Complete el CI, los nombres y el primer apellido.");
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
                    string.IsNullOrWhiteSpace(txtEspecialidad.Text)
                        ? null : txtEspecialidad.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtDireccion.Text)
                        ? null : txtDireccion.Text.Trim(),
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Doctor modificado correctamente.");

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
                    MessageBox.Show("Seleccione un doctor de la lista.");
                    return;
                }

                int idDoctor = Convert.ToInt32(
                    dgvLista.CurrentRow.Cells["IdDoctor"].Value);

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea desactivar este doctor?",
                    "Confirmar desactivación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes)
                    return;

                ClnCarsal.DoctorCln.Eliminar(
                    idDoctor,
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show("Doctor desactivado correctamente.");

                dgvLista.DataSource = ClnCarsal.DoctorCln.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message,
                    "Error al desactivar doctor",
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
                    MessageBox.Show("Ingrese el CI del doctor que desea buscar.");
                    txtBuscarCi.Focus();
                    return;
                }

                var resultado = ClnCarsal.DoctorCln.BuscarPorCi(ci);
                dgvLista.DataSource = resultado;

                if (dgvLista.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró un doctor activo con ese CI.");
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
            txtCi.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

            pnlRegistro.Enabled = false;

            btnRegistrar.Enabled = true;
            btnModificar.Enabled = true;
        }
    }
}
