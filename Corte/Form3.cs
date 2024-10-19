using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Corte.DBHelper;

namespace Corte
{
    public partial class Form3 : Form
    {
        public Form3(Elaboro elaboro, string codSucursal, string sucursal)
        {
            InitializeComponent();
            List<InventarioCorte> Inventario = getInventario(codSucursal, DateTime.Parse(elaboro.fecha));
            DataSet ds = new DataSet();


            DataTable dtInventario = new DataTable();
            dtInventario.Columns.Add("CCODIGOPRODUCTO", typeof(string));
            dtInventario.Columns.Add("CNOMBREPRODUCTO", typeof(string));
            dtInventario.Columns.Add("venta", typeof(float));
            dtInventario.Columns.Add("devs", typeof(float));
            dtInventario.Columns.Add("entrada", typeof(float));
            dtInventario.Columns.Add("salida", typeof(float));
            dtInventario.Columns.Add("compras", typeof(float));
            dtInventario.Columns.Add("existencia", typeof(float));


            foreach (InventarioCorte inventario in Inventario)
            {
                dtInventario.Rows.Add(inventario.CCODIGOPRODUCTO, inventario.CNOMBREPRODUCTO,
                                      inventario.venta, inventario.devs, inventario.entrada,
                                      inventario.salida, inventario.compras, inventario.existencia);
            }
            DataTable dtElaboro = new DataTable();
            dtElaboro.Columns.Add("elaboro", typeof(string));
            dtElaboro.Columns.Add("fecha", typeof(string));
            dtElaboro.Columns.Add("sucursal", typeof(string));

            dtElaboro.Rows.Add(elaboro.elaboro, elaboro.fecha, sucursal);


            ds.Tables.Add(dtElaboro);
            ds.Tables.Add(dtInventario);

            ReportDocument rd = new ReportDocument();
            rd.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Corte\CrystalReport3.rpt");
            rd.Database.Tables[0].SetDataSource(ds.Tables[0]);
            rd.Subreports["InformeInventario"].Database.Tables[0].SetDataSource(ds.Tables[1]);
            crystalReportViewer1.ReportSource = rd;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();
        }
    }
}
