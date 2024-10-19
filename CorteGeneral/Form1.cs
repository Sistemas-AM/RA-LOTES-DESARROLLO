using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using static CorteGeneral.DBHelper;
using System.Data.SqlClient;
using System.Data;

namespace CorteGeneral
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;

        string MAT = "";
        string IGS = "";
        string LP = "";
        string SP = "";
        string PLA = "";
        string VE = "";
        string ME = "";

        private List<string> sucursales = getSucursales();
        Excel.Application excel;
        Excel.Workbook libro;
        Excel.Worksheet hoja;
        Excel.Range rango;
        private string rutaPlantillaDelReporte = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\CorteGeneral\PLANTILLA CORTE GENERAL ACEROS.xls";
        private string rutaReporte = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\CorteGeneral\CORTE GENERAL ACEROS ";
        private int columnaInicialFacturaFolios = 0;
        private int columnaInicialNotaFolios = 0;
        private int columnaInicialSistemaEfectivos = 0;
        private int columnaInicialSistemaCheques = 0;
        private int columnaInicialSistemaVales = 0;
        private int columnaInicialSistemaFacturas = 0;
        private int columnaInicialResumen = 0;
        private int renglonInicialResumen = 0;
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sacaAnt();
            sacaIGS();
            sacaLP();
            sacaSP();
            sacaPla();
            sacaVE();
            sacaME();

            llenaHoja1();
            llenaHoja2();
            llenaHoja3();

        }

        private void sacaAnt()
        {
            
            //sqlConnection1.Open();

            DateTime fecha = DateTime.Parse(dateTimePicker1.Value.ToString());
            SqlCommand cmd = new SqlCommand("spObtieneFacNot", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sucursal", "MAT");
            cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("MM/dd/yyyy"));
            //cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                MAT = reader["factCredi"].ToString() + " - "+ reader["Factura"].ToString() + " - "+ reader["Notas"].ToString() ;

            }
            sqlConnection1.Close();

        }


        private void sacaIGS()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Value.ToString());
            SqlCommand cmd = new SqlCommand("spObtieneFacNot", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sucursal", "IGS");
            cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("MM/dd/yyyy"));
            //cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                IGS = reader["factCredi"].ToString() + " - " + reader["Factura"].ToString() + " - " + reader["Notas"].ToString();

            }
            sqlConnection1.Close();

        }



        private void sacaLP()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Value.ToString());
            SqlCommand cmd = new SqlCommand("spObtieneFacNot", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sucursal", "LP");
            cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("MM/dd/yyyy"));
            //cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                LP = reader["factCredi"].ToString() + " - " + reader["Factura"].ToString() + " - " + reader["Notas"].ToString();

            }
            sqlConnection1.Close();

        }


        private void sacaSP()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Value.ToString());
            SqlCommand cmd = new SqlCommand("spObtieneFacNot", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sucursal", "SP");
            cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("MM/dd/yyyy"));
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                SP = reader["factCredi"].ToString() + " - " + reader["Factura"].ToString() + " - " + reader["Notas"].ToString();

            }
            sqlConnection1.Close();
        }


        private void sacaPla()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Value.ToString());
            SqlCommand cmd = new SqlCommand("spObtieneFacNot", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sucursal", "PLA");
            cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("MM/dd/yyyy"));
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                PLA = reader["factCredi"].ToString() + " - " + reader["Factura"].ToString() + " - " + reader["Notas"].ToString();

            }
            sqlConnection1.Close();

        }


        private void sacaVE()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Value.ToString());
            SqlCommand cmd = new SqlCommand("spObtieneFacNot", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sucursal", "VE");
            cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("MM/dd/yyyy"));
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                VE = reader["factCredi"].ToString() + " - " + reader["Factura"].ToString() + " - " + reader["Notas"].ToString();

            }
            sqlConnection1.Close();

        }


        private void sacaME()
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Value.ToString());
            SqlCommand cmd = new SqlCommand("spObtieneFacNot", sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Sucursal", "ME");
            cmd.Parameters.AddWithValue("@Fecha", fecha.ToString("MM/dd/yyyy"));
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                ME = reader["factCredi"].ToString() + " - " + reader["Factura"].ToString() + " - " + reader["Notas"].ToString();

            }
            sqlConnection1.Close();

        }

        private void llenaHoja3()
        {
            openExcel(3);
            foreach (string sucursal in sucursales)
            {
                setColumnasIniciales(sucursal);
                List<ResumenSucursal> resumenSucursal = getResumenSucursal(sucursal, dateTimePicker1.Value);
                if (resumenSucursal.Count > 0)
                {
                    hoja.Cells[renglonInicialResumen, columnaInicialResumen]         = resumenSucursal[0].cheqDev;
                    //hoja.Cells[renglonInicialResumen+1, columnaInicialResumen]       = resumenSucursal[0].ant;
                    hoja.Cells[renglonInicialResumen+2, columnaInicialResumen]       = resumenSucursal[0].devsf;
                    hoja.Cells[renglonInicialResumen+3, columnaInicialResumen]       = resumenSucursal[0].devsn;
                    hoja.Cells[renglonInicialResumen+4, columnaInicialResumen]       = resumenSucursal[0].comisionxdevolucion;
                    hoja.Cells[renglonInicialResumen+5, columnaInicialResumen]       = resumenSucursal[0].cobranza;
                    hoja.Cells[renglonInicialResumen + 1, columnaInicialResumen+3]   = resumenSucursal[0].pagCheqDE;
                    hoja.Cells[renglonInicialResumen + 2, columnaInicialResumen + 3] = resumenSucursal[0].pagCheqDC;
                    hoja.Cells[renglonInicialResumen + 3, columnaInicialResumen + 3] = resumenSucursal[0].dolares;
                    hoja.Cells[renglonInicialResumen + 4, columnaInicialResumen + 3] = resumenSucursal[0].antEfect;
                    hoja.Cells[renglonInicialResumen + 5, columnaInicialResumen + 3] = resumenSucursal[0].antcheq;
                    hoja.Cells[renglonInicialResumen + 6, columnaInicialResumen + 3] = resumenSucursal[0].notasDeCredito;
                }
            }
            closeExcel();
        }

        private void llenaHoja2()
        {
            openExcel(2);
            foreach (string sucursal in sucursales)
            {
                setColumnasIniciales(sucursal);
                List<EfectivoCaja> efectivos = getEfectivoCaja(sucursal, dateTimePicker1.Value);
                List<Cheque> cheques = getCheques(sucursal, dateTimePicker1.Value);
                List<Vale> vales = getVales(sucursal, dateTimePicker1.Value);
                List<SistemaFactura> facturas = getSistemaFacturas(sucursal, dateTimePicker1.Value);
                for (int i = 0; i < 14; i++)
                {
                    hoja.Cells[i + 7, columnaInicialSistemaEfectivos - 2] = 0;
                    foreach (EfectivoCaja efectivo in efectivos)
                    {
                        try
                        {
                            if (efectivo.desgloce.ToString().Equals((hoja.Cells[i + 7, columnaInicialSistemaEfectivos] as Excel.Range).Value.ToString()))
                            {
                                hoja.Cells[i + 7, columnaInicialSistemaEfectivos - 2] = efectivo.cantidad;
                            }
                        }
                        catch (Exception)
                        {

                            
                        }
                    }
                }

                for (int i = 0; i < cheques.Count; i++)
                {
                    hoja.Cells[i + 7, columnaInicialSistemaCheques] = cheques[i].concepto;
                    hoja.Cells[i + 7, columnaInicialSistemaCheques + 3] = cheques[i].total;
                }

                for (int i = 0; i < vales.Count; i++)
                {
                    hoja.Cells[i + 28, columnaInicialSistemaVales] = vales[i].concepto;
                    hoja.Cells[i + 28, columnaInicialSistemaVales + 5] = vales[i].total;
                }
                for (int i = 0; i < facturas.Count; i++)
                {
                    hoja.Cells[i + 28, columnaInicialSistemaFacturas]       = facturas[i].concepto;
                    hoja.Cells[i + 28, columnaInicialSistemaFacturas + 1]   = facturas[i].proveedor;
                    hoja.Cells[i + 28, columnaInicialSistemaFacturas + 2]   = facturas[i].nofac;
                    hoja.Cells[i + 28, columnaInicialSistemaFacturas + 3]   = facturas[i].total;
                }

            }
        }



        private void llenaHoja1()
        {
            openExcel(1, true);
            hoja.Cells[2, 2]    = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            hoja.Cells[3,3]     = MAT;
            hoja.Cells[3, 15]   = IGS;
            hoja.Cells[3, 26]   = LP;
            hoja.Cells[3, 37]   = SP;
            hoja.Cells[3, 48]   = PLA;
            hoja.Cells[3, 58] = VE;
            hoja.Cells[3, 68] = ME;

            foreach (string sucursal in sucursales)
            {
                setColumnasIniciales(sucursal);
                List<Factura> facturas = getFacturas(sucursal, dateTimePicker1.Value);
                List<Factura> notasDeCredito = getNotasDeCredito(sucursal, dateTimePicker1.Value);
                for (int i = 0; i < facturas.Count; i++)
                {
                    hoja.Cells[i + 6, columnaInicialFacturaFolios]      = facturas[i].factura;
                    hoja.Cells[i + 6, columnaInicialFacturaFolios + 1]  = facturas[i].importe;
                    hoja.Cells[i + 6, columnaInicialFacturaFolios + 2]  = facturas[i].credito;
                    hoja.Cells[i + 6, columnaInicialFacturaFolios + 3]  = facturas[i].debito;
                    hoja.Cells[i + 6, columnaInicialFacturaFolios + 4]  = facturas[i].creditos;
                }
                for (int i = 0; i < notasDeCredito.Count; i++)
                {
                    hoja.Cells[i + 6, columnaInicialNotaFolios]     = notasDeCredito[i].factura;
                    hoja.Cells[i + 6, columnaInicialNotaFolios + 1] = notasDeCredito[i].importe;
                    hoja.Cells[i + 6, columnaInicialNotaFolios + 2] = notasDeCredito[i].credito;
                    hoja.Cells[i + 6, columnaInicialNotaFolios + 3] = notasDeCredito[i].debito;
                }

            }
        }

        private void setColumnasIniciales(string sucursal)
        {
            switch (sucursal)
            {
                case "IGS":
                    columnaInicialFacturaFolios     = 13;
                    columnaInicialNotaFolios        = columnaInicialFacturaFolios + 6;
                    columnaInicialSistemaEfectivos  = 18;
                    columnaInicialSistemaCheques    = columnaInicialSistemaEfectivos + 6;
                    columnaInicialSistemaVales      = 16;
                    columnaInicialSistemaFacturas   = 24;
                    columnaInicialResumen           = 9;
                    renglonInicialResumen           = 11;
                    break;
                case "MAT":
                    columnaInicialFacturaFolios     = 2;
                    columnaInicialNotaFolios        = columnaInicialFacturaFolios + 6;
                    columnaInicialSistemaEfectivos  = 4;
                    columnaInicialSistemaCheques    = columnaInicialSistemaEfectivos + 6;
                    columnaInicialSistemaVales      = 2;
                    columnaInicialSistemaFacturas   = 10;
                    columnaInicialResumen           = 3;
                    renglonInicialResumen           = 11;
                    break;
                case "LP ":
                    columnaInicialFacturaFolios     = 24;
                    columnaInicialNotaFolios        = columnaInicialFacturaFolios + 6;
                    columnaInicialSistemaEfectivos  = 32;
                    columnaInicialSistemaCheques    = columnaInicialSistemaEfectivos + 6;
                    columnaInicialSistemaVales      = 30;
                    columnaInicialSistemaFacturas   = 38;
                    columnaInicialResumen           = 3;
                    renglonInicialResumen           = 39;
                    break;
                case "SP ":
                    columnaInicialFacturaFolios     = 35;
                    columnaInicialNotaFolios        = columnaInicialFacturaFolios + 6;
                    columnaInicialSistemaEfectivos  = 46;
                    columnaInicialSistemaCheques    = columnaInicialSistemaEfectivos + 6;
                    columnaInicialSistemaVales      = 44;
                    columnaInicialSistemaFacturas   = 52;
                    columnaInicialResumen           = 9;
                    renglonInicialResumen           = 39;
                    break;
                case "PLA":
                    columnaInicialFacturaFolios     = 46;
                    columnaInicialNotaFolios        = columnaInicialFacturaFolios + 6;
                    columnaInicialSistemaEfectivos  = 60;
                    columnaInicialSistemaCheques    = columnaInicialSistemaEfectivos + 6;
                    columnaInicialSistemaVales      = 58;
                    columnaInicialSistemaFacturas   = 66;
                    columnaInicialResumen           = 3;
                    renglonInicialResumen           = 65;
                    break;

                case "VE ":
                    columnaInicialFacturaFolios = 57;
                    columnaInicialNotaFolios = columnaInicialFacturaFolios + 6;
                    columnaInicialSistemaEfectivos = 74;
                    columnaInicialSistemaCheques = columnaInicialSistemaEfectivos + 6;
                    columnaInicialSistemaVales = 72;
                    columnaInicialSistemaFacturas = 80;
                    columnaInicialResumen = 9;
                    renglonInicialResumen = 65;
                    break;

                case "ME ":
                    columnaInicialFacturaFolios = 68;
                    columnaInicialNotaFolios = columnaInicialFacturaFolios + 6;
                    columnaInicialSistemaEfectivos = 88;
                    columnaInicialSistemaCheques = columnaInicialSistemaEfectivos + 6;
                    columnaInicialSistemaVales = 86;
                    columnaInicialSistemaFacturas = 94;
                    columnaInicialResumen = 15;
                    renglonInicialResumen = 11;
                    break;


            }
        }

        private void closeExcel()
        {
            excel.DisplayAlerts = false;
            libro.Save();
            libro.Close(0);
            excel.Quit();
            DialogResult AbrirExcel = MessageBox.Show("Abrir el archivo", "Abrir", MessageBoxButtons.YesNo);
            if (AbrirExcel == DialogResult.Yes)
            {
                excel.Visible = true;
                excel.Workbooks.Open(rutaReporte + dateTimePicker1.Value.ToString("dd-MM-yy") + ".xls");
            }
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
        }

        private void openExcel(int numHoja, bool first = false)
        {
            if (first)
            {
                System.IO.File.Copy(rutaPlantillaDelReporte, rutaReporte + dateTimePicker1.Value.ToString("dd-MM-yy") + ".xls", true);
                excel   = new Excel.Application();
                libro   = null;
                hoja    = null;
                rango   = null;
                libro   = excel.Workbooks.Open(rutaReporte + dateTimePicker1.Value.ToString("dd-MM-yy") + ".xls");
            }
            hoja = libro.Worksheets[numHoja];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
