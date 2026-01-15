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
            Datum = DateTime.Parse(datum);
            Tavolsag = int.Parse(tavolsag);
            Idotartam = idotartam;
            Maxpulzus = int.Parse(maxpulzus);
        }

        public Futas(string[] futas)
        {
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
        public int Maxpulzus { get { return maxpulzus; } set { if (value > User.Nyugpul) maxpulzus = value; else User.Nyugpul = value; } }

        public override string ToString()
        {
            return $"Dátum: {Datum}, Távolság: {Tavolsag} m, Időtartam: {Idotartam} perc, Maximális pulzus: {Maxpulzus}";
        }

        /// <summary>
        /// Beolvassa a futások .json fájlját
        /// </summary>
        /// <param name="filename">A fájl amit beolvas</param>
        /// <returns>Egy listát ami a futásokból készített objektumokat tartalmaz</returns>
        public static List<Futas> RunsJsonReader(string filename)
        {
            string json = File.ReadAllText(filename);
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
    }
}
