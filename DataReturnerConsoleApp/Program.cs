using System;
using DataAccessTest;
using DataAccessTest.DataAccess;
using DataAccessTest.Models;

namespace DataReturnerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileReader fileReader = new FileReader();
            DataReturner obj = new DataReturner(fileReader);
            FilterModel filterModel = new FilterModel();
            filterModel.FilmsBeginningWithLetters = new char[] { 't','a' };
            filterModel.CarNames = new string[] { "infiniti", "saturn" };
            var outputList= obj.ReturnData(filterModel).Result;
            if(outputList.Count > 0)
            {
                foreach (var output in outputList)
                {
                    Console.Write(output.Id + "\t");
                    Console.Write(output.FirstName + "\t");
                    Console.Write(output.LastName + "\t");
                    Console.Write(output.Email + "\t");
                    Console.Write(output.City + "\t");
                    Console.Write(output.Film + "\t");
                    Console.WriteLine(string.Join(",", output.Cars)); 

                }
            }
            else
            {
                Console.WriteLine("No matching data found");
            }

            Console.ReadLine();
        }
    }
}
