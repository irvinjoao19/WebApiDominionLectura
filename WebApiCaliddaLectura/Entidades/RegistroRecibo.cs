using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class RegistroRecibo
    {
        public int reciboId { get; set; } 
        public int repartoId { get; set; }
        public int operarioId { get; set; }
        public int tipo { get; set; }
        public string ciclo { get; set; }
        public int year { get; set; }
        public int piso { get; set; }
        public int formatoVivienda { get; set; }
        public string otrosVivienda { get; set; }
        public int formatoCargoColor { get; set; }
        public string otrosCargoColor { get; set; }
        public int formatoCargoPuerta { get; set; }
        public string otrosCargoPuerta { get; set; }
        public int formatoCargoColorPuerta { get; set; }
        public string otrosCargoColorPuerta { get; set; }
        public int formatoCargoRecibo { get; set; }
        public string dniCargoRecibo { get; set; }
        public string parentesco { get; set; }
        public int formatoCargoDevuelto { get; set; }
        public string fechaMax { get; set; }
        public string fechaEntrega { get; set; }
        public string observacionCargo { get; set; }
        public string firmaCliente { get; set; }
    }
}
