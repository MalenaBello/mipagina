using Microsoft.EntityFrameworkCore;

namespace BayronInsumos;

public class CarritoRepository: ICarritoRepository
{
    private readonly AplicacionDBcontext _Context; 

    public CarritoRepository (AplicacionDBcontext context)
    {
        _Context = context;
    }
    public Carrito? ObtenerPorId (int id) => _Context.Carritos.Include(c => c.ITem).ThenInclude(i => i.productoId).FirstOrDefault(c => c.id ==id);

    public void agregarProducto (int carritoId, int productoId, int cantidad)
    {
        var itemExis = _Context.ITemsCarrito.FirstOrDefault(i => i.CarritoId == carritoId && i.ProductoId ==productoId);
        if ( itemExis != null)
        {
            itemExis.Cantidad += cantidad; // si existia lo sumamos 
        }
    } 
    public void eliminarProducto (int carritoId, int productoId)
    {
        //buscamos el item especifico 
        var item = _Context.ITemsCarrito.FirstOrDefault(i => i.CarritoId == carritoId && i.ProductoId == productoId);
        if (item == null) return;
        if (item.Cantidad > 1)
        {
            item.Cantidad--; 
        }
        else {
            _Context.ITemsCarrito.Remove(item);
            
        }
        _Context.SaveChanges();
        }
    
    public void vaciarCarrito  (int carritoId)
    {
        var item = this.ObtenerPorId(carritoId);
        _Context.Carritos.RemoveRange(item);
        _Context.SaveChanges();
    }
}