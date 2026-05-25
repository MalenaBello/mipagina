using System;
using System.Collections.Generic;
namespace BayronInsumos.Models; 

public class Pedido
{
    public int id {get;set;} // not null 
    public EstadoPedido estadoPedido {get;set;} = EstadoPedido.pendiente;

    public DateTime? fechaDeEntrega {get;set;} //nuleable

    public double total {get;set;} // notNull.
    public List <DetallePedido> Detalles {get;set;} = new List<DetallePedido>();
  
    
}