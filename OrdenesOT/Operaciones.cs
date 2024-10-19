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

namespace OrdenesOT
{
    public partial class Operaciones : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlDataReader reader;
        DataTable dt = new DataTable();

        int permiteCerrar = 0;

        public static string idComponente { get; set; }

        public Operaciones()
        {
            InitializeComponent();

            // Create a material theme manager and add the form to manage (this)
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue400, Primary.Blue500,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );

            //VERDE
            button1.BackColor = ColorTranslator.FromHtml("#06a038");
            //ROJO
            button4.BackColor = ColorTranslator.FromHtml("#d93a2c");

            CargaComboCategoria();
            CargarData();

            permiteCerrar = 0;


        }

        private void CargarData()
        {
            SqlCommand cmd = new SqlCommand(@"select CO.*, cc.Descripcion as Categoria from Catalogo_Operaciones as CO
            inner join catalogo_categorias as CC
            on cc.Id_Categoria = CO.Id_Categoria", sqlConnection3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlConnection3.Close();
        }

        private void CargaComboCategoria()
        {
            sqlConnection3.Open();
            SqlCommand sc = new SqlCommand("select * from catalogo_Categorias", sqlConnection3);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Descripcion", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "id_categoria";
            metroComboBox1.DisplayMember = "Descripcion";
            metroComboBox1.DataSource = dt;

            sqlConnection3.Close();
        }

        private void metroComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            dt.Rows.Clear();

            SqlCommand cmd = new SqlCommand(@"select CO.*, cc.Descripcion as Categoria from Catalogo_Operaciones as CO
            inner join catalogo_categorias as CC
            on cc.Id_Categoria = CO.Id_Categoria where CO.id_categoria = '"+metroComboBox1.SelectedValue.ToString()+"'", sqlConnection3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlConnection3.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int op = Int32.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["id_operacionCol"].Value.ToString());
            string des = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["DescripcionCol"].Value.ToString();
            string cod = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["CodigoCol"].Value.ToString();
            string sup = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["SupervisorCol"].Value.ToString();

            PantallaProductos.Operaciones.Add(new Operacion { id_Compo = PantallaProductos.id_Mov, id_Operacion = op, Descripcion = des, Codigo = cod, Supervisor = sup });
            if (GuardaOperacion(idComponente, op.ToString()))
            {
                permiteCerrar = 1;
                this.Close();
            }

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Operacion.Descripcion = "NO";
            permiteCerrar = 1;
            this.Close();
        }


        private bool GuardaOperacion(string id_Compi, string id_Opera)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Operaciones_Componentes]
                                                                                     ([id_Componente]
                                                                                     ,[id_Operacion])
                                                                                                           VALUES
                                                                                     (@id_Componente
                                                                                     ,@Id_Operacion)", sqlConnection3);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@id_Componente", id_Compi);
                cmd.Parameters.AddWithValue("@Id_Operacion", id_Opera);

                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(sqlConnection3);
            }

        }

        private void Operaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (permiteCerrar == 0)
            {
                e.Cancel = true;
            }
        }

        private void Descrip_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"codigo LIKE '%{Descrip.Text}%' or descripcion LIKE '%{Descrip.Text}%'";
        }
    }
}
