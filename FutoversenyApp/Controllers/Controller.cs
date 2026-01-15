using System;
using System.Collections.Generic;
using System.IO;
using FutoversenyApp.Models;
using menu.Models;

namespace FutoversenyApp.Controllers
{
    internal class Controller
    {
        public static void SzAdatok()
        {
            if (new FileInfo("User.json").Length > 0)
            {
                CenterEngine.Show("Már meg vannak adva a személyes adatok!");
                CenterEngine.ReadCentered("");
                Program.Main();
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

        public static void Edzes()
        {
            List<Futas> futasok = new List<Futas>();

            if (new FileInfo("Runs.json").Length > 0)
            {
                futasok = Futas.RunsJsonReader("Runs.json");
            }

            string datum = CenterEngine.ReadCentered("Dátum: ");
            if (datum == "")
            {
                datum = DateTime.Now.ToString();
            }
            string tavolsag = CenterEngine.ReadCentered("Távolság: ");
            string idotartam = CenterEngine.ReadCentered("Időtartam (perc): ");
            string maxpulzus = CenterEngine.ReadCentered("Maximális Pulzus: ");

            Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            futasok.Add(ujFutas);
            Futas.JsonWriter(futasok);
        }

        public static void Szerkesztes()
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

            string datum = CenterEngine.ReadCentered("Dátum: ");
            if (datum == "")
            {
                datum = DateTime.Now.ToString();
            }
            string tavolsag = CenterEngine.ReadCentered("Távolság: ");
            string idotartam = CenterEngine.ReadCentered("Időtartam (perc): ");
            string maxpulzus = CenterEngine.ReadCentered("Maximális Pulzus: ");

            Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            futasok[kivalasztott] = ujFutas;
            Futas.JsonWriter(futasok);
        }

        public static void Torles()
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
    }
}
