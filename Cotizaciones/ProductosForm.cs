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

namespace Cotizaciones
{
    public partial class ProductosForm : Form
    {
        private List<ProductoCorto> Productos = new List<ProductoCorto>();
        private List<ProductoCorto> ProductosEncontrados = new List<ProductoCorto>();
        private static string _CCODIGOPRODUCTO;
        public static string CCODIGOPRODUCTO
        {
            get
            {
                return _CCODIGOPRODUCTO;
            }
        }
        private static string _CNOMBREPRODUCTO;

        public static string CNOMBREPRODUCTO
        {
            get
            {
                return _CNOMBREPRODUCTO;
            }
        }
        private static string _CPRECIO1;

        public static string CPRECIO1
        {
            get
            {
                return _CPRECIO1;
            }
        }
        private static string _CPESOPRODUCTO;

        public static string CPESOPRODUCTO
        {
            get
            {
                return _CPESOPRODUCTO;
            }
        }
        private static string _CCONTROLEXISTENCIA;

        public static string CCONTROLEXISTENCIA
        {
            get
            {
                return _CCONTROLEXISTENCIA;
            }
        }
        public int idalmacen;
        public ProductosForm(int idalmacen)
        {
            InitializeComponent();
            this.idalmacen = idalmacen;
        }

        private void ProductosForm_Load(object sender, EventArgs e)
        {
            setListViewColumns();
            Productos = getProductos();
            fetchProductsList(Productos);
        }

        private void setListViewColumns()
        {

            listView1.View = View.Details;
            // Add a column with width 20 and left alignment.
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Código", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Nombre", 350, HorizontalAlignment.Left);
            
        }

        private void fetchProductsList(List<ProductoCorto> listaProductos)
        {
            listView1.BeginUpdate();
            using (var cliente = listaProductos.GetEnumerator())
            {
                while (cliente.MoveNext())
                {
                    string[] row = { cliente.Current.CCODIGOPRODUCTO, cliente.Current.CNOMBREPRODUCTO};
                    ListViewItem listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);
                }
            }
            listView1.EndUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buscaProducto();
        }

        private void buscaProducto()
        {
            listView1.Items.Clear(); // clear list items before adding 

            if (textBox1.Text.Equals(""))
            {
                fetchProductsList(Productos);
            }
            else
            {
                ProductosEncontrados = searchProducto(textBox1.Text);
                fetchProductsList(ProductosEncontrados);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Producto Producto = new Producto();
            Producto = getProducto(listView1.SelectedItems[0].Text);
            _CCODIGOPRODUCTO = Producto.CCODIGOPRODUCTO;
            _CNOMBREPRODUCTO = Producto.CNOMBREPRODUCTO;
            _CPRECIO1 = Producto.CPRECIO1;
            _CPESOPRODUCTO = Producto.CPESOPRODUCTO;
            _CCONTROLEXISTENCIA = getExistencias(Producto.CIDPRODUCTO, idalmacen).ToString();
            VariablesCompartidas.cancelado = "1";
            this.Close();
        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                Producto Producto = new Producto();
                Producto = getProducto(listView1.SelectedItems[0].Text);
                _CCODIGOPRODUCTO = Producto.CCODIGOPRODUCTO;
                _CNOMBREPRODUCTO = Producto.CNOMBREPRODUCTO;
                _CPRECIO1 = Producto.CPRECIO1;
                _CPESOPRODUCTO = Producto.CPESOPRODUCTO;
                _CCONTROLEXISTENCIA = getExistencias(Producto.CIDPRODUCTO, idalmacen).ToString();
                this.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VariablesCompartidas.cancelado = "0";
            this.Close();
        }
    }
}