using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Data.SqlClient;


namespace GruaHiab
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();
        int fol = 0;

        public static string folipasa { get; set; }

        public Form1()
        {
            InitializeComponent();

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );

            btnEliminar.BackColor = ColorTranslator.FromHtml("#D66F6F");
            cargarSucursal();

            //if (Principal.Variablescompartidas.sucural == "AUDITOR")
            //{
            //    metroComboBox1.Enabled = true;
            //}
            //else
            //{
            //    metroComboBox1.Enabled = false;
            //    metroComboBox1.Text = Principal.Variablescompartidas.sucural;
            //}

        }

        private void cargarSucursal()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select sucnom, idalmacen from folios", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("letra", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "idalmacen";
            metroComboBox1.DisplayMember = "sucnom";
            metroComboBox1.DataSource = dt;

            sqlConnection2.Close();
        }

        private void cargar(string sucursal)
        {
            try
            {
                sqlConnection1.Open();
                SqlCommand cmd = new SqlCommand("GruaH", sqlConnection1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy")));
                cmd.Parameters.AddWithValue("@v1", Convert.ToString("0"));
                cmd.Parameters.AddWithValue("@v2", Convert.ToString("100"));
                cmd.Parameters.AddWithValue("@almacen", sucursal);
                cmd.Parameters.AddWithValue("@Ejercicio", Principal.Variablescompartidas.Ejercicio);
                //cmd.Parameters.AddWithValue("@fechfin", Convert.ToString(VariablesCompartidas.fecfin));
                ////SqlDataReader dr = cmd.ExecuteReader();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                dataGridView1.DataSource = dt;

                sqlConnection1.Close();
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }

        private void calcula()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                double pedir  = 0;
                double bulto = 0;
                double sumar = 0;
                double diferencia = 0;
                int numBulto = 0;

                pedir = Math.Round(Double.Parse(row.Cells["Cantidad"].Value.ToString()) / Double.Parse(row.Cells["Promedio"].Value.ToString()),2);

                if (pedir <= 0.49)
                {
                    row.Cells["Pedir"].Value = "0";
                    row.Cells["CantidadBul"].Value = "0";
                    row.Cells["TotalKilos"].Value = "0";
                }
                else if(pedir >= 0.50)
                {
                    bulto = Double.Parse(row.Cells["Promedio"].Value.ToString());
                    sumar = Double.Parse(row.Cells["Existencia"].Value.ToString()) + bulto;
                    diferencia = Double.Parse(row.Cells["CapacidadSur"].Value.ToString()) - sumar;
                    pedir = diferencia / Double.Parse(row.Cells["Promedio"].Value.ToString());
                    numBulto += 1;

                    while (pedir >= 0.50)
                    {
                        bulto = bulto + Double.Parse(row.Cells["Promedio"].Value.ToString());
                        //sumar = Double.Parse(row.Cells["Existencia"].Value.ToString()) + bulto;
                        //pedir = sumar / Double.Parse(row.Cells["Promedio"].Value.ToString());

                        //bulto = Double.Parse(row.Cells["Promedio"].Value.ToString());
                        sumar = Double.Parse(row.Cells["Existencia"].Value.ToString()) + bulto;
                        diferencia = Double.Parse(row.Cells["CapacidadSur"].Value.ToString()) - sumar;
                        pedir = diferencia / Double.Parse(row.Cells["Promedio"].Value.ToString());
                        numBulto += 1;
                    }
                    row.Cells["Pedir"].Value = bulto;
                    row.Cells["CantidadBul"].Value = numBulto.ToString() ;
                    row.Cells["TotalKilos"].Value = bulto * Double.Parse(row.Cells["Kilos"].Value.ToString());

                }
                
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            cargar(metroComboBox1.SelectedValue.ToString());
            calcula();
            suma();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            sacafolio();
            updatefolio();
            guardar();
            folipasa = Folio.Text;
            using (Impresion im = new Impresion())
            {
                im.ShowDialog();
            }
        }

        private void sacafolio()
        {
            cmd.CommandText = "select folioreq from foliostraspasos";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                fol = int.Parse(reader["folioreq"].ToString()) + 1;
                Folio.Text = fol.ToString() + "-R";

            }
            sqlConnection2.Close();

        }

        private void updatefolio()
        {
            string sql = "update foliostraspasos set folioreq = @param1";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection2);
            cmd.Parameters.AddWithValue("@param1", fol.ToString());
            sqlConnection2.Open();
            cmd.ExecuteNonQuery();
            sqlConnection2.Close();
        }

        private void guardar()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Pedir"].Value.ToString() != "0")
                    {
                        string sql = "insert into dbSurtido (folio, fecha, codigo, nombre, sucent, porde,porhas, exiced, kilos, maxgen, exigen, surgen, cap, Estatus) values (@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13, @Estatus)";
                        SqlCommand cmd2 = new SqlCommand(sql, sqlConnection2);
                        cmd2.Parameters.AddWithValue("@param1", Folio.Text); //folio
                        cmd2.Parameters.AddWithValue("@param2", (DateTime.Now.ToString("MM/dd/yyyy"))); //fecha
                        cmd2.Parameters.AddWithValue("@param3", row.Cells["Codigo"].Value.ToString()); //Codigo
                        cmd2.Parameters.AddWithValue("@param4", row.Cells["Nombre"].Value.ToString()); //Nombre
                        cmd2.Parameters.AddWithValue("@param5", metroComboBox1.Text); //sucent sucursal entrada
                        cmd2.Parameters.AddWithValue("@param6", "0"); //porde 
                        cmd2.Parameters.AddWithValue("@param7", "100"); //porhas
                        cmd2.Parameters.AddWithValue("@param8", row.Cells["ExistenciaCedis"].Value.ToString()); //exiced existencia cedis
                        cmd2.Parameters.AddWithValue("@param9", row.Cells["TotalKilos"].Value.ToString()); //kilos
                        cmd2.Parameters.AddWithValue("@param10", row.Cells["CantidadBul"].Value.ToString()); //maxgen maximo venta
                        cmd2.Parameters.AddWithValue("@param11", row.Cells["Existencia"].Value.ToString()); //exigen existencia sucursal
                        cmd2.Parameters.AddWithValue("@param12", row.Cells["Pedir"].Value.ToString()); //surgen cantidad
                        cmd2.Parameters.AddWithValue("@param13", "0"); //cap desabasto
                        cmd2.Parameters.AddWithValue("@Estatus", "0"); //cap desabasto

                        //cmd.Connection = sqlConnection1;
                        sqlConnection2.Open();
                        cmd2.ExecuteNonQuery();
                        //label25.Text = (row.Cells["Column1"].Value.ToString());
                        sqlConnection2.Close();

                    }
                }
                MessageBox.Show("Guardado");
            }
            catch (Exception)
            {
                MessageBox.Show("Error al guardar");
                sqlConnection2.Close();

            }
        }

        private void suma()
        {
            try
            {
                Double Piezas = 0;
                Double Bultos = 0;
                Double Kilos = 0;
                double codigo = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToDouble(row.Cells["Pedir"].Value) >0)
                    {
                        codigo += 1;
                    }
                    Piezas += Math.Round(Convert.ToDouble(row.Cells["Pedir"].Value), 2);
                    Bultos += Math.Round(Convert.ToDouble(row.Cells["CantidadBul"].Value), 2);
                    Kilos += Math.Round(Convert.ToDouble(row.Cells["TotalKilos"].Value), 2);
                }
                PiezasText.Text = Piezas.ToString();
                BultosText.Text = Bultos.ToString();
                KilosText.Text = Kilos.ToString();
                Total1.Text = codigo.ToString();

                
            }
            catch (Exception)
            {


            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            cargar(metroComboBox1.SelectedValue.ToString());
            calcula();
            suma();

            AdjustColumnOrder();
        }

        private void AdjustColumnOrder()
        {
           
            dataGridView1.Columns["Codigo"].DisplayIndex = 0;
            dataGridView1.Columns["Nombre"].DisplayIndex = 1;
            dataGridView1.Columns["Clasificacion"].DisplayIndex = 2;
            dataGridView1.Columns["CapacidadEsp"].DisplayIndex = 3;
            dataGridView1.Columns["CapacidadSur"].DisplayIndex = 4;
            dataGridView1.Columns["Existencia"].DisplayIndex = 5;
            dataGridView1.Columns["ExistenciaCedis"].DisplayIndex = 6;
            dataGridView1.Columns["ExistenciaCedMat"].DisplayIndex = 7;

            dataGridView1.Columns["Cantidad"].DisplayIndex = 8;
            dataGridView1.Columns["Pedir"].DisplayIndex = 9;
            dataGridView1.Columns["CantidadBul"].DisplayIndex = 10;
            dataGridView1.Columns["TotalKilos"].DisplayIndex = 11;
            
            //dataGridView1.Columns["CompanyName"].DisplayIndex = 4;
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["TotalKilos"].Value = Math.Round(Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Pedir"].Value.ToString()) * Convert.ToDouble(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Kilos"].Value.ToString()), 2);
                suma();
            }
            catch (FormatException)
            {
                MessageBox.Show("No se ingreso un formato correcto", "ERROR");
            }
            catch (Exception)
            {

            }
        }
    }
}
