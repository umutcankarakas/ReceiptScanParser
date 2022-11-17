using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReceiptScanParser.Helper;
using ReceiptScanParser.Models;

namespace ReceiptScanParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<ResponseModel> ModelList = ParserHelper.LoadJson();
            ModelList = ModelList.Skip(1).ToList();
            List<ResponseModel> SortedList = ModelList.OrderBy(o => o.boundingPoly.vertices[0].y).ToList();
            List<String> ParsedList = ParserHelper.ParseList(SortedList);

            using (TextWriter tw = new StreamWriter("Output.txt"))
            {
                foreach (String s in ParsedList)
                    tw.WriteLine(s);
            }
        }
    }
}
