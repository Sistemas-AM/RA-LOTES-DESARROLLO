using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static DistribucionMateriales.DBHelper;

namespace DistribucionMateriales
{
    public partial class Form1 : Form
    {
        private List<string> sucursales;
        private List<Distribucion> distribucion;
        private List<string> letras;
        private bool conLetra = false;
        private List<Productos> productos;
        public Form1()
        {
            InitializeComponent();
            sucursales = getSucursales();
            letras = getLetras();
            foreach (string letra in letras)
            {
                comboBox2.Items.Add(letra);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!maskedTextBox1.Text.Equals("") && !maskedTextBox2.Text.Equals(""))
            {
                dataGridView5.Rows.Clear();
                productos = getProductos(textBox1.Text);
                for (int i = 0; i < productos.Count; i++)
                {
                    dataGridView5.Rows.Add();
                    dataGridView5.Rows[i].Cells[0].Value = productos[i].ccodigoproducto;
                    dataGridView5.Rows[i].Cells[1].Value = productos[i].cnombreproducto;

                }
                foreach (string sucursal in sucursales)
                {
                    switch (sucursal)
                    {
                        case "MAT":
                            if (conLetra)
                            {
                                distribucion = getDistribuciones(sucursal, textBox1.Text, true, comboBox2.Text, float.Parse(maskedTextBox1.Text), float.Parse(maskedTextBox2.Text));
                            }
                            else
                            {
                                distribucion = getDistribuciones(sucursal, textBox1.Text, false, "", float.Parse(maskedTextBox1.Text), float.Parse(maskedTextBox2.Text));
                            }
                            bindMat();
                            break;
                        case "LP ":
                            if (conLetra)
                            {
                                distribucion = getDistribuciones(sucursal, textBox1.Text, true, comboBox2.Text, float.Parse(maskedTextBox1.Text), float.Parse(maskedTextBox2.Text));
                            }
                            else
                            {
                                distribucion = getDistribuciones(sucursal, textBox1.Text, false, "", float.Parse(maskedTextBox1.Text), float.Parse(maskedTextBox2.Text));
                            }
                            bindLP();
                            break;
                        case "IGS":
                            if (conLetra)
                            {
                                distribucion = getDistribuciones(sucursal, textBox1.Text, true, comboBox2.Text, float.Parse(maskedTextBox1.Text), float.Parse(maskedTextBox2.Text));
                            }
                            else
                            {
                                distribucion = getDistribuciones(sucursal, textBox1.Text, false, "", float.Parse(maskedTextBox1.Text), float.Parse(maskedTextBox2.Text));
                            }
                            bindIgs();
                            break;
                        case "SP ":
                            if (conLetra)
                            {
                                distribucion = getDistribuciones(sucursal, textBox1.Text, true, comboBox2.Text, float.Parse(maskedTextBox1.Text), float.Parse(maskedTextBox2.Text));
                            }
                            else
                            {
                                distribucion = getDistribuciones(sucursal, textBox1.Text, false, "", float.Parse(maskedTextBox1.Text), float.Parse(maskedTextBox2.Text));
                            }
                            bindSp();
                            break;
                    }
                }
                SumaSurtido();
            }
        }

        private void valida()
        {
            int count2 = 0;
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                if (dataGridView5.Rows[count2].Cells[9].Value == null)
                {
                    //dataGridView5.Rows[count2].Cells[13].Value = "0";
                    //dataGridView5.Rows[count2].Cells[5].Value = "0";
                    dataGridView5.Rows[count2].Cells[9].Value = "0";
                    //dataGridView5.Rows[count2].Cells[17].Value = "0";
                    //dataGridView5.Rows[count2].Cells[20].Value = "0";
                }
               if (dataGridView5.Rows[count2].Cells[5].Value == null)
                {
                    dataGridView5.Rows[count2].Cells[5].Value = "0";

                }
                if (dataGridView5.Rows[count2].Cells[13].Value == null)
                {
                    dataGridView5.Rows[count2].Cells[13].Value = "0";

                }
                if (dataGridView5.Rows[count2].Cells[17].Value == null)
                {
                    dataGridView5.Rows[count2].Cells[17].Value = "0";

                }
                if (dataGridView5.Rows[count2].Cells[20].Value == null)
                {
                    dataGridView5.Rows[count2].Cells[20].Value = "0";

                }
                
                count2 += 1;

            }
        }

        private void negativos()
        {
            int count3 = 0;
            
            foreach (DataGridViewRow row in dataGridView5.Rows)
            {
                double mat = 0;
                double port = 0;
                double sala = 0;
                double sp = 0;
                if (Double.Parse(dataGridView5.Rows[count3].Cells[5].Value.ToString()) < 0)
                {
                    mat = 0;
                }
                else
                {
                    mat = Double.Parse(dataGridView5.Rows[count3].Cells[5].Value.ToString());

                }
                if (Double.Parse(dataGridView5.Rows[count3].Cells[9].Value.ToString()) < 0)
                {
                    port = 0;
                }
                else
                {
                    port = Double.Parse(dataGridView5.Rows[count3].Cells[9].Value.ToString());
                }
                if (Double.Parse(dataGridView5.Rows[count3].Cells[13].Value.ToString()) < 0)
                {
                    sala = 0;
                }
                else
                {
                    sala = Double.Parse(dataGridView5.Rows[count3].Cells[13].Value.ToString());
                }
                if (Double.Parse(dataGridView5.Rows[count3].Cells[17].Value.ToString()) < 0)
                {
                    sp = 0;
                }
                else
                {
                    sp = Double.Parse(dataGridView5.Rows[count3].Cells[17].Value.ToString());
                }

                dataGridView5.Rows[count3].Cells[19].Value = mat + port + sala + sp;
               count3 += 1;
               
            }

        }
        private void SumaSurtido()
        {
            valida();
            negativos();
            //int count = 0;
            //foreach (DataGridViewRow row in dataGridView5.Rows)
            //{
            //    try
            //    {
                   
            //        dataGridView5.Rows[count].Cells[19].Value = Double.Parse(dataGridView5.Rows[count].Cells[5].Value.ToString()) + Double.Parse(dataGridView5.Rows[count].Cells[9].Value.ToString()) + Double.Parse(dataGridView5.Rows[count].Cells[13].Value.ToString()) + Double.Parse(dataGridView5.Rows[count].Cells[17].Value.ToString());
            //        count += 1;
            //    }
            //    catch (Exception)
            //    {

                   
            //    }
            //}
        }

        private void bindSp()
        {
            dataGridView4.Rows.Clear();
            for (int i = 0; i < distribucion.Count; i++)
            {
                dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = distribucion[i].ccodigoproducto;
                dataGridView4.Rows[i].Cells[1].Value = distribucion[i].cnombreproducto;
                dataGridView4.Rows[i].Cells[2].Value = distribucion[i].letra;
                dataGridView4.Rows[i].Cells[3].Value = distribucion[i].capespacio;
                dataGridView4.Rows[i].Cells[4].Value = distribucion[i].capsurtido;
                dataGridView4.Rows[i].Cells[5].Value = distribucion[i].existencia;
                dataGridView4.Rows[i].Cells[6].Value = distribucion[i].cant;
                dataGridView4.Rows[i].Cells[7].Value = distribucion[i].existenciaplanta;
                dataGridView4.Rows[i].Cells[8].Value = distribucion[i].cedisEnMatriz;
                dataGridView4.Rows[i].Cells[9].Value = distribucion[i].desabasto;

                if (dataGridView5.Rows.Count < distribucion.Count)
                {
                    dataGridView5.Rows.Add();
                }

                foreach (DataGridViewRow row in dataGridView5.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(distribucion[i].ccodigoproducto))
                        {
                            dataGridView5.Rows[row.Index].Cells[14].Value = distribucion[i].existencia;
                            dataGridView5.Rows[row.Index].Cells[15].Value = distribucion[i].capespacio;
                            dataGridView5.Rows[row.Index].Cells[16].Value = distribucion[i].capsurtido;
                            dataGridView5.Rows[row.Index].Cells[17].Value = distribucion[i].cant;
                            dataGridView5.Rows[row.Index].Cells[20].Value = "0";
                            dataGridView5.Rows[row.Index].Cells[23].Value = distribucion[i].kilos;

                            if (dataGridView5.Rows[row.Index].Cells[18].Value == null)
                            {
                                dataGridView5.Rows[row.Index].Cells[18].Value = distribucion[i].existenciaplanta;
                            }
                        }
                    }
                }

            }
        }

        private void bindIgs()
        {
            dataGridView3.Rows.Clear();
            for (int i = 0; i < distribucion.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = distribucion[i].ccodigoproducto;
                dataGridView3.Rows[i].Cells[1].Value = distribucion[i].cnombreproducto;
                dataGridView3.Rows[i].Cells[2].Value = distribucion[i].letra;
                dataGridView3.Rows[i].Cells[3].Value = distribucion[i].capespacio;
                dataGridView3.Rows[i].Cells[4].Value = distribucion[i].capsurtido;
                dataGridView3.Rows[i].Cells[5].Value = distribucion[i].existencia;
                dataGridView3.Rows[i].Cells[6].Value = distribucion[i].cant;
                dataGridView3.Rows[i].Cells[7].Value = distribucion[i].existenciaplanta;
                dataGridView3.Rows[i].Cells[8].Value = distribucion[i].cedisEnMatriz;
                dataGridView3.Rows[i].Cells[9].Value = distribucion[i].desabasto;

                if (dataGridView5.Rows.Count < distribucion.Count)
                {
                    dataGridView5.Rows.Add();
                }

                foreach (DataGridViewRow row in dataGridView5.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(distribucion[i].ccodigoproducto))
                        {
                            dataGridView5.Rows[row.Index].Cells[10].Value = distribucion[i].existencia;
                            dataGridView5.Rows[row.Index].Cells[11].Value = distribucion[i].capsurtido;
                            dataGridView5.Rows[row.Index].Cells[12].Value = distribucion[i].capespacio;
                            dataGridView5.Rows[row.Index].Cells[13].Value = distribucion[i].cant;
                            dataGridView5.Rows[row.Index].Cells[20].Value = "0";
                            dataGridView5.Rows[row.Index].Cells[22].Value = distribucion[i].letra;
                            dataGridView5.Rows[row.Index].Cells[23].Value = distribucion[i].kilos;

                            if (dataGridView5.Rows[row.Index].Cells[18].Value == null)
                            {
                                dataGridView5.Rows[row.Index].Cells[18].Value = distribucion[i].existenciaplanta;
                            }
                        }
                    }
                }

            }
        }

        private void bindLP()
        {
            dataGridView2.Rows.Clear();
            for (int i = 0; i < distribucion.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = distribucion[i].ccodigoproducto;
                dataGridView2.Rows[i].Cells[1].Value = distribucion[i].cnombreproducto;
                dataGridView2.Rows[i].Cells[2].Value = distribucion[i].letra;
                dataGridView2.Rows[i].Cells[3].Value = distribucion[i].capespacio;
                dataGridView2.Rows[i].Cells[4].Value = distribucion[i].capsurtido;
                dataGridView2.Rows[i].Cells[5].Value = distribucion[i].existencia;
                dataGridView2.Rows[i].Cells[6].Value = distribucion[i].cant;
                dataGridView2.Rows[i].Cells[7].Value = distribucion[i].existenciaplanta;
                dataGridView2.Rows[i].Cells[8].Value = distribucion[i].cedisEnMatriz;
                dataGridView2.Rows[i].Cells[9].Value = distribucion[i].desabasto;

                if (dataGridView5.Rows.Count < distribucion.Count)
                {
                    dataGridView5.Rows.Add();
                }

                foreach (DataGridViewRow row in dataGridView5.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(distribucion[i].ccodigoproducto))
                        {
                            dataGridView5.Rows[row.Index].Cells[9].Value = distribucion[i].cant;
                            dataGridView5.Rows[row.Index].Cells[8].Value = distribucion[i].capsurtido;
                            dataGridView5.Rows[row.Index].Cells[7].Value = distribucion[i].capespacio;
                            dataGridView5.Rows[row.Index].Cells[6].Value = distribucion[i].existencia;
                            dataGridView5.Rows[row.Index].Cells[20].Value = "0";
                            dataGridView5.Rows[row.Index].Cells[23].Value = distribucion[i].kilos;

                            if(dataGridView5.Rows[row.Index].Cells[18].Value == null)
                            {
                                dataGridView5.Rows[row.Index].Cells[18].Value = distribucion[i].existenciaplanta;
                            }
                        }
                    }
                }
            }
        }

        private void bindMat()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < distribucion.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = distribucion[i].ccodigoproducto;
                dataGridView1.Rows[i].Cells[1].Value = distribucion[i].cnombreproducto;
                dataGridView1.Rows[i].Cells[2].Value = distribucion[i].letra;
                dataGridView1.Rows[i].Cells[3].Value = distribucion[i].capespacio;
                dataGridView1.Rows[i].Cells[4].Value = distribucion[i].capsurtido;
                dataGridView1.Rows[i].Cells[5].Value = distribucion[i].existencia;
                dataGridView1.Rows[i].Cells[6].Value = distribucion[i].cant;
                dataGridView1.Rows[i].Cells[7].Value = distribucion[i].existenciaplanta;
                dataGridView1.Rows[i].Cells[8].Value = distribucion[i].cedisEnMatriz;
                dataGridView1.Rows[i].Cells[9].Value = distribucion[i].desabasto;

                if (dataGridView5.Rows.Count < distribucion.Count)
                {
                    dataGridView5.Rows.Add();
                }
                foreach (DataGridViewRow row in dataGridView5.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        if (row.Cells[0].Value.ToString().Equals(dataGridView1.Rows[i].Cells[0].Value.ToString()))
                        {
                            dataGridView5.Rows[row.Index].Cells[5].Value = distribucion[i].cant;
                            dataGridView5.Rows[row.Index].Cells[4].Value = distribucion[i].capsurtido;
                            dataGridView5.Rows[row.Index].Cells[3].Value = distribucion[i].capespacio;
                            dataGridView5.Rows[row.Index].Cells[2].Value = distribucion[i].existencia;
                            dataGridView5.Rows[row.Index].Cells[20].Value = "0";
                            dataGridView5.Rows[row.Index].Cells[23].Value = distribucion[i].kilos;

                            if (dataGridView5.Rows[row.Index].Cells[18].Value == null)
                            {
                                dataGridView5.Rows[row.Index].Cells[18].Value = distribucion[i].existenciaplanta;
                            }
                        }
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            conLetra = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "No";
            conLetra = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (ReporteForm RF = new ReporteForm(dataGridView5))
            {
                RF.ShowDialog();
            }
        }

        private void dataGridView5_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView5.Rows[e.RowIndex].Cells[21].Value = Double.Parse(dataGridView5.Rows[e.RowIndex].Cells[20].Value.ToString()) * Double.Parse(dataGridView5.Rows[e.RowIndex].Cells[23].Value.ToString());

            }
            catch (Exception)
            {

                dataGridView5.Rows[e.RowIndex].Cells[21].Value = 0;
            }
        }
    }
}
