using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Graficas.Models;
using Graficas.Services;
using System.Net.Http;
using System.Web.Http;
using ProyectoGrafica.Models;

namespace Graficas.Controllers
{
    public class GraphicController : ApiController
    {
        static GraphicDataRepository _GraphicDataRepository;

        public GraphicController() 
        {
            _GraphicDataRepository = new GraphicDataRepository();
        }
        
        public GraphicsData[] Get()
        {
            return _GraphicDataRepository.GetAllGraphicPoints();
        }

        public bool Post([FromBody] GraphicsDataRequest data)
        {
            return _GraphicDataRepository.SaveDataPoint(data);
        }

    
        public bool Put(int id, [FromBody] GraphicsData data)
        {
           return _GraphicDataRepository.PutData(id, data);
        }
       
        public bool Delete(int id)
        {
            return _GraphicDataRepository.DeleteData(id);
        }
    }
}
