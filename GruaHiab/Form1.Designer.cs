namespace GruaHiab
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.materialSingleLineTextField1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.Folio = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.label3 = new System.Windows.Forms.Label();
            this.PiezasText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.BultosText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label4 = new System.Windows.Forms.Label();
            this.KilosText = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clasificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapacidadEsp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CapacidadSur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Existencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExistenciaCedis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExistenciaCedMat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Promedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalKilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadBul = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pedir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total1 = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Nombre,
            this.Clasificacion,
            this.CapacidadEsp,
            this.CapacidadSur,
            this.Existencia,
            this.ExistenciaCedis,
            this.ExistenciaCedMat,
            this.Promedio,
            this.Kilos,
            this.Cantidad,
            this.TotalKilos,
            this.CantidadBul,
            this.Pedir});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1277, 466);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Location = new System.Drawing.Point(195, 99);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(220, 29);
            this.metroComboBox1.TabIndex = 13;
            this.metroComboBox1.UseSelectable = true;
            this.metroComboBox1.SelectedIndexChanged += new System.EventHandler(this.metroComboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(192, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sucursal de Entrada:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Sucursal de Salida:";
            // 
            // materialSingleLineTextField1
            // 
            this.materialSingleLineTextField1.Depth = 0;
            this.materialSingleLineTextField1.Enabled = false;
            this.materialSingleLineTextField1.Hint = "";
            this.materialSingleLineTextField1.Location = new System.Drawing.Point(15, 105);
            this.materialSingleLineTextField1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextField1.Name = "materialSingleLineTextField1";
            this.materialSingleLineTextField1.PasswordChar = '\0';
            this.materialSingleLineTextField1.SelectedText = "";
            this.materialSingleLineTextField1.SelectionLength = 0;
            this.materialSingleLineTextField1.SelectionStart = 0;
            this.materialSingleLineTextField1.Size = new System.Drawing.Size(161, 23);
            this.materialSingleLineTextField1.TabIndex = 16;
            this.materialSingleLineTextField1.Text = "CEDIS";
            this.materialSingleLineTextField1.UseSystemPasswordChar = false;
            // 
            // Folio
            // 
            this.Folio.Depth = 0;
            this.Folio.Hint = "Folio";
            this.Folio.Location = new System.Drawing.Point(1180, 99);
            this.Folio.MouseState = MaterialSkin.MouseState.HOVER;
            this.Folio.Name = "Folio";
            this.Folio.PasswordChar = '\0';
            this.Folio.SelectedText = "";
            this.Folio.SelectionLength = 0;
            this.Folio.SelectionStart = 0;
            this.Folio.Size = new System.Drawing.Size(100, 23);
            this.Folio.TabIndex = 18;
            this.Folio.UseSystemPasswordChar = false;
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Location = new System.Drawing.Point(12, 616);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(164, 27);
            this.materialRaisedButton1.TabIndex = 19;
            this.materialRaisedButton1.Text = "Imprimir";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(463, 622);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Total Piezas:";
            // 
            // PiezasText
            // 
            this.PiezasText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PiezasText.Depth = 0;
            this.PiezasText.Enabled = false;
            this.PiezasText.Hint = "";
            this.PiezasText.Location = new System.Drawing.Point(568, 619);
            this.PiezasText.MouseState = MaterialSkin.MouseState.HOVER;
            this.PiezasText.Name = "PiezasText";
            this.PiezasText.PasswordChar = '\0';
            this.PiezasText.SelectedText = "";
            this.PiezasText.SelectionLength = 0;
            this.PiezasText.SelectionStart = 0;
            this.PiezasText.Size = new System.Drawing.Size(111, 23);
            this.PiezasText.TabIndex = 21;
            this.PiezasText.UseSystemPasswordChar = false;
            // 
            // BultosText
            // 
            this.BultosText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BultosText.Depth = 0;
            this.BultosText.Enabled = false;
            this.BultosText.Hint = "";
            this.BultosText.Location = new System.Drawing.Point(799, 619);
            this.BultosText.MouseState = MaterialSkin.MouseState.HOVER;
            this.BultosText.Name = "BultosText";
            this.BultosText.PasswordChar = '\0';
            this.BultosText.SelectedText = "";
            this.BultosText.SelectionLength = 0;
            this.BultosText.SelectionStart = 0;
            this.BultosText.Size = new System.Drawing.Size(111, 23);
            this.BultosText.TabIndex = 23;
            this.BultosText.UseSystemPasswordChar = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(694, 622);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Total Bultos:";
            // 
            // KilosText
            // 
            this.KilosText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.KilosText.Depth = 0;
            this.KilosText.Enabled = false;
            this.KilosText.Hint = "";
            this.KilosText.Location = new System.Drawing.Point(1018, 619);
            this.KilosText.MouseState = MaterialSkin.MouseState.HOVER;
            this.KilosText.Name = "KilosText";
            this.KilosText.PasswordChar = '\0';
            this.KilosText.SelectedText = "";
            this.KilosText.SelectionLength = 0;
            this.KilosText.SelectionStart = 0;
            this.KilosText.Size = new System.Drawing.Size(111, 23);
            this.KilosText.TabIndex = 25;
            this.KilosText.UseSystemPasswordChar = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(926, 622);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 16);
            this.label5.TabIndex = 24;
            this.label5.Text = "Total Kilos:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(1170, 616);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(119, 29);
            this.btnEliminar.TabIndex = 29;
            this.btnEliminar.Text = "SALIR";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // Codigo
            // 
            this.Codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Codigo.DataPropertyName = "Codigo";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Codigo.Width = 56;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Nombre.Width = 60;
            // 
            // Clasificacion
            // 
            this.Clasificacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Clasificacion.DataPropertyName = "Letra";
            this.Clasificacion.HeaderText = "Clasificacion";
            this.Clasificacion.Name = "Clasificacion";
            this.Clasificacion.ReadOnly = true;
            this.Clasificacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Clasificacion.Width = 89;
            // 
            // CapacidadEsp
            // 
            this.CapacidadEsp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CapacidadEsp.DataPropertyName = "CapacidadEspacio";
            this.CapacidadEsp.HeaderText = "Capacidad de Espacio";
            this.CapacidadEsp.Name = "CapacidadEsp";
            this.CapacidadEsp.ReadOnly = true;
            this.CapacidadEsp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CapacidadEsp.Width = 144;
            // 
            // CapacidadSur
            // 
            this.CapacidadSur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CapacidadSur.DataPropertyName = "CapacidadSurtido";
            this.CapacidadSur.HeaderText = "Capacidad de Surtido";
            this.CapacidadSur.Name = "CapacidadSur";
            this.CapacidadSur.ReadOnly = true;
            this.CapacidadSur.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CapacidadSur.Width = 139;
            // 
            // Existencia
            // 
            this.Existencia.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Existencia.DataPropertyName = "Existencia";
            this.Existencia.HeaderText = "Existencia";
            this.Existencia.Name = "Existencia";
            this.Existencia.ReadOnly = true;
            this.Existencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Existencia.Width = 75;
            // 
            // ExistenciaCedis
            // 
            this.ExistenciaCedis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ExistenciaCedis.DataPropertyName = "existenciacedis";
            this.ExistenciaCedis.HeaderText = "Existencia en Cedis";
            this.ExistenciaCedis.Name = "ExistenciaCedis";
            this.ExistenciaCedis.ReadOnly = true;
            this.ExistenciaCedis.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExistenciaCedMat
            // 
            this.ExistenciaCedMat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ExistenciaCedMat.DataPropertyName = "cedisenmatriz";
            this.ExistenciaCedMat.HeaderText = "Existencia Cedis en Matriz";
            this.ExistenciaCedMat.Name = "ExistenciaCedMat";
            this.ExistenciaCedMat.ReadOnly = true;
            this.ExistenciaCedMat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Promedio
            // 
            this.Promedio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Promedio.DataPropertyName = "Promedio";
            this.Promedio.HeaderText = "Promedio";
            this.Promedio.Name = "Promedio";
            this.Promedio.ReadOnly = true;
            this.Promedio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Promedio.Visible = false;
            this.Promedio.Width = 69;
            // 
            // Kilos
            // 
            this.Kilos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Kilos.DataPropertyName = "Kilos";
            this.Kilos.HeaderText = "Kilos";
            this.Kilos.Name = "Kilos";
            this.Kilos.ReadOnly = true;
            this.Kilos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Kilos.Visible = false;
            this.Kilos.Width = 44;
            // 
            // Cantidad
            // 
            this.Cantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Cantidad.DataPropertyName = "Cantidad";
            this.Cantidad.HeaderText = "Cantidad Requerida";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Cantidad.Width = 129;
            // 
            // TotalKilos
            // 
            this.TotalKilos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TotalKilos.HeaderText = "Total Kilos";
            this.TotalKilos.Name = "TotalKilos";
            this.TotalKilos.ReadOnly = true;
            this.TotalKilos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CantidadBul
            // 
            this.CantidadBul.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CantidadBul.HeaderText = "Cantidad de Bultos";
            this.CantidadBul.Name = "CantidadBul";
            this.CantidadBul.ReadOnly = true;
            this.CantidadBul.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CantidadBul.Width = 124;
            // 
            // Pedir
            // 
            this.Pedir.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Pedir.HeaderText = "Cantidad a Pedir";
            this.Pedir.Name = "Pedir";
            this.Pedir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Pedir.Width = 111;
            // 
            // Total1
            // 
            this.Total1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Total1.Depth = 0;
            this.Total1.Enabled = false;
            this.Total1.Hint = "";
            this.Total1.Location = new System.Drawing.Point(334, 619);
            this.Total1.MouseState = MaterialSkin.MouseState.HOVER;
            this.Total1.Name = "Total1";
            this.Total1.PasswordChar = '\0';
            this.Total1.SelectedText = "";
            this.Total1.SelectionLength = 0;
            this.Total1.SelectionStart = 0;
            this.Total1.Size = new System.Drawing.Size(111, 23);
            this.Total1.TabIndex = 31;
            this.Total1.UseSystemPasswordChar = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(239, 622);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "Total Items:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 655);
            this.Controls.Add(this.Total1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.KilosText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BultosText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PiezasText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.Folio);
            this.Controls.Add(this.materialSingleLineTextField1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroComboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grua Hiab";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextField1;
        private MaterialSkin.Controls.MaterialSingleLineTextField Folio;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialSingleLineTextField PiezasText;
        private MaterialSkin.Controls.MaterialSingleLineTextField BultosText;
        private System.Windows.Forms.Label label4;
        private MaterialSkin.Controls.MaterialSingleLineTextField KilosText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clasificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapacidadEsp;
        private System.Windows.Forms.DataGridViewTextBoxColumn CapacidadSur;
        private System.Windows.Forms.DataGridViewTextBoxColumn Existencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExistenciaCedis;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExistenciaCedMat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Promedio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadBul;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pedir;
        private MaterialSkin.Controls.MaterialSingleLineTextField Total1;
        private System.Windows.Forms.Label label6;
    }
}

