using System.Security.Cryptography.X509Certificates;
using WeatherData.Data;

namespace WeatherData
{
    internal class Program
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        static void Main(string[] args)
        {
            //Console.CursorVisible = false;
            //Menu.Start();

            //TESTER
            //Helpers.ReadTextFile(filePath);
            //Test();
            //Helpers.ReadTextFile2(filePath);
            Test();

        }
        public static void Test()
        {
            //SÖKFUNKTION IN PROGRESS!
            List<FileData> copy = new List<FileData>();
            //copy = Helpers.ReadTextFile(filePath);
            copy = Helpers.ReadTextFile2(filePath);

            //copy.Where(x => x.HumidityDouble < 10);

            foreach (var item in copy.Where(x => x.HumidityDouble < 15 && x.TemperatureDouble < 200))
            {
                //Console.WriteLine(item);
                Console.WriteLine(item.DateTime + " - " + "Humidity: " + item.HumidityDouble + " Temperature: " + item.TemperatureDouble);
                //Console.WriteLine("Year :" + item.Year + "\nMonth: " + item.Month + "\nDay: " + item.Day + "\nHour: " + item.Hour + "\nMinutes: " + item.Minutes + "\nSeconds: " + item.Seconds + "\nLocation: " + item.Location + "\nTemperature: " + item.Temperature + "\nHumidity: " + item.Humidity + "\n--------");
            }

        }

    }
}