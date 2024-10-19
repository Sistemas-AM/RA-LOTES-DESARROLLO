using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotizacion2021
{
   public  class Componente
    {
        public static string Descripcion { get; set; }
        public static string Tolerancia { get; set; }
        public static string Material { get; set; }
        public static float Precio { get; set; }
        public static float Cantidad { get; set; }
        public static float Total { get; set; }
    }

    public class componentesList
    {
        public string id_Componente { get; set; }
        public string Descripcion { get; set; }
        public string Tolerancia { get; set; }
        public string Material { get; set; }
        public float Precio { get; set; }
        public float Cantidad { get; set; }
        public float Total { get; set; }
    }
}
