using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoGrafica.Models;

namespace ProyectoGrafica.Services
{
    public class GraphicDataPost
    {
        private const string CacheKey = "ContactStore";
        private static int idCounter = 0;

        public GraphicDataPost()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var contacts = new GraphicData()
                    {


                    };

                    ctx.Cache[CacheKey] = contacts;
                }
            }
        }

        public bool saveData(GraphicData[] data)
        {
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                try
                {
                    var cachedData = ctx.Cache[CacheKey] as GraphicData[];

                    if (cachedData != null)
                    { 
                        var currentData = data;
                        ctx.Cache[CacheKey] = currentData;
                        return true;
                    }
                }
                catch
                {
                    // Manejar excepciones si es necesario
                    return false;
                }
            }
            return false;
        }

    }
}