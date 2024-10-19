using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dismatsuc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (var con1 = new SqlConnection(Principal.Variablescompartidas.Aceros))
            {
                con1.Open();
                SqlCommand cmd = new SqlCommand("exiced", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ejercicio", 7);
                cmd.Parameters.AddWithValue("@mes", 7);
                DataSet dinero = new DataSet();
                SqlDataAdapter tradin = new SqlDataAdapter(cmd);
                tradin.Fill(dinero);
                dgvproductos.DataSource = dinero.Tables[0];
                DataGridViewColumn exisuc = new DataGridViewColumn();
                exisuc.CellTemplate = dgvproductos.Columns[0].CellTemplate;
                exisuc.AutoSizeMode = dgvproductos.Columns[0].AutoSizeMode;
                
            
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var con1 = new SqlConnection("Data Source=AMSASERVER\\COMPAC;Initial Catalog=adACEROS_MEXICO;Persist Security Info=True;User ID=sa;Password=AdminSql7639!"))
            {
                con1.Open();

                SqlCommand cmd = new SqlCommand("conexi", con1);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmbsucursal.Text == "MATRIZ")
                {
                    cmd.Parameters.AddWithValue("@almacen", 1);
                }
                if (cmbsucursal.Text == "PORTILLO")
                {
                    cmd.Parameters.AddWithValue("@almacen", 3);
                }
                if (cmbsucursal.Text == "SALAZAR")
                {
                    cmd.Parameters.AddWithValue("@almacen", 4);
                }
                if (cmbsucursal.Text == "SAN PEDRO")
                {
                    cmd.Parameters.AddWithValue("@almacen", 5);
                }
                cmd.Parameters.AddWithValue("@ejercicio", 7);
                cmd.Parameters.AddWithValue("@mes", 7);
                DataSet dinero2 = new DataSet();
                SqlDataAdapter tradin = new SqlDataAdapter(cmd);
                tradin.Fill(dinero2);
                dgvproductos.DataSource = dinero2.Tables[0];
            }
        }
    }
}
