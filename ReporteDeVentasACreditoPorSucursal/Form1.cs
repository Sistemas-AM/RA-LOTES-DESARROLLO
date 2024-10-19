using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ReporteDeVentasACreditoPorSucursal
{
    public partial class Form1 : Form

    {
        public float totalCheque = 0;
        public float totalEfectivo = 0;
        public float totalTc = 0;
        public float totalTd = 0;
        public float totalTransfer = 0;
        public float total = 0;
        public float subTotal = 0;
        public float totalDiasAnteriores = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



            // TODO: esta línea de código carga datos en la tabla 'reportesAMSAFoliosSucursalCredito.folios' Puede moverla o quitarla según sea necesario.
            try
            {

                this.foliosTableAdapter.Fill(this.reportesAMSAFoliosSucursalCredito.folios);
            }
            catch (SqlException)
            {

            }


            textBox3.Text = totalCheque.ToString();
            textBox4.Text = totalEfectivo.ToString();
            textBox5.Text = totalTc.ToString();
            textBox6.Text = totalTd.ToString();
            textBox7.Text = totalTransfer.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
            SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);

            totalCheque = 0;
            totalEfectivo = 0;
            totalTc = 0;
            totalTd = 0;
            totalTransfer = 0;
            textBox3.Text = totalCheque.ToString();
            textBox4.Text = totalEfectivo.ToString();
            textBox5.Text = totalTc.ToString();
            textBox6.Text = totalTd.ToString();
            textBox7.Text = totalTransfer.ToString();

            dataGridView6.DataSource = null;
            //label3.Text = comboBox1.SelectedValue.ToString();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConnection2;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select cfecha,ctotal,cfolio,admClientes.ccodigocliente,admClientes.crazonsocial from admDocumentos inner join admClientes " +
                 "on admDocumentos.CIDCLIENTEPROVEEDOR = admClientes.CIDCLIENTEPROVEEDOR where admDocumentos.cfecha = '" + (dateTimePicker1.Value.ToString("MM/dd/yyyy")) + "' and CIDDOCUMENTODE = 5 " +
                "and(cidconceptodocumento =" + (int)comboBox1.SelectedValue + ") and ccancelado = 0 ";
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            dataGridView6.DataSource = dtRecord;


            SqlCommand sqlcmd2 = new SqlCommand();
            sqlcmd2.Connection = sqlConnection3;
            sqlcmd2.CommandType = CommandType.Text;
            sqlcmd2.CommandText = "SELECT iddevocfd FROM folios WHERE idcredito = " + comboBox1.SelectedValue;
            SqlDataAdapter sqlDataAdap2 = new SqlDataAdapter(sqlcmd2);
            DataTable dtlabel = new DataTable();
            sqlDataAdap2.Fill(dtlabel);
            label3.Text = dtlabel.Rows[0][0].ToString();



            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSetCobraant.cobraant' Puede moverla o quitarla según sea necesario.
            dataGridView5.DataSource = this.reportesAMSADataSetCobraant.cobraant;
            this.cobraantTableAdapter.Fill(this.reportesAMSADataSetCobraant.cobraant, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
            // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet1.faccob' Puede moverla o quitarla según sea necesario.
            dataGridView3.DataSource = this.reportesAMSADataSet1.faccob;
            this.faccobTableAdapter.Fill(this.reportesAMSADataSet1.faccob, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
            if (this.reportesAMSADataSet1.faccob.Count > 0)
            {

                foreach (DataGridViewRow dr in dataGridView3.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        totalCheque = totalCheque + float.Parse(dr.Cells[4].Value.ToString());
                        totalEfectivo = totalEfectivo + float.Parse(dr.Cells[5].Value.ToString());
                        totalTc = totalTc + float.Parse(dr.Cells[6].Value.ToString());
                        totalTd = totalTd + float.Parse(dr.Cells[7].Value.ToString());
                        totalTransfer = totalTransfer + float.Parse(dr.Cells[8].Value.ToString());
                    }
                }
                textBox3.Text = totalCheque.ToString();
                textBox4.Text = totalEfectivo.ToString();
                textBox5.Text = totalTc.ToString();
                textBox6.Text = totalTd.ToString();
                textBox7.Text = totalTransfer.ToString();
            }
            // Esta línea de código carga datos en la tabla 'reportesAMSACobranzaDataSet.cobranza' Puede moverla o quitarla según sea necesario.
            this.cobranzaTableAdapter.Fill(this.reportesAMSACobranzaDataSet.cobranza, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
            this.cobrafacTableAdapter.Fill(this.reportesAMSADataSet.cobrafac, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
            if (this.reportesAMSACobranzaDataSet.cobranza.Count > 0 || this.reportesAMSADataSet.cobrafac.Count > 0)
            {
                total = 0;
                this.dataGridView4.Visible = true;
                this.dataGridView1.Visible = false;

                this.dataGridView4.DataSource = this.reportesAMSACobranzaDataSet.cobranza;
                this.dataGridView2.DataSource = this.reportesAMSADataSet.cobrafac;
                // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet.cobrafac' Puede moverla o quitarla según sea necesario.
                this.cobrafacTableAdapter.Fill(this.reportesAMSADataSet.cobrafac, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
                foreach (DataGridViewRow dr in dataGridView2.Rows)
                {
                    if (dr.Cells[0].Value != null)
                    {
                        total = total + float.Parse(dr.Cells[2].Value.ToString());
                    }
                }
                textBox2.Text = total.ToString();
            }
            else
            {
                total = 0;
                textBox2.Text = total.ToString();

                this.dataGridView1.Visible = true;
                this.dataGridView4.Visible = false;
                // Esta línea de código carga datos en la tabla 'adACEROS_MEXICODataSet.admDocumentos' Puede moverla o quitarla según sea necesario.
                this.admDocumentosTableAdapter.Fill(this.adACEROS_MEXICODataSet.admDocumentos, (int)comboBox1.SelectedValue, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")));
                //label3.Text = this.adACEROS_MEXICODataSet.admDocumentos.Count.ToString();
                this.dataGridView2.DataSource = null;
                this.dataGridView2.Rows.Clear();
            }


        }


        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);

            List<Cobranza> cobranzasOri = new List<Cobranza>();
            List<Cobranza> cobranzas = new List<Cobranza>();
            if (dataGridView1.Visible == true)
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {


                    if (dr.Cells[0].Value != null)
                    {
                        if (Convert.ToInt32(dr.Cells[7].Value) == 1)
                        {
                            Cobranza itemOri = new Cobranza();
                            itemOri.Sucursal = comboBox1.Text;
                            itemOri.Cfecha = DateTime.Parse(dr.Cells[1].Value.ToString());
                            itemOri.Cseriedo01 = dr.Cells[2].Value.ToString();
                            itemOri.Cfolio = Convert.ToInt32(dr.Cells[3].Value);
                            itemOri.Ccodigoc01 = dr.Cells[4].Value.ToString();
                            itemOri.Crazonso01 = dr.Cells[5].Value.ToString();
                            itemOri.Ctotal = float.Parse(dr.Cells[6].Value.ToString());
                            itemOri.Original = Convert.ToInt32(dr.Cells[7].Value);
                            itemOri.Cr = Convert.ToInt32(dr.Cells[8].Value);
                            if (dr.Cells[9].Value != null)
                            {
                                itemOri.Orden = dr.Cells[9].Value.ToString();
                            }
                            else
                            {
                                itemOri.Orden = "Sin definir";
                            }
                            if (dr.Cells[10].Value != null)
                            {
                                    itemOri.Fecord = DateTime.Parse(dr.Cells[10].Value.ToString());
                            }
                            else
                            {
                                itemOri.Fecord = DateTime.Now;
                                //itemOri.Fecord = DateTime.Parse("MM/dd/yyyy");
                            }
                            itemOri.Firmada = Convert.ToInt32(dr.Cells[11].Value);
                            cobranzasOri.Add(itemOri);
                        }
                        else
                        {
                            Cobranza item = new Cobranza();
                            item.Sucursal = comboBox1.Text;
                            item.Cfecha = DateTime.Parse(dr.Cells[1].Value.ToString());
                            item.Cseriedo01 = dr.Cells[2].Value.ToString();
                            item.Cfolio = Convert.ToInt32(dr.Cells[3].Value);
                            item.Ccodigoc01 = dr.Cells[4].Value.ToString();
                            item.Crazonso01 = dr.Cells[5].Value.ToString();
                            item.Ctotal = float.Parse(dr.Cells[6].Value.ToString());
                            item.Original = Convert.ToInt32(dr.Cells[7].Value);
                            item.Cr = Convert.ToInt32(dr.Cells[8].Value);

                            if (dr.Cells[9].Value != null)
                            {
                                item.Orden = dr.Cells[9].Value.ToString();
                            }
                            else
                            {
                                item.Orden = "Sin definir";
                            }
                            if (dr.Cells[10].Value != null)
                            {
                                item.Fecord = DateTime.Parse(dr.Cells[10].Value.ToString());

                            }
                            else
                            {
                                item.Fecord = DateTime.Now;
                            }
                            item.Firmada = Convert.ToInt32(dr.Cells[11].Value);
                            cobranzas.Add(item);
                        }


                    }
                }
            }
            else
            {
                foreach (DataGridViewRow dr in dataGridView4.Rows)
                {


                    if (dr.Cells[0].Value != null)
                    {
                        if (Convert.ToInt32(dr.Cells[6].Value) == 1)
                        {
                            Cobranza itemOri = new Cobranza();
                            itemOri.Sucursal = comboBox1.Text;
                            itemOri.Cfecha = DateTime.Parse(dr.Cells[0].Value.ToString());
                            itemOri.Cseriedo01 = dr.Cells[1].Value.ToString();
                            itemOri.Cfolio = Convert.ToInt32(dr.Cells[2].Value);
                            itemOri.Ccodigoc01 = dr.Cells[3].Value.ToString();
                            itemOri.Crazonso01 = dr.Cells[4].Value.ToString();
                            itemOri.Ctotal = float.Parse(dr.Cells[5].Value.ToString());
                            itemOri.Original = Convert.ToInt32(dr.Cells[6].Value);
                            itemOri.Cr = Convert.ToInt32(dr.Cells[7].Value);
                            if (dr.Cells[8].Value != null)
                            {
                                itemOri.Orden = dr.Cells[8].Value.ToString();
                            }
                            else
                            {
                                itemOri.Orden = "Sin definir";
                            }
                            if (dr.Cells[9].Value != null)
                            {
                                itemOri.Fecord = DateTime.Parse(dr.Cells[9].Value.ToString());

                            }
                            else
                            {
                                itemOri.Fecord = DateTime.Now;
                            }
                            itemOri.Firmada = Convert.ToInt32(dr.Cells[10].Value);
                            cobranzasOri.Add(itemOri);
                        }
                        else
                        {
                            Cobranza item = new Cobranza();
                            item.Sucursal = comboBox1.Text;
                            item.Cfecha = DateTime.Parse(dr.Cells[0].Value.ToString());
                            item.Cseriedo01 = dr.Cells[1].Value.ToString();
                            item.Cfolio = Convert.ToInt32(dr.Cells[2].Value);
                            item.Ccodigoc01 = dr.Cells[3].Value.ToString();
                            item.Crazonso01 = dr.Cells[4].Value.ToString();
                            item.Ctotal = float.Parse(dr.Cells[5].Value.ToString());
                            item.Original = Convert.ToInt32(dr.Cells[6].Value);
                            item.Cr = Convert.ToInt32(dr.Cells[7].Value);

                            if (dr.Cells[8].Value != null)
                            {
                                item.Orden = dr.Cells[8].Value.ToString();
                            }
                            else
                            {
                                item.Orden = "Sin definir";
                            }
                            if (dr.Cells[9].Value != null)
                            {
                                item.Fecord = DateTime.Parse(dr.Cells[9].Value.ToString());

                            }
                            else
                            {
                                item.Fecord = DateTime.Now;
                            }
                            item.Firmada = Convert.ToInt32(dr.Cells[10].Value);
                            cobranzas.Add(item);
                        }


                    }
                }
            }


            if (cobranzasOri.Count > 0)
            {
                this.cobranzaTableAdapter.Fill(this.reportesAMSACobranzaDataSet.cobranza, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
                if (this.reportesAMSACobranzaDataSet.cobranza.Count > 0)
                {

                }
                else
                {

                    foreach (Cobranza cobranza in cobranzasOri)
                    {
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader reader;
                        cmd.CommandText = "INSERT INTO cobranza(sucursal, cfecha, cseriedo01, cfolio, crazonso01, ctotal, original, cr, ccodigoc01, orden, fecord, firmada)" +
                            "VALUES ('" + cobranza.Sucursal + "','" + cobranza.Cfecha.ToString("MM/dd/yyyy 00:00:00.000") + "','" + cobranza.Cseriedo01 + "','" + cobranza.Cfolio + "','" + cobranza.Crazonso01 + "','" + cobranza.Ctotal + "','" + Convert.ToInt32(cobranza.Original) + "','" + Convert.ToInt32(cobranza.Cr) + "','" + cobranza.Ccodigoc01 + "','" + cobranza.Orden + "','" + cobranza.Fecord.ToString("MM/dd/yyyy 00:00:00.000") + "','" + Convert.ToInt32(cobranza.Firmada) + "')";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;

                        sqlConnection1.Open();

                        reader = cmd.ExecuteReader();

                        sqlConnection1.Close();
                    }
                }

                //textBox1.Text = cobranzasOri.Count.ToString();
                // Esta línea de código carga datos en la tabla 'reportesAMSACobranzaDataSet.cobranza' Puede moverla o quitarla según sea necesario.
                this.dataGridView1.Visible = false;
                this.dataGridView4.Visible = true;
                this.dataGridView4.DataSource = this.reportesAMSACobranzaDataSet.cobranza;
                this.cobranzaTableAdapter.Fill(this.reportesAMSACobranzaDataSet.cobranza, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
            }
            if (cobranzas.Count > 0)
            {
                this.cobrafacTableAdapter.Fill(this.reportesAMSADataSet.cobrafac, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
                if (this.reportesAMSADataSet.cobrafac.Count > 0)
                {
                    foreach (Cobranza cobranza in cobranzas)
                    {
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader reader;
                        cmd.CommandText = "DELETE FROM cobranza " +
                            "WHERE cfolio = " + cobranza.Cfolio;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;

                        sqlConnection1.Open();

                        reader = cmd.ExecuteReader();

                        sqlConnection1.Close();
                    }
                    foreach (Cobranza cobranza in cobranzas)
                    {
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader reader;
                        cmd.CommandText = "INSERT INTO cobrafac(sucursal, fecha, factura, proveedor, importe)" +
                            "VALUES ('" + cobranza.Sucursal + "','" + cobranza.Cfecha.ToString("MM/dd/yyyy 00:00:00.000") + "','" + cobranza.Cfolio + "','" + cobranza.Crazonso01 + "','" + cobranza.Ctotal + "')";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;

                        sqlConnection1.Open();

                        reader = cmd.ExecuteReader();

                        sqlConnection1.Close();
                    }
                }
                else
                {
                    foreach (Cobranza cobranza in cobranzas)
                    {
                        SqlCommand cmd = new SqlCommand();
                        SqlDataReader reader;
                        cmd.CommandText = "INSERT INTO cobrafac(sucursal, fecha, factura, proveedor, importe)" +
                            "VALUES ('" + cobranza.Sucursal + "','" + cobranza.Cfecha.ToString("MM/dd/yyyy") + "','" + cobranza.Cfolio + "','" + cobranza.Crazonso01 + "','" + cobranza.Ctotal + "')";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;

                        sqlConnection1.Open();

                        reader = cmd.ExecuteReader();

                        sqlConnection1.Close();
                    }
                }
                this.dataGridView1.Visible = false;
                this.dataGridView4.Visible = true;
                this.dataGridView4.DataSource = this.reportesAMSACobranzaDataSet.cobranza;
                this.cobranzaTableAdapter.Fill(this.reportesAMSACobranzaDataSet.cobranza, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
                this.dataGridView2.DataSource = this.reportesAMSADataSet.cobrafac;
                // TODO: esta línea de código carga datos en la tabla 'reportesAMSADataSet.cobrafac' Puede moverla o quitarla según sea necesario.
                this.cobrafacTableAdapter.Fill(this.reportesAMSADataSet.cobrafac, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
            }
            total = 0;
            foreach (DataGridViewRow dr in dataGridView2.Rows)
            {
                if (dr.Cells[0].Value != null)
                {
                    total = total + float.Parse(dr.Cells[2].Value.ToString());
                }
            }
            textBox2.Text = total.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView4.Visible = false;

            SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandText = "DELETE FROM cobranza " +
                "WHERE cfecha = '" + dateTimePicker1.Value.ToString("MM/dd/yyyy 00:00:00.000") + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();

            sqlConnection1.Close();
            SqlCommand cmd2 = new SqlCommand();
            SqlDataReader reader2;
            cmd2.CommandText = "DELETE FROM cobrafac " +
                "WHERE fecha = '" + dateTimePicker1.Value.ToString("MM/dd/yyyy 00:00:00.000") + "'";
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader2 = cmd2.ExecuteReader();

            sqlConnection1.Close();
            total = 0;
            textBox2.Text = total.ToString();
            //dataGridView1.DataSource = null;
            //dataGridView1.DataSource = reportesAMSACobranzaDataSet.cobranza;
            this.cobranzaTableAdapter.Fill(this.reportesAMSACobranzaDataSet.cobranza, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);
            this.cobrafacTableAdapter.Fill(this.reportesAMSADataSet.cobrafac, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000")), comboBox1.Text);





        }


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks that are not in our 
            if (e.ColumnIndex == dataGridView3.Columns["chequeDataGridViewTextBoxColumn"].Index && e.RowIndex >= 0)
            {
                DatosBanco.dbFactura = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                DatosBanco.dbProveedor = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                using (Form2 cheque = new Form2(false, dateTimePicker1.Value, comboBox1.Text))
                {
                    cheque.ShowDialog(this);
                }
                dataGridView3.Rows[e.RowIndex].Cells[4].Value = String.Format("{0:0.00}", DatosBanco.dbImporte);
                dataGridView3.Rows[e.RowIndex].Cells[5].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[6].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[7].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[8].Value = 0;
            }
            if (e.ColumnIndex == dataGridView3.Columns["tcDataGridViewTextBoxColumn"].Index && e.RowIndex >= 0)
            {
                DatosBanco.dbFactura = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                DatosBanco.dbProveedor = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                using (Form2 cheque = new Form2(false, dateTimePicker1.Value, comboBox1.Text))
                {
                    cheque.ShowDialog(this);
                }
                dataGridView3.Rows[e.RowIndex].Cells[6].Value = String.Format("{0:0.00}", DatosBanco.dbImporte);
                dataGridView3.Rows[e.RowIndex].Cells[4].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[5].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[7].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[8].Value = 0;
            }
            if (e.ColumnIndex == dataGridView3.Columns["tdDataGridViewTextBoxColumn"].Index && e.RowIndex >= 0)
            {
                DatosBanco.dbFactura = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                DatosBanco.dbProveedor = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                using (Form2 cheque = new Form2(false, dateTimePicker1.Value, comboBox1.Text))
                {
                    cheque.ShowDialog(this);
                }
                dataGridView3.Rows[e.RowIndex].Cells[6].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[4].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[5].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[7].Value = String.Format("{0:0.00}", DatosBanco.dbImporte);
                dataGridView3.Rows[e.RowIndex].Cells[8].Value = 0;
            }
            if (e.ColumnIndex == dataGridView3.Columns["transferDataGridViewTextBoxColumn"].Index && e.RowIndex >= 0)
            {
                DatosBanco.dbFactura = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                DatosBanco.dbProveedor = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                using (Form2 cheque = new Form2(false, dateTimePicker1.Value, comboBox1.Text))
                {
                    cheque.ShowDialog(this);
                }
                dataGridView3.Rows[e.RowIndex].Cells[6].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[4].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[5].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[7].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[8].Value = String.Format("{0:0.00}", DatosBanco.dbImporte);
            }
            if (e.ColumnIndex == dataGridView3.Columns["sucursalDataGridViewTextBoxColumn"].Index && e.RowIndex >= 0)
            {
                dataGridView3.Rows[e.RowIndex].Cells[9].Value = dateTimePicker1.Value.ToString("MM/dd/yyyy 00:00:00.000");
                dataGridView3.Rows[e.RowIndex].Cells[1].Value = comboBox1.Text;
            }
            if (e.ColumnIndex == dataGridView3.Columns["efectivoDataGridViewTextBoxColumn"].Index && e.RowIndex >= 0)
            {
                DatosBanco.dbFactura = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
                DatosBanco.dbProveedor = dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString();
                using (Form2 cheque = new Form2(true, dateTimePicker1.Value, comboBox1.Text))
                {
                    cheque.ShowDialog(this);
                }
                dataGridView3.Rows[e.RowIndex].Cells[4].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[5].Value = String.Format("{0:0.00}", DatosBanco.dbImporte);
                dataGridView3.Rows[e.RowIndex].Cells[6].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[7].Value = 0;
                dataGridView3.Rows[e.RowIndex].Cells[8].Value = 0;
            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            SqlCommand cmd2 = new SqlCommand();
            SqlDataReader reader2;
            cmd2.CommandText = "DELETE FROM faccob " +
                "WHERE fecha = '" + dateTimePicker1.Value.ToString("MM/dd/yyyy 00:00:00.000") + "'";
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = sqlConnection3;

            sqlConnection3.Open();

            reader2 = cmd2.ExecuteReader();

            sqlConnection3.Close();

            totalCheque = 0;
            totalEfectivo = 0;
            totalTc = 0;
            totalTd = 0;
            totalTransfer = 0;
            foreach (DataGridViewRow dr in dataGridView3.Rows)
            {
                if (dr.Cells[0].Value != null)
                {
                    totalCheque = totalCheque + float.Parse(dr.Cells[4].Value.ToString());
                    totalEfectivo = totalEfectivo + float.Parse(dr.Cells[5].Value.ToString());
                    totalTc = totalTc + float.Parse(dr.Cells[6].Value.ToString());
                    totalTd = totalTd + float.Parse(dr.Cells[7].Value.ToString());
                    totalTransfer = totalTransfer + float.Parse(dr.Cells[8].Value.ToString());
                }
            }
            textBox3.Text = totalCheque.ToString();
            textBox4.Text = totalEfectivo.ToString();
            textBox5.Text = totalTc.ToString();
            textBox6.Text = totalTd.ToString();
            textBox7.Text = totalTransfer.ToString();
            foreach (DataGridViewRow dr in dataGridView3.Rows)
            {
                if (dr.Cells[0].Value != null)
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandText = "INSERT INTO faccob(sucursal, fecha, factura, proveedor, cheque, efectivo, tc, td, transfer) VALUES " +
                        "('" + dr.Cells[1].Value.ToString() + "','" + DateTime.Parse(dr.Cells[9].Value.ToString()).ToString("MM/dd/yyyy 00:00:00.000") + "'," + (int)dr.Cells[2].Value + ",'" + dr.Cells[3].Value.ToString() + "'," + float.Parse(dr.Cells[4].Value.ToString()) + "," + float.Parse(dr.Cells[5].Value.ToString()) + "," + float.Parse(dr.Cells[6].Value.ToString()) + "," + float.Parse(dr.Cells[7].Value.ToString()) + "," + float.Parse(dr.Cells[8].Value.ToString()) + ")";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection3;

                    sqlConnection3.Open();

                    reader = cmd.ExecuteReader();

                    sqlConnection3.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            cmd.CommandText = "DELETE FROM cobraant " +
                "WHERE fecha = '" + dateTimePicker1.Value.ToString("MM/dd/yyyy 00:00:00.000") + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();

            reader = cmd.ExecuteReader();

            sqlConnection.Close();

            foreach (DataGridViewRow dr in dataGridView5.Rows)
            {
                if (dr.Cells[0].Value != null)
                {
                    SqlCommand cmd2 = new SqlCommand();
                    SqlDataReader reader2;
                    cmd2.CommandText = "INSERT INTO cobraant(fecha, factura, proveedor, importe, sucursal) VALUES " +
                        "('" + dateTimePicker1.Value.ToString("MM/dd/yyyy 00:00:00.000") + "'," + (int)dr.Cells[1].Value + ",'" + dr.Cells[2].Value.ToString() + "','" + float.Parse(dr.Cells[4].Value.ToString()) + "','" + dr.Cells[3].Value.ToString() + "')";
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = sqlConnection;

                    sqlConnection.Open();

                    reader2 = cmd2.ExecuteReader();

                    sqlConnection.Close();
                }
            }


            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);


            // Indicamos donde vamos a guardar el documento
            //Ventana para guardar el archivo
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "Guardar como...";
            dialog.Filter = "Text files (*.PDF)|*pdf|All files (*.*)|*.*";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Guardado en: " + dialog.FileName);
            }

            //Guarda el archivo en la ruta y crea el archivo
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new System.IO.FileStream(dialog.FileName + ".PDF", FileMode.Create));

            // Abrimos el archivo
            doc.Open();


            // Creamos el tipo de Font que vamos utilizar
            //iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font fhead = FontFactory.GetFont("TimesnewRoman", 11, BaseColor.BLACK);
            iTextSharp.text.Font fheaders = FontFactory.GetFont("TimesnewRoman", 8, BaseColor.BLACK);
            iTextSharp.text.Font ftable = FontFactory.GetFont("TimesnewRoman", 8, BaseColor.BLACK);


            // Escribimos el encabezamiento en el documento
            Paragraph ptitulo = new Paragraph("REPORTE DE VENTAS POR SUCURSAL A CREDITO", fhead);
            ptitulo.Alignment = Element.ALIGN_CENTER;

            Paragraph pfechaSubtitulo = new Paragraph("FECHA: " + System.DateTime.Now.ToString("MM/dd/yyyy"), fhead);
            pfechaSubtitulo.Alignment = Element.ALIGN_CENTER;

            doc.Add(ptitulo);
            doc.Add(pfechaSubtitulo);

            doc.Add(Chunk.NEWLINE);

            doc.Add(new Paragraph("SUCURSAL: " + getNombreSucursal(comboBox1.Text), ftable));
            doc.Add(new Paragraph("CREDITOS DEL DIA", ftable));
            doc.Add(new Paragraph(" ", ftable));
            // Creamos la tabla
            PdfPTable pdfTable = new PdfPTable(dataGridView4.ColumnCount);

            pdfTable.SetWidths(new float[] { 4.5f, 3f, 4f, 4f, 10f, 2f, 2f, 5f, 4f, 5f, 4f });
            pdfTable.DefaultCell.Padding = 1;

            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 0;


            //Añadimos a la tabla los headers
            PdfPCell hfecha = new PdfPCell(new Phrase("FECHA", fheaders));
            hfecha.BorderWidth = 0;
            hfecha.BorderWidthBottom = 0.75f;

            PdfPCell hserie = new PdfPCell(new Phrase("SERIE", fheaders));
            hserie.BorderWidth = 0;
            hserie.BorderWidthBottom = 0.75f;

            PdfPCell hfactura = new PdfPCell(new Phrase("FACTURA", fheaders));
            hfactura.BorderWidth = 0;
            hfactura.BorderWidthBottom = 0.75f;

            PdfPCell hcodigo = new PdfPCell(new Phrase("CODIGO", fheaders));
            hcodigo.BorderWidth = 0;
            hcodigo.BorderWidthBottom = 0.75f;

            PdfPCell hcliente = new PdfPCell(new Phrase("CLIENTE", fheaders));
            hcliente.BorderWidth = 0;
            hcliente.BorderWidthBottom = 0.75f;
            hcliente.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell hori = new PdfPCell(new Phrase("ORI", fheaders));
            hori.BorderWidth = 0;
            hori.BorderWidthBottom = 0.75f;

            PdfPCell hcr = new PdfPCell(new Phrase("CR", fheaders));
            hcr.BorderWidth = 0;
            hcr.BorderWidthBottom = 0.75f;

            PdfPCell himporte = new PdfPCell(new Phrase("IMPORTE", fheaders));
            himporte.BorderWidth = 0;
            himporte.BorderWidthBottom = 0.75f;

            PdfPCell horden = new PdfPCell(new Phrase("ORDEN", fheaders));
            horden.BorderWidth = 0;
            horden.BorderWidthBottom = 0.75f;

            PdfPCell hfecord = new PdfPCell(new Phrase("FEC ORD", fheaders));
            hfecord.BorderWidth = 0;
            hfecord.BorderWidthBottom = 0.75f;



            PdfPCell hfirmada = new PdfPCell(new Phrase("FIRMADA", fheaders));
            hfirmada.BorderWidth = 0;
            hfirmada.BorderWidthBottom = 0.75f;

            pdfTable.AddCell(hfecha);
            pdfTable.AddCell(hserie);
            pdfTable.AddCell(hfactura);
            pdfTable.AddCell(hcodigo);
            pdfTable.AddCell(hcliente);
            pdfTable.AddCell(hori);
            pdfTable.AddCell(hcr);
            pdfTable.AddCell(himporte);
            pdfTable.AddCell(horden);
            pdfTable.AddCell(hfecord);

            pdfTable.AddCell(hfirmada);

            subTotal = 0;
            //Añadimos a la tabla el contenido de las columnas
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {

                if (row.Cells[0].Value != null)
                {

                    pdfTable.AddCell(new Phrase(DateTime.Parse(row.Cells[0].Value.ToString()).ToString("dd/MM/yyyy"), ftable));
                }
                if (row.Cells[1].Value != null)
                {
                    pdfTable.AddCell(new Phrase(row.Cells[1].Value.ToString(), ftable));
                }
                if (row.Cells[2].Value != null)
                {
                    pdfTable.AddCell(new Phrase(row.Cells[2].Value.ToString(), ftable));
                }
                if (row.Cells[3].Value != null)
                {
                    pdfTable.AddCell(new Phrase(row.Cells[3].Value.ToString(), ftable));
                }
                if (row.Cells[4].Value != null)
                {
                    pdfTable.AddCell(new Phrase(row.Cells[4].Value.ToString(), ftable));
                }

                if (row.Cells[6].Value != null)
                {
                    if (Convert.ToInt32(row.Cells[6].Value.ToString()) == 1)
                    {
                        pdfTable.AddCell(new Phrase("  X", ftable));
                    }
                    else
                    {
                        pdfTable.AddCell(new Phrase("  ", ftable));
                    }
                }
                if (row.Cells[7].Value != null)
                {
                    if (Convert.ToInt32(row.Cells[7].Value.ToString()) == 1)
                    {
                        pdfTable.AddCell(new Phrase(" X", ftable));
                    }
                    else
                    {
                        pdfTable.AddCell(new Phrase("  ", ftable));
                    }
                }
                if (row.Cells[5].Value != null)
                {
                    pdfTable.AddCell(new Phrase("$" + String.Format("{0:0.00}", row.Cells[5].Value), ftable));
                    subTotal = subTotal + float.Parse(row.Cells[5].Value.ToString());
                }
                if (row.Cells[8].Value != null)
                {
                    pdfTable.AddCell(new Phrase(row.Cells[8].Value.ToString(), ftable));
                }
                if (row.Cells[9].Value != null)
                {
                    pdfTable.AddCell(new Phrase(DateTime.Parse(row.Cells[9].Value.ToString()).ToString("dd/MM/yyyy"), ftable));
                }
                if (row.Cells[10].Value != null)
                {

                    if (Convert.ToInt32(row.Cells[10].Value.ToString()) == 1)
                    {

                        pdfTable.AddCell(new Phrase("      S", ftable));
                    }
                    else
                    {

                        pdfTable.AddCell(new Phrase("      N", ftable));
                    }

                }


            }

            doc.Add(pdfTable);

            Paragraph pSubtotalLine = new Paragraph("____________", fhead);
            Paragraph pSubtotal = new Paragraph("Sub total ==> $" + String.Format("{0:0.00}", subTotal), fhead);
            pSubtotalLine.Alignment = Element.ALIGN_RIGHT;
            pSubtotal.Alignment = Element.ALIGN_RIGHT;
            doc.Add(pSubtotalLine);
            doc.Add(pSubtotal);

            Paragraph pTituloTabla2 = new Paragraph("FACTURAS QUE NOS ESTAMOS QUEDANDO PARA SU COBRANZA", fheaders);
            pTituloTabla2.Alignment = Element.ALIGN_LEFT;
            doc.Add(pTituloTabla2);


            //Creamos la segunda tabla
            PdfPTable pdfTable2 = new PdfPTable(dataGridView2.ColumnCount);

            pdfTable2.SetWidths(new float[] { 4f, 10f, 4f });
            pdfTable2.DefaultCell.Padding = 1;

            pdfTable2.WidthPercentage = 60;
            pdfTable2.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable2.DefaultCell.BorderWidth = 0;


            //Añadimos a la tabla los headers
            PdfPCell hfacturat2 = new PdfPCell(new Phrase("FACTURA", fheaders));
            hfacturat2.BorderWidth = 0;
            hfacturat2.BorderWidthBottom = 0.75f;

            PdfPCell hcliente2 = new PdfPCell(new Phrase("CLIENTE", fheaders));
            hcliente2.BorderWidth = 0;
            hcliente2.BorderWidthBottom = 0.75f;

            PdfPCell himporte2 = new PdfPCell(new Phrase("IMPORTE", fheaders));
            himporte2.BorderWidth = 0;
            himporte2.BorderWidthBottom = 0.75f;
            himporte2.HorizontalAlignment = Element.ALIGN_RIGHT;

            pdfTable2.AddCell(hfacturat2);
            pdfTable2.AddCell(hcliente2);
            pdfTable2.AddCell(himporte2);

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    pdfTable2.AddCell(new Phrase(row.Cells[0].Value.ToString(), ftable));
                }
                if (row.Cells[1].Value != null)
                {
                    pdfTable2.AddCell(new Phrase(row.Cells[1].Value.ToString(), ftable));
                }
                if (row.Cells[2].Value != null)
                {
                    PdfPCell cellCantidadest2 = new PdfPCell(new Phrase(String.Format("{0:0.00}", row.Cells[2].Value), ftable));
                    cellCantidadest2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellCantidadest2.BorderWidth = 0;
                    cellCantidadest2.Padding = 1;
                    pdfTable2.AddCell(cellCantidadest2);
                }
            }

            doc.Add(pdfTable2);

            //Para el total de la tabla 2
            PdfPTable tablaTotal2 = new PdfPTable(1);
            tablaTotal2.WidthPercentage = 60;
            tablaTotal2.DefaultCell.Padding = 1;
            tablaTotal2.HorizontalAlignment = Element.ALIGN_LEFT;
            tablaTotal2.DefaultCell.BorderWidth = 0;


            PdfPCell celltotal2 = new PdfPCell(new Phrase("Total ==> " + String.Format("{0:0.00}", total), ftable));
            celltotal2.HorizontalAlignment = Element.ALIGN_RIGHT;
            celltotal2.BorderWidth = 0;
            celltotal2.Padding = 1;
            tablaTotal2.AddCell(celltotal2);

            doc.Add(tablaTotal2);

            Paragraph pTotalCreditos = new Paragraph("Total Creditos $" + String.Format("{0:0.00}", (total + subTotal)), fhead);
            pTotalCreditos.Alignment = Element.ALIGN_RIGHT;
            doc.Add(pTotalCreditos);

            Paragraph pCobranzaDelDia = new Paragraph("COBRANZA DEL DIA", fheaders);
            pCobranzaDelDia.Alignment = Element.ALIGN_CENTER;
            doc.Add(pCobranzaDelDia);

            //Creamos la TERCER tabla
            PdfPTable pdfTable3 = new PdfPTable(7);

            pdfTable3.SetWidths(new float[] { 4f, 10f, 4f, 4f, 4f, 4f, 4f });
            pdfTable3.DefaultCell.Padding = 1;

            pdfTable3.WidthPercentage = 100;
            pdfTable3.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable3.DefaultCell.BorderWidth = 0;


            //Añadimos a la tabla los headers
            PdfPCell hfacturat3 = new PdfPCell(new Phrase("FACTURA", fheaders));
            hfacturat3.BorderWidth = 0;
            hfacturat3.BorderWidthBottom = 0.75f;

            PdfPCell hcliente3 = new PdfPCell(new Phrase("CLIENTE", fheaders));
            hcliente3.BorderWidth = 0;
            hcliente3.BorderWidthBottom = 0.75f;

            PdfPCell hcheque3 = new PdfPCell(new Phrase("CHEQUE", fheaders));
            hcheque3.BorderWidth = 0;
            hcheque3.BorderWidthBottom = 0.75f;
            hcheque3.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell hefectivo3 = new PdfPCell(new Phrase("EFECTIVO", fheaders));
            hefectivo3.BorderWidth = 0;
            hefectivo3.BorderWidthBottom = 0.75f;
            hefectivo3.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell htc3 = new PdfPCell(new Phrase("TC", fheaders));
            htc3.BorderWidth = 0;
            htc3.BorderWidthBottom = 0.75f;
            htc3.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell htd3 = new PdfPCell(new Phrase("TD", fheaders));
            htd3.BorderWidth = 0;
            htd3.BorderWidthBottom = 0.75f;
            htd3.HorizontalAlignment = Element.ALIGN_RIGHT;

            PdfPCell htransfer3 = new PdfPCell(new Phrase("TRANSFER", fheaders));
            htransfer3.BorderWidth = 0;
            htransfer3.BorderWidthBottom = 0.75f;
            htransfer3.HorizontalAlignment = Element.ALIGN_RIGHT;

            pdfTable3.AddCell(hfacturat3);
            pdfTable3.AddCell(hcliente3);
            pdfTable3.AddCell(hcheque3);
            pdfTable3.AddCell(hefectivo3);
            pdfTable3.AddCell(htc3);
            pdfTable3.AddCell(htd3);
            pdfTable3.AddCell(htransfer3);

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    pdfTable3.AddCell(new Phrase(row.Cells[2].Value.ToString(), ftable));
                }
                if (row.Cells[3].Value != null)
                {
                    pdfTable3.AddCell(new Phrase(row.Cells[3].Value.ToString(), ftable));
                }
                if (row.Cells[4].Value != null)
                {
                    PdfPCell cellCantidadest2 = new PdfPCell(new Phrase(String.Format("{0:0.00}", row.Cells[4].Value), ftable));
                    cellCantidadest2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellCantidadest2.BorderWidth = 0;
                    cellCantidadest2.Padding = 1;
                    pdfTable3.AddCell(cellCantidadest2);
                }
                if (row.Cells[5].Value != null)
                {
                    PdfPCell cellCantidadest2 = new PdfPCell(new Phrase(String.Format("{0:0.00}", row.Cells[5].Value), ftable));
                    cellCantidadest2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellCantidadest2.BorderWidth = 0;
                    cellCantidadest2.Padding = 1;
                    pdfTable3.AddCell(cellCantidadest2);
                }
                if (row.Cells[6].Value != null)
                {
                    PdfPCell cellCantidadest2 = new PdfPCell(new Phrase(String.Format("{0:0.00}", row.Cells[6].Value), ftable));
                    cellCantidadest2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellCantidadest2.BorderWidth = 0;
                    cellCantidadest2.Padding = 1;
                    pdfTable3.AddCell(cellCantidadest2);
                }
                if (row.Cells[7].Value != null)
                {
                    PdfPCell cellCantidadest2 = new PdfPCell(new Phrase(String.Format("{0:0.00}", row.Cells[7].Value), ftable));
                    cellCantidadest2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellCantidadest2.BorderWidth = 0;
                    cellCantidadest2.Padding = 1;
                    pdfTable3.AddCell(cellCantidadest2);
                }
                if (row.Cells[8].Value != null)
                {
                    PdfPCell cellCantidadest2 = new PdfPCell(new Phrase(String.Format("{0:0.00}", row.Cells[8].Value), ftable));
                    cellCantidadest2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellCantidadest2.BorderWidth = 0;
                    cellCantidadest2.Padding = 1;
                    pdfTable3.AddCell(cellCantidadest2);
                }
            }

            doc.Add(pdfTable3);

            //Totales
            PdfPTable pdftotales = new PdfPTable(7);
            pdftotales.SetWidths(new float[] { 4f, 10f, 4f, 4f, 4f, 4f, 4f });
            pdftotales.DefaultCell.Padding = 1;

            pdftotales.WidthPercentage = 100;
            pdftotales.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftotales.DefaultCell.BorderWidth = 0;


            PdfPCell ct4blank1 = new PdfPCell(new Phrase("  ", fheaders));
            ct4blank1.BorderWidth = 0;

            PdfPCell ct4totales = new PdfPCell(new Phrase("                                         Totales ==>", fheaders));
            ct4totales.BorderWidth = 0;

            PdfPCell ct4cheque = new PdfPCell(new Phrase(String.Format("{0:0.00}", totalCheque), fheaders));
            ct4cheque.HorizontalAlignment = Element.ALIGN_RIGHT;
            ct4cheque.BorderWidth = 0;
            ct4cheque.Padding = 1;

            PdfPCell ct4efectivo = new PdfPCell(new Phrase(String.Format("{0:0.00}", totalEfectivo), fheaders));
            ct4efectivo.BorderWidth = 0;
            ct4efectivo.HorizontalAlignment = Element.ALIGN_RIGHT;
            ct4efectivo.Padding = 1;

            PdfPCell ct4tc = new PdfPCell(new Phrase(String.Format("{0:0.00}", totalTc), fheaders));
            ct4tc.BorderWidth = 0;
            ct4tc.HorizontalAlignment = Element.ALIGN_RIGHT;
            ct4tc.Padding = 1;

            PdfPCell ct4td = new PdfPCell(new Phrase(String.Format("{0:0.00}", totalTd), fheaders));
            ct4td.BorderWidth = 0;
            ct4td.HorizontalAlignment = Element.ALIGN_RIGHT;
            ct4td.Padding = 1;

            PdfPCell ct4transfer = new PdfPCell(new Phrase(String.Format("{0:0.00}", totalTransfer), fheaders));
            ct4transfer.BorderWidth = 0;
            ct4transfer.HorizontalAlignment = Element.ALIGN_RIGHT;
            ct4transfer.Padding = 1;

            pdftotales.AddCell(ct4blank1);
            pdftotales.AddCell(ct4totales);
            pdftotales.AddCell(ct4cheque);
            pdftotales.AddCell(ct4efectivo);
            pdftotales.AddCell(ct4tc);
            pdftotales.AddCell(ct4td);
            pdftotales.AddCell(ct4transfer);

            doc.Add(pdftotales);


            doc.Add(Chunk.NEWLINE);


            Paragraph ptitulo4 = new Paragraph("FACTURAS DE CREDITO QUE ESTAMOS ENVIANDO DE DIAS ANTERIORES", fheaders);
            ptitulo4.Alignment = Element.ALIGN_LEFT;
            doc.Add(ptitulo4);
            // Creamos la cuarta tabla
            PdfPTable pdfTable4 = new PdfPTable(4);

            pdfTable4.SetWidths(new float[] { 4f, 10f, 4f, 4f });
            pdfTable4.DefaultCell.Padding = 1;

            pdfTable4.WidthPercentage = 60;
            pdfTable4.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable4.DefaultCell.BorderWidth = 0;

            PdfPCell hfacturat4 = new PdfPCell(new Phrase("FACTURA", fheaders));
            hfacturat4.BorderWidth = 0;
            hfacturat4.BorderWidthBottom = 0.75f;

            PdfPCell hcliente4 = new PdfPCell(new Phrase("CLIENTE", fheaders));
            hcliente4.BorderWidth = 0;
            hcliente4.BorderWidthBottom = 0.75f;

            PdfPCell hsucursal4 = new PdfPCell(new Phrase("SUCURSAL", fheaders));
            hsucursal4.BorderWidth = 0;
            hsucursal4.BorderWidthBottom = 0.75f;
            hsucursal4.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell himporte4 = new PdfPCell(new Phrase("IMPORTE", fheaders));
            himporte4.BorderWidth = 0;
            himporte4.BorderWidthBottom = 0.75f;
            himporte4.HorizontalAlignment = Element.ALIGN_RIGHT;

            pdfTable4.AddCell(hfacturat4);
            pdfTable4.AddCell(hcliente4);
            pdfTable4.AddCell(hsucursal4);
            pdfTable4.AddCell(himporte4);
            totalDiasAnteriores = 0;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    pdfTable4.AddCell(new Phrase(row.Cells[1].Value.ToString(), ftable));
                }
                if (row.Cells[2].Value != null)
                {
                    pdfTable4.AddCell(new Phrase(row.Cells[2].Value.ToString(), ftable));
                }
                if (row.Cells[3].Value != null)
                {
                    pdfTable4.AddCell(new Phrase(row.Cells[3].Value.ToString(), ftable));
                }
                if (row.Cells[4].Value != null)
                {
                    PdfPCell cellCantidadest2 = new PdfPCell(new Phrase(String.Format("{0:0.00}", row.Cells[4].Value), ftable));
                    cellCantidadest2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellCantidadest2.BorderWidth = 0;
                    cellCantidadest2.Padding = 1;
                    pdfTable4.AddCell(cellCantidadest2);
                    totalDiasAnteriores = totalDiasAnteriores + float.Parse(row.Cells[4].Value.ToString());
                }
            }

            pdfTable4.AddCell(new Phrase("  ", ftable));
            pdfTable4.AddCell(new Phrase("                                   Total ==>", ftable));
            pdfTable4.AddCell(new Phrase("  ", ftable));
            PdfPCell cellTotal4 = new PdfPCell(new Phrase(String.Format("{0:0.00}", totalDiasAnteriores), ftable));
            cellTotal4.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellTotal4.BorderWidth = 0;
            cellTotal4.Padding = 1;
            pdfTable4.AddCell(cellTotal4);

            doc.Add(pdfTable4);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);


            Paragraph ptitulo5 = new Paragraph("DEVOLUCIONES SOBRE VENTA DEL DIA", fheaders);
            ptitulo5.Alignment = Element.ALIGN_LEFT;
            doc.Add(ptitulo5);

            PdfPTable pdfTable5 = new PdfPTable(3);
            pdfTable5.SetWidths(new float[] { 4f, 10f, 4f });
            pdfTable5.DefaultCell.Padding = 1;

            pdfTable5.WidthPercentage = 60;
            pdfTable5.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable5.DefaultCell.BorderWidth = 0;

            PdfPCell hfactura5 = new PdfPCell(new Phrase("NO DEVO", fheaders));
            hfactura5.BorderWidth = 0;


            PdfPCell hcliente5 = new PdfPCell(new Phrase("CLIENTE", fheaders));
            hcliente5.BorderWidth = 0;


            PdfPCell hmonto = new PdfPCell(new Phrase("MONTO", fheaders));
            hmonto.BorderWidth = 0;
            hmonto.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfTable5.AddCell(hfactura5);
            pdfTable5.AddCell(hcliente5);
            pdfTable5.AddCell(hmonto);

            foreach (DataGridViewRow row in dataGridView6.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    pdfTable5.AddCell(new Phrase(row.Cells[2].Value.ToString(), ftable));
                }
                if (row.Cells[4].Value != null)
                {
                    pdfTable5.AddCell(new Phrase(row.Cells[4].Value.ToString(), ftable));
                }
                if (row.Cells[1].Value != null)
                {
                    PdfPCell cellCantidadest2 = new PdfPCell(new Phrase(String.Format("{0:0.00}", row.Cells[1].Value), ftable));
                    cellCantidadest2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellCantidadest2.BorderWidth = 0;
                    cellCantidadest2.Padding = 1;
                    pdfTable5.AddCell(cellCantidadest2);

                }
            }

            doc.Add(pdfTable5);

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            Paragraph ptitulo6 = new Paragraph("OBSERVACIONES", fheaders);
            ptitulo6.Alignment = Element.ALIGN_LEFT;
            doc.Add(ptitulo6);

            Paragraph ptitulo7 = new Paragraph(textBox1.Text, fheaders);
            ptitulo7.Alignment = Element.ALIGN_LEFT;
            doc.Add(ptitulo7);

            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);


            PdfPTable pdfTable7 = new PdfPTable(3);

            pdfTable7.SetWidths(new float[] { 6f, 2f, 6f, });
            pdfTable7.DefaultCell.Padding = 1;

            pdfTable7.WidthPercentage = 40;
            pdfTable7.HorizontalAlignment = Element.ALIGN_RIGHT;
            pdfTable7.DefaultCell.BorderWidth = 0;

            PdfPCell hfirma1 = new PdfPCell(new Phrase("        ELABORO        ", fheaders));
            hfirma1.HorizontalAlignment = Element.ALIGN_CENTER;
            hfirma1.BorderWidth = 0;
            hfirma1.BorderWidthTop = 0.75f;

            PdfPCell hfirma2 = new PdfPCell(new Phrase("        REVISO        ", fheaders));
            hfirma2.HorizontalAlignment = Element.ALIGN_CENTER;
            hfirma2.BorderWidth = 0;
            hfirma2.BorderWidthTop = 0.75f;

            PdfPCell hblank2 = new PdfPCell(new Phrase("     ", fheaders));
            hblank2.BorderWidth = 0;

            pdfTable7.AddCell(hfirma1);
            pdfTable7.AddCell(hblank2);
            pdfTable7.AddCell(hfirma2);


            doc.Add(pdfTable7);

            doc.Close();
            writer.Close();

        }

        private string getNombreSucursal(string codigoSucursal)
        {
            string nombreSucursal = "";
            SqlConnection sqlConnection = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT sucnom FROM folios " +
                "WHERE sucursal = '" + codigoSucursal + "'";


            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(cmd);
            DataTable dtlabel = new DataTable();
            sqlDataAdap.Fill(dtlabel);
            nombreSucursal = dtlabel.Rows[0][0].ToString();

            return nombreSucursal;
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView5.Columns["sucursalDataGridViewTextBoxColumn1"].Index && e.RowIndex >= 0)
            {
                dataGridView5.Rows[e.RowIndex].Cells[5].Value = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000");
                dataGridView5.Rows[e.RowIndex].Cells[3].Value = comboBox1.Text;
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {


        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {


        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Favor de ingresar valores correctos y llenar todos los campos");
        }

        private void dataGridView5_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Favor de ingresar valores correctos y llenar todos los campos");
        }

        private void dataGridView4_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView4.Columns[e.ColumnIndex].Name == "fecordDataGridViewTextBoxColumn")
            //{
            //    try
            //    {
            //        DateTime time = DateTime.Parse(dataGridView4.Rows[dataGridView4.CurrentRow.Index].Cells[9].Value.ToString());
            //        dataGridView4.Rows[dataGridView4.CurrentRow.Index].Cells[10].Value = time.ToString("MM/dd/yyyy");
            //    }
            //    catch (Exception)
            //    {
            //        //MessageBox.Show("Ingresa un formato de fecha valido");
            //        dataGridView4.Rows[dataGridView4.CurrentRow.Index].Cells[10].Value = "";
            //    }
            //}

        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
        //    if (dataGridView1.Columns[e.ColumnIndex].Name == "FecOrd")
        //    {
        //        try
        //        {
        //            DateTime time = DateTime.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[10].Value.ToString());
        //            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[10].Value = time.ToString("MM/dd/yyyy");
        //    }
        //        catch (Exception)
        //    {
        //        //MessageBox.Show("Ingresa un formato de fecha valido");
        //        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[10].Value = "";
        //    }
        //}
        }
    }
}
