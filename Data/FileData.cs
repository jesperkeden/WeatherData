using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData.Data
{
    internal class FileData
    {
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set; }
        //public double Risk { get; set; }

        public FileData(string location, double temperature, double humidity, DateTime date)
        {
            Location = location;
            Temperature = temperature;
            Humidity = humidity;
            DateTime = date;
        }
    }
}
