using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FutoversenyApp.Models
{
    internal class User
    {
        private int magassag;
        private int tomeg;
        private int nyugpul;
        private int celido;
        private DateTime szuldat;
        [JsonInclude] public List<string[]> szemelyHistory;

        #region Konstruktorok
        public User(string magassag, string tomeg, string nyugpul, string celido, string szuldat)
        {
            Magassag = int.Parse(magassag);
            Tomeg = int.Parse(tomeg);
            Nyugpul = int.Parse(nyugpul);
            Celido = int.Parse(celido);
            Szuldat = DateTime.Parse(szuldat);
            InitializeSzemelyHistory();
        }

        public User(string[] user)
        {
            Magassag = int.Parse(user[0]);
            Tomeg = int.Parse(user[1]);
            Nyugpul = int.Parse(user[2]);
            Celido = int.Parse(user[3]);
            Szuldat = DateTime.Parse(user[4]);
            InitializeSzemelyHistory();
        }

        public User()
        {
            
        }
        #endregion

        #region Property
        public int Magassag 
        { 
            get 
            { 
                return magassag; 
            } 
            set 
            { 
                if (value > 0) 
                    magassag = value;
            } 
        }
        public int Tomeg 
        { 
            get 
            { 
                return tomeg; 
            } 
            set 
            { 
                if (value > 0) 
                    tomeg = value; 
            } 
        }
        public int Nyugpul 
        { 
            get
            { 
                return nyugpul; 
            } 
            set 
            { 
                if (value > 0) 
                    nyugpul = value; 
            } 
        }
        public int Celido 
        { 
            get 
            { 
                return celido; 
            } 
            set 
            { 
                if (value > 0) 
                    celido = value; 
            } 
        }
        public DateTime Szuldat 
        {
            get 
            { 
                return szuldat; 
            } 
            set 
            { 
                if (value < DateTime.Now) 
                    szuldat = value; 
            } 
        }
        #endregion

        public override string ToString()
        {
            return $"Magasság: {Magassag} cm, Testtömeg: {Tomeg} kg, Nyugalmi pulzus: {Nyugpul}, Cél lefutására való idő: {Celido} perc";
        }

        /// <summary>
        /// Beolvassa a felhasználó .json fájlját
        /// </summary>
        /// <returns>Egy User objektumot a fájlból</returns>
        public static User UserJsonReader()
        {
            string json = File.ReadAllText("User.json");
            User user = JsonSerializer.Deserialize<User>(json);
            return user;
        }

        /// <summary>
        /// Kiír egy .json fájlt ami tartalmazza a felhasználó személyes adatait
        /// </summary>
        /// <param name="user">Egy objektum ami tartalmazza felhasználó adatait</param>
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

        /// <summary>
        /// Létrehoz egy mentést a Tömegről és a Nyugalmi pulzusról, ellátja a mentés időpontjával, majd hozzáadja egy listához
        /// </summary>
        /// <remarks>
        /// Mivel a lista el van látva a [JsonInclude] taggel, így a User.json-be ezeket le is fogja menteni
        /// </remarks>
        public void InitializeSzemelyHistory()
        {
            if (szemelyHistory == null)
            {
                szemelyHistory = new List<string[]>();
            }

            string[] history = new string[3];
            history[0] = DateTime.Now.ToString();
            history[1] = Tomeg.ToString();
            history[2] = Nyugpul.ToString();

            szemelyHistory.Add(history);
        }
    }
}
