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

        void AvgValuesSearchDate();
        void ShowTemperature();
        void ShowHumidity();
        void ShowMold();
        string MeteorologicalDate(int temp);

    }
}
