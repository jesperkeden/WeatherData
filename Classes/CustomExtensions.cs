using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeatherData.Classes
{
    public static class CustomExtensions
    {
        public static void ViewBox(this string input, int startPosY, string date)
        {
            Console.SetCursorPosition(2, startPosY);
            Console.WriteLine('┌' + new String('─', input.Length + date.Length + 3) + '┐');
            Console.SetCursorPosition(2, startPosY + 1);
            Console.WriteLine("│ " + input + " " + date + " │");
            Console.SetCursorPosition(2, startPosY + 2);
            Console.WriteLine('└' + new String('─', input.Length + date.Length + 3) + '┘');
        }
    }
}
