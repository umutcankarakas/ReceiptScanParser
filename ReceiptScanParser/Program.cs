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
            //Loading the json file and converting it to the list of models
            List<ResponseModel> ModelList = ParserHelper.LoadJson();

            //The first item in the list is like a summary of the receipt, so we got rid of it
            ModelList = ModelList.Skip(1).ToList();

            //The list generally sorted but some items are mixed so we are going to order them by y value of top left corner
            List<ResponseModel> SortedList = ModelList.OrderBy(o => o.boundingPoly.vertices[0].y).ToList();

            //We get the final output, listed by line by line
            List<String> ParsedList = ParserHelper.ParseList(SortedList);

            //Wrote the output to a text file
            using (TextWriter tw = new StreamWriter("Output.txt"))
            {
                foreach (String s in ParsedList)
                    tw.WriteLine(s);
            }
        }
    }
}
