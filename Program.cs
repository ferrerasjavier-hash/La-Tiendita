// Sistema de Inventario "La Tiendita" - Proyecto Final C#

// Arreglos paralelos para almacenar el inventario
string[] nombreProductos = new string[100];
decimal[] precioProductos = new decimal[100];
int[] cantidadProductos = new int[100];
int totalProductos = 0;

// Variables globales
const int MAX_PRODUCTOS = 100;
bool salir = false;

// Programa Principal
MostrarBienvenida();

while (!salir)
{
    MostrarMenu();
    string opcion = Console.ReadLine();
    
    switch (opcion)
    {
        case "1":
            RegistrarProducto();
            break;
        case "2":
            ListarProductos();
            break;
        case "3":
            ActualizarStock();
            break;
        case "4":
            EliminarProducto();
            break;
        case "5":
            GenerarFactura();
            break;
        case "6":
            salir = true;
            MostrarDespedida();
            break;
        default:
            Console.WriteLine("\n❌ Opción no válida. Intenta de nuevo.");
            break;
    }
}

// ============ MÉTODOS DEL SISTEMA ============

void MostrarBienvenida()
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║   BIENVENIDO A LA TIENDITA         ║");
    Console.WriteLine("║   Sistema de Inventario            ║");
    Console.WriteLine("╚════════════════════════════════════╝\n");
}

void MostrarDespedida()
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║    ¡Gracias por usar La Tiendita!  ║");
    Console.WriteLine("║         ¡Hasta pronto!             ║");
    Console.WriteLine("╚════════════════════════════════════╝\n");
}

void MostrarMenu()
{
    Console.WriteLine("\n╔════════════════ MENÚ PRINCIPAL ════════════╗");
    Console.WriteLine("║  1. Registrar Producto                     ║");
    Console.WriteLine("║  2. Listado de Productos                   ║");
    Console.WriteLine("║  3. Actualizar Stock                       ║");
    Console.WriteLine("║  4. Eliminar Producto                      ║");
    Console.WriteLine("║  5. Generar Factura                        ║");
    Console.WriteLine("║  6. Salir                                  ║");
    Console.WriteLine("╚════════════════════════════════════════════╝");
    Console.Write("Selecciona una opción: ");
}

void RegistrarProducto()
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║      REGISTRAR NUEVO PRODUCTO      ║");
    Console.WriteLine("╚════════════════════════════════════╝\n");

    if (totalProductos >= MAX_PRODUCTOS)
    {
        Console.WriteLine("❌ No se pueden registrar más productos. Inventario lleno.\n");
        Console.ReadKey();
        return;
    }

    // Solicitar nombre
    Console.Write("Nombre del producto: ");
    string nombre = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(nombre))
    {
        Console.WriteLine("❌ El nombre no puede estar vacío.\n");
        Console.ReadKey();
        return;
    }

    // Solicitar precio
    Console.Write("Precio unitario: RD$");
    if (!decimal.TryParse(Console.ReadLine(), out decimal precio) || precio < 0)
    {
        Console.WriteLine("❌ Precio inválido. Debe ser un número positivo.\n");
        Console.ReadKey();
        return;
    }

    // Solicitar cantidad
    Console.Write("Cantidad inicial: ");
    if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad < 0)
    {
        Console.WriteLine("❌ Cantidad inválida. Debe ser un número positivo.\n");
        Console.ReadKey();
        return;
    }

    // Agregar producto
    nombreProductos[totalProductos] = nombre;
    precioProductos[totalProductos] = precio;
    cantidadProductos[totalProductos] = cantidad;
    totalProductos++;

    Console.WriteLine($"\n✅ Producto '{nombre}' registrado exitosamente.\n");
    Console.ReadKey();
}

void ListarProductos()
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║         LISTA DE PRODUCTOS         ║");
    Console.WriteLine("╚════════════════════════════════════╝\n");

    if (totalProductos == 0)
    {
        Console.WriteLine("📭 No hay productos en el inventario.\n");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("┌─────┬──────────────────┬──────────┬──────────┐");
    Console.WriteLine("│ ID  │ Producto         │  Precio  │ Cantidad │");
    Console.WriteLine("├─────┼──────────────────┼──────────┼──────────┤");

    for (int i = 0; i < totalProductos; i++)
    {
        string nombreTruncado = nombreProductos[i].Length > 16 
            ? nombreProductos[i].Substring(0, 13) + "..." 
            : nombreProductos[i];
        
        Console.WriteLine($"│ {i + 1,3} │ {nombreTruncado,-16} │ RD${precioProductos[i],7:F2} │ {cantidadProductos[i],8} │");
    }

    Console.WriteLine("└─────┴──────────────────┴──────────┴──────────┘\n");
    Console.ReadKey();
}

void ActualizarStock()
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║       ACTUALIZAR STOCK             ║");
    Console.WriteLine("╚════════════════════════════════════╝\n");

    if (totalProductos == 0)
    {
        Console.WriteLine("📭 No hay productos en el inventario.\n");
        Console.ReadKey();
        return;
    }

    ListarProductos();

    Console.Write("Selecciona el ID del producto a actualizar: ");
    if (!int.TryParse(Console.ReadLine(), out int id) || id < 1 || id > totalProductos)
    {
        Console.WriteLine("❌ ID inválido.\n");
        Console.ReadKey();
        return;
    }

    int indice = id - 1;
    Console.WriteLine($"\nProducto actual: {nombreProductos[indice]} - Cantidad: {cantidadProductos[indice]}");
    Console.Write("Nueva cantidad: ");

    if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad < 0)
    {
        Console.WriteLine("❌ Cantidad inválida.\n");
        Console.ReadKey();
        return;
    }

    cantidadProductos[indice] = cantidad;
    Console.WriteLine($"✅ Stock de '{nombreProductos[indice]}' actualizado a {cantidad}.\n");
    Console.ReadKey();
}

void EliminarProducto()
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║       ELIMINAR PRODUCTO            ║");
    Console.WriteLine("╚════════════════════════════════════╝\n");

    if (totalProductos == 0)
    {
        Console.WriteLine("📭 No hay productos en el inventario.\n");
        Console.ReadKey();
        return;
    }

    ListarProductos();

    Console.Write("Selecciona el ID del producto a eliminar: ");
    if (!int.TryParse(Console.ReadLine(), out int id) || id < 1 || id > totalProductos)
    {
        Console.WriteLine("❌ ID inválido.\n");
        Console.ReadKey();
        return;
    }

    int indice = id - 1;
    string nombreProductoEliminado = nombreProductos[indice];

    // Desplazar los elementos
    for (int i = indice; i < totalProductos - 1; i++)
    {
        nombreProductos[i] = nombreProductos[i + 1];
        precioProductos[i] = precioProductos[i + 1];
        cantidadProductos[i] = cantidadProductos[i + 1];
    }

    totalProductos--;

    Console.WriteLine($"✅ Producto '{nombreProductoEliminado}' eliminado del inventario.\n");
    Console.ReadKey();
}

void GenerarFactura()
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║        GENERAR FACTURA             ║");
    Console.WriteLine("╚════════════════════════════════════╝\n");

    if (totalProductos == 0)
    {
        Console.WriteLine("📭 No hay productos disponibles.\n");
        Console.ReadKey();
        return;
    }

    ListarProductos();

    // Arrays para almacenar la factura
    int[] idsFactura = new int[100];
    int[] cantidadesFactura = new int[100];
    int totalItems = 0;
    decimal totalFactura = 0;
    bool agregarMas = true;

    Console.WriteLine("\n--- Agregar productos a la factura ---\n");

    while (agregarMas && totalItems < 100)
    {
        Console.Write("ID del producto (0 para terminar): ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id < 0 || id > totalProductos)
        {
            if (id == 0)
            {
                agregarMas = false;
                continue;
            }
            Console.WriteLine("❌ ID inválido.\n");
            continue;
        }

        if (id == 0)
        {
            agregarMas = false;
            continue;
        }

        int indice = id - 1;

        Console.Write($"Cantidad de '{nombreProductos[indice]}' disponible ({cantidadProductos[indice]}): ");
        if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
        {
            Console.WriteLine("❌ Cantidad inválida.\n");
            continue;
        }

        if (cantidad > cantidadProductos[indice])
        {
            Console.WriteLine($"❌ Stock insuficiente. Disponible: {cantidadProductos[indice]}\n");
            continue;
        }

        // Agregar a la factura
        idsFactura[totalItems] = id;
        cantidadesFactura[totalItems] = cantidad;
        totalItems++;

        // Restar del inventario
        cantidadProductos[indice] -= cantidad;

        Console.WriteLine($"✅ Agregado a la factura.\n");
    }

    // Mostrar factura
    if (totalItems == 0)
    {
        Console.WriteLine("\n📭 No se agregó ningún producto a la factura.\n");
        Console.ReadKey();
        return;
    }

    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════════════╗");
    Console.WriteLine("║              FACTURA DE VENTA             ║");
    Console.WriteLine("╚════════════════════════════════════════════╝\n");

    Console.WriteLine("┌───────────────────────────────────────────┐");
    Console.WriteLine("│ Producto           Cantidad  Precio Total │");
    Console.WriteLine("├───────────────────────────────────────────┤");

    for (int i = 0; i < totalItems; i++)
    {
        int indice = idsFactura[i] - 1;
        int cantidad = cantidadesFactura[i];
        decimal subtotal = cantidad * precioProductos[indice];
        totalFactura += subtotal;

        string nombreTruncado = nombreProductos[indice].Length > 15
            ? nombreProductos[indice].Substring(0, 12) + "..."
            : nombreProductos[indice];

        Console.WriteLine($"│ {nombreTruncado,-18} {cantidad,7}    RD${subtotal,9:F2} │");
    }

    Console.WriteLine("├───────────────────────────────────────────┤");
    Console.WriteLine($"│ TOTAL A PAGAR:                    RD${totalFactura,9:F2} │");
    Console.WriteLine("└───────────────────────────────────────────┘\n");

    Console.WriteLine("✅ Factura generada exitosamente.\n");
    Console.ReadKey();
}

