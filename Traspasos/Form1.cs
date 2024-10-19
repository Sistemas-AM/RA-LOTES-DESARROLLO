using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace Traspasos
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.Flotillas);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string copia = "";
        string notifi = "";
        int fol = 0;
        public static string foliocom { get; set; }
        public static string sucursalcom { get; set; }
        public static string SucursalNomCom { get; set; }
        public static string Carro2 { get; set; }
        public static string Chofer2 { get; set; }
        public static string Placas2 { get; set; }

        public static string Devolucion { get; set; }

        public static string foliodevol { get; set; }

        public string valsel;
        string idregreso = "";
        int regreso = 0;
        int count = 0;

        string sucursal, idalmacen, idtrapaso, letra, iddestinoreal, destinoreal = "";

        [DllImport("MGWSERVICIOS.DLL")]
        public static extern int fInicializaSDK();
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern int fSetNombrePAQ(string aNombrePAQ);
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern int fAbreEmpresa(string Directorio);
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern void fTerminaSDK();
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern void fCierraEmpresa();
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern void fError(int NumeroError, StringBuilder Mensaje, int Longitud);
        [DllImport("KERNEL32")]
        public static extern int SetCurrentDirectory(string pPtrDirActual);
        [DllImport("MGWSERVICIOS.dll")]
        public static extern int fLeeDatoDocumento(string aCampo, StringBuilder aValor, int aLen);
        [DllImport("MGWSERVICIOS.dll")]
        public static extern int fAfectaDocto_Param(string aCodConcepto, string aSerie, double aFolio, bool aAfecta);
        [DllImport("MGWSERVICIOS.dll")]
        public static extern int fSetDatoDocumento(string aCampo, string aValor);
        [DllImport("MGWSERVICIOS.dll")]
        public static extern int fSetDatoDireccion(string aCampo, string aValor);
        [DllImport("MGWSERVICIOS.dll")]
        public static extern int fBuscaCteProv(string aCodigoCteProv);
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern void fInsertarDocumento();
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern int fGuardaDocumento();
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern int fInsertaDireccion();
        [DllImport("MGWSERVICIOS.DLL")]
        public static extern int fGuardaDireccion();
        [DllImport("MGWSERVICIOS.dll")]
        public static extern int fInsertarMovimiento();
        [DllImport("MGWSERVICIOS.dll")]
        public static extern int fGuardaMovimiento();
        [DllImport("MGWSERVICIOS.dll")]
        public static extern int fSetDatoMovimiento(string aCampo, string aValor);

        public Form1()
        {
            InitializeComponent();
            cargaCombo();
            Variablescompartidas.carga = "0";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Codigo")
            {
                Variablescompartidas.sucursalExistencia = comboBox1.Text;
                Variablescompartidas.almExistencia = idalmacen;
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value != null)
                {
                    Variablescompartidas.ProductoPaso = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
                    using (Productos p = new Productos())
                    {
                        p.ShowDialog();
                    }

                    if (Variablescompartidas.cancelado == "0")
                    {
                      //dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Codigo"].Value = Variablescompartidas.codigo;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value = Variablescompartidas.nombre;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Destino"].Value = Variablescompartidas.destino;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = Variablescompartidas.cantidad;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Medida"].Value = Variablescompartidas.medida;
                        suma();
                    }

                }
                else
                {
                    Variablescompartidas.ProductoPaso = "";
                    using (Productos p = new Productos())
                    {
                        p.ShowDialog();
                    }

                    if (Variablescompartidas.cancelado == "0")
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Codigo"].Value = Variablescompartidas.codigo;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Nombre"].Value = Variablescompartidas.nombre;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Destino"].Value = Variablescompartidas.destino;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Cantidad"].Value = Variablescompartidas.cantidad;
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells["Medida"].Value = Variablescompartidas.medida;
                        suma();
                    }
                }
                

            }
        }

        private void suma()
        {
            try
            {
                Double SubtT = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    SubtT += Math.Round(Convert.ToDouble(row.Cells["Medida"].Value), 2);
                }
                textBox3.Text = SubtT.ToString();

                //iva();
            }
            catch (Exception)
            {


            }
        }


        private void cargaCombo()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select sucursal, sucnom, idalmacen from folios where idtrassal != 0", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucnom", typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "sucursal";
            comboBox1.DisplayMember = "sucnom";
            comboBox1.DataSource = dt;
            sqlConnection2.Close();
        }

        public static void rError(int iError)
        {
            StringBuilder sMensaje = new StringBuilder(512);
            if (iError != 0)
            {
                fError(iError, sMensaje, 512);
                Console.WriteLine("Error: " + sMensaje);
                Console.Read();
            }
        }



        public void nDato(string leeDato)
        {
            StringBuilder revalor = new StringBuilder(11);
            fLeeDatoDocumento(leeDato, revalor, 11);
            valsel = revalor.ToString();
        }

        private void ActualizaReq()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string sql = "update dbsurtido set estatus = '1' where folio = @folio";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
                //cmd.Parameters.AddWithValue("@param1", textFolio.Text); //Para grabar algo de un textbox
                cmd.Parameters.AddWithValue("@folio", textBox1.Text); //Para grabar una columna
                
                sqlConnection2.Open();
                cmd.ExecuteNonQuery();
                sqlConnection2.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Chofer.Text != "" && Carro.Text != "" && Placas.Text != "")
            {
                if (Variablescompartidas.carga == "0")
                {
                    if (!textBox1.Text.Contains("-R") )
                    {
                      cargafolio(); 
                    }

                    progressBar1.Value = 10;
                    //guardarContpaq();
                    obtenReal();
                    progressBar1.Value = 20;

                    guardarTraspaso();  

                    if (!textBox1.Text.Contains("-R"))
                    {
                      updatefolio(); 
                        
                    }else
                    {
                        ActualizaReq();
                    }
                    progressBar1.Value = 30;

                    foliocom = textBox1.Text;
                    progressBar1.Value = 40;
                    sucursalcom = sucursal;
                    SucursalNomCom = comboBox1.Text;
                    progressBar1.Value = 50;
                    Carro2 = Carro.Text;
                    Chofer2 = Chofer.Text;
                    progressBar1.Value = 60;
                    Placas2 = Placas.Text;
                    progressBar1.Value = 70;

                    progressBar1.Value = 100;
                    if (checkBox2.Checked)
                    {
                        using (FormatoCliente fp = new FormatoCliente())
                        {
                            fp.ShowDialog();
                        }

                    }
                    else
                    {
                        //ReportesTraspasos.variablescompartidas.devolucion = Devolucion;
                        //ReportesTraspasos.variablescompartidas.foliodevol = foliodevol;
                        //ReportesTraspasos.variablescompartidas.foliocom = foliocom;
                        //ReportesTraspasos.variablescompartidas.sucursalcom = sucursalcom;
                        //ReportesTraspasos.variablescompartidas.SucursalNomCom = SucursalNomCom;
                        //using (ReportesTraspasos.Form1 rt = new ReportesTraspasos.Form1())
                        //{
                        //    rt.ShowDialog();
                        //}
                        using (Formato fp = new Formato())
                        {
                            fp.ShowDialog();
                        }
                        using (Formato2 fp2 = new Formato2())
                        {
                            fp2.ShowDialog();
                        }
                    }
                }
                else
                {
                    updatechofer();
                }
            }
            else
            {
                MessageBox.Show("Captura los datos");
            }
        }

        private void obtenReal()
        {
            cmd.CommandText = "select idalmacen, sucnom from folios where almtra = '" + Variablescompartidas.destinoNom + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Close();
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                iddestinoreal = reader["idalmacen"].ToString();
                destinoreal = reader["sucnom"].ToString();
            }
            sqlConnection2.Close();
        }

        private void guardarTraspaso()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //if (row.Cells["column14"].Value.ToString() != "0") Esto es para agrear alguna codicion
                    //{
                    string sql = @"insert into traspasos values 
                        (@Folio, @Fecha, @Codigo, @Nombre, @Cantidad,@idDesde, @Desde, @idDestino, 
                        @Destino, @Estatus, @Chofer, @Carro, @Placas, @idreal, @destinoreal, @fecharec, 
                        @peso, @referencia, @Observacion, @comentarios, @razon, @obsRazon, @remolque, @ObservaGeneral, @referencia2, @codigocliente)";

                    SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
                    cmd.Parameters.AddWithValue("@Folio", textBox1.Text);
                    DateTime Fecha = DateTime.Parse(dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@Fecha", Fecha.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("@Codigo", row.Cells["Codigo"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Nombre", row.Cells["Nombre"].Value.ToString());
                    cmd.Parameters.AddWithValue("@idDesde", idalmacen);
                    cmd.Parameters.AddWithValue("@Desde", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@idDestino", Variablescompartidas.destino);
                    cmd.Parameters.AddWithValue("@Destino", Variablescompartidas.destinoNom);
                    cmd.Parameters.AddWithValue("@Cantidad", row.Cells["Cantidad"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Estatus", "T");
                    cmd.Parameters.AddWithValue("@Chofer", Chofer.Text);
                    cmd.Parameters.AddWithValue("@Carro", Carro.Text);
                    cmd.Parameters.AddWithValue("@Placas", Placas.Text);
                    cmd.Parameters.AddWithValue("@idreal", iddestinoreal);
                    cmd.Parameters.AddWithValue("@destinoreal", destinoreal);
                    cmd.Parameters.AddWithValue("@fecharec", "");
                    cmd.Parameters.AddWithValue("@peso", row.Cells["Medida"].Value.ToString());
                    cmd.Parameters.AddWithValue("@referencia", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Observacion", "-");
                    cmd.Parameters.AddWithValue("@Comentarios", "-");
                    if (comboBox2.Text != "")
                    {
                        cmd.Parameters.AddWithValue("@razon", comboBox2.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@razon", "-");
                    }

                    if (textBox2.Text != "")
                    {
                        cmd.Parameters.AddWithValue("@obsRazon", textBox2.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@obsRazon", "");
                    }

                    if (checkBox1.Checked)
                    {
                        cmd.Parameters.AddWithValue("@remolque", comboBox3.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@remolque", "-");
                    }

                    cmd.Parameters.AddWithValue("@ObservaGeneral", ObsGen.Text);
                    cmd.Parameters.AddWithValue("@referencia2", textBox4.Text);

                    if (checkBox2.Checked)
                    {
                        cmd.Parameters.AddWithValue("@codigocliente", Variablescompartidas.codigoCliente);
                    }else
                    {
                        cmd.Parameters.AddWithValue("@codigocliente", "-");
                    }


                    sqlConnection2.Open();

                    cmd.ExecuteNonQuery();
                    sqlConnection2.Close();
                    
                }
            }
            catch (NullReferenceException)
            {


            }
            MessageBox.Show("Guardado");
            button1.Enabled = false;
            button10.Enabled = true;
            updatechofer();
        }

        private void updatechofer()
        {
            string sql = "update traspasos set chofer = @chofer where folio = @folio";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@folio", textBox1.Text);
            cmd.Parameters.AddWithValue("@chofer", Chofer.Text);

            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
            MessageBox.Show("Chofer Actualizado");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.File.Delete(Path.GetFullPath(copia));
            }
            catch (ArgumentException)
            {


            }
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            dataGridView2.Rows.Clear();
            Variablescompartidas.SucursalRecibir = comboBox1.Text;
            using (Recibidos rc = new Recibidos())
            {
                rc.ShowDialog();
            }

            llenadata();
        }



        private void llenadata()
        {
            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            cmd.CommandText = "select * from traspasos where folio = '" + Variablescompartidas.Folio + "' and destinoreal = '" + comboBox1.Text + "' and estatus != 'R'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            //try
            //{
            // Data is accessible through the DataReader object here.
            while (reader.Read())
            {
                dataGridView2.Rows.Add();
                // textBox1.Text = reader["ALGO"].ToString();
                FolioRec.Text = reader["Folio"].ToString();
                ChoferRec.Text = reader["Chofer"].ToString();
                CarroRec.Text = reader["Carro"].ToString();
                PlacasRec.Text = reader["Placas"].ToString();

                dataGridView2.Rows[count].Cells[0].Value = reader["Codigo"].ToString();
                //DateTime dt = DateTime.Parse(reader["Fecha"].ToString());
                dataGridView2.Rows[count].Cells[1].Value = reader["Nombre"].ToString();
                dataGridView2.Rows[count].Cells[2].Value = reader["Desde"].ToString();
                dataGridView2.Rows[count].Cells[3].Value = reader["Cantidad"].ToString();
                dataGridView2.Rows[count].Cells[4].Value = reader["Cantidad"].ToString();
                dataGridView2.Rows[count].Cells[5].Value = float.Parse(dataGridView2.Rows[count].Cells[3].Value.ToString()) - float.Parse(dataGridView2.Rows[count].Cells[4].Value.ToString());
                count++;

            }
            //}
            //catch (Exception)
            //{


            //}
            sqlConnection2.Close();
        }

        private void dataGridView2_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView2.Rows[e.RowIndex].Cells[5].Value = float.Parse(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString()) - float.Parse(dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString());
            }
            catch (Exception)
            {

            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                Devolucion = "0";
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["Column6"].Value.ToString() == "0")
                    {
                        string sql = "update Traspasos set Estatus = 'R', FechaRecibido = @param3 where Folio = @param1 and Codigo = @param2";
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
                        //cmd.Parameters.AddWithValue("@param1", textFolio.Text); //Para grabar algo de un textbox
                        cmd.Parameters.AddWithValue("@param1", FolioRec.Text); //Para grabar una columna
                        cmd.Parameters.AddWithValue("@param2", row.Cells["Column1"].Value.ToString()); //Para grabar una columna

                        cmd.Parameters.AddWithValue("@param3", DateTime.Now.ToString("MM/dd/yyyy")); //Para grabar una columna
                        sqlConnection2.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection2.Close();

                    }

                    else if (Int32.Parse(row.Cells["Column6"].Value.ToString()) > 0)
                    {
                        Devolucion = "1";
                        folioRegreso(row.Cells["Column3"].Value.ToString());
                        FolioRegreso2(idregreso);

                        //string sql = "insert into TABLA (Valor1, Valor2) values (@param1, @param2))";
                        SqlCommand cmd = new SqlCommand("spTraspasosDev", sqlConnection2);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Folio", FolioRec.Text);
                        cmd.Parameters.AddWithValue("@Codigo", row.Cells["Column1"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Cantidad1", row.Cells["Column5"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Cantidad2", row.Cells["Column6"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("MM/dd/yyyy"));
                        cmd.Parameters.AddWithValue("@FolioNuevo", regreso.ToString());
                        cmd.Parameters.AddWithValue("@Tipo", "1");

                        if (row.Cells["Column7"].Value == null)
                        {
                            cmd.Parameters.AddWithValue("@Observacion", "-");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Observacion", row.Cells["Column7"].Value.ToString());
                        }

                        if (row.Cells["Column8"].Value == null)
                        {
                            cmd.Parameters.AddWithValue("@Comentarios", "-");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Comentarios", row.Cells["Column8"].Value.ToString());
                        }

                        sqlConnection2.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection2.Close();




                    }

                    else if (Int32.Parse(row.Cells["Column6"].Value.ToString()) < 0)
                    {

                        folioRegreso(row.Cells["Column3"].Value.ToString());
                        FolioRegreso2(idregreso);

                        //string sql = "insert into TABLA (Valor1, Valor2) values (@param1, @param2))";
                        SqlCommand cmd = new SqlCommand("spTraspasosDev", sqlConnection2);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Folio", FolioRec.Text);
                        cmd.Parameters.AddWithValue("@Codigo", row.Cells["Column1"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Cantidad1", row.Cells["Column5"].Value.ToString());

                        int diferencia = Int32.Parse(row.Cells["Column6"].Value.ToString());
                        int cantidad = diferencia * -1;
                        cmd.Parameters.AddWithValue("@Cantidad2", cantidad.ToString());
                        cmd.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("MM/dd/yyyy"));
                        cmd.Parameters.AddWithValue("@FolioNuevo", FolioRec.Text + "-C");
                        cmd.Parameters.AddWithValue("@Tipo", "2");
                        if (row.Cells["Column7"].Value == null)
                        {
                            cmd.Parameters.AddWithValue("@Observacion", "-");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Observacion", row.Cells["Column7"].Value.ToString());
                        }

                        if (row.Cells["Column8"].Value == null)
                        {
                            cmd.Parameters.AddWithValue("@Comentarios", "-");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Comentarios", row.Cells["Column8"].Value.ToString());
                        }

                        sqlConnection2.Open();
                        cmd.ExecuteNonQuery();
                        sqlConnection2.Close();




                    }
                }


            }
            catch (NullReferenceException)
            {


            }

            MessageBox.Show("Guardado");

            if (Devolucion == "1")
            {
                foliodevol = regreso.ToString();
                foliocom = FolioRec.Text;
                sucursalcom = sucursal;
                SucursalNomCom = comboBox1.Text;
                Carro2 = CarroRec.Text;
                Chofer2 = ChoferRec.Text;
                Placas2 = PlacasRec.Text;
                using (Formato fp = new Formato())
                {
                    fp.ShowDialog();
                }
            }
        }


        private void folioRegreso(string Sucu)
        {
            cmd.CommandText = "select idconalmt from folios where sucnom = '" + Sucu + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                idregreso = reader["idconalmt"].ToString();

            }
            sqlConnection2.Close();
        }

        private void FolioRegreso2(string idreg)
        {
            regreso = 0;
            cmd.CommandText = "select CNOFOLIO from admconceptos where CIDCONCEPTODOCUMENTO = '" + idreg + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                regreso = Int32.Parse(reader["CNOFOLIO"].ToString());
                regreso = regreso + 1;


            }
            sqlConnection1.Close();
        }

        private void devuelve(string folio, string codigo, string fecharec)
        {
            string sql = "update Traspasos set Estatus = 'D', FechaRecibido = @FechaRec where Folio = @Folio and Codigo = @Codigo";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            //cmd.Parameters.AddWithValue("@param1", textFolio.Text); //Para grabar algo de un textbox
            cmd.Parameters.AddWithValue("@Folio", folio); //Para grabar una columna
            cmd.Parameters.AddWithValue("@Codigo", codigo); //Para grabar una columna
            cmd.Parameters.AddWithValue("@FechaRec", fecharec); //Para grabar una columna
            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foliocom = FolioRec.Text;
            sucursalcom = sucursal;
            SucursalNomCom = dataGridView2.Rows[0].Cells["Column3"].Value.ToString();
            Carro2 = CarroRec.Text;
            Chofer2 = ChoferRec.Text;
            Placas2 = PlacasRec.Text;

            using (Formato fp = new Formato())
            {
                fp.ShowDialog();
            }

            using (Formato2 fp2 = new Formato2())
            {
                fp2.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            using (Reporte rp = new Reporte())
            {
                rp.ShowDialog();
            }

            if (Reporte.Ban != "0")
            {
                ExcelTraspaso(Reporte.fecha1, Reporte.fecha2);
            }
        }

        private void ExcelTraspaso(string fecha1, string fecha2)
        {

            int proceso = 0;
            progressBar1.Value = 0;
            string ruta = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Traspasos\Plantilla\Plantilla.xlsx";
            copia = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Traspasos\Plantilla\Copia\Formato" + "-" + DateTime.Now.ToString("dd-MM-yyyy") + "-" + DateTime.Now.Second.ToString() + ".xlsx";

            System.IO.FileInfo vArchivo = new System.IO.FileInfo(ruta);

            if (System.IO.File.Exists(copia))

            {
                System.IO.File.Delete(copia);
            }


            vArchivo.CopyTo(copia);

            sqlConnection2.Open();
            string sql = @"select Folio, Fecha, Codigo, Nombre, Cantidad, Peso,(Cantidad * peso) as total, Desde, DestinoReal from Traspasos where Fecha between '" + fecha1 + "' and '" + fecha2 + "' and Estatus = 'T' and DestinoReal = '"+comboBox1.Text+"'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable miTabla = ds.Tables[0];

            sqlConnection2.Close();

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook sheet;
            Microsoft.Office.Interop.Excel.Worksheet x;

            sheet = excel.Workbooks.Open(copia);
            x = excel.Worksheets["Hoja1"];
            x.Range["I3"].Value = DateTime.Now.ToString("MM/dd/yyyy");
            x.Range["I4"].Value = DateTime.Now.ToShortTimeString();
            //x.Range["K2"].Value = DateTime.Parse(dateTimePicker1.Text).ToString("dd/MM/yyyy");

            //double columnas = 100 / miTabla.Rows.Count;
            //double filas = columnas / miTabla.Columns.Count;
            //int pro = Convert.ToInt32(filas);

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Weight = 2d;
                    x.Cells[j + 6, k + 1].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Weight = 2d;
                    x.Cells[j + 6, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();


                    proceso += 1;
                    if (proceso >= 100)
                    {
                        proceso = 99;
                    }
                    progressBar1.Value = proceso;
                }
            }

            //x.Range["I11"].Value = "XL";

            sheet.Save();
            sheet.Close(0);
            excel.Quit();
            progressBar1.Value = 100;
            DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
            if (AbrirExcel == DialogResult.Yes)
            {
                excel.Visible = true;
                excel.Workbooks.Open(copia);
            }
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Chofer.Clear();
            //Carro.Clear();
            //Placas.Clear();
            using (Choferes cf = new Choferes())
            {
                cf.ShowDialog();
            }
            Chofer.Text = Variablescompartidas.ChoferNom;
            if (Variablescompartidas.SucursalChofer != "")
            {
                MessageBox.Show("Este Chofer Pertenece a: " + Variablescompartidas.SucursalChofer);
            }
            if (Variablescompartidas.ChoferNom == "CLIENTE")
            {
                Carro.Text = "Cliente: ";
                Carro.Enabled = true;
                Placas.Enabled = true;
                Chofer.Enabled = true;
                label11.Visible = true;
                comboBox2.Visible = true;
                label10.Visible = true;
                textBox2.Visible = true;
            }
            else
            {
                Chofer.Enabled = false;
                Carro.Enabled = false;
                Placas.Enabled = false;
                //label11.Visible = false;
                //comboBox2.Visible = false;
                //label10.Visible = false;
                //textBox2.Visible = false;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            label11.Visible = false;
            comboBox2.Visible = false;
            label10.Visible = false;
            textBox2.Visible = false;
            textBox2.Clear();
            comboBox2.Text = "";

            Variablescompartidas.SucursalEnvio = comboBox1.Text;
            using (Carros cf = new Carros())
            {
                cf.ShowDialog();
            }
            Carro.Text = Variablescompartidas.Carro;
            Placas.Text = Variablescompartidas.Placa;
            //buscar.Text.Trim().Replace("'", "''")
            if (Variablescompartidas.asigna != comboBox1.Text.Trim().Replace("'", "''"))
            {
                MessageBox.Show("Este Auto Pertenece A: " + Variablescompartidas.asigna);
                label11.Visible = true;
                comboBox2.Visible = true;
                label10.Visible = true;
                textBox2.Visible = true;

            }
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Se borrara todo lo capturado?", "Limpiar", MessageBoxButtons.YesNoCancel);
            Variablescompartidas.carga = "0";
            progressBar1.Value = 0;
            textBox3.Clear();
            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                Variablescompartidas.Vuelta = "0";
                ClearTextBoxes();
                Chofer.Enabled = false;
                Carro.Enabled = false;
                Placas.Enabled = false;
                comboBox2.Text = "";
                comboBox2.Visible = false;
                textBox2.Clear();
                textBox2.Visible = false;
                label6.Visible = false;
                label11.Visible = false;
                label10.Visible = false;
                comboBox1.Enabled = true;
                dataGridView1.AllowUserToAddRows = true;
                button11.Enabled = false;
                //button11.Enabled = true;
                button1.Enabled = true;

            }
            else if (result == DialogResult.No)
            {

            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            foliocom = textBox1.Text;
            progressBar1.Value = 20;
            sucursalcom = sucursal;
            SucursalNomCom = comboBox1.Text;
            progressBar1.Value = 50;
            Carro2 = Carro.Text;
            Chofer2 = Chofer.Text;
            Placas2 = Placas.Text;
            progressBar1.Value = 100;

            using (Formato fp = new Formato())
            {
                fp.ShowDialog();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

            textBox3.Clear();
            dataGridView1.Rows.Clear();
            Variablescompartidas.SucursalRecibir = comboBox1.Text;
            Variablescompartidas.Recibir = "1";
            using (Recibidos rb = new Recibidos())
            {
                rb.ShowDialog();
            }
            Variablescompartidas.carga = "1";
            Variablescompartidas.Recibir = "0";
            llenadatos2();
            foliocom = textBox1.Text;
            comboBox1.Enabled = false;

        }

        private void llenadatos2()
        {
            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            cmd.CommandText = "select * from traspasos where folio = '" + Variablescompartidas.Folio + "'  and estatus != 'R'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            //try
            //{
            //Data is accessible through the DataReader object here.
            while (reader.Read())
            {
                dataGridView1.Rows.Add();
                // textBox1.Text = reader["ALGO"].ToString();
                textBox1.Text = reader["Folio"].ToString();
                Chofer.Text = reader["Chofer"].ToString();
                Carro.Text = reader["Carro"].ToString();
                Placas.Text = reader["Placas"].ToString();
                comboBox1.Text = reader["Desde"].ToString();
                comboBox3.Text = reader["Remolque"].ToString();
                if (reader["razon"].ToString() != "-")
                {
                    comboBox2.Visible = true;
                    textBox2.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;

                    comboBox2.Enabled = false;
                    textBox2.Enabled = false;
                    label10.Enabled = false;
                    label11.Enabled = false;

                    comboBox2.Text = reader["razon"].ToString();
                    textBox2.Text = reader["ObservacionesRazon"].ToString();
                }
                else
                {
                    comboBox2.Visible = false;
                    textBox2.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;

                    comboBox2.Text = "";
                    textBox2.Text = "";
                }
                dataGridView1.Rows[count].Cells[0].Value = reader["Codigo"].ToString();
                //DateTime dt = DateTime.Parse(reader["Fecha"].ToString());
                dataGridView1.Rows[count].Cells[1].Value = reader["Nombre"].ToString();
                dataGridView1.Rows[count].Cells[2].Value = reader["Destino"].ToString();
                dataGridView1.Rows[count].Cells[3].Value = reader["Cantidad"].ToString();
                dataGridView1.Rows[count].Cells[4].Value = reader["Peso"].ToString();
                //dataGridView1.Rows[count].Cells[5].Value = float.Parse(dataGridView2.Rows[count].Cells[3].Value.ToString()) - float.Parse(dataGridView2.Rows[count].Cells[4].Value.ToString());
                count++;

            }
            //}
            //    catch (Exception)
            //    {


            //    }
            sqlConnection2.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (Reporte1 rp = new Reporte1())
            {
                rp.ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox3.Enabled = true;
                sqlConnection3.Open();
                SqlCommand sc = new SqlCommand("select marca +' '+ cast(modelo as nvarchar) as remolque from vehiculos where tipo = 'Remolque' and estatus = 'Activo'", sqlConnection3);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("remolque", typeof(string));

                dt.Load(reader);

                comboBox3.ValueMember = "remolque";
                comboBox3.DisplayMember = "remolque";
                comboBox3.DataSource = dt;

                sqlConnection3.Close();

            }
            else
            {
                comboBox3.Enabled = false;
                comboBox3.Text = "";
            }
            }

        private void button5_Click(object sender, EventArgs e)
        {
            Variablescompartidas.Sucursal = comboBox1.Text;
            using (Recibidos rc = new Recibidos())
            {
                rc.ShowDialog();
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                suma();
            }
            catch (InvalidOperationException)
            {
                Variablescompartidas.Vuelta = "0";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Variablescompartidas.SucursalPedido = comboBox1.Text;
            dataGridView1.Rows.Clear();
            using (Pedido pd = new Pedido())
            {
                pd.ShowDialog();
            }
            cargarPedido();
            comboBox1.Text = "CEDIS";


        }

        private void cargarPedido()
        {
            //int count = 0; Este se agrega al inicio del programa
            count = 0;
            cmd.CommandText = "select folio, codigo, nombre, sucent, kilos, surgen, almtra, idalmtra from dbSurtido inner join folios on sucent = sucnom where folio = '" + Variablescompartidas.FolioPedido + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[count].Cells[0].Value = reader["codigo"].ToString();
                    dataGridView1.Rows[count].Cells[1].Value = reader["nombre"].ToString();
                    dataGridView1.Rows[count].Cells[2].Value = reader["almtra"].ToString();
                    dataGridView1.Rows[count].Cells[3].Value = reader["surgen"].ToString();
                    dataGridView1.Rows[count].Cells[4].Value = reader["kilos"].ToString();
                    Variablescompartidas.destinoNom = reader["almtra"].ToString();
                    Variablescompartidas.destino = reader["idalmtra"].ToString();
                    textBox1.Text = reader["folio"].ToString();

                    count++;

                }
                suma();
            }
            catch (Exception)
            {

            }
            sqlConnection2.Close();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void guardarContpaq()
        {
            //Tomar resultado Funci?n Manejo Errores
            string campo;
            int lResultado;
            //asignar la Ruta del Sistema
            string szRegKeySistema = @"SOFTWARE\\WOW6432Node\\Computación en Acción, SA CV\\CONTPAQ I COMERCIAL";
            //Establece la ruta donde se encuentar el archivo MGW_SDK.DLL
            RegistryKey keySistema = Registry.LocalMachine.OpenSubKey(szRegKeySistema);
            object lEntrada = keySistema.GetValue("DirectorioBase");
            SetCurrentDirectory(lEntrada.ToString());
            //Sistema : Adminpaq
            lResultado = fSetNombrePAQ("CONTPAQ I COMERCIAL");
            if (lResultado != 0)
            {
                rError(lResultado);
            }
            else
            {
                lResultado = fInicializaSDK();
                if (lResultado != 0)
                {
                    rError(lResultado);
                }
                //Se abre empresa
                fAbreEmpresa(@"C:\Compac\Empresas\adEmpresa_Capacitacion");
                if (lResultado != 0)
                {
                    rError(lResultado);
                }
                else
                {
                    //Inserta nuevo Documento
                    fInsertarDocumento();
                    if (lResultado != 0)
                    {
                        rError(lResultado);
                    }

                    fSetDatoDocumento("CIDDOCUMENTODE", "34");
                    fSetDatoDocumento("CIDCONCEPTODOCUMENTO", "3001");
                    fSetDatoDocumento("CSERIEDO01", letra);
                    fSetDatoDocumento("CFOLIO", textBox1.Text);
                    fSetDatoDocumento("CFECHA", "2019-07-02");
                    fSetDatoDocumento("CIDCLIEN01", "0");
                    fSetDatoDocumento("CRAZONSO01", "");
                    fSetDatoDocumento("CFECHAVE01", "2019-07-02");
                    fSetDatoDocumento("CFECHAPR01", "2019-07-02");
                    fSetDatoDocumento("CFECHAEN01", "2019-07-02");
                    fSetDatoDocumento("CFECHAUL01", "2019-07-02");
                    fSetDatoDocumento("CRFC", "");
                    fSetDatoDocumento("CIDMONEDA", "1");
                    fSetDatoDocumento("CTIPOCAM01", "1");
                    fSetDatoDocumento("CREFEREN01", "");
                    fSetDatoDocumento("COBSERVA01", "");
                    fSetDatoDocumento("CTEXTOEX02", "");
                    fSetDatoDocumento("CIMPORTE04", "");
                    fSetDatoDocumento("CBANOBSE01", "1");
                    fSetDatoDocumento("CNATURAL01", "2");
                    fSetDatoDocumento("CUSACLIE01", "0");
                    fSetDatoDocumento("CESTADOC01", "1");
                    fSetDatoDocumento("CTEXTOEX03", "");
                    lResultado = fGuardaDocumento();
                    if (lResultado != 0)
                    {
                        rError(lResultado);
                    }
                    else
                    {
                        campo = "CIDDOCUMENTO";
                        nDato(campo);

                        //lResultado = fInsertaDireccion();
                        //if (lResultado != 0)
                        //{
                        //    rError(lResultado);
                        //}
                        //fSetDatoDireccion("CIDCATAL01", valsel);
                        //fSetDatoDireccion("CTIPOCAT01", "3");
                        //fSetDatoDireccion("CTIPODIR01", "0");
                        //fSetDatoDireccion("CNOMBREC01", "-");
                        //fSetDatoDireccion("CNUMEROE01", "-");
                        //fSetDatoDireccion("CNUMEROI01", "-");
                        //fSetDatoDireccion("CCOLONIA", "-");
                        //fSetDatoDireccion("CCODIGOP01", "-");
                        //fSetDatoDireccion("CTELEFONO1", "-");
                        //fSetDatoDireccion("CTELEFONO2", "-");
                        //fSetDatoDireccion("CPAIS", "-");
                        //fSetDatoDireccion("CESTADO", "-");
                        //fSetDatoDireccion("CCIUDAD", "-");
                        //fSetDatoDireccion("CMUNICIPIO", "-");
                        //fSetDatoDireccion("CEMAIL", "-");
                        //lResultado = fGuardaDireccion();
                        //if (lResultado != 0)
                        //{
                        //    rError(lResultado);
                        //}
                        int count = 0;
                        try
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                lResultado = fInsertarMovimiento();
                                if (lResultado != 0)
                                {
                                    rError(lResultado);
                                }

                                fSetDatoMovimiento("CIDDOCUMENTO", valsel);
                                fSetDatoMovimiento("CIDDOCUMENTODE", "34");
                                fSetDatoMovimiento("CNUMEROMOVIMIENTO", "1");
                                fSetDatoMovimiento("cCodigoProducto", row.Cells["Codigo"].Value.ToString());
                                //fSetDatoMovimiento("cCodigoAlmacen", idalmacen);
                                fSetDatoMovimiento("CUNIDADES", row.Cells["Cantidad"].Value.ToString());
                                fSetDatoMovimiento("CUNIDADE02", row.Cells["Cantidad"].Value.ToString());
                                fSetDatoMovimiento("CPRECIO", "0");
                                fSetDatoMovimiento("CPRECIOC01", "0");
                                fSetDatoMovimiento("CIDUNIDAD", "1");
                                fSetDatoMovimiento("CAFECTAE01", "2");
                                fSetDatoMovimiento("CAFECTAD01", "1");
                                fSetDatoMovimiento("CAFECTAD02", "1");
                                fSetDatoMovimiento("CUNIDADE03", row.Cells["Cantidad"].Value.ToString());
                                fSetDatoMovimiento("CTIPOTRA01", "2");
                                fSetDatoMovimiento("CFECHA", "2019-07-02");
                                lResultado = fGuardaMovimiento();
                                if (lResultado != 0)
                                {
                                    rError(lResultado);
                                }
                                lResultado = fInsertarMovimiento();
                                if (lResultado != 0)
                                {
                                    rError(lResultado);
                                }
                                fSetDatoMovimiento("CIDDOCUMENTO", "0");
                                fSetDatoMovimiento("CNUMEROMOVIMIENTO", "2");
                                fSetDatoMovimiento("CIDDOCUMENTODE", "0");
                                fSetDatoMovimiento("cCodigoProducto", row.Cells["Codigo"].Value.ToString());
                                fSetDatoMovimiento("cCodigoAlmacen", row.Cells["Destino"].Value.ToString());
                                fSetDatoMovimiento("CUNIDADES", row.Cells["Cantidad"].Value.ToString());
                                fSetDatoMovimiento("CUNIDADE02", row.Cells["Cantidad"].Value.ToString());
                                fSetDatoMovimiento("CPRECIO", "0");
                                fSetDatoMovimiento("CPRECIOC01", "0");
                                fSetDatoMovimiento("CIDUNIDAD", "1");
                                fSetDatoMovimiento("CAFECTAE01", "3");
                                fSetDatoMovimiento("CAFECTAD01", "1");
                                fSetDatoMovimiento("CAFECTAD02", "0");
                                fSetDatoMovimiento("CUNIDADE03", row.Cells["Cantidad"].Value.ToString());
                                fSetDatoMovimiento("CTIPOTRA01", "2");
                                fSetDatoMovimiento("CFECHA", "2019-07-02");
                                lResultado = fGuardaMovimiento();
                                count += 1;
                            }
                        }
                        catch (NullReferenceException)
                        {


                        }
                        if (lResultado != 0)
                        {
                            rError(lResultado);
                        }
                        lResultado = fAfectaDocto_Param("358", "LP", 10, true);
                        if (lResultado != 0)
                        {
                            rError(lResultado);
                        }
                        string message = "Registro Guardado Satisfactoriamente";
                        string caption = "Aviso del Sistema";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;
                        result = MessageBox.Show(message, caption, buttons);
                    }
                }
                fCierraEmpresa();
                fTerminaSDK();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                button15.Visible = true;
                clientenom.Visible = true;
            }else
            {
                button15.Visible = false;
                clientenom.Visible = false;
                clientenom.Clear();
                Variablescompartidas.CalleCliente = "";
                Variablescompartidas.NumeroCliente = "";
                Variablescompartidas.Coloniacliente = "";
                Variablescompartidas.codigoCliente = "";
                Variablescompartidas.nombreCliente = "";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            using (Clientes cl = new Clientes())
            {
                cl.ShowDialog();
            }
            clientenom.Text = Variablescompartidas.nombreCliente;
            obtendatoscliente();


        }

        private void obtendatoscliente()
        {
            cmd.CommandText = @"select CCODIGOCLIENTE, CRAZONSOCIAL, CNOMBRECALLE, CNUMEROEXTERIOR, CCOLONIA  
            from admClientes inner join admDomicilios on admClientes.CIDCLIENTEPROVEEDOR = admDomicilios.CIDCATALOGO 
            where CCODIGOCLIENTE = '"+Variablescompartidas.codigoCliente+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                Variablescompartidas.CalleCliente = reader["CNOMBRECALLE"].ToString();
                Variablescompartidas.NumeroCliente = reader["CNUMEROEXTERIOR"].ToString();
                Variablescompartidas.Coloniacliente = reader["CCOLONIA"].ToString();

            }
            sqlConnection1.Close();
        }

        private void Displaynotify()
        {
            try
            {
                //notifyIcon1.Icon = new System.Drawing.Icon(Path.GetFullPath(@"image\graph.ico"));
                notifyIcon1.Text = "Export Datatable Utlity";
                notifyIcon1.Visible = true;
                notifyIcon1.BalloonTipTitle = "¡¡¡AVISO!!!";
                notifyIcon1.BalloonTipText = "HAY " + notifi + " TRASPASO(S) PENDIENTE(S) DE ESTA SUCURSAL";
                notifyIcon1.ShowBalloonTip(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void notificacion()
        {
            //Obtener valor o varios valores de la base de datos

            cmd.CommandText = "select COUNT (distinct folio) as cuenta from Traspasos where Estatus = 'T' and Desde = '" + comboBox1.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Close();
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                notifi = reader["cuenta"].ToString();

            }
            sqlConnection2.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            //DialogResult result = MessageBox.Show("Al Cambiar de Sucursal Se borrara todo lo capturado", "Limpiar", MessageBoxButtons.YesNoCancel);
            //Variablescompartidas.carga = "0";
            //progressBar1.Value = 0;
            //textBox3.Clear();
            //if (result == DialogResult.Yes)
            //{

            //}

            //Variablescompartidas.Vuelta = "0";
            //ClearTextBoxes();
            //dataGridView1.Rows.Clear();
            //dataGridView2.Rows.Clear();

            if (Variablescompartidas.carga != "1")
            {

                notificacion();
                if (notifi != "0")
                {
                    Displaynotify();
                }

                cmd.CommandText = "select sucursal, sucnom, idalmacen, idtrassal, letra from folios where sucnom = '" + comboBox1.Text + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection2;
                sqlConnection2.Close();
                sqlConnection2.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    idalmacen = reader["idalmacen"].ToString();
                    sucursal = reader["Sucursal"].ToString();
                    idtrapaso = reader["idtrassal"].ToString();
                    letra = reader["letra"].ToString();

                }
                sqlConnection2.Close();
                //cargafolio();
            }
            //else
            //{
            //    DialogResult result = MessageBox.Show("Se borrara todo lo capturado?", "Limpiar", MessageBoxButtons.YesNoCancel);
            //    Variablescompartidas.carga = "0";
            //    progressBar1.Value = 0;
            //    textBox3.Clear();
            //    if (result == DialogResult.Yes)
            //    {
            //        dataGridView1.Rows.Clear();
            //        dataGridView2.Rows.Clear();
            //        Variablescompartidas.Vuelta = "0";
            //        ClearTextBoxes();
            //        Chofer.Enabled = false;
            //        Carro.Enabled = false;
            //        Placas.Enabled = false;
            //        comboBox2.Text = "";
            //        comboBox2.Visible = false;
            //        textBox2.Clear();
            //        textBox2.Visible = false;
            //        label6.Visible = false;
            //        label11.Visible = false;

            //    }
            //    else if (result == DialogResult.No)
            //    {

            //    }


            //}
        }

        private void updatefolio()
        {
            string sql = "update foliostraspasos set FolioReq = @param1";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@param1", fol.ToString());
            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
        }

        private void cargafolio()
        {

            cmd.CommandText = "select FolioReq from foliostraspasos";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                fol = int.Parse(reader["FolioReq"].ToString()) + 1;
                textBox1.Text = fol.ToString() + "-T";

            }
            sqlConnection2.Close();

            //    cmd.CommandText = "select CNOFOLIO from admConceptos where CIDCONCEPTODOCUMENTO = '"+idtrapaso+"'";
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Connection = sqlConnection1;
            //    sqlConnection1.Close();
            //    sqlConnection1.Open();
            //    reader = cmd.ExecuteReader();

            //    Data is accessible through the DataReader object here.
            //    if (reader.Read())
            //    {
            //        int folio = Int32.Parse(reader["CNOFOLIO"].ToString());
            //        textBox1.Text = (folio + 1).ToString();

            //    }
            //    sqlConnection1.Close();
            //}
        }
    }
}