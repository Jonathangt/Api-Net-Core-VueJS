using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Orden
    {
        public int IdOrden { get; set; }
        [ForeignKey("idCliente")]
        public int IdCliente { get; set; }
        [ForeignKey("idLibro")]
        public int IdLibro { get; set; }
        public string FechaOrden { get; set; }
       
        //public string NombreCliente { get; set; }
       // public string TituloLibro { get; set; }


        //Exposicion a la clase con la cual se tiene relacion
        public Cliente Cliente { get; set; }
        public Libro Libro { get; set; }


    }
}
