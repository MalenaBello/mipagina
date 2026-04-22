# logica de diseño del sistema 
este documento describe el comportamiento interno de la aplicaccion para cumplir con los requerimientos 

## 1. Modulo de catalogo y stock 
* **Evento:** al cargar la pagina de producto 
* **logica:** *hacer un SELECT * from productos 
  * **if** stock == 0, cambiar el estado del boton a 'disabled' y cambiar el texto del boton a "sin stock"
  * **else** habilitar selector de cantidad (maximo valor permitido = "stock")

## 2. calculo de totales y carrito 
* **evento:** al agregar al carrito o quitar 
*  **calculo:** 'totalPedido= suma (precioProducto * cantidad)
*  **persistencia:** al guardar en el carrito, registrar 'fechaHoraActual' 
   *  **validacion de tiempo:** al iniciar la app, si (ahora - fechaHoraActual) > 24 hs' ejecutar 'limpiarCarrito()
*  
## 3. Validación de Usuario y Zona (HU-3)
* **Evento:** Al presionar "Siguiente" en el formulario de datos.
* **Validaciones:**
    * **Campos Vacíos:** Si algún string está vacío, mostrar alerta: *"Por favor complete todos los campos"*.
    * **Zona Geográfica:** Si Ciudad.ToLower() no es "monte hermoso", bloquear flujo y mostrar mensaje de restricción.
    * **Persistencia:** Si el Checkbox Guardar Datos es True, guardar el objeto Usuario en la base de datos local.

## 4. Generador de Mensaje WhatsApp (HU-4)
* **Entrada:** Objeto Pedido + Objeto Usuario.
* **Proceso:** Concatenar strings para formar el mensaje.
* **Lógica de Pago:**
    * **IF** MetodoPago == "Transferencia" **THEN** anteponer los datos del Alias/CBU al mensaje de WhatsApp.
* **Salida:** Abrir URL https://wa.me/[TelefonoMarina]?text=[MensajeFormateado].

# 5. Persistencia con SQLite
* **Motor:** SQLite (Archivo local `SistemaLimpieza.db`).
* **ORM:** Entity Framework Core (Para no escribir SQL a mano y usar objetos C#).
* **Estrategia de Carga:** * Al iniciar la App, se verifica la existencia del archivo `.db`.
    * Si no existe, el sistema ejecuta las `Migrations` para crear las tablas automáticamente.