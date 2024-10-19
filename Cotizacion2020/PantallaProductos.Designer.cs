namespace Cotizacion2020
{
    partial class PantallaProductos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cidproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Peso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPRECIO1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPRECIO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPRECIO3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPRECIO6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Busqueda = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.exiMP = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel16 = new MaterialSkin.Controls.MaterialLabel();
            this.exiCD = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel9 = new MaterialSkin.Controls.MaterialLabel();
            this.exiPLA = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            this.exiSP = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel7 = new MaterialSkin.Controls.MaterialLabel();
            this.exiIGS = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.exiLP = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.ExiMAT = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.ExisMI = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel10 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel11 = new MaterialSkin.Controls.MaterialLabel();
            this.CodigoPro = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.NombrePro = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel12 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel13 = new MaterialSkin.Controls.MaterialLabel();
            this.PrecioPro = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.cantidad = new System.Windows.Forms.TextBox();
            this.materialLabel18 = new MaterialSkin.Controls.MaterialLabel();
            this.exiAV = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel17 = new MaterialSkin.Controls.MaterialLabel();
            this.exiRF = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.exiPPT = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel15 = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.metroPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(93, 618);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(1016, 27);
            this.button2.TabIndex = 91;
            this.button2.Text = "CERRAR";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(332, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 18);
            this.button1.TabIndex = 90;
            this.button1.Text = "SELECCIONAR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cidproducto,
            this.Codigo,
            this.Nombre,
            this.Precio,
            this.Peso,
            this.Descuento1,
            this.Descuento2,
            this.Cliente,
            this.CPRECIO1,
            this.CPRECIO2,
            this.CPRECIO3,
            this.CPRECIO6});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 105);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(597, 499);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // cidproducto
            // 
            this.cidproducto.DataPropertyName = "cidproducto";
            this.cidproducto.HeaderText = "cidproducto";
            this.cidproducto.Name = "cidproducto";
            this.cidproducto.ReadOnly = true;
            this.cidproducto.Visible = false;
            // 
            // Codigo
            // 
            this.Codigo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Codigo.DataPropertyName = "codigo";
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 87;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nombre.DataPropertyName = "nombre";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Precio
            // 
            this.Precio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Precio.DataPropertyName = "precio";
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Visible = false;
            // 
            // Peso
            // 
            this.Peso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Peso.DataPropertyName = "peso";
            this.Peso.HeaderText = "Peso";
            this.Peso.Name = "Peso";
            this.Peso.ReadOnly = true;
            this.Peso.Width = 73;
            // 
            // Descuento1
            // 
            this.Descuento1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descuento1.DataPropertyName = "Descuento1";
            this.Descuento1.HeaderText = "Descuento1";
            this.Descuento1.Name = "Descuento1";
            this.Descuento1.ReadOnly = true;
            this.Descuento1.Visible = false;
            // 
            // Descuento2
            // 
            this.Descuento2.DataPropertyName = "Descuento2";
            this.Descuento2.HeaderText = "Descuento2";
            this.Descuento2.Name = "Descuento2";
            this.Descuento2.ReadOnly = true;
            this.Descuento2.Visible = false;
            // 
            // Cliente
            // 
            this.Cliente.DataPropertyName = "CPRECIO6";
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Visible = false;
            // 
            // CPRECIO1
            // 
            this.CPRECIO1.DataPropertyName = "Cprecio1";
            this.CPRECIO1.HeaderText = "Precio";
            this.CPRECIO1.Name = "CPRECIO1";
            this.CPRECIO1.ReadOnly = true;
            // 
            // CPRECIO2
            // 
            this.CPRECIO2.DataPropertyName = "Cprecio2";
            this.CPRECIO2.HeaderText = "CPRECIO2";
            this.CPRECIO2.Name = "CPRECIO2";
            this.CPRECIO2.ReadOnly = true;
            this.CPRECIO2.Visible = false;
            // 
            // CPRECIO3
            // 
            this.CPRECIO3.DataPropertyName = "CPrecio3";
            this.CPRECIO3.HeaderText = "CPRECIO3";
            this.CPRECIO3.Name = "CPRECIO3";
            this.CPRECIO3.ReadOnly = true;
            this.CPRECIO3.Visible = false;
            // 
            // CPRECIO6
            // 
            this.CPRECIO6.DataPropertyName = "empleado";
            this.CPRECIO6.HeaderText = "CPRECIO6";
            this.CPRECIO6.Name = "CPRECIO6";
            this.CPRECIO6.ReadOnly = true;
            this.CPRECIO6.Visible = false;
            // 
            // Busqueda
            // 
            this.Busqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Busqueda.Location = new System.Drawing.Point(84, 77);
            this.Busqueda.Name = "Busqueda";
            this.Busqueda.Size = new System.Drawing.Size(242, 20);
            this.Busqueda.TabIndex = 92;
            this.Busqueda.TextChanged += new System.EventHandler(this.Busqueda_TextChanged_1);
            this.Busqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Busqueda_KeyDown_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 18);
            this.label1.TabIndex = 93;
            this.label1.Text = "Buscar:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroPanel2
            // 
            this.metroPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel2.Controls.Add(this.materialLabel18);
            this.metroPanel2.Controls.Add(this.exiAV);
            this.metroPanel2.Controls.Add(this.materialLabel17);
            this.metroPanel2.Controls.Add(this.exiRF);
            this.metroPanel2.Controls.Add(this.exiPPT);
            this.metroPanel2.Controls.Add(this.materialLabel15);
            this.metroPanel2.Controls.Add(this.exiMP);
            this.metroPanel2.Controls.Add(this.materialLabel16);
            this.metroPanel2.Controls.Add(this.exiCD);
            this.metroPanel2.Controls.Add(this.materialLabel9);
            this.metroPanel2.Controls.Add(this.exiPLA);
            this.metroPanel2.Controls.Add(this.materialLabel8);
            this.metroPanel2.Controls.Add(this.exiSP);
            this.metroPanel2.Controls.Add(this.materialLabel7);
            this.metroPanel2.Controls.Add(this.exiIGS);
            this.metroPanel2.Controls.Add(this.materialLabel6);
            this.metroPanel2.Controls.Add(this.exiLP);
            this.metroPanel2.Controls.Add(this.materialLabel5);
            this.metroPanel2.Controls.Add(this.ExiMAT);
            this.metroPanel2.Controls.Add(this.materialLabel4);
            this.metroPanel2.Controls.Add(this.materialLabel2);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(624, 246);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(485, 349);
            this.metroPanel2.TabIndex = 95;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // exiMP
            // 
            this.exiMP.Depth = 0;
            this.exiMP.Enabled = false;
            this.exiMP.Hint = "";
            this.exiMP.Location = new System.Drawing.Point(367, 191);
            this.exiMP.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiMP.Name = "exiMP";
            this.exiMP.PasswordChar = '\0';
            this.exiMP.SelectedText = "";
            this.exiMP.SelectionLength = 0;
            this.exiMP.SelectionStart = 0;
            this.exiMP.Size = new System.Drawing.Size(108, 23);
            this.exiMP.TabIndex = 22;
            this.exiMP.UseSystemPasswordChar = false;
            // 
            // materialLabel16
            // 
            this.materialLabel16.AutoSize = true;
            this.materialLabel16.Depth = 0;
            this.materialLabel16.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel16.Location = new System.Drawing.Point(246, 191);
            this.materialLabel16.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel16.Name = "materialLabel16";
            this.materialLabel16.Size = new System.Drawing.Size(107, 19);
            this.materialLabel16.TabIndex = 21;
            this.materialLabel16.Text = "Materia Prima:";
            // 
            // exiCD
            // 
            this.exiCD.Depth = 0;
            this.exiCD.Enabled = false;
            this.exiCD.Hint = "";
            this.exiCD.Location = new System.Drawing.Point(92, 137);
            this.exiCD.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiCD.Name = "exiCD";
            this.exiCD.PasswordChar = '\0';
            this.exiCD.SelectedText = "";
            this.exiCD.SelectionLength = 0;
            this.exiCD.SelectionStart = 0;
            this.exiCD.Size = new System.Drawing.Size(108, 23);
            this.exiCD.TabIndex = 16;
            this.exiCD.UseSystemPasswordChar = false;
            // 
            // materialLabel9
            // 
            this.materialLabel9.AutoSize = true;
            this.materialLabel9.Depth = 0;
            this.materialLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel9.Location = new System.Drawing.Point(32, 137);
            this.materialLabel9.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel9.Name = "materialLabel9";
            this.materialLabel9.Size = new System.Drawing.Size(50, 18);
            this.materialLabel9.TabIndex = 15;
            this.materialLabel9.Text = "Cedis:";
            // 
            // exiPLA
            // 
            this.exiPLA.Depth = 0;
            this.exiPLA.Enabled = false;
            this.exiPLA.Hint = "";
            this.exiPLA.Location = new System.Drawing.Point(92, 85);
            this.exiPLA.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiPLA.Name = "exiPLA";
            this.exiPLA.PasswordChar = '\0';
            this.exiPLA.SelectedText = "";
            this.exiPLA.SelectionLength = 0;
            this.exiPLA.SelectionStart = 0;
            this.exiPLA.Size = new System.Drawing.Size(108, 23);
            this.exiPLA.TabIndex = 14;
            this.exiPLA.UseSystemPasswordChar = false;
            // 
            // materialLabel8
            // 
            this.materialLabel8.AutoSize = true;
            this.materialLabel8.Depth = 0;
            this.materialLabel8.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel8.Location = new System.Drawing.Point(28, 85);
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            this.materialLabel8.Size = new System.Drawing.Size(55, 19);
            this.materialLabel8.TabIndex = 13;
            this.materialLabel8.Text = "Planta:";
            // 
            // exiSP
            // 
            this.exiSP.Depth = 0;
            this.exiSP.Enabled = false;
            this.exiSP.Hint = "";
            this.exiSP.Location = new System.Drawing.Point(92, 237);
            this.exiSP.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiSP.Name = "exiSP";
            this.exiSP.PasswordChar = '\0';
            this.exiSP.SelectedText = "";
            this.exiSP.SelectionLength = 0;
            this.exiSP.SelectionStart = 0;
            this.exiSP.Size = new System.Drawing.Size(108, 23);
            this.exiSP.TabIndex = 12;
            this.exiSP.UseSystemPasswordChar = false;
            // 
            // materialLabel7
            // 
            this.materialLabel7.AutoSize = true;
            this.materialLabel7.Depth = 0;
            this.materialLabel7.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel7.Location = new System.Drawing.Point(2, 237);
            this.materialLabel7.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel7.Name = "materialLabel7";
            this.materialLabel7.Size = new System.Drawing.Size(81, 19);
            this.materialLabel7.TabIndex = 11;
            this.materialLabel7.Text = "San Pedro:";
            // 
            // exiIGS
            // 
            this.exiIGS.Depth = 0;
            this.exiIGS.Enabled = false;
            this.exiIGS.Hint = "";
            this.exiIGS.Location = new System.Drawing.Point(92, 190);
            this.exiIGS.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiIGS.Name = "exiIGS";
            this.exiIGS.PasswordChar = '\0';
            this.exiIGS.SelectedText = "";
            this.exiIGS.SelectionLength = 0;
            this.exiIGS.SelectionStart = 0;
            this.exiIGS.Size = new System.Drawing.Size(108, 23);
            this.exiIGS.TabIndex = 10;
            this.exiIGS.UseSystemPasswordChar = false;
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel6.Location = new System.Drawing.Point(21, 190);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(62, 19);
            this.materialLabel6.TabIndex = 9;
            this.materialLabel6.Text = "Salazar:";
            // 
            // exiLP
            // 
            this.exiLP.Depth = 0;
            this.exiLP.Enabled = false;
            this.exiLP.Hint = "";
            this.exiLP.Location = new System.Drawing.Point(367, 138);
            this.exiLP.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiLP.Name = "exiLP";
            this.exiLP.PasswordChar = '\0';
            this.exiLP.SelectedText = "";
            this.exiLP.SelectionLength = 0;
            this.exiLP.SelectionStart = 0;
            this.exiLP.Size = new System.Drawing.Size(108, 23);
            this.exiLP.TabIndex = 8;
            this.exiLP.UseSystemPasswordChar = false;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel5.Location = new System.Drawing.Point(247, 139);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(106, 19);
            this.materialLabel5.TabIndex = 7;
            this.materialLabel5.Text = "Lopez Portillo:";
            // 
            // ExiMAT
            // 
            this.ExiMAT.Depth = 0;
            this.ExiMAT.Enabled = false;
            this.ExiMAT.Hint = "";
            this.ExiMAT.Location = new System.Drawing.Point(367, 84);
            this.ExiMAT.MouseState = MaterialSkin.MouseState.HOVER;
            this.ExiMAT.Name = "ExiMAT";
            this.ExiMAT.PasswordChar = '\0';
            this.ExiMAT.SelectedText = "";
            this.ExiMAT.SelectionLength = 0;
            this.ExiMAT.SelectionStart = 0;
            this.ExiMAT.Size = new System.Drawing.Size(108, 23);
            this.ExiMAT.TabIndex = 6;
            this.ExiMAT.UseSystemPasswordChar = false;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel4.Location = new System.Drawing.Point(234, 86);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(119, 19);
            this.materialLabel4.TabIndex = 5;
            this.materialLabel4.Text = "Matriz Socogos:";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(138, 18);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(199, 19);
            this.materialLabel2.TabIndex = 2;
            this.materialLabel2.Text = "EXISTENCIAS SUCURSALES";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(828, 174);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(78, 19);
            this.materialLabel3.TabIndex = 3;
            this.materialLabel3.Text = "Existencia";
            // 
            // ExisMI
            // 
            this.ExisMI.Depth = 0;
            this.ExisMI.Enabled = false;
            this.ExisMI.Hint = "";
            this.ExisMI.Location = new System.Drawing.Point(912, 171);
            this.ExisMI.MouseState = MaterialSkin.MouseState.HOVER;
            this.ExisMI.Name = "ExisMI";
            this.ExisMI.PasswordChar = '\0';
            this.ExisMI.SelectedText = "";
            this.ExisMI.SelectionLength = 0;
            this.ExisMI.SelectionStart = 0;
            this.ExisMI.Size = new System.Drawing.Size(125, 23);
            this.ExisMI.TabIndex = 4;
            this.ExisMI.UseSystemPasswordChar = false;
            // 
            // materialLabel10
            // 
            this.materialLabel10.AutoSize = true;
            this.materialLabel10.Depth = 0;
            this.materialLabel10.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel10.Location = new System.Drawing.Point(630, 105);
            this.materialLabel10.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel10.Name = "materialLabel10";
            this.materialLabel10.Size = new System.Drawing.Size(61, 19);
            this.materialLabel10.TabIndex = 5;
            this.materialLabel10.Text = "Codigo:";
            // 
            // materialLabel11
            // 
            this.materialLabel11.AutoSize = true;
            this.materialLabel11.Depth = 0;
            this.materialLabel11.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel11.Location = new System.Drawing.Point(624, 140);
            this.materialLabel11.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel11.Name = "materialLabel11";
            this.materialLabel11.Size = new System.Drawing.Size(67, 19);
            this.materialLabel11.TabIndex = 96;
            this.materialLabel11.Text = "Nombre:";
            // 
            // CodigoPro
            // 
            this.CodigoPro.Depth = 0;
            this.CodigoPro.Enabled = false;
            this.CodigoPro.Hint = "";
            this.CodigoPro.Location = new System.Drawing.Point(699, 101);
            this.CodigoPro.MouseState = MaterialSkin.MouseState.HOVER;
            this.CodigoPro.Name = "CodigoPro";
            this.CodigoPro.PasswordChar = '\0';
            this.CodigoPro.SelectedText = "";
            this.CodigoPro.SelectionLength = 0;
            this.CodigoPro.SelectionStart = 0;
            this.CodigoPro.Size = new System.Drawing.Size(169, 23);
            this.CodigoPro.TabIndex = 5;
            this.CodigoPro.UseSystemPasswordChar = false;
            // 
            // NombrePro
            // 
            this.NombrePro.Depth = 0;
            this.NombrePro.Enabled = false;
            this.NombrePro.Hint = "";
            this.NombrePro.Location = new System.Drawing.Point(699, 136);
            this.NombrePro.MouseState = MaterialSkin.MouseState.HOVER;
            this.NombrePro.Name = "NombrePro";
            this.NombrePro.PasswordChar = '\0';
            this.NombrePro.SelectedText = "";
            this.NombrePro.SelectionLength = 0;
            this.NombrePro.SelectionStart = 0;
            this.NombrePro.Size = new System.Drawing.Size(338, 23);
            this.NombrePro.TabIndex = 97;
            this.NombrePro.UseSystemPasswordChar = false;
            // 
            // materialLabel12
            // 
            this.materialLabel12.AutoSize = true;
            this.materialLabel12.Depth = 0;
            this.materialLabel12.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel12.Location = new System.Drawing.Point(619, 205);
            this.materialLabel12.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel12.Name = "materialLabel12";
            this.materialLabel12.Size = new System.Drawing.Size(72, 19);
            this.materialLabel12.TabIndex = 98;
            this.materialLabel12.Text = "Cantidad:";
            // 
            // materialLabel13
            // 
            this.materialLabel13.AutoSize = true;
            this.materialLabel13.Depth = 0;
            this.materialLabel13.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel13.Location = new System.Drawing.Point(635, 174);
            this.materialLabel13.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel13.Name = "materialLabel13";
            this.materialLabel13.Size = new System.Drawing.Size(56, 19);
            this.materialLabel13.TabIndex = 100;
            this.materialLabel13.Text = "Precio:";
            // 
            // PrecioPro
            // 
            this.PrecioPro.Depth = 0;
            this.PrecioPro.Enabled = false;
            this.PrecioPro.Hint = "";
            this.PrecioPro.Location = new System.Drawing.Point(699, 171);
            this.PrecioPro.MouseState = MaterialSkin.MouseState.HOVER;
            this.PrecioPro.Name = "PrecioPro";
            this.PrecioPro.PasswordChar = '\0';
            this.PrecioPro.SelectedText = "";
            this.PrecioPro.SelectionLength = 0;
            this.PrecioPro.SelectionStart = 0;
            this.PrecioPro.Size = new System.Drawing.Size(110, 23);
            this.PrecioPro.TabIndex = 101;
            this.PrecioPro.UseSystemPasswordChar = false;
            // 
            // cantidad
            // 
            this.cantidad.Location = new System.Drawing.Point(697, 204);
            this.cantidad.Name = "cantidad";
            this.cantidad.Size = new System.Drawing.Size(114, 20);
            this.cantidad.TabIndex = 102;
            this.cantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cantidad_KeyPress);
            // 
            // materialLabel18
            // 
            this.materialLabel18.AutoSize = true;
            this.materialLabel18.Depth = 0;
            this.materialLabel18.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel18.Location = new System.Drawing.Point(7, 279);
            this.materialLabel18.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel18.Name = "materialLabel18";
            this.materialLabel18.Size = new System.Drawing.Size(74, 19);
            this.materialLabel18.TabIndex = 115;
            this.materialLabel18.Text = "A. Varilla:";
            // 
            // exiAV
            // 
            this.exiAV.Depth = 0;
            this.exiAV.Enabled = false;
            this.exiAV.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.exiAV.Hint = "";
            this.exiAV.Location = new System.Drawing.Point(92, 281);
            this.exiAV.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiAV.Name = "exiAV";
            this.exiAV.PasswordChar = '\0';
            this.exiAV.SelectedText = "";
            this.exiAV.SelectionLength = 0;
            this.exiAV.SelectionStart = 0;
            this.exiAV.Size = new System.Drawing.Size(108, 23);
            this.exiAV.TabIndex = 114;
            this.exiAV.UseSystemPasswordChar = false;
            // 
            // materialLabel17
            // 
            this.materialLabel17.AutoSize = true;
            this.materialLabel17.Depth = 0;
            this.materialLabel17.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel17.Location = new System.Drawing.Point(269, 237);
            this.materialLabel17.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel17.Name = "materialLabel17";
            this.materialLabel17.Size = new System.Drawing.Size(91, 19);
            this.materialLabel17.TabIndex = 113;
            this.materialLabel17.Text = "Roll Former:";
            // 
            // exiRF
            // 
            this.exiRF.Depth = 0;
            this.exiRF.Enabled = false;
            this.exiRF.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.exiRF.Hint = "";
            this.exiRF.Location = new System.Drawing.Point(367, 237);
            this.exiRF.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiRF.Name = "exiRF";
            this.exiRF.PasswordChar = '\0';
            this.exiRF.SelectedText = "";
            this.exiRF.SelectionLength = 0;
            this.exiRF.SelectionStart = 0;
            this.exiRF.Size = new System.Drawing.Size(108, 23);
            this.exiRF.TabIndex = 112;
            this.exiRF.UseSystemPasswordChar = false;
            // 
            // exiPPT
            // 
            this.exiPPT.Depth = 0;
            this.exiPPT.Enabled = false;
            this.exiPPT.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.exiPPT.Hint = "";
            this.exiPPT.Location = new System.Drawing.Point(367, 284);
            this.exiPPT.MouseState = MaterialSkin.MouseState.HOVER;
            this.exiPPT.Name = "exiPPT";
            this.exiPPT.PasswordChar = '\0';
            this.exiPPT.SelectedText = "";
            this.exiPPT.SelectionLength = 0;
            this.exiPPT.SelectionStart = 0;
            this.exiPPT.Size = new System.Drawing.Size(108, 23);
            this.exiPPT.TabIndex = 111;
            this.exiPPT.UseSystemPasswordChar = false;
            // 
            // materialLabel15
            // 
            this.materialLabel15.AutoSize = true;
            this.materialLabel15.Depth = 0;
            this.materialLabel15.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel15.Location = new System.Drawing.Point(211, 284);
            this.materialLabel15.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel15.Name = "materialLabel15";
            this.materialLabel15.Size = new System.Drawing.Size(150, 19);
            this.materialLabel15.TabIndex = 110;
            this.materialLabel15.Text = "Producto Terminado:";
            // 
            // PantallaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 657);
            this.ControlBox = false;
            this.Controls.Add(this.ExisMI);
            this.Controls.Add(this.cantidad);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.PrecioPro);
            this.Controls.Add(this.materialLabel13);
            this.Controls.Add(this.materialLabel12);
            this.Controls.Add(this.NombrePro);
            this.Controls.Add(this.CodigoPro);
            this.Controls.Add(this.materialLabel11);
            this.Controls.Add(this.materialLabel10);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Busqueda);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PantallaProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.PantallaProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox Busqueda;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialSingleLineTextField ExisMI;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiPLA;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiSP;
        private MaterialSkin.Controls.MaterialLabel materialLabel7;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiIGS;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiLP;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialSingleLineTextField ExiMAT;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiCD;
        private MaterialSkin.Controls.MaterialLabel materialLabel9;
        private MaterialSkin.Controls.MaterialLabel materialLabel10;
        private MaterialSkin.Controls.MaterialLabel materialLabel11;
        private MaterialSkin.Controls.MaterialSingleLineTextField CodigoPro;
        private MaterialSkin.Controls.MaterialSingleLineTextField NombrePro;
        private MaterialSkin.Controls.MaterialLabel materialLabel12;
        private System.Windows.Forms.DataGridViewTextBoxColumn cidproducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Peso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPRECIO1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPRECIO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPRECIO3;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPRECIO6;
        private MaterialSkin.Controls.MaterialLabel materialLabel13;
        private MaterialSkin.Controls.MaterialSingleLineTextField PrecioPro;
        private System.Windows.Forms.TextBox cantidad;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiMP;
        private MaterialSkin.Controls.MaterialLabel materialLabel16;
        private MaterialSkin.Controls.MaterialLabel materialLabel18;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiAV;
        private MaterialSkin.Controls.MaterialLabel materialLabel17;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiRF;
        private MaterialSkin.Controls.MaterialSingleLineTextField exiPPT;
        private MaterialSkin.Controls.MaterialLabel materialLabel15;
    }
}