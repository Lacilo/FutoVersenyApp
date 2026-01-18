using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FutoversenyApp.Models
{
    internal class Futas
    {
        private DateTime datum;
        private int tavolsag;
        private string idotartam;
        private int maxpulzus;

        public Futas(string datum, string tavolsag, string idotartam, string maxpulzus)
        {
            User user = User.UserJsonReader();
            this.Tomeg = user.Tomeg;
            this.Nyugpul = user.Nyugpul;
            Datum = DateTime.Parse(datum);
            Tavolsag = int.Parse(tavolsag);
            Idotartam = idotartam;
            Maxpulzus = int.Parse(maxpulzus);
        }

        public Futas(string[] futas)
        {
            User user = User.UserJsonReader();
            this.Tomeg = user.Tomeg;
            this.Nyugpul = user.Nyugpul;
            Datum = DateTime.Parse(futas[0]);
            Tavolsag = int.Parse(futas[1]);
            Idotartam = futas[2];
            Maxpulzus = int.Parse(futas[3]);
        }

        public Futas()
        {

        }

        public DateTime Datum { get { return datum; } set { if (value <= DateTime.Now) datum = value; } }
        public int Tavolsag { get { return tavolsag; } set { if (value > 0) tavolsag = value; } }
        public string Idotartam { get => idotartam; set => idotartam = value; } // Ezzel még nem tudom mi lesz, marad így
        public int Tomeg { get; set; }
        public int Nyugpul { get; set; }
        public int Maxpulzus { get { return maxpulzus; } set { if (value > Nyugpul) maxpulzus = value; else maxpulzus = Nyugpul; } }


        public override string ToString()
        {
            return $"Dátum: {Datum}, Távolság: {Tavolsag} m, Időtartam: {Idotartam} perc, Maximális pulzus: {Maxpulzus}";
        }

        /// <summary>
        /// Beolvassa a futások .json fájlját
        /// </summary>
        /// <returns>Egy listát ami a futásokból készített Futás objektumokat tartalmaz</returns>
        public static List<Futas> RunsJsonReader()
        {
            string json = File.ReadAllText("Runs.json");
            List<Futas> futasok = JsonSerializer.Deserialize<List<Futas>>(json);
            return futasok;
        }

        /// <summary>
        /// Kiír egy .json fájlt ami tartalmazza a felhasználó futásait
        /// </summary>
        /// <param name="futas">Egy lista ami tartalmazza felhasználó futásait</param>
        public static void JsonWriter(List<Futas> futas)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };
            string json = JsonSerializer.Serialize(futas, options);
            File.WriteAllText("Runs.json", json);
        }

        /// <summary>
        /// Kiszámolja az átlagsebességet km/h-bana a távolság és az időtartam alapján
        /// </summary>
        /// <returns>A futás objektum átlagsebességét km/h-ban</returns>
        public float AtlagSebesseg()
        {
            // óó:pp:mm, return km/h érték, / 3.6 ha m/s
            string[] ido = this.Idotartam.Split(':');
            int timeInSeconds = (int.Parse(ido[0]) * 60 * 60) + (int.Parse(ido[1]) * 60) + int.Parse(ido[2]);
            float atlagsebesseg = (float)this.Tavolsag / (float)timeInSeconds;
            return (float)(Math.Round(atlagsebesseg,1) * 3.6);
        }
    }
}
