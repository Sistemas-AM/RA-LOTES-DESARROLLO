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
    public partial class DireccionEnvio : MaterialForm
    {
        public static string direccionCotiza { get; set; }
        public static string EnvioCotiza { get; set; }
        public DireccionEnvio()
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

        public bool validacion()
        {
            bool validado = false;
            int errores = 0;
            errorProvider1.Clear();
            if (CalleText.Text.Length == 0)
            {
                errorProvider1.SetError(CalleText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (PaisText.Text.Length == 0)
            {
                errorProvider1.SetError(PaisText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (EstadoText.Text.Length == 0)
            {
                errorProvider1.SetError(EstadoText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (CpText.Text.Length == 0)
            {
                errorProvider1.SetError(CpText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (CiudadText.Text.Length == 0)
            {
                errorProvider1.SetError(CiudadText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (MunicipioText.Text.Length == 0)
            {
                errorProvider1.SetError(MunicipioText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (EstadoText.Text.Length == 0)
            {
                errorProvider1.SetError(EstadoText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (ColoniaText.Text.Length == 0)
            {
                errorProvider1.SetError(ColoniaText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (TelefonoText.Text.Length == 0)
            {
                errorProvider1.SetError(TelefonoText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validacion())
            {
                if (GuardarDireccion())
                {
                    MessageBox.Show("LA DIRECCION SE GUARDO CORRECTAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    if (idDir.Text=="0")
                    {
                        idDir.Text = idDireccion(idCliente.Text).ToString();
                    }

                    direccionCotiza = idDir.Text;

                    if (GuardarEnvio())
                    {
                        if (EnvioText.Text == "0")
                        {
                            EnvioText.Text = idEnvio(idCliente.Text).ToString();
                        }


                        EnvioCotiza = EnvioText.Text;
                        this.Dispose();
                    }

                    
                    
                }


                
            }
            else
            {
                MessageBox.Show("ASEGURATE DE LLENAR LOS CAMPOS MARCADOS EN ROJO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public int idDireccion(string idcliente)
        {
            try
            {
                string query = null;
                int direccion = 0;
                query = "select top 1* from direcciones where cidcatalogo = '"+idcliente+"' order by ciddireccion desc";
                
                SqlCommand cmd = new SqlCommand(query, Principal.Variablescompartidas.RepAmsaConnection);
                cmd.CommandType = CommandType.Text;

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                SqlDataReader reader;

                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    direccion= Int32.Parse(reader["CIDDIRECCION"].ToString());
                    

                }

                return direccion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCURRIO UN ERROR AL CARGAR LOS DATOS \n" + ex.Message, "ERROR AL GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }
        }



        public int idEnvio(string idcliente)
        {
            try
            {
                string query = null;
                int direccion = 0;
                query = "select top 1* from datosEnvio where idCliente = '" + idcliente + "' order by idEnvio desc";

                SqlCommand cmd = new SqlCommand(query, Principal.Variablescompartidas.RepAmsaConnection);
                cmd.CommandType = CommandType.Text;

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                SqlDataReader reader;

                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    direccion = Int32.Parse(reader["idEnvio"].ToString());


                }

                return direccion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCURRIO UN ERROR AL CARGAR LOS DATOS \n" + ex.Message, "ERROR AL GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }
        }



        private void DireccionEnvio_Load(object sender, EventArgs e)
        {
            idCliente.Text = Form1.idClientePasa;
            idDir.Text = Form1.idDireccionPasa;
            EnvioText.Text = Form1.idEnvioPasa;
            direccionCotiza = "0";
            EnvioCotiza = "0";
            CargarDireccion(idCliente.Text, idDir.Text);
            if (EnvioText.Text !="0")
            {
                CargarEnvio(idCliente.Text, EnvioText.Text);
            }
        }

        private void CargarDireccion(string idCliente, string idDireccion)
        {
            try
            {
                string query = null;

                if (idDireccion == "0")
                {
                    query = "select top 1 * from direcciones where cidcatalogo = '" + idCliente + "' order by ciddireccion desc";
                }
                else
                {
                    query = "select * from direcciones where cidcatalogo = '" + idCliente + "' and ciddireccion = '" + idDireccion + "'";
                }
                SqlCommand cmd = new SqlCommand(query, Principal.Variablescompartidas.RepAmsaConnection);
                cmd.CommandType = CommandType.Text;

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                SqlDataReader reader;

                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    idDir.Text = reader["CIDDIRECCION"].ToString();
                    PaisText.Text = reader["CPAIS"].ToString();
                    CpText.Text = reader["CCODIGOPOSTAL"].ToString();
                    EstadoText.Text = reader["CESTADO"].ToString();
                    MunicipioText.Text = reader["CMUNICIPIO"].ToString();
                    CiudadText.Text = reader["CCIUDAD"].ToString();
                    ColoniaText.Text = reader["CCOLONIA"].ToString();
                    ExteriorText.Text = reader["CNUMEROEXTERIOR"].ToString();
                    InteriorText.Text = reader["CNUMEROINTERIOR"].ToString();
                    CalleText.Text = reader["CNOMBRECALLE"].ToString();
                    emailText.Text = reader["CEMAIL"].ToString();
                    TelefonoText.Text = reader["CTELEFONO1"].ToString();
                    ReferenciaText.Text = reader["CTEXTOEXTRA"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCURRIO UN ERROR AL CARGAR LOS DATOS \n" + ex.Message, "ERROR AL GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }
            
        }



        private void CargarEnvio(string idCliente, string idEnvio)
        {
            try
            {
                string query = null;

                query = "select * from DatosEnvio where idcliente = '" + idCliente + "' and idEnvio = '" + idEnvio + "'";
                
                SqlCommand cmd = new SqlCommand(query, Principal.Variablescompartidas.RepAmsaConnection);
                cmd.CommandType = CommandType.Text;

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                SqlDataReader reader;

                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    DestinatarioText.Text = reader["Destinatario"].ToString();
                    GuiaText.Text = reader["NoGuia"].ToString();
                    TransportistaText.Text = reader["Mensajeria"].ToString();
                    CuentaText.Text = reader["CuentaMensajeria"].ToString();
                    CajasText.Text = reader["Cajas"].ToString();
                    PesoEnvioText.Text = reader["Peso"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCURRIO UN ERROR AL CARGAR LOS DATOS \n" + ex.Message, "ERROR AL GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }

        }


        public bool GuardarDireccion()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertaDirecciones", Principal.Variablescompartidas.RepAmsaConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CIDDIRECCION", idDir.Text);

                if (idDir.Text != "0")
                {
                    cmd.Parameters.AddWithValue("@Tipo", "2");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Tipo", "1");
                }
                

                cmd.Parameters.AddWithValue("@CIDCATALOGO", idCliente.Text);
                cmd.Parameters.AddWithValue("@CTIPOCATALOGO", "1");
                cmd.Parameters.AddWithValue("@CTIPODIRECCION", "1");
                cmd.Parameters.AddWithValue("@CNOMBRECALLE", CalleText.Text);
                cmd.Parameters.AddWithValue("@CNUMEROEXTERIOR", ExteriorText.Text);
                cmd.Parameters.AddWithValue("@CNUMEROINTERIOR", InteriorText.Text);
                cmd.Parameters.AddWithValue("@CCOLONIA", ColoniaText.Text);
                cmd.Parameters.AddWithValue("@CCODIGOPOSTAL", CpText.Text);
                cmd.Parameters.AddWithValue("@CTELEFONO1", TelefonoText.Text);
                cmd.Parameters.AddWithValue("@CTELEFONO2", "");
                cmd.Parameters.AddWithValue("@CTELEFONO3", "");
                cmd.Parameters.AddWithValue("@CTELEFONO4", "");
                cmd.Parameters.AddWithValue("@CEMAIL", emailText.Text);
                cmd.Parameters.AddWithValue("@CDIRECCIONWEB", "");
                cmd.Parameters.AddWithValue("@CPAIS", PaisText.Text);
                cmd.Parameters.AddWithValue("@CESTADO", EstadoText.Text);
                cmd.Parameters.AddWithValue("@CCIUDAD", CiudadText.Text);
                cmd.Parameters.AddWithValue("@CTEXTOEXTRA", ReferenciaText.Text);
                cmd.Parameters.AddWithValue("@CTIMESTAMP", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha));
                cmd.Parameters.AddWithValue("@CMUNICIPIO", MunicipioText.Text);
                cmd.Parameters.AddWithValue("@CSUCURSAL", "");


                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("OCURRIO UN ERROR AL GUARDAR LOS DATOS \n" + ex.Message, "ERROR AL GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }
        }



        public bool GuardarEnvio()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertaDatosEnvio", Principal.Variablescompartidas.RepAmsaConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idEnvio", EnvioText.Text);

                if (EnvioText.Text != "0")
                {
                    cmd.Parameters.AddWithValue("@Tipo", "2");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Tipo", "1");
                }
                
                cmd.Parameters.AddWithValue("@Destinatario", DestinatarioText.Text);
                cmd.Parameters.AddWithValue("@NoGuia", GuiaText.Text);
                cmd.Parameters.AddWithValue("@Mensajeria", TransportistaText.Text);
                cmd.Parameters.AddWithValue("@CuentaMensajeria", CuentaText.Text);
                cmd.Parameters.AddWithValue("@Cajas", CajasText.Text);
                cmd.Parameters.AddWithValue("@Peso", PesoEnvioText.Text);
                cmd.Parameters.AddWithValue("@idCliente", idCliente.Text);

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                if (cmd.ExecuteNonQuery() != 0)
                {

                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("OCURRIO UN ERROR AL GUARDAR LOS DATOS \n" + ex.Message, "ERROR AL GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }
        }



        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            using (CargarDirecciones CD = new CargarDirecciones())
            {
                CD.ShowDialog();
            }
            if (CargarDirecciones.idDireccionPasa != "0")
            {
                idDir.Text = CargarDirecciones.idDireccionPasa;
                CargarDireccion(idCliente.Text, idDir.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            direccionCotiza = idDir.Text;
            EnvioCotiza = EnvioText.Text;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            idDir.Text = "0";
            PaisText.Clear();
            CpText.Clear();
            EstadoText.Clear();
            MunicipioText.Clear();
            CiudadText.Clear();
            ColoniaText.Clear();
            ExteriorText.Clear();
            InteriorText.Clear();
            CalleText.Clear();
            emailText.Clear();
            TelefonoText.Clear();
            ReferenciaText.Clear();
        }

        private void CajasText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Si deseas, puedes permitir numeros decimales (o float)
            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void PesoEnvioText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Si deseas, puedes permitir numeros decimales (o float)
            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
