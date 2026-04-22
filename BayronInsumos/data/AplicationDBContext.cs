using Microsoft.EntityFrameworkCore; //traductor
using BayronInsumos;

namespace BayronInsumos;

public class AplicacionDBcontext: DbContext
{
    public AplicacionDBcontext(DbContextOptions<AplicacionDBcontext> options): base(options)
    //recibe la configuracion de program.cs y se la pasa a la clase base para conectrse 

    {
        
    }
    //creamos las tablas 
    public DbSet<Producto> Productos {get;set;}
    public DbSet<Cliente> clientes {get;set;}
    public DbSet<Carrito> Carritos {get;set;}
    public DbSet<CarritoItem> ITemsCarrito {get;set;}
}
