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
        public static ConsoleColor background = ConsoleColor.Cyan;
        public static ConsoleColor textcolor = ConsoleColor.Black;
        public static ConsoleColor highlight = ConsoleColor.Red;
        public static ConsoleColor highlightText = ConsoleColor.White;

        public static List<Futas> futasok = new List<Futas>();
        public static Display display = new Display();
        public static void Main()
        {
            Console.BackgroundColor = background;
            FilesExist();

            //for (int i = 0; i < 50; i++)
            //{
            //    futasok.Add(new Futas());
            //}
            futasok = Futas.RunsJsonReader("Runs.json");

            Menu();
        }

        //static void Menu()
        //{
        //    // ha létezik a User.json fájl, akkor a menüben jelezze, hogy meg van adva a személyes adat
        //    bool megadva = false;
        //    if (File.Exists("User.json"))
        //    {
        //        megadva = true;
        //    }

        //    string megadvat = "";
        //    if (megadva)
        //    {
        //        megadvat = "(megadava)";
        //    }

        //    CenterEngine.Show(
        //        "================= Futó App =================",
        //        $"1: Személes Adatok Megadása {megadvat}",
        //        "2: Edzés Rögzítése",
        //        "3: Edzések Megjelenítése",
        //        "4: Szerkestés",
        //        "5: Törlés",
        //        "6: Kilépés"
        //    );

        //    string ans = CenterEngine.ReadCentered("");

        //    switch (ans)
        //    {
        //        case "1":
        //            Controller.SzAdatok();
        //            break;
        //        case "2":
        //            Controller.Edzes();
        //            break;
        //        case "3":
        //            // Edzések Megjelenítése function
        //            break;
        //        case "4":
        //            Controller.Szerkesztes();
        //            break;
        //        case "5":
        //            Controller.Torles();
        //            break;
        //        case "6":
        //            Exit();
        //            break;
        //        default:
        //            CenterEngine.Show("Érvénytelen választás!");
        //            break;
        //    }

        //    Console.ReadLine();

        //}

        public static void Menu()
        {
            // ha létezik a User.json fájl, akkor a menüben jelezze, hogy meg van adva a személyes adat
            bool megadva = false;
            if (new FileInfo("User.json").Length > 0)
            {
                megadva = true;
            }

            string megadvat = "";
            if (megadva)
            {
                megadvat = "(megadava)";
            }

            string[] items =
            {
                $"Személyes Adatok Megadása {megadvat}",
                "Edzés Rögzítése",
                "Edzések Megjelenítése",
                "Szerkesztés",
                "Törlés",
                "Kilépés"
            };

            int selected = 0;

            while (true)
            {
                DrawMenu(items, selected);

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
                    Controller.SzAdatok();
                    break;
                case 1:
                    Controller.Edzes(futasok);
                    break;
                case 2:
                    display.UpdateFutasok(futasok);
                    display.DisplayFutasok(0);
                    display.GetDisplayInput();
                    break;
                case 3:
                    Controller.Szerkesztes(futasok, 0);
                    break;
                case 4:
                    Controller.Torles(futasok, 0);
                    break;
                case 5:
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
                DrawMenu(items, selected);

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

        //static void DrawMenu(string[] items, int selected)
        //{
        //    Console.Clear();

        //    Console.BackgroundColor = background;
        //    Console.ForegroundColor = textcolor;

        //    CenterEngine.CenterLine("================= Futó App =================");

        //    Console.WriteLine();

        //    for (int i = 0; i < items.Length; i++)
        //    {
        //        if (i == selected)
        //            Console.ForegroundColor = highlight;
        //        else
        //            Console.ForegroundColor = textcolor;

        //        CenterEngine.CenterLine($"{i + 1}: {items[i]}");
        //    }

        //    Console.ResetColor();
        //}

        static void DrawMenu(string[] items, int selected)
        {
            Console.Clear();

            Console.BackgroundColor = background;
            Console.ForegroundColor = textcolor;
            CenterEngine.CenterLine("================= Futó App =================");
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
