
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

            // Mostrar informacion del usuario
            lblUsuario.Text = "Usuario: " + nombreUsuario;
            lblRol.Text = "Rol: " + rolUsuario;

            // Configurar permisos segun el rol
            if (rolUsuario == "Doctor")
            {
                btnDoctores.Visible = false;
                btnUsuarios.Visible = false;
            }

            // Configurar ventana principal
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Asociar evento del boton Inicio
            btnInicio.Click += btnInicio_Click;
        }

        // ABRIR FORMULARIOS DENTRO DEL PANEL
        private void AbrirFormulario(Form formulario)
        {
            pnlContenido.Controls.Clear();

            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;

            pnlContenido.Controls.Add(formulario);
            formulario.Show();
        }

        // MOSTRAR MENU PRINCIPAL
        private void btnInicio_Click(object sender, EventArgs e)
        {
            // Limpiar el formulario que esta abierto
            pnlContenido.Controls.Clear();

            // Mostrar nuevamente el fondo del menu principal
            pnlContenido.Visible = true;
        }

        // MODULO PACIENTES
        private void btnPacientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmPaciente());
        }

        // MODULO DOCTORES
        private void btnDoctores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmDoctor());
        }

        // MODULO CITAS
        private void btnCitas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCita());
        }

        // MODULO USUARIOS
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmUsuario());
        }

        // MODULO HISTORIAL CLINICO
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmHistorialClinico());
        }

        // CERRAR SESION
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
        // MODULO PAGOS
        private void btnPagos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmPago());
        }

        private void pnlMenuLateral_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEspecialidad_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmEspecialidad());
        }
    }
}
