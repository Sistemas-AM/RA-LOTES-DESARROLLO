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

namespace Cotizacion2021
{
    public partial class AltaClienteLoc : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string cteloc, idloc;
        public static string codigocli { get; set; }
        public static string esLocal { get; set; }

        public AltaClienteLoc()
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

            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button4.BackColor = ColorTranslator.FromHtml("#D66F6F");
        }

        private void obtemSiguientecteloc()
        {
            cmd.CommandText = "select top 1 id_ctelocal + 1 as siguiente, 'CTLOC-'+CAST(id_ctelocal + 1 as nvarchar)  as cod from ctelocal order by id_ctelocal desc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                cteloc = reader["cod"].ToString();
                idloc = reader["siguiente"].ToString();

            }
            sqlConnection1.Close();
        }

        private void insertaLocal()
        {
            try
            {
                string sql = "insert into ctelocal values (@id, @nombre, @rfc, @direccion, @numero, @telefono, @colonia, @cp, @email, @pais, @ciudad, @estado, @codigoCliente)";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue("@id", idloc);
                cmd.Parameters.AddWithValue("@nombre", NombreCli.Text);
                cmd.Parameters.AddWithValue("@rfc", Rfc.Text);
                cmd.Parameters.AddWithValue("@direccion", direccion.Text);
                cmd.Parameters.AddWithValue("@numero", Numero.Text);
                cmd.Parameters.AddWithValue("@telefono", Telefono.Text);
                cmd.Parameters.AddWithValue("@colonia", Colonia.Text);
                cmd.Parameters.AddWithValue("@cp", Cp.Text);
                cmd.Parameters.AddWithValue("@email", Correo.Text);
                cmd.Parameters.AddWithValue("@pais", Pais.Text);
                cmd.Parameters.AddWithValue("@ciudad", Ciudad.Text);
                cmd.Parameters.AddWithValue("@estado", Estado.Text);
                cmd.Parameters.AddWithValue("@codigoCliente", cteloc);


                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("Cliente Guardado Con Exito");
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;

                MessageBox.Show(err.ToString(), "Error con Base de Datos");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            codigocli = "-";
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NombreCli.Text))
                {
                    MessageBox.Show("Ingresa un nombre de cliente o selecciona un cliente", "AVISO");
                }
                else
                {
                    obtemSiguientecteloc();
                    insertaLocal();
                    codigocli = cteloc;
                    esLocal = "1";
                    this.Close();
                }
        }
    }
}
