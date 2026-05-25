using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // Para leer la API Key
using System;
using System.Threading.Tasks;
using BayronInsumos.Models;
using BayronInsumos.Data;

namespace BayronInsumos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IConfiguration _configuration; // Para la seguridad

        // Actualizamos el constructor para recibir la configuración
        public PedidoController(IPedidoRepository pedidoRepository, IProductoRepository productoRepository, IConfiguration configuration)
        {
            _pedidoRepository = pedidoRepository;
            _productoRepository = productoRepository;
            _configuration = configuration;
        }

        // 1. POST: api/Pedido (Crear Compra)
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Pedido nuevoPedido)
        {
            if (nuevoPedido == null || nuevoPedido.Detalles == null || nuevoPedido.Detalles.Count == 0)
            {
                return StatusCode(400, "El pedido debe contener al menos un producto.");
            }

            try
            {
                foreach (var detalle in nuevoPedido.Detalles)
                {
                    bool pudoRestar = await _productoRepository.restarStock(detalle.idProducto, detalle.cantidad);

                    if (!pudoRestar)
                    {
                        throw new InvalidOperationException($"No se pudo completar el pedido. Stock insuficiente o producto inexistente (ID: {detalle.idProducto}).");
                    }
                }

                nuevoPedido.estadoPedido = EstadoPedido.pendiente;
                nuevoPedido.fechaDeEntrega = DateTime.Now;

                int resultadoId = await _pedidoRepository.CrearPedido(nuevoPedido);

                return Ok(new { mensaje = "Pedido creado con éxito y stock actualizado de los insumos.", id = resultadoId });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error crítico en el servidor: {ex.Message}");
            }
        }

        // ==========================================
        // MÉTODOS PARA GESTIÓN DEL NEGOCIO
        // ==========================================

        //  GET: api/Pedido (Listar todas las ventas para el panel de administración)
        [HttpGet]
        public async Task<IActionResult> TraerTodos()
        {
            try
            {
                
                var pedidos = await _pedidoRepository.ListarPedidosTodos();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al recuperar los pedidos: {ex.Message}");
            }
        }
        // GET: api/Pedido (listar pedidos pendientes para el panel de admi)
        [HttpGet("Pendientes")]
        public async Task<IActionResult> TraerPendientes([FromHeader(Name = "X-Admin-Key")] string claveRecibida)
        {
            // Validamos seguridad para que nadie Husmee las ventas del negocio
            string claveReal = _configuration["ApiSettings:AdminKey"];
            if (claveRecibida != claveReal)
            {
                return StatusCode(401, "No autorizado.");
            }

            try
            {
                // NOTA: Le va a pedir este método a tu repositorio de pedidos
                var pendientes = await _pedidoRepository.ListarPedidosPendientes();
                return Ok(pendientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al recuperar pedidos pendientes: {ex.Message}");
            }
        }

        // GET: api/Pedido/{id} (Ver el detalle de un pedido específico)
        [HttpGet("{id}")]
        public async Task<IActionResult> TraerPorId(int id)
        {
            try
            {
                // NOTA: Asegurate de que tu IPedidoRepository tenga definido el método ObtenerPorID(id)
                var pedido = await _pedidoRepository.BuscarPorId(id);
                if (pedido == null) return StatusCode(404, $"El pedido ID {id} no existe.");
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al recuperar el pedido: {ex.Message}");
            }
        }

        //PUT: api/Pedido/CambiarEstado/{id} (PROTEGIDO - Solo tu vieja pasa el pedido de 'pendiente' a 'entregado')
        [HttpPut("CambiarEstado/{id}")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] EstadoPedido nuevoEstado, [FromHeader(Name = "X-Admin-Key")] string claveRecibida)
        {
            // Validamos seguridad con la API Key igual que en Productos
            string claveReal = _configuration["ApiSettings:AdminKey"];
            if (claveRecibida != claveReal)
            {
                return StatusCode(401, "No autorizado. La clave de administración es incorrecta o no fue provista.");
            }

            try
            {
                
                bool exito = await _pedidoRepository.modificarEstado(id, nuevoEstado);
                
                if (!exito)
                {
                    return StatusCode(404, $"No se encontró el pedido ID {id} para modificar su estado.");
                }

                return Ok(new { mensaje = $"Estado del pedido #{id} actualizado a {nuevoEstado} con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el estado del pedido: {ex.Message}");
            }
        }
    }
}