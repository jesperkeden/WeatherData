using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using WeatherData.Classes;
using WeatherData.Data;
using WeatherData.Models;


namespace WeatherData
{
    //delegate void LogToFileDelegate(string message);
    internal class Program
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        static void Main(string[] args)
        {
            //Console.CursorVisible = false;
            //bool runProgram = true;

            //while (runProgram)
            //{
            //    List<iMeasurable> classList = Data1.CreateData();

            //    Menu.Start(classList);

            //}
            //Data1.Test();
            //Dictionary<DateTime, double> testdic = new();
            //testdic = MoldRiskCalculator.CalculateAverageMoldRiskPerMonth();
            //foreach (KeyValuePair<DateTime, double> kvp in testdic)
            //{
            //    Console.WriteLine(kvp.Key.ToString("MMMM yyyy") + " Value: " + kvp.Value);
            //}

            Dictionary<DateTime, double> testdic2 = new();
            testdic2 = MoldRiskCalculator.CalculateAverageMoldRiskPerday();
            foreach (KeyValuePair<DateTime, double> kvp in testdic2)
            {
                Console.WriteLine(kvp.Key.ToString("dd MMM") + " Value: " + kvp.Value);
            }



        }


        //StreamWriter file = new StreamWriter("log.txt", true);
        //LogToFileDelegate logToFile = LogToFileDelegate(WriteToFile);
        //static void WritetoFile(string message)
        //{
        //    StreamWriter file = new StreamWriter("log.txt", true);
        //    file.WriteLine(message);
        //    file.Close();
        //}

        //static void AvgTempPerMonth(LogToFileDelegate logToFile)
        //{
        //    double avgInside = 5;
        //    double avgOutside = 5;

        //    //Log to file
        //    logToFile("avg inside temp per month: " + avgInside);
        //    logToFile("avg inside temp per month: " + avgOutside);
        //}

        //static void AvgHumPerMonth(LogToFileDelegate logToFile)
        //{
        //    double avgInside = 5;
        //    double avgOutside = 5;

        //    //Log to file
        //    logToFile("avg inside hum per month: " + avgInside);
        //    logToFile("avg inside hum per month: " + avgOutside);
        //}

        //static void RiskOfMold(LogToFileDelegate logToFile)
        //{
        //    double avgInside = 5;
        //    double avgOutside = 5;

        //    //Log to file
        //    logToFile("risk of mold inside: " + avgInside);
        //    logToFile("risk of mold outside: " + avgOutside);
        //}

        //static void MeteorologicalSeasons(LogToFileDelegate logToFile)
        //{
        //    string meteorolocialFallWinter = "";
        //    logToFile("Meteorological Fall / Winter occurs: " + meteorolocialFallWinter);
        //}

        //static void MoldCalc(LogToFileDelegate logTofile)
        //{
        //    //calc på algoritm
        //    logTofile("Mold calculation Algorithm: " + "något mer");
        //}
    }
}