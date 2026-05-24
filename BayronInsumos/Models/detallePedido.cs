namespace BayronInsumos;

public class detallePedido
{
    
    int id {get;set;} // notNull 
    int idProducto {get;set;} // fk not null
    int idPedido {get;set;} // fk not null 

    int cantidad {get;set;} // fk not null 
}