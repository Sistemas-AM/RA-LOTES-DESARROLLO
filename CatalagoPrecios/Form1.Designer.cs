namespace CatalagoPrecios
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cIDPRODUCTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIMPORTEEXTRA2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cIMPORTEEXTRA1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPRECIO1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.admProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.adACEROS_MEXICODataSet = new CatalagoPrecios.adACEROS_MEXICODataSet();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.admProductosTableAdapter = new CatalagoPrecios.adACEROS_MEXICODataSetTableAdapters.admProductosTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.admProductosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buscar  por:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(266, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(384, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cIDPRODUCTODataGridViewTextBoxColumn,
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn,
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn,
            this.cIMPORTEEXTRA2DataGridViewTextBoxColumn,
            this.cIMPORTEEXTRA1DataGridViewTextBoxColumn,
            this.cPRECIO1DataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.admProductosBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(637, 347);
            this.dataGridView1.TabIndex = 2;
            // 
            // cIDPRODUCTODataGridViewTextBoxColumn
            // 
            this.cIDPRODUCTODataGridViewTextBoxColumn.DataPropertyName = "CIDPRODUCTO";
            this.cIDPRODUCTODataGridViewTextBoxColumn.HeaderText = "CIDPRODUCTO";
            this.cIDPRODUCTODataGridViewTextBoxColumn.Name = "cIDPRODUCTODataGridViewTextBoxColumn";
            this.cIDPRODUCTODataGridViewTextBoxColumn.Visible = false;
            // 
            // cCODIGOPRODUCTODataGridViewTextBoxColumn
            // 
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.DataPropertyName = "CCODIGOPRODUCTO";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.FillWeight = 86.92889F;
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.HeaderText = "Código";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.Name = "cCODIGOPRODUCTODataGridViewTextBoxColumn";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cNOMBREPRODUCTODataGridViewTextBoxColumn
            // 
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.DataPropertyName = "CNOMBREPRODUCTO";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.FillWeight = 152.2842F;
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.Name = "cNOMBREPRODUCTODataGridViewTextBoxColumn";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cIMPORTEEXTRA2DataGridViewTextBoxColumn
            // 
            this.cIMPORTEEXTRA2DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cIMPORTEEXTRA2DataGridViewTextBoxColumn.DataPropertyName = "CIMPORTEEXTRA2";
            this.cIMPORTEEXTRA2DataGridViewTextBoxColumn.FillWeight = 86.92889F;
            this.cIMPORTEEXTRA2DataGridViewTextBoxColumn.HeaderText = "Precio por kilo";
            this.cIMPORTEEXTRA2DataGridViewTextBoxColumn.Name = "cIMPORTEEXTRA2DataGridViewTextBoxColumn";
            this.cIMPORTEEXTRA2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cIMPORTEEXTRA1DataGridViewTextBoxColumn
            // 
            this.cIMPORTEEXTRA1DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cIMPORTEEXTRA1DataGridViewTextBoxColumn.DataPropertyName = "CIMPORTEEXTRA1";
            this.cIMPORTEEXTRA1DataGridViewTextBoxColumn.FillWeight = 86.92889F;
            this.cIMPORTEEXTRA1DataGridViewTextBoxColumn.HeaderText = "Peso";
            this.cIMPORTEEXTRA1DataGridViewTextBoxColumn.Name = "cIMPORTEEXTRA1DataGridViewTextBoxColumn";
            this.cIMPORTEEXTRA1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cPRECIO1DataGridViewTextBoxColumn
            // 
            this.cPRECIO1DataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cPRECIO1DataGridViewTextBoxColumn.DataPropertyName = "CPRECIO1";
            this.cPRECIO1DataGridViewTextBoxColumn.FillWeight = 86.92889F;
            this.cPRECIO1DataGridViewTextBoxColumn.HeaderText = "Precio";
            this.cPRECIO1DataGridViewTextBoxColumn.Name = "cPRECIO1DataGridViewTextBoxColumn";
            this.cPRECIO1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // admProductosBindingSource
            // 
            this.admProductosBindingSource.DataMember = "admProductos";
            this.admProductosBindingSource.DataSource = this.adACEROS_MEXICODataSet;
            // 
            // adACEROS_MEXICODataSet
            // 
            this.adACEROS_MEXICODataSet.DataSetName = "adACEROS_MEXICODataSet";
            this.adACEROS_MEXICODataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "Código",
            "Nombre",
            "Precio por kilo",
            "Peso",
            "Precio"});
            this.comboBox1.Location = new System.Drawing.Point(83, 21);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(177, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // admProductosTableAdapter
            // 
            this.admProductosTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.Image = global::CatalagoPrecios.Properties.Resources.Excel_Chico;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(12, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Exportar a Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Maroon;
            this.button2.Location = new System.Drawing.Point(574, 409);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Salir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 440);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catálago de precios";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.admProductosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adACEROS_MEXICODataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private adACEROS_MEXICODataSet adACEROS_MEXICODataSet;
        private System.Windows.Forms.BindingSource admProductosBindingSource;
        private adACEROS_MEXICODataSetTableAdapters.admProductosTableAdapter admProductosTableAdapter;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIDPRODUCTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCODIGOPRODUCTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNOMBREPRODUCTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIMPORTEEXTRA2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIMPORTEEXTRA1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPRECIO1DataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button2;
    }
}

