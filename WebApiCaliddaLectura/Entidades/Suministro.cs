using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Suministro
    {
        public int iD_Suministro { get; set; }
        public string suministro_Numero { get; set; }
        public string suministro_Medidor { get; set; }
        public string suministro_Cliente { get; set; }
        public string suministro_Direccion { get; set; }
        public string suministro_UnidadLectura { get; set; }
        public string suministro_TipoProceso { get; set; }
        public string suministro_LecturaMinima { get; set; }
        public string suministro_LecturaMaxima { get; set; }
        public string suministro_Fecha_Reg_Movil { get; set; }
        public string suministro_UltimoMes { get; set; }

        //Suminitro Lectura 
        public decimal consumoPromedio { get; set; }
        public string lecturaAnterior { get; set; }
        public string suministro_Instalacion { get; set; }
        public int valida1 { get; set; }
        public int valida2 { get; set; }
        public int valida3 { get; set; }
        public int valida4 { get; set; }
        public int valida5 { get; set; }
        public int valida6 { get; set; }
        public int tipoCliente { get; set; }
        public int flagObservada { get; set; }        
        //Suminitro Corte        
        public int suministro_NoCortar { get; set; }
        // Lo que separa de Lectura y Cortes
        public int estado { get; set; }

        public int suministroOperario_Orden { get; set; }

        public int orden { get; set; }
        public int activo { get; set; }

        public string latitud { get; set; }
        public string longitud { get; set; }
        public string telefono { get; set; }
        public string nota { get; set; }
        public string fechaAsignacion { get; set; }
        public string primeraReconexion { get; set; }
        public string avisoCorte { get; set; }
    }
}
