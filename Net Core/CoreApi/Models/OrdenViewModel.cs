using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Models
{
    public class OrdenViewModel
    {
        public int IdOrden { get; set; }
        public int IdCliente { get; set; }
        public int IdLibro { get; set; }
        public string FechaOrden { get; set; }      
        public string NombreCliente { get; set; }
        public string TituloLibro { get; set; }
    }
}
