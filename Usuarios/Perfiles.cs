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

namespace Usuarios
{
    public partial class Perfiles : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand();
        public Perfiles()
        {
            InitializeComponent();
            //button4.BackColor = ColorTranslator.FromHtml("#8FA8CD");
            //button8.BackColor = ColorTranslator.FromHtml("#8FA8CD");
            button3.BackColor = ColorTranslator.FromHtml("#E47070");

            button6.BackColor = ColorTranslator.FromHtml("#C2C251");
            button7.BackColor = ColorTranslator.FromHtml("#E47070");
            button2.BackColor = ColorTranslator.FromHtml("#64C251");
            button5.BackColor = ColorTranslator.FromHtml("#77A5DC");
            sig();
        }

        private void sig()
        {
            cmd.CommandText = "select top 1 (num) from PerfilesUsers order by num desc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            int sig = 0;
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                sig = Int32.Parse(reader["num"].ToString()) + 1;
                label7.Text = sig.ToString();

            }
            sqlConnection1.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           

            if (textBox1.Text == "")
            {
                MessageBox.Show("Asegurate De No Dejar El Nombre Del Perfil En Blanco", "AVISO");
                textBox1.Focus();
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa))
                {
                    string query = "select count(*) from perfilesusers where Perfil  =@Perfil";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Perfil", textBox1.Text);
                    conn.Open();

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 0)
                    {
                        guardar();
                        sig();
                        button2.Visible = true;
                        button5.Visible = false;
                        button7.Visible = false;

                        textBox1.Clear();
                        reinicia();
                    }

                    else
                    {
                        MessageBox.Show("Ya Existe Un Perfil Con Este Nombre", "AVISO");
                    }

                }
            }
        }

        private void guardar()
        {
            try
            {
                string sql = "insert into perfilesusers values (@param1,@param2)";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@param1", label7.Text); 
                cmd.Parameters.AddWithValue("@param2", textBox1.Text);
                
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();

                int i;
                string s;
                s = "Checked items:\n";
                for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        string sql2 = "insert into permisoPerfil values (@param1, @param2)";
                        SqlCommand cmd2 = new SqlCommand(sql2, sqlConnection1);
                        cmd2.Parameters.AddWithValue("@param1", label7.Text); //Para grabar algo de un textbox
                        cmd2.Parameters.AddWithValue("@param2", checkedListBox1.Items[i].ToString()); //Para grabar una columna

                        sqlConnection1.Open();
                        cmd2.ExecuteNonQuery();
                        sqlConnection1.Close();
                        //s = checkedListBox1.Items[i].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                MessageBox.Show(err.ToString(), "Error con Base de Datos");
            }
            catch (InvalidOperationException)
            {
                sqlConnection1.Close();
                guardar();
            }
            MessageBox.Show("Guardado");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variablescompartidas.numPerfil = "cancelado";
            //this.Close();
            using (CargaPerfiles cp = new CargaPerfiles())
            {
                cp.ShowDialog();
            }
            if (Variablescompartidas.numPerfil == "cancelado")
            {
                //sig();
                //button2.Visible = true;
                //button5.Visible = false;
                sig();
                button2.Visible = true;
                button5.Visible = false;
                button7.Visible = false;

                textBox1.Clear();
                reinicia();
            }
            else
            {
                label7.Text = Variablescompartidas.numPerfil;
                reinicia();
                cargarUsuario();
                carga();
                button2.Visible = false;
                button5.Visible = true;
                button7.Visible = true;
            }
        }

        private void reinicia()
        {
            //ClearTextBoxes();

            int i;
            string s;
            s = "Checked items:\n";
            for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void cargarUsuario()
        {
            cmd.CommandText = "Select Perfil from PerfilesUsers where num = '" + label7.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            //try
            //{
            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                textBox1.Text = reader["Perfil"].ToString();
                
            }


            //}

            //catch (Exception)
            //{


            //}
            sqlConnection1.Close();
        }

        private void carga()
        {
            //checkedListBox1.SetItemChecked(10, true);
            cmd.CommandText = "select Permiso from permisoPerfil where num = '"+label7.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int i;
                string s;
                s = "Checked items:\n";
                for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                {
                    
                    if (reader["Permiso"].ToString() == checkedListBox1.Items[i].ToString())
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            sig();
            button2.Visible = true;
            button5.Visible = false;
            button7.Visible = false;

            textBox1.Clear();
            reinicia();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Nombre De Perfil Vacio", "AVISO");
                textBox1.Focus();
            }
            else
            {
                actualizaPerfil();
                deletepermisos();
                insertapermiso();
                MessageBox.Show("Actualizado");
            }
            
        }

        private void actualizaPerfil()
        {
            try
            {
                string sql2 = "Update PerfilesUsers set Perfil = @param1 where num = @param5";
                SqlCommand cmd2 = new SqlCommand(sql2, sqlConnection1);
                cmd2.Parameters.AddWithValue("@param1", textBox1.Text); 
                cmd2.Parameters.AddWithValue("@param5", label7.Text);
                
                sqlConnection1.Close();
                sqlConnection1.Open();
                cmd2.ExecuteNonQuery();
                sqlConnection1.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void deletepermisos()
        {
            cmd.CommandText = "delete from permisoPerfil where num = '" + label7.Text + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Close();
            sqlConnection1.Open();
            cmd.ExecuteNonQuery();
            sqlConnection1.Close();

        }

        private void insertapermiso()
        {
            int i;
            string s;
            s = "Checked items:\n";
            for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string sql2 = "insert into permisoPerfil values (@param1, @param2)";
                    SqlCommand cmd2 = new SqlCommand(sql2, sqlConnection1);
                    cmd2.Parameters.AddWithValue("@param1", label7.Text); //Para grabar algo de un textbox
                    cmd2.Parameters.AddWithValue("@param2", checkedListBox1.Items[i].ToString()); //Para grabar una columna


                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();
                    sqlConnection1.Close();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estas Seguro Que Deseas Eliminar A Este Perfil?", "ELIMINAR", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                deletePermiso(label7.Text);
                deleteuser(label7.Text);
            }
            else if (result == DialogResult.No)
            {
            }
        }

        private void deletePermiso(string id)
        {
            try
            {
                cmd.CommandText = "delete from permisoPerfil where num = '" + id + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Close();
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                MessageBox.Show(err.ToString(), "Error con Base de Datos");
            }
        }

        private void deleteuser(string id)
        {
            try
            {
                cmd.CommandText = "delete from PerfilesUsers where num = '" + id + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;
                sqlConnection1.Close();
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("Eliminado");

                sig();
                button2.Visible = true;
                button5.Visible = false;
                button7.Visible = false;

                textBox1.Clear();
                reinicia();
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                MessageBox.Show(err.ToString(), "Error con Base de Datos");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}