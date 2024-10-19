namespace Dismatsuc
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmbsucursal = new System.Windows.Forms.ComboBox();
            this.dgvproductos = new System.Windows.Forms.DataGridView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dgvcod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvnom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvexi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgveced = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvpdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvproductos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sucursal Salida";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sucursal Entrada";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(23, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "CEDIS";
            // 
            // cmbsucursal
            // 
            this.cmbsucursal.FormattingEnabled = true;
            this.cmbsucursal.Items.AddRange(new object[] {
            "MATRIZ",
            "PORTILLO",
            "SALAZAR",
            "SAN PEDRO"});
            this.cmbsucursal.Location = new System.Drawing.Point(137, 42);
            this.cmbsucursal.Name = "cmbsucursal";
            this.cmbsucursal.Size = new System.Drawing.Size(121, 21);
            this.cmbsucursal.TabIndex = 0;
            this.cmbsucursal.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dgvproductos
            // 
            this.dgvproductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvproductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcod,
            this.dgvnom,
            this.dgvcla,
            this.dgvcde,
            this.dgvcds,
            this.dgvexi,
            this.dgvcan,
            this.dgveced,
            this.dgvcem,
            this.dgvpdd});
            this.dgvproductos.Location = new System.Drawing.Point(8, 99);
            this.dgvproductos.Name = "dgvproductos";
            this.dgvproductos.Size = new System.Drawing.Size(1012, 426);
            this.dgvproductos.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(274, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(62, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "% a Surtir";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(342, 42);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(62, 20);
            this.textBox3.TabIndex = 2;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(438, 42);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(51, 17);
            this.radioButton1.TabIndex = 8;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Excel";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(489, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(63, 17);
            this.radioButton2.TabIndex = 9;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Pantalla";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 532);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Sin Existencia CEDIS";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 561);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Existencia Insuficiente";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(507, 535);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(75, 20);
            this.textBox4.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(467, 537);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Piezas";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(621, 537);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(75, 20);
            this.textBox5.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(599, 539);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Kg";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(902, 563);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "Salir";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(902, 534);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 23);
            this.button4.TabIndex = 16;
            this.button4.Text = "Imprimir";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // dgvcod
            // 
            this.dgvcod.DataPropertyName = "codigo";
            this.dgvcod.HeaderText = "Codigo";
            this.dgvcod.Name = "dgvcod";
            this.dgvcod.Width = 80;
            // 
            // dgvnom
            // 
            this.dgvnom.DataPropertyName = "nombre";
            this.dgvnom.HeaderText = "Nombre";
            this.dgvnom.Name = "dgvnom";
            this.dgvnom.Width = 300;
            // 
            // dgvcla
            // 
            this.dgvcla.HeaderText = "Clasif";
            this.dgvcla.Name = "dgvcla";
            this.dgvcla.Width = 50;
            // 
            // dgvcde
            // 
            this.dgvcde.HeaderText = "Capacidad Espacio";
            this.dgvcde.Name = "dgvcde";
            this.dgvcde.Width = 75;
            // 
            // dgvcds
            // 
            this.dgvcds.HeaderText = "Capacidad Surtido";
            this.dgvcds.Name = "dgvcds";
            this.dgvcds.Width = 75;
            // 
            // dgvexi
            // 
            this.dgvexi.DataPropertyName = "exisuc";
            this.dgvexi.HeaderText = "Existencia";
            this.dgvexi.Name = "dgvexi";
            this.dgvexi.Width = 75;
            // 
            // dgvcan
            // 
            this.dgvcan.HeaderText = "Cantidad";
            this.dgvcan.Name = "dgvcan";
            this.dgvcan.Width = 75;
            // 
            // dgveced
            // 
            this.dgveced.DataPropertyName = "existencia";
            this.dgveced.HeaderText = "Existencia Cedis";
            this.dgveced.Name = "dgveced";
            this.dgveced.Width = 75;
            // 
            // dgvcem
            // 
            this.dgvcem.HeaderText = "Cedis en Matriz";
            this.dgvcem.Name = "dgvcem";
            this.dgvcem.Width = 75;
            // 
            // dgvpdd
            // 
            this.dgvpdd.HeaderText = "% Desabasto";
            this.dgvpdd.Name = "dgvpdd";
            this.dgvpdd.Width = 75;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 598);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvproductos);
            this.Controls.Add(this.cmbsucursal);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distribucion de Materiales de Sucursales";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvproductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cmbsucursal;
        private System.Windows.Forms.DataGridView dgvproductos;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcod;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvnom;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcla;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcde;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcds;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvexi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcan;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgveced;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvpdd;
    }
}

