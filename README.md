# Trabajo-Practico-3
Trabajo practico numero 3 de la materia Programación I 


Trabajo Práctico 3
Introducción
El análisis de datos es un área fundamental del desarrollo de software. Permite, a partir de la información del pasado, detectar patrones, identificar tendencias y hasta predecir comportamientos futuros.

En este trabajo práctico van a trabajar con datos históricos y reales de la Fórmula 1 y que a través de las herramientas vistas en clases puedan procesar, recorrer y analizar información para responder distintas preguntas sobre las carreras, pilotos y equipos.

Los datos completos se encuentran disponibles en Kaggle:

https://www.kaggle.com/datasets/rohanrao/formula-1-world-championship-1950-2020 

Requisitos del Sistema
El archivo proporcionado (reducido ya que solo tiene datos del año 2016 al 2024) cuenta con las siguientes columnas en el mismo orden: 

temporada: año de carrera. Ej: 2016
equipo o escudería, ej: Mercedes
piloto con nombre y apellido, ej: Nico Rosberg
carrera, ej: Australian Grand Prix
posicion de clasificación (al inicio de la carrera), ej: 2
posicion de llegada, ej: 1
puntos obtenidos en la carrera, ej: 25.0
Los puntos en Fórmula 1 se obtienen según la siguiente tabla: 


Posición

Puntos obtenidos

1 25

2 18

3 15

4 12

5 10

6 8

7 6

8 4

9 2

10 1



El sistema deberá cargar los datos mediante .csv (se encuentran las instrucciones y el código de ejemplo junto a este trabajo)

Luego, mostrará el siguiente menú de opciones: 

-Buscar un piloto específico con nombre y mostrar cuántos podios tuvo en total (posición 1, 2 o 3).
-Obtener los datos de campeonato de un equipo de un año especifico. Mostrar: puntos obtenidos por carrera junto con el nombre del piloto que los obtuvo y los puntos totales obtenidos al final de la temporada. 
-Mostrar cuál fue la remontada más grande (el piloto que sumó más posiciones desde su largada hasta el final de la carrera) indicando nombre del piloto, temporada, carerra y equipo.
-Mostrar los nombres de todos los equipos ordenados por orden alfabético. 
-Mostrar todos los datos 
-Salir del programa


Aclaraciones:
Leer todas las instrucciones del trabajo práctico antes de comenzar a programar 
Utilicen estructuras de control para manejar las diferentes opciones y cálculos del programa. 
Deben aplicar ciclos, funciones y estructuras de datos.
Añadan comentarios en el código para explicar la lógica detrás de alguna sección importante si lo consideran necesario. 
Procuren que el programa sea interactivo y fácil de entender para el usuario.

Entrega del Trabajo:

Modalidad: link al repositorio de Github o Gitlab (debe estar público para poder acceder) 

Criterios de Evaluación

Cumplimiento de fecha y condiciones de entrega 1

Búsqueda de piloto y conteo de podios 1

Simulación de campeonato 1

Mostrar cuál fue la remontada más grande 1

Muestra de todos los datos 0.5

Muestra de equipos de forma ordenada (aplicando algún algoritmo de ordenamiento) 1.5

Interfaz clara e interactiva 1

Estilo de escritura de código fuente (organización y buenas prácticas) 1.5

Aplicación de funciones (programa modularizado con funciones que reciben parámetros y retornan los valores correctos) 1.5


Anexo: Carga de un CSV

¿Qué es un CSV? es un formato de texto para almacenar datos estructurados en formato de tabla, con valores separados por comas u otros delimitadores, y se usa para compartir e importar datos entre distintas aplicaciones como hojas de cálculo y bases de datos

A continuación se muestra cómo cargar un archivo y guardar los datos en una matriz:

Llamar a System.IO, un espacio de nombres que contIene herramientas para trabajar con archivos.

using System.IO;

Colocar la ruta en donde se ubica el archivo a leer.En mi caso puse esa dirección o ruta  ya que coloqué el archivo en la misma carpeta que Program.cs  y esa es una ruta relativa desde el directorio que se ejecuta el programa.
 Luego se lee el mismo línea por línea con la función File.ReadAllLines y guarda cada línea como un elemento en un arreglo de strings

string rutaArchivo = @"..\..\..\f1_last5years.csv";

string[] lineas = File.ReadAllLines(rutaArchivo);

Se determina el tamaño de filas y columnas

int filas = lineas.Length;

int cols = lineas[0].Split(',').Length;

Se crea la matriz de datos con las dimensiones correspondientes pero inicialmente vacía.

string[,] datos = new string[filas, cols];

Se cargan los datos en la matriz. En este caso ‘i’ recorre las filas del CSV, ‘j’ recorre las columnas de cada fila.

for (int i = 0; i < filas; i++)

{

    string[] values = lineas[i].Split(',');

    for (int j = 0; j < cols; j++)

    {

        datos[i, j] = values[j];

    }

}


 
carga-csv-funcion.txt carga-csv-funcion.txt
 
f1_last5years.csv f1_last5years.csv
 
metodo-burbuja-string.txt 
