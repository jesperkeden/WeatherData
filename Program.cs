using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using WeatherData.Classes;
using WeatherData.Data;
using WeatherData.Models;

namespace WeatherData
{
    internal class Program
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool runProgram = true;

            while (runProgram)
            {
                List<iMeasurable> classList = Data1.CreateData();

                Menu.Start(classList);

            }
        }
    }
}