using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Models;
using CoreApi.ResponseM;

namespace CoreApi.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : Controller
    {
        private readonly ApiBaseContext _context;


        public LibrosController(ApiBaseContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<IEnumerable<Libro>> index()
        {
            var listaLibros = await _context.dbLibro.ToListAsync();
            return listaLibros.Select(x => new Libro()
            {
                IdLibro = x.IdLibro,
                Titulo = x.Titulo,
                AutorNombre = x.AutorNombre,
                AutorApellido = x.AutorApellido,
                Categoria = x.Categoria,
                Precio = x.Precio              

            });
        }



        [HttpPost("[action]")]
        public async Task<Response> create([FromBody] Libro parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validacion por duplicados
                var existe = await _context.dbLibro.FirstOrDefaultAsync
                        (x => x.Titulo == parametro.Titulo);
                if (existe != null)
                {
                    respuesta.CodTransaccion = 1;
                    respuesta.MsgTransaccion = "Libro ya existe";
                    return respuesta;
                }

                var libro = new Libro()
                {
                    Titulo = parametro.Titulo,
                    AutorNombre = parametro.AutorNombre,
                    AutorApellido = parametro.AutorApellido,
                    Categoria = parametro.Categoria,
                    Precio = parametro.Precio,                   
                };

                _context.dbLibro.Add(libro);
                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Libro ingresado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Error al guardar el registro del libro";
            }

            return respuesta;
        }


        // POST api/Libro/update
        [HttpPut("[action]")]
        public async Task<Response> update([FromBody] Libro parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validar si existe el país
                var existe = await _context.dbLibro.FirstOrDefaultAsync(x => x.IdLibro == parametro.IdLibro);
                if (existe == null)
                {
                    respuesta.CodTransaccion = 2;
                    respuesta.MsgTransaccion = "Libro no existe";
                    return respuesta;
                }

                existe.Titulo = parametro.Titulo;
                existe.AutorNombre = parametro.AutorNombre;
                existe.AutorApellido = parametro.AutorApellido;
                existe.Categoria = parametro.Categoria;
                existe.Precio = parametro.Precio;
             
                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Libro modificado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Ocurrió error al modificar Libro";
            }

            return respuesta;
        }

        // POST api/Libro/buscarPorId
        [HttpPost("[action]")]
        public async Task<Libro> buscarPorId([FromBody] Libro parametro)
        {
            try
            {
                var existe = await _context.dbLibro.FirstOrDefaultAsync(x => x.IdLibro == parametro.IdLibro);

                if (existe != null)
                {
                    return new Libro()
                    {
                        IdLibro = existe.IdLibro,
                        Titulo = existe.Titulo,
                        AutorNombre = existe.AutorNombre,
                        AutorApellido = existe.AutorApellido,
                        Categoria = existe.Categoria,
                        Precio = existe.Precio ,                     
                    };
                }
                else
                {
                    return new Libro();
                }
            }
            catch (Exception ex)
            {
                return new Libro();
            }
        }

        // POST api/Libro/delete
        [HttpPost("[action]")]
        public async Task<Response> delete([FromBody] Libro parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validar si existe el país
                var existe = await _context.dbLibro.FirstOrDefaultAsync(x => x.IdLibro == parametro.IdLibro);
                if (existe == null)
                {
                    respuesta.CodTransaccion = 2;
                    respuesta.MsgTransaccion = "Libro no existe";
                    return respuesta;
                }

                _context.dbLibro.Remove(existe);
                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Libro eliminado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Ocurrió error al eliminar Libro";
            }

            return respuesta;
        }
    }
}