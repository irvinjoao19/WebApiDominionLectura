using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InspeccionAdicionales
    {
        public int inspeccionAdicionalId { get; set; }
        public int inspeccionId { get; set; }
        public int tipo { get; set; }
        public string descripcion { get; set; }
        public int estado { get; set; }
        public int usuarioId { get; set; }
    }
}
