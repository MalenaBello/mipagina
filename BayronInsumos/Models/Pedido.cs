namespace BayronInsumos; 

public class Pedido
{
    public int id {get;set;} // not null 
    public EstadoPedido estadoPedido {get;set;} = EstadoPedido.pendiente;

    public DateTime? fechaDeEntrega {get;set;} //nuleable

    public double total {get;set;} // notNull.
    public List <detallePedido> Detalles {get;set;} = new List<detallePedido>();
    
}