namespace Cotizacion2022
{
    partial class PagoCheque
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
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Cheque = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Fecha = new MetroFramework.Controls.MetroDateTime();
            this.NoCheque = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label3 = new System.Windows.Forms.Label();
            this.TotalReal = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.Total = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(218, 268);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 35);
            this.button4.TabIndex = 91;
            this.button4.Text = "SALIR";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 35);
            this.button1.TabIndex = 90;
            this.button1.Text = "GUARDAR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Cheque
            // 
            this.Cheque.Depth = 0;
            this.Cheque.Hint = "Importe Cheque y/o Transferencia";
            this.Cheque.Location = new System.Drawing.Point(12, 95);
            this.Cheque.MouseState = MaterialSkin.MouseState.HOVER;
            this.Cheque.Name = "Cheque";
            this.Cheque.PasswordChar = '\0';
            this.Cheque.SelectedText = "";
            this.Cheque.SelectionLength = 0;
            this.Cheque.SelectionStart = 0;
            this.Cheque.Size = new System.Drawing.Size(320, 23);
            this.Cheque.TabIndex = 93;
            this.Cheque.UseSystemPasswordChar = false;
            this.Cheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Solonumeros);
            this.Cheque.TextChanged += new System.EventHandler(this.Cheque_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 17);
            this.label1.TabIndex = 92;
            this.label1.Text = "Importe Cheque y/o Transferencia";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 94;
            this.label2.Text = "Fecha:";
            // 
            // Fecha
            // 
            this.Fecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Fecha.Location = new System.Drawing.Point(11, 173);
            this.Fecha.MinimumSize = new System.Drawing.Size(0, 29);
            this.Fecha.Name = "Fecha";
            this.Fecha.Size = new System.Drawing.Size(200, 29);
            this.Fecha.TabIndex = 95;
            // 
            // NoCheque
            // 
            this.NoCheque.Depth = 0;
            this.NoCheque.Hint = "#Cheque, #Cuenta. #Autorizado Check Plus";
            this.NoCheque.Location = new System.Drawing.Point(11, 237);
            this.NoCheque.MouseState = MaterialSkin.MouseState.HOVER;
            this.NoCheque.Name = "NoCheque";
            this.NoCheque.PasswordChar = '\0';
            this.NoCheque.SelectedText = "";
            this.NoCheque.SelectionLength = 0;
            this.NoCheque.SelectionStart = 0;
            this.NoCheque.Size = new System.Drawing.Size(321, 23);
            this.NoCheque.TabIndex = 97;
            this.NoCheque.UseSystemPasswordChar = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 17);
            this.label3.TabIndex = 96;
            this.label3.Text = "#Cheque, #Cuenta. #Autorizado Check Plus";
            // 
            // TotalReal
            // 
            this.TotalReal.Depth = 0;
            this.TotalReal.Enabled = false;
            this.TotalReal.Hint = "Total";
            this.TotalReal.Location = new System.Drawing.Point(12, 124);
            this.TotalReal.MouseState = MaterialSkin.MouseState.HOVER;
            this.TotalReal.Name = "TotalReal";
            this.TotalReal.PasswordChar = '\0';
            this.TotalReal.SelectedText = "";
            this.TotalReal.SelectionLength = 0;
            this.TotalReal.SelectionStart = 0;
            this.TotalReal.Size = new System.Drawing.Size(91, 23);
            this.TotalReal.TabIndex = 100;
            this.TotalReal.UseSystemPasswordChar = false;
            this.TotalReal.Visible = false;
            // 
            // Total
            // 
            this.Total.Depth = 0;
            this.Total.Enabled = false;
            this.Total.Hint = "Total";
            this.Total.Location = new System.Drawing.Point(203, 122);
            this.Total.MouseState = MaterialSkin.MouseState.HOVER;
            this.Total.Name = "Total";
            this.Total.PasswordChar = '\0';
            this.Total.SelectedText = "";
            this.Total.SelectionLength = 0;
            this.Total.SelectionStart = 0;
            this.Total.Size = new System.Drawing.Size(126, 23);
            this.Total.TabIndex = 99;
            this.Total.UseSystemPasswordChar = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(119, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 98;
            this.label4.Text = "Restante:";
            // 
            // PagoCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 315);
            this.Controls.Add(this.TotalReal);
            this.Controls.Add(this.Total);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NoCheque);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Fecha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Cheque);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Name = "PagoCheque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pago Cheque";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PagoCheque_FormClosing);
            this.Load += new System.EventHandler(this.PagoCheque_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private MaterialSkin.Controls.MaterialSingleLineTextField Cheque;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroDateTime Fecha;
        private MaterialSkin.Controls.MaterialSingleLineTextField NoCheque;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialSingleLineTextField TotalReal;
        private MaterialSkin.Controls.MaterialSingleLineTextField Total;
        private System.Windows.Forms.Label label4;
    }
}