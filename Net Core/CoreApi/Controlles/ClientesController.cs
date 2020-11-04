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
    public class ClientesController : Controller
    {
        private readonly ApiBaseContext _context;


        public ClientesController(ApiBaseContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<IEnumerable<Cliente>> index()
        {
            var listaClientes = await _context.dbCliente.ToListAsync();
            return listaClientes.Select(x => new Cliente()
            {
                IdCliente = x.IdCliente,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Telefono = x.Telefono,
                Direccion = x.Direccion,
                Ciudad = x.Ciudad,
                Estado = x.Estado,
                CodigoPostal = x.CodigoPostal

            });
        }



        [HttpPost("[action]")]
        public async Task<Response> create([FromBody] Cliente parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validacion por duplicados
                var existe = await _context.dbCliente.FirstOrDefaultAsync
                        (x => x.Nombre == parametro.Nombre || x.Apellido == parametro.Apellido);
                if (existe != null)
                {
                    respuesta.CodTransaccion = 1;
                    respuesta.MsgTransaccion = "Cliente ya existe";
                    return respuesta;
                }

                var cliente = new Cliente() { 
                    Nombre = parametro.Nombre,
                    Apellido = parametro.Apellido,
                    Telefono = parametro.Telefono,
                    Direccion = parametro.Direccion,
                    Ciudad = parametro.Ciudad,
                    Estado = parametro.Estado,
                    CodigoPostal = parametro.CodigoPostal
                };

                _context.dbCliente.Add(cliente);
                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Cliente ingresado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Error al guardar el registro del libro";
            }

            return respuesta;
        }


        // POST api/Cliente/Update
        [HttpPut("[action]")]
        public async Task<Response> update([FromBody] Cliente parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validar si existe el país
                var existe = await _context.dbCliente.FirstOrDefaultAsync(x => x.IdCliente == parametro.IdCliente);
                if (existe == null)
                {
                    respuesta.CodTransaccion = 2;
                    respuesta.MsgTransaccion = "Cliente no existe";
                    return respuesta;
                }

                existe.Nombre = parametro.Nombre;
                existe.Apellido = parametro.Apellido;
                existe.Telefono = parametro.Telefono;
                existe.Direccion = parametro.Direccion;
                existe.Ciudad = parametro.Ciudad;
                existe.Estado = parametro.Estado;
                existe.CodigoPostal = parametro.CodigoPostal;

                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Cliente modificado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Ocurrió error al modificar Cliente";
            }

            return respuesta;
        }

        // POST api/Cliente/BuscarPorId
        [HttpPost("[action]")]
        public async Task<Cliente> BuscarPorId([FromBody] Cliente parametro)
        {
            try
            {
                var existe = await _context.dbCliente.FirstOrDefaultAsync(x => x.IdCliente == parametro.IdCliente);

                if (existe != null)
                {
                    return new Cliente()
                    {
                        IdCliente = existe.IdCliente,
                        Nombre = existe.Nombre,
                        Apellido = existe.Apellido,
                        Telefono = existe.Telefono,
                        Direccion = existe.Direccion,
                        Ciudad = existe.Ciudad,
                        Estado = existe.Estado,
                        CodigoPostal = existe.CodigoPostal,
                    };
                }
                else
                {
                    return new Cliente();
                }
            }
            catch (Exception ex)
            {
                return new Cliente();
            }
        }

        // POST api/Cliente/Delete
        [HttpPost("[action]")]
        public async Task<Response> Delete([FromBody] Cliente parametro)
        {
            var respuesta = new Response();

            try
            {
                // Validar si existe el país
                var existe = await _context.dbCliente.FirstOrDefaultAsync(x => x.IdCliente == parametro.IdCliente);
                if (existe == null)
                {
                    respuesta.CodTransaccion = 2;
                    respuesta.MsgTransaccion = "Cliente no existe";
                    return respuesta;
                }

                _context.dbCliente.Remove(existe);
                await _context.SaveChangesAsync();

                respuesta.CodTransaccion = 0;
                respuesta.MsgTransaccion = "Cliente eliminado correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodTransaccion = 9;
                respuesta.MsgTransaccion = "Ocurrió error al eliminar cliente";
            }

            return respuesta;
        }
    }
}