using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotizaciones
{
    public class Cotizacion
    {
        public string sucursal { set; get; }
        public string serie { set; get; }
        public int folio { set; get; }
        public DateTime fecha { set; get; }
        public string cliente { set; get; }
        public int tipocot { set; get; }
        public string codigopro { set; get; }
        public float cantidad { set; get; }
        public float precio { set; get; }
        public float importe { set; get; }
        public int idproducto { set; get; }
        public int tipo { set; get; }
        public float iva { set; get; }
        public string sp { set; get; }
        public float descto { set; get; }
        public string pago { set; get; }
        public string nombre { set; get; }
        public string rfc { set; get; }
        public string direccion { set; get; }
        public string numero { set; get; }
        public string telefono { set; get; }
        public string colonia { set; get; }
        public string cp { set; get; }
        public string mail { set; get; }
        public string pais { set; get; }
        public string ciudad { set; get; }
        public string estado { set; get; }
        public int surtida { set; get; }
        public float kilos { set; get; }
        public string observa { set; get; }
        public string atencion { set; get; }
        public string solicito { set; get; }
        public string totcot { set; get; }
    }
}
