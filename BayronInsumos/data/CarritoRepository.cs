
using BayronInsumos.Models;
using Microsoft.EntityFrameworkCore;

namespace BayronInsumos.Data
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly AplicacionDBContext _context;

        public CarritoRepository(AplicacionDBContext context)
        {
            _context = context;
        }

        // Obtener Carrito del cliente con sus ítems adentro
        public async Task<Carrito?> ObtenerPorClienteId(int clienteId)
        {
            return await _context.Carritos
                .Include(c => c.Items) // Carga tu lista "Items"
                    .ThenInclude(i => i.Producto) // Carga el producto de cada ítem
                .FirstOrDefaultAsync(c => c.ClienteID == clienteId);
        }

        //  Guardar o Actualizar un producto dentro del carrito
        public async Task<bool> GuardarOActualizarItem(int clienteId, int productoId, int cantidad)
        {
            var carrito = await this.ObtenerPorClienteId(clienteId);

            // Si el cliente no tiene carrito, le creamos la cabecera
            if (carrito == null)
            {
                carrito = new Carrito 
                { 
                    ClienteID = clienteId,
                    fechaCreacion = DateTime.Now
                };
                await _context.Carritos.AddAsync(carrito);
                await _context.SaveChangesAsync(); // Guardamos para generar el id del carrito
            }

            // Buscamos si el producto ya existe en los Items de este carrito
            var itemExistente = carrito.Items.FirstOrDefault(i => i.productoId == productoId);

            if (itemExistente == null)
            {
                // Si el producto es nuevo en el carrito, creamos un CarritoItem
                var nuevoItem = new Models.CarritoItem 
                { 
                    CarritoID = carrito.id, 
                    productoId = productoId, 
                    cantidad = cantidad 
                };
                await _context.CarritoItems.AddAsync(nuevoItem);
            }
            else
            {
                // Si ya existía, le sumamos la cantidad nueva
                itemExistente.cantidad += cantidad;
                _context.CarritoItems.Update(itemExistente);
            }

            int filasAfectadas = await _context.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        //  Eliminar un ítem específico del carrito
        public async Task<bool> EliminarItem(int clienteId, int productoId)
        {
            var carrito = await this.ObtenerPorClienteId(clienteId);
            if (carrito == null) return false;

            var item = carrito.Items.FirstOrDefault(i => i.productoId == productoId);
            if (item == null) return false;

            _context.CarritoItems.Remove(item);
            int filasAfectadas = await _context.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        // Vaciar todo el carrito
        public async Task<bool> VaciarCarrito(int clienteId)
        {
            var carrito = await this.ObtenerPorClienteId(clienteId);
            if (carrito == null) return false;

            _context.CarritoItems.RemoveRange(carrito.Items);
            int filasAfectadas = await _context.SaveChangesAsync();
            return filasAfectadas > 0;
        }
    }
}