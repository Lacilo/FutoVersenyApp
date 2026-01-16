using System;
using System.Collections.Generic;
using System.IO;
using FutoversenyApp.Models;
using menu.Models;
using FutoversenyApp;

namespace FutoversenyApp.Controllers
{
    internal class Controller
    {
        public static void SzAdatok()
        {
            Console.BackgroundColor = Program.background;
            Console.Clear();
            Console.BackgroundColor = Program.background;
            Console.ForegroundColor = Program.textcolor;


            string magassag = CenterEngine.ReadCentered("Magasság (cm): ");
            string tomeg = CenterEngine.ReadCentered("Tömeg (kg): ");
            string nyugpul = CenterEngine.ReadCentered("Nyugalmi Pulzus: ");
            string celido = CenterEngine.ReadCentered("Célidő (perc): ");
            string szuldat = CenterEngine.ReadCentered("Születési Dátum (ÉÉÉÉ.HH.NN): ");
            if (szuldat == "")
            {
                szuldat = DateTime.Now.ToString();
            }

            User ujUser = new User(magassag, tomeg, nyugpul, celido, szuldat);
            User.JsonWriter(ujUser);

            Program.Main();
        }

        public static void SzAdatokSzerk()
        {
            Console.BackgroundColor = Program.background;
            Console.Clear();
            Console.BackgroundColor = Program.background;
            Console.ForegroundColor = Program.textcolor;

            User user = User.UserJsonReader("User.json");

            string magassag = CenterEngine.ReadCentered($"Magasság ({user.Magassag}cm): ");
            if (magassag == "")
            {
                magassag = user.Magassag.ToString();
            }
            string tomeg = CenterEngine.ReadCentered($"Tömeg ({user.Tomeg}kg): ");
            if (tomeg == "")
            {
                tomeg = user.Tomeg.ToString();
            }
            string nyugpul = CenterEngine.ReadCentered($"Nyugalmi Pulzus ({user.Nyugpul}): ");
            if (nyugpul == "")
            {
                nyugpul = user.Nyugpul.ToString();
            }
            string celido = CenterEngine.ReadCentered($"Célidő ({user.Celido}perc): ");
            if (celido == "")
            {
                celido = user.Celido.ToString();
            }
            string szuldat = CenterEngine.ReadCentered($"Születési Dátum ({user.Szuldat}): ");
            if (szuldat == "")
            {
                szuldat = user.Szuldat.ToString();
            }
            User ujUser = new User(magassag, tomeg, nyugpul, celido, szuldat);
            User.JsonWriter(ujUser);

            Program.Main();
        }

        public static void Edzes(List<Futas> futasok)
        {
            Console.BackgroundColor = Program.background;
            Console.Clear();
            Console.BackgroundColor = Program.background;
            Console.ForegroundColor = Program.textcolor;

            string datum = CenterEngine.ReadCentered("Dátum: ");
            if (datum == "")
            {
                datum = DateTime.Now.ToString();
            }
            string tavolsag = CenterEngine.ReadCentered("Távolság (m): ");
            string idotartam = CenterEngine.ReadCentered("Időtartam (perc): ");
            string maxpulzus = CenterEngine.ReadCentered("Maximális Pulzus: ");

            Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            futasok.Add(ujFutas);
            Futas.JsonWriter(futasok);

            Program.Main();
        }

        /// <summary>
        /// Újra bekéri az edzés adatait, majd új objektumot hoz létre a régi edzés helyén
        /// </summary>
        public static void Szerkesztes(List<Futas> futasok, int kivalasztott)
        {
            string datum = CenterEngine.ReadCentered("Dátum: ");
            if (datum == "")
            {
                datum = futasok[kivalasztott].Datum.ToString();
            }

            string tavolsag = CenterEngine.ReadCentered("Távolság (m): ");
            if (tavolsag == "")
            {
                tavolsag = futasok[kivalasztott].Tavolsag.ToString();
            }

            string idotartam = CenterEngine.ReadCentered("Időtartam (perc): ");
            if (idotartam == "")
            {
                idotartam = futasok[kivalasztott].Idotartam;
            }

            string maxpulzus = CenterEngine.ReadCentered("Maximális Pulzus: ");
            if (maxpulzus == "")
            {
                maxpulzus = futasok[kivalasztott].Maxpulzus.ToString();
            }

            Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            futasok[kivalasztott] = ujFutas;
            Futas.JsonWriter(futasok);
        }

        /// <summary>
        /// Kitörli a kiválasztott edzést
        /// </summary>
        public static void Torles(List<Futas> futasok, int kivalasztott)
        {
            futasok.RemoveAt(kivalasztott);
            Futas.JsonWriter(futasok);
        }
    }
}
