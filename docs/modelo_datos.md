# 📊 Modelo de Datos - Sistema de Limpieza

## 1. Glosario de Términos (Modelo de Dominio)
Antes del diseño técnico, definimos los conceptos clave del negocio:
* **Producto:** Insumo de limpieza individual (ej: Lavandina 1L). Tiene un precio unitario y un stock disponible.
* **Cliente:** Persona que realiza la compra. El sistema debe recordar su nombre, dirección y teléfono para agilizar futuros pedidos.
* **Carrito:** Conjunto temporal de productos que el cliente selecciona antes de confirmar.
* **Pedido:** Es la orden de compra final. Vincula al cliente con una lista de productos, un total a pagar y un método de pago.
* **Estado del Pedido:** Indica en qué parte del proceso está la venta (Pendiente de pago, Preparando, Enviado).
* **Comprobante:** Imagen o captura de la transferencia que el cliente debe enviar para validar pagos por QR.

---

## 2. Reglas de Negocio Iniciales
* Un **Pedido** debe tener al menos un **Producto**.
* Los datos del **Cliente** son obligatorios para generar el link de WhatsApp.
* El **Precio** del producto se congela al momento de crear el Pedido (si el precio sube mañana, el pedido viejo mantiene el precio anterior).

---

## 3. Diseño Técnico (UML)
*(Aquí es donde iría el diagrama de clases que hicimos antes, una vez que el glosario esté claro)*
### tabla: producto
| campo | tipo  | nuleable | descripcion |
| :--- | :--- | :--- | :--- |
| id | int | no | clave primaria  |
| nombre | String | no | nombre del insumo |
| precio | double | no | precio unitario del    insumo|
| stock | int | no | cantidad de unidades del insumo| 

### tabla: cliente (Sesion Local)
| campo | tipo | nuleable | descripcion |
| :--- | :--- | :--- | :--- |
| id | int | no | clave primaria | 
| nombre | string | no | nombre del comprador |
| ciudad | string | no | validacion: debe ser "monte hermoso" |
| direccion | string | no | direccion de entrega |
| telefono | string | no | numero de contacto |
| persistir | bool | no | define si guardar los datos |

### tabla: carrito

| campo | tipo | nuleable |  descripcion |
| :--- | :--- | :--- | :--- |
| id | int | no | id unico de la linea carrito |
| productoId | int | no | Fk de la tabla de producto| 
| cantidad | int   |  no | cantidades de unidades elegidas | 
| fechaCreacion | DateTime | no | regla: Si fechaCreacion > 24 hs. Se elimina 