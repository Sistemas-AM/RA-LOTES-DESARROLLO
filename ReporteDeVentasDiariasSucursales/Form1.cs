using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.SqlClient;

namespace ReporteDeVentasDiariasSucursales
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public static string notas { get; set; }
        public static string credito { get; set; }
        public static string contado { get;set;}
        public static string fecini { get; set; }
        public static string fecfin { get; set; }
        public static string Sucursal { get; set; }
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
            cargaSucursales();
            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                metroComboBox1.Enabled = true;
                
            }
            else
            {
                metroComboBox1.Enabled = false;
                metroComboBox1.Text = Principal.Variablescompartidas.sucural;
                
            }


        }

        private void cargaSucursales()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select sucnom, idalmacen from folios", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucursal", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "idalmacen";
            metroComboBox1.DisplayMember = "sucnom";
            metroComboBox1.DataSource = dt;

            sqlConnection2.Close();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd.CommandText = "select sucursal, idalmacen, sucnom, idnotas, idcontado, idcredito from folios where idalmacen = '"+metroComboBox1.SelectedValue.ToString()+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection2;
            sqlConnection2.Close();
            sqlConnection2.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                notas = reader["idnotas"].ToString();
                credito = reader["idcredito"].ToString();
                contado = reader["idcontado"].ToString();

            }
            sqlConnection2.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            DateTime fecha1 = DateTime.Parse(metroDateTime1.Text);
            DateTime fecha2 = DateTime.Parse(metroDateTime2.Text);

            fecini = fecha1.ToString("MM/dd/yyyy");
            fecfin = fecha2.ToString("MM/dd/yyyy");
            Sucursal = metroComboBox1.Text;
            using (Form2 rep = new Form2())
            {
                rep.ShowDialog();
            }
        }
    }
}
