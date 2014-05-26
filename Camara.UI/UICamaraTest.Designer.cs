namespace Krisa.Camara.UI
{
    partial class UICamaraTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ucSeleccionarCamara1 = new Krisa.Camara.UI.UCSeleccionarCamara();
            this.ucCapturarVideo1 = new Krisa.Camara.UI.UCCapturarVideo();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(396, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Grabar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(491, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Detener";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ucSeleccionarCamara1
            // 
            this.ucSeleccionarCamara1.CamaraSeleccionada = null;
            this.ucSeleccionarCamara1.Location = new System.Drawing.Point(12, 12);
            this.ucSeleccionarCamara1.Name = "ucSeleccionarCamara1";
            this.ucSeleccionarCamara1.Size = new System.Drawing.Size(327, 51);
            this.ucSeleccionarCamara1.TabIndex = 2;
            // 
            // ucCapturarVideo1
            // 
            this.ucCapturarVideo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucCapturarVideo1.camara = null;
            this.ucCapturarVideo1.iniciarVideo = false;
            this.ucCapturarVideo1.Location = new System.Drawing.Point(12, 54);
            this.ucCapturarVideo1.Name = "ucCapturarVideo1";
            this.ucCapturarVideo1.Size = new System.Drawing.Size(681, 324);
            this.ucCapturarVideo1.TabIndex = 3;
            // 
            // UICamaraTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 500);
            this.Controls.Add(this.ucCapturarVideo1);
            this.Controls.Add(this.ucSeleccionarCamara1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "UICamaraTest";
            this.Text = "UICamaraTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private UCSeleccionarCamara ucSeleccionarCamara1;
        private UCCapturarVideo ucCapturarVideo1;
    }
}