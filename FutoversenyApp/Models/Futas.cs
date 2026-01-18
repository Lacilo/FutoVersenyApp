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

        #region Konstruktorok
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
        #endregion

        #region Property
        public DateTime Datum 
        { 
            get 
            { 
                return datum; 
            } 

            set 
            { 
                if (value <= DateTime.Now) 
                    datum = value; 

                else datum = DateTime.Now ; 
            } 
        }
        public int Tavolsag 
        { 
            get 
            { 
                return tavolsag; 
            } 

            set { 
                if (value > 0)
                    tavolsag = value; 
            } 
        }
        public string Idotartam 
        { 
            get => idotartam; 
            set => idotartam = value; 
        }
        public int Tomeg 
        { 
            get; 
            set; 
        }
        public int Nyugpul 
        { 
            get; 
            set;
        }
        public int Maxpulzus 
        { 
            get 
            { 
                return maxpulzus; 
            } 

            set 
            { 
                if (value > Nyugpul) 
                    maxpulzus = value; 
                else 
                    maxpulzus = Nyugpul; 
            } 
        }
        #endregion

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
        /// Kiszámolja az átlagsebességet km/h-ban a a távolság és az időtartam alapján
        /// </summary>
        /// <returns>A futás objektum átlagsebességét km/h-ban</returns>
        public float AtlagSebesseg()
        {
            // óó:pp:mm, return km/h érték, / 3.6 ha m/s
            string[] ido = this.Idotartam.Split(':');
            int timeInSeconds = (int.Parse(ido[0]) * 60 * 60) + (int.Parse(ido[1]) * 60) + int.Parse(ido[2]);
            float atlagsebesseg = ((float)this.Tavolsag / (float)timeInSeconds) * 3.6f;
            return (float)Math.Round(atlagsebesseg,1); // Kerekítsd hogy ne menjen örökké a kiírás
        }

        /// <summary>
        /// Megnézi, hogy a felhasználó elérte-e a célidejét
        /// </summary>
        /// <returns>True, ha a futás átlagsebessége nagyobb vagy egyenlő, mint a cél-é; False, ha kisebb</returns>
        public bool CelElerve()
        {
            User user = User.UserJsonReader();
            float celAtlag = 5000f / ((float)user.Celido * 60f) * 3.6f; // méter/másodperc, mivel Celido = perc, majd később vissza km/h-ba
            return this.AtlagSebesseg() >= Math.Round(celAtlag,1);
        }

        /// <summary>
        /// Megszámolja hányszor érte el a felhasználó a célját
        /// </summary>
        /// <param name="futasok">A lista ami tárolja a futásokat</param>
        /// <returns>A sikerek elérésének számát</returns>
        public static int SikerSzamlalo(List<Futas> futasok)
        {
            int siker = 0;
            foreach (Futas futas in futasok)
            {
                if (futas.CelElerve())
                {
                    siker++;
                }
            }
            return siker;
        }

        /// <summary>
        /// Kiszámolja, hogy összesen mennyi időt töltött a felhasználó futással
        /// </summary>
        /// <param name="futasok">A lista ami tárolja a futásokat</param>
        /// <returns>Egy "nn:óó:pp:mm" formátumú string ami tartalmazza az összidőt</returns>
        public static string Osszido(List<Futas> futasok)
        {
            int osszMp = 0;

            foreach (Futas futas in futasok)
            {
                int ora = int.Parse(futas.Idotartam.Split(':')[0]);
                int perc = int.Parse(futas.Idotartam.Split(':')[1]);
                int mp = int.Parse(futas.Idotartam.Split(':')[2]);

                osszMp += (ora * 3600) + (perc * 60) + mp;
            }

            // a %= b || a = a % b; úgy látszik ilyen is van, a kettő ugyanaz
            int napok = osszMp / 86400;     // Napok kiszámolása, egésszé lekerekítve
            osszMp %= 86400;                // Egyenlővé tesszük a maradékkal
            int maradekOra = osszMp / 3600; // Órák kiszámálosa, egésszé lekerekítve
            osszMp %= 3600;                 // Egyenlővé tesszük a maradékkal
            int maradekPerc = osszMp / 60;  // Percek kiszámálosa, egésszé lekerekítve
            int maradekMp = osszMp % 60;    // Egyenlővé tesszük a maradékkal

            return $"{napok:D2}:{maradekOra:D2}:{maradekPerc:D2}:{maradekMp:D2}";
        }
    }
}
