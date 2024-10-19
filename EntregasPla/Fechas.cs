using System;
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

namespace EntregasPla
{
    public partial class Fechas : MaterialForm
    {
        public static string Fecha1 { get; set; }
        public static string Fecha2 { get; set; }
        public static string Cancelado { get; set; }

        public Fechas()
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

            button7.BackColor = ColorTranslator.FromHtml("#D66F6F");
            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fecha1 = metroDateTime1.Value.ToString(Principal.Variablescompartidas.FormatoFecha);
            Fecha2 = metroDateTime2.Value.ToString(Principal.Variablescompartidas.FormatoFecha);
            Cancelado = "0";
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Cancelado = "1";
            this.Dispose();
        }
    }
}
