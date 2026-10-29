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
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            cboRol.Items.Clear();
            cboRol.Items.Add("Administrador");
            cboRol.Items.Add("Doctor");

            cboRol.SelectedIndex = 0;

            cboDoctor.Enabled = false;
        }

        private void cboRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRol.SelectedItem == null)
                return;

            cboDoctor.Enabled = true;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreUsuario = txtNombreUsuario.Text.Trim();
                string contrasena = txtContraseña.Text;
                string rol = cboRol.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(nombreUsuario) ||
                    string.IsNullOrWhiteSpace(contrasena) ||
                    string.IsNullOrWhiteSpace(rol))
                {
                    MessageBox.Show("Complete todos los campos obligatorios.");
                    return;
                }

                int? idDoctor = null;

                ClnCarsal.UsuarioCln.Registrar(
                    nombreUsuario,
                    contrasena,
                    rol,
                    idDoctor
                );

                MessageBox.Show("Usuario registrado correctamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException?.Message ?? ex.Message,
                    "Error al registrar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

        }
        private void LimpiarCampos()
        {
            txtNombreUsuario.Clear();
            txtContraseña.Clear();
            cboRol.SelectedIndex = 0;
            cboDoctor.SelectedIndex = -1;
            txtNombreUsuario.Focus();
        }
    }
}
