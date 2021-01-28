using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class InspeccionDetalle
    {
        public int inspeccionDetalleId { get; set; }
        public int inspeccionId { get; set; }        
        public int checkListId { get; set; }
        public int estadoCheckList { get; set; }
        public int estado2CheckList { get; set; }
        public string fecha { get; set; }
        public string observacion { get; set; }
        public string observacion2 { get; set; }
        public int estado { get; set; }
        public int usuarioId { get; set; }
    }
}
