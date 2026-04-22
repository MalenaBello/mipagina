using BayronInsumos;
using Microsoft.VisualBasic;

namespace BayronInsumos.Data;
public class ProductoRepository: IProductoRepository
{
    private readonly AplicacionDBcontext _context;
    
    public ProductoRepository (AplicacionDBcontext context)
    {
        _context=context;
    }

    //metodo para ver los productos en la garilla. Se devuelve la lista y luego se itera
    public IEnumerable <Producto> ObtenerTodo() => _context.Productos.ToList();
    
    
    //metodo para guardar producto

    public void Agregar( Producto newP)
    {
        _context.Productos.Add(newP);
        _context.SaveChanges();
    }
     
     //para ver el detalle de un producto
    public Producto? ObtenerPorID(int id) => _context.Productos.Find(id); 
     
     //regla de negocio

    public void actualizarStock(int id, int cantidad)
    {
        Producto? productoActualizar = this.ObtenerPorID(id);
        if (productoActualizar != null)
        {
            productoActualizar.precio = cantidad;
            _context.SaveChanges();
        }
        
    }
    //actualizacion de stock para compras
    public bool restarStock (int id,int cantidadArestar)
    {
        Producto? producto = this.ObtenerPorID(id);
        if (producto == null || producto.precio < cantidadArestar) {
            return false;
        }
        producto.stock -= cantidadArestar;
        _context.SaveChanges();
        return true;
    }
     }
