namespace CpCarsal
{
    partial class FrmPago
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
            this.pnlCobro = new System.Windows.Forms.Panel();
            this.lblCobro = new System.Windows.Forms.Label();
            this.lblObligatorios = new System.Windows.Forms.Label();
            this.lblCita = new System.Windows.Forms.Label();
            this.cboCita = new System.Windows.Forms.ComboBox();
            this.lblAtencion = new System.Windows.Forms.Label();
            this.txtAtencion = new System.Windows.Forms.TextBox();
            this.lblServicio = new System.Windows.Forms.Label();
            this.cboServicio = new System.Windows.Forms.ComboBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblMetodo = new System.Windows.Forms.Label();
            this.cboMetodo = new System.Windows.Forms.ComboBox();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.pnlSeparador = new System.Windows.Forms.Panel();
            this.pnlLista = new System.Windows.Forms.Panel();
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            this.pnlListaCabecera = new System.Windows.Forms.Panel();
            this.lblLista = new System.Windows.Forms.Label();
            this.lblAyudaLista = new System.Windows.Forms.Label();
            this.lblTotalPagos = new System.Windows.Forms.Label();
            this.erpCampos = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlPaciente.SuspendLayout();
            this.pnlCobro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.pnlLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
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
            this.txtBuscarCi.BackColor = System.Drawing.SystemColors.Window;
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
            // pnlCobro
            // 
            this.pnlCobro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCobro.BackColor = System.Drawing.Color.White;
            this.pnlCobro.Controls.Add(this.lblCobro);
            this.pnlCobro.Controls.Add(this.lblObligatorios);
            this.pnlCobro.Controls.Add(this.lblCita);
            this.pnlCobro.Controls.Add(this.cboCita);
            this.pnlCobro.Controls.Add(this.lblAtencion);
            this.pnlCobro.Controls.Add(this.txtAtencion);
            this.pnlCobro.Controls.Add(this.lblServicio);
            this.pnlCobro.Controls.Add(this.cboServicio);
            this.pnlCobro.Controls.Add(this.lblPrecio);
            this.pnlCobro.Controls.Add(this.nudPrecio);
            this.pnlCobro.Controls.Add(this.lblCantidad);
            this.pnlCobro.Controls.Add(this.nudCantidad);
            this.pnlCobro.Controls.Add(this.btnAgregar);
            this.pnlCobro.Controls.Add(this.lblDetalle);
            this.pnlCobro.Controls.Add(this.dgvDetalle);
            this.pnlCobro.Controls.Add(this.btnQuitar);
            this.pnlCobro.Controls.Add(this.lblTotal);
            this.pnlCobro.Controls.Add(this.btnLimpiar);
            this.pnlCobro.Controls.Add(this.lblMetodo);
            this.pnlCobro.Controls.Add(this.cboMetodo);
            this.pnlCobro.Controls.Add(this.btnCobrar);
            this.pnlCobro.Controls.Add(this.pnlSeparador);
            this.pnlCobro.Location = new System.Drawing.Point(24, 106);
            this.pnlCobro.Name = "pnlCobro";
            this.pnlCobro.Size = new System.Drawing.Size(1192, 360);
            this.pnlCobro.TabIndex = 1;
            // 
            // lblCobro
            // 
            this.lblCobro.AutoSize = true;
            this.lblCobro.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCobro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCobro.Location = new System.Drawing.Point(20, 14);
            this.lblCobro.Name = "lblCobro";
            this.lblCobro.Size = new System.Drawing.Size(161, 20);
            this.lblCobro.TabIndex = 6;
            this.lblCobro.Text = "COBRO DE SERVICIOS";
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
            this.lblCita.Size = new System.Drawing.Size(87, 15);
            this.lblCita.TabIndex = 8;
            this.lblCita.Text = "Cita atendida *";
            // 
            // cboCita
            // 
            this.cboCita.BackColor = System.Drawing.SystemColors.Window;
            this.cboCita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCita.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCita.FormattingEnabled = true;
            this.cboCita.Location = new System.Drawing.Point(20, 84);
            this.cboCita.Name = "cboCita";
            this.cboCita.Size = new System.Drawing.Size(600, 27);
            this.cboCita.TabIndex = 9;
            this.cboCita.SelectedIndexChanged += new System.EventHandler(this.cboCita_SelectedIndexChanged);
            // 
            // lblAtencion
            // 
            this.lblAtencion.AutoSize = true;
            this.lblAtencion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtencion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblAtencion.Location = new System.Drawing.Point(20, 124);
            this.lblAtencion.Name = "lblAtencion";
            this.lblAtencion.Size = new System.Drawing.Size(192, 15);
            this.lblAtencion.TabIndex = 10;
            this.lblAtencion.Text = "Lo que se realizó (historial clínico)";
            // 
            // txtAtencion
            // 
            this.txtAtencion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.txtAtencion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAtencion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAtencion.Location = new System.Drawing.Point(20, 146);
            this.txtAtencion.Multiline = true;
            this.txtAtencion.Name = "txtAtencion";
            this.txtAtencion.ReadOnly = true;
            this.txtAtencion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAtencion.Size = new System.Drawing.Size(600, 80);
            this.txtAtencion.TabIndex = 11;
            // 
            // lblServicio
            // 
            this.lblServicio.AutoSize = true;
            this.lblServicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblServicio.Location = new System.Drawing.Point(20, 240);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(60, 15);
            this.lblServicio.TabIndex = 12;
            this.lblServicio.Text = "Servicio *";
            // 
            // cboServicio
            // 
            this.cboServicio.BackColor = System.Drawing.SystemColors.Window;
            this.cboServicio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServicio.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServicio.FormattingEnabled = true;
            this.cboServicio.Location = new System.Drawing.Point(20, 262);
            this.cboServicio.Name = "cboServicio";
            this.cboServicio.Size = new System.Drawing.Size(260, 27);
            this.cboServicio.TabIndex = 13;
            this.cboServicio.SelectedIndexChanged += new System.EventHandler(this.cboServicio_SelectedIndexChanged);
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblPrecio.Location = new System.Drawing.Point(295, 240);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(66, 15);
            this.lblPrecio.TabIndex = 14;
            this.lblPrecio.Text = "Precio Bs *";
            // 
            // nudPrecio
            // 
            this.nudPrecio.BackColor = System.Drawing.SystemColors.Window;
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudPrecio.Location = new System.Drawing.Point(295, 262);
            this.nudPrecio.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPrecio.Name = "nudPrecio";
            this.nudPrecio.Size = new System.Drawing.Size(120, 26);
            this.nudPrecio.TabIndex = 15;
            this.nudPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudPrecio.ThousandsSeparator = true;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblCantidad.Location = new System.Drawing.Point(430, 240);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(55, 15);
            this.lblCantidad.TabIndex = 16;
            this.lblCantidad.Text = "Cantidad";
            // 
            // nudCantidad
            // 
            this.nudCantidad.BackColor = System.Drawing.SystemColors.Window;
            this.nudCantidad.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCantidad.Location = new System.Drawing.Point(430, 262);
            this.nudCantidad.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(70, 26);
            this.nudCantidad.TabIndex = 17;
            this.nudCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudCantidad.ThousandsSeparator = true;
            this.nudCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAgregar.Location = new System.Drawing.Point(515, 259);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(105, 32);
            this.btnAgregar.TabIndex = 18;
            this.btnAgregar.Text = "AGREGAR";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblDetalle.Location = new System.Drawing.Point(650, 62);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(102, 15);
            this.lblDetalle.TabIndex = 19;
            this.lblDetalle.Text = "Detalle del cobro";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalle.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Location = new System.Drawing.Point(650, 84);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.Size = new System.Drawing.Size(522, 170);
            this.dgvDetalle.TabIndex = 20;
            // 
            // btnQuitar
            // 
            this.btnQuitar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnQuitar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuitar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnQuitar.Location = new System.Drawing.Point(650, 262);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(110, 30);
            this.btnQuitar.TabIndex = 21;
            this.btnQuitar.Text = "QUITAR";
            this.btnQuitar.UseVisualStyleBackColor = false;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTotal.Location = new System.Drawing.Point(950, 262);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(141, 25);
            this.lblTotal.TabIndex = 22;
            this.lblTotal.Text = "TOTAL: Bs 0.00";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(181)))), ((int)(((byte)(181)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLimpiar.Location = new System.Drawing.Point(20, 306);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(140, 36);
            this.btnLimpiar.TabIndex = 23;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblMetodo
            // 
            this.lblMetodo.AutoSize = true;
            this.lblMetodo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblMetodo.Location = new System.Drawing.Point(650, 315);
            this.lblMetodo.Name = "lblMetodo";
            this.lblMetodo.Size = new System.Drawing.Size(106, 15);
            this.lblMetodo.TabIndex = 24;
            this.lblMetodo.Text = "Método de pago *";
            // 
            // cboMetodo
            // 
            this.cboMetodo.BackColor = System.Drawing.SystemColors.Window;
            this.cboMetodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMetodo.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMetodo.FormattingEnabled = true;
            this.cboMetodo.Items.AddRange(new object[] {
            "Efectivo",
            "QR"});
            this.cboMetodo.Location = new System.Drawing.Point(780, 311);
            this.cboMetodo.Name = "cboMetodo";
            this.cboMetodo.Size = new System.Drawing.Size(150, 27);
            this.cboMetodo.TabIndex = 25;
            // 
            // btnCobrar
            // 
            this.btnCobrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCobrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCobrar.FlatAppearance.BorderSize = 0;
            this.btnCobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCobrar.Location = new System.Drawing.Point(952, 304);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(220, 40);
            this.btnCobrar.TabIndex = 26;
            this.btnCobrar.Text = "REGISTRAR COBRO";
            this.btnCobrar.UseVisualStyleBackColor = false;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
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
            this.pnlLista.Controls.Add(this.dgvPagos);
            this.pnlLista.Controls.Add(this.pnlListaCabecera);
            this.pnlLista.Controls.Add(this.lblTotalPagos);
            this.pnlLista.Location = new System.Drawing.Point(24, 482);
            this.pnlLista.Name = "pnlLista";
            this.pnlLista.Size = new System.Drawing.Size(1192, 218);
            this.pnlLista.TabIndex = 2;
            // 
            // dgvPagos
            // 
            this.dgvPagos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPagos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPagos.Location = new System.Drawing.Point(0, 52);
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.Size = new System.Drawing.Size(1192, 134);
            this.dgvPagos.TabIndex = 29;
            // 
            // pnlListaCabecera
            // 
            this.pnlListaCabecera.Controls.Add(this.lblLista);
            this.pnlListaCabecera.Controls.Add(this.lblAyudaLista);
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
            this.lblLista.Size = new System.Drawing.Size(154, 20);
            this.lblLista.TabIndex = 27;
            this.lblLista.Text = "PAGOS REALIZADOS";
            // 
            // lblAyudaLista
            // 
            this.lblAyudaLista.AutoSize = true;
            this.lblAyudaLista.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyudaLista.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblAyudaLista.Location = new System.Drawing.Point(200, 19);
            this.lblAyudaLista.Name = "lblAyudaLista";
            this.lblAyudaLista.Size = new System.Drawing.Size(227, 15);
            this.lblAyudaLista.TabIndex = 28;
            this.lblAyudaLista.Text = "(doble clic en un pago para ver su detalle)";
            // 
            // lblTotalPagos
            // 
            this.lblTotalPagos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalPagos.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPagos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.lblTotalPagos.Location = new System.Drawing.Point(0, 186);
            this.lblTotalPagos.Name = "lblTotalPagos";
            this.lblTotalPagos.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblTotalPagos.Size = new System.Drawing.Size(1192, 32);
            this.lblTotalPagos.TabIndex = 30;
            this.lblTotalPagos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // erpCampos
            // 
            this.erpCampos.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.erpCampos.ContainerControl = this;
            // 
            // FrmPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(1240, 720);
            this.Controls.Add(this.pnlLista);
            this.Controls.Add(this.pnlCobro);
            this.Controls.Add(this.pnlPaciente);
            this.Name = "FrmPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cobro de servicios";
            this.Load += new System.EventHandler(this.FrmPago_Load);
            this.pnlPaciente.ResumeLayout(false);
            this.pnlPaciente.PerformLayout();
            this.pnlCobro.ResumeLayout(false);
            this.pnlCobro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.pnlLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            this.pnlListaCabecera.ResumeLayout(false);
            this.pnlListaCabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpCampos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPaciente;
        private System.Windows.Forms.Panel pnlCobro;
        private System.Windows.Forms.Panel pnlSeparador;
        private System.Windows.Forms.Panel pnlLista;
        private System.Windows.Forms.Panel pnlListaCabecera;
        private System.Windows.Forms.Label lblTituloPaciente;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscarCi;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label lblCobro;
        private System.Windows.Forms.Label lblObligatorios;
        private System.Windows.Forms.Label lblCita;
        private System.Windows.Forms.ComboBox cboCita;
        private System.Windows.Forms.Label lblAtencion;
        private System.Windows.Forms.TextBox txtAtencion;
        private System.Windows.Forms.Label lblServicio;
        private System.Windows.Forms.ComboBox cboServicio;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblMetodo;
        private System.Windows.Forms.ComboBox cboMetodo;
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Label lblLista;
        private System.Windows.Forms.Label lblAyudaLista;
        private System.Windows.Forms.DataGridView dgvPagos;
        private System.Windows.Forms.Label lblTotalPagos;
        private System.Windows.Forms.ErrorProvider erpCampos;
    }
}