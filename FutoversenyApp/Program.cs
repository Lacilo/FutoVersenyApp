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

        /// <summary>
        /// Main
        /// </summary>
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

        #region Menük
        /// <summary>
        /// A program főmenüje
        /// </summary>
        public static void Menu()
        {
            // ha létezik a User.json fájl, akkor a menüben jelezze, hogy meg van adva a személyes adat
            bool megadva = false;
            if (new FileInfo("User.json").Length > 2)
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
                // Menü megrajzolása
                MenuDrawer("================= Futó App =================", items, selected);

                // Lenyomott billentyű olvasása
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Tab || key == ConsoleKey.DownArrow)
                {
                    // Le gomb vagy Tab -> lentebbi menüpont
                    selected = (selected + 1) % items.Length;
                    Console.BackgroundColor = background;
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    // Fel gomb -> fentebbi menüpont
                    selected = (selected - 1 + items.Length) % items.Length;
                    Console.BackgroundColor = background;
                }
                else if (key == ConsoleKey.Enter)
                {
                    // Enter -> menüpont kiválasztása, while ciklus break-elése, switch-case következik a kiválasztott menüpont száma alapján
                    break;
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
                    Console.BackgroundColor = Program.background;
                    Console.Clear();
                    Console.BackgroundColor = Program.background;
                    Console.ForegroundColor = Program.textcolor;
                    CenterEngine.CenterLine("Megtett össztáv: " + Controller.MegtettOssztav(futasok) + " m");
                    CenterEngine.CenterLine("Sikerek száma: " + Futas.SikerSzamlalo(futasok) + " Ennyiből: " + futasok.Count);
                    CenterEngine.CenterLine("Összesített futással töltött idő: " + Futas.Osszido(futasok));
                    CenterEngine.CenterLine("Enterrel vissza a főmenübe");
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

        /// <summary>
        /// Külön menü a kilépés megerősítésére
        /// </summary>
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

        /// <summary>
        /// Témák váltása
        /// </summary>
        /// <param name="selected">A menübe belépéskor alapártelmezetten kiválasztott menüpont indexe</param>
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
                "Kék-Fehér",
                "Főmenü"
            };

            while (true)
            {
                MenuDrawer("================= Témák: =================", items, selected);

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
        #endregion

        /// <summary>
        /// Középre igazított menüt jelenít meg a konzolon a megadott címmel és menüpontokkal, kiemelve a kiválasztott elemet.
        /// </summary>
        /// <param name="title">A konzol tetejére kiirt szöveg. Nem lehet <see langword="null"/>.</param>
        /// <param name="items">Egy Menüpontokat tartalmazó tömb. Nem lehet <see langword="null"/> or empty.</param>
        /// <param name="selected">A kiválasztott menüpont indexe, ami ki lesz emelve. Érvényes indexnek kell lennie az <paramref name="items"/> tömbön belül.</param>
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

        /// <summary>
        /// Megnézi hogy léteznek-e a kellő fájlok, létrehozza ha nem.
        /// </summary>
        public static void FilesExist()
        {
            if (!File.Exists("Runs.json") || new FileInfo("Runs.json").Length == 0)
            {
                File.WriteAllText("Runs.json", "[]");
            }
            if (!File.Exists("User.json") || new FileInfo("User.json").Length == 0)
            {
                File.WriteAllText("User.json", "[]");
            }
        }
    }
}
