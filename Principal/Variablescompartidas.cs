using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principal
{
    public class Variablescompartidas
    {

        //CONEXIÓN BUENA
        //public static String Aceros { get; set; } = @"Data Source=192.168.1.127\COMPAC;Initial Catalog=adAMSACONTPAQi;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";
        //public static String ReportesAmsa { get; set; } = @"Data Source=192.168.1.127\COMPAC;Initial Catalog=ReportesAmsa;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";
        //public static String OrdenesTrabajo { get; set; } = @"Data Source=192.168.1.127\COMPAC;Initial Catalog=OrdenesTrabajo;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";
        //public static SqlConnection OrdenesConnection = new SqlConnection(OrdenesTrabajo);

        //CONEXIÓN PRUEBA
        public static String Aceros { get; set; } = @"Data Source=192.168.1.127\COMPAC;Initial Catalog=adAmsa_Pruebas no usar;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";
        //public static String Aceros { get; set; } = @"Data Source=192.168.1.127\COMPAC;Initial Catalog=adPruebas_No_Usar;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";
        public static String ReportesAmsa { get; set; } = @"Data Source=192.168.1.127\COMPAC;Initial Catalog=RepAMSACapX;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";
        public static String OrdenesTrabajo { get; set; } = @"Data Source=192.168.1.127\COMPAC;Initial Catalog=OrdenesTrabajo_Prueba;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";
        public static SqlConnection OrdenesConnection = new SqlConnection(OrdenesTrabajo);


        //Aqui terminan las conexiones.
        //public static String ReportesAmsa { get; set; } = @"Data Source=AMSASERVER1\COMPAC;Initial Catalog=RepAMSACapX;Persist Security Info=True;User ID=sa;Password=Contpaqi1";

        //public static String AcerosPrueba { get; set; } = @"Data Source=AM-SVR-APP01\COMPAC;Initial Catalog=adPruebas_No_Usar;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";



        public static String Flotillas { get; set; } = @"Data Source=AM-SVR-APP01\COMPAC;Initial Catalog=Flotillas;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";

        public static string sucursalcorta { get; set; }
        public static string sucural { get; set; }
        public static string Ejercicio { get; set; } /*= "6";*/
        //public static string Ejercicio { get; set; }
        public static string usuario { get; set; }
        public static string Pass { get; set; }
        public static string Perfil { get; set; }
        public static string Perfil_id { get; set; }

        public static string num { get; set; }

        public static string nombre { get; set; }
        public static string FormatoFecha { get; set; } = "MM/dd/yyyy";
        public static string FormatoFecha2 { get; set; } = "MM/dd/yyyy  HH:mm:ss";

        //public static String nombre_maquina { get; set; }

        //public static String responsable { get; set; }

        public static SqlConnection AcerosConnection = new SqlConnection(Aceros);
        public static SqlConnection RepAmsaConnection = new SqlConnection(ReportesAmsa);
        public static void AbrirAceros(SqlConnection Conexion)
        {
            if (Conexion.State == ConnectionState.Open)
            {
                Conexion.Close();
                Conexion.Open();

            }
            else if (Conexion.State == ConnectionState.Closed)
            {
                Conexion.Open();
            }

        }
        public static void CerrarAceros(SqlConnection Conexion)
        {
            if (Conexion.State == ConnectionState.Open)
            {
                Conexion.Close();
            }
        }

        public static void Actualizar()
        {
            using (SqlConnection connection = new SqlConnection(Aceros))
            {
                connection.Open();
                string sqlQuery = "select top 1 * from admejercicios order by CIDEJERCICIO desc";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        object resultado = reader["CIDEJERCICIO"];
                        if (resultado != DBNull.Value)
                        {
                            Ejercicio = resultado.ToString();
                        }
                    }

                    reader.Close();
                }
            }
        }
    }
}
