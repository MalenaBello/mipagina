using Microsoft.EntityFrameworkCore;

namespace BayronInsumos;

public class ClienteRepository: IClienteRepository
{
    private readonly AplicacionDBcontext _context; 

    public ClienteRepository (AplicacionDBcontext context)
    {
        _context = context;
    }
    public IEnumerable<Cliente> ObtenerTodo() =>  _context.clientes.ToList<Cliente>();

    public Cliente? ObtenerPorId(int id) => _context.clientes.Find(id);

    public void Agregar (Cliente cliente)
    {
        _context.clientes.Add(cliente);
        _context.SaveChanges();
    }
    public Cliente? obtenerPorMail (string mail) => _context.clientes.FirstOrDefault( n => n.mail == mail); 
}