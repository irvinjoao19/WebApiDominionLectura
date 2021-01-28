using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class FormatoCargo
    {
        public int formatoId { get; set; }
        public int tipo { get; set; }
        public string nombre { get; set; }
        public string abreviatura { get; set; }
        public int estado { get; set; }
    }
}