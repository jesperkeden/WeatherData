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
    public interface iMeasurable
    {
        public string? Location { get; set; }
        public string Name { get; set; }

        void AvgValues();
        void MeteorologicalDate(int temp);
        void ShowTemperature(string location);
        void ShowHumidity(string location);
        void ShowMold(string location);

    }
    public class Data1
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        private const string outPath = "../../../Data/test.txt";
        public string? Location { get; set; }
        public string Name { get; set; }


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
        public virtual void ShowTemperature(string location)
        {
            List<FileData> files = new List<FileData>();
            files = Helpers.ReadTextFile(filePath);
            var groupedData = files.Where(x => x.Location == location)
            .GroupBy(x => x.Location);

            foreach (var group in groupedData)
            {
                Console.WriteLine("Location: " + group.Key);
                Console.WriteLine("   Date\t      Avg Temp");

                var groupedByDate = group.GroupBy(x => x.DateTime.Date)
                    .OrderByDescending(g => g.Average(x => x.Temperature));

                foreach (var dateGroup in groupedByDate)
                {
                    var avgTemp = dateGroup.Average(x => x.Temperature);
                    Console.WriteLine("{0}\t{1:0.0}", dateGroup.Key.ToShortDateString(), avgTemp);
                }

                Console.WriteLine();
            }
        }
        public virtual void ShowHumidity(string location)
        {
            List<FileData> files = new List<FileData>();
            files = Helpers.ReadTextFile(filePath);
            var groupedData = files.Where(x => x.Location == location)
            .GroupBy(x => x.Location);

            foreach (var group in groupedData)
            {
                Console.WriteLine("Location: " + group.Key);
                Console.WriteLine("   Date\t    Avg Humidity");

                var groupedByDate = group.GroupBy(x => x.DateTime.Date)
                    .OrderByDescending(g => g.Average(x => x.Humidity));

                foreach (var dateGroup in groupedByDate)
                {
                    var avgHum = dateGroup.Average(x => x.Humidity);
                    Console.WriteLine("{0}\t{1:0.0}", dateGroup.Key.ToShortDateString(), avgHum);
                }

                Console.WriteLine();
            }
        }
        public virtual void ShowMold(string location)
        {
            List<FileData> files = new List<FileData>();
            files = Helpers.ReadTextFile(filePath);
            var groupedData = files.Where(x => x.Location == location)
            .GroupBy(x => x.Location);

            foreach (var group in groupedData)
            {
                Console.WriteLine("Location: " + group.Key);
                Console.WriteLine("   Date\t    Risk of Mold");

                var groupedByDate = group.GroupBy(x => x.DateTime.Date)
                    .OrderByDescending(g => g.Average(x => x.RiskPercentage));

                foreach (var dateGroup in groupedByDate)
                {
                    var riskForMold = dateGroup.Average(x => x.RiskPercentage);
                    Console.WriteLine("{0}\t{1:0.0}%", dateGroup.Key.ToShortDateString(), riskForMold);
                }

                Console.WriteLine();
            }
        }
        public static Dictionary<DateTime, double> CreateDicForAutumn()
        {
            string filePath = "../../../Data/tempdata5-med fel.txt";


            List<FileData> dataList = new List<FileData>();
            dataList = Helpers.ReadTextFile(filePath);
            var filteredData = dataList.Where(x => x.Location == "Ute").ToList();
            var dayData = filteredData.GroupBy(x => x.DateTime.Date);
            Dictionary<DateTime, double> autumnList = new Dictionary<DateTime, double>();

            foreach (var day in dayData.OrderBy(x => x.Key))
            {
                string stringToParse = day.Key.ToString("dd MMM");
                var parsedDate = DateTime.Parse(stringToParse);
                var avgTemp = (Math.Round(day.Average(y => y.Temperature), 1));
                autumnList.Add(parsedDate, avgTemp);
            }

            return autumnList;

        }

    }
    public class Inomhus : Data1, iMeasurable
    {
        public void MeteorologicalDate(int temp)
        {

        }
    }
    public class Utomhus : Data1, iMeasurable
    {
        public void MeteorologicalDate(int temp)
        {
            string filePath = "../../../Data/tempdata5-med fel.txt";
            Dictionary<DateTime, double> list = CreateDicForAutumn();

            DateTime startDate = new DateTime(2016, 08, 01);
            int tempCount = 0;

            foreach (var day in list)
            {
                if (day.Key > startDate)
                {
                    if (tempCount < 5)
                    {
                        if (day.Value <= temp)
                        {
                            tempCount++;
                        }
                        else
                        {
                            startDate = day.Key;
                            tempCount = 0;
                        }
                    }
                }
            }
            string result = ("Meterological " + (temp == 10 ? "fall" : "winter") + " occurs on the " + startDate.ToString("dd MMM"));
            Console.WriteLine(result); //Gjorde till result, men oklart hur jag ska norpa den till PrintToFile(); utan att göra om denna
            //till en string samt static.
        }
    }
}
