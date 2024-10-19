using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistribucionMateriales
{
    class Distribucion
    {
        public int cidproducto { set; get; }
        public string ccodigoproducto { set; get; }
        public string cnombreproducto { set; get; }
        public string letra { set; get; }
        public string capespacio { set; get; }
        public float capsurtido { set; get; }
        public float existencia { set; get; }
        public float cant { set; get; }
        public float existenciaplanta { set; get; }
        public float cedisEnMatriz { set; get; }
        public float desabasto { set; get; }

        public string kilos { set; get; }
    }
}
