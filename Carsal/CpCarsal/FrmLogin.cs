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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // TEMPORAL: crea/restablece "administrador" (admin123) y muestra
            // a que servidor y base se conecta la aplicacion.
            // Borrar este bloque try/catch cuando el login ya funcione.
            try
            {
                MessageBox.Show(
                    ClnCarsal.UsuarioCln.RestablecerAdministrador(),
                    "Conexion actual");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message,
                    "Error de conexion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            txtUsuario.Clear();
            txtContraseña.Clear();

            txtUsuario.Focus();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = txtUsuario.Text.Trim();
                string contrasena = txtContraseña.Text;

                if (string.IsNullOrWhiteSpace(usuario) ||
                    string.IsNullOrWhiteSpace(contrasena))
                {
                    MessageBox.Show("Ingrese su usuario y contraseña.");
                    return;
                }

                bool acceso = ClnCarsal.UsuarioCln.ValidarAcceso(
                    usuario, contrasena);

                if (acceso)
                {
                    this.Hide();

                    using (FrmPaciente frm = new FrmPaciente())
                    {
                        frm.ShowDialog();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        "Usuario o contraseña incorrectos.",
                        "Acceso denegado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtContraseña.Clear();
                    txtContraseña.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException != null
                        ? ex.InnerException.Message
                        : ex.Message,
                    "Error al iniciar sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}