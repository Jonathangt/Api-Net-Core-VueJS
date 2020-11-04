using System;
using System.Collections.Generic;

namespace Models
{
    public class Libro
    {
        public Libro()
        {
            Orden = new HashSet<Orden>();
        }

        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string AutorNombre { get; set; }
        public string AutorApellido { get; set; }
        public string Categoria { get; set; }
        public string Precio { get; set; }

        //Exposicion a la clase con la cual se tiene relacion
        public ICollection<Orden> Orden { get; set; }
    }
}
