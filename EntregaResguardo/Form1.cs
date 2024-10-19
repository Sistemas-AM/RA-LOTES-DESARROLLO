using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntregaResguardo
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        SqlDataReader reader2;
        public Form1()
        {
            InitializeComponent();
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("Select sucnom,idfmr from folios", sqlConnection1);

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucnom", typeof(string));
            dt.Columns.Add("idfmr", typeof(string));

            dt.Load(reader);

            cmbsuc.ValueMember = "sucnom";
            cmbsuc.DisplayMember = "sucnom";
            cmbsuc.DataSource = dt;

            sqlConnection1.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbsuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargafolio();
            txtfact.Focus();
        }
        private void cargafolio()
        {
            sqlConnection1.Close();
            cmd.CommandText = "select idfmr from folios where sucnom = '" + cmbsuc.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                int folio = Int32.Parse(reader["idfmr"].ToString());
                txtfolio.Text = (folio + 1).ToString();

            }
            sqlConnection1.Close();
        }

        private void txtfact_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtfact_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtfact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                string ser = "LP";
                
                switch (cmbsuc.Text)
                {
                    case "LOPEZ PORTILLO":
                        ser = "LP";
                        break;
                    case "IGNACIO SALAZAR":
                        ser = "IS";
                        break;
                    case "MATRIZ SOCOGOS":
                        ser = "MA";
                        break;
                    case "SAN PEDRO COMERCIAL":
                        ser = "SP";
                        break;
                }
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("spFolio", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fol", txtfact.Text);
                cmd.Parameters.AddWithValue("@serie", ser);
                reader = cmd.ExecuteReader();
                int cont = 0;
                try
                {
                    // Data is accessible through the DataReader object here.
                    while (reader.Read())
                    {
                        txtcte.Text = reader["crazonsocial"].ToString();
                        dtfact.Text = reader["cfecha"].ToString();
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[cont].Cells[0].Value = reader["ccodigoproducto"].ToString();
                        dataGridView1.Rows[cont].Cells[1].Value = reader["cnombreproducto"].ToString();
                        dataGridView1.Rows[cont].Cells[2].Value = reader["cunidades"].ToString();
                       
                        cont++;
                    }
                    
                    int c = 0;
                    float res = 0;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                    sqlConnection1.Open();
                    SqlCommand cmd2 = new SqlCommand("spConMat", sqlConnection1);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@fac", txtfact.Text);
                        cmd2.Parameters.AddWithValue("@ser", "LP");
                        cmd2.Parameters.AddWithValue("@cod", dataGridView1.Rows[c].Cells[0].Value.ToString());

                        reader2 = cmd2.ExecuteReader();
                        if (reader2.Read())
                        {
                            dataGridView1.Rows[c].Cells[3].Value = reader2["totent"].ToString();
                            res = float.Parse(dataGridView1.Rows[c].Cells[2].Value.ToString()) - float.Parse(dataGridView1.Rows[c].Cells[3].Value.ToString());
                            dataGridView1.Rows[c].Cells[4].Value = res.ToString();
                        }
                        else
                        {
                            dataGridView1.Rows[c].Cells[4].Value = dataGridView1.Rows[c].Cells[2].Value.ToString();
                        }
                        
                        c++;
                    sqlConnection1.Close();
                }

                }
                catch (Exception)
                {


                }
                sqlConnection.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fec = DateTime.Parse(dtent.Text);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    SqlCommand cmd2 = new SqlCommand("graba_mr", sqlConnection1);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("@fol", txtfolio.Text);
                    cmd2.Parameters.AddWithValue("@ser", "LP");
                    cmd2.Parameters.AddWithValue("@fac", txtfact.Text);
                    cmd2.Parameters.AddWithValue("@fec", fec.ToString("MM/dd/yyyy"));
                    cmd2.Parameters.AddWithValue("@cod", row.Cells["dgvcod"].Value.ToString());
                    cmd2.Parameters.AddWithValue("@cfac", Convert.ToInt32(row.Cells["dgvcan"].Value.ToString()));
                    cmd2.Parameters.AddWithValue("@cent", Convert.ToInt32(row.Cells["dgvsal"].Value.ToString()));
                    
                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();
                    sqlConnection1.Close();
                }
             }
            catch (NullReferenceException)
            {


            }
            MessageBox.Show("Registro Guardado Satisfactoriamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button3.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            varglob.factura = txtfact.Text;
            varglob.cliente = txtcte.Text;
            varglob.fecfac = dtfact.Text;
            varglob.suc = cmbsuc.Text;
            using (repentma imp = new repentma(dataGridView1))
            {
                imp.ShowDialog();
            }
        }
    }
}
