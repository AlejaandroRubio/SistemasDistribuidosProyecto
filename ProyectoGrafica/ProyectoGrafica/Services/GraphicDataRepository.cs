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

    }
}