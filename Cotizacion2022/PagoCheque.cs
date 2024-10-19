﻿using System;
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

namespace Cotizacion2022
{
    public partial class PagoCheque : MaterialForm
    {
        public static string ImporteTotal { get; set; }

        public static string Cancelado { get; set; }

        public static string ImporteCheque { get; set; }
        public static string FechaCheque { get; set; }
        public static string NumeroCheque { get; set; }


        int PermiteCerrar = 0;

        public PagoCheque()
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
            button4.BackColor = ColorTranslator.FromHtml("#D66F6F");
        }

        private void PagoCheque_Load(object sender, EventArgs e)
        {
            Total.Text = ImporteTotal;
            TotalReal.Text = ImporteTotal;
        }

        private void Solonumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private double CalculaResultado()
        {
            double ImporteCheque = 0;
            double ImporteReal = double.Parse(TotalReal.Text);

            double Total = 0;

            if (string.IsNullOrEmpty(Cheque.Text))
            {
                ImporteCheque = 0;
            }
            else
            {
                ImporteCheque = double.Parse(Cheque.Text);
            }

            Total = ImporteReal - ImporteCheque;

            return Math.Round(Total, 2);
            
        }

        private void Cheque_TextChanged(object sender, EventArgs e)
        {
            Total.Text = CalculaResultado().ToString();
        }

        private void PagoCheque_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PermiteCerrar == 0)
            {
                e.Cancel = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cancelado = "Si";
            PermiteCerrar = 1;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Cheque.Text) || !string.IsNullOrEmpty(NoCheque.Text))
            {
                if (double.Parse(Total.Text) < 0)
                {
                    MessageBox.Show("El total no puede quedar negativo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    ImporteCheque = Cheque.Text;
                    FechaCheque = Fecha.Value.ToString(Principal.Variablescompartidas.FormatoFecha);
                    NumeroCheque = NoCheque.Text;
        
                    PermiteCerrar = 1;
                    Cancelado = "No";
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show("Captura un valor en importe o el numero de cheque", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
