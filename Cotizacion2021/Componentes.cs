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
    public partial class Componentes : MaterialForm
    {

        SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        DataTable dt = new DataTable();

        int permiteCerrar = 0;

        
        public Componentes()
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
        }

        private void SoloNum(object sender, KeyPressEventArgs e)
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

        private bool valida()
        {
            bool validado = false;
            int errores = 0;
           // errorProvider1.Clear();

            if (Descrip.Text.Length == 0)
            {
                errores += 1;
            }

            if (Prec.Text.Length == 0)
            {
                Prec.Text = "0";
            }

            if (Cant.Text.Length == 0)
            {
                Cant.Text = "0";
            }
            
            if (errores >= 1)
            {
                validado = false;
                return validado;
            }
            else
            {
                validado = true;
                return validado;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (valida())
            {
                permiteCerrar = 1;
                Componente.Descripcion = Descrip.Text;
                Componente.Material = Mate.Text;
                Componente.Tolerancia = Tole.Text;
                Componente.Precio = float.Parse(Prec.Text);
                Componente.Cantidad = float.Parse(Cant.Text);
                Componente.Total = Componente.Precio * Componente.Cantidad;
                this.Close();
            }
            else
            {
                MessageBox.Show("Asegurate de escribir una descripcion", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Deseas Descartar este componente?\nTen encuenta que se eliminara todo lo capturado en este componente", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                if (DeleteComponente(idComponenteText.Text))
                {
                    permiteCerrar = 1;
                    Componente.Descripcion = "NO";
                    this.Close();
                }
            }
            else if (result == DialogResult.No)
            {

            }
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            using (Operaciones op = new Operaciones())
            {
                op.ShowDialog();
            }
            dt.Rows.Clear();
            cargarOperaciones();
            //dataGridView2.Rows.Clear();
            //var Filtrados = PantallaProductos.Operaciones.Where(x => x.id_Compo == PantallaProductos.id_Mov).ToList();
            //foreach (var valor in Filtrados)
            //{
            //    dataGridView2.Rows.Add();
            //    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Id_Operacion"].Value =valor.id_Operacion;
            //    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Codigo"].Value = valor.Codigo;
            //    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Descripcion"].Value = valor.Descripcion;
            //    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Supervisor"].Value = valor.Supervisor;
            //}
            
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (DeleteOperacion(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["id_Opera"].Value.ToString()))
                {
                    dt.Rows.Clear();
                    cargarOperaciones();
                }
                //int id = Int32.Parse(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["Id_Operacion"].Value.ToString());
                //PantallaProductos.Operaciones.RemoveAll(x => x.id_Compo == PantallaProductos.idPasa && x.id_Operacion == id);
                //dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
            }
            catch (Exception)
            {

            }
        }

        private bool DeleteOperacion(string id)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand("delete from operaciones_Componentes where id = @id", sqlConnection3);
                cmd.CommandType = CommandType.Text;

                
                cmd.Parameters.AddWithValue("@id", id);
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


        private bool DeleteComponente(string id)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand("delete from componentesOT where id_componente = @id", sqlConnection3);
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.AddWithValue("@id", id);
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

        private void cargarOperaciones()
        {
            SqlCommand cmd = new SqlCommand(@"select id, id_Componente, OC.id_operacion, Descripcion, codigo, supervisor from operaciones_Componentes as OC 
            inner join catalogo_operaciones as OP
            on OC.id_operacion = OP.id_Operacion
            where id_componente ='"+idComponenteText.Text+"'", sqlConnection3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView2.DataSource = dt;
            sqlConnection3.Close();
        }

        private void Componentes_Load(object sender, EventArgs e)
        {
            permiteCerrar = 0;
            if (PantallaProductos.idPasa != 0)
            {
                Descrip.Text = Componente.Descripcion;
                Mate.Text = Componente.Material;
                Tole.Text = Componente.Tolerancia;
                Prec.Text = Componente.Precio.ToString();
                Cant.Text = Componente.Precio.ToString();

                idComponenteText.Text = PantallaProductos.idPasa.ToString() ;
                Operaciones.idComponente = idComponenteText.Text;
                dt.Rows.Clear();
                cargarOperaciones();


                //var Filtrados = PantallaProductos.Operaciones.Where(x => x.id_Compo == PantallaProductos.idPasa).ToList();
                //foreach (var valor in Filtrados)
                //{
                //    dataGridView2.Rows.Add();
                //    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Id_Operacion"].Value = valor.id_Operacion;
                //    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Codigo"].Value = valor.Codigo;
                //    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Descripcion"].Value = valor.Descripcion;
                //    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells["Supervisor"].Value = valor.Supervisor;
                //    //MessageBox.Show(valor.id_Operacion + " " + valor.Descripcion);
                //}

            }
            else
            {
                idComponenteText.Text = ObtenIDCompo();

                Operaciones.idComponente = idComponenteText.Text;

                if (GuardaComponente(idComponenteText.Text, PantallaProductos.id_MovimientoPasaCompo.ToString(), "", "", "", "0", "0"))
                {
                    //MessageBox.Show("Prosige");
                }
            }
        }


        private bool GuardaComponente(string id_Compi, string id_Mov, string Descripcion, string Tolerancia, string Material, string Cantidad, string Precio)
        {
            //bool guardo = false;
            try
            {
                Principal.Variablescompartidas.AbrirAceros(sqlConnection3);
                SqlCommand cmd = new SqlCommand("InsertaComponentes", sqlConnection3);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_Componente", id_Compi);
                cmd.Parameters.AddWithValue("@id_Mov", id_Mov);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Tolerancia", Tolerancia);
                cmd.Parameters.AddWithValue("@Material", Material);
                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                cmd.Parameters.AddWithValue("@Precio", Precio);

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



        private string ObtenIDCompo()
        {
            cmd.CommandText = @"declare @id_doc int

	        if((select count(*) from componentesOT) =0)
	        begin
	        set @id_doc = (select 1 as siguiente);
	        end 
	        else
	        begin
	        set @id_doc = (select top 1 (id_componente + 1) as siguiente from componentesOT 
	        order by id_componente desc);
	        end

	        select @id_doc as siguiente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection3;
            sqlConnection3.Open();
            reader = cmd.ExecuteReader();

            string idDoc = "";
            if (reader.Read())
            {
                idDoc = reader["siguiente"].ToString();

            }
            sqlConnection3.Close();

            return idDoc;
        }

        private void Componentes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (permiteCerrar==0)
            {
               
                e.Cancel = true;
            }
            else
            {
               // this.Close();
            }
        }
    }
}
