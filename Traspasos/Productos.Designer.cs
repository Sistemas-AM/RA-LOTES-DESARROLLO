namespace Traspasos
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
            this.admProductosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.adAMSACONTPAQiDataSet = new Traspasos.adAMSACONTPAQiDataSet();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Codigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.exis = new System.Windows.Forms.TextBox();
            this.Medida = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Cantidad = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Destino = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Nombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.admProductosTableAdapter = new Traspasos.adAMSACONTPAQiDataSetTableAdapters.admProductosTableAdapter();
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.admProductosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adAMSACONTPAQiDataSet)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn,
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn,
            this.peso});
            this.dataGridView1.DataSource = this.admProductosBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(370, 318);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // admProductosBindingSource
            // 
            this.admProductosBindingSource.DataMember = "admProductos";
            this.admProductosBindingSource.DataSource = this.adAMSACONTPAQiDataSet;
            // 
            // adAMSACONTPAQiDataSet
            // 
            this.adAMSACONTPAQiDataSet.DataSetName = "adAMSACONTPAQiDataSet";
            this.adAMSACONTPAQiDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(192, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(190, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Codigo
            // 
            this.Codigo.Enabled = false;
            this.Codigo.Location = new System.Drawing.Point(15, 30);
            this.Codigo.Name = "Codigo";
            this.Codigo.Size = new System.Drawing.Size(130, 20);
            this.Codigo.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Codigo:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.exis);
            this.panel1.Controls.Add(this.Medida);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.Cantidad);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.Destino);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Nombre);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Codigo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(388, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 318);
            this.panel1.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(129, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Existencia:";
            // 
            // exis
            // 
            this.exis.Enabled = false;
            this.exis.Location = new System.Drawing.Point(132, 108);
            this.exis.Name = "exis";
            this.exis.Size = new System.Drawing.Size(97, 20);
            this.exis.TabIndex = 12;
            // 
            // Medida
            // 
            this.Medida.Enabled = false;
            this.Medida.Location = new System.Drawing.Point(15, 108);
            this.Medida.Name = "Medida";
            this.Medida.Size = new System.Drawing.Size(97, 20);
            this.Medida.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Peso:";
            // 
            // Cantidad
            // 
            this.Cantidad.Location = new System.Drawing.Point(79, 189);
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Size = new System.Drawing.Size(150, 20);
            this.Cantidad.TabIndex = 7;
            this.Cantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Cantidad_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Cantidad:";
            // 
            // Destino
            // 
            this.Destino.FormattingEnabled = true;
            this.Destino.Location = new System.Drawing.Point(15, 158);
            this.Destino.Name = "Destino";
            this.Destino.Size = new System.Drawing.Size(214, 21);
            this.Destino.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Destino:";
            // 
            // Nombre
            // 
            this.Nombre.Enabled = false;
            this.Nombre.Location = new System.Drawing.Point(15, 69);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(214, 20);
            this.Nombre.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nombre:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 370);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(370, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(388, 370);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(241, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // admProductosTableAdapter
            // 
            this.admProductosTableAdapter.ClearBeforeFill = true;
            // 
            // cCODIGOPRODUCTODataGridViewTextBoxColumn
            // 
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.DataPropertyName = "CCODIGOPRODUCTO";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.Name = "cCODIGOPRODUCTODataGridViewTextBoxColumn";
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.ReadOnly = true;
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cCODIGOPRODUCTODataGridViewTextBoxColumn.Width = 75;
            // 
            // cNOMBREPRODUCTODataGridViewTextBoxColumn
            // 
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.DataPropertyName = "CNOMBREPRODUCTO";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.Name = "cNOMBREPRODUCTODataGridViewTextBoxColumn";
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.ReadOnly = true;
            this.cNOMBREPRODUCTODataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // peso
            // 
            this.peso.DataPropertyName = "CPRECIO10";
            this.peso.HeaderText = "CPRECIO10";
            this.peso.Name = "peso";
            this.peso.ReadOnly = true;
            this.peso.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.peso.Visible = false;
            // 
            // Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 405);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Productos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.Productos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.admProductosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adAMSACONTPAQiDataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private adAMSACONTPAQiDataSet adAMSACONTPAQiDataSet;
        private System.Windows.Forms.BindingSource admProductosBindingSource;
        private adAMSACONTPAQiDataSetTableAdapters.admProductosTableAdapter admProductosTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox Codigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Nombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Destino;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Cantidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Medida;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox exis;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCODIGOPRODUCTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNOMBREPRODUCTODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn peso;
    }
}