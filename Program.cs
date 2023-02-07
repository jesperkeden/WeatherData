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
            // AvgValues();
            //OutsideAvg();



            //// Lista att använda för att skriva ut inne eller ute i menyn
            //List<iMeasurable> data = Data1.CreateData();
            //// Använd såhär: (data[0] = Inomhus, data[1] = Utomhus)
            //data[1].AvgValues();


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

        //static public void AvgValues()
        //{
        //    List<FileData> dataList = new List<FileData>();
        //    dataList = Helpers.ReadTextFile(filePath);
        //    int menuChoise = 0;
        //    DateTime inputDate = PromptUserForDate();
        //    Console.WriteLine("1. Inne\n2. Ute");
        //    menuChoise = Helpers.TryNumber(menuChoise, 1, 2);
        //    string locationInput = menuChoise == 1 ? "Inne" : "Ute";


        //    List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == locationInput).ToList();

        //    double dataAvgTemp = filteredData.Average(x => x.Temperature);
        //    double dataAvgResult = filteredData.Average(x => x.Humidity);

        //    if (locationInput == "Inne")
        //    {
        //        Console.WriteLine("Average temperature: " + Math.Round(dataAvgTemp, 2));
        //    }
        //    else if (locationInput == "Ute")
        //    {
        //        Console.WriteLine("Average temperature: " + Math.Round(dataAvgTemp, 2));
        //        Console.WriteLine("Average humidity: " + Math.Round(dataAvgResult, 2));
        //    }
        //    else
        //    {
        //        Console.WriteLine("Couldnt find any data from inside on that day.");
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