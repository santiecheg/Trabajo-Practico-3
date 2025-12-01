using System;
using System.IO;

class Program {


    /// <summary>
    /// Punto de entrada principal del programa.
    /// Muestra un mensaje de bienvenida, carga los datos del archivo CSV
    /// y presenta el menú principal de opciones al usuario.
    /// </summary>
static void Main (){
    Console.WriteLine("___________________________________________________________");
    Console.WriteLine("\n ----Bienvenidos al nuevo programa de Fórmula 1 ------");
    Console.WriteLine("___________________________________________________________");

   // Guardamos el momento en que comienza la carga del archivo
    var inicioCarga = DateTime.Now;  

    // Ejecutamos la función que lee todo el CSV y lo carga en memoria
    string[,] datos = ObtenerDatos(); 

   // Guardamos el momento en que finaliza la carga
    var finCarga = DateTime.Now;  

    // Llamamos al indicador de rendimiento para calcular y mostrar
    // cuánto tardó la carga del archivo
    IndicadorTiempoCarga(inicioCarga, finCarga);
    
    int totalFilas = datos.GetLength(0);
    int totalColumnas = datos.GetLength(1);

    Console.WriteLine($"\nMensaje del programa: Datos cargados exitosamente. \nTotal de registros: {totalFilas - 1}\n");
            

    
    MostrarMenu(datos, totalFilas, totalColumnas);    
}    




    /// <summary>
    /// Obtiene los datos de Fórmula 1 desde un archivo CSV y los almacena en una matriz bidimensional.
    /// Una matriz bidimensional de strings donde cada fila representa un registro
    /// y cada columna representa un campo del archivo CSV.
    /// El archivo CSV debe estar ubicado en la ruta especificada y debe tener
    /// un formato consistente con los datos de Fórmula 1.
    /// </summary>
static string[,] ObtenerDatos()
        {
            string rutaArchivo = @"C:\Users\Usuario\Desktop\NuevosProyectos\TrabajoPractico#\f1_last5years.csv";
            string[] lineas = File.ReadAllLines(rutaArchivo);

            int filas = lineas.Length;
            int cols = lineas[0].Split(',').Length;

            string[,] datos = new string[filas, cols];

            for (int i = 0; i < filas; i++)
            {
                string[] values = lineas[i].Split(',');
                for (int j = 0; j < cols; j++)
                {
                    datos[i, j] = values[j];
                }
            }

            return datos;
        }




    /// <summary>
    /// Muestra el menú principal del programa y gestiona la interacción del usuario. 
    /// Presenta un menú con 6 opciones y ejecuta la funcionalidad correspondiente
    /// según la selección del usuario. El bucle continúa hasta que el usuario
    /// selecciona la opción de salir. 
    /// </summary>
    /// <param name="datos">Matriz bidimensional con los datos de Fórmula 1.</param>
    /// <param name="totalFilas">Número total de filas en la matriz de datos.</param>
    /// <param name="totalColumnas">Número total de columnas en la matriz de datos.</param> 
    
static void MostrarMenu(string[,] datos, int totalFilas, int totalColumnas)
        {

            bool control = false; // Variable para finalizar 
            
            while (!control)
            {   
                Console.WriteLine(" ");
                Console.WriteLine("--- MENU PRINCIPAL F1 ---");
                Console.WriteLine(" ");

                Console.WriteLine("1. Buscar podios de un piloto");
                Console.WriteLine("2. Datos de campeonato de un equipo por año");
                Console.WriteLine("3. Mostrar la remontada más grande");
                Console.WriteLine("4. Mostrar equipos ordenados alfabéticamente");
                Console.WriteLine("5. Mostrar todos los datos");
                Console.WriteLine("6. Salir");       
                Console.WriteLine("7. NUEVAS FUNCIONES 0.0");         
                Console.WriteLine(" ");

                Console.Write("Escoja una opición: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {                    
                    case "1":
                        BuscarPodiosPiloto(datos, totalFilas);
                        break;

                    case "2":
                        DatosCampeonatoEquipo(datos, totalFilas);
                        break;

                    case "3":
                        MostrarRemontadaMasGrande(datos, totalFilas);
                        break;

                    case "4":
                        MostrarEquiposOrdenados(datos, totalFilas);
                        break;

                    case "5": // Reporte de todos los datos
                        MostrarTodosLosDatos(datos, totalFilas, totalColumnas);
                        break;


                    case "6": // Variable de control para cerrar el programa
                        Console.WriteLine("\nCerrrando programa.. \n");
                        control = true;
                        Console.WriteLine("¡Muchas gracias por utilizar simuladores Gatito! \n");
                        break;
                    default:
                        Console.WriteLine("Seleccione una opción válida por favor.");
                        break;

                    case "7": //Nuevo indicador Muestra pilotos unicos
                        int totalPilotosUnicos = CalcularPilotosUnicos(datos);
                        Console.WriteLine($"Cantidad total de pilotos únicos: {totalPilotosUnicos}");

                        //Indicador Nuevo: Carrera con más participantes 
                        var resultadoGP = CarreraConMasParticipantes(datos);
                        Console.WriteLine($"Carrera con más participantes: {resultadoGP.gp} ({resultadoGP.cantidad} pilotos)");

                         //<<<<<<< NUEVO INDICADOR >>>>>>>

                        // Cantidad de pilotos que lograron al menos un podio
                        int pilotosConPodio = CalcularPilotosConPodio(datos);
                        Console.WriteLine($"Pilotos que consiguieron al menos un podio: {pilotosConPodio}");

                        // REGISTROS POR AÑO
                        MostrarRegistrosPorAño(datos, totalFilas);
                        
  
                    break;    
                }
            }
        }


    /// <summary>
    /// Muestra todos los datos cargados en formato de registro detallado.
    /// Itera a través de todos los registros y columnas, mostrando cada valor
    /// individualmente con su posición en la matriz.
    /// </summary>
    /// <param name="datos">Matriz bidimensional con los datos de Fórmula 1.</param>
    /// <param name="totalFilas">Número total de filas en la matriz de datos.</param>
    /// <param name="totalColumnas">Número total de columnas en la matriz de datos.</param>

 static void MostrarTodosLosDatos(string[,] datos, int totalFilas, int totalColumnas)
        {
            Console.WriteLine("Reporte general");
            Console.WriteLine($"Visualizando {totalFilas - 1} :\n");
            
            for (int i = 0; i < totalFilas; i++)
            {
                Console.WriteLine($"Registro {i}:");
                for (int j = 0; j < totalColumnas; j++)
                {
                    Console.WriteLine($"  Columna {j}: {datos[i, j]}");
                }
                Console.WriteLine();
            }
        }        


    /// <summary>
    /// Busca y cuenta los podios (primeras tres posiciones) de un piloto específico.
    /// Solicita al usuario el nombre de un piloto y cuenta cuántas veces
    /// ese piloto terminó entre las primeras tres posiciones en cualquier carrera.
    /// La búsqueda no distingue entre mayúsculas y minúsculas.
    /// </summary>
    /// <param name="datos">Matriz bidimensional con los datos de Fórmula 1.</param>
    /// <param name="totalFilas">Número total de filas en la matriz de datos.</param>
static void BuscarPodiosPiloto(string[,] datos, int totalFilas)
        {
            Console.Write("Por favor ingrese el nombre del piloto: ");
            Console.Write("  ");
            string nombrePiloto = Console.ReadLine().ToLower();
            
            int contadorPodios = 0;
            
            for (int i = 1; i < totalFilas; i++)
            {
                string piloto = datos[i, 2];
                string posicionLlegada = datos[i, 5];
                
                if (piloto.ToLower().Contains(nombrePiloto))
                {
                    int posicion = int.Parse(posicionLlegada);
                    if (posicion >= 1 && posicion <= 3)
                    {
                        contadorPodios++;
                    }
                }
            }
            
            Console.WriteLine($"\nEl piloto {nombrePiloto} tuvo {contadorPodios} podios en total.");
        }

    /// <summary>
    /// Muestra los datos de campeonato de un equipo específico en un año determinado.
    /// Solicita al usuario el nombre de un equipo y un año, luego muestra
    /// todas las carreras de ese equipo en ese año con los puntos obtenidos
    /// por cada piloto, y calcula el total de puntos del equipo.
    /// </summary>
    /// <param name="datos">Matriz bidimensional con los datos de Fórmula 1.</param>
    /// <param name="totalFilas">Número total de filas en la matriz de datos.</param>
static void DatosCampeonatoEquipo(string[,] datos, int totalFilas)
        {
            Console.Write("Ingrese el nombre del equipo: ");
            string equipoBuscado = Console.ReadLine().ToLower();
            
            Console.Write("Ingrese el año de la temporada: ");
            string añoBuscado = Console.ReadLine();
            
            double puntosTotales = 0;
            bool existe = false;
            
            Console.WriteLine(" ");
            Console.WriteLine($" --- Buscando al equipo: {equipoBuscado.ToUpper()} - en el año: {añoBuscado} --- ");
            Console.WriteLine(" ");

            for (int i = 1; i < totalFilas; i++)
            {
                string equipo = datos[i, 1];
                string temporada = datos[i, 0];
                string puntos = datos[i, 6];
                
                if (equipo.ToLower().Contains(equipoBuscado) && temporada == añoBuscado)
                {
                    existe = true;
                    Console.WriteLine($"Carrera: {datos[i, 3]}");
                    Console.WriteLine($"Piloto: {datos[i, 2]} - Puntos: {puntos}");
                    puntosTotales += double.Parse(puntos);
                }
            }
            
            if (!existe)
            {
                Console.WriteLine($"No se encontraron datos para el equipo {equipoBuscado} en el año {añoBuscado}.");
            }
            else
            {
                Console.WriteLine($"Con un total de: {puntosTotales} puntos para el equipo.");
            }
        }

    /// <summary>
    /// Encuentra y muestra la mayor remontada en la historia de los datos cargados.
    /// Una remontada se calcula como la diferencia entre la posición de salida
    /// y la posición de llegada. Mayor remontada significa más posiciones ganadas
    /// durante la carrera.
    /// </summary>
    /// <param name="datos">Matriz bidimensional con los datos de Fórmula 1.</param>
    /// <param name="totalFilas">Número total de filas en la matriz de datos.</param>
static void MostrarRemontadaMasGrande(string[,] datos, int totalFilas)
        {
            int mayorRemontada = -100;
            int indiceMejorRemontada = -1;
            
            for (int i = 1; i < totalFilas; i++)
            {
                string posClasifStr = datos[i, 4];
                string posLlegadaStr = datos[i, 5];
                
                int posClasif = int.Parse(posClasifStr);
                int posLlegada = int.Parse(posLlegadaStr);
                
                if (posClasif > 0 && posLlegada > 0)
                {
                    int remontada = posClasif - posLlegada;
                    
                    if (remontada > mayorRemontada)
                    {
                        mayorRemontada = remontada;
                        indiceMejorRemontada = i;
                    }
                }
            }
            
            if (indiceMejorRemontada != -1)
            {
                Console.WriteLine("\n=== REMONTADA MÁS GRANDE ===");
                Console.WriteLine($"Piloto: {datos[indiceMejorRemontada, 2]}");
                Console.WriteLine($"Temporada: {datos[indiceMejorRemontada, 0]}");
                Console.WriteLine($"Carrera: {datos[indiceMejorRemontada, 3]}");
                Console.WriteLine($"Equipo: {datos[indiceMejorRemontada, 1]}");
                Console.WriteLine($"Posición de salida: {datos[indiceMejorRemontada, 4]}");
                Console.WriteLine($"Posición de llegada: {datos[indiceMejorRemontada, 5]}");
                Console.WriteLine($"Posiciones ganadas: {mayorRemontada}");
            }
            else
            {
                Console.WriteLine("No se encontraron datos de remontadas.");
            }
        }


static void MetodoBurbuja(string[] v) 
        {
            // Comparación alfabética de menor a mayor con el método de la burbuja
            // Algoritmo facilitado por la profe, utiliza magia negra algebráica para ordenar en un arreglo de manera ordenada los datos

            int n = v.Length;
            for (int i = 0; i < n - 1; i++)
            {
                bool ordenado = true;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (string.Compare(v[j], v[j + 1]) > 0)
                    {
                        ordenado = false;
                        // Intercambiar elementos
                        string temp = v[j];
                        v[j] = v[j + 1];
                        v[j + 1] = temp;
                    }
                }
                if (ordenado)
                {
                    break;
                }
            }
        }


    /// <summary>
    /// Muestra una lista de todos los equipos únicos encontrados en los datos,
    /// ordenados alfabéticamente y los muestra en pantalla junto con el conteo total de equipos.
    /// </summary>
    /// <param name="datos">Matriz bidimensional con los datos de Fórmula 1.</param>
    /// <param name="totalFilas">Número total de filas en la matriz de datos.</param>
static void MostrarEquiposOrdenados(string[,] datos, int totalFilas)
        {
            string[] equiposUnicos = new string[totalFilas - 1];
            int cantidadEquipos = 0;
            
            for (int i = 1; i < totalFilas; i++)
            {
                string equipoActual = datos[i, 1];
                bool existe = false;
                
                for (int j = 0; j < cantidadEquipos; j++)
                {
                    if (equiposUnicos[j] == equipoActual)
                    {
                        existe = true;
                        break;
                    }
                }
                
                if (!existe)
                {
                    equiposUnicos[cantidadEquipos] = equipoActual;
                    cantidadEquipos++;
                }
            }
            
            // Crear un arreglo con equipos encontrados
            string[] equiposFinal = new string[cantidadEquipos];
            for (int i = 0; i < cantidadEquipos; i++)
            {
                equiposFinal[i] = equiposUnicos[i];
            }
            
            // Ordenamos con el método de la Burbuja
            MetodoBurbuja(equiposFinal);
            
            Console.WriteLine(" --- Reporte en Orden alfabético --- ");
            Console.WriteLine("  ");

            for (int i = 0; i < equiposFinal.Length; i++)
            {
                Console.WriteLine($"• {equiposFinal[i]}");
            }

            Console.WriteLine($"\n En total hay {equiposFinal.Length} equipos en el campeonato" );
        }


    /// <summary>
    /// Esta función muestra el tiempo total de carga del archivo CSV, recibe dos tiempos (inicio y fin) 
    /// y calcula los milisegundos que tardó el programa en leer y procesar el archivo de datos.
    /// Calculamos la diferencia en milisegundos
    /// 
    /// En la invicación de la función se explica mejor
    /// </summary>
static void IndicadorTiempoCarga(DateTime inicio, DateTime fin)
{
    double tiempoCargaMs = (fin - inicio).TotalMilliseconds;

    // Mostramos el indicador al usuario
    Console.WriteLine("\n=== INDICADOR DE RENDIMIENTO ===");
    Console.WriteLine($"Tiempo de carga del archivo CSV: {tiempoCargaMs:F2} ms");
    Console.WriteLine("=================================\n");
}



    /// <summary>
    /// Cuenta cuántos pilotos diferentes aparecen en el archivo.
    /// Usa un HashSet para evitar duplicados automáticamente.
    /// </summary>  
    static int CalcularPilotosUnicos(string[,] datos)
    {     
        // HashSet almacena elementos SIN repetir
        HashSet<string> pilotos = new HashSet<string>();

        // Recorremos todas las filas del CSV
        for (int i = 0; i < datos.GetLength(0); i++)
        {
            // Columna 2 = piloto
            string piloto = datos[i, 2];

            // Sólo agregamos si no está vacío o nulo
            if (!string.IsNullOrWhiteSpace(piloto))
            {
                pilotos.Add(piloto);
            }
        }

        // El tamaño del HashSet = cantidad real de pilotos únicos
        return pilotos.Count;
    }


    /// <summary>
    /// Detecta qué carrera tuvo más participantes.
    /// Para eso cuenta cuántas filas del CSV pertenecen a cada GP.
    /// </summary>
static (string gp, int cantidad) CarreraConMasParticipantes(string[,] datos)
    {      
        // Diccionario donde clave = GP, valor = cantidad de pilotos que participaron
        Dictionary<string, int> conteoGP = new Dictionary<string, int>();

        // Recorremos todas las filas del archivo
        for (int i = 0; i < datos.GetLength(0); i++)
        {
            // Columna 0 = nombre de la carrera o GP
            string gp = datos[i, 0];

            // Validación por si hay líneas vacías
            if (!string.IsNullOrWhiteSpace(gp))
            {
                // Si no existe en el diccionario, lo agregamos
                if (!conteoGP.ContainsKey(gp))
                {
                    conteoGP[gp] = 0;
                }

                // Sumamos 1 participante a esa carrera
                conteoGP[gp]++;
            }
        }

        // Variables para guardar el GP con más participantes
        string gpMax = "";
        int maxParticipantes = 0;

        // Recorremos el diccionario para encontrar el mayor valor
        foreach (var item in conteoGP)
        {
            if (item.Value > maxParticipantes)
            {
                gpMax = item.Key;         // Nombre del GP
                maxParticipantes = item.Value; // Cantidad de participantes
            }
        }

        // Devolvemos los datos como una tupla
        return (gpMax, maxParticipantes);
    }


    /// <summary>
    /// Cuenta la cantidad de pilotos que lograron al menos un podio
    /// </summary>
    static int CalcularPilotosConPodio(string[,] datos)
    {
        HashSet<string> pilotosConPodio = new HashSet<string>();

        for (int i = 1; i < datos.GetLength(0); i++)
        {
            string piloto = datos[i, 2];
            int posicionLlegada = int.Parse(datos[i, 5]);

            if (posicionLlegada >= 1 && posicionLlegada <= 3)
            {
                pilotosConPodio.Add(piloto);
            }
        }

        return pilotosConPodio.Count;

    }




    /// <summary>
    /// INDICADOR: Total de registros por año.
    /// Mide cuántas filas hay por temporada en los últimos 5 años.
    /// Cuenta la cantidad de registros (filas) por temporada/año.
    /// Recorre la columna 0 (temporada) y suma ocurrencias por año.
    /// Omite la primera fila (cabecera).
    /// Muestra el resultado en consola en formato "Año XXXX: N registros".
    /// </summary>
    static void MostrarRegistrosPorAño(string[,] datos, int totalFilas)
    {
        // Diccionario: clave = año (string), valor = cantidad de registros
        Dictionary<string, int> conteoAños = new Dictionary<string, int>();

        // Recorremos desde 1 para omitir cabecera
        for (int i = 1; i < totalFilas; i++)
        {
            // Columna 0 = año/temporada
            string año = datos[i, 0]; // columna 0 = año/temporada

            // Si la celda está vacía la saltamos
            if (string.IsNullOrWhiteSpace(año))
                continue;

            // Si el año no existe en el diccionario, lo creamos
            if (!conteoAños.ContainsKey(año))
            {
                conteoAños[año] = 0;
            }

            // Sumamos 1 registro a ese año
            conteoAños[año]++;
        }

        // Mostramos el resultado
        Console.WriteLine("\n---- TOTAL DE REGISTROS POR AÑO (ÚLTIMOS 5 AÑOS) ----\n");
        
        // Si querés ordenado por año numérico: convertir claves a int y ordenar.
        // Aquí mostramos en el orden que aparezcan en el diccionario.
        foreach (var item in conteoAños)
        {
            // Por cada elemento del diccionario "conteoAños":
            // item.Key  = año de la temporada (ej: "2021").
            // item.Value = cantidad de registros encontrados para ese año.
            // Se imprime el resultado en formato legible para el usuario.
            Console.WriteLine($"Año {item.Key}: {item.Value} registros");
        }
        Console.WriteLine("\n------------------------------------------------------------\n");
    }




}






