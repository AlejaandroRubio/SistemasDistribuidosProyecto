using Graficas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graficas.Services
{
    public class GraphicDataRepository
    {
        private const string CacheKey = "GraphicsKey";

        public GraphicDataRepository()
        {
            var ctx = HttpContext.Current;
            if (ctx == null)
                throw new ArgumentNullException(nameof(ctx));
        }


        public GraphicsData[] GetAllGraphicPoints()
        {
            var ctx = HttpContext.Current;

            /* Si la cache no esta vacia */
            if (ctx != null)
                return (GraphicsData[])ctx.Cache[CacheKey];
            
            /* Si la cache esta vacia */
            return new GraphicsData[]
            {
                    new GraphicsData
                    {
                        x = -1, 
                        y = -1, 
                        z = -1
                    }
            };
        }

        public bool SaveDataPoint(GraphicsData data)
        {
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
                    dataList.Add(data);
                    ctx.Cache[CacheKey] = dataList.ToArray();

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

        public bool PutData(int id, GraphicsData value)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                GraphicsData[] tempData = (GraphicsData[])ctx.Cache[CacheKey];

                List<GraphicsData> tempList = tempData.ToList();

                tempList[id].x = value.x;
                tempList[id].y = value.y;
                tempList[id].z = value.z;

                return true;
            }
      

            return false;
        }

        public bool DeleteData(int id) 
        {
            var ctx = HttpContext.Current;

            if (ctx != null && id != -1)
            {
                GraphicsData[] tempData = (GraphicsData[])ctx.Cache[CacheKey];

                List<GraphicsData> tempList = tempData.ToList();

                tempList.RemoveAt(id);

                tempData = tempList.ToArray();

                ctx.Cache[CacheKey] = tempData;

                return true;
            }
            else if (ctx != null && id == -1)
            {
                DeleteAllData();
            }

            return false;
        }

        public bool DeleteAllData()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                GraphicsData[] tempData = (GraphicsData[])ctx.Cache[CacheKey];

                List<GraphicsData> tempList = tempData.ToList();

                tempList.RemoveRange(0, tempList.Count);

                tempData = tempList.ToArray();

                ctx.Cache[CacheKey] = tempData;

                return true;
            }

            return false;
        }

    }
}