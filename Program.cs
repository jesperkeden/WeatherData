using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
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
            ShowInside();

        }
        public static void Test()
        {
            //SÖKFUNKTION IN PROGRESS!
            List<FileData> copy = new List<FileData>();
            copy = Helpers.ReadTextFile(filePath);

            foreach (var item in copy.Where(x => x.Humidity < 15 && x.Temperature < 20))
            {
                Console.WriteLine(item.DateTime + " - " + "Humidity: " + item.Humidity + " Temperature: " + item.Temperature);
            }

        }
        
        static public void ShowInside()
        {
            List<FileData> dataList = new List<FileData>();
            dataList = Helpers.ReadTextFile(filePath);

            DateTime inputDate = PromptUserForDate();

            List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == "Inne").ToList();

            double avgTemp = filteredData.Average(d => d.Temperature);
            double avgHumidity = filteredData.Average(d => d.Humidity);
            filteredData = filteredData.OrderBy(d => d.Location).ToList();

            Console.WriteLine("Results for the entered date:");
            Console.WriteLine("Average temperature: " + avgTemp);
            Console.WriteLine("Average humidity: " + avgHumidity);
            Console.WriteLine("Data sorted by location:");
            foreach (var data in filteredData)
            {
                Console.WriteLine(data.Location + ": " + data.Temperature + ", " + data.Humidity);
            }

        }

        static bool IsValidDate(string date, out DateTime inputDate)
        {
            string pattern = @"^(\d{4})([-./])(\d{2})\2(\d{2})$";
            DateTime earliest = new DateTime(2016, 6, 01);
            DateTime latest = new DateTime(2016, 12, 31);
            inputDate = default(DateTime);

            if (!Regex.IsMatch(date, pattern))
            {
                Console.WriteLine("Invalid date format.");
                return false;
            }

            inputDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            if (inputDate >= earliest && inputDate <= latest)
            {
                return true;
            }

            Console.WriteLine("No data available for the input date");
            return false;
        }

        public static DateTime PromptUserForDate()
        {
            Console.WriteLine("Enter a date in yyyy-mm-dd format:");
            string input = Console.ReadLine();
            while (true)
            {
                if (IsValidDate(input, out var inputDate))
                {
                    Console.WriteLine("Valid date entered: " + inputDate.ToString("yyyy-MM-dd"));
                    return inputDate;
                }
                Console.WriteLine("Invalid date format. Enter a date in yyyy-mm-dd format:");
                input = Console.ReadLine();
            }
        }
    }
}