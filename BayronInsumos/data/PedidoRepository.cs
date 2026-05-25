
using Microsoft.EntityFrameworkCore;

using BayronInsumos.Models;

namespace BayronInsumos.Data
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AplicacionDBContext _context;

        public PedidoRepository(AplicacionDBContext context)
        {
            _context = context;
        }

        public async Task<int> CrearPedido(Pedido nuevoPedido)
        {
            await _context.pedidos.AddAsync(nuevoPedido);
            await _context.SaveChangesAsync();
            return nuevoPedido.id; 
        }

        public async Task<Pedido?> BuscarPorId(int pedidoId)
        {
            return await _context.pedidos.FindAsync(pedidoId);
        }

        public async Task<bool> modificarEstado(int pedidoId, EstadoPedido nuevoEstado)
        {
            var pedidoExistente = await this.BuscarPorId(pedidoId);
            if (pedidoExistente == null) return false;

            pedidoExistente.estadoPedido = nuevoEstado; 
            int filasAfectadas = await _context.SaveChangesAsync(); 
            return filasAfectadas > 0;
        }

        public async Task<IEnumerable<Pedido>> ListarPedidosTodos()
        {
            return await _context.pedidos.ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> ListarPedidosPendientes()
        {
            return await _context.pedidos
                .Where(p => p.estadoPedido == EstadoPedido.pendiente)
                .ToListAsync();
        }

        public async Task<bool> CancelarPedido(int pedidoId)
        {
            return await this.modificarEstado(pedidoId, EstadoPedido.cancelado);
        }

        public async Task<EstadoPedido?> mostrarEstado(int idPedido)
        {
            var pedido = await this.BuscarPorId(idPedido);
            if (pedido == null) return null; 
            return pedido.estadoPedido;
        }
    }
}