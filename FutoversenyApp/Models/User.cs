using System;
using System.IO;
using System.Text.Json;

namespace FutoversenyApp.Models
{
    internal class User
    {
        private int magassag;
        private int tomeg;
        private static int nyugpul;
        private int celido;
        private DateTime szuldat;

        public User(string magassag, string tomeg, string nyugpul, string celido, string szuldat)
        {
            Magassag = int.Parse(magassag);
            Tomeg = int.Parse(tomeg);
            Nyugpul = int.Parse(nyugpul);
            Celido = int.Parse(celido);
            Szuldat = DateTime.Parse(szuldat);
        }

        public User(string[] user)
        {
            Magassag = int.Parse(user[0]);
            Tomeg = int.Parse(user[1]);
            Nyugpul = int.Parse(user[2]);
            Celido = int.Parse(user[3]);
            Szuldat = DateTime.Parse(user[4]);
        }

        public User()
        {
            
        }

        public int Magassag { get { return magassag; } set { if (value > 0) magassag = value; } }
        public int Tomeg { get { return tomeg; } set { if (value > 0) tomeg = value; } }
        public static int Nyugpul { get { return nyugpul; } set { if (value > 0) nyugpul = value; } }
        public int Celido { get { return celido; } set { if (value > 0) celido = value; } }
        public DateTime Szuldat { get { return szuldat; } set { if (value < DateTime.Now) szuldat = value; } }

        public override string ToString()
        {
            return $"Magasság: {Magassag} cm, Testtömeg: {Tomeg} kg, Nyugalmi pulzus: {Nyugpul}, Cél lefutására való idő: {Celido} perc";
        }

        /// <summary>
        /// Beolvassa a felhasználó .json fájlját
        /// </summary>
        /// <param name="filename">A fájl amit beolvas</param>
        /// <returns>Egy User objektumot a fájlból</returns>
        public static User UserJsonReader(string filename)
        {
            string json = File.ReadAllText(filename);
            User user = JsonSerializer.Deserialize<User>(json);
            return user;
        }

        /// <summary>
        /// Kiír egy .json fájlt ami tartalmazza a felhasználó személyes adatait
        /// </summary>
        /// <param name="user">Egy tömb ami tartalmazza felhasználó adatait</param>
        public static void JsonWriter(User user)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
            };
            string json = JsonSerializer.Serialize(user, options);
            File.WriteAllText("User.json", json);
        }
    }
}
