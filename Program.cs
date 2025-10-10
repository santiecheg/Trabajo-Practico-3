using System;
using System.IO;

class Program {

static void Main (){
    Console.WriteLine("___________________________________________________________");
    Console.WriteLine("\n ----Bienvenidos al nuevo programa de Fórmula 1 ------");
    Console.WriteLine("___________________________________________________________");
    string[,] datos = ObtenerDatos();
    int totalFilas = datos.GetLength(0);
    int totalColumnas = datos.GetLength(1);

    Console.WriteLine($"\nMensaje del programa: Datos cargados exitosamente. \nTotal de registros: {totalFilas - 1}\n");
            
    MostrarMenu(datos, totalFilas, totalColumnas);    
}    

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
                }
            }
        }

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
        {// Comparación alfabética de menor a mayor con el método de la burbuja
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

}
