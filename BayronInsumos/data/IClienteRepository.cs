using BayronInsumos.Models;
namespace BayronInsumos.Data;

public interface IClienteRepository
{
   Task <IEnumerable<Cliente>>ObtenerTodos();

    Task <bool> Agregar (Cliente cliente);

    Task <Cliente?> obtenerPorId(int id);

    Task<Cliente?> obtenerPorEmail(String mail);
    Task <bool> ActualizarDatos(Cliente clienteModificado);
}