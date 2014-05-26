namespace Krisa.Camara.UI
{
    partial class UCSeleccionarCamara
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbCamaras = new System.Windows.Forms.ComboBox();
            this.lblSeleccionarCamara = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbCamaras
            // 
            this.cmbCamaras.FormattingEnabled = true;
            this.cmbCamaras.Location = new System.Drawing.Point(126, 16);
            this.cmbCamaras.Name = "cmbCamaras";
            this.cmbCamaras.Size = new System.Drawing.Size(191, 21);
            this.cmbCamaras.TabIndex = 0;
            this.cmbCamaras.SelectedIndexChanged += new System.EventHandler(this.cmbCamaras_SelectedIndexChanged);
            // 
            // lblSeleccionarCamara
            // 
            this.lblSeleccionarCamara.AutoSize = true;
            this.lblSeleccionarCamara.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeleccionarCamara.Location = new System.Drawing.Point(8, 19);
            this.lblSeleccionarCamara.Name = "lblSeleccionarCamara";
            this.lblSeleccionarCamara.Size = new System.Drawing.Size(107, 13);
            this.lblSeleccionarCamara.TabIndex = 1;
            this.lblSeleccionarCamara.Text = "Seleccionar Cámara";
            // 
            // UCSeleccionarCamara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSeleccionarCamara);
            this.Controls.Add(this.cmbCamaras);
            this.Name = "UCSeleccionarCamara";
            this.Size = new System.Drawing.Size(327, 51);
            this.Load += new System.EventHandler(this.UCSeleccionarCamara_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCamaras;
        private System.Windows.Forms.Label lblSeleccionarCamara;
    }
}
