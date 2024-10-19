using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdenesOT
{
    public partial class Adjunta : MaterialForm
    {
        public static string FolioCotiza { get; set; }
        string ruta = "";
        int count = 0;

        SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);

        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;


        public Adjunta()
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
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            string path = @"\\192.168.1.127\Fuentes Sistemas\Cotizaciones\" + FolioCotiza;

            // Determine whether the directory exists.
            if (Directory.Exists(path))
            {
                copiarArchivo();
                //insertar();
            }
            else
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);

                copiarArchivo();
                //insertar();

            }
        }


        private void copiarArchivo()
        {
            try
            {
                OpenFileDialog sfd = new OpenFileDialog();
                //sfd.Filter = "Imagen JPG|*.jpg|Imagen BMP|*.bmp";
                sfd.Title = "Abriendo archivo a guardar";
                DialogResult result2 = sfd.ShowDialog();
                FileInfo vArchivo = new FileInfo(sfd.FileName.ToString());

                if (result2 == DialogResult.OK)
                {
                    ruta = @"\\192.168.1.127\Fuentes Sistemas\Cotizaciones\" + FolioCotiza + @"\" + sfd.SafeFileName;
                    if (System.IO.File.Exists(ruta))
                    {
                        DialogResult result = MessageBox.Show("Ya existe un archivo con este nombre\n¿Desea Reemplazarlo?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            System.IO.File.Delete(ruta);
                            vArchivo.CopyTo(@"\\192.168.1.127\Fuentes Sistemas\Cotizaciones\" + FolioCotiza + @"\" + sfd.SafeFileName);
                            insertar();
                        }
                        else if (result == DialogResult.No)
                        {

                        }
                    }
                    else
                    {
                        vArchivo.CopyTo(@"\\192.168.1.127\Fuentes Sistemas\Cotizaciones\" + FolioCotiza + @"\" + sfd.SafeFileName);
                        //ruta = @"\\192.168.1.129\controlflotilla\GRAPHICS\AUTOS\Archivos\" + CodigoText.Text + @"\" + sfd.SafeFileName;
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = sfd.SafeFileName;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = ruta;

                        count += 1;
                        insertar();
                    }


                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }
        }



        private void insertar()
        {
            //try
            //{
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string sql = "INSERT into Archivos(idDocumento, nombre, ruta) VALUES(@idDocu, @Nombre, @Ruta)";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection3);
                cmd.Parameters.AddWithValue("@idDocu", Form1.idDocumentoPasa);
                cmd.Parameters.AddWithValue("@Nombre", row.Cells["Nombre"].Value.ToString());
                cmd.Parameters.AddWithValue("@Ruta", row.Cells["Ver"].Value.ToString());
                sqlConnection3.Open();
                cmd.ExecuteNonQuery();
                sqlConnection3.Close();
            }
            MessageBox.Show("Guardado");
            //}
            //catch (NullReferenceException)
            //{


            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Ver")
                {
                    System.Diagnostics.Process.Start(Path.GetFullPath(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()));

                }
            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo abrir");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "delete from Archivos where nombre = @nombre and ruta = @ruta";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection3);
                cmd.Parameters.AddWithValue("@nombre", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString()); //Codigo
                cmd.Parameters.AddWithValue("@ruta", dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString()); //Codigo

                sqlConnection3.Open();
                cmd.ExecuteNonQuery();
                sqlConnection3.Close();

                System.IO.File.Delete(Path.GetFullPath(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString()));
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                count -= 1;
                MessageBox.Show("Borrado");
            }
            catch (Exception)
            {

                MessageBox.Show("Error al borrar");
            }
        }

        private void Adjunta_Load(object sender, EventArgs e)
        {
            try
            {
                //---------------------------Llenar Datos del producto ---------------------------------
                cmd.CommandText = "select Nombre, Ruta from Archivos where idDocumento = '" + Form1.idDocumentoPasa + "'";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection3;
                sqlConnection3.Open();
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[count].Cells[0].Value = reader["Nombre"].ToString();
                    dataGridView1.Rows[count].Cells[1].Value = reader["Ruta"].ToString();
                    count += 1;

                }
                sqlConnection3.Close();
            }
            catch (Exception)
            {


            }
        }
    }
}
