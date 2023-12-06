using Graficas.Models;
using Graficas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Graficas.Services;

namespace ProyectoGrafica.Controllers
{
    public class Graphic2DController : ApiController
    {

        static GraphicData2DRepository _GraphicDataRepository;

        public Graphic2DController()
        {
            _GraphicDataRepository = new GraphicData2DRepository();
        }

        public GraphicData2D[] Get()
        {
            return _GraphicDataRepository.GetAllGraphicPoints();
        }

        public bool Post([FromBody] GraphicData2D data)
        {
            return _GraphicDataRepository.SaveDataPoint(data);
        }


        public bool Put(int id, [FromBody] GraphicData2D data)
        {
           return _GraphicDataRepository.PutData(id, data);
        }

        public bool Delete(int id)
        {
            return _GraphicDataRepository.DeleteData(id);
        }

    }
}
