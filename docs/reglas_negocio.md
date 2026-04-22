# reglas de negocio y validaciones 

## 1. gestion de stock
* **Regla:** no se puede vender un producto sin stock 
* **Logica:** si producto.stock == 0. Eliminar la opcion de "agregar al carrito" y remplazarlo con el aviso **producto sin stock**   

## 2. validacion de ubicacion 
* **Regla:** el sistema solo funciona para **monte hermoso**
* **Logica:** si cliente.ciudad es distinto a  **Monte hermoso** el boton siguiente queda deshabilitado con el aviso: servicio solo disponible para la ciudad de Monte hermoso! 


## 3. persistencia del carrito (HU 2)
* **Regla:** el carrito vence a las 24 hotas 
* **logica:** al abrir la App el sistema debe comparar 'dateTime.now' con carrito 'carrito.fechaCreacion'. Si la diferencia es mayor a 24 hs, se elimina el carrito
  
  ## 4.formato de notificacion (HU 4)

  * **regla:** el mensaje de whatsApp debe ser profecional 
  * **estructura del menesaje:** 
    ```text
    Hola! Soy [Nombre], mi pedido es:
    - [Cantidad]x [Producto] ($[Subtotal])
    ----------------------------
    TOTAL: $[Total]
    Pago: [Efectivo/Transferencia]
    Dirección: [Calle] [Nro], Monte Hermoso.
    ```