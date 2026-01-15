using System;
using System.Collections.Generic;
using FutoversenyApp.Models;
using System.IO;
using menu.Models;

namespace FutoversenyApp
{
    internal class Program
    {
        public static List<Futas> futasok = new List<Futas>();
        public static void Main()
        {
            Menu();
        }

        static void Menu()
        {
            // ha létezik a User.json fájl, akkor a menüben jelezze, hogy meg van adva a személyes adat
            bool megadva = false;
            if (File.Exists("User.json"))
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
                    SzAdatok();
                    break;
                case "2":
                    Edzes();
                    break;
                case "3":
                    // Edzések Megjelenítése function
                    break;
                case "4":
                    Szerkesztes();
                    break;
                case "5":
                    Torles();
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
        static void SzAdatok()
        {
            if (File.Exists("User.json"))
            {
                CenterEngine.Show("Már meg vannak adva a személyes adatok!");
                CenterEngine.ReadCentered("");
                Main();
            }

            User user = User.UserJsonReader("User.json");
            string magassag = CenterEngine.ReadCentered("Magasság: ");
            string tomeg = CenterEngine.ReadCentered("Tömeg: ");
            string nyugpul = CenterEngine.ReadCentered("Nyugalmi Pulzus: ");
            string celido = CenterEngine.ReadCentered("Célidő (perc): ");
            string szuldat = CenterEngine.ReadCentered("Születési Dátum (ÉÉÉÉ.HH.NN): ");

            User ujUser = new User(magassag, tomeg, nyugpul, celido, szuldat);
            User.JsonWriter(ujUser);
        }

        static void Edzes()
        {
            List<Futas> futasok = new List<Futas>();

            if (new FileInfo("Runs.json").Length > 0)
            {
                futasok = Futas.RunsJsonReader("Runs.json");
            }

            string input = CenterEngine.ReadCentered("Dátum: ");
            DateTime datum;
            if (input == "")
            {
                datum = DateTime.Now;
            }
            else
            {
                datum = DateTime.Parse(input);
            }

            int tavolsag = int.Parse(CenterEngine.ReadCentered("Távolság: "));
            string idotartam = CenterEngine.ReadCentered("Időtartam (perc): ");
            int maxpulzus = int.Parse(CenterEngine.ReadCentered("Maximális Pulzus: "));

            Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            futasok.Add(ujFutas);
            Futas.JsonWriter(futasok);
        }

        static void Szerkesztes()
        {
            List<Futas> futasok = new List<Futas>();

            if (new FileInfo("Runs.json").Length > 0)
            {
                futasok = Futas.RunsJsonReader("Runs.json");
            }

            for (int i = 0; i < futasok.Count; i++)
            {
                Console.WriteLine(futasok[i]);
            }
            int kivalasztott = int.Parse(CenterEngine.ReadCentered("Index: "));

            string input = CenterEngine.ReadCentered("Dátum: ");
            DateTime datum;
            if (input == null)
            {
                datum = DateTime.Now;
            }
            else
            {
                datum = DateTime.Parse(input);
            }

            int tavolsag = int.Parse(CenterEngine.ReadCentered("Távolság: "));
            string idotartam = CenterEngine.ReadCentered("Időtartam (perc): ");
            int maxpulzus = int.Parse(CenterEngine.ReadCentered("Maximális Pulzus: "));

            Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            futasok[kivalasztott] = ujFutas;
            Futas.JsonWriter(futasok);
        }

        static void Torles()
        {
            List<Futas> futasok = new List<Futas>();

            if (new FileInfo("Runs.json").Length > 0)
            {
                futasok = Futas.RunsJsonReader("Runs.json");
            }
            for (int i = 0; i < futasok.Count; i++)
            {
                Console.WriteLine(futasok[i]);
            }
            int kivalasztott = int.Parse(CenterEngine.ReadCentered("Index: "));

            futasok.RemoveAt(kivalasztott);
            Futas.JsonWriter(futasok);
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
    }
}
