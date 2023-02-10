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
        static void Main(string[] args)
        {
            //Console.CursorVisible = false;
            //bool runProgram = true;

            //while (runProgram)
            //{
            //    List<iMeasurable> classList = Models.Data.CreateData();

            //    Menu.Start(classList);

            //}

            Helpers.PrintToFile();
            

        }
    }
}