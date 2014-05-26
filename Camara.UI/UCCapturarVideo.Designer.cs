namespace Krisa.Camara.UI
{
    partial class UCCapturarVideo
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
            this.components = new System.ComponentModel.Container();
            this.pcbVideo = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pcbVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbVideo
            // 
            this.pcbVideo.Location = new System.Drawing.Point(3, 3);
            this.pcbVideo.Name = "pcbVideo";
            this.pcbVideo.Size = new System.Drawing.Size(640, 480);
            this.pcbVideo.TabIndex = 0;
            this.pcbVideo.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.actualizarVideoFrame);
            // 
            // UCCapturarVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcbVideo);
            this.Name = "UCCapturarVideo";
            this.Size = new System.Drawing.Size(645, 492);
            this.Load += new System.EventHandler(this.UCCapturarVideo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbVideo;
        private System.Windows.Forms.Timer timer1;
    }
}
