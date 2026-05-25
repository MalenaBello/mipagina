using BayronInsumos.Models;
using Microsoft.EntityFrameworkCore;

namespace BayronInsumos.Data;

public class ClienteRepository : IClienteRepository
{
    private readonly AplicacionDBContext _context;

    public ClienteRepository(AplicacionDBContext context)
    {
        _context = context;
    }

    //  Guardar un nuevo cliente (ej: cuando se registra)
    public async Task<bool> Agregar(Cliente nuevoCliente)
    {
        await _context.clientes.AddAsync(nuevoCliente);
        await _context.SaveChangesAsync();
        return true;
    }

    //  Buscar por ID único
    public async Task<Cliente?> obtenerPorId(int id)
    {
        return await _context.clientes.FindAsync(id);
    }

    // Buscar por Email (sirve para no tener clientes duplicados con el mismo correo)
    public async Task<Cliente?> obtenerPorEmail(string email)
    {
        return await _context.clientes
            .FirstOrDefaultAsync(c => c.mail.ToLower() == email.ToLower()); 
            // Usamos .ToLower() para que no importe si escriben con mayúscula o minúscula
    }

    // 4. Listar todos los clientes para el panel de administración de tu vieja
    public async Task<IEnumerable<Cliente>> ObtenerTodos()
    {
        return await _context.clientes.ToListAsync();
    }

    // 5. Modificar datos (ej: si el cliente cambia su teléfono o dirección de entrega)
    public async Task<bool> ActualizarDatos(Cliente clienteModificado)
    {
        _context.clientes.Update(clienteModificado);
        int filasAfectadas = await _context.SaveChangesAsync();
        
        return filasAfectadas > 0;
    }
}