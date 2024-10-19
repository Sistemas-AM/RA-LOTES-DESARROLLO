namespace Cotizacion
{
    partial class Productos
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
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPRECIO1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPESOPRODUCTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.admProductosBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.adACEROS_MEXICODataSet2 = new Cotizacion.adACEROS_MEXICODataSet2();
            this.adACEROS_MEXICODataSet1 = new Cotizacion.adACEROS_MEXICODataSet1();
            this.admProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.admProductosTableAdapter = new Cotizacion.adACEROS_MEXICODataSet1TableAdapters.admProductosTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.admProductosTableAdapter1 = new Cotizacion.adACEROS_MEXICODataSet2TableAdapters.admProductosTableAdapter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.admProductosBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.admProductosBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn,
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn,
            this.cPRECIO1DataGridViewTextBoxColumn,
            this.cPESOPRODUCTODataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.admProductosBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(569, 256);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // cCODIGOPRODUCTODataGridViewTextBoxColumn
            // 
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.DataPropertyName = "CCODIGOPRODUCTO";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.Name = "cCODIGOPRODUCTODataGridViewTextBoxColumn";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.ReadOnly = true;
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.Width = 65;
            // 
            // cNOMBREPRODUCTODataGridViewTextBoxColumn
            // 
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.DataPropertyName = "CNOMBREPRODUCTO";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.HeaderText = "Producto";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.Name = "cNOMBREPRODUCTODataGridViewTextBoxColumn";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.ReadOnly = true;
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // cPRECIO1DataGridViewTextBoxColumn
            // 
            this.cPRECIO1DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.cPRECIO1DataGridViewTextBoxColumn.DataPropertyName = "CPRECIO1";
            this.cPRECIO1DataGridViewTextBoxColumn.HeaderText = "Precio";
            this.cPRECIO1DataGridViewTextBoxColumn.Name = "cPRECIO1DataGridViewTextBoxColumn";
            this.cPRECIO1DataGridViewTextBoxColumn.ReadOnly = true;
            this.cPRECIO1DataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cPRECIO1DataGridViewTextBoxColumn.Width = 62;
            // 
            // cPESOPRODUCTODataGridViewTextBoxColumn
            // 
            this.cPESOPRODUCTODataGridViewTextBoxColumn.DataPropertyName = "CPESOPRODUCTO";
            this.cPESOPRODUCTODataGridViewTextBoxColumn.HeaderText = "CPESOPRODUCTO";
            this.cPESOPRODUCTODataGridViewTextBoxColumn.Name = "cPESOPRODUCTODataGridViewTextBoxColumn";
            this.cPESOPRODUCTODataGridViewTextBoxColumn.Visible = false;
            // 
            // admProductosBindingSource1
            // 
            this.admProductosBindingSource1.DataMember = "admProductos";
            this.admProductosBindingSource1.DataSource = this.adACEROS_MEXICODataSet2;
            // 
            // adACEROS_MEXICODataSet2
            // 
            this.adACEROS_MEXICODataSet2.DataSetName = "adACEROS_MEXICODataSet2";
            this.adACEROS_MEXICODataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // adACEROS_MEXICODataSet1
            // 
            this.adACEROS_MEXICODataSet1.DataSetName = "adACEROS_MEXICODataSet1";
            this.adACEROS_MEXICODataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // admProductosBindingSource
            // 
            this.admProductosBindingSource.DataMember = "admProductos";
            this.admProductosBindingSource.DataSource = this.adACEROS_MEXICODataSet1;
            // 
            // admProductosTableAdapter
            // 
            this.admProductosTableAdapter.ClearBeforeFill = true;
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
            this.button1.Location = new System.Drawing.Point(13, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 2;
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
            this.button2.Location = new System.Drawing.Point(506, 316);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.admProductosBindingSource1;
            this.comboBox1.DisplayMember = "CCODIGOPRODUCTO";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(130, 316);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.ValueMember = "CCODIGOPRODUCTO";
            this.comboBox1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(243, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(338, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Buscar:";
            // 
            // admProductosTableAdapter1
            // 
            this.admProductosTableAdapter1.ClearBeforeFill = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(13, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 45);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(82, 19);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(68, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Producto";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(58, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Codigo";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 357);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Productos";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.Productos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.admProductosBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.admProductosBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private adACEROS_MEXICODataSet1 adACEROS_MEXICODataSet1;
        private System.Windows.Forms.BindingSource admProductosBindingSource;
        private adACEROS_MEXICODataSet1TableAdapters.admProductosTableAdapter admProductosTableAdapter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private adACEROS_MEXICODataSet2 adACEROS_MEXICODataSet2;
        private System.Windows.Forms.BindingSource admProductosBindingSource1;
        private adACEROS_MEXICODataSet2TableAdapters.admProductosTableAdapter admProductosTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCODIGOPRODUCTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNOMBREPRODUCTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPRECIO1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPESOPRODUCTODataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}