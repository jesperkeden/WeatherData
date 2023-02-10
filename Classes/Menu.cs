using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherData.Models;
using static WeatherData.Models.Utomhus;
using static WeatherData.Classes.CustomExtensions;

namespace WeatherData.Classes
{
    internal class Menu
    {
        private int SelectedIndex;
        private string[] Options;

        public Menu(string[] options)
        {
            Options = options;
            SelectedIndex = 0;
        }

        private static void RunMainMenu(List<iMeasurable> classList)
        {
            var mainMenuEnums = Enum.GetNames(typeof(Enums.MainMenu));
            Menu mainMenu = new Menu(mainMenuEnums);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunInsideMenu(classList[0]);
                    break;
                case 1:
                    RunOutsideMenu(classList[1]);
                    break;
                case 2:
                    Exit();
                    break;
            }
        }
        public static void RunInsideMenu(iMeasurable insideClass)
        {
            var InsideMenuEnums = Enum.GetNames(typeof(Enums.InsideMenu));
            Menu insideMenu = new Menu(InsideMenuEnums);
            int selectedIndex = insideMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    insideClass.AvgValuesSearchDate();
                    Console.ReadKey();
                    break;
                case 1:     
                    Console.Clear();
                    insideClass.ShowTemperature();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    insideClass.ShowHumidity();
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    insideClass.ShowMold();
                    Console.ReadKey();
                    break;
                case 4:
                    break;
            }
        }
        public static void RunOutsideMenu(iMeasurable outsideClass)
        {
            var outsideMenuEnums = Enum.GetNames(typeof(Enums.OutsideMenu));
            Menu outsideMenu = new Menu(outsideMenuEnums);
            int selectedIndex = outsideMenu.Run();
            string resultWinter = string.Empty;
            string resultFall = string.Empty;

            switch (selectedIndex)
            {
                case 0:
                    Console.Clear();
                    outsideClass.AvgValuesSearchDate();
                    Console.ReadKey();
                    break;
                case 1:
                    Console.Clear();
                    outsideClass.ShowTemperature();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    outsideClass.ShowHumidity();
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    outsideClass.ShowMold();
                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    outsideClass.Name.ViewBox(0, "Fall");
                    resultFall = outsideClass.MeteorologicalDate(10);
                    Console.WriteLine(resultFall);
                    Console.ReadKey();
                    break;
                case 5:
                    Console.Clear();
                    outsideClass.Name.ViewBox(0, "Winter");
                    resultWinter = outsideClass.MeteorologicalDate(0);
                    Console.WriteLine(resultWinter);
                    Console.ReadKey();
                    break;
                case 6:
                    break;
            }
        }
        public static void Start(List<iMeasurable> classList)
        {
            RunMainMenu(classList);
        }
        private void DisplayOptions()
        {
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i].Replace("_", " ");
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = ">";
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine($"{prefix} {currentOption}");
            }
            Console.ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
        private static void Exit()
        {
            Console.WriteLine("\nPress any key to exit..");
            Console.ReadKey(true);
            Environment.Exit(0);
        }
    }
}
