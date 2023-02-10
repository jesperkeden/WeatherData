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

    }
}
