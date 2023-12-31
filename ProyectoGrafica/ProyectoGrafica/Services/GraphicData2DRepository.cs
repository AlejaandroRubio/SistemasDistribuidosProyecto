﻿using Graficas.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Graficas.Services
{
    public class GraphicData2DRepository
    {
        private const string CacheKey = "GraphicsKey";
        //string path = "C:\\Program Files\\SistemasDistribuidosProyecto\\ProyectoGrafica\\ProyectoGrafica\\Archivos_Compartidos\\Global2D.txt";
        //string path = "C:\\Users\\diego\\Desktop\\SistemasDistribuidosProyecto-FormulasIdea\\ProyectoGrafica\\Frontend\\DEBUGArchivosLocales\\Global2D.txt";

        static string relativePath = "Archivos_Compartidos\\Global2D.txt";
        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);

        public GraphicData2DRepository()
        {
            var ctx = HttpContext.Current;
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));
        }

        #region GET
        public GraphicData2D[] GetAllGraphicPoints()
        {

            /* Si la cache no esta vacia */
            return FormatData();

        }

        GraphicData2D[] FormatData()
        {
            string[] tJs = File.ReadAllLines(path);

            GraphicData2D[] tempData = new GraphicData2D[tJs.Length];

            float sanitizedX = 0;
            float sanitizedY = 0;

            char[] tempCharArr = { };

            for (int i = 0; i < tJs.Length; i++)
            {
                tempCharArr = tJs[i].ToCharArray();
                tempData[i] = new GraphicData2D();

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
                }
            }

            return tempData;
        }
        #endregion

        #region POST

        public bool SaveDataPoint(GraphicData2D data)
        {

            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    /*
                    // Recuperar los datos de la cache
                    var currentData = ctx.Cache[CacheKey] as GraphicData2D[];
                    // Si la cache de entrada es nula, inicializar un array nuevo
                    if (currentData == null)
                        currentData = new GraphicData2D[0];

                    var dataList = currentData.ToList();
                    dataList.Add(data);
                    ctx.Cache[CacheKey] = dataList.ToArray();
                    */
                    // Escribir al TXT Global
                    //WritePostToTXT(dataList[dataList.Count - 1]);
                    WritePostToTXT(data);

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

        #endregion

        #region PUT

        public bool PutData(int id, GraphicData2D value)
        {
            // Leer todas las líneas del archivo
            string[] lines = File.ReadAllLines(path);
            string dataModificada = "x:/" + value.x + ", y:/" + value.y;
            // Modificar la línea deseada (supongamos que quieres cambiarla por "Nueva línea")
            lines[id +1] = dataModificada;
            // Sobrescribir el archivo con las líneas modificadas
            File.WriteAllLines(path, lines);


            return true;
        }

        #endregion

        #region DELETE

        public bool DeleteData(int id)
        {
            if (id != -1)
            {
                id = id +1;
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
            string[] lines = { "[Datos]" };
            // Sobrescribir el archivo con las líneas modificadas
            File.WriteAllLines(path, lines);

            return true;
        }

        #endregion

        #region GetNumbersFromJson
        string GetNumbersFromJson(ref char[] tJs, int i)
        {
            string tempValue = "";
            int j = 0;
            /* Idenitificar X, Y, Z para conseguir el numero asignado a cada una */
            j = i + 3;
            while (true)
            {
                if (tJs[j] == 46)
                    tempValue += ",";
                else
                    tempValue += tJs[j];

                if (j < tJs.Length - 1)
                    j++;
                else
                    break;

                if (tJs[j] == 44 || tJs[j] == 125)
                    break;
            }
            return tempValue;
        }
        #endregion

        #region WriteToTXT

        void WritePostToTXT(GraphicData2D data)
        {
            CreateGlobalTXT();

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("x:/" + data.x + ", y:/" + data.y);
            }
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
        #endregion



    }
}