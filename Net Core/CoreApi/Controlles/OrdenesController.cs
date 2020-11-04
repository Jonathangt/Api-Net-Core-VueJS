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
using CoreApi.Models;

namespace CoreApi.Controlles
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly ApiBaseContext _context;

        public OrdenesController(ApiBaseContext context)
        {
            _context = context;
        }

        // GET: api/Ordenes
        [HttpGet]     
        public async Task<IEnumerable<Orden>> Index()
        {
            var listaOrden = await _context.dbOrden.ToListAsync();
            return listaOrden.Select(x => new Orden()
            {
                IdOrden = x.IdOrden,
                IdCliente = x.IdCliente,
                IdLibro = x.IdLibro,
                FechaOrden = x.FechaOrden
            });
        }

        // GET: api/orden/Listar
       /* [HttpGet("[action]")]
        public async Task<IEnumerable<OrdenViewModel>> Listar()
        {
            var ordenes = await _context.dbOrden.Include(x => x.Cliente.Nombre)
                                                  .Include(x => x.Libro.Titulo)                                                                                                 
                                                  .ToListAsync();
            return ordenes.Select(x => new OrdenViewModel()
            {
                IdOrden = x.IdOrden,
                IdCliente = x.IdCliente,
                NombreCliente = x.Cliente.Nombre,

                IdLibro = x.IdLibro,
                TituloLibro = x.Libro.Titulo,
                FechaOrden = x.FechaOrden,               
            });

        }*/


        [HttpPost("[action]")]
        public async Task<Response> create([FromBody] Orden parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validacion por duplicados
                var existe = await _context.dbOrden.FirstOrDefaultAsync
                        (x => x.IdOrden == parametro.IdOrden);
                if (existe != null)
                {
                    respuesta.CodTransaccion = 1;
                    respuesta.MsgTransaccion = "Orden ya existe";
                    return respuesta;
                }

                var orden = new Orden()
                {
                    IdCliente = parametro.IdCliente,                  
                    IdLibro = parametro.IdLibro,
                    FechaOrden = parametro.FechaOrden,                  
                };

                _context.dbOrden.Add(orden);
                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Orden ingresada correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Error al guardar el registro la orden";
            }

            return respuesta;
        }


        // POST api/Orden/update
        [HttpPut("[action]")]
        public async Task<Response> update([FromBody] Orden parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validar si existe el país
                var existe = await _context.dbOrden.FirstOrDefaultAsync(x => x.IdOrden == parametro.IdOrden);
                if (existe == null)
                {
                    respuesta.CodTransaccion = 2;
                    respuesta.MsgTransaccion = "Orden no existe";
                    return respuesta;
                }

                existe.IdCliente = parametro.IdCliente;
                existe.IdLibro = parametro.IdLibro;
                existe.FechaOrden = parametro.FechaOrden;               

                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Orden modificado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Ocurrió error al modificar la Orden";
            }

            return respuesta;
        }

        // POST api/Orden/buscarPorId
        [HttpPost("[action]")]
        public async Task<Orden> buscarPorId([FromBody] Orden parametro)
        {
            try
            {
                var existe = await _context.dbOrden.FirstOrDefaultAsync(x => x.IdOrden == parametro.IdOrden);

                if (existe != null)
                {
                    return new Orden()
                    {
                        IdOrden = existe.IdOrden,
                        IdCliente = existe.IdCliente,
                        IdLibro = existe.IdLibro,
                        FechaOrden = existe.FechaOrden,                      
                    };
                }
                else
                {
                    return new Orden();
                }
            }
            catch (Exception ex)
            {
                return new Orden();
            }
        }

        // POST api/Orden/delete
        [HttpPost("[action]")]
        public async Task<Response> delete([FromBody] Orden parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validar si existe el país
                var existe = await _context.dbOrden.FirstOrDefaultAsync(x => x.IdOrden == parametro.IdOrden);
                if (existe == null)
                {
                    respuesta.CodTransaccion = 2;
                    respuesta.MsgTransaccion = "Orden no existe";
                    return respuesta;
                }

                _context.dbOrden.Remove(existe);
                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Orden eliminado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Ocurrió error al eliminar Orden";
            }

            return respuesta;
        }
    }
}