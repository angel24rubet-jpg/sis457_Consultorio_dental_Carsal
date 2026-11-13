namespace CpCarsal
{
    partial class FrmHistorialClinico
    {
        private System.ComponentModel.IContainer components = null;

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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbPaciente = new System.Windows.Forms.GroupBox();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscarCi = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.gbAtencion = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtTratamiento = new System.Windows.Forms.TextBox();
            this.lblTratamiento = new System.Windows.Forms.Label();
            this.txtDiagnostico = new System.Windows.Forms.TextBox();
            this.lblDiagnostico = new System.Windows.Forms.Label();
            this.cboCita = new System.Windows.Forms.ComboBox();
            this.lblCita = new System.Windows.Forms.Label();
            this.lblListado = new System.Windows.Forms.Label();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.gbPaciente.SuspendLayout();
            this.gbAtencion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(185, 20);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "HISTORIAL CLÍNICO";
            // 
            // gbPaciente
            // 
            this.gbPaciente.Controls.Add(this.lblPaciente);
            this.gbPaciente.Controls.Add(this.btnBuscar);
            this.gbPaciente.Controls.Add(this.txtBuscarCi);
            this.gbPaciente.Controls.Add(this.lblBuscar);
            this.gbPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPaciente.Location = new System.Drawing.Point(20, 50);
            this.gbPaciente.Name = "gbPaciente";
            this.gbPaciente.Size = new System.Drawing.Size(1094, 75);
            this.gbPaciente.TabIndex = 1;
            this.gbPaciente.TabStop = false;
            this.gbPaciente.Text = "Paciente";
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(15, 33);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(30, 16);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "CI:";
            // 
            // txtBuscarCi
            // 
            this.txtBuscarCi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarCi.Location = new System.Drawing.Point(55, 30);
            this.txtBuscarCi.MaxLength = 20;
            this.txtBuscarCi.Name = "txtBuscarCi";
            this.txtBuscarCi.Size = new System.Drawing.Size(160, 22);
            this.txtBuscarCi.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(225, 27);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 28);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblPaciente.Location = new System.Drawing.Point(345, 33);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(210, 16);
            this.lblPaciente.TabIndex = 3;
            this.lblPaciente.Text = "Ningún paciente seleccionado";
            // 
            // gbAtencion
            // 
            this.gbAtencion.Controls.Add(this.btnCancelar);
            this.gbAtencion.Controls.Add(this.btnModificar);
            this.gbAtencion.Controls.Add(this.btnRegistrar);
            this.gbAtencion.Controls.Add(this.txtObservaciones);
            this.gbAtencion.Controls.Add(this.lblObservaciones);
            this.gbAtencion.Controls.Add(this.txtTratamiento);
            this.gbAtencion.Controls.Add(this.lblTratamiento);
            this.gbAtencion.Controls.Add(this.txtDiagnostico);
            this.gbAtencion.Controls.Add(this.lblDiagnostico);
            this.gbAtencion.Controls.Add(this.cboCita);
            this.gbAtencion.Controls.Add(this.lblCita);
            this.gbAtencion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAtencion.Location = new System.Drawing.Point(20, 135);
            this.gbAtencion.Name = "gbAtencion";
            this.gbAtencion.Size = new System.Drawing.Size(1094, 245);
            this.gbAtencion.TabIndex = 2;
            this.gbAtencion.TabStop = false;
            this.gbAtencion.Text = "Registro de atención";
            // 
            // lblCita
            // 
            this.lblCita.AutoSize = true;
            this.lblCita.Location = new System.Drawing.Point(15, 32);
            this.lblCita.Name = "lblCita";
            this.lblCita.Size = new System.Drawing.Size(40, 16);
            this.lblCita.TabIndex = 0;
            this.lblCita.Text = "Cita:";
            // 
            // cboCita
            // 
            this.cboCita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCita.FormattingEnabled = true;
            this.cboCita.Location = new System.Drawing.Point(130, 29);
            this.cboCita.Name = "cboCita";
            this.cboCita.Size = new System.Drawing.Size(420, 23);
            this.cboCita.TabIndex = 1;
            // 
            // lblDiagnostico
            // 
            this.lblDiagnostico.AutoSize = true;
            this.lblDiagnostico.Location = new System.Drawing.Point(15, 68);
            this.lblDiagnostico.Name = "lblDiagnostico";
            this.lblDiagnostico.Size = new System.Drawing.Size(96, 16);
            this.lblDiagnostico.TabIndex = 2;
            this.lblDiagnostico.Text = "Diagnóstico:";
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnostico.Location = new System.Drawing.Point(130, 65);
            this.txtDiagnostico.MaxLength = 500;
            this.txtDiagnostico.Multiline = true;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDiagnostico.Size = new System.Drawing.Size(420, 75);
            this.txtDiagnostico.TabIndex = 3;
            // 
            // lblTratamiento
            // 
            this.lblTratamiento.AutoSize = true;
            this.lblTratamiento.Location = new System.Drawing.Point(15, 155);
            this.lblTratamiento.Name = "lblTratamiento";
            this.lblTratamiento.Size = new System.Drawing.Size(97, 16);
            this.lblTratamiento.TabIndex = 4;
            this.lblTratamiento.Text = "Tratamiento:";
            // 
            // txtTratamiento
            // 
            this.txtTratamiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTratamiento.Location = new System.Drawing.Point(130, 152);
            this.txtTratamiento.MaxLength = 500;
            this.txtTratamiento.Multiline = true;
            this.txtTratamiento.Name = "txtTratamiento";
            this.txtTratamiento.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTratamiento.Size = new System.Drawing.Size(420, 75);
            this.txtTratamiento.TabIndex = 5;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(575, 32);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(118, 16);
            this.lblObservaciones.TabIndex = 6;
            this.lblObservaciones.Text = "Observaciones:";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.Location = new System.Drawing.Point(575, 55);
            this.txtObservaciones.MaxLength = 1000;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(500, 120);
            this.txtObservaciones.TabIndex = 7;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(575, 190);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(130, 35);
            this.btnRegistrar.TabIndex = 8;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(715, 190);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(150, 35);
            this.btnModificar.TabIndex = 9;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(875, 190);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(130, 35);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblListado
            // 
            this.lblListado.AutoSize = true;
            this.lblListado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListado.Location = new System.Drawing.Point(20, 392);
            this.lblListado.Name = "lblListado";
            this.lblListado.Size = new System.Drawing.Size(180, 16);
            this.lblListado.TabIndex = 3;
            this.lblListado.Text = "Atenciones registradas";
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Location = new System.Drawing.Point(20, 415);
            this.dgvHistorial.MultiSelect = false;
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.RowHeadersVisible = false;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(1094, 300);
            this.dgvHistorial.TabIndex = 4;
            // 
            // FrmHistorialClinico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 735);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.lblListado);
            this.Controls.Add(this.gbAtencion);
            this.Controls.Add(this.gbPaciente);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FrmHistorialClinico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial Clínico";
            this.Load += new System.EventHandler(this.FrmHistorialClinico_Load);
            this.gbPaciente.ResumeLayout(false);
            this.gbPaciente.PerformLayout();
            this.gbAtencion.ResumeLayout(false);
            this.gbAtencion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbPaciente;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscarCi;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.GroupBox gbAtencion;
        private System.Windows.Forms.Label lblCita;
        private System.Windows.Forms.ComboBox cboCita;
        private System.Windows.Forms.Label lblDiagnostico;
        private System.Windows.Forms.TextBox txtDiagnostico;
        private System.Windows.Forms.Label lblTratamiento;
        private System.Windows.Forms.TextBox txtTratamiento;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblListado;
        private System.Windows.Forms.DataGridView dgvHistorial;
    }
}