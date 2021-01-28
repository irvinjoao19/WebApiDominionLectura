using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Migracion
    {
        public int migrationId { get; set; }
        public List<Servicio> servicios { get; set; }
        public List<Parametro> parametros { get; set; }
        public List<Suministro> suministroLecturas { get; set; }
        public List<Suministro> suministroCortes { get; set; }
        public List<Suministro> suministroReconexiones { get; set; }
        public List<TipoLectura> tipoLecturas { get; set; }
        public List<DetalleGrupo> detalleGrupos { get; set; }
        public List<Reparto> repartoLectura { get; set; }
        public List<Motivo> motivos { get; set; }
        public List<Estado> estados { get; set; }
        public List<FormatoCargo> formatos { get; set; }
        public List<GrandesClientes> clientes { get; set; }
        public List<Marca> marcas { get; set; }
        public string mensaje { get; set; }
    }
}