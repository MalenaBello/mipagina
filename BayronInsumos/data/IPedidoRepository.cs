namespace BayronInsumos;

public interface IPedidoRepository 
{
    Task<int> crearPedido (Pedido nuevoPedido);
    Task <bool> confirmarPedido (int pedidoId);
    Task <bool> modificarEstado (int pedidoId, string nuevoEstado);

    Task <Pedido?> BuscarPorId (int pedidoId);

    Task <bool> confirmarEntrega (int pedidoId);
    Task <IEnumerable<Pedido>> ListarPedidosPendientes (); 
    Task <EstadoPedido> mostrarEstado (int idPedido);

}