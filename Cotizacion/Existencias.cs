using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cotizacion
{
    public partial class Existencias : Form
    {
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.Aceros);

        public Existencias()
        {
            InitializeComponent();
            label2.Text = Variablescompartidas.CodigoPro;
            label3.Text = Variablescompartidas.Nombre;
            dataGridView1.Rows.Add("Matriz Socogos","0");
            dataGridView1.Rows.Add("Lopez Portillo","0");
            dataGridView1.Rows.Add("Salazar","0");
            dataGridView1.Rows.Add("San Pedro","0");
            dataGridView1.Rows.Add("Planta","0");
            dataGridView1.Rows.Add("Cedis", "0");


            try
            {
                int almacen = 1;
                int cont = 0;

                while (almacen < 9)
                {
                    if (almacen != 2 || almacen != 6)
                    {

                        sqlConnection2.Open();
                        SqlCommand cmd = new SqlCommand("conexiaBIEN", sqlConnection2);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@almacen", Convert.ToString(almacen));
                        cmd.Parameters.AddWithValue("@ejercicio", Principal.Variablescompartidas.Ejercicio);
                        cmd.Parameters.AddWithValue("@mes", Convert.ToString(DateTime.Now.Month.ToString()));
                        cmd.Parameters.AddWithValue("@codigo", Variablescompartidas.CodigoPro);  //Convert.ToString(Variablescompartidas.CodigoPro)
                                                                                                 //SqlDataReader dr = cmd.ExecuteReader();
                        DataTable ds = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        if (almacen == 1 || almacen == 3 || almacen == 4 || almacen == 5 || almacen == 7 || almacen ==8)
                        {
                            if (ds.Rows.Count > 0)
                            {

                                DataRow row = ds.Rows[0];
                                dataGridView1.Rows[cont].Cells[1].Value = Math.Round(Convert.ToDouble(row["existencia"]),2);
                                cont = cont + 1;
                                
                            }
                        }
                        sqlConnection2.Close();
                    }
                    almacen = almacen + 1;

                }

                Double Existencia = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Existencia += Math.Round(Convert.ToDouble(row.Cells["Column2"].Value), 2);

                }
                textBox1.Text = Existencia.ToString();
            }
            catch (Exception)
            {

                
            }
        }
    }
}