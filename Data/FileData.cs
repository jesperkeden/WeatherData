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
        public string Location { get; set; }
        public string Temperature { get; set; }
        public int Humidity { get; set; }

        public FileData(int year, int month, int day, int hour, int minutes, int seconds, string location, string temperature, int humidity)
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minutes = minutes;
            Seconds = seconds;
            Location = location;
            Temperature = temperature;
            Humidity = humidity;
        }
    }
}
