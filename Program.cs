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
using WeatherData.Models;

namespace WeatherData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool runProgram = true;
            WriteFileDel delPrint = PrintToFile;

            delPrint();
            while (runProgram)
            {
                List<iMeasurable> classList = Weather.CreateData();

                Menu.Start(classList);
            }
        }
    }
}