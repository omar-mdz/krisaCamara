namespace Krisa.Tarjeta.UI
{
    partial class UIAgregarTarjeta
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIAgregarTarjeta));
            this.pcbTarjeta = new System.Windows.Forms.PictureBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cmbDireccion = new System.Windows.Forms.ComboBox();
            this.errorAgregarTarjeta = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcbTarjeta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAgregarTarjeta)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbTarjeta
            // 
            this.pcbTarjeta.Image = ((System.Drawing.Image)(resources.GetObject("pcbTarjeta.Image")));
            this.pcbTarjeta.Location = new System.Drawing.Point(12, 12);
            this.pcbTarjeta.Name = "pcbTarjeta";
            this.pcbTarjeta.Size = new System.Drawing.Size(111, 115);
            this.pcbTarjeta.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbTarjeta.TabIndex = 0;
            this.pcbTarjeta.TabStop = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnAceptar.Location = new System.Drawing.Point(133, 133);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(85, 36);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnCancelar.Location = new System.Drawing.Point(302, 133);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(85, 36);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(130, 12);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(68, 17);
            this.lblNombre.TabIndex = 9;
            this.lblNombre.Text = "*Nombre:";
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.Location = new System.Drawing.Point(130, 72);
            this.lblDireccion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(76, 17);
            this.lblDireccion.TabIndex = 10;
            this.lblDireccion.Text = "*Dirección:";
            // 
            // txtNombre
            // 
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(133, 33);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(254, 25);
            this.txtNombre.TabIndex = 7;
            this.txtNombre.Validating += new System.ComponentModel.CancelEventHandler(this.txtNombre_Validating);
            // 
            // cmbDireccion
            // 
            this.cmbDireccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDireccion.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbDireccion.FormattingEnabled = true;
            this.cmbDireccion.Location = new System.Drawing.Point(133, 92);
            this.cmbDireccion.Name = "cmbDireccion";
            this.cmbDireccion.Size = new System.Drawing.Size(254, 25);
            this.cmbDireccion.TabIndex = 11;
            // 
            // errorAgregarTarjeta
            // 
            this.errorAgregarTarjeta.ContainerControl = this;
            // 
            // UIAgregarTarjeta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(411, 183);
            this.Controls.Add(this.cmbDireccion);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.pcbTarjeta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIAgregarTarjeta";
            this.Text = "Agregar Tarjeta";
            this.Load += new System.EventHandler(this.UIAgregarTarjeta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbTarjeta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAgregarTarjeta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbTarjeta;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        internal System.Windows.Forms.Label lblNombre;
        internal System.Windows.Forms.Label lblDireccion;
        internal System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ComboBox cmbDireccion;
        private System.Windows.Forms.ErrorProvider errorAgregarTarjeta;

    }
}