using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Graficas.Models;
using Graficas.Services;
using System.Net.Http;
using System.Web.Http;


namespace Graficas.Controllers
{
    public class GraphicController : ApiController
    {
        private GraphicDataRepository _GraphicDataRepository;

        public GraphicController() 
        {
            _GraphicDataRepository = new GraphicDataRepository();
        }
        
        
        public GraphicsData[] Get()
        {
            return _GraphicDataRepository.GetAllGraphicPoints();
        }

        /* Devuelve un punto dentro de la grafica */
        public GraphicsData Get(int id)
        {
            return null;
        }


        public bool Post([FromBody]GraphicsData data)
        {
            return _GraphicDataRepository.SaveDataPoint(data);
        }

    
        public void Put(int id, [FromBody]string value)
        {

        }

       
        public void Delete(int id)
        {

        }
    }
}
