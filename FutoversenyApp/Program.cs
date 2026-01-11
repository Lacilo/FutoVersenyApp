using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutoversenyApp.Models;
using System.IO;
using System.Text.Json;

namespace FutoversenyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
