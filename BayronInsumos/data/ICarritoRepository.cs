namespace BayronInsumos;

public interface ICarritoRepository
{
    Carrito? ObtenerPorId (int id);

    void agregarProducto (int carritoId, int productoId, int cantidad);

    void eliminarProducto (int carritoId, int productoId);

    void vaciarCarrito  (int carritoId);

}
