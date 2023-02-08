using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherData
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

        private static void RunMainMenu()
        {
            var mainMenuEnums = (Enum.GetNames(typeof(Enums.MainMenu)));
            Menu mainMenu = new Menu(mainMenuEnums);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    RunInsideMenu();
                    break;
                case 1:
                    RunOutsideMenu();
                    break;
                case 2:
                    Exit();
                    break;
            }
        }
        public static void RunInsideMenu()
        {
            var InsideMenuEnums = (Enum.GetNames(typeof(Enums.InsideMenu)));
            Menu insideMenu = new Menu(InsideMenuEnums);
            int selectedIndex = insideMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("Medeltemperatur för valt datum (sökmöjlighet med validering)");
                    break;
                case 1:
                    //HotNCold();
                    Console.WriteLine("Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                    break;
                case 2:
                    Console.WriteLine("Sortering av torrast till fuktigaste dagen enligt medeltemperatur per dag");
                    break;
                case 3:
                    Console.WriteLine("Sortering av minst till störst risk av mögel");
                    break;
                case 4:
                    RunMainMenu();
                    break;
            }
        }
        public static void RunOutsideMenu()
        {
            var outsideMenuEnums = (Enum.GetNames(typeof(Enums.OutsideMenu)));
            Menu outsideMenu = new Menu(outsideMenuEnums);
            int selectedIndex = outsideMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Console.WriteLine("Medeltemperatur för valt datum (sökmöjlighet med validering)");
                    break;
                case 1:
                    Console.WriteLine("Sortering av varmast till kallaste dagen enligt medeltemperatur per dag");
                    break;
                case 2:
                    Console.WriteLine("Sortering av torrast till fuktigaste dagen enligt medeltemperatur per dag");
                    break;
                case 3:
                    Console.WriteLine("Sortering av minst till störst risk av mögel");
                    break;
                case 4:
                    Console.WriteLine("Datum för meteorologisk höst");
                    break;
                case 5:
                    Console.WriteLine("Datum för meteorologisk vinter(OBS Mild vinter!)");
                    break;
                case 6:
                    RunMainMenu();
                    break;
            }
        }
        public static void Start()
        {
            RunMainMenu();
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
