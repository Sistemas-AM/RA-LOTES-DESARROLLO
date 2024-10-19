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
using Microsoft.Reporting.WinForms;

namespace ReporteTraspasos
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection FlotillasConection = new SqlConnection(Principal.Variablescompartidas.Flotillas);
        SqlConnection Aceros = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlConnection RepoAmsa = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Form1()
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

            metroRadioButton4.Checked = true;

        }


        
        private void cargarCombo()
        {
            RepoAmsa.Open();
            SqlCommand sc = new SqlCommand("select sucnom from folios where idalmacen != 0", RepoAmsa);
            //select customerid,contactname from customer
            SqlDataReader reader;

            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("letra", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "sucnom";
            metroComboBox1.DisplayMember = "sucnom";
            metroComboBox1.DataSource = dt;

            RepoAmsa.Close();
        }

        private void cargaReporte()
        {
            SqlConnection S_Conn = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            S_Conn.Open();
            string query_1 = "";
            if (metroRadioButton4.Checked)
            {
                //   query_1 = @"select *,(Cantidad * peso) as PestoTot
                //from Traspasos where fecha between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha)+ "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "'  order by desde, fecha desc, horaSalida asc";

                query_1 = @"with devo as(

                select folio as folio2, Referencia
                as referencia2, fecha as fecha2, codigo as codigo2, 
                nombre as nombre2, Cantidad as cantidad2,(Cantidad * peso) as PestoTot2
                from Traspasos where folio like '%D%'),
                
                Normales as (
                select *,(Cantidad * peso) as PestoTot
                from Traspasos where 
                fecha  between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + @"' 
                )
                
                select normales.*, devo.*, 
                case when doc.ccancelado = 0 then 'No'
                when doc.CCANCELADO = 1 then 'CANCELADO'
                when doc.CCANCELADO is null then 'Nulo'
                end as Cancelado,
                doc.CCANCELADO 
                from Normales left join devo on
                Normales.Folio = devo.Referencia2
                and Normales.codigo = devo.codigo2
                left join adAmsaContpaqi.dbo.admdocumentos as doc
                on doc.ciddocumento = normales.idDocumento
                order by desde, normales.fecha desc, normales.Folio desc, horaSalida asc";

            }
            else if (metroRadioButton3.Checked)
            {
                if (metroRadioButton1.Checked)
                {
                    //    query_1 = @"select *,(Cantidad * peso) as PestoTot
                    //from Traspasos where fecha between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and desde = '"+metroComboBox1.Text+"' order by desde, fecha desc, horaSalida asc";

                    query_1 = @"with devo as(

                select folio as folio2, Referencia
                as referencia2, fecha as fecha2, codigo as codigo2, 
                nombre as nombre2, Cantidad as cantidad2, (Cantidad * peso) as PestoTot2
                from Traspasos where folio like '%D%'),
                
                Normales as (
                select *,(Cantidad * peso) as PestoTot
                    from Traspasos where fecha between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and desde = '" + metroComboBox1.Text + @"' 
                )
                
                select normales.*, devo.*, 
                case when doc.ccancelado = 0 then 'No'
                when doc.CCANCELADO = 1 then 'CANCELADO'
                when doc.CCANCELADO is null then 'Nulo'
                end as Cancelado, 
                doc.CCANCELADO 
                from Normales left join devo on
                Normales.Folio = devo.Referencia2
                and Normales.codigo = devo.codigo2
                left join adAmsaContpaqi.dbo.admdocumentos as doc
                on doc.ciddocumento = normales.idDocumento
                order by desde, normales.fecha desc, normales.Folio desc, horaSalida asc";
                }
                else if (metroRadioButton2.Checked)
                {
                    //    query_1 = @"select *,(Cantidad * peso) as PestoTot
                    //from Traspasos where FechaRecibido between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and DestinoReal = '" + metroComboBox1.Text + "' order by desde, fecha desc, horaSalida asc";

                    query_1 = @"with devo as(

                select folio as folio2, Referencia
                as referencia2, fecha as fecha2, codigo as codigo2, 
                nombre as nombre2, Cantidad as cantidad2, (Cantidad * peso) as PestoTot2
                from Traspasos where folio like '%D%'),
                
                Normales as (
                select *,(Cantidad * peso) as PestoTot
                    from Traspasos where FechaRecibido between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and DestinoReal = '" + metroComboBox1.Text + @"'
                )
                
                select normales.*, devo.*, 
                case when doc.ccancelado = 0 then 'No'
                when doc.CCANCELADO = 1 then 'CANCELADO'
                when doc.CCANCELADO is null then 'Nulo'
                end as Cancelado,
                doc.CCANCELADO 
                from Normales left join devo on
                Normales.Folio = devo.Referencia2
                and Normales.codigo = devo.codigo2
                left join adAmsaContpaqi.dbo.admdocumentos as doc
                on doc.ciddocumento = normales.idDocumento
                order by desde, normales.fecha desc, normales.Folio desc, horaSalida asc";
                }

                else if (metroRadioButton5.Checked)
                {
                    //    query_1 = @"select *,(Cantidad * peso) as PestoTot
                    //from Traspasos where Fecha between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and DestinoReal = '" + metroComboBox1.Text + "' and estatus = 'T' order by desde, fecha desc, horaSalida asc";

                    query_1 = @"with devo as(

                select folio as folio2, Referencia
                as referencia2, fecha as fecha2, codigo as codigo2, 
                nombre as nombre2, Cantidad as cantidad2, (Cantidad * peso) as PestoTot2
                from Traspasos where folio like '%D%'),
                
                Normales as (
                select *,(Cantidad * peso) as PestoTot
                    from Traspasos where Fecha between '" + metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and '" + metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha) + "' and DestinoReal = '" + metroComboBox1.Text + @"' and estatus = 'T'
                )
                
                select normales.*, devo.*, 
                case when doc.ccancelado = 0 then 'No'
                when doc.CCANCELADO = 1 then 'CANCELADO'
                when doc.CCANCELADO is null then 'Nulo'
                end as Cancelado,
                doc.CCANCELADO 
                from Normales left join devo on
                Normales.Folio = devo.Referencia2
                and Normales.codigo = devo.codigo2
                left join adAmsaContpaqi.dbo.admdocumentos as doc
                on doc.ciddocumento = normales.idDocumento
                order by desde, normales.fecha desc, normales.Folio desc, horaSalida asc";


                }
            }

           



            SqlCommand Command_1 = new SqlCommand(query_1, S_Conn);
            SqlDataAdapter Data_Adapter = new SqlDataAdapter(Command_1);
            DataSet1 Data_Set = new DataSet1();
            Data_Adapter.Fill(Data_Set);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", Data_Set.Tables[1]));
            this.reportViewer1.RefreshReport();

            //ReportParameter[] parameters = new ReportParameter[1];

            //parameters[0] = new ReportParameter("letra", Form1.Letra);
            ////parameters[0] = new ReportParameter("Imagen", "file:\\" + Form1.imagen, true);
            ////reportViewer1.LocalReport.EnableExternalImages = true;
            //reportViewer1.LocalReport.SetParameters(parameters);

            //reportViewer1.Visible = true;
            reportViewer1.RefreshReport();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargarCombo();

            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                metroComboBox1.Enabled = true;
            }
            else
            {
                metroRadioButton4.Checked = false;
                metroRadioButton4.Visible = false;
                metroRadioButton3.Checked = true;
                metroComboBox1.Enabled = false;
                metroComboBox1.Text = Principal.Variablescompartidas.sucural;

            }
            this.reportViewer1.RefreshReport();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            cargaReporte();
        }

        private void metroRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (metroRadioButton3.Checked)
            {

                if (Principal.Variablescompartidas.sucural == "AUDITOR")
                {
                    metroComboBox1.Enabled = true;
                }
                else
                {
                    metroRadioButton4.Checked = false;
                    metroComboBox1.Enabled = false;
                    metroComboBox1.Text = Principal.Variablescompartidas.sucural;

                }

                //metroComboBox1.Enabled = true;
            }
            else
            {
                metroComboBox1.Enabled = false;
            }
        }

        private void metroRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (metroRadioButton4.Checked)
            {
                metroRadioButton1.Checked = false;
                metroRadioButton2.Checked = false;
                groupBox1.Enabled = false;
            }
            else
            {
                metroRadioButton1.Checked = true;
                metroRadioButton2.Checked = false;
                groupBox1.Enabled = true;
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
