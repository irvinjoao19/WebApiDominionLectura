using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Login
    {
        public int iD_Operario { get; set; }
        public string operario_Login { get; set; }
        public string operario_Contrasenia { get; set; }
        public string operario_Nombre { get; set; }
        public int operario_EnvioEn_Linea { get; set; }
        public string tipoUsuario { get; set; }
        public string estado { get; set; }
        public int lecturaManual { get; set; }
        public string mensaje { get; set; }
    }
}
