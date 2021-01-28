using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class GrandesClientes
    { 
        public int clienteId { get; set; }
        public string fechaImportacion { get; set; }
        public string archivoImportacion { get; set; }
        public string codigoEMR { get; set; }
        public string nombreCliente { get; set; }
        public string direccion { get; set; }
        public string distrito { get; set; }
        public string fechaAsignacion { get; set; }
        public string fechaEnvioCelular { get; set; }
        public int operarioId { get; set; }
        public int ordenLectura { get; set; }
        public string fechaRegistroInicio { get; set; }
        public string clientePermiteAcceso { get; set; }
        public string fotoConstanciaPermiteAcceso { get; set; }
        public string porMezclaExplosiva { get; set; }
        public string vManoPresionEntrada { get; set; }
        public string fotovManoPresionEntrada  { get; set; } 
        public int marcaCorrectorId { get; set; }
        public string fotoMarcaCorrector { get; set; }
        public string vVolumenSCorreUC { get; set; }
        public string fotovVolumenSCorreUC { get; set; }
        public string vVolumenSCorreMedidor { get; set; }
        public string fotovVolumenSCorreMedidor { get; set; }
        public string vVolumenRegUC { get; set; }
        public string fotovVolumenRegUC { get; set; }
        public string vPresionMedicionUC { get; set; }
        public string fotovPresionMedicionUC { get; set; }
        public string vTemperaturaMedicionUC { get; set; }
        public string fotovTemperaturaMedicionUC { get; set; }
        public string tiempoVidaBateria { get; set; }
        public string fotoTiempoVidaBateria { get; set; }
        public string fotoPanomarica { get; set; }
        public string tieneGabinete { get; set; }
        public string foroSitieneGabinete { get; set; }
        public string presenteCliente { get; set; }
        public string contactoCliente { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public int estado { get; set; }
        public string tomaLectura { get; set; }
        public string fotoTomaLectura { get; set; }
        public string existeMedidor { get; set; }
        public string fotoBateriaDescargada { get; set; }
        public string fotoDisplayMalogrado { get; set; } 
        public string fotoPorMezclaExplosiva { get; set; } 
        public string comentario { get; set; } 
    }
}
