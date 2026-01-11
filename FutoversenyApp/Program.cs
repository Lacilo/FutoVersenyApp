using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutoversenyApp.Models;
using System.IO;
using System.Text.Json;
using menu.Models;

namespace FutoversenyApp
{
    internal class Program
    {
        public static void Main()
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
                "3: Szerkestés",
                "4: Törlés",
                "5: Kilépés"
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
                    Szerkesztes();
                    break;
                case "4":
                    Torles();
                    break;
                case "5":
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
            List<Futas> futasok = Futas.RunsJsonReader("futasok.json");

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
            futasok.Add(ujFutas);
            Futas.JsonWriter(futasok);
        }

        static void Szerkesztes()
        { }

        static void Torles()
        { }

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
