using System;
using ReceiptScanParser.Helper;

namespace ReceiptScanParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            var Models = ParserHelper.LoadJson();
            Console.WriteLine("Hello World!");
        }
    }
}
