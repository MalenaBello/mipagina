using System.Collections.Generic;
using System.Threading.Tasks;
using BayronInsumos.Models;

namespace BayronInsumos.Data
{
    public interface IPedidoRepository
    {
        Task<int> crearPedido(Pedido nuevoPedido);
        Task<Pedido?> BuscarPorId(int PedidoId);
        Task<bool> modificarEstado(int pedidoId, EstadoPedido nuevoEstado);
        Task<bool> CancelarPedido(int pedidoId);
        Task<bool> confirmarEntrega(int pedidoId);
        Task<IEnumerable<Pedido>> ListarPedidosPendientes(); 
        Task<EstadoPedido> mostrarEstado(int idPedido);
    }
}
