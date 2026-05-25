using BayronInsumos.Models;

namespace BayronInsumos.Data;

public interface IProductoRepository
{
    Task <IEnumerable<Producto>> ObtenerTodo(); //para mostrar la garilla
    Task <bool> Agregar (Producto newP); // agregar producto
    Task <Producto?> ObtenerPorID(int id); //para ver el detalle de un producto
    Task<bool> actualizarStock(int id, int cantidad); //regla de negocio
    Task <bool> restarStock (int id, int cantidadArestar);
}