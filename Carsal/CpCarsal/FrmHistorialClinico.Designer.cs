namespace CpCarsal
{
    partial class FrmHistorialClinico
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
            this.components = new System.ComponentModel.Container();
            this.pnlPaciente = new System.Windows.Forms.Panel();
            this.lblTituloPaciente = new System.Windows.Forms.Label();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscarCi = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.pnlAtencion = new System.Windows.Forms.Panel();
            this.lblAtencion = new System.Windows.Forms.Label();
            this.lblObligatorios = new System.Windows.Forms.Label();
            this.lblCita = new System.Windows.Forms.Label();
            this.cboCita = new System.Windows.Forms.ComboBox();
            this.lblDiagnostico = new System.Windows.Forms.Label();
            this.txtDiagnostico = new System.Windows.Forms.TextBox();
            this.lblTratamiento = new System.Windows.Forms.Label();
            this.txtTratamiento = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlSeparador = new System.Windows.Forms.Panel();
            this.pnlLista = new System.Windows.Forms.Panel();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.pnlListaCabecera = new System.Windows.Forms.Panel();
            this.lblLista = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.erpCampos = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblFiltro = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlPaciente.SuspendLayout();
            this.pnlAtencion.SuspendLayout();
            this.pnlLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.pnlListaCabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpCampos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPaciente
            // 
            this.pnlPaciente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPaciente.BackColor = System.Drawing.Color.White;
            this.pnlPaciente.Controls.Add(this.lblTituloPaciente);
            this.pnlPaciente.Controls.Add(this.lblBuscar);
            this.pnlPaciente.Controls.Add(this.txtBuscarCi);
            this.pnlPaciente.Controls.Add(this.btnBuscar);
            this.pnlPaciente.Controls.Add(this.lblPaciente);
            this.pnlPaciente.Location = new System.Drawing.Point(24, 20);
            this.pnlPaciente.Name = "pnlPaciente";
            this.pnlPaciente.Size = new System.Drawing.Size(1192, 70);
            this.pnlPaciente.TabIndex = 0;
            // 
            // lblTituloPaciente
            // 
            this.lblTituloPaciente.AutoSize = true;
            this.lblTituloPaciente.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloPaciente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTituloPaciente.Location = new System.Drawing.Point(20, 24);
            this.lblTituloPaciente.Name = "lblTituloPaciente";
            this.lblTituloPaciente.Size = new System.Drawing.Size(79, 20);
            this.lblTituloPaciente.TabIndex = 1;
            this.lblTituloPaciente.Text = "PACIENTE";
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblBuscar.Location = new System.Drawing.Point(130, 27);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(24, 17);
            this.lblBuscar.TabIndex = 2;
            this.lblBuscar.Text = "CI:";
            // 
            // txtBuscarCi
            // 
            this.txtBuscarCi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarCi.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarCi.Location = new System.Drawing.Point(160, 23);
            this.txtBuscarCi.MaxLength = 20;
            this.txtBuscarCi.Name = "txtBuscarCi";
            this.txtBuscarCi.Size = new System.Drawing.Size(200, 26);
            this.txtBuscarCi.TabIndex = 3;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBuscar.Location = new System.Drawing.Point(370, 21);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 30);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaciente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblPaciente.Location = new System.Drawing.Point(490, 26);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(217, 20);
            this.lblPaciente.TabIndex = 5;
            this.lblPaciente.Text = "Ningún paciente seleccionado";
            // 
            // pnlAtencion
            // 
            this.pnlAtencion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAtencion.BackColor = System.Drawing.Color.White;
            this.pnlAtencion.Controls.Add(this.lblAtencion);
            this.pnlAtencion.Controls.Add(this.lblObligatorios);
            this.pnlAtencion.Controls.Add(this.lblCita);
            this.pnlAtencion.Controls.Add(this.cboCita);
            this.pnlAtencion.Controls.Add(this.lblDiagnostico);
            this.pnlAtencion.Controls.Add(this.txtDiagnostico);
            this.pnlAtencion.Controls.Add(this.lblTratamiento);
            this.pnlAtencion.Controls.Add(this.txtTratamiento);
            this.pnlAtencion.Controls.Add(this.lblObservaciones);
            this.pnlAtencion.Controls.Add(this.txtObservaciones);
            this.pnlAtencion.Controls.Add(this.btnRegistrar);
            this.pnlAtencion.Controls.Add(this.btnModificar);
            this.pnlAtencion.Controls.Add(this.btnCancelar);
            this.pnlAtencion.Controls.Add(this.pnlSeparador);
            this.pnlAtencion.Location = new System.Drawing.Point(24, 106);
            this.pnlAtencion.Name = "pnlAtencion";
            this.pnlAtencion.Size = new System.Drawing.Size(1192, 290);
            this.pnlAtencion.TabIndex = 1;
            // 
            // lblAtencion
            // 
            this.lblAtencion.AutoSize = true;
            this.lblAtencion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtencion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAtencion.Location = new System.Drawing.Point(20, 14);
            this.lblAtencion.Name = "lblAtencion";
            this.lblAtencion.Size = new System.Drawing.Size(184, 20);
            this.lblAtencion.TabIndex = 6;
            this.lblAtencion.Text = "REGISTRO DE ATENCIÓN";
            // 
            // lblObligatorios
            // 
            this.lblObligatorios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObligatorios.AutoSize = true;
            this.lblObligatorios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObligatorios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblObligatorios.Location = new System.Drawing.Point(1040, 18);
            this.lblObligatorios.Name = "lblObligatorios";
            this.lblObligatorios.Size = new System.Drawing.Size(125, 15);
            this.lblObligatorios.TabIndex = 7;
            this.lblObligatorios.Text = "* Campos obligatorios";
            // 
            // lblCita
            // 
            this.lblCita.AutoSize = true;
            this.lblCita.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCita.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCita.Location = new System.Drawing.Point(20, 62);
            this.lblCita.Name = "lblCita";
            this.lblCita.Size = new System.Drawing.Size(28, 15);
            this.lblCita.TabIndex = 8;
            this.lblCita.Text = "Cita";
            // 
            // cboCita
            // 
            this.cboCita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCita.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCita.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCita.FormattingEnabled = true;
            this.cboCita.Location = new System.Drawing.Point(20, 84);
            this.cboCita.Name = "cboCita";
            this.cboCita.Size = new System.Drawing.Size(430, 27);
            this.cboCita.TabIndex = 9;
            // 
            // lblDiagnostico
            // 
            this.lblDiagnostico.AutoSize = true;
            this.lblDiagnostico.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnostico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblDiagnostico.Location = new System.Drawing.Point(20, 124);
            this.lblDiagnostico.Name = "lblDiagnostico";
            this.lblDiagnostico.Size = new System.Drawing.Size(80, 15);
            this.lblDiagnostico.TabIndex = 10;
            this.lblDiagnostico.Text = "Diagnóstico *";
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiagnostico.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnostico.Location = new System.Drawing.Point(20, 146);
            this.txtDiagnostico.MaxLength = 500;
            this.txtDiagnostico.Multiline = true;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDiagnostico.Size = new System.Drawing.Size(430, 120);
            this.txtDiagnostico.TabIndex = 11;
            // 
            // lblTratamiento
            // 
            this.lblTratamiento.AutoSize = true;
            this.lblTratamiento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTratamiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblTratamiento.Location = new System.Drawing.Point(480, 62);
            this.lblTratamiento.Name = "lblTratamiento";
            this.lblTratamiento.Size = new System.Drawing.Size(83, 15);
            this.lblTratamiento.TabIndex = 12;
            this.lblTratamiento.Text = "Tratamiento *";
            // 
            // txtTratamiento
            // 
            this.txtTratamiento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTratamiento.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTratamiento.Location = new System.Drawing.Point(480, 84);
            this.txtTratamiento.MaxLength = 500;
            this.txtTratamiento.Multiline = true;
            this.txtTratamiento.Name = "txtTratamiento";
            this.txtTratamiento.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTratamiento.Size = new System.Drawing.Size(430, 76);
            this.txtTratamiento.TabIndex = 13;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObservaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblObservaciones.Location = new System.Drawing.Point(480, 170);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(88, 15);
            this.lblObservaciones.TabIndex = 14;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObservaciones.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.Location = new System.Drawing.Point(480, 192);
            this.txtObservaciones.MaxLength = 1000;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(430, 74);
            this.txtObservaciones.TabIndex = 15;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.FlatAppearance.BorderSize = 0;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRegistrar.Location = new System.Drawing.Point(952, 62);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(220, 40);
            this.btnRegistrar.TabIndex = 16;
            this.btnRegistrar.Text = "REGISTRAR";
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(99)))), ((int)(((byte)(168)))));
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.FlatAppearance.BorderSize = 0;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnModificar.Location = new System.Drawing.Point(952, 110);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(220, 40);
            this.btnModificar.TabIndex = 17;
            this.btnModificar.Text = "MODIFICAR";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(181)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelar.Location = new System.Drawing.Point(952, 158);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(220, 40);
            this.btnCancelar.TabIndex = 18;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // pnlSeparador
            // 
            this.pnlSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSeparador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlSeparador.Location = new System.Drawing.Point(0, 46);
            this.pnlSeparador.Name = "pnlSeparador";
            this.pnlSeparador.Size = new System.Drawing.Size(1192, 1);
            this.pnlSeparador.TabIndex = 99;
            // 
            // pnlLista
            // 
            this.pnlLista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLista.BackColor = System.Drawing.Color.White;
            this.pnlLista.Controls.Add(this.dgvHistorial);
            this.pnlLista.Controls.Add(this.pnlListaCabecera);
            this.pnlLista.Controls.Add(this.lblTotal);
            this.pnlLista.Location = new System.Drawing.Point(24, 412);
            this.pnlLista.Name = "pnlLista";
            this.pnlLista.Size = new System.Drawing.Size(1192, 288);
            this.pnlLista.TabIndex = 2;
            // 
            // dgvHistorial
            // 
            this.dgvHistorial.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistorial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistorial.Location = new System.Drawing.Point(0, 52);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.Size = new System.Drawing.Size(1192, 204);
            this.dgvHistorial.TabIndex = 1;
            // 
            // pnlListaCabecera
            // 
            this.pnlListaCabecera.Controls.Add(this.button1);
            this.pnlListaCabecera.Controls.Add(this.txtFiltro);
            this.pnlListaCabecera.Controls.Add(this.lblFiltro);
            this.pnlListaCabecera.Controls.Add(this.lblLista);
            this.pnlListaCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlListaCabecera.Location = new System.Drawing.Point(0, 0);
            this.pnlListaCabecera.Name = "pnlListaCabecera";
            this.pnlListaCabecera.Size = new System.Drawing.Size(1192, 52);
            this.pnlListaCabecera.TabIndex = 0;
            // 
            // lblLista
            // 
            this.lblLista.AutoSize = true;
            this.lblLista.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLista.Location = new System.Drawing.Point(20, 16);
            this.lblLista.Name = "lblLista";
            this.lblLista.Size = new System.Drawing.Size(191, 20);
            this.lblLista.TabIndex = 19;
            this.lblLista.Text = "HISTORIAL DEL PACIENTE";
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblTotal.Location = new System.Drawing.Point(0, 256);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblTotal.Size = new System.Drawing.Size(1192, 32);
            this.lblTotal.TabIndex = 20;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // erpCampos
            // 
            this.erpCampos.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.erpCampos.ContainerControl = this;
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblFiltro.Location = new System.Drawing.Point(462, 22);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(124, 15);
            this.lblFiltro.TabIndex = 20;
            this.lblFiltro.Text = "Buscar en el historial:";
            // 
            // txtFiltro
            // 
            this.txtFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFiltro.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtFiltro.Location = new System.Drawing.Point(604, 16);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(200, 26);
            this.txtFiltro.TabIndex = 21;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(824, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // FrmHistorialClinico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1240, 720);
            this.Controls.Add(this.pnlLista);
            this.Controls.Add(this.pnlAtencion);
            this.Controls.Add(this.pnlPaciente);
            this.Name = "FrmHistorialClinico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial Clínico";
            this.Load += new System.EventHandler(this.FrmHistorialClinico_Load);
            this.pnlPaciente.ResumeLayout(false);
            this.pnlPaciente.PerformLayout();
            this.pnlAtencion.ResumeLayout(false);
            this.pnlAtencion.PerformLayout();
            this.pnlLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.pnlListaCabecera.ResumeLayout(false);
            this.pnlListaCabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpCampos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPaciente;
        private System.Windows.Forms.Panel pnlAtencion;
        private System.Windows.Forms.Panel pnlSeparador;
        private System.Windows.Forms.Panel pnlLista;
        private System.Windows.Forms.Panel pnlListaCabecera;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Label lblTituloPaciente;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscarCi;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label lblAtencion;
        private System.Windows.Forms.Label lblObligatorios;
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
        private System.Windows.Forms.Label lblLista;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.ErrorProvider erpCampos;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Button button1;
    }
}