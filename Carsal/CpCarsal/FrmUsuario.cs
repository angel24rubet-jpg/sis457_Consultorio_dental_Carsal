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
        private int idUsuarioSeleccionado = 0;
        private bool modoEdicion = false;

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            DarFormatoGrilla();
            // Cargar roles
            cboRol.Items.Clear();
            cboRol.Items.Add("Administrador");
            cboRol.Items.Add("Doctor");
            cboRol.SelectedIndex = 0;

            // Cargar doctores
            cboDoctor.DataSource = ClnCarsal.DoctorCln.Listar();
            cboDoctor.DisplayMember = "Nombres";
            cboDoctor.ValueMember = "IdDoctor";
            cboDoctor.SelectedIndex = -1;
            cboDoctor.Enabled = false;

            // Configurar paneles
            pnlRegistro.Enabled = true;
            pnlLista.Enabled = true;

            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            btnModificar.Text = "MODIFICAR";

            // Cargar usuarios
            ActualizarLista();
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

        private void ActualizarLista()
        {
            // Primero cargar los datos
            dgvLista.DataSource = null;
            dgvLista.AutoGenerateColumns = true;
            dgvLista.DataSource = ClnCarsal.UsuarioCln.Listar();

            // Despues aplicar el formato
            DarFormatoGrilla();

            // Ocultar identificadores
            if (dgvLista.Columns.Contains("IdUsuario"))
                dgvLista.Columns["IdUsuario"].Visible = false;

            if (dgvLista.Columns.Contains("IdDoctor"))
                dgvLista.Columns["IdDoctor"].Visible = false;

            // Configurar encabezados
            if (dgvLista.Columns.Contains("NombreUsuario"))
                dgvLista.Columns["NombreUsuario"].HeaderText = "Usuario";

            if (dgvLista.Columns.Contains("Rol"))
                dgvLista.Columns["Rol"].HeaderText = "Rol";

            if (dgvLista.Columns.Contains("Doctor"))
                dgvLista.Columns["Doctor"].HeaderText = "Doctor asociado";

            if (dgvLista.Columns.Contains("Estado"))
                dgvLista.Columns["Estado"].HeaderText = "Estado";

            dgvLista.ClearSelection();
        }

        private void cboRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRol.SelectedItem == null)
                return;

            bool esDoctor = cboRol.SelectedItem.ToString() == "Doctor";

            cboDoctor.Enabled = esDoctor;

            if (!esDoctor)
                cboDoctor.SelectedIndex = -1;
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

                if (rol == "Doctor")
                {
                    if (cboDoctor.SelectedIndex == -1)
                    {
                        MessageBox.Show("Seleccione el doctor asociado.");
                        return;
                    }

                    idDoctor = Convert.ToInt32(cboDoctor.SelectedValue);
                }

                ClnCarsal.UsuarioCln.Registrar(
                    nombreUsuario,
                    contrasena,
                    rol,
                    idDoctor
                );

                MessageBox.Show(
                    "Usuario registrado correctamente.",
                    "CONSULTORIO CARSAL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                ActualizarLista();
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
            cboDoctor.Enabled = false;

            txtNombreUsuario.Focus();
        }
        private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila = dgvLista.Rows[e.RowIndex];

            idUsuarioSeleccionado = Convert.ToInt32(
                fila.Cells["IdUsuario"].Value
            );

            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                // Primera pulsacion: cargar datos
                if (!modoEdicion)
                {
                    if (idUsuarioSeleccionado == 0)
                    {
                        MessageBox.Show("Seleccione un usuario de la lista.");
                        return;
                    }

                    var fila = dgvLista.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r =>
                            Convert.ToInt32(r.Cells["IdUsuario"].Value)
                            == idUsuarioSeleccionado);

                    if (fila == null)
                    {
                        MessageBox.Show("No se encontró el usuario seleccionado.");
                        return;
                    }

                    txtNombreUsuario.Text =
                        fila.Cells["NombreUsuario"].Value?.ToString();

                    cboRol.SelectedItem =
                        fila.Cells["Rol"].Value?.ToString();

                    // Recuperar doctor asociado
                    string nombreDoctor =
                        fila.Cells["Doctor"].Value?.ToString();

                    if (cboRol.SelectedItem.ToString() == "Doctor")
                    {
                        cboDoctor.Enabled = true;

                        object valorDoctor = fila.Cells["IdDoctor"].Value;

                        if (valorDoctor != null && valorDoctor != DBNull.Value)
                            cboDoctor.SelectedValue = Convert.ToInt32(valorDoctor);
                        else
                            cboDoctor.SelectedIndex = -1;
                    }
                    else
                    {
                        cboDoctor.SelectedIndex = -1;
                        cboDoctor.Enabled = false;
                    }

                    txtContraseña.Clear();

                    pnlRegistro.Enabled = true;

                    modoEdicion = true;
                    btnModificar.Text = "GUARDAR CAMBIOS";
                    btnRegistrar.Enabled = false;

                    return;
                }

                // Segunda pulsacion: guardar cambios
                string nombreUsuario = txtNombreUsuario.Text.Trim();
                string contraseña = txtContraseña.Text;
                string rol = cboRol.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(nombreUsuario) ||
                    string.IsNullOrWhiteSpace(rol))
                {
                    MessageBox.Show("Complete los campos obligatorios.");
                    return;
                }

                int? idDoctor = null;

                if (rol == "Doctor")
                {
                    if (cboDoctor.SelectedIndex == -1)
                    {
                        MessageBox.Show("Seleccione el doctor asociado.");
                        return;
                    }

                    idDoctor = Convert.ToInt32(cboDoctor.SelectedValue);
                }

                ClnCarsal.UsuarioCln.Modificar(
                    idUsuarioSeleccionado,
                    nombreUsuario,
                    contraseña,
                    rol,
                    idDoctor
                );

                MessageBox.Show("Usuario modificado correctamente.");

                ActualizarLista();
                LimpiarCampos();

                modoEdicion = false;
                idUsuarioSeleccionado = 0;
                btnModificar.Text = "MODIFICAR";
                btnRegistrar.Enabled = true;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException?.Message ?? ex.Message,
                    "Error al modificar usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idUsuarioSeleccionado == 0)
                {
                    MessageBox.Show("Seleccione un usuario de la lista.");
                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Está seguro de que desea desactivar este usuario?",
                    "Confirmar desactivación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (respuesta != DialogResult.Yes)
                    return;

                ClnCarsal.UsuarioCln.Eliminar(idUsuarioSeleccionado);

                MessageBox.Show("Usuario desactivado correctamente.");

                ActualizarLista();
                LimpiarCampos();

                idUsuarioSeleccionado = 0;
                modoEdicion = false;

                btnModificar.Text = "MODIFICAR";
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnRegistrar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.InnerException?.Message ?? ex.Message,
                    "Error al desactivar usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();

            idUsuarioSeleccionado = 0;
            modoEdicion = false;

            btnModificar.Text = "MODIFICAR";
            btnRegistrar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            dgvLista.ClearSelection();
        }
    }
}
