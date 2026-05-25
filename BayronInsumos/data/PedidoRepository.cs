using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BayronInsumos.Models;

namespace BayronInsumos;

public class PedidoRepository : IPedidoRepository
{
    private readonly AplicacionDBcontext _Context; 

    public PedidoRepository(AplicacionDBcontext context)
    {
        _Context = context;
    }

    public async Task<int> crearPedido(Pedido nuevoPedido)
    {
         _Context.Add(nuevoPedido); 
         await _Context.SaveChangesAsync();   
         return nuevoPedido.id;
    }

    public async Task<Pedido?> BuscarPorId(int PedidoId)
    {
        return await _Context.pedidos
            .Include(p => p.Detalles)
            .FirstOrDefaultAsync(p => p.id == PedidoId);
    }
    
    // Tu lógica general: si pasa a entregado, clava la fecha de entrega
    public async Task<bool> modificarEstado(int pedidoId, EstadoPedido nuevoEstado)
    {
        var pedidoExistente = await this.BuscarPorId(pedidoId);
        if (pedidoExistente == null) return false;
        
        pedidoExistente.estado = nuevoEstado; 

        if (nuevoEstado == EstadoPedido.Entregado)
        {
            pedidoExistente.fechaEntrega = DateTime.Now; 
        }
        
        int filasAfectadas = await _Context.SaveChangesAsync();
        return filasAfectadas > 0;
    }

    // Tu método de cancelar
    public async Task<bool> CancelarPedido(int pedidoId)
    {
        return await this.modificarEstado(pedidoId, EstadoPedido.Cancelado);
    }
}