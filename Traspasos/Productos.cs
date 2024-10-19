using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Traspasos
{
    public partial class Productos : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection sqlConnection2 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        public Productos()
        {
            InitializeComponent();
            cargaCombo();
            textBox1.Text = Variablescompartidas.ProductoPaso;
            if (Variablescompartidas.Vuelta == "1")
            {
                Destino.Enabled = false;
            }
            Destino.Text = Variablescompartidas.destinoNom;
        }

        private void Productos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'adAMSACONTPAQiDataSet.admProductos' Puede moverla o quitarla según sea necesario.
            this.admProductosTableAdapter.Fill(this.adAMSACONTPAQiDataSet.admProductos);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var Binding = (BindingSource)dataGridView1.DataSource;
            Binding.Filter = string.Format("CCODIGOPRODUCTO like '%{0}%' or CNOMBREPRODUCTO like '%{0}%'", textBox1.Text.Trim().Replace("'", "''"));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            exis.Text = "0";
            Codigo.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cCODIGOPRODUCTODataGridViewTextBoxColumn"].Value.ToString();
            Nombre.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["cNOMBREPRODUCTODataGridViewTextBoxColumn"].Value.ToString();
            Medida.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["peso"].Value.ToString();
            existencia(Variablescompartidas.almExistencia, Codigo.Text);
        }

        private void existencia(string alm, string codigoo)
        {
             cmd.CommandText = @"select CCODIGOPRODUCTO, 
	case when DATEPART(m,getdate()) = 1  then centradasperiodo1-csalidasperiodo1
         when DATEPART(m,getdate()) = 2  then centradasperiodo2-csalidasperiodo2
         when DATEPART(m,getdate()) = 3  then centradasperiodo3-csalidasperiodo3
         when DATEPART(m,getdate()) = 4  then centradasperiodo4-csalidasperiodo4
         when DATEPART(m,getdate()) = 5  then centradasperiodo5-csalidasperiodo5
         when DATEPART(m,getdate()) = 6  then centradasperiodo6-csalidasperiodo6
         when DATEPART(m,getdate()) = 7  then centradasperiodo7-csalidasperiodo7
         when DATEPART(m,getdate()) = 8  then centradasperiodo8-csalidasperiodo8
         when DATEPART(m,getdate()) = 9  then centradasperiodo9-csalidasperiodo9
         when DATEPART(m,getdate()) = 10 then centradasperiodo10-csalidasperiodo10
         when DATEPART(m,getdate()) = 11 then centradasperiodo11-csalidasperiodo11
         when DATEPART(m,getdate()) = 12 then centradasperiodo12-csalidasperiodo12

    end as Existencia  from admExistenciaCosto
    inner join admProductos on admProductos.CIDPRODUCTO = admExistenciaCosto.CIDPRODUCTO

    where CIDEJERCICIO = 2 and CIDALMACEN = '" + alm+"' and CCODIGOPRODUCTO = '"+codigoo+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                exis.Text = reader["Existencia"].ToString();

            }
            sqlConnection1.Close();
        }


        private void cargaCombo()
        {
            sqlConnection2.Open();
            SqlCommand sc = new SqlCommand("select sucursal, almtra, idalmtra from folios where idtrassal != 0", sqlConnection2);
            //select customerid,contactname from customer
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucnom", typeof(string));
            dt.Load(reader);
            Destino.ValueMember = "idalmtra";
            Destino.DisplayMember = "almtra";
            Destino.DataSource = dt;
            sqlConnection2.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Codigo.Text != "")
            {
                if (float.Parse(Cantidad.Text) > float.Parse(exis.Text) || float.Parse(Cantidad.Text) <= 0)
                {
                    MessageBox.Show("No hay suficiente existencia");
                }
                else
                {
                try
                {
                    Variablescompartidas.codigo = Codigo.Text;
                    Variablescompartidas.nombre = Nombre.Text;
                    Variablescompartidas.cantidad = Cantidad.Text;
                    Variablescompartidas.destino = Destino.SelectedValue.ToString();
                    Variablescompartidas.medida = Medida.Text;
                    Variablescompartidas.cancelado = "0";
                    Variablescompartidas.destinoNom = Destino.Text;
                    Variablescompartidas.Vuelta = "1";

                    this.Close();
                }
                catch (NullReferenceException)
                {

                    MessageBox.Show("Asegurate de seleccionar un destino");
                }
                }
                
            }else
            {
                MessageBox.Show("Asegurate de seleccionar un producto");
            }
        }

        private void Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Variablescompartidas.cancelado = "1";
            this.Close();
        }
    }
}