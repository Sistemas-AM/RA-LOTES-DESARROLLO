﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class EditarCliente : MaterialForm
    {
        SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.OrdenesTrabajo);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        string TipoCli;
        public static int Guardo;

        public EditarCliente()
        {
            InitializeComponent();
            IdText.Text = Form1.TipoCli;
            NombreText.Text = Form1.Nombre2;
            DescripcionText.Text = Form1.Descripcion2;

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
            BtnGuardar.BackColor = ColorTranslator.FromHtml("#76CA62");
            btnSalir.BackColor = ColorTranslator.FromHtml("#D66F6F");
        }

        private void EditarCliente_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Guardo = 0;
            this.Close();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NombreText.Text) || !string.IsNullOrEmpty(DescripcionText.Text))
            {
                Guardar(IdText.Text);
                Guardo = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("LLENAR LOS DATOS", "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Guardar(string TipoCli)
        {
            try
            {
                string sql = "update TipoCliente set Nombre = @Nombre, Descripcion = @Descripcion where Id_TipoCli = @Id_TipoCli";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection1);
                cmd.Parameters.AddWithValue(@"Id_TipoCli", IdText.Text);
                cmd.Parameters.AddWithValue("@Nombre", NombreText.Text);
                cmd.Parameters.AddWithValue("@Descripcion", DescripcionText.Text);

                sqlConnection1.Open();
                cmd.ExecuteNonQuery();
                sqlConnection1.Close();
                MessageBox.Show("ACTUALIZADO CON ÉXITO", "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                SqlError err = ex.Errors[0];
                string mensaje = string.Empty;
                MessageBox.Show(err.ToString(), "AVISO DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sqlConnection1.Close();
            }
        }
    }
}