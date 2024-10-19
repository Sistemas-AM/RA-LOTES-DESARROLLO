namespace LaborTeams
{
    partial class EditarLT
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
            this.CodigoText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.BtnGuardar = new System.Windows.Forms.Button();
            this.DescripcionText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.NombreText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IdText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // CodigoText
            // 
            this.CodigoText.Depth = 0;
            this.CodigoText.Enabled = false;
            this.CodigoText.Hint = "Código";
            this.CodigoText.Location = new System.Drawing.Point(133, 117);
            this.CodigoText.MouseState = MaterialSkin.MouseState.HOVER;
            this.CodigoText.Name = "CodigoText";
            this.CodigoText.PasswordChar = '\0';
            this.CodigoText.SelectedText = "";
            this.CodigoText.SelectionLength = 0;
            this.CodigoText.SelectionStart = 0;
            this.CodigoText.Size = new System.Drawing.Size(289, 23);
            this.CodigoText.TabIndex = 70;
            this.CodigoText.UseSystemPasswordChar = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 69;
            this.label3.Text = "Codigo:";
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(363, 237);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(103, 36);
            this.btnSalir.TabIndex = 68;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // BtnGuardar
            // 
            this.BtnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGuardar.ForeColor = System.Drawing.Color.White;
            this.BtnGuardar.Location = new System.Drawing.Point(27, 237);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(103, 36);
            this.BtnGuardar.TabIndex = 67;
            this.BtnGuardar.Text = "GUARDAR";
            this.BtnGuardar.UseVisualStyleBackColor = false;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // DescripcionText
            // 
            this.DescripcionText.Depth = 0;
            this.DescripcionText.Hint = "Descripción";
            this.DescripcionText.Location = new System.Drawing.Point(133, 182);
            this.DescripcionText.MouseState = MaterialSkin.MouseState.HOVER;
            this.DescripcionText.Name = "DescripcionText";
            this.DescripcionText.PasswordChar = '\0';
            this.DescripcionText.SelectedText = "";
            this.DescripcionText.SelectionLength = 0;
            this.DescripcionText.SelectionStart = 0;
            this.DescripcionText.Size = new System.Drawing.Size(289, 23);
            this.DescripcionText.TabIndex = 66;
            this.DescripcionText.UseSystemPasswordChar = false;
            // 
            // NombreText
            // 
            this.NombreText.Depth = 0;
            this.NombreText.Hint = "Nombre de la aseguradora";
            this.NombreText.Location = new System.Drawing.Point(133, 150);
            this.NombreText.MouseState = MaterialSkin.MouseState.HOVER;
            this.NombreText.Name = "NombreText";
            this.NombreText.PasswordChar = '\0';
            this.NombreText.SelectedText = "";
            this.NombreText.SelectionLength = 0;
            this.NombreText.SelectionStart = 0;
            this.NombreText.Size = new System.Drawing.Size(289, 23);
            this.NombreText.TabIndex = 65;
            this.NombreText.UseSystemPasswordChar = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 64;
            this.label2.Text = "Descripción:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 63;
            this.label1.Text = "Nombre:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 71;
            this.label4.Text = "Id_LT:";
            this.label4.Visible = false;
            // 
            // IdText
            // 
            this.IdText.Depth = 0;
            this.IdText.Enabled = false;
            this.IdText.Hint = "Id LT";
            this.IdText.Location = new System.Drawing.Point(133, 82);
            this.IdText.MouseState = MaterialSkin.MouseState.HOVER;
            this.IdText.Name = "IdText";
            this.IdText.PasswordChar = '\0';
            this.IdText.SelectedText = "";
            this.IdText.SelectionLength = 0;
            this.IdText.SelectionStart = 0;
            this.IdText.Size = new System.Drawing.Size(289, 23);
            this.IdText.TabIndex = 72;
            this.IdText.UseSystemPasswordChar = false;
            this.IdText.Visible = false;
            // 
            // EditarLT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 285);
            this.Controls.Add(this.IdText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CodigoText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.BtnGuardar);
            this.Controls.Add(this.DescripcionText);
            this.Controls.Add(this.NombreText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditarLT";
            this.Text = "Editar Labor Teams";
            this.Load += new System.EventHandler(this.EditarLT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField CodigoText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button BtnGuardar;
        private MaterialSkin.Controls.MaterialSingleLineTextField DescripcionText;
        private MaterialSkin.Controls.MaterialSingleLineTextField NombreText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private MaterialSkin.Controls.MaterialSingleLineTextField IdText;
    }
}