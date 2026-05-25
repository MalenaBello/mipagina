
using BayronInsumos;
using BayronInsumos.Models;

public class Carrito
{
    public int id {get;set;}
    public int ClienteID {get;set;}
    
  public DateTime fechaCreacion {get;set;}
  public List<CarritoItem> Items {get;set;} = new(); 
}