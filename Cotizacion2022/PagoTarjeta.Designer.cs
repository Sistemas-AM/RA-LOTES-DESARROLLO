namespace Cotizacion2022
{
    partial class PagoTarjeta
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
            this.label1 = new System.Windows.Forms.Label();
            this.TDC = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TD = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Total = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TotalReal = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Importe TDC:";
            // 
            // TDC
            // 
            this.TDC.Depth = 0;
            this.TDC.Hint = "Importe Tarjeta de Crédito";
            this.TDC.Location = new System.Drawing.Point(23, 101);
            this.TDC.MouseState = MaterialSkin.MouseState.HOVER;
            this.TDC.Name = "TDC";
            this.TDC.PasswordChar = '\0';
            this.TDC.SelectedText = "";
            this.TDC.SelectionLength = 0;
            this.TDC.SelectionStart = 0;
            this.TDC.Size = new System.Drawing.Size(292, 23);
            this.TDC.TabIndex = 1;
            this.TDC.UseSystemPasswordChar = false;
            this.TDC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Solonumeros);
            this.TDC.TextChanged += new System.EventHandler(this.TDC_TextChanged);
            // 
            // TD
            // 
            this.TD.Depth = 0;
            this.TD.Hint = "Importe Tarjeta de Debito";
            this.TD.Location = new System.Drawing.Point(23, 153);
            this.TD.MouseState = MaterialSkin.MouseState.HOVER;
            this.TD.Name = "TD";
            this.TD.PasswordChar = '\0';
            this.TD.SelectedText = "";
            this.TD.SelectionLength = 0;
            this.TD.SelectionStart = 0;
            this.TD.Size = new System.Drawing.Size(292, 23);
            this.TD.TabIndex = 3;
            this.TD.UseSystemPasswordChar = false;
            this.TD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Solonumeros);
            this.TD.TextChanged += new System.EventHandler(this.TD_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Importe TD:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 230);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 35);
            this.button1.TabIndex = 86;
            this.button1.Text = "GUARDAR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(207, 230);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 35);
            this.button4.TabIndex = 89;
            this.button4.Text = "SALIR";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(120, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 17);
            this.label3.TabIndex = 90;
            this.label3.Text = "Restante:";
            // 
            // Total
            // 
            this.Total.Depth = 0;
            this.Total.Enabled = false;
            this.Total.Hint = "Total";
            this.Total.Location = new System.Drawing.Point(204, 189);
            this.Total.MouseState = MaterialSkin.MouseState.HOVER;
            this.Total.Name = "Total";
            this.Total.PasswordChar = '\0';
            this.Total.SelectedText = "";
            this.Total.SelectionLength = 0;
            this.Total.SelectionStart = 0;
            this.Total.Size = new System.Drawing.Size(111, 23);
            this.Total.TabIndex = 91;
            this.Total.UseSystemPasswordChar = false;
            // 
            // TotalReal
            // 
            this.TotalReal.Depth = 0;
            this.TotalReal.Enabled = false;
            this.TotalReal.Hint = "Total";
            this.TotalReal.Location = new System.Drawing.Point(12, 189);
            this.TotalReal.MouseState = MaterialSkin.MouseState.HOVER;
            this.TotalReal.Name = "TotalReal";
            this.TotalReal.PasswordChar = '\0';
            this.TotalReal.SelectedText = "";
            this.TotalReal.SelectionLength = 0;
            this.TotalReal.SelectionStart = 0;
            this.TotalReal.Size = new System.Drawing.Size(102, 23);
            this.TotalReal.TabIndex = 92;
            this.TotalReal.UseSystemPasswordChar = false;
            this.TotalReal.Visible = false;
            // 
            // PagoTarjeta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 277);
            this.Controls.Add(this.TotalReal);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TDC);
            this.Controls.Add(this.label1);
            this.Name = "PagoTarjeta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pago Tarjeta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PagoTarjeta_FormClosing);
            this.Load += new System.EventHandler(this.PagoTarjeta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialSingleLineTextField TDC;
        private MaterialSkin.Controls.MaterialSingleLineTextField TD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialSingleLineTextField Total;
        private MaterialSkin.Controls.MaterialSingleLineTextField TotalReal;
    }
}