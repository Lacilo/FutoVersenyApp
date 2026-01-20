// TODO:
// - Előzőleges terv (ami kész arra is)
// - Dokumentáció
// - Pulzus időbeli változása
// - Átlagsebesség időbeli változása

using System;
using System.Collections.Generic;
using System.IO;
using FutoversenyApp.Models;
using FutoversenyApp.Controllers;
using menu.Models;

namespace FutoversenyApp
{
    internal class Program
    {
        public static ConsoleColor background = ConsoleColor.Black;
        public static ConsoleColor textcolor = ConsoleColor.White;
        public static ConsoleColor highlight = ConsoleColor.White;
        public static ConsoleColor highlightText = ConsoleColor.Black;

        public static List<Futas> futasok = new List<Futas>();
        public static Display display = new Display();
        public static User user = User.UserJsonReader();

        public static void Main()
        {
            Console.BackgroundColor = background;
            FilesExist();

            //for (int i = 0; i < 50; i++)
            //{
            //    futasok.Add(new Futas());
            //}
            futasok = Futas.RunsJsonReader();

            Menu();
        }

        public static void Menu()
        {
            // ha létezik a User.json fájl, akkor a menüben jelezze, hogy meg van adva a személyes adat
            bool megadva = false;
            if (new FileInfo("User.json").Length > 0)
            {
                megadva = true;
            }

            string megadvat = "Megadása";
            if (megadva)
            {
                megadvat = "Szerkesztése";
            }

            string[] items =
            {
                $"Személyes Adatok {megadvat}",
                "Edzés Rögzítése",
                "Edzések Kezelése",
                "Súly és Pulzus Változás",
                "Egyéb Adatok",
                "Beállítások",
                "Kilépés"
            };

            int selected = 0;

            while (true)
            {
                MenuDrawer("================= Futó App =================", items, selected);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Tab || key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % items.Length;
                    Console.BackgroundColor = background;
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = (selected - 1 + items.Length) % items.Length;
                    Console.BackgroundColor = background;
                }
                else if (key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    return;
                }
            }

            switch (selected)
            {
                case 0:
                    if (megadva)
                    {
                        Controller.SzAdatokSzerk();
                        break;
                    }
                    else
                    {
                        Controller.SzAdatok();
                        break;
                    }
                case 1:
                    Controller.Edzes(futasok);
                    break;
                case 2:
                    display.UpdateFutasok(futasok);
                    display.DisplayFutasok(0);
                    display.GetDisplayInput();
                    break;
                case 3:
                    display.DisplayWeightAndBPMChangeMenu(user);
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Megtett össztáv: " + Controller.MegtettOssztav(futasok) + " m");
                    Console.WriteLine("\nEnterrel vissza a főmenübe");
                    Console.ReadKey();
                    Menu();
                    break;
                case 5:
                    Settings(0);
                    break;
                case 6:
                    Exit();
                    break;
            }
        }

        static void Exit()
        {
            Console.BackgroundColor = background;
            string[] items = { "Igen", "Nem" };
            int selected = 0;

            while (true)
            {
                MenuDrawer("================= Futó App =================",items, selected);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Tab || key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % items.Length;
                    Console.BackgroundColor = background;
                }

                else if (key == ConsoleKey.UpArrow)
                {
                    selected = (selected - 1 + items.Length) % items.Length;
                    Console.BackgroundColor = background;
                }

                else if (key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    Main();
                    return;
                }
            }

            switch (selected)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Main();
                    break;
            }
        }

        static void MenuDrawer(string title, string[] items, int selected)
        {
            Console.Clear();

            Console.BackgroundColor = background;
            Console.ForegroundColor = textcolor;
            CenterEngine.CenterLine(title);
            Console.WriteLine();

            for (int i = 0; i < items.Length; i++)
            {
                if (i == selected)
                {
                    Console.BackgroundColor = highlight;
                    Console.ForegroundColor = highlightText;
                }
                else
                {
                    Console.BackgroundColor = background;
                    Console.ForegroundColor = textcolor;
                }

                CenterEngine.CenterLine($"{items[i]}");
            }
            Console.ResetColor();
        }

        static void Settings(int selected) 
        {
            Console.BackgroundColor = background;
            Console.Clear();
            Console.BackgroundColor = background;
            Console.ForegroundColor = textcolor;
            //témák váltása

            string[] items =
            {
                "Fekete-Fehér (default)",
                "Fehér-Fekete",
                "Fekete-Piros-Zöld",
                "Piros-Zöld",
                "Főmenü"
            };

            while (true)
            {
                MenuDrawer("================= Témák: =================",items, selected);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Tab || key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % items.Length;
                    Console.BackgroundColor = background;
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = (selected - 1 + items.Length) % items.Length;
                    Console.BackgroundColor = background;
                }
                else if (key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    return;
                }
            }

            switch (selected)
            {
                case 0:
                    background = ConsoleColor.Black;
                    textcolor = ConsoleColor.White;
                    highlight = ConsoleColor.White;
                    highlightText = ConsoleColor.Black;
                    Console.BackgroundColor = background;
                    Console.ForegroundColor = textcolor;
                    Settings(4);
                    break;
                case 1:
                    background = ConsoleColor.White;
                    textcolor = ConsoleColor.Black;
                    highlight = ConsoleColor.Black;
                    highlightText = ConsoleColor.White;
                    Console.BackgroundColor = background;
                    Console.ForegroundColor = textcolor;
                    Settings(4);
                    break;
                case 2:
                    background = ConsoleColor.Black;
                    textcolor = ConsoleColor.Red;
                    highlight = ConsoleColor.Green;
                    highlightText = ConsoleColor.Black;
                    Console.BackgroundColor = background;
                    Console.ForegroundColor = textcolor;
                    Settings(4);
                    break;
                case 3:
                    background = ConsoleColor.DarkBlue;
                    textcolor = ConsoleColor.White;
                    highlight = ConsoleColor.Blue;
                    highlightText = ConsoleColor.White;
                    Console.BackgroundColor = background;
                    Console.ForegroundColor = textcolor;
                    Settings(4);
                    break;
                case 4:
                    Main();
                    break;
            }
        }

        /// <summary>
        /// Megnézi hogy léteznek-e a kellő fájlok, létrehozza ha nem.
        /// </summary>
        static void FilesExist()
        {
            if (!File.Exists("Runs.json") || new FileInfo("Runs.json").Length == 0)
            {
                File.WriteAllText("Runs.json", "[]");
            }
            if (!File.Exists("User.json"))
            {
                File.Create("User.json").Close();
            }
        }
    }
}
