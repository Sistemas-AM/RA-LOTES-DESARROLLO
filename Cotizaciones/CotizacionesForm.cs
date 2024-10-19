using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static Cotizaciones.DBHelper;

namespace Cotizaciones
{
    public partial class CotizacionesForm : Form
    {
        private bool isAdminpaq = false;
        private string sucursal;
        private List<Cotizacion> Cotizaciones = new List<Cotizacion>();
        private static List<Cotizacion> _Cotizacion = new List<Cotizacion>();
        public static List<Cotizacion> Cotizacion
        {
            get
            {
                return _Cotizacion;
            }
        }
        public CotizacionesForm(string sucursal, bool isAdminpaq)
        {
            InitializeComponent();
            this.isAdminpaq = isAdminpaq;
            this.sucursal = sucursal;
        }

        private void CotizacionesForm_Load(object sender, EventArgs e)
        {
            setListViewColumns();
            Cotizaciones = getCotizaciones(sucursal, isAdminpaq);
            bindCotizacionesToList(Cotizaciones);
        }

        private void bindCotizacionesToList(List<Cotizacion> Cotizaciones)
        {
            listView1.BeginUpdate();
            using (var cotizacion = Cotizaciones.GetEnumerator())
            {
                while (cotizacion.MoveNext())
                {
                    string[] row = { cotizacion.Current.fecha.ToString("dd/MM/yyyy"), cotizacion.Current.folio.ToString(), cotizacion.Current.totcot };
                    ListViewItem listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);
                }
            }
            listView1.EndUpdate();
        }

        private void setListViewColumns()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Fecha", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Folio", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Importe", 100, HorizontalAlignment.Left);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                _Cotizacion = getCotizacion(DateTime.Parse(listView1.SelectedItems[0].Text), Convert.ToInt32(listView1.SelectedItems[0].SubItems[1].Text), isAdminpaq, sucursal);
                this.Close();
            }
            catch (ArgumentOutOfRangeException)
            {

                MessageBox.Show("Asegurate de seleccionar una cotización");
            }
        }
    }
}
