using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Classes;
using WeatherData.Models;

namespace WeatherData.Data
{
    internal class FileData
    {
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set; }
        public double RiskPercentage { get; set; }
        public static List<iMeasurable> CreateData()
        {
            List<iMeasurable> data = new List<iMeasurable>();
            data.Add(new Inomhus() { Location = "Inne", Name = "Inside" });
            data.Add(new Utomhus() { Location = "Ute", Name = "Outside" });

            return data;
        }
        public FileData(string location, double temperature, double humidity, DateTime date)
        {
            this.Location = location;
            this.Temperature = temperature;
            this.Humidity = humidity;
            this.DateTime = date;
            this.RiskPercentage = CalculateMoldRisk(temperature, humidity);
        }

        public static double CalculateMoldRisk(double temp, double hum)
        {
            double risk = 0;

            if (temp > 0 && temp < 10 && hum > 95 && hum <= 100)
            {
                risk = 100;
            }
            else if (temp < 0 && hum < 75)
            {
                risk = 0;
                return risk;
            }
            else
            {
                //TEMP
                if (temp < 10)
                {
                    risk += 20;
                }

                if (temp < 20)
                {
                    risk += 15;
                }

                if (temp < 30)
                {
                    risk += 10;
                }

                else if (temp < 40)
                {
                    risk += 5;
                }

                //HUM
                if (hum < 79)
                {
                    risk += 5;
                }
                if (hum > 80)
                {
                    risk += 10;
                }

                if (hum > 90)
                {
                    risk += 15;
                }

                else if (hum > 95)
                {
                    risk += 20;
                }
            }

            return risk;
        }

        public static void GroupByLocationAndPrintAvg(List<FileData> data)
        {
            var groupedData1 = data.Where(x => x.Location == "Ute")
                                   .GroupBy(x => x.Location);


            var groupedData = data.GroupBy(x => x.Location);

            foreach (var group in groupedData1)
            {
                Console.WriteLine("Location: " + group.Key);
                Console.WriteLine("   Date\t      Avg Temp\t      Avg Hum\t    Risk of mold");

                var groupedByDate = group.GroupBy(x => x.DateTime.Date)
                    .OrderByDescending(g => g.Average(x => x.Temperature)); //sortera efter hum/temp/date HÄR

                foreach (var dateGroup in groupedByDate)
                {
                    var avgTemp = dateGroup.Average(x => x.Temperature);
                    var avgHum = dateGroup.Average(x => x.Humidity);
                    var avgRisk = dateGroup.Average(x => x.RiskPercentage);
                    Console.WriteLine("{0}\t{1:0.0}\t\t{2:0.0}\t\t{3:0}%", dateGroup.Key.ToShortDateString(), avgTemp, avgHum, avgRisk);
                }

                Console.WriteLine();
            }
        }
        public static void GroupByMonthAndPrintAvg(List<FileData> fileDataList)
        {
            var groupedData = fileDataList.GroupBy(data => data.DateTime.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    AverageRiskPercentage = group.Average(data => data.RiskPercentage)
                });

            Console.WriteLine("Month\t\tAverage Risk Percentage");
            Console.WriteLine("---------------------------------------------");

            foreach (var group in groupedData)
            {
                Console.WriteLine("{0}\t\t{1}", group.Month, Math.Round(group.AverageRiskPercentage, 2));
            }
        }

    }
}
