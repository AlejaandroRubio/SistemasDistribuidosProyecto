using Graficas.Models;
using Microsoft.SqlServer.Server;
using Graficas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace Graficas.Services
{
    public class GraphicDataRepository
    {
        private const string CacheKey = "GraphicsKey";
        //string path = "C:\\Program Files\\SistemasDistribuidosProyecto\\ProyectoGrafica\\ProyectoGrafica\\Archivos_Compartidos\\Global.txt";
        string path = "C:\\Users\\caoal_7ce87t7\\SistemasDistribuidosProyecto\\ProyectoGrafica\\ProyectoGrafica\\Archivos_Compartidos\\Global.txt";


        public GraphicDataRepository()
        {
            var ctx = HttpContext.Current;
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));
        }


        public GraphicsData[] GetAllGraphicPoints()
        {

            /* Si la cache no esta vacia */
            return FormatData();
            
        }


        public bool SaveDataPoint(GraphicsDataRequest data)
        {

            GraphicsData graphicsData = TransformGraphicsData(data);
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    // Recuperar los datos de la cache
                    var currentData = ctx.Cache[CacheKey] as GraphicsData[];
                    // Si la cache de entrada es nula, inicializar un array nuevo
                    if (currentData == null)
                        currentData = new GraphicsData[0];
                    
                    var dataList = currentData.ToList();
                    dataList.Add(graphicsData);
                    ctx.Cache[CacheKey] = dataList.ToArray();

                    // Escribir al TXT Global
                    WritePostToTXT(dataList[dataList.Count - 1]);

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        /* Recuperamos es la cache entera */
        /* Value es la lista de puntos completa */
        /* ID = posicion en la lista */

        public bool PutData(int id, GraphicsDataRequest value)
        {       
                // Leer todas las líneas del archivo
                string[] lines = File.ReadAllLines(path);
                float Z = PerformTransformation(value);
                string dataModificada= "x:/" + value.x + "; y:/" + value.y + "; z:/" + Z;
                // Modificar la línea deseada (supongamos que quieres cambiarla por "Nueva línea")
                lines[id + 1] = dataModificada;
                // Sobrescribir el archivo con las líneas modificadas
                File.WriteAllLines(path, lines);
            

            return true;
        }

        public bool DeleteData(int id) 
        {
            if (id != -1)
            {
                string[] lines = File.ReadAllLines(path);
                // Índice de línea que deseas borrar
                int lineIndexToDelete = id; 
                // Eliminar la línea deseada por índice
                lines = lines.Where((line, index) => index != lineIndexToDelete).ToArray();
                // Sobrescribir el archivo con las líneas restantes
                File.WriteAllLines(path, lines);
            }
            else if (id == -1)
            {
                DeleteAllData();
            }

            return true;
        }

        public bool DeleteAllData()
        {
            string[] lines = {"[Datos]" };
            // Sobrescribir el archivo con las líneas modificadas
            File.WriteAllLines(path, lines);

            return true;
        }

        void CreateGlobalTXT()
        {
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("[Datos]");
                }
            }
        }

        // Esto es el input del usuario al archivo global
        void WritePostToTXT(GraphicsData data)
        {
            CreateGlobalTXT();

            using (StreamWriter sw = File.AppendText(path))
            { 
                sw.WriteLine("x:/" + data.x + "; y:/" + data.y + "; z:/" + data.z);
            }
        }


        // GET
        GraphicsData[] FormatData()
        {
            string[] tJs = File.ReadAllLines(path);

            GraphicsData[] tempData = new GraphicsData[tJs.Length];

            float sanitizedX = 0;
            float sanitizedY = 0;
            float sanitizedZ = 0;

            char[] tempCharArr = { };

            for (int i = 0; i < tJs.Length; i++)
            {
                tempCharArr = tJs[i].ToCharArray();
                tempData[i] = new GraphicsData();

                for (int j = 0; j < tempCharArr.Length - 1; j++)
                {
                    if (tempCharArr[j] == 120) // 120 = x
                    {
                        float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out sanitizedX);
                        tempData[i].x = sanitizedX;
                    }

                    if (tempCharArr[j] == 121) // 121 = y
                    {
                        float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out sanitizedY);
                        tempData[i].y = sanitizedY;
                    }

                    if (tempCharArr[j] == 122) // 120 = z
                    {
                        float.TryParse(GetNumbersFromJson(ref tempCharArr, j), out sanitizedZ);
                        tempData[i].z = sanitizedZ;
                    }
                }
            }

            return tempData;
        }

        string GetNumbersFromJson(ref char[] tJs, int i)
        {
            string tempValue = "";
            int j = i + 3;
            bool isNegative = false;

            while (j < tJs.Length)
            {
                if (char.IsDigit(tJs[j]) || tJs[j] == '-' || tJs[j] == ',')
                {
                    if (tJs[j] == '-')
                    {
                        // Marcar como negativo y omitir el carácter '-'
                        isNegative = true;
                    }
                    else
                    {
                        tempValue += tJs[j];
                    }
                }
                else if (tJs[j] == '.')
                {
                    // Reemplazar punto por coma para decimales
                    tempValue += ",";
                }
                else if (tJs[j] == ';' || tJs[j] == '}')
                {
                    // Salir del bucle al encontrar coma o llave de cierre
                    break;
                }

                j++;
            }

            // Agregar el signo negativo si es necesario
            if (isNegative)
            {
                tempValue = "-" + tempValue;
            }

            return tempValue;
        }



        public GraphicsData TransformGraphicsData(GraphicsDataRequest requestData) {

            try
            {
                // Realiza la transformación de GraphicsDataRequest a GraphicsData
                var transformedData = new GraphicsData
                {
                    x = requestData.x,
                    y = requestData.y,
                    z = PerformTransformation(requestData)
                };

                // Devuelve los datos transformados
                return transformedData;
            }
            catch (Exception ex)
            {
                return null;
               // Error
            }

        }

        private float PerformTransformation(GraphicsDataRequest requestData)
        {
            /*
            "x^2 * y/2)",
            "Sin(x) + Cos(y)",
            "sqrt(x^2+y^2)",
            "X^2 + y^3 * x + y^5"
             */

            switch (requestData.forumla)
            {
                case 0: return 0;
                case 1: { return (float)Math.Pow(requestData.x, 2) + requestData.y/2; } //x^2 * y/2
                case 2: { return (float)(Math.Sin(requestData.x) + Math.Cos(requestData.y)); } //Sin(x) + Cos(y)
                case 3: { return (float)Math.Sqrt((float)Math.Pow(requestData.x, 2) + (float)Math.Pow(requestData.y, 2)); } //sqrt(x^2+y^2)
                case 4: { return (float)(Math.Pow(requestData.x, 2) + Math.Pow(requestData.y, 3) * requestData.x + Math.Pow(requestData.y, 5)); }
            }

            return 0;



        }


    }
}