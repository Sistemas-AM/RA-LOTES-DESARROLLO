using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cotizacion2
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int cont = 0;

        public Form1()
        {
            InitializeComponent();
            consecutivo();
        }

        private void consecutivo()
        {
            //---------------------------Llenar las ventas especiales ---------------------------------
            cmd.CommandText = "select top 1 TEC from Concot order by TEC DESC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                int fol = int.Parse(reader["TEC"].ToString()) + 1;
                textFolio.Text = fol.ToString();
                textSerie.Text = "TEC";

            }
            sqlConnection1.Close();
            //-----------------------------------------------------------------------------------------
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (radioButton2.Checked)
            {
                using (Clienteslocales CL = new Clienteslocales())
                {
                    CL.ShowDialog();
                }
                try
                {
                    textCodCliente.Text = Variablescompartidas.Codigo;
                    //---------------------------Llenar el cliente ---------------------------------
                    cmd.CommandText = "select nombre, rfc, direccion, numero, telefono, colonia, cp, email, pais, ciudad, estado from ctelocal where id_ctelocal='" + Variablescompartidas.Codigo + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {

                        textNombre.Text =   reader["nombre"].ToString();
                        textRFC.Text =      reader["rfc"].ToString();
                        textMail.Text =     reader["email"].ToString();
                        textCalle.Text =    reader["direccion"].ToString();
                        textNum.Text =      reader["numero"].ToString();
                        textTelefono.Text = reader["telefono"].ToString();
                        textColonia.Text =  reader["colonia"].ToString();
                        textCP.Text =       reader["cp"].ToString();
                        textPais.Text =     reader["pais"].ToString();
                        textCiudad.Text =   reader["ciudad"].ToString();
                        textEstado.Text =   reader["estado"].ToString();


                    }
                    sqlConnection1.Close();
                    //-----------------------------------------------------------------------------------------

                }
                catch (Exception)
                {

                }

            }
            else
            {
                using (Clientes cl = new Clientes())
                {
                    cl.ShowDialog();
                }
                try
                {
                    textCodCliente.Text = Variablescompartidas.Codigo;
                    //---------------------------Llenar el cliente ---------------------------------
                    cmd.CommandText = "select CRAZONSOCIAL, CRFC, CEMAIL1, CIDCLIENTEPROVEEDOR from admClientes where CCODIGOCLIENTE = '" + Variablescompartidas.Codigo + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();
                    string idpro = "";
                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {

                        textNombre.Text =   reader["CRAZONSOCIAL"].ToString();
                        textRFC.Text =      reader["CRFC"].ToString();
                        textMail.Text =     reader["CEMAIL1"].ToString();
                        idpro =             reader["CIDCLIENTEPROVEEDOR"].ToString();

                    }
                    sqlConnection2.Close();
                    //-----------------------------------------------------------------------------------------

                    //---------------------------Llenar el Domicilio del cliente ---------------------------------
                    cmd.CommandText = "Select CNOMBRECALLE, CNUMEROEXTERIOR, CCOLONIA, CCODIGOPOSTAL, CTELEFONO1, CCIUDAD, CESTADO, CPAIS from admDomicilios WHERE CIDCATALOGO = '" + idpro + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();

                    // Data is accessible through the DataReader object here.
                    if (reader.Read())
                    {

                        textCalle.Text =    reader["CNOMBRECALLE"].ToString();
                        textNum.Text =      reader["CNUMEROEXTERIOR"].ToString();
                        textColonia.Text =  reader["CCOLONIA"].ToString();
                        textCP.Text =       reader["CCODIGOPOSTAL"].ToString();
                        textTelefono.Text = reader["CTELEFONO1"].ToString();
                        textCiudad.Text =   reader["CCIUDAD"].ToString();
                        textEstado.Text =   reader["CESTADO"].ToString();
                        textPais.Text =     reader["CPAIS"].ToString();

                    }
                    sqlConnection2.Close();
                    //-----------------------------------------------------------------------------------------
                }
                catch (Exception)
                {


                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[e.RowIndex].Cells[4].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());

            }
            catch (Exception)
            {

               
            }

            //Calcular el subtotal del importe
            try
            {
                Double SubtT = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    SubtT += Math.Round(Convert.ToDouble(row.Cells["Column5"].Value), 2);
                }
                textSubCot.Text = SubtT.ToString();
                textTotal.Text = (SubtT * 1.16).ToString();
                textTotIva.Text = (Double.Parse(textTotal.Text) - Double.Parse(textSubCot.Text)).ToString();

                //textTotal.Text = (Double.Parse(textSubCot.Text) + Double.Parse(textTotIva.Text)).ToString();

            }
            catch (Exception)
            {


            }



        }

        private void button9_Click(object sender, EventArgs e)
        {
            textObs.Text = textObs.Text + String.Format(Environment.NewLine);
            List<String> condicion = new List<String>();

            Condicion CD = new Condicion();
            {
                CD.ShowDialog();
                condicion = CD.condicion;
            }

            textObs.Text = textObs.Text + String.Join(String.Format(Environment.NewLine), condicion);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            guardar();
            guardacon();
            //consecutivo();
            MessageBox.Show("Guardado");
        }

        private void delete()
        {
            string sql = @"delete from bdcotizao where folio = '" + textFolio.Text + "'";
            SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
            sqlConnection1.Close();
            sqlConnection1.Open();
            cmd2.ExecuteNonQuery();
            sqlConnection1.Close();
        }


        private void guardar()
        {
            try
            {
               delete();
                DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                  
                    string sql = @"insert into bdcotizao(sucursal, serie, folio, fecha, cliente, tipocot, cantidad, precio, importe, iva, 
                        nombre, rfc, direccion, numero, telefono, colonia, cp, mail, pais, ciudad, estado, observa, atencion, solicito, nombrePro, codigopro )
                        values(@param1, @param2, @param3, @param4, @param5, @param6, @param8, @param9, @param10, @param13, @param17, @param18, @param19, @param20, @param21, @param22, @param23, @param24, @param25, @param26, @param27, @param30, @param31, @param32, @param43, @param44)";
                    SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                    cmd2.Parameters.AddWithValue("@param1", textSerie.Text); //Sucursal
                    cmd2.Parameters.AddWithValue("@param2", "0"); //Serie
                    cmd2.Parameters.AddWithValue("@param3", textFolio.Text); //Folio
                    cmd2.Parameters.AddWithValue("@param4", fecha.ToString("MM/dd/yyyy")); //Fecha
                    cmd2.Parameters.AddWithValue("@param5", textCodCliente.Text); //Cliente
                    cmd2.Parameters.AddWithValue("@param6", "0"); //Tipocot
                    cmd2.Parameters.AddWithValue("@param8", row.Cells["Column2"].Value.ToString()); //cantidad
                    cmd2.Parameters.AddWithValue("@param9", row.Cells["Column4"].Value.ToString()); //Precio
                    cmd2.Parameters.AddWithValue("@param10", row.Cells["Column5"].Value.ToString()); //Importe
                    cmd2.Parameters.AddWithValue("@param13", textTotIva.Text); //Iva
                    cmd2.Parameters.AddWithValue("@param17", textNombre.Text); //Nombre
                    cmd2.Parameters.AddWithValue("@param18", textRFC.Text); //Rfc
                    cmd2.Parameters.AddWithValue("@param19", textCalle.Text); //Direccion
                    cmd2.Parameters.AddWithValue("@param20", textNum.Text); //Numero
                    cmd2.Parameters.AddWithValue("@param21", textTelefono.Text); //Telefono
                    cmd2.Parameters.AddWithValue("@param22", textColonia.Text); //Colonia
                    cmd2.Parameters.AddWithValue("@param23", textCP.Text); //CP
                    cmd2.Parameters.AddWithValue("@param24", textMail.Text); //Mail
                    cmd2.Parameters.AddWithValue("@param25", textPais.Text); //Pais
                    cmd2.Parameters.AddWithValue("@param26", textCiudad.Text); //Ciudad
                    cmd2.Parameters.AddWithValue("@param27", textEstado.Text); //Estado
                    cmd2.Parameters.AddWithValue("@param30", textObs.Text); //Obs
                    cmd2.Parameters.AddWithValue("@param31", textAtencion.Text); //Atecnion
                    cmd2.Parameters.AddWithValue("@param32", textSolicito.Text); //Solicito
                    cmd2.Parameters.AddWithValue("@param43", row.Cells["Column1"].Value.ToString()); //NombrePro
                    cmd2.Parameters.AddWithValue("@param44", row.Cells["Column3"].Value.ToString()); //CodigoPro
                    sqlConnection1.Close();
                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();
                    sqlConnection1.Close();
                }
                //MessageBox.Show("Guardado");
            }
            catch (Exception)
            {


            }
        }

        private void guardacon()
        {
            string sql = "insert into Concot (TEC) values (@param1)";
            SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
            cmd2.Parameters.AddWithValue("@param1", textFolio.Text); //Folio ventas
            sqlConnection1.Close();
            sqlConnection1.Open();
            cmd2.ExecuteNonQuery();
            sqlConnection1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cont = 0;
            dataGridView1.Rows.Clear();
            
            using (ctzaciones ct = new ctzaciones())
            {
                ct.ShowDialog();
            }

            //---------------------------Llenar el Domicilio del cliente ---------------------------------
            cmd.CommandText = "select cliente, tipocot, nombre, rfc, direccion, numero , telefono, colonia, cp, mail, pais, ciudad, estado,atencion, solicito, observa from bdcotizao where folio ='" + Variablescompartidas.Foliocot + "' and fecha = '" + Variablescompartidas.Fechacot + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {

                textFolio.Text = Variablescompartidas.Foliocot;
                dateTimePicker1.Text = Variablescompartidas.Fechacot;
                textCalle.Text = reader["direccion"].ToString();
                textNum.Text = reader["numero"].ToString();
                textColonia.Text = reader["colonia"].ToString();
                textCP.Text = reader["cp"].ToString();
                textTelefono.Text = reader["telefono"].ToString();
                textCiudad.Text = reader["ciudad"].ToString();
                textEstado.Text = reader["estado"].ToString();
                textPais.Text = reader["pais"].ToString();
                textNombre.Text = reader["nombre"].ToString();
                textRFC.Text = reader["rfc"].ToString();
                textCodCliente.Text = reader["cliente"].ToString();
                textMail.Text = reader["mail"].ToString();
                textAtencion.Text = reader["atencion"].ToString();
                textSolicito.Text = reader["solicito"].ToString();
                textObs.Text = reader["observa"].ToString();

            }
            sqlConnection1.Close();

            //---------------------------Llenar Datos del producto ---------------------------------
            cmd.CommandText = "select nombrePro, cantidad, codigopro, precio, importe from bdcotizao where folio ='" + Variablescompartidas.Foliocot + "' and fecha = '" + Variablescompartidas.Fechacot + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.

            while (reader.Read())
            {

                dataGridView1.Rows.Add();
                dataGridView1.Rows[cont].Cells[0].Value = reader["nombrePro"].ToString().Trim();
                dataGridView1.Rows[cont].Cells[1].Value = reader["cantidad"].ToString();
                dataGridView1.Rows[cont].Cells[2].Value = reader["codigopro"].ToString();
                dataGridView1.Rows[cont].Cells[3].Value = reader["precio"].ToString();
                dataGridView1.Rows[cont].Cells[4].Value = reader["importe"].ToString();

                cont++;
                
            }
            sqlConnection1.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            textNombre.Text = "";
            textRFC.Text = "";
            textCalle.Text = "";
            textNum.Text = "";
            textColonia.Text = "";
            textCP.Text = "";
            textTelefono.Text = "";
            textCiudad.Text = "";
            textEstado.Text = "";
            textPais.Text = "";
            textMail.Text = "";
            textSolicito.Text = "";
            textAtencion.Text = "";
            textFolio.Text = "";
            textSubT.Text = "0";
            textIvaImp.Text = "0";
            textTotImp.Text = "0";
            textSubCot.Text = "0";
            textTotIva.Text = "0";
            textTotPeso.Text = "0";
            textTotal.Text = "0";
            textObs.Text = "* Precios sujetos a cambios sin previo aviso y disponibilidad "+ String.Format(Environment.NewLine) + "* Precios ya incluyen Iva "+ String.Format(Environment.NewLine) + "* Precios LAB Hermosillo";
            textCodCliente.Clear();
            dateTimePicker1.Value = DateTime.Now;

            cont = 0;
            consecutivo();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);


            Variablescompartidas.Folio = textFolio.Text;
            Variablescompartidas.Fecha = fecha.ToString("dd/MM/yyyy");
            Variablescompartidas.Cliente = textNombre.Text;
            Variablescompartidas.Telefono = textTelefono.Text;
            Variablescompartidas.Direccion = textCalle.Text;
            Variablescompartidas.Email = textMail.Text;
            Variablescompartidas.Atencion = textAtencion.Text;
            Variablescompartidas.Solicito = textSolicito.Text;
            Variablescompartidas.Textos = textObs.Text;


            using (Reporte RP = new Reporte(dataGridView1))
            {
                RP.ShowDialog();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                cont -= 1;
            }
            catch (Exception)
            {

               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
