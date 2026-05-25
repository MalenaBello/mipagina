
using BayronInsumos;
using BayronInsumos.Models;

namespace BayronInsumos.Data
{
    public interface IPedidoRepository
    {
        Task<int> CrearPedido(Pedido nuevoPedido);
        Task<Pedido?> BuscarPorId(int pedidoId);
        Task<bool> modificarEstado(int pedidoId, EstadoPedido nuevoEstado);
        Task<IEnumerable<Pedido>> ListarPedidosTodos();
        Task<IEnumerable<Pedido>> ListarPedidosPendientes();
        Task<bool> CancelarPedido(int pedidoId);
        Task<EstadoPedido?> mostrarEstado(int idPedido);
    }
}