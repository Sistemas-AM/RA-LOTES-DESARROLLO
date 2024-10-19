using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace Cotizaciones
{
    public class DBHelper
    {
        private static string AcerosMexicoDBString = Principal.Variablescompartidas.Aceros;
        private static string ReportesAMSADBString = Principal.Variablescompartidas.ReportesAmsa;
        internal static List<Sucursal> getSucursales()
        {
            string sql = "SELECT sucursal, ultimoFolio, letra, idalmacen FROM folios";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Sucursal>(sql).ToList();

            }
        }

        internal static List<Cliente> getClientes(bool isAdminpaq)
        {
            if (isAdminpaq)
            {
                string sql = "SELECT CCODIGOCLIENTE, CRAZONSOCIAL, CRFC FROM admClientes WHERE CESTATUS = 0";
                using (var connection = new SqlConnection(AcerosMexicoDBString))
                {
                    return connection.Query<Cliente>(sql, buffered: false).ToList();
                }
            }
            else
            {
                string sql = "SELECT id_ctelocal, nombre, rfc FROM ctelocal";
                using (var connection = new SqlConnection(ReportesAMSADBString))
                {
                    return connection.Query<Cliente>(sql).ToList();
                }
            }
        }
        internal static List<Cliente> searchCliente(bool isAdminpaq, string like)
        {
            if (isAdminpaq)
            {
                string sql = "SELECT CCODIGOCLIENTE, CRAZONSOCIAL, CRFC FROM admClientes WHERE CRAZONSOCIAL LIKE '" + like + "%'";
                using (var connection = new SqlConnection(AcerosMexicoDBString))
                {
                    return connection.Query<Cliente>(sql, buffered: false).ToList();
                }
            }
            else
            {
                // To be changed to ReportesAMSA
                string sql = "SELECT nombre, rfc FROM ctelocal";
                using (var connection = new SqlConnection(ReportesAMSADBString))
                {
                    return connection.Query<Cliente>(sql).ToList();
                }
            }
        }
        internal static List<ClienteActual> getDatosDelCliente(string codigoDelCliente, bool isAdminpaq)
        {
            string sql = isAdminpaq ? "select ccodigocliente,crazonsocial, crfc,cnombrecalle,cnumeroexterior,ccolonia,ccodigopostal, " +
                            "ctelefono1,cemail,cciudad, cestado, cpais from admclientes inner join admDomicilios on " +
                            "CIDCLIENTEPROVEEDOR = CIDCATALOGO and CTIPOCATALOGO = 1 where CCODIGOCLIENTE = @CCODIGOCLIENTE" :
                            "select crazonsocial, crfc,cnombrecalle,cnumeroexterior,ccolonia,ccodigopostal, " +
                            "ctelefono01,cemail,cciudad, cestado, cpais from ctelocal WHERE CRFC = @CRFC";
            using (var connection = new SqlConnection(isAdminpaq ? AcerosMexicoDBString : ReportesAMSADBString))
            {
                return isAdminpaq ?
                    connection.Query<ClienteActual>(sql, new { CCODIGOCLIENTE = codigoDelCliente }).ToList()
                    : connection.Query<ClienteActual>(sql, new { CRFC = codigoDelCliente }).ToList();
            }
        }
        internal static void updateFolio(string sucursalSeleccionada, int folioNuevo)
        {
            string sql = "UPDATE folios SET ultimoFolio = @folio WHERE sucursal = @sucursal;";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                var renglonesAfectados = connection.Execute(sql, new { folio = folioNuevo, sucursal = sucursalSeleccionada });
                System.Windows.Forms.MessageBox.Show("Renglones de folio cambiados: " + renglonesAfectados);
            }
        }
        internal static int getProductoId(string codigoproducto)
        {
            string sql = "SELECT CIDPRODUCTO FROM admProductos WHERE CCODIGOPRODUCTO = @codigoProducto;";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                try
                {

                    return connection.QueryFirst<int>(sql, new { codigoProducto = codigoproducto });
                }
                catch (InvalidOperationException)
                {
                    System.Windows.Forms.MessageBox.Show("No se encontró el código del producto");
                    return 0;
                }
            }
        }
        internal static string getProductoSp(string codigoproducto)
        {
            string sql = "SELECT CTextoExtra1 FROM admProductos WHERE CCODIGOPRODUCTO = @codigoProducto;";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                try
                {
                    return connection.QueryFirst<string>(sql, new { codigoProducto = codigoproducto });

                }
                catch (InvalidOperationException)
                {
                    return "";
                }


            }
        }

        internal static void guardaCotizacion(Cotizacion cotizacion)
        {
            string sql = "INSERT INTO bdcotizao(sucursal, serie, folio, fecha, cliente, tipocot, codigopro, cantidad, precio, importe, idproducto, tipo, iva, sp, descto, pago, nombre, rfc, direccion, numero, telefono, colonia, cp, mail, pais, ciudad, estado, surtida, kilos, observa, atencion, solicito) " +
                "VALUES (@sucursal, @serie, @folio, @fecha, @cliente, @tipocot, @codigopro, @cantidad, @precio, @importe, @idproducto, @tipo, @iva, @sp, @descto, @pago, @nombre, @rfc, @direccion, @numero, @telefono, @colonia, @cp, @mail, @pais, @ciudad, @estado, @surtida, @kilos, @observa, @atencion, @solicito);";

            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                var renglonesAfectados = connection.Execute(sql, new
                {
                    sucursal = cotizacion.sucursal,
                    serie = cotizacion.serie,
                    folio = cotizacion.folio,
                    fecha = cotizacion.fecha,
                    cliente = cotizacion.cliente,
                    tipocot = cotizacion.tipocot,
                    codigopro = cotizacion.codigopro,
                    cantidad = cotizacion.cantidad,
                    precio = cotizacion.precio,
                    importe = cotizacion.importe,
                    idproducto = cotizacion.idproducto,
                    tipo = cotizacion.tipo,
                    iva = cotizacion.iva,
                    sp = cotizacion.sp,
                    descto = cotizacion.descto,
                    pago = cotizacion.pago,
                    nombre = cotizacion.nombre,
                    rfc = cotizacion.rfc,
                    direccion = cotizacion.direccion,
                    numero = cotizacion.numero,
                    telefono = cotizacion.telefono,
                    colonia = cotizacion.colonia,
                    cp = cotizacion.cp,
                    mail = cotizacion.mail,
                    pais = cotizacion.pais,
                    ciudad = cotizacion.ciudad,
                    estado = cotizacion.estado,
                    surtida = cotizacion.surtida,
                    kilos = cotizacion.kilos,
                    observa = cotizacion.observa,
                    atencion = cotizacion.atencion,
                    solicito = cotizacion.solicito
                });
                //System.Windows.Forms.MessageBox.Show("Renglones afectados: " + renglonesAfectados);
            }
        }
        internal static List<ProductoCorto> getProductos()
        {
            string sql = "select CCODIGOPRODUCTO,CNOMBREPRODUCTO from admProductos;";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<ProductoCorto>(sql).ToList();
            }
        }
        internal static List<ProductoCorto> searchProducto(string like)
        {
            string sql = "select CCODIGOPRODUCTO,CNOMBREPRODUCTO from admProductos WHERE CNOMBREPRODUCTO LIKE CONCAT(@Like,'%');";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.Query<ProductoCorto>(sql, new { Like = like }).ToList();
            }
        }
        internal static Producto getProducto(string codigoProducto)
        {
            string sql = "select CIDPRODUCTO, CCODIGOPRODUCTO,CNOMBREPRODUCTO,CPRECIO1, CPESOPRODUCTO, CCONTROLEXISTENCIA from admProductos WHERE CCODIGOPRODUCTO = @CodigoProducto;";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {
                return connection.QueryFirst<Producto>(sql, new { codigoProducto = codigoProducto });
            }
        }

        internal static bool existeCotizacion(int folio, string sucursal)
        {
            List<Cotizacion> cotizaciones = new List<Cotizacion>();
            string sql = "SELECT id_BdCotizao FROM bdcotizao WHERE folio = @folio and sucursal = @sucursal;";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {

                cotizaciones = connection.Query<Cotizacion>(sql, new { folio = folio, sucursal = sucursal }).ToList();
                if (cotizaciones.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }
        internal static List<Cotizacion> getCotizaciones(string sucursal, bool isAdminpaq)
        {
            string sql = "select folio,fecha,sum(importe+iva) as totcot from bdcotizao where sucursal = @Sucursal and fecha >= DATEADD(DAY, -20, getdate()) group by folio,fecha ORDER BY folio desc;";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Cotizacion>(sql, new { Sucursal = sucursal }).ToList();
            }
        }
        internal static List<Cotizacion> getCotizacion(DateTime fecha, int folio, bool isAdminpaq, string sucursal)
        {
            string sql = "select * from bdcotizao where fecha = @Fecha and folio = @Folio and sucursal = @Sucursal";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Cotizacion>(sql, new { Fecha = fecha, Folio = folio, Sucursal = sucursal }).ToList();
            }
        }
        internal static void eliminaCotizaciones(int folio, string sucursal)
        {
            string sql = "DELETE FROM bdcotizao WHERE folio = @folio and sucursal = @sucursal;";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                var renglonesAfectados = connection.Execute(sql, new { folio = folio, sucursal = sucursal });
                System.Windows.Forms.MessageBox.Show("Renglones Actualizados: " + renglonesAfectados);
            }
        }
        internal static void guardaCliente(ClienteActual cliente)
        {
            string sql1 = "DELETE FROM ctelocal WHERE CRFC = @CRFC;";
            string sql2 = "INSERT INTO ctelocal(CRAZONSOCIAL, CRFC, CNOMBRECALLE, CNUMEROEXTERIOR, CTELEFONO01, CCOLONIA, CCODIGOPOSTAL, CEMAIL, CPAIS, CCIUDAD, CESTADO) " +
                "VALUES (@nombre, @rfc, @direccion, @numero, @telefono, @colonia, @cp, @mail, @pais, @ciudad, @estado) ;";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                var renglonesAfectados = connection.Execute(sql1, new { CRFC = cliente.CRFC });
                //System.Windows.Forms.MessageBox.Show("Renglones afectados: " + renglonesAfectados);
            }
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                var renglonesAfectados = connection.Execute(sql2, new
                {
                    nombre = cliente.CRAZONSOCIAL,
                    rfc = cliente.CRFC,
                    direccion = cliente.CNOMBRECALLE,
                    numero = cliente.CNUMEROEXTERIOR,
                    telefono = cliente.CTELEFONO01,
                    colonia = cliente.CCOLONIA,
                    cp = cliente.CCODIGOPOSTAL,
                    mail = cliente.CEMAIL,
                    pais = cliente.CPAIS,
                    ciudad = cliente.CCIUDAD,
                    estado = cliente.CESTADO
                });
                //System.Windows.Forms.MessageBox.Show("Renglones afectados: " + renglonesAfectados);
            }
        }
        internal static float getExistencias(string CIDPRODUCTO, int idalmacen)
        {
            string sql = "select centradasperiodo" + DateTime.Now.Month + ",csalidasperiodo" + DateTime.Now.Month + " from admExistenciaCosto where CIDPRODUCTO = @producto and " +
            "CIDALMACEN = @almacen and CIDEJERCICIO = '"+ Principal.Variablescompartidas.Ejercicio + "'";
            using (var connection = new SqlConnection(AcerosMexicoDBString))
            {

                try
                {
                    Existencia existencia = connection.QueryFirst<Existencia>(sql, new { producto = CIDPRODUCTO, almacen = idalmacen });
                    return existencia.getExistencia(DateTime.Now.Month);
                }
                catch (InvalidOperationException)
                {
                    return 0;
                }

            }
        }
        internal static List<Sucursales> getDatosSucursales()
        {
            string sql = "select * from datger";
            using (var connection = new SqlConnection(ReportesAMSADBString))
            {
                return connection.Query<Sucursales>(sql).ToList();
            }
        }

    }
}
