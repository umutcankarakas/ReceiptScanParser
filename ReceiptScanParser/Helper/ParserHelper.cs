using Newtonsoft.Json;
using ReceiptScanParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReceiptScanParser.Helper
{
    public class ParserHelper
    {
        public static List<ResponseModel> LoadJson()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Data\\response.json";
            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                List<ResponseModel> items = JsonConvert.DeserializeObject<List<ResponseModel>>(json);
                return items;
            }

        }
    }
}
