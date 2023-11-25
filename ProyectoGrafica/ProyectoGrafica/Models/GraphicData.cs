using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGrafica.Models
{
    public class GraphicData
    {
        public List<DataPoint> data { get; set; }

    }
}

public class DataPoint
{
    public float x { get; set; } 
    public float y { get; set; } 
    public float z { get; set; }
}
