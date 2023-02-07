﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static WeatherData.Data.FileData;
using WeatherData.Data;
using static WeatherData.Program;

namespace WeatherData
{
    public interface iMeasurable
    {
        public string? Location { get; set; }

        void AvgValues();
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
        public virtual void AvgValues()
        {
            List<FileData> dataList = new List<FileData>();
            dataList = Helpers.ReadTextFile(filePath);
            DateTime inputDate = PromptUserForDate();

            List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == Location).ToList();

            double dataAvgTemp = filteredData.Average(x => x.Temperature);
            double dataAvgResult = filteredData.Average(x => x.Humidity);

            if (Location == "Inne")
            {
                Console.WriteLine("Average temperature inside: " + Math.Round(dataAvgTemp, 2));
            }
            else if (Location == "Ute")
            {
                Console.WriteLine("Average temperature outside: " + Math.Round(dataAvgTemp, 2));
                Console.WriteLine("Average humidity: " + Math.Round(dataAvgResult, 2));
            }
            else
            {
                Console.WriteLine("Couldnt find any data from inside on that day.");
            }
        }
        public virtual void MaxMinWeatherDay()
        {
            List<FileData> dataList = new List<FileData>();
            dataList = Helpers.ReadTextFile(filePath);
            double maxDay = 0;
            double minDay = 0;
            var filteredData = dataList.Where(x => x.Location == Location).ToList();
            var locationList = filteredData.GroupBy(x => x.DateTime.Date).ToList();
            List<double> results = new();
            foreach (var d in locationList)
            {
                double avgTempPerDay = d.Average(x => x.Temperature);
                results.Add(avgTempPerDay);
                maxDay = results.Max();
                minDay = results.Min();
            }
            Console.WriteLine("Hottest day of the period is: " + Math.Round(maxDay, 2));
            Console.WriteLine("Coldest day of the period is: " + Math.Round(minDay, 2));
        }
    }
    public class Inomhus : Data1, iMeasurable
    {
    }
    public class Utomhus : Data1, iMeasurable
    {
        public static void MeteorloghalWinther()
        {

        }
    }
}
