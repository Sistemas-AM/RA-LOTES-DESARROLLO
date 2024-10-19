using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Usuarios
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand();


        public Form1()
        {

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
                    );

            InitializeComponent();
            button4.BackColor = ColorTranslator.FromHtml("#8FA8CD");
            button8.BackColor = ColorTranslator.FromHtml("#8FA8CD");
            button3.BackColor = ColorTranslator.FromHtml("#E47070");

            button6.BackColor = ColorTranslator.FromHtml("#C2C251");
            button7.BackColor = ColorTranslator.FromHtml("#E47070");
            button2.BackColor = ColorTranslator.FromHtml("#64C251");
            button5.BackColor = ColorTranslator.FromHtml("#77A5DC");

            sig();
            //cargaCombo();
            cargaCombo2();
        }
        private void cargaCombo()
        {
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("select top 6 sucursal, sucnom, idalmacen from folios where idtrassal != 0", sqlConnection1);
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
        
        private void cargaCombo2()
        {
            sqlConnection1.Close();
            sqlConnection1.Open();
            SqlCommand sc = new SqlCommand("select num, perfil from PerfilesUsers order by num", sqlConnection1);
            //select customerid,contactname from customer
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("perfil", typeof(string));
            dt.Load(reader);
            comboBox2.ValueMember = "num";
            comboBox2.DisplayMember = "perfil";
            comboBox2.DataSource = dt;
            sqlConnection1.Close();
        }
        private void sig()
        {
            cmd.CommandText = "select top 1 (num) from usuarios order by num desc";
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

        private void existe()
        {
            string query = "SELECT COUNT(*) FROM Usuarios WHERE usuario=@Id";
            SqlCommand cmd = new SqlCommand(query, sqlConnection1);
            cmd.Parameters.AddWithValue("Id", textBox2.Text);
            sqlConnection1.Close();
            sqlConnection1.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count == 0)
            {
                textBox2.BackColor = Color.PaleGreen;
                label6.ForeColor = Color.Green;
                label6.Text = "* Nombre de usuario disponible";
            }

            else
            {
                textBox2.BackColor = Color.LightCoral;
                label6.ForeColor = Color.Red;
                label6.Text = "* Nombre de usuario no disponible";
                textBox2.Focus();

            }
            sqlConnection1.Close();

        }

        private void guardar()
        {
            try
            {
                string sql = "insert into Usuarios values (@param1, @param2, @param3, @param4,@param5, @param6, @param7)";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@param1", label7.Text); //Para grabar algo de un textbox
                cmd.Parameters.AddWithValue("@param2", textBox1.Text); //Para grabar una columna
                cmd.Parameters.AddWithValue("@param3", textBox2.Text);
                cmd.Parameters.AddWithValue("@param4", textBox3.Text);

                //string sexo = "";
                //if (radioButton1.Checked)
                //{
                //    sexo = "H";
                //}
                //else if (radioButton2.Checked)
                //{
                //    sexo = "M";
                //}
                //cmd.Parameters.AddWithValue("@param5", sexo);

                cmd.Parameters.AddWithValue("@param5", comboBox1.Text);
                cmd.Parameters.AddWithValue("@param6", comboBox2.Text);
                cmd.Parameters.AddWithValue("@param7", comboBox2.SelectedValue.ToString());
                sqlConnection1.Close();
                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                

                //int i;
                //string s;
                //s = "Checked items:\n";
                //for (i = 0; i <= (checkedListBox1.Items.Count - 1); i++)
                //{
                //    if (checkedListBox1.GetItemChecked(i))
                //    {
                //        string sql2 = "insert into PermisosUsuarios values (@param1, @param2)";
                //        SqlCommand cmd2 = new SqlCommand(sql2, sqlConnection1);
                //        cmd2.Parameters.AddWithValue("@param1", label7.Text); //Para grabar algo de un textbox
                //        cmd2.Parameters.AddWithValue("@param2", checkedListBox1.Items[i].ToString()); //Para grabar una columna

                //        sqlConnection1.Open();
                //        cmd2.ExecuteNonQuery();
                //        sqlConnection1.Close();
                //        //s = checkedListBox1.Items[i].ToString();
                //    }
                //}
            }
            catch (Exception)
            {

                MessageBox.Show("Ocurrio un error al guardar");
            }
            MessageBox.Show("Guardado");

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.BackColor = Color.White;
                label6.Text = "";
            }
            else
            {
                existe();
            }
        }

        private void actualizauser()
        {
            try
            {
                string sql2 = "Update Usuarios set nombre = @param1, Usuario = @param2, Pass = @param3, sucursal = @param6, perfil = @Perfil, PerfilId = @PerfilID where num = @param5";
                SqlCommand cmd2 = new SqlCommand(sql2, sqlConnection1);
                cmd2.Parameters.AddWithValue("@param1", textBox1.Text); //Para grabar algo de un textbox
                cmd2.Parameters.AddWithValue("@param2", textBox2.Text); //Para grabar una columna
                cmd2.Parameters.AddWithValue("@param3", textBox3.Text);
                //if (radioButton1.Checked)
                //{
                //    cmd2.Parameters.AddWithValue("@param4", "H");
                //}
                //else if (radioButton2.Checked)
                //{
                //    cmd2.Parameters.AddWithValue("@param4", "M");
                //}
                cmd2.Parameters.AddWithValue("@param5", label7.Text);
                cmd2.Parameters.AddWithValue("@param6", comboBox1.Text);
                cmd2.Parameters.AddWithValue("@Perfil", comboBox2.Text);
                cmd2.Parameters.AddWithValue("@PerfilId", comboBox2.SelectedValue.ToString());

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
            cmd.CommandText = "delete from permisosusuarios where num = '" + label7.Text + "'";
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
                    string sql2 = "insert into PermisosUsuarios values (@param1, @param2)";
                    SqlCommand cmd2 = new SqlCommand(sql2, sqlConnection1);
                    cmd2.Parameters.AddWithValue("@param1", label7.Text); //Para grabar algo de un textbox
                    cmd2.Parameters.AddWithValue("@param2", checkedListBox1.Items[i].ToString()); //Para grabar una columna


                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();
                    sqlConnection1.Close();
                }
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox3.UseSystemPasswordChar = false;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            textBox3.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label6.Text == "* Nombre de usuario no disponible")
            {
                MessageBox.Show("Elija otro nombre de usuario");
                textBox2.Focus();
            }
            else if (textBox2.Text =="")
            {
                MessageBox.Show("Ingresa un nombre de usuario");
                textBox2.Focus();
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Ingresa una contraseña");
                textBox3.Focus();
            }
            else
            {
                guardar();
                button2.Visible = false;
                button5.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("No dejes el usuario en blanco", "AVISO");
                textBox2.Focus();
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("No dejes la contraseña en blanco", "AVISO");
                textBox3.Focus();
            }
            else
            {
                actualizauser();
                //deletepermisos();
                //insertapermiso();
                MessageBox.Show("Actualizado");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Variablescompartidas.num = "cancelado";
            using (CargaUsuarios cu = new CargaUsuarios())
            {
                cu.ShowDialog();
            }
            if (Variablescompartidas.num == "cancelado")
            {
                //sig();
                //button2.Visible = true;
                //button5.Visible = false;
                sig();
                button2.Visible = true;
                button5.Visible = false;
                button7.Visible = false;

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                reinicia();
                comboBox2.Text = " ";
            }
            else
            {
                label7.Text = Variablescompartidas.num;
                reinicia();
                cargarUsuario();
                //carga();
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
            cmd.CommandText = "Select Nombre, usuario, pass, sucursal, perfil from Usuarios where num = '" + label7.Text + "'";
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
                comboBox2.Text = reader["Perfil"].ToString();
                textBox1.Text = reader["Nombre"].ToString();
                comboBox1.Text = reader["Sucursal"].ToString();
                textBox3.Text = reader["pass"].ToString();
                //string sexo = reader["sexo"].ToString();

                //if (sexo == "H")
                //{
                //    radioButton1.Checked = true;
                //    //radioButton2.Checked = false;
                //}
                //else if (sexo == "M")
                //{
                //    //radioButton1.Checked = false;
                //    radioButton2.Checked = true;
                //}
                textBox2.Text = reader["Usuario"].ToString();
                
                //comboBox1.Text = reader["Sucursal"].ToString();
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
            cmd.CommandText = "select permiso from permisosUsuarios where num = '" + label7.Text + "'";
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
                    if (reader["permiso"].ToString() == checkedListBox1.Items[i].ToString())
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
            textBox2.Clear();
            textBox3.Clear();
            reinicia();
            comboBox2.Text = " ";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estas Seguro Que Deseas Eliminar A Este Usuario?", "ELIMINAR", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                //deletePermiso(label7.Text);
                deleteuser(label7.Text);
                comboBox2.Text = " ";
            }
            else if (result == DialogResult.No)
            {
            }

            
        }

        private void deletePermiso(string id)
        {
            try
            {
                cmd.CommandText = "delete from permisosusuarios where num = '" + id + "'";
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
                cmd.CommandText = "delete from usuarios where num = '" + id + "'";
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
                textBox2.Clear();
                textBox3.Clear();
                reinicia();
            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                MessageBox.Show(err.ToString(), "Error con Base de Datos");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Variablescompartidas.ususario == "Master")
            {
                using (Perfiles pf = new Perfiles())
                {
                    pf.ShowDialog();
                }
                cargaCombo2();
            }
            else
            {
                MessageBox.Show("No Tienes Acceso a Este Apartado", "AVISO");
            }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //reinicia();
            //CargaPerfilPerm();
        }

        private void CargaPerfilPerm()
        {
            //checkedListBox1.SetItemChecked(10, true);
            cmd.CommandText = "select permiso from permisoPerfil where num = '" + comboBox2.SelectedValue.ToString() + "'";
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
                    if (reader["permiso"].ToString() == checkedListBox1.Items[i].ToString())
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}