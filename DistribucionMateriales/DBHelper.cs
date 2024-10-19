using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace DistribucionMateriales
{
    class DBHelper
    {
        private static string AcerosMexicoDBString = Principal.Variablescompartidas.Aceros;
        private static string ReportesAMSADBString = Principal.Variablescompartidas.ReportesAmsa;
        internal static List<string> getSucursales()
        {
            string sql = "SELECT sucursal FROM folios";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<string>(sql).ToList();
            }
        }
        internal static List<Distribucion> getDistribuciones(string sucursal, string inicia_con, bool conLetra = false, string letra = "", float rangomenor = 0, float rangomayor = 3000)
        {
            string sql = "";
            if (conLetra)
            {
                sql = "spDistribucionMaterialesConLetra";
                using (var connection = new SqlConnection(AcerosMexicoDBString))
                {
                    return connection.Query<Distribucion>(sql, new { sucursal = sucursal, inicia_con = inicia_con, letra = letra, rangomenor = rangomenor, rangomayor = rangomayor },
                        commandType: System.Data.CommandType.StoredProcedure).ToList();
                }

            }
            else
            {
                sql = "spDistribucionMaterialesBIEN";
                using (var connection = new SqlConnection(AcerosMexicoDBString))
                {
                    return connection.Query<Distribucion>(sql, new { sucursal = sucursal, inicia_con = inicia_con, rangomenor = rangomenor, rangomayor = rangomayor },
                        commandType: System.Data.CommandType.StoredProcedure).ToList();
                }
            }

        }
        internal static List<string> getLetras()
        {
            string sql = "select distinct letra from clasifi$ order by letra";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<string>(sql).ToList();
            }
        }
        internal static List<Productos> getProductos(string inicia_con)
        {
            string sql = "select ccodigoproducto, cnombreproducto from admproductos where ccodigoproducto like @inicia_con + '%' order by ccodigoproducto";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<Productos>(sql, new { inicia_con = inicia_con}).ToList();
            }
        }
    }
}
