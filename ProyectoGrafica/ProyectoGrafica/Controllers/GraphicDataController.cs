using ProyectoGrafica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProyectoGrafica.Services;
using Microsoft.Ajax.Utilities;

namespace ProyectoGrafica.Controllers
{
    public class GraphicDataController : ApiController
    {
        private GraphicDataPost graphicDataPost;
        private GraphicDataRead graphicDataRead;
        public List<GraphicData> tables; 

        public GraphicDataController() 
        { 
            tables = new List<GraphicData>();
            graphicDataPost = new GraphicDataPost();
            graphicDataRead = new GraphicDataRead();
        }

        

        // GET: api/GraphicData
        public GraphicData[] Get()
        {
           return graphicDataRead.GraphicDataGet();
        }

        // GET: api/GraphicData/5
        public List<DataPoint> Get(int id)
        {
            return tables[id].data;
        }

        // POST: api/GraphicData
        public GraphicData[] Post(GraphicData graphicData)
        {
            GraphicData[] tempData = new GraphicData[1];
            tempData[0] = graphicData;

            graphicDataPost.saveData(tempData);

            return tempData;
        }

        // PUT: api/GraphicData/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GraphicData/5
        public void Delete(int id)
        {
        }
    }
}
