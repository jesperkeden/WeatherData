using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Classes;
using WeatherData.Models;
using WeatherData.Classes;
using WeatherData.Data;

namespace WeatherData.Models
{
    internal class MoldRiskCalculator
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";

        //Per day
        public static Dictionary<DateTime, double> CalculateAverageMoldRiskPerday()
        {
            Dictionary<DateTime, double> riskList = new Dictionary<DateTime, double>();
            List<FileData> dataList = Helpers.ReadTextFile(filePath);

            var groupedData = dataList.GroupBy(x => x.DateTime.Date);
            foreach (var dayData in groupedData)
            {
                double avgTemp = Math.Round(dayData.Average(y => y.Temperature), 1);
                double avgHum = Math.Round(dayData.Average(y => y.Humidity), 1);
                double risk = CalculateMoldRisk(avgTemp, avgHum);
                riskList.Add(dayData.Key, risk);
            }

            return riskList;
        }
        
        public static Dictionary<DateTime, double> CalculateAverageMoldRiskPerMonth()
        {
            Dictionary<DateTime, double> riskList = new Dictionary<DateTime, double>();
            List<FileData> dataList = Helpers.ReadTextFile(filePath);

            var groupedData = dataList.GroupBy(x => new DateTime(x.DateTime.Year, x.DateTime.Month, 1));
            foreach (var monthData in groupedData)
            {
                double avgTemp = Math.Round(monthData.Average(y => y.Temperature), 1);
                double avgHum = Math.Round(monthData.Average(y => y.Humidity), 1);
                double risk = CalculateMoldRisk(avgTemp, avgHum);
                riskList.Add(monthData.Key, risk);
            }

            return riskList;
        }



        public static double CalculateMoldRisk(double temp, double hum)
        {
            double risk = 0;

            if ((temp > 0 && temp < 10) && (hum > 95 && hum <= 100))
            {
                risk = 100;
            }
            else if (temp < 0 || hum < 75)
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

                if (temp < 40)
                {
                    risk += 5;
                }

                //HUM
                if (hum > 80)
                {
                    risk += 10;
                }

                else if (hum > 90)
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
    }
}
