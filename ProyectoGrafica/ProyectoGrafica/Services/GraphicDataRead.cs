using ProyectoGrafica.Models;
using System.Web;

namespace ProyectoGrafica.Services
{
    public class GraphicDataRead
    {
        private const string CacheKey = "ContactStore";

        public GraphicData[] GraphicDataGet()
        {
            var ctx = HttpContext.Current;

            if (ctx != null && ctx.Cache[CacheKey] != null)
            {
                var s = ctx.Cache[CacheKey];

                //s = (GraphicData[])s;


                return s;
            }

            var defaultData = new GraphicData[]
            {
            };

            if (ctx != null)
            {
                ctx.Cache[CacheKey] = defaultData;
            }

            return defaultData;
        }
    }
}
