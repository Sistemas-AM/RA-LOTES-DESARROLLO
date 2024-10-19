using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CargaArchivos
{
    class Importar
    {
        OleDbConnection conn;
        OleDbDataAdapter MyDataAdapter;
        DataTable dt;

        public void importarExcel(DataGridView dgv, String nombreHoja)
        {
            String ruta = "";
            try
            {
                OpenFileDialog openfile1 = new OpenFileDialog();
                openfile1.Filter = "Excel Files |*.xlsx";
                openfile1.Title = "Seleccione el archivo de Excel";
                if (openfile1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openfile1.FileName.Equals("") == false)
                    {
                        ruta = openfile1.FileName;
                    }
                }
                //F3 as Almacen, F4 as PzasRec, F5 as Precio, F6 as Neto, F9 as Impuesto, F11 as Total, F12 as KPT, F13 as KgTeo, F14 as PzasFac, F15 as DifPzas, F16 as KgFac/Pzas,  F17 as KgFac, F18 as DifKg, F19 as ImpTotal
                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + ruta + ";Extended Properties='Excel 12.0 Xml;HDR=No'");
                MyDataAdapter = new OleDbDataAdapter("Select F2 as Productos, F20 as Nombre, F3 as Almacen, F4 as PzasRe, F5 as Precio, F6 as Neto, F9 as Impuesto, F11 as Total, F12 as KPT, F13 as KgTeo, F14 as PzasFac, F15 as DifPzas, F16 as KgFacPzas,  F17 as KgFac, F18 as DifKg, F19 as ImpTotal from [" + nombreHoja + "$A18:T317]", conn);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                dgv.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
