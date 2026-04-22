graph TD 

%% inicio y carga 
    start((inicio Aap)) --> loadP[LecturaSQLite:CargarProductos]

%% gestion carrito
    checkC -- si --> showC[mostrar:Productos+CarritoExistente]
    checkC --no--> DelC[BorrarCarritoViejo]

%% seleccion 
    showC --> Sel[Selecciondeproducto]   
     Sel --> Stock{¿hay stock?}
    stock --No--> MsgNo[mostrar:SinStock]
    stock --si--> addC[insertarEnTablaCarrito]

%% registro y validacion 
     addC --> Reg[formularioDeDatos]
    Reg--> valCiud {¿es de Monte Hermoso?}
    valciud --no--> msgErr [error:SoloSePrestaElServicioAMonteHermoso]
    valciud --si--> SaveU {¿tildo guardar} 

%% Persistencia y Cierre
    SaveU -- Sí --> DB_U[UpdateTablaClientes]
    SaveU -- No --> Pay[ElegirPago]
    DB_U --> Pay
    Pay --> WA[GenerarStringWhatsApp]
    WA --> End((Abrir WhatsApp))