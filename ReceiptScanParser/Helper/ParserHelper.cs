using Newtonsoft.Json;
using ReceiptScanParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static List<String> ParseList(List<ResponseModel> responseList)
        {
            List<String> output = new List<string>();
            List<ResponseModel> tempList = new List<ResponseModel>();
            String tempStr = "";
            double upperBound = (responseList[0].boundingPoly.vertices[0].y + responseList[0].boundingPoly.vertices[1].y)/2;
            double lowerBound = (responseList[0].boundingPoly.vertices[2].y + responseList[0].boundingPoly.vertices[3].y)/2;
            foreach (ResponseModel item in responseList)
            {
                double midPoint = (item.boundingPoly.vertices[0].y + item.boundingPoly.vertices[1].y + +item.boundingPoly.vertices[2].y + +item.boundingPoly.vertices[3].y) / 4;
                if(midPoint > upperBound && midPoint < lowerBound)
                {
                    tempList.Add(item);
                }
                else
                {
                    tempList = tempList.OrderBy(o => o.boundingPoly.vertices[0].x).ToList();
                    foreach(var i in tempList)
                    {
                        tempStr += i.description + " ";
                    }
                    tempStr = tempStr.Remove(tempStr.Length - 1);
                    output.Add(tempStr);
                    upperBound = (item.boundingPoly.vertices[0].y + item.boundingPoly.vertices[1].y) / 2;
                    lowerBound = (item.boundingPoly.vertices[2].y + item.boundingPoly.vertices[3].y) / 2;
                    tempStr = "";
                    tempList.Clear();
                    tempList.Add(item);

                }
            }
            foreach (var i in tempList)
            {
                tempStr += i.description + " ";
            }
            tempStr = tempStr.Remove(tempStr.Length - 1);
            output.Add(tempStr);
            return output;
        }
    }
}
