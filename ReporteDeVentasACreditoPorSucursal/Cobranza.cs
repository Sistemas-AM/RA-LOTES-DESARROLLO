using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReporteDeVentasACreditoPorSucursal
{
    public class Cobranza
    {
        private string sucursal;
        public string Sucursal { get { return sucursal; } set { sucursal = value; } }
        private DateTime cfecha;
        public DateTime Cfecha { get { return cfecha; } set { cfecha = value; } }
        private string cseriedo01;
        public string Cseriedo01 { get { return cseriedo01; } set { cseriedo01 = value; } }
        private int cfolio;
        public int Cfolio { get { return cfolio; } set { cfolio = value; } }
        private string crazonso01;
        public string Crazonso01 { get { return crazonso01; } set { crazonso01 = value; } }
        private float ctotal;
        public float Ctotal { get { return ctotal; } set { ctotal = value; } }
        private int original;
        public int Original { get { return original; } set { original = value; } }
        private int cr;
        public int Cr { get { return cr; } set { cr = value; } }
        private string ccodigoc01;
        public string Ccodigoc01 { get { return ccodigoc01; } set { ccodigoc01 = value; } }
        private string orden;
        public string Orden { get { return orden; } set { orden = value; } }
        private DateTime fecord;
        public DateTime Fecord { get { return fecord; } set { fecord = value; } }
        private int firmada;
        public int Firmada { get { return firmada; } set { firmada = value; } }

    }
}
