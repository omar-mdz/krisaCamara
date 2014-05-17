namespace Krisa.Tarjeta.Test
{
    partial class MainDialog
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
            this.lblTarjeta = new System.Windows.Forms.Label();
            this.cmbTarjeta = new System.Windows.Forms.ComboBox();
            this.chkEmulador = new System.Windows.Forms.CheckBox();
            this.lblPuertos = new System.Windows.Forms.Label();
            this.lstPuertos = new System.Windows.Forms.ListBox();
            this.lblCanales = new System.Windows.Forms.Label();
            this.lstCanales = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTarjeta
            // 
            this.lblTarjeta.AutoSize = true;
            this.lblTarjeta.Location = new System.Drawing.Point(12, 15);
            this.lblTarjeta.Name = "lblTarjeta";
            this.lblTarjeta.Size = new System.Drawing.Size(43, 13);
            this.lblTarjeta.TabIndex = 0;
            this.lblTarjeta.Text = "Tarjeta:";
            // 
            // cmbTarjeta
            // 
            this.cmbTarjeta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTarjeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarjeta.FormattingEnabled = true;
            this.cmbTarjeta.Location = new System.Drawing.Point(61, 12);
            this.cmbTarjeta.Name = "cmbTarjeta";
            this.cmbTarjeta.Size = new System.Drawing.Size(369, 21);
            this.cmbTarjeta.TabIndex = 1;
            this.cmbTarjeta.SelectedIndexChanged += new System.EventHandler(this.cmbTarjeta_SelectedIndexChanged);
            // 
            // chkEmulador
            // 
            this.chkEmulador.AutoSize = true;
            this.chkEmulador.Enabled = false;
            this.chkEmulador.Location = new System.Drawing.Point(12, 39);
            this.chkEmulador.Name = "chkEmulador";
            this.chkEmulador.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkEmulador.Size = new System.Drawing.Size(70, 17);
            this.chkEmulador.TabIndex = 2;
            this.chkEmulador.Text = "Emulador";
            this.chkEmulador.UseVisualStyleBackColor = true;
            // 
            // lblPuertos
            // 
            this.lblPuertos.AutoSize = true;
            this.lblPuertos.Location = new System.Drawing.Point(3, 0);
            this.lblPuertos.Name = "lblPuertos";
            this.lblPuertos.Size = new System.Drawing.Size(46, 13);
            this.lblPuertos.TabIndex = 3;
            this.lblPuertos.Text = "Puertos:";
            // 
            // lstPuertos
            // 
            this.lstPuertos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPuertos.FormattingEnabled = true;
            this.lstPuertos.Location = new System.Drawing.Point(3, 16);
            this.lstPuertos.Name = "lstPuertos";
            this.lstPuertos.Size = new System.Drawing.Size(204, 118);
            this.lstPuertos.TabIndex = 4;
            // 
            // lblCanales
            // 
            this.lblCanales.AutoSize = true;
            this.lblCanales.Location = new System.Drawing.Point(213, 0);
            this.lblCanales.Name = "lblCanales";
            this.lblCanales.Size = new System.Drawing.Size(48, 13);
            this.lblCanales.TabIndex = 5;
            this.lblCanales.Text = "Canales:";
            // 
            // lstCanales
            // 
            this.lstCanales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCanales.FormattingEnabled = true;
            this.lstCanales.Location = new System.Drawing.Point(213, 16);
            this.lstCanales.Name = "lstCanales";
            this.lstCanales.Size = new System.Drawing.Size(205, 118);
            this.lstCanales.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblPuertos, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstCanales, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCanales, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstPuertos, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 62);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(421, 137);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 337);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.chkEmulador);
            this.Controls.Add(this.cmbTarjeta);
            this.Controls.Add(this.lblTarjeta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainDialog";
            this.Text = "Krisa.Tarjeta.Test";
            this.Load += new System.EventHandler(this.MainDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTarjeta;
        private System.Windows.Forms.ComboBox cmbTarjeta;
        private System.Windows.Forms.CheckBox chkEmulador;
        private System.Windows.Forms.Label lblPuertos;
        private System.Windows.Forms.ListBox lstPuertos;
        private System.Windows.Forms.Label lblCanales;
        private System.Windows.Forms.ListBox lstCanales;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

