using Graficas.Models;
using Graficas.Services;
using ProyectoGrafica.Models;
using System.Web.Http;

namespace Graficas.Controllers
{
    public class GraphicController : ApiController
    {
        static GraphicDataRepository _GraphicDataRepository;

        public GraphicController() 
        {
            _GraphicDataRepository = new GraphicDataRepository();
        }
        
        public OpenGLGeneratedMesh Get()
        {
            return _GraphicDataRepository.GetOpenGLData();
        }

        public bool Post([FromBody] GraphicsDataRequest data)
        {
            return _GraphicDataRepository.SaveGeneratedMesh(data);
        }

    
        public bool Put(int id, [FromBody] GraphicsData data)
        {
            return false;
        }
       
        public bool Delete(int id)
        {
            return false;
        }
    }
}
