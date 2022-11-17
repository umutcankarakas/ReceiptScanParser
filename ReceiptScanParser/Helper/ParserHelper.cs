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
        //Loading the json file and converting it to the list of models
        public static List<ResponseModel> LoadJson()
        {
            string filepath = "..\\..\\..\\Data\\response.json";
            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                List<ResponseModel> items = JsonConvert.DeserializeObject<List<ResponseModel>>(json);
                return items;
            }

        }
        //Gets list of models as input and gives the list of final strings as output
        public static List<String> ParseList(List<ResponseModel> responseList)
        {
            List<String> output = new List<string>();
            List<ResponseModel> tempList = new List<ResponseModel>();
            String tempStr = "";

            //First we calculate the upper and lower bounds of our line in the receipt by calculating the average y value
            double upperBound = (responseList[0].boundingPoly.vertices[0].y + responseList[0].boundingPoly.vertices[1].y)/2;
            double lowerBound = (responseList[0].boundingPoly.vertices[2].y + responseList[0].boundingPoly.vertices[3].y)/2;
            foreach (ResponseModel item in responseList)
            {
                //Then we calculate the y value of the middle point of the box, surrounding a certain text
                double midPoint = (item.boundingPoly.vertices[0].y + item.boundingPoly.vertices[1].y + +item.boundingPoly.vertices[2].y + +item.boundingPoly.vertices[3].y) / 4;
                
                //If that middle point is in between the bounds we calculated earlier, that means we are still on the same line
                if(midPoint > upperBound && midPoint < lowerBound)
                {
                    tempList.Add(item); //Since the orders of the texts in the same line could be mixed, so we are going to order them too
                }
                //Else, that means we are on the next line
                else
                {
                    //Ordering the items -which are on the same line- by x values
                    tempList = tempList.OrderBy(o => o.boundingPoly.vertices[0].x).ToList();
                    //Creating the string
                    foreach(var i in tempList)
                    {
                        tempStr += i.description + " ";
                    }
                    tempStr = tempStr.Remove(tempStr.Length - 1);

                    //Adding this newly created string to the output
                    output.Add(tempStr);
                    //Calculating borders of the next line
                    upperBound = (item.boundingPoly.vertices[0].y + item.boundingPoly.vertices[1].y) / 2;
                    lowerBound = (item.boundingPoly.vertices[2].y + item.boundingPoly.vertices[3].y) / 2;
                    //Clearing the temp variables
                    tempStr = "";
                    tempList.Clear();
                    //Adding the first item of the next line to the temp list and going back again
                    tempList.Add(item);

                }
            }
            //Handling the last line
            foreach (var i in tempList)
            {
                tempStr += i.description + " ";
            }
            tempStr = tempStr.Remove(tempStr.Length - 1);
            output.Add(tempStr);

            //Returning the final list of strings
            return output;
        }
    }
}
