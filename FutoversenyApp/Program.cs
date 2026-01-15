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
        public static List<Futas> futasok = new List<Futas>();
        public static Display display = new Display();
        public static void Main() 
        {
            FilesExist();

            //for (int i = 0; i < 50; i++)
            //{
            //    futasok.Add(new Futas());
            //}
            futasok = Futas.RunsJsonReader("Runs.json");

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

            string megadvat = "";
            if (megadva)
            {
                megadvat = "(megadava)";
            }

            CenterEngine.Show(
                "================= Futó App =================",
                $"1: Személes Adatok Megadása {megadvat}",
                "2: Edzés Rögzítése",
                "3: Edzések Megjelenítése",
                "4: Szerkestés",
                "5: Törlés",
                "6: Kilépés"
            );

            string ans = CenterEngine.ReadCentered("");

            switch (ans)
            {
                case "1":
                    Controller.SzAdatok();
                    break;
                case "2":
                    Controller.Edzes(futasok);
                    break;
                case "3":
                    display.UpdateFutasok(futasok);
                    display.DisplayFutasok(0);
                    display.GetDisplayInput();
                    break;
                case "4":
                    Controller.Szerkesztes(futasok);
                    break;
                case "5":
                    Controller.Torles(futasok);
                    break;
                case "6":
                    Exit();
                    break;
                default:
                    CenterEngine.Show("Érvénytelen választás!");
                    break;
            }

            Console.ReadLine();
            
        }

        static void Exit()
        {
            CenterEngine.Show(
                "================= Futó App =================",
                "1: Igen",
                "2: Nem"
            );
            string ans = CenterEngine.ReadCentered("");

            switch (ans)
            {
                case "1":
                    Environment.Exit(0);
                    break;
                case "2":
                    Main();
                    break;
            }
        }

        /// <summary>
        /// Megnézi hogy léteznek-e a kellő fájlok, létrehozza ha nem.
        /// </summary>
        static void FilesExist()
        {
            if (!File.Exists("Runs.json"))
            {
                File.Create("Runs.json").Close();
            }
            if (!File.Exists("User.json"))
            {
                File.Create("User.json").Close();
            }
        }
    }
}
