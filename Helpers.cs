using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
                    int humidityInt = Convert.ToInt32(humidity);

                    if (year == 2016 && month == 05 || year == 2017)
                    {
                        
                    }
                    else
                    {
                        files.Add(new FileData(year, month, day, hour, minute, second, location, temperature, humidityInt));
                    }


                }

            }
            return files;
        }

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
