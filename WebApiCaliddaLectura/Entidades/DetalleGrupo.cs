using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleGrupo
    {
        public int id { get; set; }
        public int iD_DetalleGrupo { get; set; }
        public string grupo { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string abreviatura { get; set; }
        public string estado { get; set; }
        public string descripcionGrupo { get; set; }
        public string pideFoto { get; set; }
        public string noPideFoto { get; set; }
        public string pideLectura { get; set; }
        public int id_Servicio { get; set; }
        public int parentId { get; set; }
        public int ubicaMedidor { get; set; }
    }
}
