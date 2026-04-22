namespace BayronInsumos;
public class CarritoItem
{
    public int Id { get; set; }

    // Conexión con el Carrito
    public int CarritoId { get; set; }
    public Carrito Carrito { get; set; } = null!;

    // Conexión con el Producto
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;

    // El dato propio de esta relación
    public int Cantidad { get; set; }
}