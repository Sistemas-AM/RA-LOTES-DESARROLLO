using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Traspasos
{
    public partial class Reporte1 : Form
    {

        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        //SqlDataReader reader;
        public Reporte1()
        {
            InitializeComponent();
            
        }

        private void cargaCombo()
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("select sucursal, sucnom, idalmacen from folios where idtrassal != 0", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucnom", typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "idalmacen";
            comboBox1.DisplayMember = "sucnom";
            comboBox1.DataSource = dt;
            sqlConnection1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Variablescompartidas.GeneralReporte = "select *, ROW_NUMBER() OVER(ORDER BY Folio ASC) AS Row#  from Traspasos where Desde = '"+comboBox1.Text+"' and fecha between '"+dateTimePicker1.Value.ToString("MM/dd/yyyy")+"' and '"+ dateTimePicker2.Value.ToString("MM/dd/yyyy") + "'";
                Variablescompartidas.SucursalReporte = comboBox1.Text;
                Variablescompartidas.Fecha1 = dateTimePicker1.Value.ToString("MM/dd/yyyy");
                Variablescompartidas.Fecha2 = dateTimePicker2.Value.ToString("MM/dd/yyyy");
            }
            else if (radioButton2.Checked)
            {
                Variablescompartidas.SucursalReporte = "GENERAL";
                Variablescompartidas.GeneralReporte = "select *, ROW_NUMBER() OVER(ORDER BY Folio ASC) AS Row#  from Traspasos where fecha between '" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' and '" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "'";
                Variablescompartidas.Fecha1 = dateTimePicker1.Value.ToString("MM/dd/yyyy");
                Variablescompartidas.Fecha2 = dateTimePicker2.Value.ToString("MM/dd/yyyy");
            }
            using (Reporte1Pdf pd = new Reporte1Pdf())
            {
                pd.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            cargaCombo();
            comboBox1.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
        }
    }
}
