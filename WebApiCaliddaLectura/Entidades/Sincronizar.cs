using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sincronizar
    {
        public int sincronizarId { get; set; }
        public List<Suministro> suministrosCortes { get; set; }
        public List<Suministro> suministroReconexion { get; set; } 
    }
}
