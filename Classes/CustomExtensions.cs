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
        public static void ViewBox(this string input)
        {
            Console.SetCursorPosition(2, 0);
            Console.WriteLine('┌' + new String('─', input.Length + 2) + '┐');
            Console.SetCursorPosition(2, 1);
            Console.WriteLine("│ " + input + " │");
            Console.SetCursorPosition(2, 2);
            Console.WriteLine('└' + new String('─', input.Length + 2) + '┘');
        }
    }
}
