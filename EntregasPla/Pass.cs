using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;

namespace EntregasPla
{
    
    public partial class Pass : MaterialForm
    {
        public static string PassPasa { get; set; }
        public Pass()
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

            button1.BackColor = ColorTranslator.FromHtml("#76CA62");
            button7.BackColor = ColorTranslator.FromHtml("#D66F6F");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PassPasa = textBox1.Text;
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PassPasa = "Cancelado";
            this.Dispose();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                PassPasa = textBox1.Text;
                this.Dispose();
            }
        }
    }
}
