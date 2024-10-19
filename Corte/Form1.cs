using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using static Corte.DBHelper;

namespace Corte
{
    public partial class Form1 : Form
    {
        private List<Sucursal> Sucursales = DBHelper.getSucursales();
        private List<string> denominaciones = new List<string>(new string[] { "1000", "500", "200", "100", "50", "20", "10", "5", "2", "1", "0.5", "0.2", "0.1" });
        private List<Caja> Cajas;
        private float GeneralChDevTD = 0;
        private float GeneralChDevTC = 0;
        private float totalOtroEfectivo = 0;
        private float totalEfectivo = 0;
        private string nombreDeSucursal = "";
        int cerrado = 0;

        SqlConnection sqlAmsa = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
        SqlConnection sqlAceros = new SqlConnection(Principal.Variablescompartidas.Aceros);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridViews();
            InitializeComboBoxSucursales();
            textBox4.Text = getTipoCambio().ToString();
            Variablescompartidas.cr = "PRELIMINAR";
            if (Principal.Variablescompartidas.sucural == "AUDITOR")
            {
                comboBoxSucursales.Enabled = true;
            }
            else if (Principal.Variablescompartidas.sucural == "PLANTA                   ")
            {
                comboBoxSucursales.Enabled = true;
            }
            else
            {
                comboBoxSucursales.Enabled = false;
                comboBoxSucursales.Text = Principal.Variablescompartidas.sucursalcorta;
            }
            
        }

        private void InitializeComboBoxSucursales()
        {
            foreach (Sucursal sucursal in Sucursales)
            {
                comboBoxSucursales.Items.Add(sucursal.sucursal);
            }
        }

        private void InitializeDataGridViews()
        {

            foreach (string denominacion in denominaciones)
            {
                dataGridView1.Rows.Add(denominacion);
                dataGridView7.Rows.Add(denominacion);
                dataGridView12.Rows.Add(denominacion);
                dataGridView13.Rows.Add(denominacion);
            }


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nombreSucursal =
                from sucursal in Sucursales
                where sucursal.sucursal == comboBoxSucursales.Text
                select sucursal;
            labelNombreSucursal.Text = "Sucursal " + nombreSucursal.ElementAt(0).sucnom;
            nombreDeSucursal = nombreSucursal.ElementAt(0).sucnom;
        }

        private void buttonGenerar_Click(object sender, EventArgs e)
        {
            if (!comboBoxSucursales.Text.Equals(""))
            {
                Variablescompartidas.cr = "PRELIMINAR";
                totalEfectivo = 0;
                totalOtroEfectivo = 0;
                Consecutivos consecutivos;
                Dictionary<string, float> general = getCorte(comboBoxSucursales.Text, dateTimePicker1.Value);
                textBoxGeneralEfectivo.Text = "0";
                textBoxEfectivo.Text = "0";
                textBox2.Text = "";
                textBoxTotalCorte.Text = "0";
                textBox2.Text = "";
                textBox3.Text = "0";
                textBox5.Text = "0";


                textBoxFacturado.Text = general["facturado"].ToString();
                textBoxNotasDeVenta.Text = general["notas"].ToString();
                textBoxVentasDelDia.Text = general["ventas del dia"].ToString();
                textBoxCreditos.Text = general["credito"].ToString();
                textBoxVentasDeContado.Text = general["ventas de contado"].ToString();
                GeneralChDevTC = float.Parse(getTarjeta("credito", dateTimePicker1.Value, comboBoxSucursales.Text));
                GeneralChDevTD = float.Parse(getTarjeta("debito", dateTimePicker1.Value, comboBoxSucursales.Text));
                textBoxGeneralTarjetaCredito.Text = GeneralChDevTC.ToString();
                textBoxGeneralTarjetaDebito.Text = GeneralChDevTD.ToString();
                cargarTablaAnticipos(getAnticipos(dateTimePicker1.Value, comboBoxSucursales.Text));
                cargarTablaNotasDeCredito(getNotasDeCredito(dateTimePicker1.Value, comboBoxSucursales.Text));
                consecutivos = getConsecutivos(dateTimePicker1.Value, comboBoxSucursales.Text);
                if (consecutivos != null)
                {
                    textBoxConsecutivosFInicial.Text = consecutivos.fact_Inicial.ToString();
                    textBoxConsecutivosFFinal.Text = consecutivos.fact_Final.ToString();
                    textBoxConsecutivosNVInicial.Text = consecutivos.nv_Inicial.ToString();
                    textBoxConsecutivosNVFinal.Text = consecutivos.nv_Final.ToString();
                }

                updateCajas();

                updateCOD();
                textBox13.Text = getDSVentasf(dateTimePicker1.Value, comboBoxSucursales.Text).ToString();
                textBox14.Text = getDSVentasn(dateTimePicker1.Value, comboBoxSucursales.Text).ToString();
                textBox15.Text = ((float.Parse(textBox13.Text.ToString()) + float.Parse(textBox14.Text.ToString())) * 0.05).ToString();
                textBox16.Text = "0";
                textBox17.Text = textBox15.Text;
                textBoxGeneralTransferCobranza.Text = getTransferCobranza(comboBoxSucursales.Text, dateTimePicker1.Value).ToString();
                textBoxCobranzaDia.Text = getCobranzaDia(comboBoxSucursales.Text, dateTimePicker1.Value).ToString();
                updateDevueltos();
                comboBox2.Items.Add(new { Text = "", Value = "" });
                comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
                button6.Enabled = true;
                updateCajaChica();
                updateFacturas();
                updateVales();
                updateCheques();
                updateOtros();
                updateDocumentos();
                updateEfectivoCobranza();
                calculaTotalDelDia();
                
                updateElaboro();
                calculaTotal();
                List<Comdev> comdev = getComdev(comboBoxSucursales.Text, dateTimePicker1.Value);
                if (comdev.Count > 0)
                {
                    textBox16.Text = (comdev[0].no_cobrado - comdev[0].total).ToString();
                    textBox17.Text = comdev[0].total.ToString();
                }

            }
        }

        private void updateElaboro()
        {
            List<Elaboro> elaboro = getElaboro(comboBoxSucursales.Text, dateTimePicker1.Value);
            textBox2.Text = elaboro.Count < 1 ? "" : elaboro[0].elaboro;
            textBox3.Text = elaboro.Count < 1 ? "0" : elaboro[0].dolares.ToString();
            textBox5.Text = elaboro.Count < 1 ? "0" : (float.Parse(elaboro[0].dolares.ToString()) * float.Parse(textBox4.Text)).ToString();
        }

        private void updateEfectivoCobranza()
        {
            dataGridView12.Rows.Clear();
            foreach (string denominacion in denominaciones)
            {
                dataGridView12.Rows.Add(denominacion);
            }
            foreach (EfectivoCobranza cobranza in getEfectivoCobranza(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                foreach (DataGridViewRow row in dataGridView12.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(cobranza.desgloce.ToString()))
                        {
                            row.Cells[1].Value = cobranza.cantidad;
                        }

                    }
                }
            }
            actualizaEfectivoCob();
        }

        private void updateDocumentos()
        {
            List<Documento> doc;
            doc = getDocumentos(comboBoxSucursales.Text, dateTimePicker1.Value);
            dataGridView11.Rows.Clear();
            for (int i = 0; i < doc.Count; i++)
            {
                dataGridView11.Rows.Add();
                dataGridView11.Rows[i].Cells[0].Value = doc[i].concepto;

            }
        }

        private void updateDevueltos()
        {
            List<Devuelto> devueltos;
            devueltos = getDevueltos(comboBoxSucursales.Text, dateTimePicker1.Value);
            dataGridView8.Rows.Clear();
            for (int i = 0; i < devueltos.Count; i++)
            {
                dataGridView8.Rows.Add();
                dataGridView8.Rows[i].Cells[0].Value = devueltos[i].cliente;
                dataGridView8.Rows[i].Cells[1].Value = devueltos[i].cheque.ToString();
                dataGridView8.Rows[i].Cells[2].Value = devueltos[i].impcheq.ToString();
                dataGridView8.Rows[i].Cells[3].Value = devueltos[i].impefec.ToString();
                dataGridView8.Rows[i].Cells[4].Value = devueltos[i].pago.ToString();
                dataGridView8.Rows[i].Cells[5].Value = devueltos[i].comcheq.ToString();
                dataGridView8.Rows[i].Cells[6].Value = devueltos[i].comefe.ToString();
            }
            textBoxImpEfectivo.Text = calculaVarios(3, dataGridView8).ToString();
            textBoxGeneralDevEfectivo.Text = textBoxImpEfectivo.Text;
            textBoxImpCheque.Text = calculaVarios(2, dataGridView8).ToString();
            textBoxGeneralDevCheque.Text = textBoxImpCheque.Text;
            textBoxTotalTC.Text = calculaVarios(5, dataGridView8).ToString();
            textBoxGeneralTarjetaCredito.Text = (GeneralChDevTC + float.Parse(textBoxTotalTC.Text)).ToString();
            textBoxTotalTD.Text = calculaVarios(6, dataGridView8).ToString();
            textBoxGeneralTarjetaDebito.Text = (GeneralChDevTD + float.Parse(textBoxTotalTD.Text)).ToString();
            float total = 0;
            textBoxTotalChequesDevueltos.Text = "0";
            foreach (DataGridViewRow row in dataGridView8.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    total = total + float.Parse(row.Cells[4].Value.ToString());
                }

                if (row.Cells[5].Value != null)
                {
                    total = total + float.Parse(row.Cells[5].Value.ToString());
                }
                if (row.Cells[6].Value != null)
                {
                    total = total + float.Parse(row.Cells[6].Value.ToString());
                }
            }
            textBoxTotalChequesDevueltos.Text = (float.Parse(textBoxTotalChequesDevueltos.Text) + total).ToString();
            textBoxGeneralChDevueltos.Text = textBoxTotalChequesDevueltos.Text;
        }

        private void updateOtros()
        {
            List<Otro> otros;
            otros = getOtros(comboBoxSucursales.Text, dateTimePicker1.Value);
            dataGridView6.Rows.Clear();
            for (int i = 0; i < otros.Count; i++)
            {
                dataGridView6.Rows.Add();
                dataGridView6.Rows[i].Cells[0].Value = otros[i].proveedor;
                dataGridView6.Rows[i].Cells[1].Value = otros[i].cheque;
                dataGridView6.Rows[i].Cells[2].Value = otros[i].importe;

            }
            textBoxTotalOtroCheque.Text = calculaOtros(1).ToString();
            textBoxGeneralCheques.Text = (float.Parse(textBoxGeneralCheques.Text) + calculaOtros(1)).ToString();
            textBoxTotalOtroEfectivo.Text = calculaOtros(2).ToString();
            totalOtroEfectivo = calculaOtros(2);
            textBoxGeneralEfectivo.Text = (totalEfectivo + totalOtroEfectivo).ToString();
            calculaGeneralOtros();
        }

        private void updateCheques()
        {
            List<Cheque> cheques;
            cheques = getCheques(comboBoxSucursales.Text, dateTimePicker1.Value);
            dataGridView5.Rows.Clear();
            for (int i = 0; i < cheques.Count; i++)
            {
                dataGridView5.Rows.Add();
                dataGridView5.Rows[i].Cells[0].Value = cheques[i].concepto;
                dataGridView5.Rows[i].Cells[1].Value = cheques[i].total;

            }
            calculaCheques(dataGridView5);
        }

        private void updateVales()
        {
            List<Vale> vales;
            vales = getVales(comboBoxSucursales.Text, dateTimePicker1.Value);
            dataGridView4.Rows.Clear();
            for (int i = 0; i < vales.Count; i++)
            {
                dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = vales[i].concepto;
                dataGridView4.Rows[i].Cells[1].Value = vales[i].total;

            }
            calculaVales(dataGridView4);
        }

        private void updateFacturas()
        {
            List<Factura> facturas;
            facturas = getFacturas(comboBoxSucursales.Text, dateTimePicker1.Value);
            dataGridView3.Rows.Clear();
            for (int i = 0; i < facturas.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = facturas[i].concepto;
                dataGridView3.Rows[i].Cells[1].Value = facturas[i].proveedor;
                dataGridView3.Rows[i].Cells[2].Value = facturas[i].nofac;
                dataGridView3.Rows[i].Cells[3].Value = facturas[i].total.ToString();
            }
            calculaFacturas();
        }

        private void updateCajaChica()
        {
            dataGridView1.Rows.Clear();
            foreach (string denominacion in denominaciones)
            {
                dataGridView1.Rows.Add(denominacion);
            }
            foreach (CajaChica cajachica in getCajaChica(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(cajachica.desgloce.ToString()))
                        {
                            row.Cells[1].Value = cajachica.cantidad;
                        }

                    }
                }
            }
        }

        private void calculaTotal()
        {
            textBoxTotal.Text =(float.Parse(textBoxGeneralEfectivo.Text) +
                                float.Parse(textBoxGeneralCheques.Text) +
                                float.Parse(textBoxGeneralVales.Text) +
                                float.Parse(textBoxGeneralFacturas.Text) +
                                float.Parse(textBoxGeneralDevEfectivo.Text) +
                                float.Parse(textBoxGeneralDevCheque.Text) +
                                float.Parse(textBoxGeneralNotas.Text) +
                                float.Parse(textBoxGeneralTarjetaCredito.Text) +
                                float.Parse(textBoxGeneralTarjetaDebito.Text) +
                                float.Parse(textBoxGeneralTransferCobranza.Text)+
                                float.Parse(textBox5.Text)).ToString();
        }

        private void calculaTotalDelDia()
        {
            textBoxTotalDia.Text = (float.Parse(textBoxVentasDeContado.Text) +
                                    float.Parse(textBoxGeneralChDevueltos.Text) -
                                    float.Parse(textBox13.Text) -
                                    float.Parse(textBox14.Text) +
                                    float.Parse(textBox17.Text)).ToString();
        }

        private void updateCOD()
        {
            dataGridView2.Rows.Clear();
            List<COD> cods = getCOD(dateTimePicker1.Value, comboBoxSucursales.Text);
            for (int i = 0; i < cods.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = cods[i].folio;
                dataGridView2.Rows[i].Cells[1].Value = cods[i].total;
                dataGridView2.Rows[i].Cells[2].Value = cods[i].tipo;
                dataGridView2.Rows[i].Cells[3].Value = cods[i].fecha.ToShortTimeString();


            }

        }

        private void updateCajas()
        {
            Cajas = getCajas(dateTimePicker1.Value, comboBoxSucursales.Text);
            comboBox2.Items.Clear();
            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Value";
            foreach (CajasCombo caja in getCajasCombo(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                if (caja.elaboro != null)
                {
                    comboBox2.Items.Add(new { Text = "Corte: " + caja.caja + " (" + caja.elaboro.Trim() + ")", Value = caja.caja });
                }
                
            }

            dataGridView7.Rows.Clear();
            foreach (string denominacion in denominaciones)
            {
                dataGridView7.Rows.Add(denominacion);
            }

            foreach (Caja caja in Cajas)
            {
                foreach (DataGridViewRow row in dataGridView7.Rows)
                {
                    if (row.Cells[0].Value.Equals(caja.desgloce.ToString()))
                    {
                        if (row.Cells[1].Value != null)
                        {
                            row.Cells[1].Value = (int.Parse(row.Cells[1].Value.ToString()) + caja.cantidad).ToString();
                        }
                        else
                        {
                            row.Cells[1].Value = caja.cantidad;
                        }


                    }


                }
            }


            dataGridView1.Rows.Clear();
            foreach (string denominacion in denominaciones)
            {
                dataGridView1.Rows.Add(denominacion);
            }
            textBoxTotalCajaChica.Text = "0";

            dataGridView12.Rows.Clear();
            foreach (string denominacion in denominaciones)
            {
                dataGridView12.Rows.Add(denominacion);
            }
            actualizaEfectivoCob();
            textBox51.Text = "0";

            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
            }
        }

        private void cargarTablaNotasDeCredito(List<Nota> Notas)
        {
            DateTime fecha = DateTime.Parse(dateTimePicker1.Value.ToString());

            int count = 0; //Este se agrega al inicio del programa
            count = 0;
            SqlCommand cmd = new SqlCommand("spNotasDeCredito", sqlAceros);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sucursal", comboBoxSucursales.Text);
            cmd.Parameters.AddWithValue("@fecha", fecha.ToString("MM/dd/yyyy"));
            cmd.Connection = sqlAceros;
            sqlAceros.Open();
            reader = cmd.ExecuteReader();

            try
            {
                // Data is accessible through the DataReader object here.
                while (reader.Read())
                {
                    dataGridView10.Rows.Add();
                    //textBox1.Text = reader["ALGO"].ToString();
                    dataGridView10.Rows[count].Cells[0].Value = reader["NoAnt"].ToString();
                    dataGridView10.Rows[count].Cells[2].Value = reader["importeapl"].ToString();
                    dataGridView10.Rows[count].Cells[4].Value = reader["NoFact"].ToString();
                    dataGridView10.Rows[count].Cells[5].Value = reader["Importe"].ToString();


                    count++;

                }
            }
            catch (Exception)
            {


            }
            sqlAceros.Close();

            //dataGridView10.Rows.Clear();
            //int renglon = 0;
            float totalNotas = 0;
            //for (int i = 0; i < Notas.Count; i++)
            //{
            //    if (Notas[i] != null)
            //    {
            //        dataGridView10.Rows.Add();
            //        dataGridView10.Rows[renglon].Cells[0].Value = Notas[i].NoAnt;
            //        dataGridView10.Rows[renglon].Cells[2].Value = Notas[i].ImporteApl;
            //        dataGridView10.Rows[renglon].Cells[4].Value = Notas[i].NoFact;
            //        dataGridView10.Rows[renglon].Cells[5].Value = Notas[i].Importe;
            //    }
            //}
            foreach (DataGridViewRow row in dataGridView10.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    try
                    {
                        totalNotas = totalNotas + float.Parse(row.Cells[2].Value.ToString());
                    }
                    catch (FormatException)
                    {
                        row.Cells[2].Value = 0;
                        MessageBox.Show("Ingrese numeros solamente.");
                    }
                }
            }
            textBoxTotalNotas.Text = totalNotas.ToString();
            textBoxGeneralNotas.Text = textBoxTotalNotas.Text;

        }

        private void cargarTablaAnticipos(List<Anticipo> Anticipos)
        {
            dataGridView9.Rows.Clear();
            int renglon = 0;
            for (int i = 0; i < Anticipos.Count; i++)
            {
                if (Anticipos[i] != null)
                {
                    dataGridView9.Rows.Add();
                    dataGridView9.Rows[renglon].Cells[0].Value = Anticipos[i].CRAZONSOCIAL;
                    dataGridView9.Rows[renglon].Cells[1].Value = Anticipos[i].CFOLIO;
                    //if (Anticipos[i].CTEXTOEXTRA2.Contains("1"))
                    //{
                    //    dataGridView9.Rows[renglon].Cells[7].Value = Anticipos[i].CIMPORTEEXTRA1;
                    //}
                    //else
                    //if (Anticipos[i].CTEXTOEXTRA2.Contains("2"))
                    //{
                    //    dataGridView9.Rows[renglon].Cells[8].Value = Anticipos[i].CIMPORTEEXTRA3;
                    //}
                    //else
                    //if (Anticipos[i].CTEXTOEXTRA2.Contains("3"))
                    //{
                    //    dataGridView9.Rows[renglon].Cells[2].Value = Anticipos[i].CTEXTOEXTRA1;
                    //    dataGridView9.Rows[renglon].Cells[3].Value = Anticipos[i].CIMPORTEEXTRA4;
                    //    dataGridView9.Rows[renglon].Cells[6].Value = Anticipos[i].CTEXTOEXTRA3;
                    //}
                    //else
                    //{
                    //    dataGridView9.Rows[renglon].Cells[4].Value = Anticipos[i].CTOTAL;
                    //}


                    if (Anticipos[i].CIMPORTEEXTRA1 > 0)
                    {
                        dataGridView9.Rows[renglon].Cells[7].Value = Anticipos[i].CIMPORTEEXTRA1;
                    }
                    else
                    if (Anticipos[i].CIMPORTEEXTRA3 > 0)
                    {
                        dataGridView9.Rows[renglon].Cells[8].Value = Anticipos[i].CIMPORTEEXTRA3;
                    }
                    else
                    if (Anticipos[i].CIMPORTEEXTRA4 > 0)
                    {
                        dataGridView9.Rows[renglon].Cells[2].Value = Anticipos[i].CTEXTOEXTRA1;
                        dataGridView9.Rows[renglon].Cells[3].Value = Anticipos[i].CIMPORTEEXTRA4;
                        dataGridView9.Rows[renglon].Cells[6].Value = Anticipos[i].CTEXTOEXTRA3;
                    }
                    else
                    {
                        dataGridView9.Rows[renglon].Cells[4].Value = Anticipos[i].CTOTAL;
                    }
                    renglon = renglon + 1;
                }
            }
            float totalChTransf = 0;
            float totalEfectivo = 0;
            float totalTCredito = 0;
            float totalTDebito = 0;
            foreach (DataGridViewRow row in dataGridView9.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    totalChTransf = totalChTransf + float.Parse(row.Cells[3].Value.ToString());
                }
                if (row.Cells[4].Value != null)
                {
                    totalEfectivo = totalEfectivo + float.Parse(row.Cells[4].Value.ToString());
                }
                if (row.Cells[7].Value != null)
                {
                    totalTCredito = totalTCredito + float.Parse(row.Cells[7].Value.ToString());
                }
                if (row.Cells[8].Value != null)
                {
                    totalTDebito = totalTDebito + float.Parse(row.Cells[8].Value.ToString());
                }
            }
            textBoxAnticipoChTranf.Text = totalChTransf.ToString();
            textBoxAnticipoEfvo.Text = totalEfectivo.ToString();
            textBoxAnticipoTCredito.Text = totalTCredito.ToString();
            textBoxAnticipoTDebito.Text = totalTDebito.ToString();
            textBoxAnticipoTotal.Text = (totalChTransf + totalEfectivo + totalTCredito + totalTDebito).ToString();
            textBoxGeneralAnticipos.Text = textBoxAnticipoTotal.Text;
            textBoxGeneralTarjetaCredito.Text = (float.Parse(textBoxGeneralTarjetaCredito.Text) + totalTCredito).ToString();
            textBoxGeneralTarjetaDebito.Text = (float.Parse(textBoxGeneralTarjetaDebito.Text) + totalTDebito).ToString();
            textBoxGeneralAnticipoChTransf.Text = textBoxAnticipoChTranf.Text;
            textBoxGeneralAnticipoEfectivo.Text = textBoxAnticipoEfvo.Text;

        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.Rows[e.RowIndex].Cells[0].Value == null)
            {
                MessageBox.Show("Favor de ingresar un concepto");
            }
            calculaVales(dataGridView4);
        }

        private void calculaVales(DataGridView datagrid)
        {
            float total = 0;

            foreach (DataGridViewRow row in datagrid.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[0].Value != null)
                {
                    try
                    {
                        total = total + float.Parse(row.Cells[1].Value.ToString());
                    }
                    catch (FormatException)
                    {
                        row.Cells[1].Value = 0;
                    }
                }
                textBoxTotalVales.Text = total.ToString();
                textBoxGeneralVales.Text = textBoxTotalVales.Text;
            }
        }

        private void dataGridView4_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calculaVales(dataGridView4);
        }

        private void calculaCheques(DataGridView datagrid)
        {
            float total = 0;

            foreach (DataGridViewRow row in datagrid.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[0].Value != null)
                {
                    try
                    {
                        total = total + float.Parse(row.Cells[1].Value.ToString());
                    }
                    catch (FormatException)
                    {
                        row.Cells[1].Value = 0;
                    }
                }
                textBoxTotalCheques.Text = total.ToString();
                textBoxGeneralCheques.Text = (float.Parse(textBoxTotalCheques.Text) + float.Parse(textBoxTotalOtroCheque.Text)).ToString();
            }
        }

        private void dataGridView5_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView5.Rows[e.RowIndex].Cells[0].Value == null)
            {
                MessageBox.Show("Favor de ingresar todos los datos");
                dataGridView5.CurrentCell = dataGridView5.Rows[e.RowIndex].Cells[0];
            }
            calculaCheques(dataGridView5);
        }

        private void dataGridView5_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calculaCheques(dataGridView4);
        }



        private void dataGridView6_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (dataGridView6.Rows[e.RowIndex].Cells[1].Value != null &&
                    dataGridView6.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Ingresa el concepto");
                    dataGridView6.Rows[e.RowIndex].Cells[1].Value = 0;
                }
                textBoxTotalOtroCheque.Text = calculaOtros(1).ToString();
                textBoxGeneralCheques.Text = (float.Parse(textBoxTotalCheques.Text) + calculaOtros(1)).ToString();
                calculaGeneralOtros();
            }
            if (e.ColumnIndex == 2)
            {

                if (dataGridView6.Rows[e.RowIndex].Cells[2].Value != null &&
                    dataGridView6.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Ingresa el concepto");
                    dataGridView6.Rows[e.RowIndex].Cells[2].Value = 0;
                }

                textBoxTotalOtroEfectivo.Text = calculaOtros(2).ToString();
                totalOtroEfectivo = calculaOtros(2);
                textBoxGeneralEfectivo.Text = (totalEfectivo + totalOtroEfectivo).ToString();
                calculaGeneralOtros();
            }
        }

        private void calculaGeneralOtros()
        {
            textBox32.Text = (float.Parse(textBoxTotalOtroCheque.Text) + float.Parse(textBoxTotalOtroEfectivo.Text)).ToString();
        }

        private float calculaOtros(int tipo)
        {
            float total = 0;
            foreach (DataGridViewRow row in dataGridView6.Rows)
            {
                if (row.Cells[tipo].Value != null)
                {
                    try
                    {
                        total = total + float.Parse(row.Cells[tipo].Value.ToString());

                    }
                    catch (FormatException)
                    {
                        row.Cells[tipo].Value = 0;
                    }
                }
            }
            return total;
        }

        private void dataGridView6_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            textBoxTotalOtroEfectivo.Text = calculaOtros(2).ToString();
            totalOtroEfectivo = calculaOtros(2);
            textBoxGeneralEfectivo.Text = (totalEfectivo + totalOtroEfectivo).ToString();
            textBoxTotalOtroCheque.Text = calculaOtros(1).ToString();
            textBoxGeneralCheques.Text = (float.Parse(textBoxGeneralCheques.Text) + calculaOtros(1)).ToString();
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (dataGridView3.Rows[e.RowIndex].Cells[3].Value != null &&
                   (dataGridView3.Rows[e.RowIndex].Cells[0].Value == null ||
                    dataGridView3.Rows[e.RowIndex].Cells[1].Value == null ||
                    dataGridView3.Rows[e.RowIndex].Cells[2].Value == null))
                {
                    MessageBox.Show("Ingrese los datos");
                    dataGridView3.Rows[e.RowIndex].Cells[3].Value = 0;
                }
                calculaFacturas();
            }
        }

        private void calculaFacturas()
        {
            textBoxTotalFacturas.Text = calculaVarios(3, dataGridView3).ToString();
            textBoxGeneralFacturas.Text = textBoxTotalFacturas.Text;
        }

        private float calculaVarios(int columna, DataGridView dg)
        {
            float total = 0;
            foreach (DataGridViewRow row in dg.Rows)
            {
                if (row.Cells[columna].Value != null)
                {
                    try
                    {
                        total = total + float.Parse(row.Cells[columna].Value.ToString());

                    }
                    catch (FormatException)
                    {
                        row.Cells[columna].Value = 0;
                    }
                }
            }
            return total;
        }

        private void dataGridView3_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            textBoxTotalFacturas.Text = calculaVarios(3, dataGridView3).ToString();
            textBoxGeneralFacturas.Text = textBoxTotalFacturas.Text;
        }

        private void dataGridView8_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        private void dataGridView8_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (dataGridView8.Rows[e.RowIndex].Cells[3].Value != null &&
                    dataGridView8.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Ingrese los datos del cliente");
                    dataGridView8.Rows[e.RowIndex].Cells[3].Value = 0;
                }
                textBoxImpEfectivo.Text = calculaVarios(3, dataGridView8).ToString();
                textBoxGeneralDevEfectivo.Text = textBoxImpEfectivo.Text;
            }
            if (e.ColumnIndex == 2)
            {
                if (dataGridView8.Rows[e.RowIndex].Cells[2].Value != null &&
                   (dataGridView8.Rows[e.RowIndex].Cells[0].Value == null ||
                    dataGridView8.Rows[e.RowIndex].Cells[1].Value == null))
                {
                    MessageBox.Show("Ingrese los datos del cliente/cheque");
                    dataGridView8.Rows[e.RowIndex].Cells[2].Value = 0;
                }
                textBoxImpCheque.Text = calculaVarios(2, dataGridView8).ToString();
                textBoxGeneralDevCheque.Text = textBoxImpCheque.Text;
            }
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                float pago = 0;
                if (dataGridView8.Rows[e.RowIndex].Cells[2].Value != null)
                {
                    pago = pago + float.Parse(dataGridView8.Rows[e.RowIndex].Cells[2].Value.ToString());
                }
                if (dataGridView8.Rows[e.RowIndex].Cells[3].Value != null)
                {
                    pago = pago + float.Parse(dataGridView8.Rows[e.RowIndex].Cells[3].Value.ToString());
                }
                dataGridView8.Rows[e.RowIndex].Cells[4].Value = pago;
            }
            if (e.ColumnIndex == 5)
            {
                if (dataGridView8.Rows[e.RowIndex].Cells[5].Value != null &&
                    dataGridView8.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Ingrese los datos del cliente");
                    dataGridView8.Rows[e.RowIndex].Cells[5].Value = 0;
                }
                textBoxTotalTC.Text = calculaVarios(5, dataGridView8).ToString();
                textBoxGeneralTarjetaCredito.Text = (GeneralChDevTC + float.Parse(textBoxTotalTC.Text)).ToString();
            }
            if (e.ColumnIndex == 6)
            {
                if (dataGridView8.Rows[e.RowIndex].Cells[6].Value != null &&
                    dataGridView8.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    MessageBox.Show("Ingrese los datos del cliente");
                    dataGridView8.Rows[e.RowIndex].Cells[6].Value = 0;
                }
                textBoxTotalTD.Text = calculaVarios(6, dataGridView8).ToString();
                textBoxGeneralTarjetaDebito.Text = (GeneralChDevTD + float.Parse(textBoxTotalTD.Text)).ToString();
            }
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                float total = 0;
                textBoxTotalChequesDevueltos.Text = "0";
                foreach (DataGridViewRow row in dataGridView8.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {
                        total = total + float.Parse(row.Cells[4].Value.ToString());
                    }

                    if (row.Cells[5].Value != null)
                    {
                        total = total + float.Parse(row.Cells[5].Value.ToString());
                    }
                    if (row.Cells[6].Value != null)
                    {
                        total = total + float.Parse(row.Cells[6].Value.ToString());
                    }
                }
                textBoxTotalChequesDevueltos.Text = (float.Parse(textBoxTotalChequesDevueltos.Text) + total).ToString();
                textBoxGeneralChDevueltos.Text = textBoxTotalChequesDevueltos.Text;
            }
        }

        private void dataGridView12_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            actualizaEfectivoCob();
        }

        private void actualizaEfectivoCob()
        {
            float total = 0;
            foreach (DataGridViewRow row in dataGridView12.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    try
                    {
                        row.Cells[2].Value = float.Parse(row.Cells[0].Value.ToString())
                            * float.Parse(row.Cells[1].Value.ToString());
                    }
                    catch (FormatException)
                    {
                        row.Cells[1].Value = 0;
                        row.Cells[2].Value = 0;
                    }
                    total = total + float.Parse(row.Cells[2].Value.ToString());
                }

            }
            textBox51.Text = total.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Enabled = false;
            dataGridView13.Rows.Clear();
            foreach (string denominacion in denominaciones)
            {
                dataGridView13.Rows.Add(denominacion);
            }
            foreach (Caja caja in Cajas)
            {
                foreach (DataGridViewRow row in dataGridView13.Rows)
                {

                    if (row.Cells[0].Value.Equals(caja.desgloce.ToString()) && (comboBox2.SelectedItem as dynamic).Value.ToString().Equals(caja.caja.ToString()))
                    {
                        row.Cells[1].Value = caja.cantidad;
                        row.Cells[2].Value = caja.desgloce * caja.cantidad;
                    }

                }
            }

        }
        private void dataGridView7_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            float total = 0;
            foreach (DataGridViewRow row in dataGridView7.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    try
                    {
                        row.Cells[2].Value = float.Parse(row.Cells[0].Value.ToString())
                            * float.Parse(row.Cells[1].Value.ToString());
                    }
                    catch (FormatException)
                    {
                        row.Cells[1].Value = 0;
                        row.Cells[2].Value = 0;
                    }
                    total = total + float.Parse(row.Cells[2].Value.ToString());
                }

            }
            textBoxEfectivo.Text = total.ToString();
            totalEfectivo = total;
            textBoxGeneralEfectivo.Text = (totalEfectivo + totalOtroEfectivo).ToString();
        }

        private void dataGridView13_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            float total = 0;
            foreach (DataGridViewRow row in dataGridView13.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    try
                    {
                        row.Cells[2].Value = float.Parse(row.Cells[0].Value.ToString())
                            * float.Parse(row.Cells[1].Value.ToString());
                    }
                    catch (FormatException)
                    {
                        row.Cells[1].Value = 0;
                        row.Cells[2].Value = 0;
                    }
                    total = total + float.Parse(row.Cells[2].Value.ToString());
                }

            }
            textBoxTotalCorte.Text = total.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (RetirosForm retiroform = new RetirosForm(comboBoxSucursales.Text, float.Parse(textBoxTotalCorte.Text), dateTimePicker1.Value))
            {
                retiroform.ShowDialog();
                if (!existeCorte(comboBoxSucursales.Text, dateTimePicker1.Value))
                {
                    guardaCorteInicial(comboBoxSucursales.Text, dateTimePicker1.Value, retiroform.nombre);
                }
                int noCaja = getNoCajas(comboBoxSucursales.Text, dateTimePicker1.Value) + 1;
                int idCorte = getIdCorte(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView13.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        guardaCaja(noCaja, float.Parse(row.Cells[0].Value.ToString()), int.Parse(row.Cells[1].Value.ToString()), retiroform.nombre, idCorte);
                    }
                }
                updateCajas();
                button6.Enabled = false;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Add(new { Text = "", Value = "" });
            comboBox2.SelectedIndex = comboBox2.Items.Count - 1;
            textBoxTotalCorte.Text = "0";
            button6.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Ingresa quién elaboró el corte");
                return;
            }
            if (!existeCorte(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                guardaCorteInicial(comboBoxSucursales.Text, dateTimePicker1.Value, textBox2.Text);
            }
            guardaCajasEfectivo();
            guardaGeneralCajaChica();
            guardaRelacionFacturas();
            guardaRelacionVales();
            guardaRelacionCheques();
            guardaOtros();
            guardaDevueltos();
            guardaDocumentos();
            guardaGeneralEfectivoCobranza();
            guardaExtras();
            guardaComdev(comboBoxSucursales.Text, dateTimePicker1.Value, float.Parse(textBox15.Text), float.Parse(textBox17.Text));
            MessageBox.Show("Guardado");
        }

        private void guardaExtras()
        {
            if (!textBox2.Text.Equals(""))
            {
                float dolares = textBox3.Text.Equals("") ? 0 : float.Parse(textBox3.Text);
                eliminaElaboro(comboBoxSucursales.Text, dateTimePicker1.Value);
                guardaElaboro(comboBoxSucursales.Text, dateTimePicker1.Value, textBox2.Text, dolares);
            }
        }

        private void guardaGeneralEfectivoCobranza()
        {
            if (existeEfectivoCobranza(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                eliminaEfectivoCobranza(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView12.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaEfectivoCobranza(comboBoxSucursales.Text, dateTimePicker1.Value, float.Parse(row.Cells[0].Value.ToString()), int.Parse(row.Cells[1].Value.ToString()), textBox2.Text);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView12.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaEfectivoCobranza(comboBoxSucursales.Text, dateTimePicker1.Value, float.Parse(row.Cells[0].Value.ToString()), int.Parse(row.Cells[1].Value.ToString()), textBox2.Text);
                        }
                    }
                }
            }
        }

        private void guardaDocumentos()
        {
            if (existeDocumentos(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                eliminaDocumentos(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView11.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            string concepto = row.Cells[0].Value.ToString();
                            guardaDocumentosdb(comboBoxSucursales.Text, dateTimePicker1.Value, concepto);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView11.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            string concepto = row.Cells[0].Value.ToString();
                            guardaDocumentosdb(comboBoxSucursales.Text, dateTimePicker1.Value, concepto);
                        }
                    }
                }
            }
        }

        private void guardaDevueltos()
        {
            if (existeDevuelto(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                eliminaDevuelto(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView8.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            string cliente = row.Cells[0].Value.ToString();
                            int noCheque = row.Cells[1].Value == null ? 0 : int.Parse(row.Cells[1].Value.ToString());
                            float impcheq = row.Cells[2].Value == null ? 0 : float.Parse(row.Cells[2].Value.ToString());
                            float impefec = row.Cells[3].Value == null ? 0 : float.Parse(row.Cells[3].Value.ToString());
                            float pago = row.Cells[4].Value == null ? 0 : float.Parse(row.Cells[4].Value.ToString());
                            float comcheq = row.Cells[5].Value == null ? 0 : float.Parse(row.Cells[5].Value.ToString());
                            float comefe = row.Cells[6].Value == null ? 0 : float.Parse(row.Cells[6].Value.ToString());

                            guardaDevueltodb(comboBoxSucursales.Text, dateTimePicker1.Value, cliente, noCheque, impcheq, impefec, comcheq, comefe, pago);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView8.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            string cliente = row.Cells[0].Value.ToString();
                            int noCheque = row.Cells[1].Value == null ? 0 : int.Parse(row.Cells[1].Value.ToString());
                            float impcheq = row.Cells[2].Value == null ? 0 : float.Parse(row.Cells[2].Value.ToString());
                            float impefec = row.Cells[3].Value == null ? 0 : float.Parse(row.Cells[3].Value.ToString());
                            float pago = row.Cells[4].Value == null ? 0 : float.Parse(row.Cells[4].Value.ToString());
                            float comcheq = row.Cells[5].Value == null ? 0 : float.Parse(row.Cells[5].Value.ToString());
                            float comefe = row.Cells[6].Value == null ? 0 : float.Parse(row.Cells[6].Value.ToString());

                            guardaDevueltodb(comboBoxSucursales.Text, dateTimePicker1.Value, cliente, noCheque, impcheq, impefec, comcheq, comefe, pago);
                        }
                    }
                }
            }
        }

        private void guardaOtros()
        {
            if (existeOtros(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                eliminaOtros(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            float cheque = row.Cells[1].Value == null ? 0 : float.Parse(row.Cells[1].Value.ToString());
                            float efectivo = row.Cells[2].Value == null ? 0 : float.Parse(row.Cells[2].Value.ToString());
                            guardaOtrosbd(comboBoxSucursales.Text, dateTimePicker1.Value, cheque, row.Cells[0].Value.ToString(), efectivo);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView6.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            float cheque = row.Cells[1].Value == null ? 0 : float.Parse(row.Cells[1].Value.ToString());
                            float efectivo = row.Cells[2].Value == null ? 0 : float.Parse(row.Cells[2].Value.ToString());
                            guardaOtrosbd(comboBoxSucursales.Text, dateTimePicker1.Value, cheque, row.Cells[0].Value.ToString(), efectivo);
                        }
                    }
                }
            }
        }

        private void guardaRelacionCheques()
        {
            if (existeCheques(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                eliminaCheques(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView5.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[1].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaCheques(comboBoxSucursales.Text, dateTimePicker1.Value, row.Cells[0].Value.ToString(), float.Parse(row.Cells[1].Value.ToString()));
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView5.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[1].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaCheques(comboBoxSucursales.Text, dateTimePicker1.Value, row.Cells[0].Value.ToString(), float.Parse(row.Cells[1].Value.ToString()));
                        }
                    }
                }
            }
        }

        private void guardaRelacionVales()
        {
            if (existeVales(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                eliminaVales(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[1].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaVales(comboBoxSucursales.Text, dateTimePicker1.Value, row.Cells[0].Value.ToString(), float.Parse(row.Cells[1].Value.ToString()));
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView4.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[1].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaVales(comboBoxSucursales.Text, dateTimePicker1.Value, row.Cells[0].Value.ToString(), float.Parse(row.Cells[1].Value.ToString()));
                        }
                    }
                }
            }
        }

        private void guardaRelacionFacturas()
        {
            if (existeFacturas(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                eliminaFacturas(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[1].Value != null &&
                        row.Cells[2].Value != null &&
                        row.Cells[3].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaFacturas(comboBoxSucursales.Text, dateTimePicker1.Value, row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), float.Parse(row.Cells[3].Value.ToString()));
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Cells[0].Value != null &&
                        row.Cells[1].Value != null &&
                        row.Cells[2].Value != null &&
                        row.Cells[3].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaFacturas(comboBoxSucursales.Text, dateTimePicker1.Value, row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), float.Parse(row.Cells[3].Value.ToString()));
                        }
                    }
                }
            }
        }

        private void guardaGeneralCajaChica()
        {
            if (existeCajaChica(comboBoxSucursales.Text, dateTimePicker1.Value))
            {
                eliminaCajaChica(comboBoxSucursales.Text, dateTimePicker1.Value);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaCajaChica(comboBoxSucursales.Text, dateTimePicker1.Value, float.Parse(row.Cells[0].Value.ToString()), int.Parse(row.Cells[1].Value.ToString()), textBox2.Text);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        if (!textBox2.Text.Equals(""))
                        {
                            guardaCajaChica(comboBoxSucursales.Text, dateTimePicker1.Value, float.Parse(row.Cells[0].Value.ToString()), int.Parse(row.Cells[1].Value.ToString()), textBox2.Text);
                        }
                    }
                }
            }
        }

        private void guardaCajasEfectivo()
        {
            foreach (DataGridViewRow row in dataGridView7.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    if (!textBox2.Text.Equals(""))
                    {
                        if (getDesglocesTotalEfectivoCorte(comboBoxSucursales.Text, dateTimePicker1.Value, float.Parse(row.Cells[0].Value.ToString())) == 0)
                        {
                            guardaTotalEfectivoCorte(float.Parse(row.Cells[0].Value.ToString()), int.Parse(row.Cells[1].Value.ToString()), textBox2.Text, getIdCorte(comboBoxSucursales.Text, dateTimePicker1.Value));
                        }
                        else
                        {
                            actualizaDesglocesTotalEfectivoCorte(comboBoxSucursales.Text, dateTimePicker1.Value, float.Parse(row.Cells[0].Value.ToString()), int.Parse(row.Cells[1].Value.ToString()));
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {


        }



        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            if (!textBox16.Text.Equals(""))
            {

                try
                {
                    if (float.Parse(textBox16.Text) > float.Parse(textBox15.Text))
                    {
                        textBox16.Text = textBox15.Text;
                    }
                    textBox17.Text = (float.Parse(textBox15.Text) - float.Parse(textBox16.Text)).ToString();
                }
                catch (System.FormatException)
                {
                    textBox16.Text = "0";
                }
                calculaTotalDelDia();
            }
        }

        private void textBoxGeneralEfectivo_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralCheques_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralVales_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralFacturas_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralDevEfectivo_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralDevCheque_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralAnticipoEfectivo_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralAnticipoChTransf_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralNotas_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralTarjetaCredito_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralTarjetaDebito_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void textBoxGeneralTransferCobranza_TextChanged(object sender, EventArgs e)
        {
            calculaTotal();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            float total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    try
                    {
                        row.Cells[2].Value = float.Parse(row.Cells[0].Value.ToString())
                            * float.Parse(row.Cells[1].Value.ToString());
                    }
                    catch (FormatException)
                    {
                        row.Cells[1].Value = 0;
                        row.Cells[2].Value = 0;
                    }
                    total = total + float.Parse(row.Cells[2].Value.ToString());
                }

            }
            textBoxTotalCajaChica.Text = total.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Variablescompartidas.cr = "0";
            Totales1 totales = new Totales1();
            totales.facturado = float.Parse(textBoxFacturado.Text);
            totales.notasDeVenta = float.Parse(textBoxNotasDeVenta.Text);
            totales.totalVentasDelDia = float.Parse(textBoxVentasDelDia.Text);
            totales.credito = float.Parse(textBoxCreditos.Text);
            totales.totalVentasDeContado = float.Parse(textBoxVentasDeContado.Text);
            totales.chequesDevueltos = float.Parse(textBoxGeneralChDevueltos.Text);
            totales.anticipos = float.Parse(textBoxGeneralAnticipos.Text);
            totales.devSobreVentas = float.Parse(textBox13.Text);
            totales.devDeRemision = float.Parse(textBox14.Text);
            totales.comisionPorDev = float.Parse(textBox17.Text);
            totales.cobranzaDelDia = float.Parse(textBoxCobranzaDia.Text);
            totales.totalDelDia = float.Parse(textBoxTotalDia.Text);
            totales.otros = float.Parse(textBox32.Text);
            Totales2 totales2 = new Totales2();
            totales2.efectivo = float.Parse(textBoxGeneralEfectivo.Text);
            totales2.cheques = float.Parse(textBoxGeneralCheques.Text);
            totales2.vales = float.Parse(textBoxGeneralVales.Text);
            totales2.facturas = float.Parse(textBoxGeneralFacturas.Text);
            totales2.pagoChDevEfectivo = float.Parse(textBoxGeneralDevEfectivo.Text);
            totales2.pagoChDevCheque = float.Parse(textBoxGeneralDevCheque.Text);
            totales2.anticipoConEfectivo = float.Parse(textBoxGeneralAnticipoEfectivo.Text);
            totales2.anticipoCCheqTransf = float.Parse(textBoxGeneralAnticipoChTransf.Text);
            totales2.notaDeCredito = float.Parse(textBoxGeneralNotas.Text);
            totales2.tarjetaDeCredito = float.Parse(textBoxGeneralTarjetaCredito.Text);
            totales2.tarjetaDeDebito = float.Parse(textBoxGeneralTarjetaDebito.Text);
            totales2.transferenciaCobranza = float.Parse(textBoxGeneralTransferCobranza.Text);
            totales2.dlls = textBox3.Text.Equals("") ? 0 : float.Parse(textBox3.Text);
            totales2.tc = textBox4.Text.Equals("") ? 0 : float.Parse(textBox4.Text);
            totales2.total = textBoxTotal.Text.Equals("") ? 0 : float.Parse(textBoxTotal.Text);

            Consecutivos consecutivo = new Consecutivos();
            consecutivo.fact_Inicial = textBoxConsecutivosFInicial.Text.Equals("") ? 0 : int.Parse(textBoxConsecutivosFInicial.Text);
            consecutivo.fact_Final = textBoxConsecutivosFFinal.Text.Equals("") ? 0 : int.Parse(textBoxConsecutivosFFinal.Text);
            consecutivo.nv_Inicial = textBoxConsecutivosNVInicial.Text.Equals("") ? 0 : int.Parse(textBoxConsecutivosNVInicial.Text);
            consecutivo.nv_Final = textBoxConsecutivosNVFinal.Text.Equals("") ? 0 : int.Parse(textBoxConsecutivosNVFinal.Text);

            Elaboro elaboro = new Elaboro();
            elaboro.elaboro = textBox2.Text;
            elaboro.fecha = dateTimePicker1.Text;

            using (Form4 fr = new Form4(dataGridView3,
                                       dataGridView4,
                                       dataGridView5,
                                       dataGridView7,
                                       dataGridView1,
                                       dataGridView6,
                                       totales,
                                       totales2,
                                       dataGridView8,
                                       dataGridView9,
                                       dataGridView11,
                                       dataGridView10,
                                       consecutivo,
                                       elaboro,
                                       nombreDeSucursal,
                                       comboBoxSucursales.Text))
            {
                fr.ShowDialog();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!textBox3.Text.Equals(""))
            {
                try
                {
                    textBox5.Text = (float.Parse(textBox3.Text) * float.Parse(textBox4.Text)).ToString();
                }
                catch (FormatException)
                {
                    textBox3.Text = "0";
                }
            }
            else
            {
                textBox5.Text = "0";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cierre()
        {
            cerrado = 0;
            DateTime fecha = DateTime.Parse(dateTimePicker1.Text);
            //Obtener valor o varios valores de la base de datos
            string credito = "";
            string contado = "";
            string notas = "";
            string trassal = "";
            cmd.CommandText = "select idcredito, idcontado, idnotas, idtrassal from folios where sucursal = '"+comboBoxSucursales.Text+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlAmsa;
            sqlAmsa.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                credito = reader["idcredito"].ToString();
                contado = reader["idcontado"].ToString();
                notas = reader["idnotas"].ToString();
                trassal = reader["idtrassal"].ToString();

            }
            sqlAmsa.Close();

            string sql = "update admDocumentos set CIMPRESO = '1' where CIDCONCEPTODOCUMENTO = @param1 or CIDCONCEPTODOCUMENTO = @param2 and CFECHA = @param3";
            cmd = new SqlCommand(sql, sqlAceros);
            cmd.Parameters.AddWithValue("@param1", credito); //Para grabar algo de un textbox
            cmd.Parameters.AddWithValue("@param2", contado); //Para grabar una columna
            cmd.Parameters.AddWithValue("@param3", fecha.ToString("MM/dd/yyyy")); //Para grabar una columna

            sqlAceros.Open();
            cmd.ExecuteNonQuery();
            sqlAceros.Close();


            string sql2 = "update admDocumentos set CIMPRESO = '1' where CIDCONCEPTODOCUMENTO = @param1 or CIDCONCEPTODOCUMENTO = @param2  and CFECHA = @param3";
            cmd = new SqlCommand(sql2, sqlAceros);
            cmd.Parameters.AddWithValue("@param1", notas); //Para grabar algo de un textbox
            cmd.Parameters.AddWithValue("@param2", trassal); //Para grabar una columna
            cmd.Parameters.AddWithValue("@param3", fecha.ToString("MM/dd/yyyy")); //Para grabar una columna

            sqlAceros.Open();
            cmd.ExecuteNonQuery();
            sqlAceros.Close();
            //cerrado = 1;
            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Esta seguro que desea cerrar el corte?", "Cierre", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (textBox2.Text.Equals(""))
                {
                    MessageBox.Show("Ingresa quién elaboró el corte");
                    return;
                }
                if (!existeCorte(comboBoxSucursales.Text, dateTimePicker1.Value))
                {
                    guardaCorteInicial(comboBoxSucursales.Text, dateTimePicker1.Value, textBox2.Text);
                }
                guardaCajasEfectivo();
                guardaGeneralCajaChica();
                guardaRelacionFacturas();
                guardaRelacionVales();
                guardaRelacionCheques();
                guardaOtros();
                guardaDevueltos();
                guardaDocumentos();
                guardaGeneralEfectivoCobranza();
                guardaExtras();
                guardaComdev(comboBoxSucursales.Text, dateTimePicker1.Value, float.Parse(textBox15.Text), float.Parse(textBox17.Text));
                cierre();
                Variablescompartidas.cr = " ";
                MessageBox.Show("Corte cerrado");
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridView5.SelectedRows.Count > 0)
                {
                    dataGridView5.Rows.RemoveAt(this.dataGridView5.SelectedRows[0].Index);
                }
                calculaCheques(dataGridView5);
            }
            catch (Exception)
            {

                
            }
        }
    }
}
