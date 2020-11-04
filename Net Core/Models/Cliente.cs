using System;
using System.Collections.Generic;

namespace Models
{
    public class Cliente
    {
        public Cliente()
        {
            Orden = new HashSet<Orden>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
       

        //Exposicion a la clase con la cual se tiene relacion
        public ICollection<Orden> Orden { get; set; }
    }
}
