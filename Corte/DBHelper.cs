using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Data;

namespace Corte
{
    public class DBHelper
    {
        private static string AcerosMexicoDBString = Principal.Variablescompartidas.Aceros;
        private static string ReportesAMSADBString = Principal.Variablescompartidas.ReportesAmsa;
        
        internal static List<Sucursal> getSucursales()
        {
            string sql = "";
            if (Principal.Variablescompartidas.sucursalcorta== "PLA")
            {
                 sql = "SELECT sucursal, sucnom FROM folios where idalmacen in (6, 17)";
            }
            else
            {
                 sql = "SELECT sucursal, sucnom FROM folios";
            }
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Sucursal>(sql).ToList();

            }
        }
        internal static Dictionary<string, float> getCorte(string sucursal, DateTime fecha)
        {
            Sucursal ids;
            string queryFolios = "SELECT * FROM folios WHERE sucursal = @sucursal";
            string query = "SELECT sum(ctotal) as facturado FROM admdocumentos where CIDCONCEPTODOCUMENTO = @id and cfecha = @fecha and ccancelado = 0";

            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                ids = connection.QueryFirst<Sucursal>(queryFolios, new { sucursal = sucursal });
            }

            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                float credito;
                if (float.TryParse(connection.Query<string>(query, new { id = ids.idcredito, fecha = fecha.ToString("yyyy-MM-dd") }).First(), out credito))
                { }
                float contado;
                if (float.TryParse(connection.Query<string>(query, new { id = ids.idcontado, fecha = fecha.ToString("yyyy-MM-dd") }).First(), out contado))
                { }
                float notas;
                if (float.TryParse(connection.Query<string>(query, new { id = ids.idnotas, fecha = fecha.ToString("yyyy-MM-dd") }).First(), out notas))
                { }
                float facturado = contado + credito;
                float ventasDelDia = facturado + notas;
                float ventasDeContado = ventasDelDia - credito;
                return new Dictionary<string, float>()
                {
                    {"credito", credito},
                    {"contado", contado },
                    {"notas", notas},
                    {"facturado", facturado},
                    {"ventas del dia", ventasDelDia},
                    {"ventas de contado", ventasDeContado}
                };
            }
        }
        internal static string getTarjeta(string tipo, DateTime fecha, string sucursal)
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
                notas = reader["idnotas"].ToString();

            }

            sqlConnection1.Close();
            string letra = "";
            string queryLetra = "select letra from folios where sucursal = @sucursal";
            string sql;
            if (tipo.Equals("debito"))
            {
                sql = "select sum(cimporteextra3) as total from admDocumentos where CFECHA = @fecha and CIMPORTEEXTRA3>0 and (CIDCONCEPTODOCUMENTO ='"+credito+"' or CIDCONCEPTODOCUMENTO ='"+contado+"' or CIDCONCEPTODOCUMENTO ='"+notas+"') and ccancelado = 0";

            }
            else
            {
                sql = "select sum(cimporteextra1) as total from admDocumentos where CFECHA = @fecha and CIMPORTEEXTRA1>0 and (CIDCONCEPTODOCUMENTO ='" + credito + "' or CIDCONCEPTODOCUMENTO ='" + contado + "' or CIDCONCEPTODOCUMENTO ='" + notas + "') and ccancelado = 0";
            }
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                letra = connection.Query<string>(queryLetra, new { sucursal = sucursal }).First();
            }
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<string>(sql, new { fecha = fecha.ToString("yyyy-MM-dd")}).First() == null ? "0" : connection.Query<string>(sql, new { fecha = fecha.ToString("yyyy-MM-dd")}).First();
            }
        }
        internal static List<Anticipo> getAnticipos(DateTime fecha, string sucursal)
        {
            string letra = "";
            List<int> CIDSDOCUMENTO;
            List<Anticipo> Anticipos = new List<Anticipo>();
            string queryFolios = "select letra from folios where sucursal = @sucursal";
            string queryMovimientos = "select CIDDOCUMENTO from admMovimientos where cidproducto = 3667 and cfecha = @fecha";
            string queryDocumentos = "select*from admDocumentos where ciddocumento = @documento and CSERIEDOCUMENTO = @letra and CCANCELADO = '0'";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                letra = connection.Query<string>(queryFolios, new { sucursal = sucursal }).First();
            }
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                CIDSDOCUMENTO = connection.Query<int>(queryMovimientos, new { fecha = fecha.ToString("yyyy-MM-dd") }).ToList();
            }
            foreach (int CIDDOCUMENTO in CIDSDOCUMENTO)
            {
                using (var connection = new SqlConnection(AcerosMexicoDBString))
                {
                    Anticipos.Add(connection.Query<Anticipo>(queryDocumentos, new { documento = CIDDOCUMENTO, letra = letra }).FirstOrDefault());
                }
            }
            foreach (Anticipo anticipo in Anticipos)
            {
                if (anticipo != null)
                {
                    Console.Out.WriteLine(anticipo.CRAZONSOCIAL);
                }
            }
            return Anticipos;
        }
        internal static List<Nota> getNotasDeCredito(DateTime fecha, string sucursal)
        {
            string sql = "spNotasDeCredito";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<Nota>(sql, new { sucursal = sucursal, fecha = fecha },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        internal static Consecutivos getConsecutivos(DateTime fecha, string sucursal)
        {
            string sql = "spConsecutivos";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<Consecutivos>(sql, new { fecha = fecha, sucursal = sucursal },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        internal static List<Caja> getCajas(DateTime fecha, string sucursal)
        {
            string sql = "spCajasEnCorte";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Caja>(sql, new { sucursal = sucursal, fecha = fecha },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();

            }
        }
        internal static int getNoCajas(DateTime fecha, string sucursal)
        {
            string sql = "spNoCajasEnCorte";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { sucursal = sucursal, fecha = fecha },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }
        internal static void guardaRetiro(DateTime fecha, string sucursal, string hora, int folio, float monto, string elaboro, string observa)
        {
            string sql = "spGuardaRetiro";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, hora = hora, folio = folio, monto = monto, elaboro = elaboro, observa = observa },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }
        internal static int getFolioRetiro(string sucursal)
        {
            string sql = "select foldin from folios where sucursal = @sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { sucursal = sucursal });

            }
        }
        internal static bool existeCorte(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_dbfgen) from dbfgen where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void guardaCorteInicial(string sucursal, DateTime fecha, string nombre)
        {
            string sql = "INSERT INTO dbfgen(fecha, sucursal, elaboro) VALUES (@fecha, @sucursal, @nombre)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, nombre = nombre });

            }
        }
        internal static int getNoCajas(string sucursal, DateTime fecha)
        {
            string sql = "with cajas as " +
            "(select distinct a.caja, a.elaboro from dbfgen_desgloces as a join dbfgen as b on a.id_dbfgen = b.id_dbfgen where b.fecha = @fecha and b.sucursal = @sucursal) " +
            " select distinct count(caja) from cajas";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }
        internal static int getIdCorte(string sucursal, DateTime fecha)
        {
            string sql = "SELECT id_dbfgen from dbfgen where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }
        internal static void guardaCaja(int caja, float desgloce, int cantidad, string elaboro, int idCorte)
        {
            string sql = "INSERT INTO dbfgen_desgloces(caja, desgloce, cantidad, elaboro, id_dbfgen) VALUES (@caja, @desgloce, @cantidad, @elaboro, @id_dbfgen)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { caja = caja, desgloce = desgloce, cantidad = cantidad, elaboro = elaboro, id_dbfgen = idCorte });

            }
        }
        internal static List<CajasCombo> getCajasCombo(string sucursal, DateTime fecha)
        {
            string sql = "select distinct a.caja, a.elaboro from dbfgen_desgloces as a join dbfgen as b on a.id_dbfgen = b.id_dbfgen where b.fecha=@fecha and b.sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<CajasCombo>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static void guardaTotalEfectivoCorte( float desgloce, int cantidad, string elaboro, int idCorte)
        {
            string sql = "INSERT INTO dbfgen_total(desgloce, cantidad, elaboro, id_dbfgen) VALUES (@desgloce, @cantidad, @elaboro, @id_dbfgen)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new {desgloce = desgloce, cantidad = cantidad, elaboro = elaboro, id_dbfgen = idCorte });

            }
        }
        internal static int getDesglocesTotalEfectivoCorte(string sucursal, DateTime fecha, float desgloce)
        {
            string sql = "select count(id_dbfgen_total) from dbfgen_total as a join dbfgen as b on a.id_dbfgen = b.id_dbfgen where b.fecha = @fecha and b.sucursal = @sucursal and a.desgloce = @desgloce";
            using(var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal, desgloce = desgloce });
            }
        }
        internal static void actualizaDesglocesTotalEfectivoCorte(string sucursal, DateTime fecha, float desgloce, int cantidad)
        {
            string sql1 = "select id_dbfgen_total from dbfgen_total as a join dbfgen as b on a.id_dbfgen = b.id_dbfgen  where b.fecha = @fecha and b.sucursal = @sucursal and desgloce = @desgloce";
            int id_total;
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                id_total = connection.Query<int>(sql1, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal, desgloce = desgloce }).FirstOrDefault();
            }
            string sql2 = "update dbfgen_total set cantidad = @cantidad where id_dbfgen_total = @id_total and desgloce = @desgloce";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
               connection.Query(sql2, new { cantidad = cantidad, id_total = id_total, desgloce = desgloce });
            }
        }
        internal static List<COD> getCOD(DateTime fecha, string sucursal)
        {
            string sql = "spCOD";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<COD>(sql, new { sucursal = sucursal, fecha = fecha },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();

            }
        }
        internal static int getDSVentasf(DateTime fecha, string sucursal)
        {
            string sql = "spDevSVentasf";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd") },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }
        internal static int getDSVentasn(DateTime fecha, string sucursal)
        {
            string sql = "spDevSVentasn";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd") },
                    commandType: System.Data.CommandType.StoredProcedure);

            }
        }
        internal static int getTransferCobranza(string sucursal, DateTime fecha)
        {
            string sql = "select isnull(sum(transfer),0) from faccob where sucursal = @sucursal and fecha = @fecha";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd") });
            }
        }
        internal static int getCobranzaDia(string sucursal, DateTime fecha)
        {
            string sql = "select isnull(sum(transfer+td+tc+cheque+efectivo),0) as total from faccob where sucursal = @sucursal and fecha = @fecha";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd") });
            }
        }
        internal static bool existeCajaChica(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_bdfondo) from bdfondo where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void eliminaCajaChica(string sucursal, DateTime fecha)
        {
            string sql = "delete from bdfondo where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaCajaChica(string sucursal, DateTime fecha,  float desgloce, int cantidad, string elaboro)
        {
            string sql = "INSERT INTO bdfondo(sucursal, fecha, desgloce, cantidad, elaboro) VALUES (@sucursal, @fecha, @desgloce, @cantidad, @elaboro)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, desgloce = desgloce, cantidad = cantidad, elaboro = elaboro });
            }
        }
        internal static bool existeFacturas(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_dbcapfac) from dbcapfac where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void eliminaFacturas(string sucursal, DateTime fecha)
        {
            string sql = "delete from dbcapfac where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaFacturas(string sucursal, DateTime fecha, string concepto, string proveedor, string nofac, float total)
        {
            string sql = "INSERT INTO dbcapfac(sucursal, fecha, concepto, proveedor, nofac, total) VALUES (@sucursal, @fecha, @concepto, @proveedor, @nofac, @total)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, concepto = concepto, proveedor = proveedor, nofac = nofac, total = total });
            }
        }
        internal static List<CajaChica> getCajaChica(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from bdfondo where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<CajaChica>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static List<Factura> getFacturas(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from dbcapfac where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Factura>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }


        internal static bool existeVales(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_dbcapval) from dbcapval where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void eliminaVales(string sucursal, DateTime fecha)
        {
            string sql = "delete from dbcapval where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaVales(string sucursal, DateTime fecha, string concepto, float total)
        {
            string sql = "INSERT INTO dbcapval(sucursal, fecha, concepto, total) VALUES (@sucursal, @fecha, @concepto, @total)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, concepto = concepto, total = total });
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

        internal static bool existeCheques(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_bcpcheq) from bcpcheq where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void eliminaCheques(string sucursal, DateTime fecha)
        {
            string sql = "delete from bcpcheq where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaCheques(string sucursal, DateTime fecha, string concepto, float total)
        {
            string sql = "INSERT INTO bcpcheq(sucursal, fecha, concepto, total) VALUES (@sucursal, @fecha, @concepto, @total)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, concepto = concepto, total = total });
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
                notas = reader["idnotas"].ToString();

            }

            sqlConnection1.Close();

            string sql2 = "select ctextoextra3 as concepto, cimporteextra4 as total from admDocumentos where CFECHA = @fecha and cimporteextra4 != 0 and (CIDCONCEPTODOCUMENTO ='" + credito + "' or CIDCONCEPTODOCUMENTO ='" + contado + "' or CIDCONCEPTODOCUMENTO ='" + notas + "') and ccancelado = 0";
            string sql = "SELECT * from bcpcheq where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<Cheque>(sql2, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }

        internal static bool existeOtros(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_bdcfp) from bdcfp where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void eliminaOtros(string sucursal, DateTime fecha)
        {
            string sql = "delete from bdcfp where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaOtrosbd(string sucursal, DateTime fecha, float cheque, string proveedor, float importe)
        {
            string sql = "INSERT INTO bdcfp(sucursal, fecha, cheque, proveedor, importe) VALUES (@sucursal, @fecha, @cheque, @proveedor, @importe)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, cheque = cheque, proveedor = proveedor, importe = importe });
            }
        }
        internal static List<Otro> getOtros(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from bdcfp where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Otro>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static bool existeDevuelto(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_bdcheqdev) from bdcheqdev where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void eliminaDevuelto(string sucursal, DateTime fecha)
        {
            string sql = "delete from bdcheqdev where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaDevueltodb(string sucursal, DateTime fecha, string cliente, int cheque, float impcheq, float impefec, float comcheq, float comefe, float pago)
        {
            string sql = "INSERT INTO bdcheqdev(sucursal, fecha, cliente, cheque, impcheq, impefec, comcheq, comefe, pago) VALUES (@sucursal, @fecha, @cliente, @cheque, @impcheq, @impefec, @comcheq, @comefe, @pago)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, cliente = cliente, cheque = cheque, impcheq = impcheq, impefec = impefec, comcheq = comcheq, comefe = comefe, pago = pago});
            }
        }
        internal static List<Devuelto> getDevueltos(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from bdcheqdev where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Devuelto>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static bool existeDocumentos(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_bddocto) from bddocto where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void eliminaDocumentos(string sucursal, DateTime fecha)
        {
            string sql = "delete from bddocto where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaDocumentosdb(string sucursal, DateTime fecha, string concepto)
        {
            string sql = "INSERT INTO bddocto(sucursal, fecha, concepto) VALUES (@sucursal, @fecha, @concepto)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, concepto = concepto});
            }
        }
        internal static List<Documento> getDocumentos(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from bddocto where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Documento>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static bool existeEfectivoCobranza(string sucursal, DateTime fecha)
        {
            string sql = "SELECT count(id_dbefvoco) from dbefvoco where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<int>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }) == 0 ? false : true;

            }
        }
        internal static void eliminaEfectivoCobranza(string sucursal, DateTime fecha)
        {
            string sql = "delete from dbefvoco where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaEfectivoCobranza(string sucursal, DateTime fecha, float desgloce, int cantidad, string elaboro)
        {
            string sql = "INSERT INTO dbefvoco(sucursal, fecha, desgloce, cantidad, elaboro) VALUES (@sucursal, @fecha, @desgloce, @cantidad, @elaboro)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, desgloce = desgloce, cantidad = cantidad, elaboro = elaboro });
            }
        }
        internal static List<EfectivoCobranza> getEfectivoCobranza(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from dbefvoco where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<EfectivoCobranza>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }

        internal static void eliminaElaboro(string sucursal, DateTime fecha)
        {
            string sql = "delete from elaborocorte where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal });
            }
        }

        internal static void guardaElaboro(string sucursal, DateTime fecha, string elaboro, float dolares)
        {
            string sql = "INSERT INTO elaborocorte(sucursal, fecha, elaboro, dolares) VALUES (@sucursal, @fecha, @elaboro, @dolares)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha, elaboro = elaboro, dolares = dolares });
            }
        }
        internal static List<Elaboro> getElaboro(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from elaborocorte where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Elaboro> (sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static float getTipoCambio()
        {
            string sql = "SELECT top 1 tc from tipo_cambio";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.QueryFirst<float>(sql);
            }
        }
        internal static List<FacturaTipoPago> getFacturasTipoPago(string sucursal, DateTime fecha)
        {
            string sql = "spFacturasTipoPago";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<FacturaTipoPago>(sql, new { sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd") },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        internal static void actualizaConsecutivoFolio(string sucursal)
        {
            int folio = 0;
            string sqlfolio = "select foldin from folios where sucursal = @sucursal";
            using(var connection = new SqlConnection(ReportesAMSADBString))
            {
                folio = connection.QueryFirst<int>(sqlfolio, new { sucursal = sucursal });
            }
            string sql = "update folios set foldin = @cantidad where sucursal = @sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql, new { cantidad = folio+1, sucursal = sucursal});
            }
        }
        internal static List<Retiro> getRetiros(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from retiros where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Retiro>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }

        internal static List<InventarioCorte> getInventario(string sucursal, DateTime fecha)
        {
            string sql = "spInventarioCorteBIEN";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<InventarioCorte>(sql, new { sucursal = sucursal , fecha = fecha.ToString("yyyy-MM-dd") },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
        internal static void guardaComdev(string sucursal, DateTime fecha, float no_cobrado, float total)
        {
            string sql2 = "delete from comdev where sucursal = @sucursal and fecha = @fecha";
            string sql = "INSERT INTO comdev(sucursal, fecha,no_cobrado, total) VALUES (@sucursal, @fecha, @no_cobrado, @total)";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                connection.Query(sql2, new {sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd") });
                connection.Query(sql, new { sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd"), no_cobrado = no_cobrado, total = total });
            }
        }
        internal static List<Comdev> getComdev(string sucursal, DateTime fecha)
        {
            string sql = "SELECT * from comdev where fecha=@fecha and sucursal=@sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Comdev>(sql, new { fecha = fecha.ToString("yyyy-MM-dd"), sucursal = sucursal }).ToList();
            }
        }
        internal static List<FacturaCancelada> getFacturasCanceladas(string sucursal, DateTime fecha)
        {
            string sql = "spFacturasCanceladas";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<FacturaCancelada>(sql, new { sucursal = sucursal, fecha = fecha.ToString("yyyy-MM-dd") },
                    commandType: System.Data.CommandType.StoredProcedure).ToList();
            }
        }
    }
}
