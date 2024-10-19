using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encuesta
{
    public partial class Form1 : MaterialForm
    {
        SqlConnection ConectionAmsa = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection ConectionCompac = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dt = new DataTable();

        //public static string IdPasa { get; set; }
        public static string IDocumento { get; set; }
        public static string NombreCliente { get; set; }
        public static string NSucursal { get; set; }
        public static string Phone1 { get; set; }
        public static string Phone2 { get; set; }
        public static string SerieDocumento { get; set; }
        public static string CFolio { get; set; }
        public static string CIdAlmacen { get; set; }
        public static string CodigoCliente { get; set; }
        public static string CFecha { get; set; }

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

            LlenaCombo();

            //VERDE
            btn_Seleccionar.BackColor = ColorTranslator.FromHtml("#76CA62");

            //ROJO
            btn_salir.BackColor = ColorTranslator.FromHtml("#D66F6F");

            Usuario.Text = Principal.Variablescompartidas.usuario;
            NombreUsu.Text = Principal.Variablescompartidas.nombre;

        }

        private void AdjustColumnOrder()
        {

            dataGridView1.Columns["IdDocumento"].DisplayIndex = 0;
            dataGridView1.Columns["codCliente"].DisplayIndex = 1;
            dataGridView1.Columns["Nombre"].DisplayIndex = 2;
            dataGridView1.Columns["Documento"].DisplayIndex = 3;
            dataGridView1.Columns["Folio"].DisplayIndex = 4;
            dataGridView1.Columns["Fecha"].DisplayIndex = 5;
            dataGridView1.Columns["Sucursal"].DisplayIndex = 6;
            dataGridView1.Columns["Telefono1"].DisplayIndex = 7;

            dataGridView1.Columns["Telefono2"].DisplayIndex = 8;
            dataGridView1.Columns["Almacen"].DisplayIndex = 9;
        }

        private void ObtenValores()
        {
            string query = @"select (select count(*) from Encuestas where Usuario = @usuario
            and  semana = DATEPART(ISO_WEEK, GETDATE())) as Realizadas,
            (select count(*) from Encuestas where Usuario = @usuario
            and  semana = DATEPART(ISO_WEEK, GETDATE()) and contesto = 'Si') as Contestadas,
            semana,
            usuarios.usuario, 
            usuarios.nombre
            from encuestas
            inner join Usuarios on Encuestas.Usuario = Usuarios.Num
            where encuestas.usuario = @usuario
            and semana = DATEPART(ISO_WEEK, GETDATE())
            group by semana, usuarios.usuario, usuarios.nombre";
            
            SqlCommand cmd = new SqlCommand(query, ConectionAmsa);
            cmd.Parameters.AddWithValue("@Usuario", Principal.Variablescompartidas.num);

            ConectionAmsa.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                label3.Text = reader["semana"].ToString();
                Realizadas.Text = reader["Realizadas"].ToString();
                Contestadas.Text = reader["Contestadas"].ToString();
            }
            ConectionAmsa.Close();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            
            materialCheckBox1.Checked = true;
            ObtenValores();
            AdjustColumnOrder();
            dt.Rows.Clear();
            cargarGrid();
        }

        private void cargarGrid()
        {
            //SqlCommand cmd = new SqlCommand(@"select doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
            //                                  dom.ctelefono1, dom.ctelefono2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE
            //                                  from admDocumentos as doc
            //                                  INNER JOIN admDomicilios as dom ON doc.CIDDOCUMENTO = dom.CIDCATALOGO
            //                                  INNER JOIN admMovimientos as mov ON doc.CIDDOCUMENTO = mov.cidDocumento
            //                                  INNER JOIN ReportesAMSA.dbo.folios as folio ON mov.CIDALMACEN = folio.idalmacen
            //                                  INNER JOIN admClientes as cli ON  doc.CIDCLIENTEPROVEEDOR = cli.CIDCLIENTEPROVEEDOR
            //                                  where doc.CIDDOCUMENTODE = 4 and dom.ctipocatalogo = 3 and dom.CTIPODIRECCION = 0 and doc.ccancelado = 0
            //                                  and len (CTELEFONO1)! = 0
            //                                  and (doc.CFECHA between cast(DATEADD(DAY,-3,GETDATE())as date) and CAST(GETDATE() as Date))
            //                                  and doc.CIDCLIENTEPROVEEDOR NOT IN (SELECT DOC.CIDCLIENTEPROVEEDOR FROM ReportesAmsa.dbo.ENCUESTAS 
            //                                  INNER JOIN ADAMSACONTPAQI.DBO.ADMDOCUMENTOS AS DOC
            //                                  ON ENCUESTAS.IDDOCUMENTO = DOC.CIDDOCUMENTO
            //                                  WHERE FECHA >= CAST(DATEADD(DAY, -90, GETDATE()) AS DATE)  and contesto = 'Si')
            //                                  group by doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
            //                                  dom.ctelefono1, dom.ctelefono2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE", ConectionCompac);

            SqlCommand cmd = new SqlCommand(@"with consulta1 as(
select doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
dom.ctelefono1, dom.ctelefono2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE, doc.cidclienteproveedor
from admClientes as cli
INNER JOIN admDomicilios as dom ON cli.cidclienteproveedor = dom.CIDCATALOGO
INNER JOIN admdocumentos as doc ON  cli.CIDCLIENTEPROVEEDOR = doc.CIDCLIENTEPROVEEDOR
INNER JOIN admMovimientos as mov ON doc.CIDDOCUMENTO = mov.cidDocumento
INNER JOIN ReportesAMSA.dbo.folios as folio ON mov.CIDALMACEN = folio.idalmacen
where doc.CIDDOCUMENTODE in (3, 4) and dom.ctipocatalogo = 1 and dom.CTIPODIRECCION = 0
and (doc.CFECHA between cast(DATEADD(DAY,-3,GETDATE())as date) and CAST(GETDATE() as Date))
and len (CTELEFONO1)! = 0
group by doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
dom.ctelefono1, dom.ctelefono2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE, doc.cidclienteproveedor),

id_Clientes as (
SELECT DOC.CIDCLIENTEPROVEEDOR FROM ReportesAmsa.dbo.ENCUESTAS 
INNER JOIN ADAMSACONTPAQI.DBO.ADMDOCUMENTOS AS DOC
ON ENCUESTAS.IDDOCUMENTO = DOC.CIDDOCUMENTO
WHERE FECHA >= CAST(DATEADD(DAY, -90, GETDATE()) AS DATE)  and contesto = 'Si')

select consulta1.CIDDOCUMENTO, consulta1.CSERIEDOCUMENTO, consulta1.CFOLIO, consulta1.CFECHA, consulta1.CRAZONSOCIAL, 
consulta1.ctelefono1, consulta1.ctelefono2, consulta1.cidalmacen, consulta1.sucnom, consulta1.CCODIGOCLIENTE 
from consulta1
left join id_clientes 
on consulta1.cidclienteproveedor = id_clientes.CIDCLIENTEPROVEEDOR
where id_clientes.cidclienteproveedor is  null", ConectionCompac);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            ConectionCompac.Close();
        }

        private void LlenaCombo()
        {
            ConectionAmsa.Open();
            SqlCommand sc = new SqlCommand("Select idalmacen, sucnom from folios where idalmacen NOT IN (19, 16, 18, 0, 17)", ConectionAmsa);
            SqlDataReader reader;
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("sucursal", typeof(string));

            dt.Load(reader);

            metroComboBox1.ValueMember = "idalmacen";
            metroComboBox1.DisplayMember = "sucnom";
            metroComboBox1.DataSource = dt;

            ConectionAmsa.Close();

        }

        private void btn_Seleccionar_Click(object sender, EventArgs e)
        {
            //IdPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value.ToString();
            IDocumento = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdDocumento"].Value.ToString();
            NombreCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
            NSucursal = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Sucursal"].Value.ToString();
            Phone1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Telefono1"].Value.ToString();
            Phone2 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Telefono2"].Value.ToString();
            SerieDocumento = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Documento"].Value.ToString();
            CFolio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Folio"].Value.ToString();
            CIdAlmacen = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Almacen"].Value.ToString();
            CodigoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CodCliente"].Value.ToString();
            CFecha = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Fecha"].Value.ToString();
            
            using (Encuesta.Preguntas EA = new Encuesta.Preguntas())
            {
                EA.ShowDialog();
            }

            if (materialCheckBox1.Checked)
            {
                label1.Enabled = false;
                metroComboBox1.Enabled = false;
                dt.Rows.Clear();
                cargarGrid();
            }
            else
            {
                label1.Enabled = true;
                metroComboBox1.Enabled = true;
                dt.Rows.Clear();
                CargarSucursal();
            }

            ObtenValores();
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Rows.Clear();

            //SqlCommand cmd = new SqlCommand(@"select doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
            //                                    dom.ctelefono1, dom.ctelefono2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE
            //                                    from admDocumentos as doc
            //                                    INNER JOIN admDomicilios as dom ON doc.CIDDOCUMENTO = dom.CIDCATALOGO
            //                                    INNER JOIN admMovimientos as mov ON doc.CIDDOCUMENTO = mov.cidDocumento
            //                                    INNER JOIN ReportesAMSA.dbo.folios as folio ON mov.CIDALMACEN = folio.idalmacen
            //                                    INNER JOIN admClientes as cli ON  doc.CIDCLIENTEPROVEEDOR = cli.CIDCLIENTEPROVEEDOR
            //                                    where doc.CIDDOCUMENTODE = 4 and dom.ctipocatalogo = 3 and dom.CTIPODIRECCION = 0 and doc.ccancelado = 0
            //                                    and len (CTELEFONO1)! = 0
            //                                    and (doc.CFECHA between cast(DATEADD(DAY,-3,GETDATE())as date) and CAST(GETDATE() as Date))
            //                                    and idalmacen = '" + metroComboBox1.SelectedValue.ToString() + @"'
            //                                    and doc.CIDCLIENTEPROVEEDOR NOT IN (SELECT DOC.CIDCLIENTEPROVEEDOR FROM ReportesAmsa.dbo.ENCUESTAS 
            //                                    INNER JOIN ADAMSACONTPAQI.DBO.ADMDOCUMENTOS AS DOC
            //                                    ON ENCUESTAS.IDDOCUMENTO = DOC.CIDDOCUMENTO
            //                                    WHERE FECHA >= CAST(DATEADD(DAY, -90, GETDATE()) AS DATE)  and contesto = 'Si')
            //                                    group by doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
            //                                    dom.ctelefono1, dom.ctelefono2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE", ConectionCompac);

            SqlCommand cmd = new SqlCommand(@"with consulta1 as(
select doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
dom.ctelefono1, dom.CTELEFONO2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE, doc.cidclienteproveedor
from admClientes as cli
INNER JOIN admDomicilios as dom ON cli.cidclienteproveedor = dom.CIDCATALOGO
INNER JOIN admdocumentos as doc ON  cli.CIDCLIENTEPROVEEDOR = doc.CIDCLIENTEPROVEEDOR
INNER JOIN admMovimientos as mov ON doc.CIDDOCUMENTO = mov.cidDocumento
INNER JOIN ReportesAMSA.dbo.folios as folio ON mov.CIDALMACEN = folio.idalmacen
where doc.CIDDOCUMENTODE in (3, 4) and dom.ctipocatalogo = 1 and dom.CTIPODIRECCION = 0
and len (CTELEFONO1)! = 0 and idalmacen = '" + metroComboBox1.SelectedValue.ToString() + @"'
and (doc.CFECHA between cast(DATEADD(DAY,-3,GETDATE())as date) and CAST(GETDATE() as Date))
group by doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
dom.ctelefono1, dom.ctelefono2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE, doc.cidclienteproveedor),

id_Clientes as (
SELECT DOC.CIDCLIENTEPROVEEDOR FROM ReportesAmsa.dbo.ENCUESTAS 
INNER JOIN ADAMSACONTPAQI.DBO.ADMDOCUMENTOS AS DOC
ON ENCUESTAS.IDDOCUMENTO = DOC.CIDDOCUMENTO
WHERE FECHA >= CAST(DATEADD(DAY, -90, GETDATE()) AS DATE)  and contesto = 'Si')

select consulta1.CIDDOCUMENTO, consulta1.CSERIEDOCUMENTO, consulta1.CFOLIO, consulta1.CFECHA, consulta1.CRAZONSOCIAL, 
consulta1.ctelefono1, consulta1.ctelefono2, consulta1.cidalmacen, consulta1.sucnom, consulta1.CCODIGOCLIENTE 
from consulta1
left join id_clientes 
on consulta1.cidclienteproveedor = id_clientes.CIDCLIENTEPROVEEDOR
where id_clientes.cidclienteproveedor is  null", ConectionCompac);


            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            ConectionCompac.Close();
        }

        private void CargarSucursal()
        {
            dt.Rows.Clear();

            SqlCommand cmd = new SqlCommand(@"with consulta1 as(
select doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
dom.ctelefono1, dom.CTELEFONO2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE, doc.cidclienteproveedor
from admClientes as cli
INNER JOIN admDomicilios as dom ON cli.cidclienteproveedor = dom.CIDCATALOGO
INNER JOIN admdocumentos as doc ON  cli.CIDCLIENTEPROVEEDOR = doc.CIDCLIENTEPROVEEDOR
INNER JOIN admMovimientos as mov ON doc.CIDDOCUMENTO = mov.cidDocumento
INNER JOIN ReportesAMSA.dbo.folios as folio ON mov.CIDALMACEN = folio.idalmacen
where doc.CIDDOCUMENTODE in (3, 4) and dom.ctipocatalogo = 1 and dom.CTIPODIRECCION = 0
and len (CTELEFONO1)! = 0 and idalmacen = '" + metroComboBox1.SelectedValue.ToString() + @"'
and (doc.CFECHA between cast(DATEADD(DAY,-3,GETDATE())as date) and CAST(GETDATE() as Date))
group by doc.CIDDOCUMENTO, doc.CSERIEDOCUMENTO, doc.CFOLIO, doc.CFECHA, doc.CRAZONSOCIAL, 
dom.ctelefono1, dom.ctelefono2, mov.cidalmacen, folio.sucnom, cli.CCODIGOCLIENTE, doc.cidclienteproveedor),

id_Clientes as (
SELECT DOC.CIDCLIENTEPROVEEDOR FROM ReportesAmsa.dbo.ENCUESTAS 
INNER JOIN ADAMSACONTPAQI.DBO.ADMDOCUMENTOS AS DOC
ON ENCUESTAS.IDDOCUMENTO = DOC.CIDDOCUMENTO
WHERE FECHA >= CAST(DATEADD(DAY, -90, GETDATE()) AS DATE)  and contesto = 'Si')

select consulta1.CIDDOCUMENTO, consulta1.CSERIEDOCUMENTO, consulta1.CFOLIO, consulta1.CFECHA, consulta1.CRAZONSOCIAL, 
consulta1.ctelefono1, consulta1.ctelefono2, consulta1.cidalmacen, consulta1.sucnom, consulta1.CCODIGOCLIENTE 
from consulta1
left join id_clientes 
on consulta1.cidclienteproveedor = id_clientes.CIDCLIENTEPROVEEDOR
where id_clientes.cidclienteproveedor is  null", ConectionCompac);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
            ConectionCompac.Close();
        }

        private void metroComboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //IdPasa = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Id"].Value.ToString();
            IDocumento = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["IdDocumento"].Value.ToString();
            NombreCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Nombre"].Value.ToString();
            NSucursal = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Sucursal"].Value.ToString();
            Phone1 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Telefono1"].Value.ToString();
            Phone2 = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Telefono2"].Value.ToString();
            SerieDocumento = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Documento"].Value.ToString();
            CFolio = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Folio"].Value.ToString();
            CIdAlmacen = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Almacen"].Value.ToString();
            CodigoCliente = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CodCliente"].Value.ToString();
            CFecha = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Fecha"].Value.ToString();

            using (Encuesta.Preguntas EA = new Encuesta.Preguntas())
            {
                EA.ShowDialog();
            }

            if (materialCheckBox1.Checked)
            {
                label1.Enabled = false;
                metroComboBox1.Enabled = false;
                dt.Rows.Clear();
                cargarGrid();
            }
            else
            {
                label1.Enabled = true;
                metroComboBox1.Enabled = true;
                dt.Rows.Clear();
                CargarSucursal();
            }

            ObtenValores();
        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialCheckBox1.Checked)
            {
                label1.Enabled = false;
                metroComboBox1.Enabled = false;
                cargarGrid();
            }
            else
            {
                label1.Enabled = true;
                metroComboBox1.Enabled = true;
                CargarSucursal();
            }
        }
    }
}
