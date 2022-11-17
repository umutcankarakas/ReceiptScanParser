using System;
using System.Collections.Generic;
using System.Text;

namespace ReceiptScanParser.Models
{
    public class BoundingPoly
    {
        public List<Vertex> vertices { get; set; }
    }

    public class ResponseModel
    {
        public string locale { get; set; }
        public string description { get; set; }
        public BoundingPoly boundingPoly { get; set; }
    }

    public class Vertex
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
