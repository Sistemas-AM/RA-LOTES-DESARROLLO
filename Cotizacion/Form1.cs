using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cotizacion
{
    public partial class Form1 : Form
    {

        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int cont = 0;
        string sigfolio = "";
        string folioOri = "";
        int serie = 0;

        public Form1()
        {
            InitializeComponent();
            //DateTime hoy = DateTime.Now;
            //textFecha.Text = hoy.ToString("MM/dd/yyyy");
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //--------------------------Llenar las ventas especiales ---------------------------------
            cmd.CommandText = "select Ventas from Concot order by ventas DESC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                int fol = int.Parse(reader["Ventas"].ToString()) + 1;
                textFolio.Text = fol.ToString();
                textSerie.Text = "VE";

            }
            sqlConnection1.Close();
            //-----------------------------------------------------------------------------------------
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //---------------------------Llenar Agentes ---------------------------------
            cmd.CommandText = "select Agente from Concot order by Agente DESC";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                int fol = int.Parse(reader["Agente"].ToString()) + 1;
                textFolio.Text = fol.ToString();
                textSerie.Text = "AG";

            }
            sqlConnection1.Close();
            //----------------------------------------------------------------------------

        }

        private void button2_Click(object sender, EventArgs e)
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

                        textNombre.Text = reader["nombre"].ToString();
                        textRFC.Text = reader["rfc"].ToString();
                        textMail.Text = reader["email"].ToString();
                        textCalle.Text = reader["direccion"].ToString();
                        textNum.Text = reader["numero"].ToString();
                        textTelefono.Text = reader["telefono"].ToString();
                        textColonia.Text = reader["colonia"].ToString();
                        textCP.Text = reader["cp"].ToString();
                        textPais.Text = reader["pais"].ToString();
                        textCiudad.Text = reader["ciudad"].ToString();
                        textEstado.Text = reader["estado"].ToString();


                    }
                    sqlConnection1.Close();
                    //-----------------------------------------------------------------------------------------

                }
                catch (Exception)
                {

                }

            } else
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

                        textNombre.Text = reader["CRAZONSOCIAL"].ToString();
                        textRFC.Text = reader["CRFC"].ToString();
                        textMail.Text = reader["CEMAIL1"].ToString();
                        idpro = reader["CIDCLIENTEPROVEEDOR"].ToString();

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

                        textCalle.Text = reader["CNOMBRECALLE"].ToString();
                        textNum.Text = reader["CNUMEROEXTERIOR"].ToString();
                        textColonia.Text = reader["CCOLONIA"].ToString();
                        textCP.Text = reader["CCODIGOPOSTAL"].ToString();
                        textTelefono.Text = reader["CTELEFONO1"].ToString();
                        textCiudad.Text = reader["CCIUDAD"].ToString();
                        textEstado.Text = reader["CESTADO"].ToString();
                        textPais.Text = reader["CPAIS"].ToString();

                    }
                    sqlConnection2.Close();
                    //-----------------------------------------------------------------------------------------
                }
                catch (Exception)
                {


                }

            }


        }

        private void button9_Click(object sender, EventArgs e)
        {
            List<String> condicion = new List<String>();

            Condicion CD = new Condicion();
            {
                CD.ShowDialog();
                condicion = CD.condicion;
            }

            textObs.Text = textObs.Text + String.Join("\n", condicion);



        }

        private void remplaza() {
            //if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() == "")
            //{
            //dataGridView1.Rows.Add();
            // dataGridView1.Rows[cont].Cells[13].ReadOnly = true;
            // }


            int almacen = 1;
            double existencia = 0;
            double cedis = 0;

            try
            {

                while (almacen < 9)
                {

                    sqlConnection2.Open();
                    SqlCommand cmd = new SqlCommand("conexiaBIEN", sqlConnection2);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@almacen", Convert.ToString(almacen));
                    cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);
                    cmd.Parameters.AddWithValue("@mes", Convert.ToString(DateTime.Now.Month.ToString()));
                    cmd.Parameters.AddWithValue("@codigo", Convert.ToString(Variablescompartidas.CodigoPro));
                    //SqlDataReader dr = cmd.ExecuteReader();


                    DataTable ds = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);

                    if (ds.Rows.Count > 0)
                    {
                        DataRow row = ds.Rows[0];
                        if (almacen == 2 || almacen == 6)
                        {
                            existencia = existencia + 0;
                        }
                        else if (almacen == 8)
                        {
                            cedis = Convert.ToDouble(row["existencia"]);
                        }
                        else
                        {
                            existencia = existencia + Convert.ToDouble(row["existencia"]);
                        }
                        //if (almacen != 2 || almacen!=6 || almacen!=7)
                        //{

                        //}

                        almacen = almacen + 1;
                    }
                    sqlConnection2.Close();
                }
            }
            catch (Exception)
            {


            }


            try
            {
                //---------------------------Llenar Datos del producto ---------------------------------
                cmd.CommandText = "select CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIMPORTEEXTRA1, CPRECIO1, CTIPOPRODUCTO from admProductos where CCODIGOPRODUCTO = '" + Variablescompartidas.CodigoPro + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection2;
                sqlConnection2.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {

                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value = reader["CCODIGOPRODUCTO"].ToString();
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value = reader["CNOMBREPRODUCTO"].ToString();
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value = existencia.ToString();
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value = reader["CPRECIO1"].ToString();
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value = reader["CIMPORTEEXTRA1"].ToString();
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column19"].Value = reader["CIMPORTEEXTRA1"].ToString();

                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[13].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[16].Value = reader["CIDPRODUCTO"].ToString();
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[17].Value = reader["CTIPOPRODUCTO"].ToString();

                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[10].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[14].Value = "0";
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[15].Value = "0";



                    float cero = float.Parse(reader["CIMPORTEEXTRA1"].ToString());
                    if (cero == 0)
                    {
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[13].ReadOnly = false;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[13].Value = "0";

                    }

                }
                sqlConnection2.Close();

                //cont = cont + 1;
            }
            catch (Exception)
            {


            }

            Variablescompartidas.CodigoPro = null;
            totales();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column1")
            {
                using (Productos Pr = new Productos())
                {
                    Pr.ShowDialog();
                }
                if (Variablescompartidas.CodigoPro != null || Variablescompartidas.CodigoPro == "")
                {

                    if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Column1"].Value == null)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[cont].Cells[13].ReadOnly = true;


                        int almacen = 1;
                        double existencia = 0;
                        double cedis = 0;

                        try
                        {

                            while (almacen < 9)
                            {

                                sqlConnection2.Open();
                                SqlCommand cmd = new SqlCommand("conexiaBIEN", sqlConnection2);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@almacen", Convert.ToString(almacen));
                                cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);
                                cmd.Parameters.AddWithValue("@mes", Convert.ToString(DateTime.Now.Month.ToString()));
                                cmd.Parameters.AddWithValue("@codigo", Convert.ToString(Variablescompartidas.CodigoPro));
                                //SqlDataReader dr = cmd.ExecuteReader();


                                DataTable ds = new DataTable();

                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                da.Fill(ds);

                                if (ds.Rows.Count > 0)
                                {
                                    DataRow row = ds.Rows[0];
                                    if (almacen == 2 || almacen == 6)
                                    {
                                        existencia = existencia + 0;
                                    }
                                    else if (almacen == 8)
                                    {
                                        cedis = Convert.ToDouble(row["existencia"]);
                                    }
                                    else
                                    {
                                        existencia = existencia + Convert.ToDouble(row["existencia"]);
                                    }
                                    //if (almacen != 2 || almacen!=6 || almacen!=7)
                                    //{

                                    //}

                                    almacen = almacen + 1;
                                }
                                sqlConnection2.Close();
                            }
                        }
                        catch (Exception)
                        {


                        }


                        try
                        {
                            //---------------------------Llenar Datos del producto ---------------------------------
                            cmd.CommandText = "select CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIMPORTEEXTRA1, CPRECIO1, CTIPOPRODUCTO from admProductos where CCODIGOPRODUCTO = '" + Variablescompartidas.CodigoPro + "'";
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection2;
                            sqlConnection2.Open();
                            reader = cmd.ExecuteReader();

                            // Data is accessible through the DataReader object here.
                            if (reader.Read())
                            {

                                dataGridView1.Rows[cont].Cells[0].Value = reader["CCODIGOPRODUCTO"].ToString();
                                dataGridView1.Rows[cont].Cells[1].Value = reader["CNOMBREPRODUCTO"].ToString();
                                dataGridView1.Rows[cont].Cells[2].Value = "0";
                                dataGridView1.Rows[cont].Cells[3].Value = existencia.ToString();
                                dataGridView1.Rows[cont].Cells[4].Value = reader["CPRECIO1"].ToString();
                                dataGridView1.Rows[cont].Cells[5].Value = "0";
                                dataGridView1.Rows[cont].Cells[6].Value = reader["CIMPORTEEXTRA1"].ToString();
                                dataGridView1.Rows[cont].Cells["Column19"].Value = reader["CIMPORTEEXTRA1"].ToString();

                                dataGridView1.Rows[cont].Cells[13].Value = "0";
                                dataGridView1.Rows[cont].Cells[16].Value = reader["CIDPRODUCTO"].ToString();
                                dataGridView1.Rows[cont].Cells[17].Value = reader["CTIPOPRODUCTO"].ToString();

                                dataGridView1.Rows[cont].Cells[7].Value = "0";
                                dataGridView1.Rows[cont].Cells[8].Value = "0";
                                dataGridView1.Rows[cont].Cells[9].Value = "0";
                                dataGridView1.Rows[cont].Cells[10].Value = "0";
                                dataGridView1.Rows[cont].Cells[11].Value = "0";
                                dataGridView1.Rows[cont].Cells[12].Value = "0";
                                dataGridView1.Rows[cont].Cells[14].Value = "0";
                                dataGridView1.Rows[cont].Cells[15].Value = "0";
                                dataGridView1.Rows[cont].Cells["Cedis"].Value = cedis.ToString();



                                float cero = float.Parse(reader["CIMPORTEEXTRA1"].ToString());
                                if (cero == 0)
                                {
                                    dataGridView1.Rows[cont].Cells[13].ReadOnly = false;
                                    dataGridView1.Rows[cont].Cells[13].Value = "0";

                                }

                            }
                            sqlConnection2.Close();

                            cont = cont + 1;
                        }
                        catch (Exception)
                        {


                        }

                    }
                    else
                    {
                        remplaza();
                    }

                    Variablescompartidas.CodigoPro = null;
                    totales();
                }


            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column4")
            {
                try
                {
                    Variablescompartidas.CodigoPro = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Variablescompartidas.Nombre = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
                catch (Exception)
                {


                }
                using (Existencias ex = new Existencias())
                {
                    ex.ShowDialog();
                }
            }
            Variablescompartidas.CodigoPro = null;
        }
        private void calcula()
        {
            try
            {
                // float cero = float.Parse(reader["CIMPORTEEXTRA1"].ToString());
                //if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() == "0")
                //{
                //    dataGridView1.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                //    dataGridView1.Rows[e.RowIndex].Cells[13].Value = "0";

                //}

                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString()) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString());
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString()) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString());
                if (float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString()) == 0)
                {
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[13].ReadOnly = false;
                    //dataGridView1.Rows[e.RowIndex].Cells[13].Value = "0";
                    if (float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[13].Value.ToString()) > 0)
                    {
                        float descuento = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[13].Value.ToString()) / 100;
                        float totdescuento = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString()) * descuento;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[14].Value = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString()) - totdescuento;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[15].Value = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[14].Value.ToString()) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString());
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value = (float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString()) + float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[10].Value.ToString())) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString());

                    }
                    else
                    {
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = "0";
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value = (float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString()) + float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[10].Value.ToString())) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString());
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[14].Value = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[15].Value = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[14].Value.ToString()) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString());
                    }

                }
                else
                {
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[8].Value = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString()) / float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString());
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value = (float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[9].Value.ToString()) + float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[10].Value.ToString())) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[11].Value.ToString());
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[14].Value = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value.ToString()) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value.ToString());
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[15].Value = float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[14].Value.ToString()) * float.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString());

                }


            }
            catch (Exception)
            {

            }
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                // float cero = float.Parse(reader["CIMPORTEEXTRA1"].ToString());
                //if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() == "0")
                //{
                //    dataGridView1.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                //    dataGridView1.Rows[e.RowIndex].Cells[13].Value = "0";

                //}

                dataGridView1.Rows[e.RowIndex].Cells[5].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                dataGridView1.Rows[e.RowIndex].Cells[7].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells["Column19"].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());

                if (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) == 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                    //dataGridView1.Rows[e.RowIndex].Cells[13].Value = "0";
                    if (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString()) > 0)
                    {
                        float descuento = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString()) / 100;
                        float totdescuento = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) * descuento;
                        dataGridView1.Rows[e.RowIndex].Cells[14].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) - totdescuento;
                        dataGridView1.Rows[e.RowIndex].Cells[15].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                        dataGridView1.Rows[e.RowIndex].Cells[8].Value = "0";
                        dataGridView1.Rows[e.RowIndex].Cells[12].Value = (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString())) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString());

                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[8].Value = "0";
                        dataGridView1.Rows[e.RowIndex].Cells[12].Value = (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString())) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString());
                        dataGridView1.Rows[e.RowIndex].Cells[14].Value = dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                        dataGridView1.Rows[e.RowIndex].Cells[15].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    }

                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[8].Value = Math.Round((float.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()) / float.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString())) / 1.16, 2);
                    dataGridView1.Rows[e.RowIndex].Cells[12].Value = (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString()) + float.Parse(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString())) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString());
                    dataGridView1.Rows[e.RowIndex].Cells[14].Value = (float.Parse(dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) * 1.16);
                    dataGridView1.Rows[e.RowIndex].Cells[15].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());

                }


            }
            catch (Exception)
            {

            }


            //Calcular el subtotal del importe
            try
            {
                Double SubtT = 0;
                Double totPeso = 0;
                Double SubCot = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    SubtT += Math.Round(Convert.ToDouble(row.Cells["Column6"].Value), 2);
                    totPeso += Math.Round(Convert.ToDouble(row.Cells["Column7"].Value), 2);
                    SubCot += Math.Round(Convert.ToDouble(row.Cells["Column15"].Value), 2);
                }
                textTotImp.Text = SubtT.ToString();
                textTotPeso.Text = totPeso.ToString();
                textTotal.Text = SubCot.ToString();
                ivarevez();
                //iva();
            }
            catch (Exception)
            {


            }
        }

        private void totales()
        {

            //Calcular el subtotal del importe
            try
            {
                Double SubtT = 0;
                Double totPeso = 0;
                Double SubCot = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    SubtT += Math.Round(Convert.ToDouble(row.Cells["Column6"].Value), 2);
                    totPeso += Math.Round(Convert.ToDouble(row.Cells["Column7"].Value), 2);
                    SubCot += Math.Round(Convert.ToDouble(row.Cells["Column15"].Value), 2);
                }
                textTotImp.Text = SubtT.ToString();
                textTotPeso.Text = totPeso.ToString();
                textTotal.Text = SubCot.ToString();
                ivarevez();
                //iva();
            }
            catch (Exception)
            {


            }
        }

        private void ivarevez()
        {
            textSubT.Text = Math.Round((Double.Parse(textTotImp.Text) / 1.16), 2).ToString();
            textIvaImp.Text = Math.Round((Double.Parse(textTotImp.Text) - Double.Parse(textSubT.Text)), 2).ToString();

            textSubCot.Text = Math.Round((Double.Parse(textTotal.Text) / 1.16), 2).ToString();
            textTotIva.Text = Math.Round((Double.Parse(textTotal.Text) - Double.Parse(textSubCot.Text)), 2).ToString();
        }

        private void iva()
        {
            textTotIva.Text = Math.Round((Double.Parse(textSubCot.Text) * 0.16), 2).ToString();
            textTotal.Text = (Double.Parse(textSubCot.Text) + Double.Parse(textTotIva.Text)).ToString();
        }
        private void button5_Click_1(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
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

            using (ReporteGeneral RG = new ReporteGeneral(dataGridView1))
            {
                RG.ShowDialog();
            }
        }

        private void textSubT_TextChanged(object sender, EventArgs e)
        {
            textIvaImp.Text = Math.Round((Double.Parse(textSubT.Text) * 0.16), 2).ToString();
            textTotImp.Text = Math.Round((Double.Parse(textSubT.Text) + Double.Parse(textIvaImp.Text)), 2).ToString();
        }

        private void textSubCot_TextChanged(object sender, EventArgs e)
        {
            textTotIva.Text = Math.Round((Double.Parse(textSubCot.Text) * 0.16), 2).ToString();
            textTotal.Text = Math.Round((Double.Parse(textSubCot.Text) + Double.Parse(textTotIva.Text)), 2).ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textSerie.Text = "VE";
            }
            if (radioButton3.Checked)
            {
                textSerie.Text = "AG";
            }
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
            textCodCliente.Clear();

            Recibe.Clear();
            TelRecibe.Clear();
            TiempoEnt.Clear();
            FacNot.Clear();
            TipPag.Clear();
            radioButton6.Checked = false;
            radioButton5.Checked = true;

            button11.Enabled = false;
            button4.Enabled = true;

            cont = 0;
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

        private void button4_Click(object sender, EventArgs e)
        {
            //insert into bdcotizao (sucursal, serie, folio, fecha, cliente, tipocot, codigopro, cantidad, precio, importe, idproducto, tipo, iva, sp, descto, pago, nombre, rfc, direccion, numero, telefono, colonia, cp, mail, pais, ciudad, estado, surtida, kilos, observa, atencion, solicito, kgmon, flete, utilidad, subt, neto, prexpza, precot) values ()
            //------------------------------------------ GRABAR -----------------------------------------
            if (radioButton3.Checked || radioButton4.Checked)
            {
                try
                {
                    delete();
                    DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        /*IF NOT EXISTS(select * from bdcotizao where folio = @param3) 
                        insert into bdcotizao(sucursal, serie, folio, fecha, cliente, tipocot, codigopro, cantidad, precio, importe, idproducto, tipo, iva, sp, descto, pago, 
                        nombre, rfc, direccion, numero, telefono, colonia, cp, mail, pais, ciudad, estado, surtida, kilos, observa, atencion, solicito, kgmon, flete, utilidad, subt, neto, prexpza, precot, kgiva1, kgiva2, nombrePro, labM)
                        values(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15, @param16, @param17, @param18, @param19, @param20, @param21, @param22, @param23, @param24, @param25, @param26, @param27, @param28, @param29, @param30, @param31, @param32, @param33, @param34, @param35, @param36, @param37, @param38, @param39, @param40, @param41, @param42, @param43)
                        else update bdcotizao set sucursal = @param1, serie = @param2, fecha = @param4, cliente = @param5,
                        tipocot = @param6, codigopro = @param7, cantidad = @param8, precio = @param9, importe = @param10, idproducto = @param11, tipo = @param12,
                        iva = @param13, sp = @param14, descto = @param15, pago = @param16, nombre = @param17, rfc = @param18, direccion = @param19, numero = @param20,
                        telefono = @param21, colonia = @param22, cp = @param23, mail = @param24, pais = @param25, ciudad = @param26, estado = @param27,
                        surtida = @param28, kilos = @param29, observa = @param30, atencion = @param31, solicito = @param32, kgmon = @param33, flete = @param34,
                        utilidad = @param35, subt = @param36, neto = @param37, prexpza = @param38, precot = @param39, kgiva1 = @param40, kgiva2 = @param41, nombrePro = @param42, labM = @param43 where folio = @param3*/



                        string sql = @"insert into bdcotizao(sucursal, serie, folio, fecha, cliente, tipocot, codigopro, cantidad, precio, importe, idproducto, tipo, iva, sp, descto, pago, 
                        nombre, rfc, direccion, numero, telefono, colonia, cp, mail, pais, ciudad, estado, surtida, kilos, observa, atencion, solicito, kgmon, flete, utilidad, subt, neto, prexpza, precot, kgiva1, kgiva2, nombrePro, labM, Recibe, TelRecibe, TiempoEnt, FacNot, TipPag, Remolque)
                        values(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15, @param16, @param17, @param18, @param19, @param20, @param21, @param22, @param23, @param24, @param25, @param26, @param27, @param28, @param29, @param30, @param31, @param32, @param33, @param34, @param35, @param36, @param37, @param38, @param39, @param40, @param41, @param42, @param43, @Recibe, @TelRecibe, @TiempoEnt, @FacNot, @TipPag, @Remolque)";
                        SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                        cmd2.Parameters.AddWithValue("@param1", textSerie.Text); //Sucursal
                        cmd2.Parameters.AddWithValue("@param2", "0"); //Serie
                        cmd2.Parameters.AddWithValue("@param3", textFolio.Text); //Folio
                        cmd2.Parameters.AddWithValue("@param4", fecha.ToString("MM/dd/yyyy")); //Fecha
                        cmd2.Parameters.AddWithValue("@param5", textCodCliente.Text); //Cliente
                        cmd2.Parameters.AddWithValue("@param6", "0"); //Tipocot
                        cmd2.Parameters.AddWithValue("@param7", row.Cells["Column1"].Value.ToString()); //codigoPro
                        cmd2.Parameters.AddWithValue("@param8", row.Cells["Column3"].Value.ToString()); //cantidad
                        cmd2.Parameters.AddWithValue("@param9", row.Cells["Column5"].Value.ToString()); //Precio
                        cmd2.Parameters.AddWithValue("@param10", row.Cells["Column6"].Value.ToString()); //Importe
                        cmd2.Parameters.AddWithValue("@param11", row.Cells["Column17"].Value.ToString()); //IdProducto
                        cmd2.Parameters.AddWithValue("@param12", row.Cells["Column18"].Value.ToString()); //Tipo
                        cmd2.Parameters.AddWithValue("@param13", textTotIva.Text); //Iva
                        cmd2.Parameters.AddWithValue("@param14", "0"); //Sp
                        cmd2.Parameters.AddWithValue("@param15", row.Cells["Column13"].Value.ToString()); //Descto
                        cmd2.Parameters.AddWithValue("@param16", "0"); //Pago
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
                        cmd2.Parameters.AddWithValue("@param28", row.Cells["Column4"].Value.ToString()); //Surtida
                        cmd2.Parameters.AddWithValue("@param29", row.Cells["Column16"].Value.ToString()); //Kilos
                        cmd2.Parameters.AddWithValue("@param30", textObs.Text); //Obs
                        cmd2.Parameters.AddWithValue("@param31", textAtencion.Text); //Atecnion
                        cmd2.Parameters.AddWithValue("@param32", textSolicito.Text); //Solicito
                        cmd2.Parameters.AddWithValue("@param33", row.Cells["Column7"].Value.ToString()); //Kgmon
                        cmd2.Parameters.AddWithValue("@param34", row.Cells["Column10"].Value.ToString()); //Flete
                        cmd2.Parameters.AddWithValue("@param35", row.Cells["Column11"].Value.ToString()); //Utilidad
                        cmd2.Parameters.AddWithValue("@param36", textSubT.Text); //Subt
                        cmd2.Parameters.AddWithValue("@param37", textTotal.Text); //Neto
                        cmd2.Parameters.AddWithValue("@param38", row.Cells["Column14"].Value.ToString()); //prexpza
                        cmd2.Parameters.AddWithValue("@param39", row.Cells["Column15"].Value.ToString()); //preacot
                        cmd2.Parameters.AddWithValue("@param40", row.Cells["Column8"].Value.ToString()); //preacot
                        cmd2.Parameters.AddWithValue("@param41", row.Cells["Column12"].Value.ToString()); //preacot
                        cmd2.Parameters.AddWithValue("@param42", row.Cells["Column2"].Value.ToString()); //preacot
                        cmd2.Parameters.AddWithValue("@param43", row.Cells["Column9"].Value.ToString()); //preacot

                        cmd2.Parameters.AddWithValue("@Recibe", Recibe.Text); 
                        cmd2.Parameters.AddWithValue("@TelRecibe", TelRecibe.Text); 
                        cmd2.Parameters.AddWithValue("@TiempoEnt", TiempoEnt.Text); 
                        cmd2.Parameters.AddWithValue("@FacNot", FacNot.Text);
                        cmd2.Parameters.AddWithValue("@TipPag", TipPag.Text);

                        if (radioButton6.Checked)
                        {
                            cmd2.Parameters.AddWithValue("@Remolque", "Si");
                        }
                        else if (radioButton5.Checked)
                        {
                            cmd2.Parameters.AddWithValue("@Remolque", "No");
                        }else
                        {
                            cmd2.Parameters.AddWithValue("@Remolque", "No");
                        }
                        
                        sqlConnection1.Close();
                        sqlConnection1.Open();
                        cmd2.ExecuteNonQuery();
                        sqlConnection1.Close();
                    }
                    //MessageBox.Show("Guardado");
                }
                catch (NullReferenceException)
                {


                }catch(Exception a)
                {
                    MessageBox.Show("Error al Guardar "+a);
                }

                if (radioButton4.Checked)
                {
                    string sql = "insert into Concot (Ventas) values (@param1)";
                    SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                    cmd2.Parameters.AddWithValue("@param1", textFolio.Text); //Folio ventas
                    sqlConnection1.Close();
                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();
                    sqlConnection1.Close();
                }
                else if (radioButton3.Checked)
                {
                    string sql = "insert into Concot (Agente) values (@param1)";
                    SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                    cmd2.Parameters.AddWithValue("@param1", textFolio.Text); //Folio ventas
                    sqlConnection1.Close();
                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();
                    sqlConnection1.Close();

                }
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("Selecciona Ventas Especiales o Agente");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult di = MessageBox.Show("¿Guardar al cliente?", "Nuevo Cliente", MessageBoxButtons.YesNo);
            if (di == DialogResult.Yes)
            {
                try
                {
                    string sql = "insert into ctelocal (nombre, rfc, direccion, numero, telefono, colonia, cp, email, pais, ciudad, estado) values (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11)";
                    SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                    cmd2.Parameters.AddWithValue("@param1", textNombre.Text); //nombre
                    cmd2.Parameters.AddWithValue("@param2", textRFC.Text); //rfc
                    cmd2.Parameters.AddWithValue("@param3", textCalle.Text); //direccion
                    cmd2.Parameters.AddWithValue("@param4", textNum.Text); //numero
                    cmd2.Parameters.AddWithValue("@param5", textTelefono.Text); //telefono
                    cmd2.Parameters.AddWithValue("@param6", textColonia.Text); //colonia
                    cmd2.Parameters.AddWithValue("@param7", textCP.Text); //cp
                    cmd2.Parameters.AddWithValue("@param8", textMail.Text); //email
                    cmd2.Parameters.AddWithValue("@param9", textPais.Text); //Pais
                    cmd2.Parameters.AddWithValue("@param10", textCiudad.Text); //Ciudad
                    cmd2.Parameters.AddWithValue("@param11", textEstado.Text); //estado

                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();
                    //label25.Text = (row.Cells["Column1"].Value.ToString());
                    sqlConnection1.Close();
                    MessageBox.Show("Cliente Guardado Con Exito");
                }
                catch (Exception)
                {

                    MessageBox.Show("No se pudo grabar al cliente");
                }

            }
        }

        private void cargacopia()
        {
            //---------------------------Llenar el Domicilio del cliente ----------------------------------
            //---------------------------Llenar el Domicilio del cliente ----------------------------------
            cmd.CommandText = "select cliente, tipocot, nombre, rfc, direccion, numero , telefono, colonia, cp, mail, pais, ciudad, estado,atencion, solicito, observa, Recibe, TelRecibe, TiempoEnt, FacNot, TipPag, Remolque from bdcotizao2 where FolioCopia ='" + Variablescompartidas.Foliocot + "' and fecha = '" + Variablescompartidas.Fechacot + "'";
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

                Recibe.Text = reader["Recibe"].ToString();
                TelRecibe.Text = reader["TelRecibe"].ToString();
                TiempoEnt.Text = reader["TiempoEnt"].ToString();
                FacNot.Text = reader["FacNot"].ToString();
                TipPag.Text = reader["TipPag"].ToString();

                if (reader["Remolque"].ToString() == "Si")
                {
                    radioButton6.Checked = true;
                }
                else if (reader["Remolque"].ToString() == "No")
                {
                    radioButton5.Checked = true;
                }
                else
                {
                    radioButton5.Checked = true;
                }

                //Recibe, TelRecibe, TiempoEnt, FacNot, TipPag, Remolque

            }
            sqlConnection1.Close();

            ////-----------------------------------------------------------------------------------------
            //string query = "select codigopro, cantidad, surtida, precio, importe, kilos, kgmon, flete, utilidad, descto, prexpza, precot, kgiva1, kgiva2, nombrePro from bdcotizao where folio ='" + Variablescompartidas.Foliocot + "' and fecha = '" + Variablescompartidas.Fechacot + "'";
            //SqlCommand cmd2 = new SqlCommand(query, sqlConnection1);

            //SqlDataAdapter da = new SqlDataAdapter(cmd2);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            //dataGridView1.DataSource = dt;

            //---------------------------Llenar Datos del producto ---------------------------------
            cmd.CommandText = "select codigopro, cantidad, surtida, precio, importe, kilos, kgmon, flete, utilidad, descto, prexpza, precot, kgiva1, kgiva2, nombrePro, labM, idproducto, tipo from bdcotizao2 where foliocopia ='" + Variablescompartidas.Foliocot + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.

            while (reader.Read())
            {

                dataGridView1.Rows.Add();
                dataGridView1.Rows[cont].Cells[0].Value = reader["codigopro"].ToString().Trim();
                dataGridView1.Rows[cont].Cells[1].Value = reader["nombrePro"].ToString();
                dataGridView1.Rows[cont].Cells[2].Value = reader["cantidad"].ToString();
                dataGridView1.Rows[cont].Cells[3].Value = reader["surtida"].ToString();
                dataGridView1.Rows[cont].Cells[4].Value = reader["precio"].ToString();
                dataGridView1.Rows[cont].Cells[5].Value = reader["importe"].ToString();
                dataGridView1.Rows[cont].Cells[6].Value = reader["kilos"].ToString();
                dataGridView1.Rows[cont].Cells[7].Value = reader["kgmon"].ToString();
                dataGridView1.Rows[cont].Cells[8].Value = reader["kgiva1"].ToString();
                dataGridView1.Rows[cont].Cells[9].Value = reader["labM"].ToString();
                dataGridView1.Rows[cont].Cells[10].Value = reader["flete"].ToString();
                dataGridView1.Rows[cont].Cells[11].Value = reader["utilidad"].ToString();
                dataGridView1.Rows[cont].Cells[12].Value = reader["kgiva2"].ToString();
                dataGridView1.Rows[cont].Cells[13].Value = reader["descto"].ToString();
                dataGridView1.Rows[cont].Cells[14].Value = reader["prexpza"].ToString();
                dataGridView1.Rows[cont].Cells[15].Value = reader["precot"].ToString();
                dataGridView1.Rows[cont].Cells["Column17"].Value = reader["idproducto"].ToString();
                dataGridView1.Rows[cont].Cells["Column18"].Value = reader["tipo"].ToString();
                dataGridView1.Rows[cont].Cells["Column19"].Value = reader["kilos"].ToString();
                cont++;


            }
            sqlConnection1.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cont = 0;
            dataGridView1.Rows.Clear();
            if (radioButton4.Checked)
            {
                Variablescompartidas.Serie = "VE";
            }
            else if (radioButton3.Checked)
            {
                Variablescompartidas.Serie = "AG";
            }
            using (ctzaciones ct = new ctzaciones())
            {
                ct.ShowDialog();
            }
            
            if (Variablescompartidas.copia == "1")
            {
                cargacopia();
                button11.Enabled = true;
                button4.Enabled = false;
            }
            else
            {
                button11.Enabled = true;
                button4.Enabled = true;
                //---------------------------Llenar el Domicilio del cliente ---------------------------------
                cmd.CommandText = "select cliente, tipocot, nombre, rfc, direccion, numero , telefono, colonia, cp, mail, pais, ciudad, estado,atencion, solicito, observa, Recibe, TelRecibe, TiempoEnt, FacNot, TipPag, Remolque from bdcotizao where folio ='" + Variablescompartidas.Foliocot + "' and fecha = '" + Variablescompartidas.Fechacot + "'";
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

                    Recibe.Text = reader["Recibe"].ToString();
                    TelRecibe.Text = reader["TelRecibe"].ToString();
                    TiempoEnt.Text = reader["TiempoEnt"].ToString();
                    FacNot.Text = reader["FacNot"].ToString();
                    TipPag.Text = reader["TipPag"].ToString();

                    if (reader["Remolque"].ToString() == "Si")
                    {
                        radioButton6.Checked = true;
                    }
                    else if (reader["Remolque"].ToString() == "No")
                    {
                        radioButton5.Checked = true;
                    }else
                    {
                        radioButton5.Checked = true;
                    }

                    //Recibe, TelRecibe, TiempoEnt, FacNot, TipPag, Remolque

                }
                sqlConnection1.Close();

                ////-----------------------------------------------------------------------------------------
                //string query = "select codigopro, cantidad, surtida, precio, importe, kilos, kgmon, flete, utilidad, descto, prexpza, precot, kgiva1, kgiva2, nombrePro from bdcotizao where folio ='" + Variablescompartidas.Foliocot + "' and fecha = '" + Variablescompartidas.Fechacot + "'";
                //SqlCommand cmd2 = new SqlCommand(query, sqlConnection1);

                //SqlDataAdapter da = new SqlDataAdapter(cmd2);
                //DataTable dt = new DataTable();
                //da.Fill(dt);

                //dataGridView1.DataSource = dt;

                //---------------------------Llenar Datos del producto ---------------------------------
                cmd.CommandText = "select codigopro, cantidad, surtida, precio, importe, kilos, kgmon, flete, utilidad, descto, prexpza, precot, kgiva1, kgiva2, nombrePro, labM, idproducto, tipo from bdcotizao where folio ='" + Variablescompartidas.Foliocot + "' and fecha = '" + Variablescompartidas.Fechacot + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.

                while (reader.Read())
                {

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cont].Cells[0].Value = reader["codigopro"].ToString().Trim();
                    dataGridView1.Rows[cont].Cells[1].Value = reader["nombrePro"].ToString();
                    dataGridView1.Rows[cont].Cells[2].Value = reader["cantidad"].ToString();
                  //dataGridView1.Rows[cont].Cells[3].Value = reader["surtida"].ToString();
                    dataGridView1.Rows[cont].Cells[4].Value = reader["precio"].ToString();
                    dataGridView1.Rows[cont].Cells[5].Value = reader["importe"].ToString();
                    dataGridView1.Rows[cont].Cells[6].Value = reader["kilos"].ToString();
                    dataGridView1.Rows[cont].Cells[7].Value = reader["kgmon"].ToString();
                    dataGridView1.Rows[cont].Cells[8].Value = reader["kgiva1"].ToString();
                    dataGridView1.Rows[cont].Cells[9].Value = reader["labM"].ToString();
                    dataGridView1.Rows[cont].Cells[10].Value = reader["flete"].ToString();
                    dataGridView1.Rows[cont].Cells[11].Value = reader["utilidad"].ToString();
                    dataGridView1.Rows[cont].Cells[12].Value = reader["kgiva2"].ToString();
                    dataGridView1.Rows[cont].Cells[13].Value = reader["descto"].ToString();
                    dataGridView1.Rows[cont].Cells[14].Value = reader["prexpza"].ToString();
                    dataGridView1.Rows[cont].Cells[15].Value = reader["precot"].ToString();
                    dataGridView1.Rows[cont].Cells["Column17"].Value = reader["idproducto"].ToString();
                    dataGridView1.Rows[cont].Cells["Column18"].Value = reader["tipo"].ToString();
                    dataGridView1.Rows[cont].Cells["Column19"].Value = reader["kilos"].ToString();
                    cont++;


                }
                sqlConnection1.Close();
                
            }
            recalcula();
            obtenpeso();


            try
            {
                Double SubtT = 0;
                Double totPeso = 0;
                Double SubCot = 0;
                foreach (DataGridViewRow row2 in dataGridView1.Rows)
                {
                    SubtT += Math.Round(Convert.ToDouble(row2.Cells["Column6"].Value), 2);
                    totPeso += Math.Round(Convert.ToDouble(row2.Cells["Column7"].Value), 2);
                    SubCot += Math.Round(Convert.ToDouble(row2.Cells["Column15"].Value), 2);
                }
                textTotImp.Text = SubtT.ToString();
                textTotPeso.Text = totPeso.ToString();
                textTotal.Text = SubCot.ToString();
                ivarevez();
                //iva();
            }
            catch (Exception)
            {


            }

        }

        private void obtenpeso()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    cmd.CommandText = "select CIMPORTEEXTRA1 from admProductos where CCODIGOPRODUCTO = '" + row.Cells["Column1"].Value.ToString() + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection2;
                    sqlConnection2.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        row.Cells["Column19"].Value = reader["CIMPORTEEXTRA1"].ToString();
                    }
                    sqlConnection2.Close();
                }
            }
            catch (Exception)
            {

                sqlConnection2.Close();
            }
                
        }

        private void recalcula()
        {

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int almacen = 1;
                    double existencia = 0;
                    double cedis = 0;
                    while (almacen < 9)
                    {
                        sqlConnection2.Open();
                        SqlCommand cmd = new SqlCommand("conexiaBIEN", sqlConnection2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@almacen", Convert.ToString(almacen));
                        cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);
                        cmd.Parameters.AddWithValue("@mes", Convert.ToString(DateTime.Now.Month.ToString()));
                        cmd.Parameters.AddWithValue("@codigo", row.Cells["Column1"].Value.ToString());
                        //SqlDataReader dr = cmd.ExecuteReader();
                        reader = cmd.ExecuteReader();

                        //DataTable ds = new DataTable();

                        //SqlDataAdapter da = new SqlDataAdapter(cmd);
                        //da.Fill(ds);

                        //if (ds.Rows.Count > 0)
                        //{
                        //DataRow row2 = ds.Rows[0];
                        if (reader.Read())
                        {
                            if (almacen == 2 || almacen == 6)
                            {
                                existencia = existencia + 0;
                            }
                            else if (almacen == 8)
                            {
                                cedis = Double.Parse(reader["Existencia"].ToString());
                            }
                            else
                            {
                                existencia = existencia + Convert.ToDouble(reader["Existencia"].ToString());
                            }
                            //if (almacen != 2 || almacen!=6 || almacen!=7)
                            //{

                            //}

                            almacen = almacen + 1;
                        }
                    
                        sqlConnection2.Close();
                    }

                    row.Cells["Column4"].Value = existencia.ToString();
                    row.Cells["Cedis"].Value = cedis.ToString();
                }
            }
            catch (Exception)
            {
                
                sqlConnection2.Close();
            }
                
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                cont -= 1;


                try
                {
                    Double SubtT = 0;
                    Double totPeso = 0;
                    Double SubCot = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        SubtT += Math.Round(Convert.ToDouble(row.Cells["Column6"].Value), 2);
                        totPeso += Math.Round(Convert.ToDouble(row.Cells["Column7"].Value), 2);
                        SubCot += Math.Round(Convert.ToDouble(row.Cells["Column15"].Value), 2);
                    }
                    textTotImp.Text = SubtT.ToString();
                    textTotPeso.Text = totPeso.ToString();
                    textTotal.Text = SubCot.ToString();
                    ivarevez();
                }
                catch (Exception)
                {


                }
            }
            catch (Exception)
            {

                
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {

            if ((keyData != Keys.F1) & (keyData != Keys.F2))
                return base.ProcessCmdKey(ref msg, keyData);

            if (keyData == Keys.F2)
            {
                using (Productos Pr = new Productos())
                {
                    Pr.ShowDialog();
                }

                if (Variablescompartidas.CodigoPro != null || Variablescompartidas.CodigoPro == "")
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cont].Cells[13].ReadOnly = true;

                    int almacen = 1;
                    double existencia = 0;

                    try
                    {

                        while (almacen < 9)
                        {

                            sqlConnection2.Open();
                            SqlCommand cmd = new SqlCommand("conexiaBIEN", sqlConnection2);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@almacen", Convert.ToString(almacen));
                            cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);
                            cmd.Parameters.AddWithValue("@mes", Convert.ToString(DateTime.Now.Month.ToString()));
                            cmd.Parameters.AddWithValue("@codigo", Convert.ToString(Variablescompartidas.CodigoPro));
                            //SqlDataReader dr = cmd.ExecuteReader();


                            DataTable ds = new DataTable();

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);

                            if (ds.Rows.Count > 0)
                            {
                                DataRow row = ds.Rows[0];
                                if (almacen == 2 || almacen == 6 || almacen == 7)
                                {
                                    existencia = existencia + 0;
                                }
                                else
                                {
                                    existencia = existencia + Convert.ToDouble(row["existencia"]);
                                }
                                //if (almacen != 2 || almacen!=6 || almacen!=7)
                                //{

                                //}

                                almacen = almacen + 1;
                            }
                            sqlConnection2.Close();
                        }
                    }
                    catch (Exception)
                    {


                    }


                    try
                    {
                        //---------------------------Llenar Datos del producto ---------------------------------
                        cmd.CommandText = "select CIDPRODUCTO, CCODIGOPRODUCTO, CNOMBREPRODUCTO, CIMPORTEEXTRA1, CPRECIO1, CTIPOPRODUCTO from admProductos where CCODIGOPRODUCTO = '" + Variablescompartidas.CodigoPro + "'";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection2;
                        sqlConnection2.Open();
                        reader = cmd.ExecuteReader();

                        // Data is accessible through the DataReader object here.
                        if (reader.Read())
                        {

                            dataGridView1.Rows[cont].Cells[0].Value = reader["CCODIGOPRODUCTO"].ToString();
                            dataGridView1.Rows[cont].Cells[1].Value = reader["CNOMBREPRODUCTO"].ToString();
                            dataGridView1.Rows[cont].Cells[2].Value = "0";
                            dataGridView1.Rows[cont].Cells[3].Value = existencia.ToString();
                            dataGridView1.Rows[cont].Cells[4].Value = reader["CPRECIO1"].ToString();
                            dataGridView1.Rows[cont].Cells[5].Value = "0";
                            dataGridView1.Rows[cont].Cells[6].Value = reader["CIMPORTEEXTRA1"].ToString();
                            dataGridView1.Rows[cont].Cells["Column19"].Value = reader["CIMPORTEEXTRA1"].ToString();
                            dataGridView1.Rows[cont].Cells[13].Value = "0";
                            dataGridView1.Rows[cont].Cells[16].Value = reader["CIDPRODUCTO"].ToString();
                            dataGridView1.Rows[cont].Cells[17].Value = reader["CTIPOPRODUCTO"].ToString();
                            dataGridView1.Rows[cont].Cells[7].Value = "0";
                            dataGridView1.Rows[cont].Cells[8].Value = "0";
                            dataGridView1.Rows[cont].Cells[9].Value = "0";
                            dataGridView1.Rows[cont].Cells[10].Value = "0";
                            dataGridView1.Rows[cont].Cells[11].Value = "0";
                            dataGridView1.Rows[cont].Cells[12].Value = "0";
                            dataGridView1.Rows[cont].Cells[14].Value = "0";
                            dataGridView1.Rows[cont].Cells[15].Value = "0";

                            float cero = float.Parse(reader["CIMPORTEEXTRA1"].ToString());
                            if (cero == 0)
                            {
                                dataGridView1.Rows[cont].Cells[13].ReadOnly = false;
                                dataGridView1.Rows[cont].Cells[13].Value = "0";

                            }

                        }
                        sqlConnection2.Close();
                        cont = cont + 1;
                    }
                    catch (Exception)
                    {


                    }
                }
                totales();
                Variablescompartidas.CodigoPro = null;


            }

            return true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            foliosig();
            Copia();

        }
        private void foliosig()
        {

            if (textFolio.Text.Contains("-"))
            {
                string[] separadas;

                separadas = textFolio.Text.Split('-');

                folioOri = separadas[0];

                cmd.CommandText = "select top 1 serie as fol from bdcotizao2 where folioori = '" + separadas[0] + "' order by serie desc ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    int fol = int.Parse(reader["fol"].ToString());
                    serie = fol + 1;
                    sigfolio = separadas[0] + "-" + serie.ToString();

                }
                sqlConnection1.Close();
                //MessageBox.Show(sigfolio);
            }
            else
            {
                folioOri = textFolio.Text;
                using (SqlConnection conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa))
                {
                    string query = "select count(*) from bdcotizao2 where folioori = @folio";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("folio", textFolio.Text);
                    conn.Open();

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        //noExiste();
                        serie = 1;
                        sigfolio = textFolio.Text + "-1";
                        //MessageBox.Show("No existe " + sigfolio);
                    }

                    else
                    {
                        cmd.CommandText = "select  top 1 serie as fol from bdcotizao2 where folioOri = '" + textFolio.Text + "' order by serie desc";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        sqlConnection1.Open();
                        reader = cmd.ExecuteReader();

                        // Data is accessible through the DataReader object here.
                        if (reader.Read())
                        {
                            int fol = int.Parse(reader["fol"].ToString());
                            serie = fol + 1;
                            sigfolio = textFolio.Text + "-" + serie.ToString();

                        }
                        sqlConnection1.Close();
                        //MessageBox.Show(sigfolio);
                    }

                }
            }
            
        }

        private void Copia()
        {
            if (radioButton3.Checked || radioButton4.Checked)
            {
                try
                {
                    //delete();
                    DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        /*IF NOT EXISTS(select * from bdcotizao where folio = @param3) 
                        insert into bdcotizao(sucursal, serie, folio, fecha, cliente, tipocot, codigopro, cantidad, precio, importe, idproducto, tipo, iva, sp, descto, pago, 
                        nombre, rfc, direccion, numero, telefono, colonia, cp, mail, pais, ciudad, estado, surtida, kilos, observa, atencion, solicito, kgmon, flete, utilidad, subt, neto, prexpza, precot, kgiva1, kgiva2, nombrePro, labM)
                        values(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15, @param16, @param17, @param18, @param19, @param20, @param21, @param22, @param23, @param24, @param25, @param26, @param27, @param28, @param29, @param30, @param31, @param32, @param33, @param34, @param35, @param36, @param37, @param38, @param39, @param40, @param41, @param42, @param43)
                        else update bdcotizao set sucursal = @param1, serie = @param2, fecha = @param4, cliente = @param5,
                        tipocot = @param6, codigopro = @param7, cantidad = @param8, precio = @param9, importe = @param10, idproducto = @param11, tipo = @param12,
                        iva = @param13, sp = @param14, descto = @param15, pago = @param16, nombre = @param17, rfc = @param18, direccion = @param19, numero = @param20,
                        telefono = @param21, colonia = @param22, cp = @param23, mail = @param24, pais = @param25, ciudad = @param26, estado = @param27,
                        surtida = @param28, kilos = @param29, observa = @param30, atencion = @param31, solicito = @param32, kgmon = @param33, flete = @param34,
                        utilidad = @param35, subt = @param36, neto = @param37, prexpza = @param38, precot = @param39, kgiva1 = @param40, kgiva2 = @param41, nombrePro = @param42, labM = @param43 where folio = @param3*/



                        string sql = @"insert into bdcotizao2(sucursal, serie, folioOri, folioCopia, fecha, cliente, tipocot, codigopro, cantidad, precio, importe, idproducto, tipo, iva, sp, descto, pago, 
                        nombre, rfc, direccion, numero, telefono, colonia, cp, mail, pais, ciudad, estado, surtida, kilos, observa, atencion, solicito, kgmon, flete, utilidad, subt, neto, prexpza, precot, kgiva1, kgiva2, nombrePro, labM, Recibe, TelRecibe, TiempoEnt, FacNot, TipPag, Remolque)
                        values(@param1, @param2, @param3, @foliocopia, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15, @param16, @param17, @param18, @param19, @param20, @param21, @param22, @param23, @param24, @param25, @param26, @param27, @param28, @param29, @param30, @param31, @param32, @param33, @param34, @param35, @param36, @param37, @param38, @param39, @param40, @param41, @param42, @param43, @Recibe, @TelRecibe, @TiempoEnt, @FacNot, @TipPag, @Remolque)";
                        SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                        cmd2.Parameters.AddWithValue("@param1", textSerie.Text); //Sucursal
                        cmd2.Parameters.AddWithValue("@param2", serie.ToString()); //Serie
                        cmd2.Parameters.AddWithValue("@param3", folioOri); //Folio
                        cmd2.Parameters.AddWithValue("@foliocopia", sigfolio); //Folio
                        cmd2.Parameters.AddWithValue("@param4", fecha.ToString("MM/dd/yyyy")); //Fecha
                        cmd2.Parameters.AddWithValue("@param5", textCodCliente.Text); //Cliente
                        cmd2.Parameters.AddWithValue("@param6", "0"); //Tipocot
                        cmd2.Parameters.AddWithValue("@param7", row.Cells["Column1"].Value.ToString()); //codigoPro
                        cmd2.Parameters.AddWithValue("@param8", row.Cells["Column3"].Value.ToString()); //cantidad
                        cmd2.Parameters.AddWithValue("@param9", row.Cells["Column5"].Value.ToString()); //Precio
                        cmd2.Parameters.AddWithValue("@param10", row.Cells["Column6"].Value.ToString()); //Importe
                        cmd2.Parameters.AddWithValue("@param11", row.Cells["Column17"].Value.ToString()); //IdProducto
                        cmd2.Parameters.AddWithValue("@param12", row.Cells["Column18"].Value.ToString()); //Tipo
                        cmd2.Parameters.AddWithValue("@param13", textTotIva.Text); //Iva
                        cmd2.Parameters.AddWithValue("@param14", "0"); //Sp
                        cmd2.Parameters.AddWithValue("@param15", row.Cells["Column13"].Value.ToString()); //Descto
                        cmd2.Parameters.AddWithValue("@param16", "0"); //Pago
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
                        cmd2.Parameters.AddWithValue("@param28", row.Cells["Column4"].Value.ToString()); //Surtida
                        cmd2.Parameters.AddWithValue("@param29", row.Cells["Column16"].Value.ToString()); //Kilos
                        cmd2.Parameters.AddWithValue("@param30", textObs.Text); //Obs
                        cmd2.Parameters.AddWithValue("@param31", textAtencion.Text); //Atecnion
                        cmd2.Parameters.AddWithValue("@param32", textSolicito.Text); //Solicito
                        cmd2.Parameters.AddWithValue("@param33", row.Cells["Column7"].Value.ToString()); //Kgmon
                        cmd2.Parameters.AddWithValue("@param34", row.Cells["Column10"].Value.ToString()); //Flete
                        cmd2.Parameters.AddWithValue("@param35", row.Cells["Column11"].Value.ToString()); //Utilidad
                        cmd2.Parameters.AddWithValue("@param36", textSubT.Text); //Subt
                        cmd2.Parameters.AddWithValue("@param37", textTotal.Text); //Neto
                        cmd2.Parameters.AddWithValue("@param38", row.Cells["Column14"].Value.ToString()); //prexpza
                        cmd2.Parameters.AddWithValue("@param39", row.Cells["Column15"].Value.ToString()); //preacot
                        cmd2.Parameters.AddWithValue("@param40", row.Cells["Column8"].Value.ToString()); //preacot
                        cmd2.Parameters.AddWithValue("@param41", row.Cells["Column12"].Value.ToString()); //preacot
                        cmd2.Parameters.AddWithValue("@param42", row.Cells["Column2"].Value.ToString()); //preacot
                        cmd2.Parameters.AddWithValue("@param43", row.Cells["Column9"].Value.ToString()); //preacot

                        cmd2.Parameters.AddWithValue("@Recibe", Recibe.Text);
                        cmd2.Parameters.AddWithValue("@TelRecibe", TelRecibe.Text);
                        cmd2.Parameters.AddWithValue("@TiempoEnt", TiempoEnt.Text);
                        cmd2.Parameters.AddWithValue("@FacNot", FacNot.Text);
                        cmd2.Parameters.AddWithValue("@TipPag", TipPag.Text);

                        if (radioButton6.Checked)
                        {
                            cmd2.Parameters.AddWithValue("@Remolque", "Si");
                        }
                        else if (radioButton5.Checked)
                        {
                            cmd2.Parameters.AddWithValue("@Remolque", "No");
                        }
                        else
                        {
                            cmd2.Parameters.AddWithValue("@Remolque", "No");
                        }

                        sqlConnection1.Close();
                        sqlConnection1.Open();
                        cmd2.ExecuteNonQuery();
                        sqlConnection1.Close();
                    }
                    
                }
                catch (NullReferenceException )
                {
                    MessageBox.Show("Guardado Como Copia: "+ sigfolio);

                }
                catch(Exception e)
                {
                    MessageBox.Show("Ocurrio un Error " + e);
                }
            }
            }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);


            Variablescompartidas.Folio = textSerie.Text + "-" + textFolio.Text;
            Variablescompartidas.Fecha = fecha.ToString("dd/MM/yyyy");
            Variablescompartidas.Cliente = textNombre.Text;
            Variablescompartidas.Telefono = textTelefono.Text;
            Variablescompartidas.Direccion = textCalle.Text;
            Variablescompartidas.Email = textMail.Text;
            Variablescompartidas.Atencion = textAtencion.Text;
            Variablescompartidas.Solicito = textSolicito.Text;
            Variablescompartidas.Textos = textObs.Text;

            Variablescompartidas.Recibe = Recibe.Text;
            Variablescompartidas.TelRecibe = TelRecibe.Text;
            Variablescompartidas.TiempoEnt = TiempoEnt.Text;
            Variablescompartidas.FacNot = FacNot.Text;
            Variablescompartidas.TipPag = TipPag.Text;
            if (radioButton6.Checked)
            {
                Variablescompartidas.RadioSi = "X";
            }else
            {
                Variablescompartidas.RadioSi = "";
            }

            if (radioButton5.Checked)
            {
                Variablescompartidas.RadioNo = "X";
            }
            else
            {
                Variablescompartidas.RadioNo = "";
            }


            using (FormatoEntrega RP = new FormatoEntrega(dataGridView1))
            {
                RP.ShowDialog();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (ReporteCotizaciones rc =  new ReporteCotizaciones())
            {
                rc.ShowDialog();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    row.Cells["Column5"].Value = Precio(row.Cells["Column1"].Value.ToString()).ToString();


                    try
                    {
                        // float cero = float.Parse(reader["CIMPORTEEXTRA1"].ToString());
                        //if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() == "0")
                        //{
                        //    dataGridView1.Rows[e.RowIndex].Cells[13].ReadOnly = false;
                        //    dataGridView1.Rows[e.RowIndex].Cells[13].Value = "0";

                        //}

                        row.Cells[5].Value = float.Parse( row.Cells[4].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());
                        row.Cells[7].Value = float.Parse(row.Cells["Column19"].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());

                        if (float.Parse(row.Cells[6].Value.ToString()) == 0)
                        {
                            row.Cells[13].ReadOnly = false;
                            //dataGridView1.Rows[e.RowIndex].Cells[13].Value = "0";
                            if (float.Parse(row.Cells[13].Value.ToString()) > 0)
                            {
                                float descuento = float.Parse(row.Cells[13].Value.ToString()) / 100;
                                float totdescuento = float.Parse(row.Cells[4].Value.ToString()) * descuento;
                                row.Cells[14].Value = float.Parse(row.Cells[4].Value.ToString()) - totdescuento;
                                row.Cells[15].Value = float.Parse(row.Cells[14].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());
                                row.Cells[8].Value = "0";
                                row.Cells[12].Value = (float.Parse(row.Cells[9].Value.ToString()) + float.Parse(row.Cells[10].Value.ToString())) * float.Parse(row.Cells[11].Value.ToString());

                            }
                            else
                            {
                                row.Cells[8].Value = "0";
                                row.Cells[12].Value = (float.Parse(row.Cells[9].Value.ToString()) + float.Parse(row.Cells[10].Value.ToString())) * float.Parse(row.Cells[11].Value.ToString());
                                row.Cells[14].Value = row.Cells[4].Value;
                                row.Cells[15].Value = float.Parse(row.Cells[14].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());
                            }

                        }
                        else
                        {
                            row.Cells[8].Value = Math.Round((float.Parse(row.Cells[4].Value.ToString()) / float.Parse(row.Cells[6].Value.ToString())) / 1.16, 2);
                            row.Cells[12].Value = (float.Parse(row.Cells[9].Value.ToString()) + float.Parse(row.Cells[10].Value.ToString())) * float.Parse(row.Cells[11].Value.ToString());
                            row.Cells[14].Value = (float.Parse(row.Cells[12].Value.ToString()) *float.Parse(row.Cells[6].Value.ToString()) * 1.16);
                            row.Cells[15].Value = float.Parse(row.Cells[14].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());

                        }


                    }
                    catch (Exception)
                    {

                    }


                    //Calcular el subtotal del importe
                    try
                    {
                        Double SubtT = 0;
                        Double totPeso = 0;
                        Double SubCot = 0;
                        foreach (DataGridViewRow row2 in dataGridView1.Rows)
                        {
                            SubtT += Math.Round(Convert.ToDouble(row2.Cells["Column6"].Value), 2);
                            totPeso += Math.Round(Convert.ToDouble(row2.Cells["Column7"].Value), 2);
                            SubCot += Math.Round(Convert.ToDouble(row2.Cells["Column15"].Value), 2);
                        }
                        textTotImp.Text = SubtT.ToString();
                        textTotPeso.Text = totPeso.ToString();
                        textTotal.Text = SubCot.ToString();
                        ivarevez();
                        //iva();
                    }
                    catch (Exception)
                    {


                    }












                }
                catch (NullReferenceException)
                {

                   
                }
            }
        }


        public double Precio(string codigo)
        {

            Double exis = 0;
            string sql = @"select CCODIGOPRODUCTO, CNOMBREPRODUCTO, cprecio1 from admproductos 
                            where CCODIGOPRODUCTO =@Codigo";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@codigo", codigo);

            sqlConnection2.Close();
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                exis = Double.Parse(reader["cprecio1"].ToString());

            }
            sqlConnection2.Close();

            return exis;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}