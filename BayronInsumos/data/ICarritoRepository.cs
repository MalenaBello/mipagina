using System.Threading.Tasks;
using BayronInsumos.Models;

namespace BayronInsumos.Data
{
    public interface ICarritoRepository
    {
        Task<Carrito?> ObtenerPorClienteId(int clienteId);
        Task<bool> GuardarOActualizarItem(int clienteId, int productoId, int cantidad);
        Task<bool> EliminarItem(int clienteId, int productoId);
        Task<bool> VaciarCarrito(int clienteId);
    }
}