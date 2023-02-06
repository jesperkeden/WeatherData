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
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public double HumidityDouble { get; set; }
        public double TemperatureDouble { get; set; }

        public FileData(int year, int month, int day, int hour, int minutes, int seconds, string location, string temperature, string humidity/* DateTime dateTime*/)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minutes = minutes;
            Seconds = seconds;
            //DateTime = dateTime;
            Location = location;
            Temperature = temperature;
            Humidity = humidity;
        }
        public FileData(string location, double temperatureDouble, double humidityDouble, DateTime date)
        {
            Location = location;
            TemperatureDouble = temperatureDouble;
            HumidityDouble = humidityDouble;
            DateTime = date;
        }
    }
}
