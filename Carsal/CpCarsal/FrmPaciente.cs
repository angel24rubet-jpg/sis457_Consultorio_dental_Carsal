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
    public partial class FrmPaciente : Form
    {
        public FrmPaciente()
        {
            InitializeComponent();
        }

        private void FrmPaciente_Load(object sender, EventArgs e)
        {
            dgvLista.DataSource = ClnCarsal.PacienteCln.Listar();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                ClnCarsal.PacienteCln.Insertar(
                    txtCi.Text.Trim(),
                    txtNombres.Text.Trim(),
                    txtPrimerApellido.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtSegundoApellido.Text)
                        ? null : txtSegundoApellido.Text.Trim(),
                    dtpFechaNacimiento.Value,
                    cboSexo.Text,
                    txtCelular.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    1
                );

                MessageBox.Show("Paciente registrado correctamente.");
                dgvLista.DataSource = ClnCarsal.PacienteCln.Listar();
            }
            catch (Exception ex)
             {
                            MessageBox.Show(
                ex.InnerException != null
                    ? ex.InnerException.Message
                    : ex.Message,
                "Detalle del error"
            );
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLista.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un paciente de la lista.");
                    return;
                }

                int idPaciente = Convert.ToInt32(
                    dgvLista.CurrentRow.Cells["IdPaciente"].Value);

                ClnCarsal.PacienteCln.Modificar(
                    idPaciente,
                    txtCi.Text.Trim(),
                    txtNombres.Text.Trim(),
                    txtPrimerApellido.Text.Trim(),
                    string.IsNullOrWhiteSpace(txtSegundoApellido.Text)
                        ? null : txtSegundoApellido.Text.Trim(),
                    dtpFechaNacimiento.Value,
                    cboSexo.Text,
                    txtCelular.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    1
                );

                MessageBox.Show("Paciente modificado correctamente.");
                dgvLista.DataSource = ClnCarsal.PacienteCln.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar paciente: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLista.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un paciente de la lista.");
                    return;
                }

                int idPaciente = Convert.ToInt32(
                    dgvLista.CurrentRow.Cells["IdPaciente"].Value);

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de eliminar este paciente?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    ClnCarsal.PacienteCln.Eliminar(idPaciente, 1);

                    MessageBox.Show("Paciente eliminado correctamente.");
                    dgvLista.DataSource = ClnCarsal.PacienteCln.Listar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar paciente: " + ex.Message);
            }
        }

        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvLista.Rows[e.RowIndex];

            txtCi.Text = Convert.ToString(fila.Cells["Ci"].Value);
            txtNombres.Text = Convert.ToString(fila.Cells["Nombres"].Value);
            txtPrimerApellido.Text = Convert.ToString(fila.Cells["PrimerApellido"].Value);
            txtSegundoApellido.Text = Convert.ToString(fila.Cells["SegundoApellido"].Value);
            txtCelular.Text = Convert.ToString(fila.Cells["Celular"].Value);
            txtDireccion.Text = Convert.ToString(fila.Cells["Direccion"].Value);
            cboSexo.Text = Convert.ToString(fila.Cells["Sexo"].Value);

            if (fila.Cells["FechaNacimiento"].Value != DBNull.Value)
            {
                dtpFechaNacimiento.Value =
                    Convert.ToDateTime(fila.Cells["FechaNacimiento"].Value);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string ci = txtBuscarCi.Text.Trim();

                if (string.IsNullOrWhiteSpace(ci))
                {
                    MessageBox.Show("Ingrese el CI del paciente que desea buscar.");
                    return;
                }

                dgvLista.DataSource = ClnCarsal.PacienteCln.BuscarPorCi(ci);

                if (dgvLista.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró ningún paciente con ese CI.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar paciente: " + ex.Message);
            }
        }
    }
}
