using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Cotizaciones.DBHelper;
//using static Cotizaciones.PDFHelper;

namespace Cotizaciones
{
    public partial class ClientesForm : Form
    {
        private bool isAdminpaq = false;
        private List<Cliente> Clientes = new List<Cliente>();
        private List<Cliente> ClientesEncontrados = new List<Cliente>();
        private static string _CCODIGOCLIENTE;
        public static string CCODIGOCLIENTE
        {
            get
            {
                return _CCODIGOCLIENTE;
            }
        }
        private static string _CRAZONSOCIAL;
        public static string CRAZONSOCIAL
        {
            get
            {
                return _CRAZONSOCIAL;
            }
        }
        private static string _CRFC;
        public static string CRFC
        {
            get
            {
                return _CRFC;
            }
        }
        private static string _CNOMBRECALLE;
        public static string CNOMBRECALLE
        {
            get
            {
                return _CNOMBRECALLE;
            }
        }
        private static string _CNUMEROEXTERIOR;
        public static string CNUMEROEXTERIOR
        {
            get
            {
                return _CNUMEROEXTERIOR;
            }
        }
        private static string _CCOLONIA;
        public static string CCOLONIA
        {
            get
            {
                return _CCOLONIA;
            }
        }
        private static string _CCODIGOPOSTAL;
        public static string CCODIGOPOSTAL
        {
            get
            {
                return _CCODIGOPOSTAL;
            }
        }
        private static string _CTELEFONO01;
        public static string CTELEFONO01
        {
            get
            {
                return _CTELEFONO01;
            }
        }
        private static string _CEMAIL;
        public static string CEMAIL
        {
            get
            {
                return _CEMAIL;
            }
        }
        private static string _CCIUDAD;
        public static string CCIUDAD
        {
            get
            {
                return _CCIUDAD;
            }
        }
        private static string _CESTADO;
        public static string CESTADO
        {
            get
            {
                return _CESTADO;
            }
        }
        private static string _CPAIS;
        public static string CPAIS
        {
            get
            {
                return _CPAIS;
            }
        }
        public ClientesForm(bool isAdminpaq)
        {
            InitializeComponent();
            this.isAdminpaq = isAdminpaq;
        }

        private void ClientesForm_Load(object sender, EventArgs e)
        {
            setListViewColumns();
            if (isAdminpaq)
            {
                Clientes = getClientes(true);
                fetchClientList(Clientes);
            }
            else
            {
                Clientes = getClientes(false);
                fetchClientList(Clientes);
            }

        }

        private void fetchClientList(List<Cliente> Clientes)
        {
            listView1.BeginUpdate();
            using (var cliente = Clientes.GetEnumerator())
            {
                while (cliente.MoveNext())
                {

                    if (isAdminpaq)
                    {
                        string[] row = { cliente.Current.CCODIGOCLIENTE, cliente.Current.CRAZONSOCIAL, cliente.Current.CRFC };
                        ListViewItem listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                    }
                    else
                    {
                        string[] row = { cliente.Current.CRAZONSOCIAL, cliente.Current.CRFC };
                        ListViewItem listViewItem = new ListViewItem(row);
                        listView1.Items.Add(listViewItem);
                    }
                    
                }
            }
            listView1.EndUpdate();
        }

        private void setListViewColumns()
        {
            listView1.View = View.Details;
            // Add a column with width 20 and left alignment.
            listView1.FullRowSelect = true;
            if (isAdminpaq)
            {
                listView1.Columns.Add("Clave", 70, HorizontalAlignment.Left);

            }
            listView1.Columns.Add("Razón Social", 350, HorizontalAlignment.Left);
            listView1.Columns.Add("RFC", 100, HorizontalAlignment.Left);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            buscaCliente();
        }

        private void buscaCliente()
        {
            listView1.Items.Clear(); // clear list items before adding 

            if (textBox1.Text.Equals(""))
            {
                fetchClientList(Clientes);
            }
            else
            {
                ClientesEncontrados = searchCliente(isAdminpaq, textBox1.Text);
                fetchClientList(ClientesEncontrados);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setDatosDelCliente();
        }

        private void setDatosDelCliente()
        {
            if (listView1.SelectedItems.Count <= 0)
            {

            }
            else
            {
                List<ClienteActual> datosDelCliente = new List<ClienteActual>();
                datosDelCliente = getDatosDelCliente(isAdminpaq?listView1.SelectedItems[0].Text: listView1.SelectedItems[0].SubItems[1].Text, isAdminpaq);
                foreach (ClienteActual cliente in datosDelCliente)
                {
                    if (isAdminpaq)
                    {
                        _CCODIGOCLIENTE = cliente.CCODIGOCLIENTE;

                    }
                    else
                    {
                        _CCODIGOCLIENTE = ".";
                    }
                    _CRAZONSOCIAL = cliente.CRAZONSOCIAL;
                    _CRFC = cliente.CRFC;
                    _CNOMBRECALLE = cliente.CNOMBRECALLE;
                    _CNUMEROEXTERIOR = cliente.CNUMEROEXTERIOR;
                    _CCOLONIA = cliente.CCOLONIA;
                    _CCODIGOPOSTAL = cliente.CCODIGOPOSTAL;
                    _CTELEFONO01 = cliente.CTELEFONO01;
                    _CEMAIL = cliente.CEMAIL;
                    _CCIUDAD = cliente.CCIUDAD;
                    _CESTADO = cliente.CESTADO;
                    _CPAIS = cliente.CPAIS;
                }
                //label2.Text = _CCODIGOCLIENTE;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
