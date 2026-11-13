
using System;
using System.Windows.Forms;

namespace CpCarsal
{
    public partial class frmMenuPrincipal : Form
    {
        private string nombreUsuario;
        private string rolUsuario;

        public frmMenuPrincipal(string usuario, string rol)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            rolUsuario = rol;

            lblUsuario.Text = "Usuario: " + nombreUsuario;
            lblRol.Text = "Rol: " + rolUsuario;

            if (rolUsuario == "Doctor")
            {
                btnDoctores.Visible = false;
                btnUsuarios.Visible = false;
            }


            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AbrirFormulario(Form formulario)
        {
            pnlContenido.Controls.Clear();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            pnlContenido.Controls.Add(formulario);
            formulario.Show();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmPaciente());
        }

        private void btnDoctores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmDoctor());
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCita());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmUsuario());
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de que desea cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }
    }
}
