
using BayronInsumos;

public class Carrito
{
    public int id {get;set;}
    public int ClienteID {get;set;}
    public int productoId {get;set;}
    public int cantidad {get;set;}
  public DateTime fechaCreacion {get;set;}
  public List<CarritoItem> ITem {get;set;} = new(); 
}