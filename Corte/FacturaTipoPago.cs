using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corte
{
    public class FacturaTipoPago
    {
        public int tipo { set; get; }
        public string banco { set; get; }
        public string serie { set; get; }
        public string folio { set; get; }
        public string timbrada { set; get; }
        public float importe { set; get; }
    }
}
