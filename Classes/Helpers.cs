using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherData.Data;
using WeatherData.Models;

namespace WeatherData.Classes
{
    internal class Helpers
    {
        private const string filePath = "../../../Data/tempdata5-med fel.txt";
        private const string outPath = "../../../Data/Log.txt";
        public delegate void WriteFileDel();

        static bool IsValidDate(string date, out DateTime inputDate)
        {
            string pattern = @"^(\d{4})([-./])(\d{2})\2(\d{2})$";
            DateTime earliest = new DateTime(2016, 6, 01);
            DateTime latest = new DateTime(2016, 12, 31);
            inputDate = default;

            if (!Regex.IsMatch(date, pattern))
            {
                return false;
            }

            try
            {
                inputDate = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                Console.Clear();
                Console.WriteLine("Did you try a date that doesn't exist?");
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }


            if (inputDate >= earliest && inputDate <= latest)
            {
                return true;
            }
            return false;
        }
        public static DateTime PromptUserForDate()
        {
            Console.WriteLine("Enter a date in yyyy-mm-dd format:");
            string? input = Console.ReadLine();
            while (true)
            {
                if (IsValidDate(input, out var inputDate))
                {
                    Console.Clear();
                    ("Date entered: ").ViewBox(0, inputDate.ToString("yyyy-MM-dd"));

                    return inputDate;
                }
                else
                {
                    ("Invalid date format: ").ViewBox(3, (input == string.Empty ? "No date selected" : input));
                    Console.ReadKey();
                    Console.Clear();
                }
                Console.WriteLine("Enter a date in yyyy-mm-dd format:");
                input = Console.ReadLine();
            }
        }
        public static List<FileData> ReadTextFile(string filePath)
        {
            List<FileData> logs = new List<FileData>();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The file could not be found at the specified path: " + filePath);
            }

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line = string.Empty;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Match match = Regex.Match(line, @"(\d{4})([-.])(0[1-9]|1[0-2])([-.])(0[1-9]|[1-2][0-9]|3[0-1])([\w\s])(0[0-9]|1[0-9]|2[0-3])([:;.,-_])([0-5][0-9])([:;.,-_])([0-5][0-9]),([Inne|Ute]+),(-?\d+[\W\S]?\d*?),(-?\d+[\W\S]?\d+)?$");

                        if (match.Success)
                        {
                            try
                            {
                                DateTime date = DateTime.ParseExact(
                                    match.Groups[1].Value + "-" + match.Groups[3].Value + "-" + match.Groups[5].Value + " " +
                                    match.Groups[7].Value + ":" + match.Groups[9].Value + ":" + match.Groups[11].Value,
                                    "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                                if (date.Month == 5 && date.Year == 2016 || date.Month == 1 && date.Year == 2017)
                                {
                                    continue;
                                }
                                string location = match.Groups[12].Value;
                                double temperature;
                                if (!double.TryParse(match.Groups[13].Value, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out temperature))
                                {
                                    Console.WriteLine("Could not parse temperature value.");
                                    continue;
                                }
                                double humidity;
                                if (!double.TryParse(match.Groups[14].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out humidity))
                                {
                                    Console.WriteLine("Could not parse humidity value.");
                                    continue;
                                }
                                FileData fileData = new FileData(location, temperature, humidity, date);
                                logs.Add(fileData);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error parsing line: " + ex.Message);
                            }
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                throw;
            }
            return logs;
        }
        public static void PrintToFile()
        {

            List<FileData> logs = Helpers.ReadTextFile(filePath);

            // Initialize dictionaries for average temperature, humidity and mold risk for both inside and outside locations

            //Temperature
            Dictionary<string, double> averageTemperatureInne = new Dictionary<string, double>();
            Dictionary<string, double> averageTemperatureUte = new Dictionary<string, double>();
            Dictionary<string, double> countTemperatureInne = new Dictionary<string, double>();
            Dictionary<string, double> countTemperatureUte = new Dictionary<string, double>();

            //Humidity
            Dictionary<string, double> averageHumidityInne = new Dictionary<string, double>();
            Dictionary<string, double> averageHumidityUte = new Dictionary<string, double>();
            Dictionary<string, double> countHumidityInne = new Dictionary<string, double>();
            Dictionary<string, double> countHumidityUte = new Dictionary<string, double>();

            //Mold
            Dictionary<string, double> averageRiskInne = new Dictionary<string, double>();
            Dictionary<string, double> averageRiskUte = new Dictionary<string, double>();
            Dictionary<string, double> countRiskInne = new Dictionary<string, double>();
            Dictionary<string, double> countRiskUte = new Dictionary<string, double>();


            // Iterate through each log entry
            foreach (FileData log in logs)
            {
                // Extract the month from the date-time of the log entry
                string month = log.DateTime.ToString("MMM");

                // Check if the log entry location is inside or outside
                if (log.Location == "Inne")
                {
                    // If the month does not exist in the dictionary, initialize its values
                    if (!averageTemperatureInne.ContainsKey(month))
                    {
                        averageTemperatureInne[month] = 0;
                        averageHumidityInne[month] = 0;
                        averageRiskInne[month] = 0;

                        countTemperatureInne[month] = 0;
                        countHumidityInne[month] = 0;
                        countRiskInne[month] = 0;
                    }
                    // Add the values of the log entry to the corresponding month in the dictionaries
                    averageTemperatureInne[month] += log.Temperature;
                    averageHumidityInne[month] += log.Humidity;
                    averageRiskInne[month] += log.RiskPercentage;

                    countTemperatureInne[month]++;
                    countHumidityInne[month]++;
                    countRiskInne[month]++;
                }
                else
                {
                    // If the month does not exist in the dictionary, initialize its values
                    if (!averageTemperatureUte.ContainsKey(month))
                    {
                        averageTemperatureUte[month] = 0;
                        averageHumidityUte[month] = 0;
                        averageRiskUte[month] = 0;

                        countTemperatureUte[month] = 0;
                        countHumidityUte[month] = 0;
                        countRiskUte[month] = 0;
                    }
                    // Add the values of the log entry to the corresponding month in the dictionaries
                    averageTemperatureUte[month] += log.Temperature;
                    averageHumidityUte[month] += log.Humidity;
                    averageRiskUte[month] += log.RiskPercentage;

                    countTemperatureUte[month]++;
                    countHumidityUte[month]++;
                    countRiskUte[month]++;
                }
            }
            // Calculate the average values of temperature, humidity and risk from Inside, and writes the result to the output file.
            foreach (var entry in averageTemperatureInne)
            {
                string month = entry.Key;
                averageTemperatureInne[month] /= countTemperatureInne[month];
                averageHumidityInne[month] /= countHumidityInne[month];
                averageRiskInne[month] /= countRiskInne[month];
            }

            // Calculate the average values of temperature, humidity and risk from Outside, and writes the result to the output file.
            foreach (var entry in averageTemperatureUte)
            {
                string month = entry.Key;
                averageTemperatureUte[month] /= countTemperatureUte[month];
                averageHumidityUte[month] /= countHumidityUte[month];
                averageRiskUte[month] /= countRiskUte[month];
            }


            using (StreamWriter writer = new StreamWriter(outPath))
            {
                writer.WriteLine("---------------");
                writer.WriteLine("Avg Temperature");
                writer.WriteLine("---------------");
                foreach (var entry in averageTemperatureInne)
                {
                    string month = entry.Key;
                    double avgInneTemp = entry.Value;
                    double avgUteTemp = averageTemperatureUte[month];

                    writer.WriteLine(month + "\t" +
                        "Inside: " + avgInneTemp.ToString("0.00", CultureInfo.InvariantCulture) + "°C\t\t" +
                        "Outside: " + avgUteTemp.ToString("0.00", CultureInfo.InvariantCulture) + "°C");
                }

                writer.WriteLine("");
                writer.WriteLine("------------");
                writer.WriteLine("Avg Humidity");
                writer.WriteLine("------------");
                foreach (var entry in averageHumidityInne)
                {
                    string month = entry.Key;
                    double avgInneHum = averageHumidityInne[month];
                    double avgUteHum = averageHumidityUte[month];

                    writer.WriteLine(month + "\t" +
                        "Inside: " + avgInneHum.ToString("0.00", CultureInfo.InvariantCulture) + "%\t\t" +
                        "Outside: " + avgUteHum.ToString("0.00", CultureInfo.InvariantCulture) + "%");
                }

                writer.WriteLine("");
                writer.WriteLine("---------------------------");
                writer.WriteLine("Avg Risk for mold per month");
                writer.WriteLine("---------------------------");
                foreach (var entry in averageRiskInne)
                {
                    string month = entry.Key;
                    double avgInneRisk = averageRiskInne[month];
                    double avgUteRisk = averageRiskUte[month];

                    writer.WriteLine(month + "\t" +
                        "Inside: " + avgInneRisk.ToString("0.00", CultureInfo.InvariantCulture) + "%\t\t" +
                        "Outside: " + avgUteRisk.ToString("0.00", CultureInfo.InvariantCulture) + "%");
                }

                Utomhus utomHus = new();
                string resultWinter = utomHus.MeteorologicalDate(0);
                string resultFall = utomHus.MeteorologicalDate(10);
                writer.WriteLine("");
                writer.WriteLine("------------------");
                writer.WriteLine("Meterological Fall");
                writer.WriteLine("------------------");
                writer.WriteLine();
                writer.WriteLine(resultFall);
                writer.WriteLine("");
                writer.WriteLine("--------------------");
                writer.WriteLine("Meterological Winter");
                writer.WriteLine("--------------------");
                writer.WriteLine();
                writer.WriteLine(resultWinter);

                writer.WriteLine("");
                writer.WriteLine("-----------------------------------");
                writer.WriteLine("Algorithm for calculating Mold Risk");
                writer.WriteLine("-----------------------------------");
                writer.WriteLine();
                string algorithm = @"  public static double CalculateMoldRisk(double temp, double hum)
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
        }";
                writer.WriteLine(algorithm);
            }
        }

    }
}
