namespace BayronInsumos.Models
{
    public class CarritoItem
    {
        public int id { get; set; }
        public int CarritoID { get; set; } // Conecta con el carrito de arriba
        public int productoId { get; set; }
        public int cantidad { get; set; }
        
        // Propiedad de navegación para EF (para poder sacar el precio, nombre, etc.)
        public Producto Producto { get; set; } = null!; 
    }
}