namespace Catalogo_Clientes
{
    partial class PantallaCliente
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
            this.CodigoText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label6 = new System.Windows.Forms.Label();
            this.NombreText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label1 = new System.Windows.Forms.Label();
            this.RfcText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label2 = new System.Windows.Forms.Label();
            this.DenomText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label3 = new System.Windows.Forms.Label();
            this.CurpText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label4 = new System.Windows.Forms.Label();
            this.CorreoText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.idText = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.NombreLargoText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // CodigoText
            // 
            this.CodigoText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CodigoText.Depth = 0;
            this.CodigoText.Enabled = false;
            this.CodigoText.Hint = "Código del cliente";
            this.CodigoText.Location = new System.Drawing.Point(26, 108);
            this.CodigoText.MouseState = MaterialSkin.MouseState.HOVER;
            this.CodigoText.Name = "CodigoText";
            this.CodigoText.PasswordChar = '\0';
            this.CodigoText.SelectedText = "";
            this.CodigoText.SelectionLength = 0;
            this.CodigoText.SelectionStart = 0;
            this.CodigoText.Size = new System.Drawing.Size(595, 23);
            this.CodigoText.TabIndex = 34;
            this.CodigoText.UseSystemPasswordChar = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Código:";
            // 
            // NombreText
            // 
            this.NombreText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NombreText.Depth = 0;
            this.NombreText.Hint = "Nombre Del Cliente";
            this.NombreText.Location = new System.Drawing.Point(26, 167);
            this.NombreText.MouseState = MaterialSkin.MouseState.HOVER;
            this.NombreText.Name = "NombreText";
            this.NombreText.PasswordChar = '\0';
            this.NombreText.SelectedText = "";
            this.NombreText.SelectionLength = 0;
            this.NombreText.SelectionStart = 0;
            this.NombreText.Size = new System.Drawing.Size(595, 23);
            this.NombreText.TabIndex = 36;
            this.NombreText.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 148);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Nombre:";
            // 
            // RfcText
            // 
            this.RfcText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RfcText.Depth = 0;
            this.RfcText.Hint = "RFC ";
            this.RfcText.Location = new System.Drawing.Point(26, 280);
            this.RfcText.MouseState = MaterialSkin.MouseState.HOVER;
            this.RfcText.Name = "RfcText";
            this.RfcText.PasswordChar = '\0';
            this.RfcText.SelectedText = "";
            this.RfcText.SelectionLength = 0;
            this.RfcText.SelectionStart = 0;
            this.RfcText.Size = new System.Drawing.Size(595, 23);
            this.RfcText.TabIndex = 40;
            this.RfcText.UseSystemPasswordChar = false;
            this.RfcText.Click += new System.EventHandler(this.RfcText_Click);
            this.RfcText.Leave += new System.EventHandler(this.RfcText_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 261);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 41;
            this.label2.Text = "RFC:";
            // 
            // DenomText
            // 
            this.DenomText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DenomText.Depth = 0;
            this.DenomText.Hint = "Denominación Comercial";
            this.DenomText.Location = new System.Drawing.Point(26, 338);
            this.DenomText.MouseState = MaterialSkin.MouseState.HOVER;
            this.DenomText.Name = "DenomText";
            this.DenomText.PasswordChar = '\0';
            this.DenomText.SelectedText = "";
            this.DenomText.SelectionLength = 0;
            this.DenomText.SelectionStart = 0;
            this.DenomText.Size = new System.Drawing.Size(595, 23);
            this.DenomText.TabIndex = 42;
            this.DenomText.UseSystemPasswordChar = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 16);
            this.label3.TabIndex = 43;
            this.label3.Text = "Denom. Comercial:";
            // 
            // CurpText
            // 
            this.CurpText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurpText.Depth = 0;
            this.CurpText.Hint = "CURP";
            this.CurpText.Location = new System.Drawing.Point(26, 395);
            this.CurpText.MouseState = MaterialSkin.MouseState.HOVER;
            this.CurpText.Name = "CurpText";
            this.CurpText.PasswordChar = '\0';
            this.CurpText.SelectedText = "";
            this.CurpText.SelectionLength = 0;
            this.CurpText.SelectionStart = 0;
            this.CurpText.Size = new System.Drawing.Size(595, 23);
            this.CurpText.TabIndex = 44;
            this.CurpText.UseSystemPasswordChar = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 376);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 45;
            this.label4.Text = "CURP:";
            // 
            // CorreoText
            // 
            this.CorreoText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CorreoText.Depth = 0;
            this.CorreoText.Hint = "Correo Electrónico";
            this.CorreoText.Location = new System.Drawing.Point(26, 452);
            this.CorreoText.MouseState = MaterialSkin.MouseState.HOVER;
            this.CorreoText.Name = "CorreoText";
            this.CorreoText.PasswordChar = '\0';
            this.CorreoText.SelectedText = "";
            this.CorreoText.SelectionLength = 0;
            this.CorreoText.SelectionStart = 0;
            this.CorreoText.Size = new System.Drawing.Size(595, 23);
            this.CorreoText.TabIndex = 46;
            this.CorreoText.UseSystemPasswordChar = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 433);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 16);
            this.label5.TabIndex = 47;
            this.label5.Text = "Correo Electrónico:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 565);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 35);
            this.button1.TabIndex = 90;
            this.button1.Text = "GUARDAR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(522, 565);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 35);
            this.button2.TabIndex = 91;
            this.button2.Text = "SALIR";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(139, 565);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 35);
            this.button3.TabIndex = 92;
            this.button3.Text = "DOMICILIO";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // idText
            // 
            this.idText.Location = new System.Drawing.Point(522, 70);
            this.idText.Name = "idText";
            this.idText.Size = new System.Drawing.Size(100, 20);
            this.idText.TabIndex = 93;
            this.idText.Visible = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(13, 565);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 35);
            this.button4.TabIndex = 94;
            this.button4.Text = "ACTUALIZAR";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(23, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.label7.TabIndex = 95;
            this.label7.Text = "Regimen Fiscal:";
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Location = new System.Drawing.Point(26, 507);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(596, 29);
            this.metroComboBox1.TabIndex = 96;
            this.metroComboBox1.UseSelectable = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 16);
            this.label8.TabIndex = 39;
            this.label8.Text = "Nombre Largo:";
            this.label8.Visible = false;
            // 
            // NombreLargoText
            // 
            this.NombreLargoText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NombreLargoText.Depth = 0;
            this.NombreLargoText.Hint = "Nombre Largo Del Cliente";
            this.NombreLargoText.Location = new System.Drawing.Point(26, 225);
            this.NombreLargoText.MouseState = MaterialSkin.MouseState.HOVER;
            this.NombreLargoText.Name = "NombreLargoText";
            this.NombreLargoText.PasswordChar = '\0';
            this.NombreLargoText.SelectedText = "";
            this.NombreLargoText.SelectionLength = 0;
            this.NombreLargoText.SelectionStart = 0;
            this.NombreLargoText.Size = new System.Drawing.Size(595, 23);
            this.NombreLargoText.TabIndex = 38;
            this.NombreLargoText.UseSystemPasswordChar = false;
            this.NombreLargoText.Visible = false;
            // 
            // PantallaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 612);
            this.Controls.Add(this.NombreLargoText);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.metroComboBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.idText);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CorreoText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CurpText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DenomText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RfcText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NombreText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CodigoText);
            this.Controls.Add(this.label6);
            this.Name = "PantallaCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PantallaCliente";
            this.Load += new System.EventHandler(this.PantallaCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField CodigoText;
        private System.Windows.Forms.Label label6;
        private MaterialSkin.Controls.MaterialSingleLineTextField NombreText;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialSingleLineTextField RfcText;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialSingleLineTextField DenomText;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialSingleLineTextField CurpText;
        private System.Windows.Forms.Label label4;
        private MaterialSkin.Controls.MaterialSingleLineTextField CorreoText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox idText;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button4;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private System.Windows.Forms.Label label7;
        private MaterialSkin.Controls.MaterialSingleLineTextField NombreLargoText;
        private System.Windows.Forms.Label label8;
    }
}