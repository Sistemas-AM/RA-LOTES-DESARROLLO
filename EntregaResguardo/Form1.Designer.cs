namespace EntregaResguardo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbsuc = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtfolio = new System.Windows.Forms.TextBox();
            this.txtfact = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcte = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtfact = new System.Windows.Forms.DateTimePicker();
            this.dtent = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvcod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvdes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvcan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvsdo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvsal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvobs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbsuc
            // 
            this.cmbsuc.FormattingEnabled = true;
            this.cmbsuc.Location = new System.Drawing.Point(12, 33);
            this.cmbsuc.Name = "cmbsuc";
            this.cmbsuc.Size = new System.Drawing.Size(169, 21);
            this.cmbsuc.TabIndex = 0;
            this.cmbsuc.SelectedIndexChanged += new System.EventHandler(this.cmbsuc_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sucursal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(612, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Folio";
            // 
            // txtfolio
            // 
            this.txtfolio.Enabled = false;
            this.txtfolio.Location = new System.Drawing.Point(584, 88);
            this.txtfolio.Name = "txtfolio";
            this.txtfolio.Size = new System.Drawing.Size(100, 20);
            this.txtfolio.TabIndex = 3;
            // 
            // txtfact
            // 
            this.txtfact.Location = new System.Drawing.Point(12, 88);
            this.txtfact.Name = "txtfact";
            this.txtfact.Size = new System.Drawing.Size(100, 20);
            this.txtfact.TabIndex = 5;
            this.txtfact.TextChanged += new System.EventHandler(this.txtfact_TextChanged);
            this.txtfact.Enter += new System.EventHandler(this.txtfact_Enter);
            this.txtfact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtfact_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "No Factura";
            // 
            // txtcte
            // 
            this.txtcte.Location = new System.Drawing.Point(118, 88);
            this.txtcte.Name = "txtcte";
            this.txtcte.Size = new System.Drawing.Size(252, 20);
            this.txtcte.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Cliente";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(373, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Facturado";
            // 
            // dtfact
            // 
            this.dtfact.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtfact.Location = new System.Drawing.Point(376, 87);
            this.dtfact.Name = "dtfact";
            this.dtfact.Size = new System.Drawing.Size(98, 20);
            this.dtfact.TabIndex = 9;
            // 
            // dtent
            // 
            this.dtent.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtent.Location = new System.Drawing.Point(480, 88);
            this.dtent.Name = "dtent";
            this.dtent.Size = new System.Drawing.Size(98, 20);
            this.dtent.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(477, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Entrega";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvcod,
            this.dgvdes,
            this.dgvcan,
            this.dgvsdo,
            this.dgvsal,
            this.dgvobs});
            this.dataGridView1.Location = new System.Drawing.Point(11, 127);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(754, 259);
            this.dataGridView1.TabIndex = 12;
            // 
            // dgvcod
            // 
            this.dgvcod.HeaderText = "Codigo";
            this.dgvcod.Name = "dgvcod";
            this.dgvcod.ReadOnly = true;
            this.dgvcod.Width = 75;
            // 
            // dgvdes
            // 
            this.dgvdes.HeaderText = "Descripción";
            this.dgvdes.Name = "dgvdes";
            this.dgvdes.ReadOnly = true;
            this.dgvdes.Width = 250;
            // 
            // dgvcan
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle1.NullValue = "0.00";
            this.dgvcan.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvcan.HeaderText = "Facturado";
            this.dgvcan.Name = "dgvcan";
            this.dgvcan.ReadOnly = true;
            this.dgvcan.Width = 60;
            // 
            // dgvsdo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle2.NullValue = "0.00";
            this.dgvsdo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvsdo.HeaderText = "Entregado";
            this.dgvsdo.Name = "dgvsdo";
            this.dgvsdo.ReadOnly = true;
            this.dgvsdo.Width = 60;
            // 
            // dgvsal
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "0.00";
            this.dgvsal.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvsal.HeaderText = "Saldo";
            this.dgvsal.Name = "dgvsal";
            this.dgvsal.Width = 60;
            // 
            // dgvobs
            // 
            this.dgvobs.HeaderText = "Observa";
            this.dgvobs.Name = "dgvobs";
            this.dgvobs.Width = 200;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(452, 407);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(528, 407);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "Imprimir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(603, 407);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "Salir";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(218, 33);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(58, 17);
            this.radioButton1.TabIndex = 16;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Credito";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(280, 33);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(65, 17);
            this.radioButton2.TabIndex = 17;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Contado";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 445);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dtent);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtfact);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtcte);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtfact);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtfolio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbsuc);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entrega de Material en Resguardo";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbsuc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtfolio;
        private System.Windows.Forms.TextBox txtfact;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtcte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtfact;
        private System.Windows.Forms.DateTimePicker dtent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcod;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvdes;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcan;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvsdo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvsal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvobs;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}

