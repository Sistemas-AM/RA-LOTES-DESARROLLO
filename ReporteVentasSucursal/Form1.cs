using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReporteVentasSucursal
{
    public partial class Form1 : Form
    {
        SqlConnection sqlAceros = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlAmsa = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int count = 0;
        int count2 = 0;
        int count3 = 0;
        int count4 = 0;
        int count5 = 0;
        int countbajar = 0;
        int cuentaexiste = 0;

        public float totalCheque = 0;
        public float totalEfectivo = 0;
        public float totalTc = 0;
        public float totalTd = 0;
        public float totalTransfer = 0;
        public Form1()
        {
            InitializeComponent();
            cargaSucu();
            //if (Principal.Variablescompartidas.sucural == "AUDITOR" || Principal.Variablescompartidas.sucursalcorta == "ME" || Principal.Variablescompartidas.sucursalcorta == "PLA")
            if (Principal.Variablescompartidas.sucural == "AUDITOR")

            {
                    comboBox1.Enabled = true; 
            }

            //else if (Principal.Variablescompartidas.sucursalcorta == "PLA" || Principal.Variablescompartidas.sucursalcorta == "ME")
            else if (Principal.Variablescompartidas.Perfil_id == "36")
            {
                comboBox1.Enabled = true;
                comboBox1.Text = Principal.Variablescompartidas.sucursalcorta;
            }

            else
            {
                comboBox1.Enabled = false;
                comboBox1.Text = Principal.Variablescompartidas.sucursalcorta;
            }

        }


    public void cargaSucu()
        {
            sqlAmsa.Open();

            if (Principal.Variablescompartidas.Perfil_id == "36")
            {
                SqlCommand sc = new SqlCommand("Select sucursal, idcredito from folios where idalmacen in (6, 17)", sqlAmsa);
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("sucursal", typeof(string));

                dt.Load(reader);

                comboBox1.ValueMember = "idcredito";
                comboBox1.DisplayMember = "sucursal";
                comboBox1.DataSource = dt;

                sqlAmsa.Close();
            }
            else
            {
                SqlCommand sc = new SqlCommand("Select sucursal, idcredito from folios", sqlAmsa);

                //SqlCommand sc = new SqlCommand("Select sucursal, idcredito from folios where idalmacen in (6, 17)", sqlAmsa);
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("sucursal", typeof(string));

                dt.Load(reader);

                comboBox1.ValueMember = "idcredito";
                comboBox1.DisplayMember = "sucursal";
                comboBox1.DataSource = dt;

                sqlAmsa.Close();
            }
        }

        public void cobranza()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            cmd.CommandText = @"
            select* from cobranza where sucursal = '"+comboBox1.Text+"' and cfecha = '"+fecha.ToString("MM/dd/yyyy")+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlAmsa;
            sqlAmsa.Open();
            reader = cmd.ExecuteReader();

            //try
            //{
            // Data is accessible through the DataReader object here.

            if (reader.Read() == true)
            {
                reader.Close();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    //textBox1.Text = reader["ALGO"].ToString();
                    dataGridView1.Rows[count].Cells[0].Value = reader["cfecha"].ToString();
                    dataGridView1.Rows[count].Cells[1].Value = reader["cseriedo01"].ToString();
                    dataGridView1.Rows[count].Cells[2].Value = reader["cfolio"].ToString();
                    dataGridView1.Rows[count].Cells[3].Value = reader["ccodigoc01"].ToString();
                    dataGridView1.Rows[count].Cells[4].Value = reader["crazonso01"].ToString();
                    dataGridView1.Rows[count].Cells[5].Value = reader["ctotal"].ToString();

                    if (reader["original"].ToString() == "1")
                    {
                        dataGridView1.Rows[count].Cells[Column18.Name].Value = true;
                    }
                    if (reader["cr"].ToString() == "1")
                    {
                        dataGridView1.Rows[count].Cells[Column19.Name].Value = true;
                    }
                    if (reader["firmada"].ToString() == "1")
                    {
                        dataGridView1.Rows[count].Cells[Column22.Name].Value = true;
                    }

                    dataGridView1.Rows[count].Cells[8].Value = reader["orden"].ToString();
                    dataGridView1.Rows[count].Cells[9].Value = reader["fecord"].ToString();

                    count++;

                }
            }else
            {
                cuentaexiste += 1;
              
            }
                
            //}
            //catch (Exception)
            //{


            //}
            sqlAmsa.Close();
        }

        private void cobrafac()
        {
            //select factura, proveedor, importe from cobrafac where sucursal = '' and fecha = ''

            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            //int count = 0; Este se agrega al inicio del programa
            count2 = 0;
            cmd.CommandText = @"select factura, proveedor, importe from cobrafac where sucursal = '" + comboBox1.Text + "' and fecha = '" + fecha.ToString("MM/dd/yyyy") + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlAmsa;
            sqlAmsa.Open();
            reader = cmd.ExecuteReader();

            //try
            //{
            // Data is accessible through the DataReader object here.

            if (reader.Read() == true)
            {
                reader.Close();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView2.Rows.Add();
                    //textBox1.Text = reader["ALGO"].ToString();
                    dataGridView2.Rows[count2].Cells[0].Value = reader["factura"].ToString();
                    dataGridView2.Rows[count2].Cells[1].Value = reader["proveedor"].ToString();
                    dataGridView2.Rows[count2].Cells[2].Value = reader["importe"].ToString();
                   

                    count2++;

                }
            }
            else
            {
                cuentaexiste += 1;
             
            }

            //}
            //catch (Exception)
            //{


            //}
            sqlAmsa.Close();
        }

        private void faccob()
        {

            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            //int count = 0; Este se agrega al inicio del programa
            count4 = 0;
            cmd.CommandText = @"select factura, proveedor, cheque, efectivo, tc, td, transfer from faccob where sucursal = '" + comboBox1.Text + "' and fecha = '" + fecha.ToString("MM/dd/yyyy") + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlAmsa;
            sqlAmsa.Open();
            reader = cmd.ExecuteReader();

            //try
            //{
            // Data is accessible through the DataReader object here.

            if (reader.Read() == true)
            {
                reader.Close();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView3.Rows.Add();
                    //textBox1.Text = reader["ALGO"].ToString();
                    dataGridView3.Rows[count4].Cells[0].Value = reader["factura"].ToString();
                    dataGridView3.Rows[count4].Cells[1].Value = reader["proveedor"].ToString();
                    dataGridView3.Rows[count4].Cells[2].Value = reader["cheque"].ToString();
                    dataGridView3.Rows[count4].Cells[3].Value = reader["efectivo"].ToString();
                    dataGridView3.Rows[count4].Cells[4].Value = reader["tc"].ToString();
                    dataGridView3.Rows[count4].Cells[5].Value = reader["td"].ToString();
                    dataGridView3.Rows[count4].Cells[6].Value = reader["transfer"].ToString();
                    
                    count4++;

                }
            }
            else
            {
                cuentaexiste += 1;

            }

            //}
            //catch (Exception)
            //{


            //}
            sqlAmsa.Close();
        }

        private void cargaant()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            //int count = 0; Este se agrega al inicio del programa
            count5 = 0;
            cmd.CommandText = @"select factura, proveedor, importe from cobraant where sucursal = '" + comboBox1.Text + "' and fecha = '" + fecha.ToString("MM/dd/yyyy") + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlAmsa;
            sqlAmsa.Open();
            reader = cmd.ExecuteReader();

            //try
            //{
            // Data is accessible through the DataReader object here.

            if (reader.Read() == true)
            {
                reader.Close();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView4.Rows.Add();
                    //textBox1.Text = reader["ALGO"].ToString();
                    dataGridView4.Rows[count5].Cells[0].Value = reader["factura"].ToString();
                    dataGridView4.Rows[count5].Cells[1].Value = reader["proveedor"].ToString();
                    dataGridView4.Rows[count5].Cells[2].Value = reader["importe"].ToString();
                    count5++;

                }
            }
            else
            {
                cuentaexiste += 1;

            }

            //}
            //catch (Exception)
            //{


            //}
            sqlAmsa.Close();
        }

        private void carga()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            count3 = 0;
            cmd.CommandText = @"select CIDDOCUMENTO, CFECHA, CSERIEDOCUMENTO, CCODIGOCLIENTE, admClientes.CRAZONSOCIAL, CTOTAL,
            CFOLIO from admDocumentos 
            inner join admClientes on admDocumentos.CIDCLIENTEPROVEEDOR = admClientes.CIDCLIENTEPROVEEDOR
            where admDocumentos.CIDCONCEPTODOCUMENTO = '" + comboBox1.SelectedValue.ToString() +"' and CFECHA = '"+fecha.ToString(Principal.Variablescompartidas.FormatoFecha)+ "' and CCANCELADO = '0'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlAceros;
            sqlAceros.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.

                reader.Close();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    //textBox1.Text = reader["ALGO"].ToString();
                    dataGridView1.Rows[count].Cells[0].Value = reader["CFECHA"].ToString();
                    dataGridView1.Rows[count].Cells[1].Value = reader["CSERIEDOCUMENTO"].ToString();
                    dataGridView1.Rows[count].Cells[2].Value = reader["CFOLIO"].ToString();
                    dataGridView1.Rows[count].Cells[3].Value = reader["CCODIGOCLIENTE"].ToString();
                    dataGridView1.Rows[count].Cells[4].Value = reader["CRAZONSOCIAL"].ToString();
                    dataGridView1.Rows[count].Cells[5].Value = reader["CTOTAL"].ToString();
                    
                    count++;

                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar los datos");

            }
            sqlAceros.Close();
        }
        private void validaorden()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            int countvalida = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (dataGridView1.Rows[countvalida].Cells["Column20"].Value == null || dataGridView1.Rows[countvalida].Cells["Column21"].Value == null)
                {
                    dataGridView1.Rows[countvalida].Cells["Column20"].Value = "Sin definir";
                    dataGridView1.Rows[countvalida].Cells["Column21"].Value = fecha.ToString();
                    countvalida += 1;
                }else
                {
                    countvalida += 1;
                }
            }
        }
        private void bajar()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            countbajar = 0;
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //Convert.ToInt32(dr.Cells[7].Value) == 1
                    if (Convert.ToInt32(dataGridView1.Rows[countbajar].Cells["column18"].Value) == 0 && Convert.ToInt32(dataGridView1.Rows[countbajar].Cells["column19"].Value) == 0)
                    {
                        dataGridView2.Rows.Add();
                        //textBox1.Text = reader["ALGO"].ToString();
                        dataGridView2.Rows[count2].Cells[0].Value = dataGridView1.Rows[countbajar].Cells["column14"].Value.ToString();
                        dataGridView2.Rows[count2].Cells[1].Value = dataGridView1.Rows[countbajar].Cells["column16"].Value.ToString();
                        dataGridView2.Rows[count2].Cells[2].Value = dataGridView1.Rows[countbajar].Cells["column17"].Value.ToString();

                        //insertacobrafac();
                        //deletecobranza();
                       
                        
                        count2 += 1;
                        countbajar += 1;
                    }
                    else
                    {
                        countbajar += 1;
                    }
                }
                validaorden();
            }
            catch (Exception)
            {


            }


        }

        private void insertacobrafac()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string sql = "insert into cobrafac (sucursal, fecha, factura, proveedor, importe) values ('"+ comboBox1.Text + "', '"+ fecha.ToString("MM/dd/yyyy") + "', '"+ row.Cells["column1"].Value.ToString() + "', '"+ row.Cells["column2"].Value.ToString() + "', '"+ row.Cells["column3"].Value.ToString() + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlAmsa);
                //cmd.Parameters.AddWithValue("@param1", comboBox1.Text); //Para grabar algo de un textbox
                //cmd.Parameters.AddWithValue("@param2", fecha.ToString("MM/dd/yyyy")); //Para grabar una columna
                //cmd.Parameters.AddWithValue("@param4", row.Cells["column1"].Value.ToString());
                //cmd.Parameters.AddWithValue("@param5", row.Cells["column2"].Value.ToString());
                //cmd.Parameters.AddWithValue("@param3", row.Cells["column3"].Value.ToString());
                sqlAmsa.Open();
                cmd.ExecuteNonQuery();
                sqlAmsa.Close(); 
            }
        }

        private void insertafaccob()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    string sql = "insert into faccob (sucursal, fecha, factura, proveedor, cheque, efectivo, tc, td, transfer) values(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9)";
                    SqlCommand cmd = new SqlCommand(sql, sqlAmsa);
                    cmd.Parameters.AddWithValue("@param1", comboBox1.Text); //Para grabar algo de un textbox
                    cmd.Parameters.AddWithValue("@param2", fecha.ToString("MM/dd/yyyy")); //Para grabar una columna
                    cmd.Parameters.AddWithValue("@param3", row.Cells["column5"].Value.ToString());
                    cmd.Parameters.AddWithValue("@param4", row.Cells["column6"].Value.ToString());
                    cmd.Parameters.AddWithValue("@param5", row.Cells["column7"].Value.ToString());
                    cmd.Parameters.AddWithValue("@param6", row.Cells["column8"].Value.ToString());
                    cmd.Parameters.AddWithValue("@param7", row.Cells["column9"].Value.ToString());
                    cmd.Parameters.AddWithValue("@param8", row.Cells["column10"].Value.ToString());
                    cmd.Parameters.AddWithValue("@param9", row.Cells["column11"].Value.ToString());
                    sqlAmsa.Open();
                    cmd.ExecuteNonQuery();
                    sqlAmsa.Close();
                    
                }
            }
            catch (NullReferenceException)
            {

                
            }
        }

        private void insertacobraant()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            try
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    string sql = "insert into cobraant (sucursal, fecha, factura, proveedor, importe) values(@param1, @param2, @param3, @param4, @param5)";
                    SqlCommand cmd = new SqlCommand(sql, sqlAmsa);
                    cmd.Parameters.AddWithValue("@param1", comboBox1.Text); //Para grabar algo de un textbox
                    cmd.Parameters.AddWithValue("@param2", fecha.ToString("MM/dd/yyyy")); //Para grabar una columna
                    cmd.Parameters.AddWithValue("@param3", row.Cells["column23"].Value.ToString());
                    cmd.Parameters.AddWithValue("@param4", row.Cells["column24"].Value.ToString());
                    cmd.Parameters.AddWithValue("@param5", row.Cells["column26"].Value.ToString());
                  
                    sqlAmsa.Open();
                    cmd.ExecuteNonQuery();
                    sqlAmsa.Close();
                }
            }
            catch (NullReferenceException)
            {


            }
        }
        private void deletecobraant()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            string sql = @"delete from cobraant where 
            sucursal = '" + comboBox1.Text + "' and fecha = '" + fecha.ToString("MM/dd/yyyy") + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlAmsa);
            sqlAmsa.Open();
            cmd.ExecuteNonQuery();
            sqlAmsa.Close();
        }

        private void deletecobranza()
        {
          
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            string sql = @"delete from cobranza where 
            sucursal = '"+ comboBox1.Text+"' and cfecha = '"+fecha.ToString("MM/dd/yyyy")+"'";
            SqlCommand cmd = new SqlCommand(sql, sqlAmsa);
            sqlAmsa.Open();
            cmd.ExecuteNonQuery();
            sqlAmsa.Close();
                    
            
        }

        private void deletecobrafac()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            string sql = @"delete from cobrafac where 
            sucursal = '" + comboBox1.Text + "' and fecha = '" + fecha.ToString("MM/dd/yyyy") + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlAmsa);
            sqlAmsa.Open();
            cmd.ExecuteNonQuery();
            sqlAmsa.Close();
        }

        private void deletefaccob()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            string sql = @"delete from faccob where 
            sucursal = '" + comboBox1.Text + "' and fecha = '" + fecha.ToString("MM/dd/yyyy") + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlAmsa);
            sqlAmsa.Open();
            cmd.ExecuteNonQuery();
            sqlAmsa.Close();
        }

        private void borrar()
        {
            List<DataGridViewRow> toDelete = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells["column18"].Value) == 0 && Convert.ToInt32(row.Cells["column19"].Value) == 0)
                {
                    toDelete.Add(row);
                }
            }
            foreach (DataGridViewRow row in toDelete)
            {
                dataGridView1.Rows.Remove(row);
                count -= 1;
            }
        }

        private void subir()
        {
            count3 = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
                //int count = 0; Este se agrega al inicio del programa
                
                cmd.CommandText = @"select CIDDOCUMENTO, CFECHA, CSERIEDOCUMENTO, CCODIGOCLIENTE, admClientes.CRAZONSOCIAL, CTOTAL,
                CFOLIO from admDocumentos 
                inner join admClientes on admDocumentos.CIDCLIENTEPROVEEDOR = admClientes.CIDCLIENTEPROVEEDOR
                where admDocumentos.CIDCONCEPTODOCUMENTO = '" + comboBox1.SelectedValue.ToString() + "' and CFECHA = '" + fecha.ToString("MM/dd/yyyy") + "' and admClientes.CRAZONSOCIAL = '" + dataGridView2.Rows[count3].Cells["Column2"].Value.ToString()+ "' and cTotal = '" + dataGridView2.Rows[count3].Cells["Column3"].Value.ToString() + "' and cfolio = '" + dataGridView2.Rows[count3].Cells["Column1"].Value.ToString() + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlAceros;
                sqlAceros.Open();
                reader = cmd.ExecuteReader();

                //try
                //{
                //    // Data is accessible through the DataReader object h

                    if (reader.Read())
                    {
                        dataGridView1.Rows.Add();
                        //textBox1.Text = reader["ALGO"].ToString();
                        dataGridView1.Rows[count].Cells[0].Value = reader["CFECHA"].ToString();
                        dataGridView1.Rows[count].Cells[1].Value = reader["CSERIEDOCUMENTO"].ToString();
                        dataGridView1.Rows[count].Cells[2].Value = reader["CFOLIO"].ToString();
                        dataGridView1.Rows[count].Cells[3].Value = reader["CCODIGOCLIENTE"].ToString();
                        dataGridView1.Rows[count].Cells[4].Value = reader["CRAZONSOCIAL"].ToString();
                        dataGridView1.Rows[count].Cells[5].Value = reader["CTOTAL"].ToString();
                    
                        count++;
                        count3 += 1;

                }
                    
                //}
                //catch (Exception)
                //{
                //    MessageBox.Show("Error al cargar los datos");

                //}
                sqlAceros.Close();
            }
            validaorden();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cuentaexiste = 0;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            cargaant();
            cobranza();
            cobrafac();
            faccob();
            suma();
            if (cuentaexiste >= 4)
            {
                carga();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bajar();
            borrar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            subir();
            dataGridView2.Rows.Clear();
            count2 = 0;
        }

        private void guarda()
        {
            //try
            //{
                DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                    //{
                string sql = @"insert into cobranza (sucursal, cfecha, cseriedo01, cfolio, crazonso01, ctotal, original, cr, ccodigoc01, orden, fecord, firmada) values(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12)";
                SqlCommand cmd = new SqlCommand(sql, sqlAmsa);
                cmd.Parameters.AddWithValue("@param1", comboBox1.Text); //Para grabar algo de un textbox
                cmd.Parameters.AddWithValue("@param2", fecha.ToString("MM/dd/yyyy")); //Para grabar una columna
                cmd.Parameters.AddWithValue("@param3", row.Cells["Column13"].Value.ToString()); //Para grabar una columna
                cmd.Parameters.AddWithValue("@param4", row.Cells["Column14"].Value.ToString()); //Para grabar una columna
                cmd.Parameters.AddWithValue("@param5", row.Cells["Column16"].Value.ToString()); //Para grabar una columna
                cmd.Parameters.AddWithValue("@param6", row.Cells["Column17"].Value.ToString()); //Para grabar una columna


                if (Convert.ToInt32(row.Cells["Column18"].Value) == 1)
                {
                    cmd.Parameters.AddWithValue("@param7", "1"); //Para grabar una columna 
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param7", "0"); //Para grabar una columna 
                }

                if (Convert.ToInt32(row.Cells["Column19"].Value) == 1)
                {
                    cmd.Parameters.AddWithValue("@param8", "1"); //Para grabar una columna 
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param8", "0"); //Para grabar una columna 
                }

                cmd.Parameters.AddWithValue("@param9", row.Cells["Column15"].Value.ToString()); //Para grabar una columna
                cmd.Parameters.AddWithValue("@param10", row.Cells["Column20"].Value.ToString()); //Para grabar una columna
                DateTime fecha2 = DateTime.Parse(row.Cells["Column21"].Value.ToString());
                cmd.Parameters.AddWithValue("@param11", fecha2.ToString("MM/dd/yyyy")); //Para grabar una columna
                if (Convert.ToInt32(row.Cells["Column22"].Value) == 1)
                {
                    cmd.Parameters.AddWithValue("@param12", "1"); //Para grabar una columna 
                }
                else
                {
                    cmd.Parameters.AddWithValue("@param12", "0"); //Para grabar una columna 
                }
                
                    sqlAmsa.Open();
                    cmd.ExecuteNonQuery();
                    sqlAmsa.Close();
                    
                }
            
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            deletecobranza();
            deletecobrafac();
            deletefaccob();
            deletecobraant();
            guarda();
            insertacobrafac();
            insertafaccob();
            insertacobraant();
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            Variablescompartidas.sucursal = comboBox1.Text;
            Variablescompartidas.fecha = fecha.ToString("MM/dd/yyyy");
            Variablescompartidas.idcredito = comboBox1.SelectedValue.ToString();

            using (Reporte rp = new Reporte())
            {
                rp.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView3_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        
        private void suma()
        {
            try
            {
                foreach (DataGridViewRow dr in dataGridView3.Rows)
                {
                    if (dr.Cells["Column5"].Value != null)
                    {
                        totalCheque = totalCheque + float.Parse(dr.Cells["Column7"].Value.ToString());
                        totalEfectivo = totalEfectivo + float.Parse(dr.Cells["Column8"].Value.ToString());
                        totalTc = totalTc + float.Parse(dr.Cells["Column9"].Value.ToString());
                        totalTd = totalTd + float.Parse(dr.Cells["Column10"].Value.ToString());
                        totalTransfer = totalTransfer + float.Parse(dr.Cells["Column11"].Value.ToString());
                    }
                }
                textBox3.Text = totalCheque.ToString();
                textBox4.Text = totalEfectivo.ToString();
                textBox5.Text = totalTc.ToString();
                textBox6.Text = totalTd.ToString();
                textBox7.Text = totalTransfer.ToString();
            }
            catch (Exception)
            {

                
            }
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.Columns[e.ColumnIndex].Name == "Column7")
            {
                if (dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column5"].Value ==null || dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column6"].Value == null)
                {
                    MessageBox.Show("Favor de llenar los campos anteriores");
                }else
                {
                    DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
                    Variablescompartidas.fecha = fecha.ToString("MM/dd/yyyy");
                    Variablescompartidas.sucursal = comboBox1.Text;
                    Variablescompartidas.factura = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column5"].Value.ToString();
                    Variablescompartidas.provedor = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column6"].Value.ToString();
                    Variablescompartidas.tipo = "Cheque";
                using (Cobros cr = new Cobros())
                {
                    cr.ShowDialog();
                }

                    dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column7"].Value = Variablescompartidas.cantidad;
                    suma();
                }
                
            }
           else if (dataGridView3.Columns[e.ColumnIndex].Name == "Column11")
            {
                if (dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column5"].Value == null || dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column6"].Value == null)
                {
                    MessageBox.Show("Favor de llenar los campos anteriores");
                }
                else
                {
                    DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
                    Variablescompartidas.fecha = fecha.ToString("MM/dd/yyyy");
                    Variablescompartidas.sucursal = comboBox1.Text;
                    Variablescompartidas.factura = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column5"].Value.ToString();
                    Variablescompartidas.provedor = dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column6"].Value.ToString();
                    Variablescompartidas.tipo = "Transferencia";
                    using (Cobros cr = new Cobros())
                    {
                        cr.ShowDialog();
                    }
                    dataGridView3.Rows[dataGridView3.CurrentRow.Index].Cells["Column11"].Value = Variablescompartidas.cantidad;
                    suma();
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}