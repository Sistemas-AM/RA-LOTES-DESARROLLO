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

namespace Catalogo_Clientes
{
    public partial class Direcciones : MaterialForm
    {

        SqlDataReader reader;
        public static string idClientePasa { get; set; }
        public Direcciones()
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
            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button2.BackColor = ColorTranslator.FromHtml("#D66F6F");
            button4.BackColor = ColorTranslator.FromHtml("#D66F6F");

            colonia1.Visible = false;
        }

        private void Direcciones_Load(object sender, EventArgs e)
        {
            //llenarComboColoniasSINFILTRO();
            Colonia.SelectedIndex = -1;
            metroComboBox1.Text = "FISCAL";
            idCliente.Text = idClientePasa;
            CargarDireccion(idCliente.Text, "0");
        }

        private void CpText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(CpText.Text))
                {
                    llenarComboColonias(CpText.Text);
                }

            }
        }


        private void CargarDireccion(string idCliente, string TipoDirec)
        {
            try
            {
                string query = null;
                query = "select top 1 * from admdomicilios where cidcatalogo = '" + idCliente + "'  and ctipocatalogo = '1' and ctipodireccion= '" + TipoDirec + "' order by ciddireccion desc";


                SqlCommand cmd = new SqlCommand(query, Principal.Variablescompartidas.AcerosConnection);
                cmd.CommandType = CommandType.Text;

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
                SqlDataReader reader;

                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    idDir.Text = reader["CIDDIRECCION"].ToString();
                    PaisText.Text = reader["CPAIS"].ToString();
                    CpText.Text = reader["CCODIGOPOSTAL"].ToString();
                    llenarComboColonias(CpText.Text);
                    EstadoText.Text = reader["CESTADO"].ToString();
                    MunicipioText.Text = reader["CMUNICIPIO"].ToString();
                    CiudadText.Text = reader["CCIUDAD"].ToString();


                    Colonia.Text = reader["CCOLONIA"].ToString();
                    colonia1.Text = reader["CCOLONIA"].ToString();

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
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
            }

        }


        private void llenarComboColonias(string cp)
        {
            try
            {
                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                SqlCommand sc = new SqlCommand("select id, Colonia from codigosPostales where codigo = '" + cp + "' order by colonia", Principal.Variablescompartidas.RepAmsaConnection);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(string));

                dt.Load(reader);

                Colonia.ValueMember = "id";
                Colonia.DisplayMember = "Colonia";
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
                Colonia.DataSource = dt;


            }
            catch (Exception)
            {
                Colonia.Text = "";
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }

        }


        private void llenarComboColoniasSINFILTRO()
        {
            try
            {
                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                SqlCommand sc = new SqlCommand("select id, Colonia from codigosPostales order by colonia", Principal.Variablescompartidas.RepAmsaConnection);
                //select customerid,contactname from customer
                SqlDataReader reader;

                reader = sc.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(string));

                dt.Load(reader);

                Colonia.ValueMember = "id";
                Colonia.DisplayMember = "Colonia";
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
                Colonia.DataSource = dt;


            }
            catch (Exception)
            {
                Colonia.Text = "";
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }

        }

        private void Colonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Colonia.SelectedValue.ToString()))
                {
                    obtenDatosColonia();
                }
            }
            catch (NullReferenceException)
            {

            }
        }


        private void obtenDatosColonia()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"select codigospostales.*, folios.sucnom, folios.sucursal 
                from codigospostales inner join folios on codigospostales.idSucu = folios.idalmacen where id = '" + Colonia.SelectedValue.ToString() + "' order by colonia", Principal.Variablescompartidas.RepAmsaConnection);
                cmd.CommandType = CommandType.Text;

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    CiudadText.Text = reader["Ciudad"].ToString();
                    MunicipioText.Text = reader["Ciudad"].ToString();
                    EstadoText.Text = reader["Estado"].ToString();
                    PaisText.Text = reader["Pais"].ToString();
                    CpText.Text = reader["Codigo"].ToString();
                    //comboBox1.Text = reader["Sucursal"].ToString();

                }
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                CiudadText.Clear();
                EstadoText.Clear();
                PaisText.Clear();
                MunicipioText.Clear();
                CpText.Clear();
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
            }
        }

        public bool validacion()
        {
            bool validado = false;
            int errores = 0;
            errorProvider1.Clear();
            //if (CalleText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(CalleText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            //if (PaisText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(PaisText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            //if (EstadoText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(EstadoText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            if (CpText.Text.Length == 0)
            {
                errorProvider1.SetError(CpText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            //if (CiudadText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(CiudadText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            //if (MunicipioText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(MunicipioText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            //if (EstadoText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(EstadoText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            //if (Colonia.Text.Length == 0)
            //{
            //    errorProvider1.SetError(Colonia, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

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

        private void button1_Click(object sender, EventArgs e)
        {
            if (validacion())
            {
                if (idDir.Text == "0")
                {
                    if (metroComboBox1.Text == "FISCAL")
                    {
                        if (GuardarDireccion("0"))
                        {
                            MessageBox.Show("Guardado con exito", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CargarDireccion(idCliente.Text, "0");

                        }
                    }
                    else if (metroComboBox1.Text == "ENVIO")
                    {
                        if (GuardarDireccion("1"))
                        {
                            MessageBox.Show("Guardado con exito", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CargarDireccion(idCliente.Text, "1");
                        }
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Ya existe una dirección guardada\n¿Deseas Actualizarla?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (Actualizar(idDir.Text))
                        {
                            MessageBox.Show("Actualizado con exito", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                    }
                }
            }
            else
            {
                MessageBox.Show("ASEGURATE DE LLENAR LOS CAMPOS MARCADOS EN ROJO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        public bool GuardarDireccion(string tipoDireccion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("insDirecciones", Principal.Variablescompartidas.AcerosConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CIDCATALOGO", idCliente.Text);
                cmd.Parameters.AddWithValue("@CTIPOCATALOGO", "1");
                cmd.Parameters.AddWithValue("@CTIPODIRECCION", tipoDireccion);
                cmd.Parameters.AddWithValue("@CNOMBRECALLE", CalleText.Text);
                cmd.Parameters.AddWithValue("@CNUMEROEXTERIOR", ExteriorText.Text);
                cmd.Parameters.AddWithValue("@CNUMEROINTERIOR", InteriorText.Text);

                if (checkBox1.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@CCOLONIA", colonia1.Text);
                    colonias();

                }
                else
                {
                    cmd.Parameters.AddWithValue("@CCOLONIA", Colonia.Text);

                }


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

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
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
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
            }
        }


        public bool colonias()
        {
            try
            {
                SqlCommand cmd1 = new SqlCommand("InsertaCodigoPostal", Principal.Variablescompartidas.RepAmsaConnection);
                cmd1.CommandType = CommandType.StoredProcedure;

                //cmd1.Parameters.AddWithValue("@id", consultaId);
                cmd1.Parameters.AddWithValue("@codigo", CpText.Text);
                cmd1.Parameters.AddWithValue("@colonia", colonia1.Text);
                cmd1.Parameters.AddWithValue("@tipo", "");
                cmd1.Parameters.AddWithValue("@municipio", MunicipioText.Text);
                cmd1.Parameters.AddWithValue("@estado", EstadoText.Text);
                cmd1.Parameters.AddWithValue("@ciudad", CiudadText.Text);
                cmd1.Parameters.AddWithValue("@pais", PaisText.Text);
                cmd1.Parameters.AddWithValue("@idsucu", "");

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
                if (cmd1.ExecuteNonQuery() != 0)
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



        public bool Actualizar(string ciddireccion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[admDomicilios]
                            SET[CNOMBRECALLE] = @CNOMBRECALLE
                            ,[CNUMEROEXTERIOR] = @CNUMEROEXTERIOR
                            ,[CNUMEROINTERIOR] = @CNUMEROINTERIOR
                            ,[CCOLONIA] = @CCOLONIA
                            ,[CCODIGOPOSTAL] = @CCODIGOPOSTAL
                            ,[CTELEFONO1] = @CTELEFONO1
                            ,[CTELEFONO2] = @CTELEFONO2
                            ,[CTELEFONO3] = @CTELEFONO3
                            ,[CTELEFONO4] = @CTELEFONO4
                            ,[CEMAIL] = @CEMAIL
                            ,[CDIRECCIONWEB] = @CDIRECCIONWEB
                            ,[CPAIS] = @CPAIS
                            ,[CESTADO] = @CESTADO
                            ,[CCIUDAD] = @CCIUDAD
                            ,[CTEXTOEXTRA] = @CTEXTOEXTRA
                            ,[CTIMESTAMP] = @CTIMESTAMP
                            ,[CMUNICIPIO] = @CMUNICIPIO
                        WHERE CIDDIRECCION = @CIDIRECCION", Principal.Variablescompartidas.AcerosConnection);
                    cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@CIDCATALOGO", idCliente.Text);
                cmd.Parameters.AddWithValue("@CTIPOCATALOGO", "1");
                cmd.Parameters.AddWithValue("@CNOMBRECALLE", CalleText.Text);
                cmd.Parameters.AddWithValue("@CNUMEROEXTERIOR", ExteriorText.Text);
                cmd.Parameters.AddWithValue("@CNUMEROINTERIOR", InteriorText.Text);

                if (checkBox1.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@CCOLONIA", colonia1.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CCOLONIA", Colonia.Text);

                }

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
                cmd.Parameters.AddWithValue("@CIDIRECCION", ciddireccion);


                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
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

                MessageBox.Show("OCURRIO UN ERROR AL ACTUALIZAR LOS DATOS \n" + ex.Message, "ERROR AL ACTUALIZAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
            }
        }


        //public bool ActualizarCodigoPostal(string id)
        //{
        //    try
        //    {


        //        SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[CodigosPostales]
        //        SET [Codigo] = @Codigo
        //            ,[Colonia] = @Colonia
        //            ,[Tipo] = @Tipo
        //            ,[Municipio] = @Municipio
        //            ,[Estado] = @Estado
        //            ,[Ciudad] = @Ciudad
        //            ,[Pais] = @Pais
        //            ,[idSucu] = @idSucu
        //            where id = @id", Principal.Variablescompartidas.RepAmsaConnection);
        //        cmd.CommandType = CommandType.Text;

        //        cmd.Parameters.AddWithValue("@Codigo", CpText.Text);
        //        cmd.Parameters.AddWithValue("@Colonia", colonia1.Text);
        //        cmd.Parameters.AddWithValue("@Tipo", "");
        //        cmd.Parameters.AddWithValue("@Municipio", MunicipioText);
        //        cmd.Parameters.AddWithValue("@Estado", EstadoText.Text);
        //        cmd.Parameters.AddWithValue("@Ciudad", CiudadText.Text);
        //        cmd.Parameters.AddWithValue("@Pais", PaisText.Text);
        //        cmd.Parameters.AddWithValue("@idSucu", "");

        //        Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
        //        if (cmd.ExecuteNonQuery() != 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("OCURRIO UN ERROR AL ACTUALIZAR LOS DATOS \n" + ex.Message, "ERROR AL ACTUALIZAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);
        //    }
        //}

        public bool Delete(string ciddireccion)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[admDomicilios]  
                WHERE CIDDIRECCION = @CIDIRECCION", Principal.Variablescompartidas.AcerosConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CIDIRECCION", ciddireccion);


                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
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

                MessageBox.Show("OCURRIO UN ERROR AL ELIMINAR LOS DATOS \n" + ex.Message, "ERROR AL ELIMINAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
            }
        }





        private void limpiar()
        {
            idDir.Text = "0";
            PaisText.Clear();
            CpText.Clear();
            EstadoText.Clear();
            MunicipioText.Clear();
            CiudadText.Clear();
            Colonia.Text = "";
            ExteriorText.Clear();
            InteriorText.Clear();
            CalleText.Clear();
            emailText.Clear();
            TelefonoText.Clear();
            ReferenciaText.Clear();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == "FISCAL")
            {
                limpiar();
                CargarDireccion(idCliente.Text, "0");
            }
            else
            {
                limpiar();
                CargarDireccion(idCliente.Text, "1");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (idDir.Text!= "0")
            {
                DialogResult result = MessageBox.Show("¿Seguro que deseas eliminar esta dirección?", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (Delete(idDir.Text))
                    {
                        MessageBox.Show("Eliminado con exito", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        if (metroComboBox1.Text == "FISCAL")
                        {
                            limpiar();
                            CargarDireccion(idCliente.Text, "0");
                        }
                        else
                        {
                            limpiar();
                            CargarDireccion(idCliente.Text, "1");
                        }
                    }
                }
                else if (result == DialogResult.No)
                {

                }
            }
            else
            {
                MessageBox.Show("No hay una dirección guardada para eliminar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            checar();
        }

        private void checar()
        {
            if (checkBox1.Checked == true)
            {
                Colonia.Visible = false;
                colonia1.Visible = true;
                colonia1.Enabled = true;
            }else
            {
                Colonia.Visible = true;
                colonia1.Visible = false;
                colonia1.Enabled = false;
            }
        }
    }
}
