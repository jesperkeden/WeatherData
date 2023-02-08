using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static WeatherData.Data.FileData;
using WeatherData.Data;
using static WeatherData.Program;
using System.Collections.Immutable;
using WeatherData.Classes;
using static WeatherData.Classes.Helpers;

namespace WeatherData.Models
{
    public interface iMeasurable
    {
        public string? Location { get; set; }

        void AvgValues();
        void MaxMinWeatherDay(string chooseOrderBy);
    }
    public class Data1
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        public string? Location { get; set; }

        public static List<iMeasurable> CreateData()
        {
            List<iMeasurable> data = new List<iMeasurable>();
            data.Add(new Inomhus() { Location = "Inne" });
            data.Add(new Utomhus() { Location = "Ute" });

            return data;
        }
        // Search for a specific Date
        public virtual void AvgValues()
        {
            List<FileData> dataList = new List<FileData>();
            dataList = ReadTextFile(filePath);
            DateTime inputDate = PromptUserForDate();

            List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == Location).ToList();
            if (filteredData.Count > 0)
            {
                double dataAvgTemp = filteredData.Average(x => x.Temperature);
                double dataAvgResult = filteredData.Average(x => x.Humidity);

                if (Location == "Inne")
                {
                    Console.WriteLine(new String('=', 30));
                    Console.WriteLine("Average temperature inside: " + Math.Round(dataAvgTemp, 2));
                }
                else if (Location == "Ute")
                {
                    Console.WriteLine(new String('=', 30));
                    Console.WriteLine("Average temperature outside: " + Math.Round(dataAvgTemp, 2));
                    Console.WriteLine(new String('-', 30));
                    Console.WriteLine("Average humidity: " + Math.Round(dataAvgResult, 2));
                }
                else
                {
                    // This will probably never happen, but better safe than sorry..
                    Console.WriteLine(new String('-', 30));
                    Console.WriteLine("Couldn't find any data from inside on that day.");
                }
            }
            else
            {
                Console.WriteLine(new String('-', 30));
                Console.WriteLine("Couldn't find any data from inside on that day.");
            }
        }

        // Warm/Cold || Wet/Dry
        public virtual void MaxMinWeatherDay(string chooseOrderBy)
        {
            List<FileData> dataList = new List<FileData>();
            dataList = Helpers.ReadTextFile(filePath);
            var filteredData = dataList.Where(x => x.Location == Location).ToList();
            var dayData = filteredData.GroupBy(x => x.DateTime.Date);
            var sortedData = dayData;

            if (chooseOrderBy == "Temperature")
            {
                sortedData = dayData.OrderByDescending(x => x.Average(y => y.Temperature));
            }
            else if (chooseOrderBy == "Humidity")
            {
                sortedData = dayData.OrderByDescending(x => x.Average(y => y.Humidity));
            }

            foreach (var item in sortedData)
            {
                if (chooseOrderBy == "Temperature")
                {
                    Console.WriteLine($"{item.Key.ToString("dd MMM")} - Average temperature: {Math.Round(item.Average(y => y.Temperature), 1)}°C");
                }
                if (chooseOrderBy == "Humidity")
                {
                    Console.WriteLine($"{item.Key.ToString("dd MMM")} - Average humidity: {Math.Round(item.Average(y => y.Humidity), 1)}%");
                }
            }
        }
    }
    public class Inomhus : Data1, iMeasurable { }
    public class Utomhus : Data1, iMeasurable
    {
        public static void MeteorologicalWinther()
        {

        }
        public static void MeteorologicalFall() // Same method as winther? or maybe delegate??
        {

        }
    }
}
