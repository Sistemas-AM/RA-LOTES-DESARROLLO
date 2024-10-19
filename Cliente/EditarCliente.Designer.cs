﻿namespace Cliente
{
    partial class EditarCliente
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
            this.IdText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.BtnGuardar = new System.Windows.Forms.Button();
            this.DescripcionText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.NombreText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IdText
            // 
            this.IdText.Depth = 0;
            this.IdText.Enabled = false;
            this.IdText.Hint = "Id Cliente";
            this.IdText.Location = new System.Drawing.Point(151, 88);
            this.IdText.MouseState = MaterialSkin.MouseState.HOVER;
            this.IdText.Name = "IdText";
            this.IdText.PasswordChar = '\0';
            this.IdText.SelectedText = "";
            this.IdText.SelectionLength = 0;
            this.IdText.SelectionStart = 0;
            this.IdText.Size = new System.Drawing.Size(289, 23);
            this.IdText.TabIndex = 82;
            this.IdText.UseSystemPasswordChar = false;
            this.IdText.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(58, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 81;
            this.label4.Text = "Id_Cliente:";
            this.label4.Visible = false;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(370, 202);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(103, 40);
            this.btnSalir.TabIndex = 78;
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
            this.BtnGuardar.Location = new System.Drawing.Point(12, 202);
            this.BtnGuardar.Name = "BtnGuardar";
            this.BtnGuardar.Size = new System.Drawing.Size(103, 40);
            this.BtnGuardar.TabIndex = 77;
            this.BtnGuardar.Text = "GUARDAR";
            this.BtnGuardar.UseVisualStyleBackColor = false;
            this.BtnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // DescripcionText
            // 
            this.DescripcionText.Depth = 0;
            this.DescripcionText.Hint = "Descripción";
            this.DescripcionText.Location = new System.Drawing.Point(151, 149);
            this.DescripcionText.MouseState = MaterialSkin.MouseState.HOVER;
            this.DescripcionText.Name = "DescripcionText";
            this.DescripcionText.PasswordChar = '\0';
            this.DescripcionText.SelectedText = "";
            this.DescripcionText.SelectionLength = 0;
            this.DescripcionText.SelectionStart = 0;
            this.DescripcionText.Size = new System.Drawing.Size(289, 23);
            this.DescripcionText.TabIndex = 76;
            this.DescripcionText.UseSystemPasswordChar = false;
            // 
            // NombreText
            // 
            this.NombreText.Depth = 0;
            this.NombreText.Hint = "Nombre del cliente";
            this.NombreText.Location = new System.Drawing.Point(151, 117);
            this.NombreText.MouseState = MaterialSkin.MouseState.HOVER;
            this.NombreText.Name = "NombreText";
            this.NombreText.PasswordChar = '\0';
            this.NombreText.SelectedText = "";
            this.NombreText.SelectionLength = 0;
            this.NombreText.SelectionStart = 0;
            this.NombreText.Size = new System.Drawing.Size(289, 23);
            this.NombreText.TabIndex = 75;
            this.NombreText.UseSystemPasswordChar = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 74;
            this.label2.Text = "Descripción:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 73;
            this.label1.Text = "Nombre:";
            // 
            // EditarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 254);
            this.Controls.Add(this.IdText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.BtnGuardar);
            this.Controls.Add(this.DescripcionText);
            this.Controls.Add(this.NombreText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditarCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editar Cliente";
            this.Load += new System.EventHandler(this.EditarCliente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField IdText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button BtnGuardar;
        private MaterialSkin.Controls.MaterialSingleLineTextField DescripcionText;
        private MaterialSkin.Controls.MaterialSingleLineTextField NombreText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}