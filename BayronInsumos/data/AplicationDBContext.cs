using Microsoft.EntityFrameworkCore; 
using BayronInsumos;
using BayronInsumos.Models;

namespace BayronInsumos.Data;

public class AplicacionDBContext: DbContext
{
    public AplicacionDBContext(DbContextOptions<AplicacionDBContext> options): base(options)
    //recibe la configuracion de program.cs y se la pasa a la clase base para conectrse 

    {
        
    }
    //creamos las tablas 
    public DbSet<Producto> Productos {get;set;}
    public DbSet<Cliente> clientes {get;set;}
    public DbSet<Carrito> Carritos {get;set;}
    public DbSet<CarritoItem> CarritoItems {get;set;}
    public DbSet<Pedido> pedidos {get;set;}
    public DbSet <DetallePedido> detallePedidos {get;set;}
}