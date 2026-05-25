using BayronInsumos.Models; // Asegurate que tus modelos estén acá
using Microsoft.EntityFrameworkCore; 

namespace BayronInsumos.Data;

public class ProductoRepository : IProductoRepository
{
    private readonly AplicacionDBContext _context;
    
    public ProductoRepository(AplicacionDBContext context)
    {
        _context = context;
    }

    // 1. Obtener todo: Ahora devuelve una Tarea con la lista
    public async Task<IEnumerable<Producto>> ObtenerTodo() 
    {
        return await _context.Productos.ToListAsync();
    }
    
    // 2. Agregar: Devuelve Task (vacío asincrónico) y usa SaveChangesAsync
    public async  Task<bool> Agregar(Producto newP)
    {
        await _context.Productos.AddAsync(newP);
        await _context.SaveChangesAsync();
        return true;
    }
     
    // 3. Obtener por ID: Usa FindAsync
    public async Task<Producto?> ObtenerPorID(int id) 
    {
        return await _context.Productos.FindAsync(id);
    }
     
    // 4. Actualizar Stock (Corregido el error de .precio)
    public async Task<bool> actualizarStock(int id, int cantidad)
    {
        // Usamos await porque nuestro propio método ahora es asincrónico
        Producto? productoActualizar = await this.ObtenerPorID(id);
        
        if (productoActualizar == null) return false;

        productoActualizar.stock = cantidad; // CORREGIDO: Antes decía .precio
        
        int filasAfectadas = await _context.SaveChangesAsync();
        return filasAfectadas > 0;
    }

    // 5. Restar Stock para compras (Corregido el error de .precio)
    public async Task<bool> restarStock(int id, int cantidadArestar)
    {
        Producto? producto = await this.ObtenerPorID(id);
        
        // CORREGIDO: Antes comparaba producto.precio < cantidadArestar
        if (producto == null || producto.stock < cantidadArestar) 
        {
            return false; // No hay stock suficiente o no existe
        }
        
        producto.stock -= cantidadArestar;
        
        int filasAfectadas = await _context.SaveChangesAsync();
        return filasAfectadas > 0; // Devuelve true si impactó el cambio
    }
}