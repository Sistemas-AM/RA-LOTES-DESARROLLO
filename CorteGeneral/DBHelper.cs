using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace CorteGeneral
{
    class DBHelper
    {
        private static string AcerosMexicoDBString = Principal.Variablescompartidas.Aceros;
        private static string ReportesAMSADBString = Principal.Variablescompartidas.ReportesAmsa;
        internal static List<string> getSucursales()
        {
            string sql = @"SELECT sucursal FROM folios 
where sucursal not in ('FM', 'OFI', 'CD', 'TMP', 'CDM', 'MNC', 'PPT')";
            //string sql = "SELECT sucursal FROM folios where sucursal = 'PLA'";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<string>(sql).ToList();
            }
        }
        internal static List<Factura> getFacturas(string sucursal, DateTime fecha)
        {
            string sql = "spFacturasCorteGeneral";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<Factura>(sql, new { sucursal = sucursal, fecha = fecha.ToString("MM/dd/yyyy") },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        internal static List<Factura> getNotasDeCredito(string sucursal, DateTime fecha)
        {
            string sql = "spNotasDeCreditoCorteGeneral";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<Factura>(sql, new { sucursal = sucursal, fecha = fecha.ToString("MM/dd/yyyy") },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }

        internal static List<EfectivoCaja> getEfectivoCaja(string sucursal, DateTime fecha)
        {
            string sql = "spEfectivos";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<EfectivoCaja>(sql, new { sucursal = sucursal, fecha = fecha.ToString("MM/dd/yyyy") },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        internal static List<Cheque> getCheques(string sucursal, DateTime fecha)
        {
            SqlConnection sqlConnection1 = new SqlConnection(Principal.Variablescompartidas.ReportesAmsa);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            string credito = "";
            string contado = "";
            string notas = "";
            cmd.CommandText = "select idcredito, idcontado, idnotas from folios where sucursal = @sucursal";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@sucursal", sucursal);
            cmd.Connection = sqlConnection1;
            sqlConnection1.Open();
            reader = cmd.ExecuteReader();

            // Data is accessible through the DataReader object here.
            if (reader.Read())
            {
                credito = reader["idcredito"].ToString();
                contado = reader["idcontado"].ToString();
                notas   = reader["idnotas"].ToString();

            }

            sqlConnection1.Close();

            string sql2 = "select ctextoextra3 as concepto, cimporteextra4 as total from admDocumentos where CFECHA = @fecha  and cimporteextra4 != 0 and (CIDCONCEPTODOCUMENTO ='" + credito + "' or CIDCONCEPTODOCUMENTO ='" + contado + "' or CIDCONCEPTODOCUMENTO ='" + notas + "') and ccancelado = 0";
            
            string sql = "SELECT * from bcpcheq where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<Cheque>(sql2, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static List<Vale> getVales(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from dbcapval where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Vale>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static List<SistemaFactura> getSistemaFacturas(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from dbcapfac where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<SistemaFactura>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static List<ResumenSucursal> getResumenSucursal(string sucursal, DateTime fecha)
        {
            string sql = "spResumenSucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<ResumenSucursal>(sql, new { sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd") },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
    }
}
