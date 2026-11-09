namespace CpCarsal
{
    partial class FrmHistorial
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.grpPaciente = new System.Windows.Forms.GroupBox();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscarCi = new System.Windows.Forms.TextBox();
            this.lblBuscarCi = new System.Windows.Forms.Label();
            this.grpCitas = new System.Windows.Forms.GroupBox();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.grpAtencion = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtTratamiento = new System.Windows.Forms.TextBox();
            this.lblTratamiento = new System.Windows.Forms.Label();
            this.txtDiagnostico = new System.Windows.Forms.TextBox();
            this.lblDiagnostico = new System.Windows.Forms.Label();
            this.grpHistorial = new System.Windows.Forms.GroupBox();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.grpPaciente.SuspendLayout();
            this.grpCitas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.grpAtencion.SuspendLayout();
            this.grpHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPaciente
            // 
            this.grpPaciente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPaciente.Controls.Add(this.lblPaciente);
            this.grpPaciente.Controls.Add(this.btnBuscar);
            this.grpPaciente.Controls.Add(this.txtBuscarCi);
            this.grpPaciente.Controls.Add(this.lblBuscarCi);
            this.grpPaciente.Location = new System.Drawing.Point(16, 12);
            this.grpPaciente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPaciente.Name = "grpPaciente";
            this.grpPaciente.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpPaciente.Size = new System.Drawing.Size(1290, 86);
            this.grpPaciente.TabIndex = 0;
            this.grpPaciente.TabStop = false;
            this.grpPaciente.Text = "1. Buscar paciente";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaciente.Location = new System.Drawing.Point(547, 38);
            this.lblPaciente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(173, 20);
            this.lblPaciente.TabIndex = 3;
            this.lblPaciente.Text = "Paciente: (ninguno)";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(393, 32);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(120, 34);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscarCi
            // 
            this.txtBuscarCi.Location = new System.Drawing.Point(173, 37);
            this.txtBuscarCi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscarCi.Name = "txtBuscarCi";
            this.txtBuscarCi.Size = new System.Drawing.Size(199, 22);
            this.txtBuscarCi.TabIndex = 1;
            // 
            // lblBuscarCi
            // 
            this.lblBuscarCi.AutoSize = true;
            this.lblBuscarCi.Location = new System.Drawing.Point(20, 41);
            this.lblBuscarCi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBuscarCi.Name = "lblBuscarCi";
            this.lblBuscarCi.Size = new System.Drawing.Size(99, 16);
            this.lblBuscarCi.TabIndex = 0;
            this.lblBuscarCi.Text = "CI del paciente:";
            // 
            // grpCitas
            // 
            this.grpCitas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCitas.Controls.Add(this.dgvCitas);
            this.grpCitas.Location = new System.Drawing.Point(16, 108);
            this.grpCitas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpCitas.Name = "grpCitas";
            this.grpCitas.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpCitas.Size = new System.Drawing.Size(1290, 160);
            this.grpCitas.TabIndex = 1;
            this.grpCitas.TabStop = false;
            this.grpCitas.Text = "2. Citas por atender (haga clic en la cita que va a atender)";
            // 
            // dgvCitas
            // 
            this.dgvCitas.AllowUserToAddRows = false;
            this.dgvCitas.AllowUserToDeleteRows = false;
            this.dgvCitas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCitas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCitas.BackgroundColor = System.Drawing.Color.White;
            this.dgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCitas.Location = new System.Drawing.Point(13, 25);
            this.dgvCitas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvCitas.MultiSelect = false;
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.ReadOnly = true;
            this.dgvCitas.RowHeadersVisible = false;
            this.dgvCitas.RowHeadersWidth = 51;
            this.dgvCitas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCitas.Size = new System.Drawing.Size(1263, 123);
            this.dgvCitas.TabIndex = 0;
            this.dgvCitas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCitas_CellClick);
            // 
            // grpAtencion
            // 
            this.grpAtencion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAtencion.Controls.Add(this.btnLimpiar);
            this.grpAtencion.Controls.Add(this.btnModificar);
            this.grpAtencion.Controls.Add(this.btnGuardar);
            this.grpAtencion.Controls.Add(this.txtObservaciones);
            this.grpAtencion.Controls.Add(this.lblObservaciones);
            this.grpAtencion.Controls.Add(this.txtTratamiento);
            this.grpAtencion.Controls.Add(this.lblTratamiento);
            this.grpAtencion.Controls.Add(this.txtDiagnostico);
            this.grpAtencion.Controls.Add(this.lblDiagnostico);
            this.grpAtencion.Location = new System.Drawing.Point(16, 278);
            this.grpAtencion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAtencion.Name = "grpAtencion";
            this.grpAtencion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpAtencion.Size = new System.Drawing.Size(1290, 302);
            this.grpAtencion.TabIndex = 2;
            this.grpAtencion.TabStop = false;
            this.grpAtencion.Text = "3. Datos de la atención";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(447, 244);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(147, 39);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(233, 244);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(200, 39);
            this.btnModificar.TabIndex = 7;
            this.btnModificar.Text = "Guardar cambios";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(20, 244);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(200, 39);
            this.btnGuardar.TabIndex = 6;
            this.btnGuardar.Text = "Registrar atención";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(20, 160);
            this.txtObservaciones.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtObservaciones.MaxLength = 1000;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(1239, 67);
            this.txtObservaciones.TabIndex = 5;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(20, 138);
            this.lblObservaciones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(165, 16);
            this.lblObservaciones.TabIndex = 4;
            this.lblObservaciones.Text = "Observaciones (opcional):";
            // 
            // txtTratamiento
            // 
            this.txtTratamiento.Location = new System.Drawing.Point(647, 53);
            this.txtTratamiento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTratamiento.MaxLength = 500;
            this.txtTratamiento.Multiline = true;
            this.txtTratamiento.Name = "txtTratamiento";
            this.txtTratamiento.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTratamiento.Size = new System.Drawing.Size(612, 73);
            this.txtTratamiento.TabIndex = 3;
            // 
            // lblTratamiento
            // 
            this.lblTratamiento.AutoSize = true;
            this.lblTratamiento.Location = new System.Drawing.Point(647, 31);
            this.lblTratamiento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTratamiento.Name = "lblTratamiento";
            this.lblTratamiento.Size = new System.Drawing.Size(141, 16);
            this.lblTratamiento.TabIndex = 2;
            this.lblTratamiento.Text = "Tratamiento realizado:";
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.Location = new System.Drawing.Point(20, 53);
            this.txtDiagnostico.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDiagnostico.MaxLength = 500;
            this.txtDiagnostico.Multiline = true;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDiagnostico.Size = new System.Drawing.Size(599, 73);
            this.txtDiagnostico.TabIndex = 1;
            // 
            // lblDiagnostico
            // 
            this.lblDiagnostico.AutoSize = true;
            this.lblDiagnostico.Location = new System.Drawing.Point(20, 31);
            this.lblDiagnostico.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiagnostico.Name = "lblDiagnostico";
            this.lblDiagnostico.Size = new System.Drawing.Size(82, 16);
            this.lblDiagnostico.TabIndex = 0;
            this.lblDiagnostico.Text = "Diagnóstico:";
            // 
            // grpHistorial
            // 
            this.grpHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHistorial.Controls.Add(this.dgvHistorial);
            this.grpHistorial.Location = new System.Drawing.Point(16, 590);
            this.grpHistorial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpHistorial.Name = "grpHistorial";
            this.grpHistorial.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grpHistorial.Size = new System.Drawing.Size(1290, 117);
            this.grpHistorial.TabIndex = 3;
            this.grpHistorial.TabStop = false;
            this.grpHistorial.Text = "4. Historial del paciente (haga clic en un registro para corregirlo)";
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(13, 25);
            this.dgvHistorial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvHistorial.MultiSelect = false;
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.RowHeadersWidth = 51;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(1263, 80);
            this.dgvHistorial.TabIndex = 0;
            this.dgvHistorial.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistorial_CellClick);
            // 
            // FrmHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 721);
            this.Controls.Add(this.grpHistorial);
            this.Controls.Add(this.grpAtencion);
            this.Controls.Add(this.grpCitas);
            this.Controls.Add(this.grpPaciente);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmHistorial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::: HISTORIAL CLÍNICO :::";
            this.Load += new System.EventHandler(this.FrmHistorial_Load);
            this.grpPaciente.ResumeLayout(false);
            this.grpPaciente.PerformLayout();
            this.grpCitas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.grpAtencion.ResumeLayout(false);
            this.grpAtencion.PerformLayout();
            this.grpHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPaciente;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscarCi;
        private System.Windows.Forms.Label lblBuscarCi;
        private System.Windows.Forms.GroupBox grpCitas;
        private System.Windows.Forms.DataGridView dgvCitas;
        private System.Windows.Forms.GroupBox grpAtencion;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtTratamiento;
        private System.Windows.Forms.Label lblTratamiento;
        private System.Windows.Forms.TextBox txtDiagnostico;
        private System.Windows.Forms.Label lblDiagnostico;
        private System.Windows.Forms.GroupBox grpHistorial;
        private System.Windows.Forms.DataGridView dgvHistorial;
    }
}