using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Pedido
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);

        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int cont = 0;
        int cont2 = 0;
        float cap = 0;
        int row = 0;
        public Form1()
        {
            InitializeComponent();
            textFeIni.Text = DateTime.Now.ToShortDateString();
            textFecha.Text = DateTime.Now.ToShortDateString();

            //---------------------------Sacar el Folio ---------------------------------
            cmd.CommandText = "select sigped from consec";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                int fol = int.Parse(reader["sigped"].ToString()) + 1;
                textFolio.Text = fol.ToString();
            }
            sqlConnection1.Close();
            //---------------------------------------------------------------------------

            
        }
        //Este metodo obtiene el folio del pedido siguiente 
        private void sacaFolio()
        {
            //---------------------------Sacar el Folio ---------------------------------
            cmd.CommandText = "select sigped from consec";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                int fol = int.Parse(reader["sigped"].ToString()) + 1;
                textFolio.Text = fol.ToString();
                
            }
            sqlConnection1.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select * from admclasificaciones where cidclasificacion > '24' and cidclasificacion < '31'", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));

            dt.Load(reader);

            comboBox2.ValueMember = "CIDCLASIFICACION";
            comboBox2.DisplayMember = "CNOMBRECLASIFICACION";
            comboBox2.DataSource = dt;

            sqlConnection2.Close();

            sqlConnection1.Close();
            sqlConnection1.Open();
            SqlCommand sc2 = new SqlCommand("select letra from clasifi$ group by letra order by letra", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader2;

            reader2 = sc2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("letra", typeof(string));

            dt2.Load(reader2);

            comboBox1.ValueMember = "letra";
            comboBox1.DisplayMember = "letra";
            comboBox1.DataSource = dt2;

            sqlConnection1.Close();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlConnection2.Close();
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select * from admclasificacionesvalores where cidclasificacion = '"+comboBox2.SelectedValue.ToString()+"'", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));

            dt.Load(reader);

            comboBox3.ValueMember = "CIDVALORCLASIFICACION";
            comboBox3.DisplayMember = "CVALORCLASIFICACION";
            comboBox3.DataSource = dt;
            sqlConnection2.Close();
        }

        private void sacaExistencia()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                //{
                string sql = @"with periAnt as (
                select '1' as relaciona, 
                case when     DATEPART(m,getdate()) = 1 then isnull(sum (cEntradasPeriodo1 - cSalidasPeriodo1 ),0) 
                		 when DATEPART(m,getdate()) = 2 then isnull(sum (cEntradasPeriodo1 - cSalidasPeriodo1 ),0)
                		 when DATEPART(m,getdate()) = 3 then isnull(sum (cEntradasPeriodo2 - cSalidasPeriodo2 ),0)
                		 when DATEPART(m,getdate()) = 4 then isnull(sum (cEntradasPeriodo3 - cSalidasPeriodo3 ),0)
                		 when DATEPART(m,getdate()) = 5 then isnull(sum (cEntradasPeriodo4 - cSalidasPeriodo4 ),0) 
                		 when DATEPART(m,getdate()) = 6 then isnull(sum (cEntradasPeriodo5 - cSalidasPeriodo5 ),0) 
                		 when DATEPART(m,getdate()) = 7 then isnull(sum (cEntradasPeriodo6 - cSalidasPeriodo6 ),0) 
                		 when DATEPART(m,getdate()) = 8 then isnull(sum (cEntradasPeriodo7 - cSalidasPeriodo7 ),0) 
                		 when DATEPART(m,getdate()) = 9 then isnull(sum (cEntradasPeriodo8 - cSalidasPeriodo8 ),0) 
                		 when DATEPART(m,getdate()) = 10 then isnull(sum (cEntradasPeriodo9 - cSalidasPeriodo9 ) ,0)
                		 when DATEPART(m,getdate()) = 11 then isnull(sum (cEntradasPeriodo10 - cSalidasPeriodo10 ),0) 
                		 when DATEPART(m,getdate()) = 12 then isnull(sum (cEntradasPeriodo11 - cSalidasPeriodo11 ),0) 
                
                end as cUnidadesAcumulado 
                from admExistenciaCosto inner join admProductos on admExistenciaCosto.CIDPRODUCTO = admProductos.CIDPRODUCTO
                 where CCODIGOPRODUCTO = @Codigo and cIdEjercicio = 3 and cTipoExistencia = 1 
                 and (CIDALMACEN = 1 or CIDALMACEN = 3 or CIDALMACEN = 4 or CIDALMACEN = 5 or CIDALMACEN = 7 or CIDALMACEN = 8 or CIDALMACEN = 24
                 or CIDALMACEN = 20 or CIDALMACEN = 22 or CIDALMACEN = 21 or CIDALMACEN = 19 or CIDALMACEN = 25)
                ),
                
                movs as (
                select isnull(sum( case when cAfectaExistencia=1 then cUnidades 
                when cAfectaExistencia=2 then 0-cUnidades else 0 end ),0)
                as cUnidadesMovto, '1' as relaciona from admMovimientos
                inner join admProductos on admMovimientos.CIDPRODUCTO = admProductos.CIDPRODUCTO where (CCODIGOPRODUCTO = @Codigo) 
                and (cAfectadoInventario = 1 or cAfectadoInventario = 2) 
                and (cFecha >= DATEADD(MONTH, DATEDIFF(MONTH, 0,GETDATE()), 0) and cFecha <= GETDATE()) 
                 and (CIDALMACEN = 1 or CIDALMACEN = 3 or CIDALMACEN = 4 or CIDALMACEN = 5 or CIDALMACEN = 7 or CIDALMACEN = 8 or CIDALMACEN = 24
                 or CIDALMACEN = 20 or CIDALMACEN = 22 or CIDALMACEN = 21 or CIDALMACEN = 19 or CIDALMACEN = 25))
                
                select cUnidadesAcumulado + cUnidadesMovto as existencia from periAnt 
                inner join movs on periAnt.relaciona = movs.relaciona 
                
                ";
                SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                cmd2.Parameters.AddWithValue("@Codigo", row.Cells["Codigo"].Value.ToString());

                sqlConnection2.Close();
                sqlConnection2.Open();
                reader = cmd2.ExecuteReader();
                if (reader.Read())
                {
                    row.Cells["Existe"].Value = reader["existencia"].ToString();
                }
                sqlConnection2.Close();
               
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //--------------------------- Sacar la Capacidad ---------------------------------
                cmd.CommandText = "select sum(porcentaje) as capacidad from tabcap";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {

                    cap = float.Parse(reader["capacidad"].ToString());

                }
                sqlConnection1.Close();
                //-------------------------------------------------------------------------------
                
                
                sqlConnection1.Open();
                SqlCommand cmd2 = new SqlCommand("spPedidoBIEN", sqlConnection1);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@familia", comboBox3.SelectedValue);
                cmd2.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("MM/dd/yyyy"));
                cmd2.Parameters.AddWithValue("@folio", "");
                cmd2.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);
                reader = cmd2.ExecuteReader();
                
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cont].Cells["Codigo"].Value = reader["ccodigoproducto"].ToString().Trim();
                    dataGridView1.Rows[cont].Cells["Nombre"].Value = reader["cnombreproducto"].ToString();
                    dataGridView1.Rows[cont].Cells["Clas"].Value =   reader["letra"].ToString();

                    //                    string sql = @"with periAnt as (
                    //select '1' as relaciona, 
                    //case when     DATEPART(m,getdate()) = 1 then isnull(sum (cEntradasPeriodo1 - cSalidasPeriodo1 ),0) 
                    //		 when DATEPART(m,getdate()) = 2 then isnull(sum (cEntradasPeriodo1 - cSalidasPeriodo1 ),0)
                    //		 when DATEPART(m,getdate()) = 3 then isnull(sum (cEntradasPeriodo2 - cSalidasPeriodo2 ),0)
                    //		 when DATEPART(m,getdate()) = 4 then isnull(sum (cEntradasPeriodo3 - cSalidasPeriodo3 ),0)
                    //		 when DATEPART(m,getdate()) = 5 then isnull(sum (cEntradasPeriodo4 - cSalidasPeriodo4 ),0) 
                    //		 when DATEPART(m,getdate()) = 6 then isnull(sum (cEntradasPeriodo5 - cSalidasPeriodo5 ),0) 
                    //		 when DATEPART(m,getdate()) = 7 then isnull(sum (cEntradasPeriodo6 - cSalidasPeriodo6 ),0) 
                    //		 when DATEPART(m,getdate()) = 8 then isnull(sum (cEntradasPeriodo7 - cSalidasPeriodo7 ),0) 
                    //		 when DATEPART(m,getdate()) = 9 then isnull(sum (cEntradasPeriodo8 - cSalidasPeriodo8 ),0) 
                    //		 when DATEPART(m,getdate()) = 10 then isnull(sum (cEntradasPeriodo9 - cSalidasPeriodo9 ) ,0)
                    //		 when DATEPART(m,getdate()) = 11 then isnull(sum (cEntradasPeriodo10 - cSalidasPeriodo10 ),0) 
                    //		 when DATEPART(m,getdate()) = 12 then isnull(sum (cEntradasPeriodo11 - cSalidasPeriodo11 ),0) 

                    //end as cUnidadesAcumulado 
                    //from admExistenciaCosto inner join admProductos on admExistenciaCosto.CIDPRODUCTO = admProductos.CIDPRODUCTO
                    // where CCODIGOPRODUCTO = @Codigo and cIdEjercicio = 3 and cTipoExistencia = 1 
                    // and (CIDALMACEN = 1 or CIDALMACEN = 3 or CIDALMACEN = 4 or CIDALMACEN = 5   or CIDALMACEN = 24
                    // or CIDALMACEN = 20 or CIDALMACEN = 22 or CIDALMACEN = 21 or CIDALMACEN = 19 or CIDALMACEN = 25)
                    //),

                    //movs as (
                    //select isnull(sum( case when cAfectaExistencia=1 then cUnidades 
                    //when cAfectaExistencia=2 then 0-cUnidades else 0 end ),0)
                    //as cUnidadesMovto, '1' as relaciona from admMovimientos
                    //inner join admProductos on admMovimientos.CIDPRODUCTO = admProductos.CIDPRODUCTO where (CCODIGOPRODUCTO = @Codigo) 
                    //and (cAfectadoInventario = 1 or cAfectadoInventario = 2) 
                    //and (cFecha >= DATEADD(MONTH, DATEDIFF(MONTH, 0,GETDATE()), 0) and cFecha <= GETDATE()) 
                    // and (CIDALMACEN = 1 or CIDALMACEN = 3 or CIDALMACEN = 4 or CIDALMACEN = 5   or CIDALMACEN = 24
                    // or CIDALMACEN = 20 or CIDALMACEN = 22 or CIDALMACEN = 21 or CIDALMACEN = 19 or CIDALMACEN = 25))

                    //select cUnidadesAcumulado + cUnidadesMovto as existencia from periAnt 
                    //inner join movs on periAnt.relaciona = movs.relaciona 

                    //";
                    //                    SqlCommand cmd3 = new SqlCommand(sql, sqlConnection2);
                    //                    cmd3.Parameters.AddWithValue("@Codigo", reader["ccodigoproducto"].ToString().Trim());
                    //                SqlDataReader reader2;

                    //                sqlConnection2.Open();

                    //                    reader2 = cmd3.ExecuteReader();
                    //                    if (reader2.Read())
                    //                    {
                    //                        dataGridView1.Rows[cont].Cells["Existe"].Value = reader2["existencia"].ToString();
                    //                    }
                    //                    sqlConnection2.Close();

                    dataGridView1.Rows[cont].Cells["Existe"].Value = reader["existencia"].ToString();
                    dataGridView1.Rows[cont].Cells["Kilos"].Value = reader["max"].ToString();
                    dataGridView1.Rows[cont].Cells["Pzas"].Value = float.Parse(reader["CIMPORTEEXTRA1"].ToString()) * float.Parse(dataGridView1.Rows[cont].Cells["Existe"].Value.ToString());
                    dataGridView1.Rows[cont].Cells["Kilos2"].Value = float.Parse(reader["CIMPORTEEXTRA1"].ToString()) * float.Parse(dataGridView1.Rows[cont].Cells["Pzas"].Value.ToString());
                    dataGridView1.Rows[cont].Cells["MaxAdmin"].Value = reader["exisbase"].ToString();
                    dataGridView1.Rows[cont].Cells["Espacio"].Value = reader["CZona"].ToString();
                    float resta = float.Parse(dataGridView1.Rows[cont].Cells["Espacio"].Value.ToString()) - float.Parse(dataGridView1.Rows[cont].Cells["MaxAdmin"].Value.ToString());
                    float op = (cap / 4) / 100;
                    dataGridView1.Rows[cont].Cells["Surtido"].Value = ((resta * op) + float.Parse(dataGridView1.Rows[cont].Cells["MaxAdmin"].Value.ToString()));
                    dataGridView1.Rows[cont].Cells["MaxCedis"].Value = float.Parse(reader["maxCedis"].ToString());
                    //if (reader["letra"].ToString() == "A")
                    //{
                    //    dataGridView1.Rows[cont].Cells["MaxCedis"].Value = float.Parse(dataGridView1.Rows[cont].Cells["MaxAdmin"].Value.ToString()) * 1;//1.5
                    //}
                    //else if (reader["letra"].ToString() == "B")
                    //{
                    //    dataGridView1.Rows[cont].Cells["MaxCedis"].Value = float.Parse(dataGridView1.Rows[cont].Cells["MaxAdmin"].Value.ToString()) * 1.10;
                    //}
                    //else if (reader["letra"].ToString() == "C")
                    //{
                    //    dataGridView1.Rows[cont].Cells["MaxCedis"].Value = float.Parse(dataGridView1.Rows[cont].Cells["MaxAdmin"].Value.ToString()) * 1;

                    //}else
                    //{
                    //    dataGridView1.Rows[cont].Cells["MaxCedis"].Value = float.Parse(dataGridView1.Rows[cont].Cells["MaxAdmin"].Value.ToString()) * 0;

                    //}


                    //                    string sql2 = @"with periAnt as (
                    //select '1' as relaciona, 
                    //case when     DATEPART(m,getdate()) = 1 then isnull(sum (cEntradasPeriodo1 - cSalidasPeriodo1 ),0) 
                    //		 when DATEPART(m,getdate()) = 2 then isnull(sum (cEntradasPeriodo1 - cSalidasPeriodo1 ),0)
                    //		 when DATEPART(m,getdate()) = 3 then isnull(sum (cEntradasPeriodo2 - cSalidasPeriodo2 ),0)
                    //		 when DATEPART(m,getdate()) = 4 then isnull(sum (cEntradasPeriodo3 - cSalidasPeriodo3 ),0)
                    //		 when DATEPART(m,getdate()) = 5 then isnull(sum (cEntradasPeriodo4 - cSalidasPeriodo4 ),0) 
                    //		 when DATEPART(m,getdate()) = 6 then isnull(sum (cEntradasPeriodo5 - cSalidasPeriodo5 ),0) 
                    //		 when DATEPART(m,getdate()) = 7 then isnull(sum (cEntradasPeriodo6 - cSalidasPeriodo6 ),0) 
                    //		 when DATEPART(m,getdate()) = 8 then isnull(sum (cEntradasPeriodo7 - cSalidasPeriodo7 ),0) 
                    //		 when DATEPART(m,getdate()) = 9 then isnull(sum (cEntradasPeriodo8 - cSalidasPeriodo8 ),0) 
                    //		 when DATEPART(m,getdate()) = 10 then isnull(sum (cEntradasPeriodo9 - cSalidasPeriodo9 ) ,0)
                    //		 when DATEPART(m,getdate()) = 11 then isnull(sum (cEntradasPeriodo10 - cSalidasPeriodo10 ),0) 
                    //		 when DATEPART(m,getdate()) = 12 then isnull(sum (cEntradasPeriodo11 - cSalidasPeriodo11 ),0) 

                    //end as cUnidadesAcumulado 
                    //from admExistenciaCosto inner join admProductos on admExistenciaCosto.CIDPRODUCTO = admProductos.CIDPRODUCTO
                    // where CCODIGOPRODUCTO = @Codigo and cIdEjercicio = 3 and cTipoExistencia = 1 
                    // and CIDALMACEN = 8
                    //),

                    //movs as (
                    //select isnull(sum( case when cAfectaExistencia=1 then cUnidades 
                    //when cAfectaExistencia=2 then 0-cUnidades else 0 end ),0)
                    //as cUnidadesMovto, '1' as relaciona from admMovimientos
                    //inner join admProductos on admMovimientos.CIDPRODUCTO = admProductos.CIDPRODUCTO where (CCODIGOPRODUCTO = @Codigo) 
                    //and (cAfectadoInventario = 1 or cAfectadoInventario = 2) 
                    //and (cFecha >= DATEADD(MONTH, DATEDIFF(MONTH, 0,GETDATE()), 0) and cFecha <= GETDATE()) 
                    //and CIDALMACEN = 8 )

                    //select cUnidadesAcumulado + cUnidadesMovto as existencia from periAnt 
                    //inner join movs on periAnt.relaciona = movs.relaciona 

                    //";
                    //                    SqlCommand cmd4 = new SqlCommand(sql2, sqlConnection2);
                    //                    cmd4.Parameters.AddWithValue("@Codigo", reader["ccodigoproducto"].ToString().Trim());
                    //                    SqlDataReader reader3;

                    //                    sqlConnection2.Open();

                    //                    reader3 = cmd4.ExecuteReader();
                    //                    if (reader3.Read())
                    //                    {
                    //                        dataGridView1.Rows[cont].Cells["ExiCedis"].Value = reader3["existencia"].ToString();
                    //                    }
                    //                    sqlConnection2.Close();

                    dataGridView1.Rows[cont].Cells["ExiCedis"].Value = reader["existenciaCedis"].ToString();
                    dataGridView1.Rows[cont].Cells["Trans"].Value = reader["realpzas"].ToString();
                    dataGridView1.Rows[cont].Cells["Pzas2"].Value = float.Parse(dataGridView1.Rows[cont].Cells["Surtido"].Value.ToString()) - (float.Parse(dataGridView1.Rows[cont].Cells["Existe"].Value.ToString()) + float.Parse(dataGridView1.Rows[cont].Cells["Trans"].Value.ToString()));
                    dataGridView1.Rows[cont].Cells["Kilos3"].Value = float.Parse(reader["CIMPORTEEXTRA1"].ToString()) * float.Parse(dataGridView1.Rows[cont].Cells["Pzas2"].Value.ToString());
                    dataGridView1.Rows[cont].Cells["Pzas3"].Value = "0";
                    dataGridView1.Rows[cont].Cells["Kilos4"].Value = "0";
                    dataGridView1.Rows[cont].Cells["Peso"].Value = reader["CIMPORTEEXTRA1"].ToString();
                    dataGridView1.Rows[cont].Cells["MP"].Value = reader["MP"].ToString();

                    dataGridView1.Rows[cont].Cells["ExisTotal"].Value = float.Parse(dataGridView1.Rows[cont].Cells["Existe"].Value.ToString()) + float.Parse(dataGridView1.Rows[cont].Cells["ExiCedis"].Value.ToString());
                    dataGridView1.Rows[cont].Cells["ExisReq"].Value = float.Parse(dataGridView1.Rows[cont].Cells["MaxAdmin"].Value.ToString()) + float.Parse(dataGridView1.Rows[cont].Cells["MaxCedis"].Value.ToString());
                    cont++;
                }
                sqlConnection1.Close();


        }
            catch (Exception)
            {
                
            }
            //sacaExistencia();
            transito();
            //label1.Text = CStr(datagridview1.Row.Count);
            textBox1.Text = dataGridView1.Rows.Count.ToString();


            
            Double totalPedido = 0;
            Double totalSug = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToDouble(row.Cells["PedSug"].Value) > 0)
                {
                    totalPedido += Math.Round(Convert.ToDouble(row.Cells["PedSug"].Value), 2);
                }
                if (Convert.ToDouble(row.Cells["PedRel"].Value) > 0)
                {
                    totalSug += Math.Round(Convert.ToDouble(row.Cells["PedRel"].Value), 2);
                }
                
                
                //PedRel

            }
            
            textBox2.Text = totalPedido.ToString();
            textBox3.Text = totalSug.ToString();
        }

        private void transito()
        {
           
                int contador = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    try
                    {
                        //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                        //{
                        string sql = "select isnull(sum(realpza),0) as transito from bdpbno where cerrado = '' and codigo = '" + dataGridView1.Rows[contador].Cells["Codigo"].Value.ToString() + "'";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                        //cmd.Parameters.AddWithValue("@param1", textFolio.Text); //Para grabar algo de un textbox
                        //cmd.Parameters.AddWithValue("@param2", row.Cells["Column1"].Value.ToString()); //Para grabar una columna

                        sqlConnection1.Close();
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                             dataGridView1.Rows[contador].Cells["Trans"].Value = reader["transito"].ToString();
                            dataGridView1.Rows[contador].Cells["PedSug"].Value = ((float.Parse(dataGridView1.Rows[contador].Cells["ExisTotal"].Value.ToString()) + float.Parse(dataGridView1.Rows[contador].Cells["Trans"].Value.ToString()) - float.Parse(dataGridView1.Rows[contador].Cells["ExisReq"].Value.ToString())) * -1);
                            dataGridView1.Rows[contador].Cells["PedRel"].Value =   float.Parse(dataGridView1.Rows[contador].Cells["PedSug"].Value.ToString()) * float.Parse(dataGridView1.Rows[contador].Cells["Peso"].Value.ToString());
                        }
                        contador += 1;
                        sqlConnection1.Close();
                    }
                    catch (NullReferenceException)
                    {
                    contador += 1;
                    sqlConnection1.Close();

                }

                    
                    //}
                }
          
            
        }


        private void limpiar()
        {
            cont = 0;
            dataGridView1.Rows.Clear();
            textProveedor.Clear();
            textBox5.Clear();
            textBox9.Clear();
            textObs.Clear();
            textPrf.Clear();
            textPrc.Clear();
            textProveedor.Clear();

            textFeIni.Text = DateTime.Now.ToShortDateString();
            textFecha.Text = DateTime.Now.ToShortDateString();
            sacaFolio();

        }


        private void borrar()
        {
            cmd.CommandText = "delete from bdpbno where folio = '" + textFolio.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();


            sqlConnection1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Actualizar el folio
            //"update consec set sigped = '"+textFolio.Text+"'"

            //--------------------------- ACTUALIZAR FOLIO ---------------------------------
            cmd.CommandText = "update consec set sigped = '" + textFolio.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

          
            sqlConnection1.Close();


            borrar();
            // ------------------------------------------GRABAR-----------------------------------------
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Pzas3"].Value.ToString() != "0")
                    {
                        string sql = "insert into bdpbno (folio, codigo, nombre, clasif, exipza, exikil, maxpza, maxkil, pedpza, pedkil, realpza, realkil, kilos, calibre, proveedor, fecha, prf, prc, observa, cerrado, maxAdmin, espacio, surtido, transito) values (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13,@param14,@param15,@param16,@param17,@param18,@param19,@param20,@param21, @param22,@param23, @param24)";
                        SqlCommand cmd2 = new SqlCommand(sql, sqlConnection1);
                        cmd2.Parameters.AddWithValue("@param1", textFolio.Text); //Folio
                        cmd2.Parameters.AddWithValue("@param2", row.Cells["Codigo"].Value.ToString()); //Codigo
                        cmd2.Parameters.AddWithValue("@param3", row.Cells["Nombre"].Value.ToString()); //Nombre
                        cmd2.Parameters.AddWithValue("@param4", row.Cells["Clas"].Value.ToString()); //Clasif
                        cmd2.Parameters.AddWithValue("@param5", row.Cells["Existe"].Value.ToString()); //Exipza
                        cmd2.Parameters.AddWithValue("@param6", row.Cells["Kilos"].Value.ToString()); //Exikil
                        cmd2.Parameters.AddWithValue("@param7", row.Cells["Pzas"].Value.ToString()); //Maxpza
                        cmd2.Parameters.AddWithValue("@param8", row.Cells["Kilos2"].Value.ToString()); //maxkil
                        cmd2.Parameters.AddWithValue("@param9", row.Cells["Pzas2"].Value.ToString()); //pedpza
                        cmd2.Parameters.AddWithValue("@param10", row.Cells["Kilos3"].Value.ToString()); //pedkil
                        cmd2.Parameters.AddWithValue("@param11", row.Cells["Pzas3"].Value.ToString()); //realpza
                        cmd2.Parameters.AddWithValue("@param12", row.Cells["Kilos4"].Value.ToString()); //realkil
                        cmd2.Parameters.AddWithValue("@param13", row.Cells["Peso"].Value.ToString()); //kilos
                        cmd2.Parameters.AddWithValue("@param14", "0"); //calibre
                        cmd2.Parameters.AddWithValue("@param15", textProveedor.Text); //proveedor
                        cmd2.Parameters.AddWithValue("@param16", textFecha.Text); //fecha
                        cmd2.Parameters.AddWithValue("@param17", textPrf.Text); //prf
                        cmd2.Parameters.AddWithValue("@param18", textPrc.Text); //prc
                        cmd2.Parameters.AddWithValue("@param19", textObs.Text); //observa
                        cmd2.Parameters.AddWithValue("@param20", ""); //cerrado
                        cmd2.Parameters.AddWithValue("@param21", row.Cells["MaxAdmin"].Value.ToString()); //maxadmin
                        cmd2.Parameters.AddWithValue("@param22", row.Cells["Espacio"].Value.ToString()); //espacio
                        cmd2.Parameters.AddWithValue("@param23", row.Cells["Surtido"].Value.ToString()); //surtido
                        cmd2.Parameters.AddWithValue("@param24", row.Cells["Trans"].Value.ToString()); //transito

                        sqlConnection1.Open();
                        cmd2.ExecuteNonQuery();
                        sqlConnection1.Close(); 
                    }
                }
        }
            catch (NullReferenceException)
            {

            }

            MessageBox.Show("Guardado");

        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                dataGridView1.Rows[e.RowIndex].Cells["Kilos4"].Value = float.Parse(dataGridView1.Rows[e.RowIndex].Cells["Pzas3"].Value.ToString()) * float.Parse(dataGridView1.Rows[e.RowIndex].Cells["Peso"].Value.ToString());
             
            }
            catch
            {

            }

            Double totalMonto = 0;
            Double totalPedido = 0;
            Double totalSug = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                totalMonto +=Math.Round(Convert.ToDouble(row.Cells["Kilos4"].Value),2);
                if (Convert.ToDouble(row.Cells["PedSug"].Value) > 0)
                {
                    totalPedido += Math.Round(Convert.ToDouble(row.Cells["PedSug"].Value), 2);
                }
                if (Convert.ToDouble(row.Cells["PedRel"].Value) > 0)
                {
                    totalSug += Math.Round(Convert.ToDouble(row.Cells["PedRel"].Value), 2);
                }
                //PedRel

            }
            textBox9.Text = totalMonto.ToString();
            textBox2.Text = totalPedido.ToString();
            textBox3.Text = totalSug.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Parse(textFeIni.Text);
            Variablescompartidas.Fecha = fecha.ToString("dd/MM/yyyy");
            Variablescompartidas.Folio = textFolio.Text;
            Variablescompartidas.RC = textPrc.Text;
            Variablescompartidas.RF = textPrf.Text;
            Variablescompartidas.Observaciones = textObs.Text;
            Variablescompartidas.proveedor = textProveedor.Text;
            using (repoPedido RP = new repoPedido(dataGridView1))
            {
                RP.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                dataGridView1.Rows.Clear();
                cont = 0;
                //---------------------------------  CARGA DE ARCHIVOS  ----------------------------------
                using (CargaPedido cp = new CargaPedido())              
                {                                                       
                    cp.ShowDialog();                                    
                }                                                       
                //----------------------------------- Sacar 4l datagrid ----------------------------------
                cmd.CommandText = "select * from bdpbno where folio ='" + Variablescompartidas.Folio + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Close();
                sqlConnection1.Open();
                reader = cmd.ExecuteReader();

                try
                {
                // Data is accessible through the DataReader object here.
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add();
                        textFolio.Text = reader["folio"].ToString();
                        textProveedor.Text = reader["proveedor"].ToString();
                        textFecha.Text = reader["fecha"].ToString();
                        textPrc.Text = reader["prc"].ToString();
                        textPrf.Text = reader["prf"].ToString();

                        dataGridView1.Rows[cont].Cells["Codigo"].Value = reader["codigo"].ToString();
                        dataGridView1.Rows[cont].Cells["Nombre"].Value = reader["nombre"].ToString();
                        dataGridView1.Rows[cont].Cells["Clas"].Value = reader["clasif"].ToString();
                        dataGridView1.Rows[cont].Cells["Existe"].Value = reader["exipza"].ToString();
                        dataGridView1.Rows[cont].Cells["Kilos"].Value = reader["exikil"].ToString();
                        dataGridView1.Rows[cont].Cells["Pzas"].Value = reader["maxpza"].ToString();
                        dataGridView1.Rows[cont].Cells["Kilos2"].Value = reader["maxkil"].ToString();

                        dataGridView1.Rows[cont].Cells["MaxAdmin"].Value = reader["maxAdmin"].ToString();
                        dataGridView1.Rows[cont].Cells["Espacio"].Value = reader["espacio"].ToString();
                        dataGridView1.Rows[cont].Cells["Surtido"].Value = reader["surtido"].ToString();
                        
                        dataGridView1.Rows[cont].Cells["Surtido"].Value = float.Parse(reader["surtido"].ToString())/2;
                        
                        dataGridView1.Rows[cont].Cells["Trans"].Value = reader["transito"].ToString();
                        
                        dataGridView1.Rows[cont].Cells["Pzas2"].Value = reader["pedpza"].ToString();
                        dataGridView1.Rows[cont].Cells["Kilos3"].Value = reader["pedkil"].ToString();
                        
                        dataGridView1.Rows[cont].Cells["Pzas3"].Value = reader["realpza"].ToString();
                        dataGridView1.Rows[cont].Cells["Kilos4"].Value = reader["realkil"].ToString();
                        dataGridView1.Rows[cont].Cells["Peso"].Value = reader["kilos"].ToString();

                        cont++;

                    }
                }
                catch (Exception)
                {

                }
                sqlConnection1.Close();

                button9.Visible = true;
                //----------------------------------------------------------------------------------------- 
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Fecha = textFeIni.Text;
            Variablescompartidas.Folio = textFolio.Text;
            Variablescompartidas.RC = textPrc.Text;
            Variablescompartidas.RF = textPrf.Text;
            Variablescompartidas.Observaciones = textObs.Text;
            Variablescompartidas.proveedor = textProveedor.Text;
            using (ReporteCopia Rc = new ReporteCopia(dataGridView1))
            {
                Rc.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            limpiar();
            button9.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Esta seguro que desea cerrar este pedido?", "Salir", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                cierre();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        private void cierre()
        {

            try
            {
                string sql = "update bdpbno set cerrado = 'S' where folio = @param1";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@param1", textFolio.Text); // Para grabar algo de un textbox
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("Pedido cerrado");
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void textPrf_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textPrf_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                try
                {
                    double a = float.Parse(textPrf.Text);
                    double b = a + 3.5;
                    textPrf.Text = b.ToString();
                }
                catch (Exception)
                {
                    
                }

            }
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.Rows[dataGridView1.CurrentRow.Index].DefaultCellStyle.BackColor = System.Drawing.Color.Red;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Existe")
            {
                Variablescompartidas.CodigoPro = dataGridView1.Rows[e.RowIndex].Cells["Codigo"].Value.ToString();
                Variablescompartidas.NombrePro = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                using (Existencias ex = new Existencias())
                {
                    ex.ShowDialog();
                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            //row = 0;
            //try
            //{
            //    dataGridView1.Rows[row].DefaultCellStyle.BackColor = System.Drawing.Color.White;
            //    dataGridView1.Rows[row].Cells[10].Style.BackColor = System.Drawing.Color.PaleTurquoise;
            //    dataGridView1.Rows[row].Cells[11].Style.BackColor = System.Drawing.Color.PaleTurquoise;

            //    int row2 = dataGridView1.CurrentRow.Index;
            //    dataGridView1.Rows[row2].DefaultCellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
            //    row = row2;
            //}
            //catch (NullReferenceException)
            //{

            //    //throw;
            //}
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}