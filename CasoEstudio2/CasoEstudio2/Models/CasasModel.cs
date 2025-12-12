using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasoEstudio2.Models
{
    public class CasasModel
    {
        public long IdCasa { get; set; }
        public string DescripcionCasa { get; set; }
        public decimal PrecioCasa { get; set; }
        public string UsuarioAlquiler { get; set; }
        public DateTime? FechaAlquiler { get; set; }

        // Para vista de Consulta
        public string Estado { get; set; }
        public string FechaAlquilerString { get; set; }
    }
}