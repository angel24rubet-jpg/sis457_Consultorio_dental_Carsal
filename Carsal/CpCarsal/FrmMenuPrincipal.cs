
using System;
using System.Windows.Forms;

namespace CpCarsal
{
    public partial class frmMenuPrincipal : Form
    {
        public frmMenuPrincipal()
        {
            InitializeComponent();

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
    }
}
