using Microsoft.EntityFrameworkCore;

namespace BayronInsumos;

public class PedidoRepository : IPedidoRepository
{
     private readonly AplicacionDBcontext _Context; 

    public PedidoRepository (AplicacionDBcontext context)
    {
        _Context = context;
    }

    public async Task<int> crearPedido (Pedido nuevoPedido)
    {
         _Context.Add(nuevoPedido); // 1.Le decime a EF que queremos guardar este pedido
         await _Context.SaveChangesAsync(); // 2. Le decime que impacte los cambios en el archivo .db el 'await' es para que espere a que el disco termine de escribir.   
         return nuevoPedido.id;
    }

    // el metodo para buscar 
    public async Task<Pedido?> BuscarPorId (int PedidoId)
    {
        return await _Context.pedidos.Include(p => p.Detalles).FirstOrDefaultAsync (p => p.id == PedidoId);

    }
    
    
    public async Task <bool> confirmarPedido (int pedidoId)
    {
       var pedidoExistente= await this.BuscarPorId(pedidoId);
            if (pedidoExistente==null) return false;
            pedidoExistente.estado="confirmado";
    }
    Task <bool> modificarEstado (int pedidoId, string nuevoEstado);

    

    Task <bool> confirmarEntrega (int pedidoId);
    Task <IEnumerable<Pedido>> ListarPedidosPendientes (); 
    Task <EstadoPedido> mostrarEstado (int idPedido);
   
}