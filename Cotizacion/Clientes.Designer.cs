﻿namespace Cotizacion
{
    partial class Clientes
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cRAZONSOCIALDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.admClientesBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.adACEROS_MEXICODataSet = new Cotizacion.adACEROS_MEXICODataSet();
            this.admClientesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.admClientesTableAdapter = new Cotizacion.adACEROS_MEXICODataSetTableAdapters.admClientesTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.admClientesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.adACEROS_MEXICODataSet1 = new Cotizacion.adACEROS_MEXICODataSet();
            this.admClientesBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.admClientesBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.admClientesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.admClientesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.admClientesBindingSource3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cRAZONSOCIALDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.admClientesBindingSource2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(477, 296);
            this.dataGridView1.TabIndex = 0;
            // 
            // cRAZONSOCIALDataGridViewTextBoxColumn
            // 
            this.cRAZONSOCIALDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cRAZONSOCIALDataGridViewTextBoxColumn.DataPropertyName = "CRAZONSOCIAL";
            this.cRAZONSOCIALDataGridViewTextBoxColumn.HeaderText = "Cliente";
            this.cRAZONSOCIALDataGridViewTextBoxColumn.Name = "cRAZONSOCIALDataGridViewTextBoxColumn";
            // 
            // admClientesBindingSource2
            // 
            this.admClientesBindingSource2.DataMember = "admClientes";
            this.admClientesBindingSource2.DataSource = this.adACEROS_MEXICODataSet;
            // 
            // adACEROS_MEXICODataSet
            // 
            this.adACEROS_MEXICODataSet.DataSetName = "adACEROS_MEXICODataSet";
            this.adACEROS_MEXICODataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // admClientesBindingSource
            // 
            this.admClientesBindingSource.DataMember = "admClientes";
            this.admClientesBindingSource.DataSource = this.adACEROS_MEXICODataSet;
            // 
            // admClientesTableAdapter
            // 
            this.admClientesTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(123, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(366, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.admClientesBindingSource2;
            this.comboBox1.DisplayMember = "CCODIGOCLIENTE";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(221, 351);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.ValueMember = "CCODIGOCLIENTE";
            this.comboBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Green;
            this.button1.Image = global::Cotizacion.Properties.Resources.check;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(12, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Seleccionar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Maroon;
            this.button2.Location = new System.Drawing.Point(414, 350);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // admClientesBindingSource1
            // 
            this.admClientesBindingSource1.DataMember = "admClientes";
            this.admClientesBindingSource1.DataSource = this.adACEROS_MEXICODataSet;
            // 
            // adACEROS_MEXICODataSet1
            // 
            this.adACEROS_MEXICODataSet1.DataSetName = "adACEROS_MEXICODataSet";
            this.adACEROS_MEXICODataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // admClientesBindingSource3
            // 
            this.admClientesBindingSource3.DataMember = "admClientes";
            this.admClientesBindingSource3.DataSource = this.adACEROS_MEXICODataSet1;
            // 
            // Clientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 385);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Clientes";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.Clientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.admClientesBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.admClientesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.admClientesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.admClientesBindingSource3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private adACEROS_MEXICODataSet adACEROS_MEXICODataSet;
        private System.Windows.Forms.BindingSource admClientesBindingSource;
        private adACEROS_MEXICODataSetTableAdapters.admClientesTableAdapter admClientesTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource admClientesBindingSource1;
        private System.Windows.Forms.BindingSource admClientesBindingSource2;
        private adACEROS_MEXICODataSet adACEROS_MEXICODataSet1;
        private System.Windows.Forms.BindingSource admClientesBindingSource3;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRAZONSOCIALDataGridViewTextBoxColumn;
    }
}