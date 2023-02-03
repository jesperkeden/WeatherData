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
            //Helpers.ReadTextFile2(filePath);
            Test();

        }
        public static void Test() 
        {
            //SÖKFUNKTION IN PROGRESS!
            List<FileData> copy = new List<FileData>();
            copy = Helpers.ReadTextFile(filePath);

            var koko = copy.Where(x => x.Month == 11 && x.Hour == 10);

            foreach (var item in koko)
            {
                Console.WriteLine("Month: " + item.Month + " - " + "Humidity: " + item.Humidity);
                //Console.WriteLine("Year :" + item.Year + "\nMonth: " + item.Month + "\nDay: " + item.Day + "\nHour: " + item.Hour + "\nMinutes: " + item.Minutes + "\nSeconds: " + item.Seconds + "\nLocation: " + item.Location + "\nTemperature: " + item.Temperature + "\nHumidity: " + item.Humidity + "\n--------");
            }

        }

    }
}