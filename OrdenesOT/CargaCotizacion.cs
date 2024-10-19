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

    
    public partial class CargaCotizacion : MaterialForm
    {

        int permiteCerrar = 0;
        //SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlConnection3 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();

        public static string foliocot { get; set; }
        public static string ClienteCodigo { get; set; }
        public static string Local { get; set; }
        public static string observaciones { get; set; }

        public static string date1 { get; set; }
        public static string FolioSolo { get; set; }

        public static string id_DocumentoPasa { get; set; }

        public static string estatus { get; set; }

        public static string tipoDocu { get; set; }

        public static string FolioOTPasa { get; set; }

        public static string estatuspasa { get; set; }

        public static string AtencionPasa { get; set; }
        public static string SolicitoPasa { get; set; }

        public static string NotasI { get; set; }

        public static string ListaPasa { get; set; }

        public CargaCotizacion()
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
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#CBD0D3");
            cargargrid(estatus);
            foliocot = "";
             permiteCerrar = 0;

        }

        private void cargargrid(string estatus)
        {
            SqlCommand cmd = new SqlCommand(@"Select id_Documento, tipo_doc, FolioCotizacion, FolioCot, FolioOT, Fecha, esLocal, codCliente, observaciones,  CRAZONSOCIAL as nombrecliente, estatus, Atencion, Solicito, notas_Internas, lista_precios
            from CotizacionesOT left join adAMSACONTPAQi.dbo.admClientes
            on CodCliente = CCODIGOCLIENTE
            where sucursal = '" + Form1.sucursal+"' and estatus = '"+estatus+"' order by Fecha desc, foliocotizacion desc", sqlConnection3);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection3.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                try
                {
                    foliocot = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["FolioBus"].Value.ToString();
                    ClienteCodigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codCliente"].Value.ToString();
                    Local = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["esLocal"].Value.ToString();
                    observaciones = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["obs"].Value.ToString();
                    FolioSolo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["folio"].Value.ToString();
                    date1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Fecha"].Value.ToString();
                    id_DocumentoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_doc"].Value.ToString();
                    tipoDocu = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Tipo_Documento"].Value.ToString();
                    FolioOTPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["FolioOT"].Value.ToString();
                    estatuspasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["estatus"].Value.ToString();

                    AtencionPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Atencion"].Value.ToString();
                    SolicitoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Solicito"].Value.ToString();
                    NotasI = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Notas"].Value.ToString();
                ListaPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Lista"].Value.ToString();
                permiteCerrar = 1;
                    this.Close();
                }
                catch (NullReferenceException)
                {

                    MessageBox.Show("No se ha seleccionado nada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foliocot = "";
            id_DocumentoPasa = "";
            ClienteCodigo = "";
            Local = "";
            permiteCerrar = 1;
            this.Close();
        }

        private void Busqueda_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"folioCotizacion LIKE '%{Busqueda.Text}%' or nombrecliente LIKE '%{Busqueda.Text}%'";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                try
                {
                    foliocot = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["FolioBus"].Value.ToString();
                    ClienteCodigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codCliente"].Value.ToString();
                    Local = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["esLocal"].Value.ToString();
                    observaciones = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["obs"].Value.ToString();
                    FolioSolo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["folio"].Value.ToString();
                    date1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Fecha"].Value.ToString();
                    id_DocumentoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_doc"].Value.ToString();
                    tipoDocu = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Tipo_Documento"].Value.ToString();
                    FolioOTPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["FolioOT"].Value.ToString();
                    estatuspasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["estatus"].Value.ToString();

                    AtencionPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Atencion"].Value.ToString();
                    SolicitoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Solicito"].Value.ToString();
                    NotasI = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Notas"].Value.ToString();
                    ListaPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Lista"].Value.ToString();
                    permiteCerrar = 1;
                    this.Close();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("No se ha seleccionado nada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void Busqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.Select();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                foliocot = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["FolioBus"].Value.ToString();
                ClienteCodigo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["codCliente"].Value.ToString();
                Local = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["esLocal"].Value.ToString();
                observaciones = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["obs"].Value.ToString();
                FolioSolo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["folio"].Value.ToString();
                date1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Fecha"].Value.ToString();
                id_DocumentoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_doc"].Value.ToString();
                tipoDocu = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Tipo_Documento"].Value.ToString();
                FolioOTPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["FolioOT"].Value.ToString();
                estatuspasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["estatus"].Value.ToString();

                AtencionPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Atencion"].Value.ToString();
                SolicitoPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Solicito"].Value.ToString();
                NotasI = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Notas"].Value.ToString();
                ListaPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Lista"].Value.ToString();
                permiteCerrar = 1;
                this.Close();
            }
            catch (NullReferenceException)
            {

                MessageBox.Show("No se ha seleccionado nada", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void CargaCotizacion_Load(object sender, EventArgs e)
        {

        }

        private void CargaCotizacion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (permiteCerrar == 0)
            {
                e.Cancel = true;
            }
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            dt.Rows.Clear();

            if (materialCheckBox1.Checked == true)
            {
                cargargrid("4");
            }
            else
            {
                cargargrid("3");
            }
        }
    }
}
