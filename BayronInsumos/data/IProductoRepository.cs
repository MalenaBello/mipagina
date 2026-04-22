using BayronInsumos;

namespace BayronInsumos.Data;

public interface IProductoRepository
{
    IEnumerable<Producto> ObtenerTodo(); //para mostrar la garilla
    void Agregar (Producto newP); // agregar producto
    Producto? ObtenerPorID(int id); //para ver el detalle de un producto
    void actualizarStock(int id, int cantidad); //regla de negocio
    bool restarStock (int id, int cantidadArestar);
}