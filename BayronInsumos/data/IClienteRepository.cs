namespace BayronInsumos;

public interface IClienteRepository
{
    IEnumerable<Cliente>ObtenerTodo();

    void Agregar (Cliente cliente);

    Cliente? ObtenerPorId(int id);

    Cliente? obtenerPorMail(String mail);
}