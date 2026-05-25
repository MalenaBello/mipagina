namespace BayronInsumos.Models;

public class DetallePedido
{
    
   public int Id {get;set;} // notNull 
   public int idProducto {get;set;} // fk not null
    public int idPedido {get;set;} // fk not null 

    public int cantidad {get;set;} // fk not null 
}