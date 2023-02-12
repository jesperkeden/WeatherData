using System;
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
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.IO;

namespace WeatherData.Models
{
    public class Weather
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        private const string outPath = "../../../Data/test.txt";
        public string? Location { get; set; }
        public string Name { get; set; }
        public static List<iMeasurable> CreateData()
        {
            List<iMeasurable> data = new List<iMeasurable>();
            data.Add(new Inomhus() { Location = "Inne", Name = "Inside" });
            data.Add(new Utomhus() { Location = "Ute", Name = "Outside" });

            return data;
        }
        public virtual string MeteorologicalDate(int temp) { string nothing = string.Empty; return nothing; }
        public virtual void AvgValuesSearchDate()
        {
            List<FileData> dataList = new List<FileData>();
            dataList = ReadTextFile(filePath);
            DateTime inputDate = PromptUserForDate();

            // Filter the data list by date and location
            List<FileData> filteredData = dataList.Where(d => d.DateTime.Date == inputDate.Date && d.Location == Location).ToList();

            // If there is any data with the specified date and location
            if (filteredData.Count > 0)
            {
                // Calculate the average temperature and humidity for the filtered data
                double dataAvgTemp = filteredData.Average(x => x.Temperature);
                double dataAvgResult = filteredData.Average(x => x.Humidity);

                Console.WriteLine("Chosen location: " + Location);
                // Show the average temperature for the location "Inne"
                if (Location == "Inne")
                {
                    Console.WriteLine("\n\nAverage temperature: " + Math.Round(dataAvgTemp, 2) + "°C");
                }
                // Show the average temperature and humidity for the location "Ute"
                else if (Location == "Ute")
                {
                    Console.WriteLine("\n\nAverage temperature: " + Math.Round(dataAvgTemp, 2) + "°C");
                    Console.WriteLine("Average humidity: " + Math.Round(dataAvgResult, 2) + "%");
                }
                else
                {
                    // In case the location does not match "Inne" or "Ute"
                    Console.WriteLine();
                    Console.WriteLine(new String('-', 30));
                    Console.WriteLine("Couldn't find any data from inside or outside on that day.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            // If there is no data with the specified date and location
            else
            {
                Console.WriteLine();
                Console.WriteLine(new String('-', 30));
                Console.WriteLine("Couldn't find any data.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // Method to show the average temperature for a specific location
        public virtual void ShowTemperature()
        {
            // Initialize an empty list of FileData objects
            List<FileData> files = new List<FileData>();
            // Read the text file using the Helpers class and store the data in the files list
            files = Helpers.ReadTextFile(filePath);

            // Group the data in the files list by location
            var groupedData = files.Where(x => x.Location == Location)
                .GroupBy(x => x.Location);

            // Iterate through each group of data by location
            foreach (var group in groupedData)
            {
                // Write the location name to the console
                ("Location: " + group.Key).ViewBox(0, "");
                // Write the header for the temperature data to the console
                Console.WriteLine("\n   Date\t      Avg Temp");
                Console.WriteLine(new String('-', 22));

                // Group the data in the current group by date
                var groupedByDate = group.GroupBy(x => x.DateTime.Date)
                    // Order the data by the average temperature in descending order
                    .OrderByDescending(g => g.Average(x => x.Temperature));

                // Iterate through each group of data by date
                foreach (var dateGroup in groupedByDate)
                {
                    // Calculate the average temperature for the current group of data by date
                    var avgTemp = dateGroup.Average(x => x.Temperature);
                    // Write the date and average temperature to the console
                    Console.WriteLine("{0}\t{1:0.0}°C", dateGroup.Key.ToShortDateString(), avgTemp);
                }

                // Write a new line to the console
                Console.WriteLine();
            }
        }

        // Method to show the average humidity for a specific location
        public virtual void ShowHumidity()
        {
            // Create a list to store the data from the text file
            List<FileData> files = new List<FileData>();
            // Read the data from the text file and store it in the list
            files = Helpers.ReadTextFile(filePath);

            // Group the data by location
            var groupedData = files.Where(x => x.Location == Location)
                                  .GroupBy(x => x.Location);

            // Loop through each location group
            foreach (var group in groupedData)
            {
                ("Location: " + group.Key).ViewBox(0, "");
                Console.WriteLine("\n   Date\t    Avg Humidity");
                Console.WriteLine(new String('-', 24));

                // Group the data by date
                var groupedByDate = group.GroupBy(x => x.DateTime.Date)
                                         .OrderByDescending(g => g.Average(x => x.Humidity));

                // Loop through each date group
                foreach (var dateGroup in groupedByDate)
                {
                    // Calculate the average humidity for the date group
                    var avgHum = dateGroup.Average(x => x.Humidity);
                    // Write the date and average humidity to the console
                    Console.WriteLine("{0}\t{1:0.0}%", dateGroup.Key.ToShortDateString(), avgHum);
                }

                Console.WriteLine();
            }
        }

        // Method to show the average risk of mold for a specific location
        public virtual void ShowMold()
        {
            // Initialize a list to store the file data
            List<FileData> files = new List<FileData>();

            // Read the text file and store its contents in the `files` list
            files = Helpers.ReadTextFile(filePath);

            // Group the data by location
            var groupedData = files.Where(x => x.Location == Location)
            .GroupBy(x => x.Location);

            // Iterate through each group of data (each location)
            foreach (var group in groupedData)
            {
                // Write the location to the console
                ("Location: " + group.Key).ViewBox(0, "");
                Console.WriteLine("\n   Date\t    Risk of Mold");
                Console.WriteLine(new String('-', 24));

                // Group the data by date
                var groupedByDate = group.GroupBy(x => x.DateTime.Date)
                    .OrderByDescending(g => g.Average(x => x.RiskPercentage));

                // Iterate through each group of data (each date)
                foreach (var dateGroup in groupedByDate)
                {
                    // Calculate the average risk of mold for the date
                    var riskForMold = dateGroup.Average(x => x.RiskPercentage);
                    // Write the date and average risk of mold to the console
                    Console.WriteLine("{0}\t{1:0.0}%", dateGroup.Key.ToShortDateString(), riskForMold);
                }

                // Write a blank line to separate the data for different locations
                Console.WriteLine();
            }
        }

        // Method to create a dictionary of average temperature values for meteorological Date (Fall/Winter)
        public static Dictionary<DateTime, double> CreateDicForMeteorologicalDate()
        {
            // File path for the data file
            string filePath = "../../../Data/tempdata5-med fel.txt";

            // List to store the data from the file
            List<FileData> dataList = new List<FileData>();

            // Read the data from the file and store it in the list
            dataList = Helpers.ReadTextFile(filePath);

            // Filter the data by location
            var filteredData = dataList.Where(x => x.Location == "Ute").ToList();

            // Group the data by date
            var dayData = filteredData.GroupBy(x => x.DateTime.Date);

            // Dictionary to store the average temperature values for the autumn season
            Dictionary<DateTime, double> autumnList = new Dictionary<DateTime, double>();

            // Loop through each date group, get the average temperature for the day and add it to the dictionary
            foreach (var day in dayData.OrderBy(x => x.Key))
            {
                // Get the date in the desired format
                string stringToParse = day.Key.ToString("dd MMM");

                // Parse the date string to a DateTime object
                var parsedDate = DateTime.Parse(stringToParse);

                // Calculate the average temperature for the day
                var avgTemp = (Math.Round(day.Average(y => y.Temperature), 1));

                // Add the date and average temperature to the dictionary
                autumnList.Add(parsedDate, avgTemp);
            }

            // Return the dictionary of average temperature values
            return autumnList;
        }

    }
}
