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
    public partial class PantallaCliente : MaterialForm
    {
        public static string codigoPasa { get; set; }
        public static string NombrePasa { get; set; }
        public static string RFCPasa { get; set; }
        public static string DemonPasa { get; set; }
        public static string CurpPasa { get; set; }
        public static string CorreoPasa { get; set; }
        public static string idPasa { get; set; }
        public static string RegimenPasa { get; set; }
        //public static string NombreLargo { get; set; }

        public PantallaCliente()
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
            llenaCombo();
            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button3.BackColor = ColorTranslator.FromHtml("#3FACA9");
            button2.BackColor = ColorTranslator.FromHtml("#D66F6F");
            button4.BackColor = ColorTranslator.FromHtml("#72A0CA");
        }

        private void llenaCombo()
        {
            Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.RepAmsaConnection);
            //SqlCommand sc = new SqlCommand(@"select Codigo, CONCAT(Codigo, ' - ', Descripcion) as descripcion from regimen_fiscal
            //order by Codigo", Principal.Variablescompartidas.RepAmsaConnection);

            SqlCommand sc = new SqlCommand(@"select Codigo, CONCAT(Codigo, ' - ', Descripcion) as descripcion from regimen_fiscal
            order by Codigo", Principal.Variablescompartidas.RepAmsaConnection);

            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "Codigo";
            metroComboBox1.DisplayMember = "Descripcion";
            metroComboBox1.DataSource = dt;

            Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.RepAmsaConnection);

        }

        private void PantallaCliente_Load(object sender, EventArgs e)
        {
            if (idPasa == "0")
            {
                idText.Text = "0";
                button4.Visible = false;
                button1.Visible = true;

            }
            else
            {
                CodigoText.Text = codigoPasa;
                NombreText.Text = NombrePasa;
                RfcText.Text = RFCPasa;
                DenomText.Text = DemonPasa;
                CurpText.Text = CurpPasa;
                CorreoText.Text = CorreoPasa;
                idText.Text = idPasa;
                metroComboBox1.SelectedValue = RegimenPasa;
                button4.Visible = true;
                button1.Visible = false;
                //NombreLargoText.Text = NombreLargo;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //int caracteres = 60;

            //if (NombreText.Text.Length > caracteres)
            //{
            //    errorProvider1.SetError(NombreText, "EL CAMPO CONTIENE DEMASIADOS CARACTERES");
            //}

            if (validacion())
            {
                if (verificaRFC(RfcText.Text))
                {
                    MessageBox.Show("El RFC ya existe", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                {
                    if (idText.Text == "0")
                    {
                        idText.Text = idCliente().ToString();

                        if (InsertaCliente())
                        {
                            MessageBox.Show("Guardado");
                            CodigoText.Text = idText.Text;
                        }
                    }
                    else
                    {
                        if (validacion())
                        {
                            if (Update())
                            {
                                MessageBox.Show("LOS DATOS SE HAN ACTUALIZADO CORRECTAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                        else
                        {
                            MessageBox.Show("ASEGURATE DE LLENAR LOS CAMPOS MARCADOS EN ROJO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                }
                
            }
            else
            {
                MessageBox.Show("ASEGURATE DE LLENAR LOS CAMPOS MARCADOS EN ROJO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool validacion()
        {
            //int caracteres = 60;
            bool validado = false;
            int errores = 0;
            errorProvider1.Clear();
            //if (CodigoText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(CodigoText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            if (NombreText.Text.Length == 0)
            {
                errorProvider1.SetError(NombreText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (RfcText.Text.Length == 0)
            {
                errorProvider1.SetError(RfcText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (CorreoText.Text.Length == 0)
            {
                errorProvider1.SetError(CorreoText, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (metroComboBox1.SelectedIndex.Equals(-1))
            {
                errorProvider1.SetError(metroComboBox1, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            if (metroComboBox1.SelectedIndex.Equals(0))
            {
                errorProvider1.SetError(metroComboBox1, "EL CAMPO NO PUEDE ESTAR VACIO");
                errores += 1;
            }

            //if (NombreText.Text.Length > caracteres)
            //{
            //    MessageBox.Show("EL CAMPO NOMBRE CONTIENE DEMASIADOS CARACTERES", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    errorProvider1.SetError(NombreText, "CONTIENE DEMASIADOS CARACTERES");
            //    errores += 1;

            //}


            //if (DenomText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(DenomText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            //if (CurpText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(CurpText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

            //if (CorreoText.Text.Length == 0)
            //{
            //    errorProvider1.SetError(CorreoText, "EL CAMPO NO PUEDE ESTAR VACIO");
            //    errores += 1;
            //}

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

        public int idCliente()
        {
            try
            {
                string query = null;
                int idCliente = 0;
                query = @"select top 1 CIDCLIENTEPROVEEDOR + 1 as CIDCLIENTEPROVEEDOR from admclientes  
                    order by cidclienteproveedor desc";

                SqlCommand cmd = new SqlCommand(query, Principal.Variablescompartidas.AcerosConnection);
                cmd.CommandType = CommandType.Text;

                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
                SqlDataReader reader;

                reader = cmd.ExecuteReader();

                // Data is accessible through the DataReader object here.
                if (reader.Read())
                {
                    idCliente = Int32.Parse(reader["CIDCLIENTEPROVEEDOR"].ToString());
                    
                }

                return idCliente;
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCURRIO UN ERROR AL CARGAR LOS DATOS \n" + ex.Message, "ERROR AL GUARDAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
            }
        }


        public bool InsertaCliente()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("InsertaClientes", Principal.Variablescompartidas.AcerosConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CIDCLIENTEPROVEEDOR", idText.Text);
                cmd.Parameters.AddWithValue("@CCODIGOCLIENTE", idText.Text);
                cmd.Parameters.AddWithValue("@CRAZONSOCIAL", NombreText.Text);
                cmd.Parameters.AddWithValue("@CFECHAALTA", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@CRFC", RfcText.Text);
                cmd.Parameters.AddWithValue("@CCURP", CurpText.Text);
                cmd.Parameters.AddWithValue("@CDENCOMERCIAL", DenomText.Text);
                cmd.Parameters.AddWithValue("@CREPLEGAL", "");
                cmd.Parameters.AddWithValue("@CIDMONEDA", "1");
                cmd.Parameters.AddWithValue("@CLISTAPRECIOCLIENTE", "1");
                cmd.Parameters.AddWithValue("@CDESCUENTODOCTO", "0");
                cmd.Parameters.AddWithValue("@CDESCUENTOMOVTO", "0");
                cmd.Parameters.AddWithValue("@CBANVENTACREDITO", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFCLIENTE1", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFCLIENTE2", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFCLIENTE3", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFCLIENTE4", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFCLIENTE5", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFCLIENTE6", "0");
                cmd.Parameters.AddWithValue("@CTIPOCLIENTE", "1");
                cmd.Parameters.AddWithValue("@CESTATUS", "1");
                cmd.Parameters.AddWithValue("@CFECHABAJA", "");
                cmd.Parameters.AddWithValue("@CFECHAULTIMAREVISION", "");
                cmd.Parameters.AddWithValue("@CLIMITECREDITOCLIENTE", "0");
                cmd.Parameters.AddWithValue("@CDIASCREDITOCLIENTE", "0");
                cmd.Parameters.AddWithValue("@CBANEXCEDERCREDITO", "0");
                cmd.Parameters.AddWithValue("@CDESCUENTOPRONTOPAGO", "0");
                cmd.Parameters.AddWithValue("@CDIASPRONTOPAGO", "0");
                cmd.Parameters.AddWithValue("@CINTERESMORATORIO", "0");
                cmd.Parameters.AddWithValue("@CDIAPAGO", "31");
                cmd.Parameters.AddWithValue("@CDIASREVISION", "31");
                cmd.Parameters.AddWithValue("@CMENSAJERIA", "");
                cmd.Parameters.AddWithValue("@CCUENTAMENSAJERIA", "");
                cmd.Parameters.AddWithValue("@CDIASEMBARQUECLIENTE", "31");
                cmd.Parameters.AddWithValue("@CIDALMACEN", "0");
                cmd.Parameters.AddWithValue("@CIDAGENTEVENTA", "0");
                cmd.Parameters.AddWithValue("@CIDAGENTECOBRO", "0");
                cmd.Parameters.AddWithValue("@CRESTRICCIONAGENTE", "0");
                cmd.Parameters.AddWithValue("@CIMPUESTO1", "0");
                cmd.Parameters.AddWithValue("@CIMPUESTO2", "0");
                cmd.Parameters.AddWithValue("@CIMPUESTO3", "0");
                cmd.Parameters.AddWithValue("@CRETENCIONCLIENTE1", "0");
                cmd.Parameters.AddWithValue("@CRETENCIONCLIENTE2", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFPROVEEDOR1", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFPROVEEDOR2", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFPROVEEDOR3", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFPROVEEDOR4", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFPROVEEDOR5", "0");
                cmd.Parameters.AddWithValue("@CIDVALORCLASIFPROVEEDOR6", "0");
                cmd.Parameters.AddWithValue("@CLIMITECREDITOPROVEEDOR", "0");
                cmd.Parameters.AddWithValue("@CDIASCREDITOPROVEEDOR", "0");
                cmd.Parameters.AddWithValue("@CTIEMPOENTREGA", "0");
                cmd.Parameters.AddWithValue("@CDIASEMBARQUEPROVEEDOR", "31");
                cmd.Parameters.AddWithValue("@CIMPUESTOPROVEEDOR1", "0");
                cmd.Parameters.AddWithValue("@CIMPUESTOPROVEEDOR2", "0");
                cmd.Parameters.AddWithValue("@CIMPUESTOPROVEEDOR3", "0");
                cmd.Parameters.AddWithValue("@CRETENCIONPROVEEDOR1", "0");
                cmd.Parameters.AddWithValue("@CRETENCIONPROVEEDOR2", "0");
                cmd.Parameters.AddWithValue("@CBANINTERESMORATORIO", "0");
                cmd.Parameters.AddWithValue("@CCOMVENTAEXCEPCLIENTE", "0");
                cmd.Parameters.AddWithValue("@CCOMCOBROEXCEPCLIENTE", "0");
                cmd.Parameters.AddWithValue("@CBANPRODUCTOCONSIGNACION", "0");
                cmd.Parameters.AddWithValue("@CSEGCONTCLIENTE1", "");
                cmd.Parameters.AddWithValue("@CSEGCONTCLIENTE2", "");
                cmd.Parameters.AddWithValue("@CSEGCONTCLIENTE3", "");
                cmd.Parameters.AddWithValue("@CSEGCONTCLIENTE4", "");
                cmd.Parameters.AddWithValue("@CSEGCONTCLIENTE5", "");
                cmd.Parameters.AddWithValue("@CSEGCONTCLIENTE6", "");
                cmd.Parameters.AddWithValue("@CSEGCONTCLIENTE7", "");
                cmd.Parameters.AddWithValue("@CSEGCONTPROVEEDOR1", "");
                cmd.Parameters.AddWithValue("@CSEGCONTPROVEEDOR2", "");
                cmd.Parameters.AddWithValue("@CSEGCONTPROVEEDOR3", "");
                cmd.Parameters.AddWithValue("@CSEGCONTPROVEEDOR4", "");
                cmd.Parameters.AddWithValue("@CSEGCONTPROVEEDOR5", "");
                cmd.Parameters.AddWithValue("@CSEGCONTPROVEEDOR6", "");
                cmd.Parameters.AddWithValue("@CSEGCONTPROVEEDOR7", "");
                cmd.Parameters.AddWithValue("@CTEXTOEXTRA1", "");
                cmd.Parameters.AddWithValue("@CTEXTOEXTRA2", "");
                cmd.Parameters.AddWithValue("@CTEXTOEXTRA3", "");
                cmd.Parameters.AddWithValue("@CFECHAEXTRA", "");
                cmd.Parameters.AddWithValue("@CIMPORTEEXTRA1", "0");
                cmd.Parameters.AddWithValue("@CIMPORTEEXTRA2", "0");
                cmd.Parameters.AddWithValue("@CIMPORTEEXTRA3", "0");
                cmd.Parameters.AddWithValue("@CIMPORTEEXTRA4", "0");
                cmd.Parameters.AddWithValue("@CBANDOMICILIO", "0");
                cmd.Parameters.AddWithValue("@CBANCREDITOYCOBRANZA", "1");
                cmd.Parameters.AddWithValue("@CBANENVIO", "1");
                cmd.Parameters.AddWithValue("@CBANAGENTE", "1");
                cmd.Parameters.AddWithValue("@CBANIMPUESTO", "1");
                cmd.Parameters.AddWithValue("@CBANPRECIO", "0");
                cmd.Parameters.AddWithValue("@CTIMESTAMP", DateTime.Now.ToString(Principal.Variablescompartidas.FormatoFecha2));
                cmd.Parameters.AddWithValue("@CFACTERC01", "0");
                cmd.Parameters.AddWithValue("@CCOMVENTA", "0");
                cmd.Parameters.AddWithValue("@CCOMCOBRO", "0");
                cmd.Parameters.AddWithValue("@CIDMONEDA2", "1");
                cmd.Parameters.AddWithValue("@CEMAIL1", CorreoText.Text);
                cmd.Parameters.AddWithValue("@CEMAIL2", "");
                cmd.Parameters.AddWithValue("@CEMAIL3", "");
                cmd.Parameters.AddWithValue("@CTIPOENTRE", "6");
                cmd.Parameters.AddWithValue("@CCONCTEEMA", "0");
                cmd.Parameters.AddWithValue("@CFTOADDEND", "0");
                cmd.Parameters.AddWithValue("@CIDCERTCTE", "0");
                cmd.Parameters.AddWithValue("@CENCRIPENT", "0");
                cmd.Parameters.AddWithValue("@CBANCFD", "0");
                cmd.Parameters.AddWithValue("@CTEXTOEXTRA4", "");
                cmd.Parameters.AddWithValue("@CTEXTOEXTRA5", "");
                cmd.Parameters.AddWithValue("@CIMPORTEEXTRA5", "0");
                cmd.Parameters.AddWithValue("@CIDADDENDA", "-1");
                cmd.Parameters.AddWithValue("@CCODPROVCO", "");
                cmd.Parameters.AddWithValue("@CENVACUSE", "0");
                cmd.Parameters.AddWithValue("@CCON1NOM", "");
                cmd.Parameters.AddWithValue("@CCON1TEL", "");
                cmd.Parameters.AddWithValue("@CQUITABLAN", "");
                cmd.Parameters.AddWithValue("@CFMTOENTRE", "4");
                cmd.Parameters.AddWithValue("@CIDCOMPLEM", "-1");
                cmd.Parameters.AddWithValue("@CDESGLOSAI2", "0");
                cmd.Parameters.AddWithValue("@CLIMDOCTOS", "0");
                cmd.Parameters.AddWithValue("@CSITIOFTP", "");
                cmd.Parameters.AddWithValue("@CUSRFTP", "");
                cmd.Parameters.AddWithValue("@CMETODOPAG", "");
                cmd.Parameters.AddWithValue("@CNUMCTAPAG", "");
                cmd.Parameters.AddWithValue("@CIDCUENTA", "0");
                cmd.Parameters.AddWithValue("@CUSOCFDI", "");
                cmd.Parameters.AddWithValue("@CREGIMFISC", metroComboBox1.SelectedValue.ToString());
                //insertNombreLargo();
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


        //private void insertNombreLargo()
        //{
        //    try
        //    {
        //        string sql = "insert into admDatosAddenda values (@idaddenda, @tipocat, @idcat, @numcampo, @valor)";
        //        SqlCommand cmd = new SqlCommand(sql, Principal.Variablescompartidas.AcerosConnection);
        //        cmd.Parameters.AddWithValue("@idaddenda", 0);
        //        cmd.Parameters.AddWithValue("@tipocat", 1);
        //        cmd.Parameters.AddWithValue("@idcat", idText.Text);
        //        cmd.Parameters.AddWithValue("@numcampo", 0);
        //        cmd.Parameters.AddWithValue("valor", NombreLargoText.Text);

        //        Principal.Variablescompartidas.AcerosConnection.Open();
        //        cmd.ExecuteNonQuery();
        //        Principal.Variablescompartidas.AcerosConnection.Close();
        //        MessageBox.Show("Guardado");

        //    }
        //    catch (SqlException ex)
        //    {
        //        SqlError err = ex.Errors[0];
        //        string mensaje = string.Empty;

        //        MessageBox.Show(err.ToString(), "Error con Base de Datos");
        //        throw;
        //    }
        //}



        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(idText.Text))
            {
                if (idText.Text != "0")
                {
                    
                    Direcciones.idClientePasa = idText.Text;
                    using (Direcciones dr = new Direcciones())
                    {
                        dr.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Asegurate de primero guardar al cliente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Asegurate de primero guardar al cliente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RfcText_Leave(object sender, EventArgs e)
        {
            if (verificaRFC(RfcText.Text))
            {
                MessageBox.Show("El RFC ya existe", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool verificaRFC(string rfc)
        {
            if (rfc !="")
            {
                if (rfc.ToUpper() == "XAXX010101000")
                {
                    return false;
                }else
                {
                    using (SqlConnection conn = new SqlConnection(Principal.Variablescompartidas.Aceros))
                    {
                        string query = "select count(*) from admclientes where CRFC =@rfc";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("rfc", rfc);
                        conn.Open();

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count == 0)
                        {
                            return false;
                        }

                        else
                        {
                            return true;
                        }

                    }
                }
               
            }
            else
            {
                return false;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (validacion())
            {
                if (Update())
                {
                    MessageBox.Show("LOS DATOS SE HAN ACTUALIZADO CORRECTAMENTE", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            else
            {
                MessageBox.Show("ASEGURATE DE LLENAR LOS CAMPOS MARCADOS EN ROJO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool Update()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[admClientes]
                SET [CRAZONSOCIAL] = @CRAZONSOCIAL
                ,[CRFC] = @CRFC
                ,[CCURP] = @CCURP
                ,[CDENCOMERCIAL] = @CDENCOMERCIAL
                ,[CREGIMFISC]= @CREGIMFISC
                ,[CEMAIL1] = @Email
                WHERE [CIDCLIENTEPROVEEDOR] = @CIDCLIENTEPROVEEDOR", Principal.Variablescompartidas.AcerosConnection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@CIDCLIENTEPROVEEDOR", idText.Text);
                cmd.Parameters.AddWithValue("@CRAZONSOCIAL", NombreText.Text);
                cmd.Parameters.AddWithValue("@CRFC", RfcText.Text);
                cmd.Parameters.AddWithValue("@CCURP", CurpText.Text);
                cmd.Parameters.AddWithValue("@Email", CorreoText.Text);
                cmd.Parameters.AddWithValue("@CDENCOMERCIAL", DenomText.Text);
                cmd.Parameters.AddWithValue("@CREGIMFISC", metroComboBox1.SelectedValue.ToString());


                Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
                if (cmd.ExecuteNonQuery() != 0)
                {
                    //UpdateNombreLargo();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCURRIO UN ERROR AL ACTUALIZAR LOS DATOS \n" + ex.Message, "ERROR AL ELIMINAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
            }
        }



        //public bool UpdateNombreLargo()
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[admDatosAddenda]
        //        SET [VALOR] = @VALOR
        //        WHERE [IDCAT] = @CIDCLIENTEPROVEEDOR", Principal.Variablescompartidas.AcerosConnection);
        //        cmd.CommandType = CommandType.Text;

        //        cmd.Parameters.AddWithValue("@CIDCLIENTEPROVEEDOR", idText.Text);
        //        cmd.Parameters.AddWithValue("@VALOR", NombreLargoText.Text);

        //        Principal.Variablescompartidas.AbrirAceros(Principal.Variablescompartidas.AcerosConnection);
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
        //        MessageBox.Show("OCURRIO UN ERROR AL ACTUALIZAR LOS DATOS \n" + ex.Message, "ERROR AL ELIMINAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    finally
        //    {
        //        Principal.Variablescompartidas.CerrarAceros(Principal.Variablescompartidas.AcerosConnection);
        //    }
        //}


        
        private void RfcText_Click(object sender, EventArgs e)
        {

        }

    }
}
