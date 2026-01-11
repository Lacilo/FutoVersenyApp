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
                "2: Edzés Rögzítése"
            );

            CenterEngine.ReadCentered("");

            List<Futas> futasok = Futas.RunsJsonReader("futasok.json");

            DateTime datum = Console.ReadLine() != null ? DateTime.Parse(Console.ReadLine()) : DateTime.Now;
            int tavolsag = int.Parse(Console.ReadLine());
            string idotartam = Console.ReadLine();
            int maxpulzus = int.Parse(Console.ReadLine());

            Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            futasok.Add(ujFutas);
            Futas.JsonWriter(futasok);

            Console.WriteLine("hallo");
            Console.ReadLine();
        }
    }
}
