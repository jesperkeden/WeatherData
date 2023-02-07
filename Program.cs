using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using WeatherData.Data;

namespace WeatherData
{
    internal class Program
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        public delegate void delHumidityOutside(List<FileData> files);
        static void Main(string[] args)
        {
            //Console.CursorVisible = false;
            //Menu.Start();

            //TESTER
            AvgTemp();
            //OutsideAvg();

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
        //static public void AvgHumidityOutside(List<FileData> filteredData)
        //{
        //    double humSum = filteredData.Sum(x => x.Humidity);
        //    double humResult = humSum / filteredData.Count;
        //    Console.WriteLine(" Average humidity: " + Math.Round(humResult, 2));
        //}
        static public void AvgTemp()
        {
            List<FileData> dataList = new List<FileData>();
            dataList = Helpers.ReadTextFile(filePath);
            string name = "";
            int menuChoise = 0;
            DateTime inputDate = PromptUserForDate();
            Console.WriteLine("1. Inne\n2. Ute");
            menuChoise = Helpers.TryNumber(menuChoise, 1, 2);
            name = menuChoise == 1 ? "Inne" : "Ute";


            List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == name).ToList();

            if (filteredData == null)
            {
                Console.WriteLine("Couldnt find any data from inside on that day.");
            }
            else
            {
                double dataSum = filteredData.Sum(x => x.Temperature);
                double result = dataSum / filteredData.Count;
                Console.WriteLine("Average temp: " + Math.Round(result, 2));
                if (name == "Ute")
                {
                    double humSum = filteredData.Sum(x => x.Humidity);
                    double humResult = humSum / filteredData.Count;
                    Console.WriteLine("Average humidity: " + Math.Round(humResult, 2));
                }
            }
        }

        //static public void InsideAvgTemp()
        //{
        //    List<FileData> dataList = new List<FileData>();
        //    dataList = Helpers.ReadTextFile(filePath);

        //    DateTime inputDate = PromptUserForDate();

        //    List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == "Inne").ToList();

        //    if (filteredData == null)
        //    {
        //        Console.WriteLine("Couldnt find any data from inside on that day.");
        //    }
        //    else
        //    {
        //        double dataSum = filteredData.Sum(x => x.Temperature);
        //        double result = dataSum / filteredData.Count;
        //        Console.WriteLine(Math.Round(result, 2));
        //    }
        //}
        //static public void OutsideAvg()
        //{
        //    List<FileData> dataList = new List<FileData>();
        //    dataList = Helpers.ReadTextFile(filePath);

        //    DateTime inputDate = PromptUserForDate();

        //    List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == "Ute").ToList();

        //    if (filteredData == null)
        //    {
        //        Console.WriteLine("Couldnt find any data from outside on that day.");
        //    }
        //    else
        //    {
        //        double tempSum = filteredData.Sum(x => x.Temperature);
        //        double tempResult = tempSum / filteredData.Count;

        //        double humSum = filteredData.Sum(x => x.Humidity);
        //        double humResult = humSum / filteredData.Count;
        //        Console.WriteLine("Average temperature: " + Math.Round(tempResult, 2) + " Average humidity: " + Math.Round(humResult, 2));
        //    }
        //}

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