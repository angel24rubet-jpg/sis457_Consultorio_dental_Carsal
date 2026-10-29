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
        public FrmDoctor()
        {
            InitializeComponent();
        }

        private void FrmDoctor_Load(object sender, EventArgs e)
        {
            dgvLista.DataSource = ClnCarsal.DoctorCln.Listar();
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
                if (dgvLista.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un doctor de la lista.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCi.Text) ||
                    string.IsNullOrWhiteSpace(txtNombres.Text) ||
                    string.IsNullOrWhiteSpace(txtPrimerApellido.Text))
                {
                    MessageBox.Show(
                        "Complete el CI, los nombres y el primer apellido.");
                    return;
                }

                int idDoctor = Convert.ToInt32(
                    dgvLista.CurrentRow.Cells["IdDoctor"].Value);

                ClnCarsal.DoctorCln.Modificar(
                    idDoctor,
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

            txtCi.Text = fila.Cells["Ci"].Value?.ToString();
            txtNombres.Text = fila.Cells["Nombres"].Value?.ToString();

            string apellidos = fila.Cells["Apellidos"].Value?.ToString() ?? "";
            string[] partes = apellidos.Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            txtPrimerApellido.Text = partes.Length > 0 ? partes[0] : "";
            txtSegundoApellido.Text = partes.Length > 1
                ? string.Join(" ", partes, 1, partes.Length - 1)
                : "";

            txtCelular.Text = fila.Cells["Celular"].Value?.ToString();
            txtEspecialidad.Text = fila.Cells["Especialidad"].Value?.ToString();
            txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();
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
    }
}
