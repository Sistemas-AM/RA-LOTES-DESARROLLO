using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corte
{
    class InventarioCorte
    {
        public string CCODIGOPRODUCTO { set; get; }
        public string CNOMBREPRODUCTO { set; get; }
        public float venta { set; get; }
        public float devs { set; get; }
        public float entrada { set; get; }
        public float salida { set; get; }
        public float compras { set; get; }
        public float existencia { set; get; }
    }
}
