namespace CpCarsal
{
    partial class FrmEspecialidad
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRegistro = new System.Windows.Forms.Panel();
            this.lblNombreEspecialidad = new System.Windows.Forms.Label();
            this.txtNombreEspecialidad = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlLista = new System.Windows.Forms.Panel();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.pnlRegistro.SuspendLayout();
            this.pnlLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(1, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(414, 84);
            this.panel2.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Oswald", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 59);
            this.label1.TabIndex = 25;
            this.label1.Text = "GESTION DE USUARIOS";
            // 
            // pnlRegistro
            // 
            this.pnlRegistro.BackColor = System.Drawing.Color.White;
            this.pnlRegistro.Controls.Add(this.btnCancelar);
            this.pnlRegistro.Controls.Add(this.btnRegistrar);
            this.pnlRegistro.Controls.Add(this.txtDescripcion);
            this.pnlRegistro.Controls.Add(this.lblDescripcion);
            this.pnlRegistro.Controls.Add(this.txtNombreEspecialidad);
            this.pnlRegistro.Controls.Add(this.lblNombreEspecialidad);
            this.pnlRegistro.Location = new System.Drawing.Point(270, 175);
            this.pnlRegistro.Name = "pnlRegistro";
            this.pnlRegistro.Size = new System.Drawing.Size(724, 238);
            this.pnlRegistro.TabIndex = 59;
            // 
            // lblNombreEspecialidad
            // 
            this.lblNombreEspecialidad.AutoSize = true;
            this.lblNombreEspecialidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreEspecialidad.Location = new System.Drawing.Point(64, 36);
            this.lblNombreEspecialidad.Name = "lblNombreEspecialidad";
            this.lblNombreEspecialidad.Size = new System.Drawing.Size(181, 21);
            this.lblNombreEspecialidad.TabIndex = 2;
            this.lblNombreEspecialidad.Text = "Nombre de Especialidad:";
            // 
            // txtNombreEspecialidad
            // 
            this.txtNombreEspecialidad.Location = new System.Drawing.Point(274, 39);
            this.txtNombreEspecialidad.Name = "txtNombreEspecialidad";
            this.txtNombreEspecialidad.Size = new System.Drawing.Size(239, 20);
            this.txtNombreEspecialidad.TabIndex = 4;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Location = new System.Drawing.Point(111, 99);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(94, 21);
            this.lblDescripcion.TabIndex = 5;
            this.lblDescripcion.Text = "Descripcion:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(274, 102);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(239, 20);
            this.txtDescripcion.TabIndex = 6;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.Teal;
            this.btnRegistrar.FlatAppearance.BorderSize = 0;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.Location = new System.Drawing.Point(141, 171);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(125, 37);
            this.btnRegistrar.TabIndex = 10;
            this.btnRegistrar.Text = "REGISTRAR";
            this.btnRegistrar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Location = new System.Drawing.Point(388, 171);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(125, 37);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // pnlLista
            // 
            this.pnlLista.BackColor = System.Drawing.Color.White;
            this.pnlLista.Controls.Add(this.btnEliminar);
            this.pnlLista.Controls.Add(this.btnModificar);
            this.pnlLista.Controls.Add(this.dgvLista);
            this.pnlLista.Location = new System.Drawing.Point(199, 441);
            this.pnlLista.Name = "pnlLista";
            this.pnlLista.Size = new System.Drawing.Size(861, 345);
            this.pnlLista.TabIndex = 60;
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.Location = new System.Drawing.Point(52, 25);
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.Size = new System.Drawing.Size(765, 237);
            this.dgvLista.TabIndex = 1;
            // 
            // btnModificar
            // 
            this.btnModificar.BackColor = System.Drawing.Color.Navy;
            this.btnModificar.FlatAppearance.BorderSize = 0;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificar.ForeColor = System.Drawing.Color.White;
            this.btnModificar.Location = new System.Drawing.Point(247, 285);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(125, 37);
            this.btnModificar.TabIndex = 11;
            this.btnModificar.Text = "MODIFICAR";
            this.btnModificar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(481, 285);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(125, 37);
            this.btnEliminar.TabIndex = 12;
            this.btnEliminar.Text = "ELIMINAR";
            this.btnEliminar.UseVisualStyleBackColor = false;
            // 
            // FrmEspecialidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 810);
            this.Controls.Add(this.pnlLista);
            this.Controls.Add(this.pnlRegistro);
            this.Controls.Add(this.panel2);
            this.Name = "FrmEspecialidad";
            this.Text = "FrmEspecialidad";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlRegistro.ResumeLayout(false);
            this.pnlRegistro.PerformLayout();
            this.pnlLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlRegistro;
        private System.Windows.Forms.Label lblNombreEspecialidad;
        private System.Windows.Forms.TextBox txtNombreEspecialidad;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel pnlLista;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
    }
}