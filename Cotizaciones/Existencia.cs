using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cotizaciones
{
    class Existencia
    {
        public float CENTRADASPERIODO1 { set; get; }
        public float CENTRADASPERIODO2 { set; get; }
        public float CENTRADASPERIODO3 { set; get; }
        public float CENTRADASPERIODO4 { set; get; }
        public float CENTRADASPERIODO5 { set; get; }
        public float CENTRADASPERIODO6 { set; get; }
        public float CENTRADASPERIODO7 { set; get; }
        public float CENTRADASPERIODO8 { set; get; }
        public float CENTRADASPERIODO9 { set; get; }
        public float CENTRADASPERIODO10 { set; get; }
        public float CENTRADASPERIODO11 { set; get; }
        public float CENTRADASPERIODO12 { set; get; }
        public float CSALIDASPERIODO1 { set; get; }
        public float CSALIDASPERIODO2 { set; get; }
        public float CSALIDASPERIODO3 { set; get; }
        public float CSALIDASPERIODO4 { set; get; }
        public float CSALIDASPERIODO5 { set; get; }
        public float CSALIDASPERIODO6 { set; get; }
        public float CSALIDASPERIODO7 { set; get; }
        public float CSALIDASPERIODO8 { set; get; }
        public float CSALIDASPERIODO9 { set; get; }
        public float CSALIDASPERIODO10 { set; get; }
        public float CSALIDASPERIODO11 { set; get; }
        public float CSALIDASPERIODO12 { set; get; }

        public float getExistencia(int periodo)
        {
            switch (periodo)
            {
                case 1:
                    return CENTRADASPERIODO1 - CSALIDASPERIODO1;
                case 2:
                    return CENTRADASPERIODO2 - CSALIDASPERIODO2;
                case 3:
                    return CENTRADASPERIODO3 - CSALIDASPERIODO3;
                case 4:
                    return CENTRADASPERIODO4 - CSALIDASPERIODO4;
                case 5:
                    return CENTRADASPERIODO5 - CSALIDASPERIODO5;
                case 6:
                    return CENTRADASPERIODO6 - CSALIDASPERIODO6;
                case 7:
                    return CENTRADASPERIODO7 - CSALIDASPERIODO7;
                case 8:
                    return CENTRADASPERIODO8 - CSALIDASPERIODO8;
                case 9:
                    return CENTRADASPERIODO9 - CSALIDASPERIODO9;
                case 10:
                    return CENTRADASPERIODO10 - CSALIDASPERIODO10;
                case 11:
                    return CENTRADASPERIODO11 - CSALIDASPERIODO11;
                case 12:
                    return CENTRADASPERIODO12 - CSALIDASPERIODO12;
            }

            return 9999999999;
        }
       
    }
}
