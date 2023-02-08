using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherData.Data;

namespace WeatherData.Classes
{
    internal class Helpers
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";

        static bool IsValidDate(string date, out DateTime inputDate)
        {
            string pattern = @"^(\d{4})([-./])(\d{2})\2(\d{2})$";
            DateTime earliest = new DateTime(2016, 6, 01);
            DateTime latest = new DateTime(2016, 12, 31);
            inputDate = default;

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
                    Console.Clear();
                    Console.WriteLine("Valid date entered: " + inputDate.ToString("yyyy-MM-dd"));
                    return inputDate;
                }
                Console.WriteLine("Invalid date format. Enter a date in yyyy-mm-dd format:");
                input = Console.ReadLine();
            }
        }
        public static List<string> CreateListOfData()
        {
            List<string> lines = new List<string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }
            return lines;
        }

        public static List<FileData> ReadTextFile(string filePath)
        {
            List<FileData> logs = new List<FileData>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Match match = Regex.Match(line, @"(\d{4})([-.])(0[1-9]|1[0-2])([-.])(0[1-9]|[1-2][0-9]|3[0-1])([\w\s])(0[0-9]|1[0-9]|2[0-3])([:;.,-_])([0-5][0-9])([:;.,-_])([0-5][0-9]),([Inne|Ute]+),(\d+[\W\S]?\d*?),(\d+[\W\S]?\d+)?$");

                    if (match.Success)
                    {
                        try
                        {
                            DateTime date = DateTime.ParseExact(
                                match.Groups[1].Value + "-" + match.Groups[3].Value + "-" + match.Groups[5].Value + " " +
                                match.Groups[7].Value + ":" + match.Groups[9].Value + ":" + match.Groups[11].Value,
                                "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                            string location = match.Groups[12].Value;
                            double temperature;
                            if (!double.TryParse(match.Groups[13].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out temperature))
                            {
                                Console.WriteLine("Could not parse temperature value.");
                                continue;
                            }
                            double humidity;
                            if (!double.TryParse(match.Groups[14].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out humidity))
                            {
                                Console.WriteLine("Could not parse humidity value.");
                                continue;
                            }
                            FileData fileData = new FileData(location, temperature, humidity, date);
                            logs.Add(fileData);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error parsing line: " + ex.Message);
                        }
                    }
                }
            }
            return logs;
        }



        //SORTING METHODS     No references so far..
        public static void SortByMonth()
        {
            List<string> list = CreateListOfData();
            string targetMonth = "05";

            var filteredData = from line in list
                               let parts = line.Split(',')
                               let dateTime = parts[0]
                               let dateTimeParts = dateTime.Split(' ')
                               let date = dateTimeParts[0]
                               let month = date.Split('-')[1]
                               where month == targetMonth
                               select line;

            foreach (string line in filteredData)
            {
                Console.WriteLine(line);
            }
        }
   

        // Secure number input
        internal static int TryNumber(int number, int minValue, int maxValue)
        {
            bool correctInput = false;

            while (!correctInput)
            {
                if (!int.TryParse(Console.ReadLine(), out number) || number > maxValue || number < minValue)
                {
                    Console.Write("Fel inmatning. Försök igen: ");
                }
                else
                {
                    correctInput = true;
                }
            }
            return number;
        }
    }
}
