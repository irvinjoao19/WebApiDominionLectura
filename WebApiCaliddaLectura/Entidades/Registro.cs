using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Registro
    {
        public int iD_Registro { get; set; } // tambien se utiliza para id_Reparto
        public int iD_Operario { get; set; }
        public int iD_Suministro { get; set; }  // para tipo 6 = medidor
        public int suministro_Numero { get; set; }
        public int iD_TipoLectura { get; set; }
        public string registro_Fecha_SQLITE { get; set; }
        public string registro_Latitud { get; set; }
        public string registro_Longitud { get; set; }
        public string registro_Lectura { get; set; }
        public string registro_Confirmar_Lectura { get; set; }
        public string registro_Observacion { get; set; }
        public string grupo_Incidencia_Codigo { get; set; }
        public string registro_TieneFoto { get; set; }
        public string registro_TipoProceso { get; set; }
        public string fecha_Sincronizacion_Android { get; set; }
        public string registro_Constancia { get; set; } // para tipo 6 = contrato
        public string registro_Desplaza { get; set; }
        public string codigo_Resultado { get; set; }
        public string horaActa { get; set; }
        public string suministroCliente { get; set; }
        public string suministroDireccion { get; set; }
        public int tipo { get; set; }
        public int id_Observacion { get; set; }
        public int lecturaManual { get; set; }
        public int motivoId { get; set; }
        public int parentId { get; set; }
        public List<RegistroPhoto> photos { get; set; }
        public RegistroRecibo recibo { get; set; }
    }
}
