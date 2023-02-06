using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherData.Data;

namespace WeatherData
{
    internal class Helpers
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";

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
            List<FileData> files = new List<FileData>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(' ', ',', ':', '-');
                    int year = int.Parse(parts[0]);
                    int month = int.Parse(parts[1]);
                    int day = int.Parse(parts[2]);
                    int hour = int.Parse(parts[3]);
                    int minute = int.Parse(parts[4]);
                    int second = int.Parse(parts[5]);
                    string location = parts[6];
                    string temperature = parts[7];
                    string humidity = parts[8].Replace(".", "");
                    double humidityInt = Convert.ToDouble(humidity);

                    if (year == 2016 && month == 05 || year == 2017)
                    {

                    }
                    else
                    {
                        files.Add(new FileData(year, month, day, hour, minute, second, location, temperature, humidity));
                    }


                }

            }
            return files;
        }
        public static List<FileData> ReadTextFile2(string filePath)
        {
            List<FileData> logs = new List<FileData>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Match match = Regex.Match(line, @"(\d{4})([-.])(0[1-9]|1[0-2])([-.])(0[1-9]|[1-2][0-9]|3[0-1])([\w\s])(0[0-9]|1[0-9]|2[0-3])([:;.,-_])([0-5][0-9])([:;.,-_])([0-5][0-9]),([Inne|Ute]+),(\d+[\W\S]?\d+)[\W\S]?(\d+[\W\S]?\d+)?$");
                    //Sista regex från Jesper
                    //(\d{4})([-.])(0[1-9]|1[0-2])([-.])(0[1-9]|[1-2][0-9]|3[0-1])([\w\s])(0[0-9]|1[0-9]|2[0-3])([:;.,-_])([0-5][0-9])([:;.,-_])([0-5][0-9]),([Inne|Ute]+),(\d+[\W\S]?\d+)[\W\S]?(\d+[\W\S]?\d+)?$
                    if (match.Success)
                    {

                        DateTime date = DateTime.ParseExact(match.Groups[1].Value + "-" + match.Groups[3].Value + "-" + match.Groups[5].Value + " " + match.Groups[7].Value + ":" + match.Groups[9].Value + ":" + match.Groups[11].Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                        string location = match.Groups[12].Value;

                        double temperatureDouble = (Convert.ToDouble(match.Groups[13].Value));
                        double humidityDouble = (Convert.ToDouble(match.Groups[16].Value));

                        FileData fileData = new FileData(location, temperatureDouble, humidityDouble, date);
                        logs.Add(fileData);

                    }
                }
            }
            return logs;
        }
        //private static FileData TryParseLine(string line)
        //{
        //    Match match = Regex.Match(line, @"^(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2}) (?<hour>\d{2}):(?<minute>\d{2}):(?<second>\d{2}) (?<location>(inne|ute)) (?<temp>-?\d+(?:\.\d+)?) (?<humidity>\d+(?:\.\d+)?)");
        //    if (!match.Success)
        //    {
        //        return null;
        //    }

        //    int year = int.Parse(match.Groups["year"].Value);
        //    int month = int.Parse(match.Groups["month"].Value);
        //    int day = int.Parse(match.Groups["day"].Value);
        //    int hour = int.Parse(match.Groups["hour"].Value);
        //    int minute = int.Parse(match.Groups["minute"].Value);
        //    int second = int.Parse(match.Groups["second"].Value);
        //    string location = match.Groups["location"].Value;
        //    double temperature = double.Parse(match.Groups["temp"].Value);
        //    int humidity = int.Parse(match.Groups["humidity"].Value);

        //    try
        //    {
        //        DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
        //        return new FileData(year, month, day, hour, minute, second, location, temperature, humidity);
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //        return null;
        //    }
        //}
        //public static List<FileData> ReadTextFile3(string filePath)
        //{
        //    List<FileData> logs = new List<FileData>();
        //    using (var reader = new StreamReader(filePath))
        //    {
        //        while (!reader.EndOfStream)
        //        {
        //            string line = reader.ReadLine();
        //            FileData log = TryParseLine(line);
        //            if (log != null)
        //            {
        //                logs.Add(log);
        //            }
        //        }
        //    }
        //    return logs;
        //}

        //SORTERINGSMETODER
        public static void SortByYear()
        {
            List<string> list = Helpers.CreateListOfData();

            var groupedData = from line in list
                              let parts = line.Split(',')
                              let dateTime = parts[0]
                              let dateTimeParts = dateTime.Split(' ')
                              let date = dateTimeParts[0]
                              let year = date.Split('-')[0]
                              where year == "2016"
                              group line by year into yearGroup
                              select new { Year = yearGroup.Key, Data = yearGroup.ToList() };

            foreach (var yearData in groupedData)
            {
                Console.WriteLine("Year: " + yearData.Year);
                Console.WriteLine("Data:");
                foreach (string line in yearData.Data)
                {
                    Console.WriteLine("    " + line);
                }
            }
        }
        public static void SortByMonth()
        {
            List<string> list = Helpers.CreateListOfData();
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
        public static void SearchForDate()
        {

            List<string> lines = Helpers.CreateListOfData();
            Console.WriteLine("Enter a date to search for (YYYY-MM-DD):");
            string targetDate = Console.ReadLine();

            var filteredData = from line in lines
                               let parts = line.Split(',')
                               let dateTime = parts[0]
                               let dateTimeParts = dateTime.Split(' ')
                               let date = dateTimeParts[0]
                               where date == targetDate
                               select line;

            if (filteredData.Any())
            {
                var sortedData = filteredData.OrderBy(line =>
                {
                    string[] parts = line.Split(',');
                    try
                    {
                        return double.Parse(parts[2]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid temperature value found in the data: " + parts[2]);
                        return double.MinValue;
                    }
                });

                foreach (string line in sortedData)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No data found for the entered date.");
            }
        }
    }
}
