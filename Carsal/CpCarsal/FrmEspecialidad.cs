
using System;
using System.Linq;
using System.Windows.Forms;

namespace CpCarsal
{
    public partial class FrmEspecialidad : Form
    {
        private int idEspecialidadSeleccionada = 0;
        private bool modoEdicion = false;

        public FrmEspecialidad()
        {
            InitializeComponent();

            // Asociar eventos
            this.Load += FrmEspecialidad_Load;

            btnRegistrar.Click += btnRegistar_Click;
            btnCancelar.Click += btnCancelar_Click;
            btnModificar.Click += btnModificar_Click;
            btnEliminar.Click += btnEliminar_Click;

            dgvLista.CellClick += dgvLista_CellClick;
        }

        // ================= CARGAR FORMULARIO =================
        private void FrmEspecialidad_Load(object sender, EventArgs e)
        {
            DarFormatoGrilla();
            Limpiar();
            Listar();
        }

        // ================= LISTAR =================
        private void Listar()
        {
            dgvLista.DataSource = ClnCarsal.EspecialidadCln.ListarTodos();

            FormatearGrilla();
        }

        // ================= FORMATO DE GRILLA =================
        private void DarFormatoGrilla()
        {
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
            dgvLista.ColumnHeadersHeight = 38;
            dgvLista.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(0, 64, 64);
            dgvLista.ColumnHeadersDefaultCellStyle.ForeColor =
                System.Drawing.Color.White;
            dgvLista.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);

            // Filas
            dgvLista.RowTemplate.Height = 34;
            dgvLista.DefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            dgvLista.DefaultCellStyle.ForeColor =
                System.Drawing.Color.FromArgb(31, 45, 45);
            dgvLista.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(203, 232, 229);
            dgvLista.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.FromArgb(31, 45, 45);
            dgvLista.AlternatingRowsDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(244, 249, 249);
        }

        // ================= FORMATEAR COLUMNAS =================
        private void FormatearGrilla()
        {
            // Ocultar identificador
            if (dgvLista.Columns.Contains("IdEspecialidad"))
                dgvLista.Columns["IdEspecialidad"].Visible = false;

            // Encabezados
            if (dgvLista.Columns.Contains("Nombre"))
                dgvLista.Columns["Nombre"].HeaderText = "Especialidad";

            if (dgvLista.Columns.Contains("Descripcion"))
                dgvLista.Columns["Descripcion"].HeaderText = "Descripción";

            if (dgvLista.Columns.Contains("Estado"))
                dgvLista.Columns["Estado"].HeaderText = "Estado";

            if (dgvLista.Columns.Contains("FechaRegistro"))
            {
                dgvLista.Columns["FechaRegistro"].HeaderText = "Fecha de registro";
                dgvLista.Columns["FechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            // Mostrar únicamente las columnas necesarias
            string[] visibles =
            {
                "Nombre",
                "Descripcion",
                "Estado",
                "FechaRegistro"
            };

            foreach (DataGridViewColumn columna in dgvLista.Columns)
            {
                columna.Visible = visibles.Contains(columna.Name);
            }
        }

        // ================= REGISTRAR =================
        private void btnRegistar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                ClnCarsal.EspecialidadCln.Insertar(
                    txtNombreEspecialidad.Text.Trim(),
                    txtDescripcion.Text.Trim(),
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show(
                    "Especialidad registrada correctamente.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= VALIDAR CAMPOS =================
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreEspecialidad.Text))
            {
                MessageBox.Show(
                    "Ingrese el nombre de la especialidad.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtNombreEspecialidad.Focus();
                return false;
            }

            if (txtNombreEspecialidad.Text.Trim().Length > 100)
            {
                MessageBox.Show(
                    "El nombre no puede superar los 100 caracteres.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtNombreEspecialidad.Focus();
                return false;
            }

            if (txtDescripcion.Text.Trim().Length > 250)
            {
                MessageBox.Show(
                    "La descripción no puede superar los 250 caracteres.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtDescripcion.Focus();
                return false;
            }

            return true;
        }

        // ================= SELECCIONAR FILA =================
        private void dgvLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || modoEdicion)
                return;

            // Guardar solamente el ID seleccionado
            idEspecialidadSeleccionada = Convert.ToInt32(
                dgvLista.Rows[e.RowIndex].Cells["IdEspecialidad"].Value
            );

            // No cargar datos en los TextBox
        }

        // ================= MODIFICAR =================
        private void btnModificar_Click(object sender, EventArgs e)
        {
            // PRIMERA ETAPA: recuperar datos
            if (!modoEdicion)
            {
                if (idEspecialidadSeleccionada == 0)
                {
                    MessageBox.Show(
                        "Seleccione una especialidad de la lista.",
                        "Especialidades",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                DataGridViewRow fila = dgvLista.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r =>
                        Convert.ToInt32(r.Cells["IdEspecialidad"].Value)
                        == idEspecialidadSeleccionada
                    );

                if (fila == null)
                    return;

                // Recuperar datos solamente al pulsar MODIFICAR
                txtNombreEspecialidad.Text =
                    Convert.ToString(fila.Cells["Nombre"].Value);

                txtDescripcion.Text =
                    Convert.ToString(fila.Cells["Descripcion"].Value);

                modoEdicion = true;

                btnModificar.Text = "GUARDAR CAMBIOS";
                btnRegistrar.Enabled = false;
                btnEliminar.Enabled = false;

                txtNombreEspecialidad.Focus();
                return;
            }

            // SEGUNDA ETAPA: guardar cambios
            if (!ValidarCampos())
                return;

            try
            {
                ClnCarsal.EspecialidadCln.Modificar(
                    idEspecialidadSeleccionada,
                    txtNombreEspecialidad.Text.Trim(),
                    txtDescripcion.Text.Trim(),
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show(
                    "Especialidad modificada correctamente.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= ELIMINAR (LOGICO) =================
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idEspecialidadSeleccionada == 0)
            {
                MessageBox.Show(
                    "Seleccione una especialidad de la lista.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de desactivar esta especialidad?",
                "Confirmar desactivación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                ClnCarsal.EspecialidadCln.Eliminar(
                    idEspecialidadSeleccionada,
                    ClnCarsal.UsuarioCln.ObtenerIdUsuarioActivo().Value
                );

                MessageBox.Show(
                    "Especialidad desactivada correctamente.",
                    "Especialidades",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        // ================= CANCELAR / LIMPIAR =================
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtNombreEspecialidad.Clear();
            txtDescripcion.Clear();

            idEspecialidadSeleccionada = 0;
            modoEdicion = false;

            btnModificar.Text = "MODIFICAR";
            btnRegistrar.Enabled = true;
            btnEliminar.Enabled = true;

            dgvLista.ClearSelection();

            txtNombreEspecialidad.Focus();
        }

        // ================= MOSTRAR ERRORES =================
        private void MostrarError(Exception ex)
        {
            string mensaje = ex.InnerException != null
                ? ex.InnerException.Message
                : ex.Message;

            MessageBox.Show(
                mensaje,
                "Detalle del error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
    }
}
