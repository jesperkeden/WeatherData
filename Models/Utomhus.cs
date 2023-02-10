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
    public class Utomhus : Weather, iMeasurable
    {

        // Method to find meteorological winter or fall based on average temperature
        public override string MeteorologicalDate(int temp)
        {
            // File path to read data from
            string filePath = "../../../Data/tempdata5-med fel.txt";



            // Call method to create dictionary with autumn average temperature data
            Dictionary<DateTime, double> list = CreateDicForMeteorologicalDate();


            // Start date for meteorological analysis
            DateTime startDate = new DateTime(2016, 08, 01);


            // Counter to track consecutive days with temperature lower or equal to given input
            int tempCount = 0;


            // Loop through the dictionary to find meteorological winter or fall
            foreach (var day in list)
            {

                // Check if the date is after the start date
                if (day.Key > startDate)
                {
                    // Check if 5 consecutive days with temperature lower or equal to input have not been found yet
                    if (tempCount < 5)
                    {
                        // Check if the temperature for the day is lower or equal to the input
                        if (day.Value <= temp)
                        {
                            // Increment the tempCount if the temperature for the day is lower or equal to the input
                            tempCount++;
                        }
                        else
                        {
                            // Reset the start date and the tempCount if the temperature for the day is not lower or equal to the input
                            startDate = day.Key;
                            tempCount = 0;
                        }
                    }
                }
            }

            // Create the result string based on the input temperature and the start date found
            string result = ("Meterological " + (temp == 10 ? "fall" : "winter") + " occurs on the " + startDate.ToString("dd MMM"));

            return result;
        }
    }
}
