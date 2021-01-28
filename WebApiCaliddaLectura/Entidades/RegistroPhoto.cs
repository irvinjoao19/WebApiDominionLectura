using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class RegistroPhoto
    {
        public int iD_Registro { get; set; }
        public string rutaFoto { get; set; }
        public string fecha_Sincronizacion_Android { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public int estado { get; set; }
    }
}
