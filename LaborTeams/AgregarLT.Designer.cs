namespace LaborTeams
{
    partial class AgregarLT
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
            this.btnSalir = new System.Windows.Forms.Button();
            this.BtnGuardar = new System.Windows.Forms.Button();
            this.DescripcionText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.NombreText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CodigoText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(340, 217);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(103, 35);
            this.btnSalir.TabIndex = 60;
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
            this.BtnGuardar.Location = new System.Drawing.Point(12, 217);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(103, 35);
            this.BtnGuardar.TabIndex = 59;
            this.BtnGuardar.Text = "GUARDAR";
            this.BtnGuardar.UseVisualStyleBackColor = false;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // DescripcionText
            // 
            this.DescripcionText.Depth = 0;
            this.DescripcionText.Hint = "Descripción";
            this.DescripcionText.Location = new System.Drawing.Point(122, 167);
            this.DescripcionText.MouseState = MaterialSkin.MouseState.HOVER;
            this.DescripcionText.Name = "DescripcionText";
            this.DescripcionText.PasswordChar = '\0';
            this.DescripcionText.SelectedText = "";
            this.DescripcionText.SelectionLength = 0;
            this.DescripcionText.SelectionStart = 0;
            this.DescripcionText.Size = new System.Drawing.Size(289, 23);
            this.DescripcionText.TabIndex = 58;
            this.DescripcionText.UseSystemPasswordChar = false;
            // 
            // NombreText
            // 
            this.NombreText.Depth = 0;
            this.NombreText.Hint = "Nombre del labor teams";
            this.NombreText.Location = new System.Drawing.Point(122, 130);
            this.NombreText.MouseState = MaterialSkin.MouseState.HOVER;
            this.NombreText.Name = "NombreText";
            this.NombreText.PasswordChar = '\0';
            this.NombreText.SelectedText = "";
            this.NombreText.SelectionLength = 0;
            this.NombreText.SelectionStart = 0;
            this.NombreText.Size = new System.Drawing.Size(289, 23);
            this.NombreText.TabIndex = 57;
            this.NombreText.UseSystemPasswordChar = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 56;
            this.label2.Text = "Descripción:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 55;
            this.label1.Text = "Nombre:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 61;
            this.label3.Text = "Código:";
            // 
            // CodigoText
            // 
            this.CodigoText.Depth = 0;
            this.CodigoText.Hint = "Código";
            this.CodigoText.Location = new System.Drawing.Point(122, 98);
            this.CodigoText.MouseState = MaterialSkin.MouseState.HOVER;
            this.CodigoText.Name = "CodigoText";
            this.CodigoText.PasswordChar = '\0';
            this.CodigoText.SelectedText = "";
            this.CodigoText.SelectionLength = 0;
            this.CodigoText.SelectionStart = 0;
            this.CodigoText.Size = new System.Drawing.Size(289, 23);
            this.CodigoText.TabIndex = 62;
            this.CodigoText.UseSystemPasswordChar = false;
            // 
            // AgregarLT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 264);
            this.Controls.Add(this.CodigoText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.BtnGuardar);
            this.Controls.Add(this.DescripcionText);
            this.Controls.Add(this.NombreText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AgregarLT";
            this.Text = "Agregar Labor Teams";
            this.Load += new System.EventHandler(this.AgregarLT_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button BtnGuardar;
        private MaterialSkin.Controls.MaterialSingleLineTextField DescripcionText;
        private MaterialSkin.Controls.MaterialSingleLineTextField NombreText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialSingleLineTextField CodigoText;
    }
}