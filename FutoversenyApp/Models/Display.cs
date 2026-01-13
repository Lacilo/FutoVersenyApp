using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FutoversenyApp.Models
{
    internal class Display
    {
        private List<Futas> futasok;        

        internal List<Futas> Futasok { get => futasok; set => futasok = value; }

        public Display(List<Futas> futasok)
        {
            Futasok = futasok;
        }

        void UpdateFutasok(List<Futas> ujFutasok)
        {
            Futasok = ujFutasok;
        }

        void DisplayFutasok()
        {
            foreach (Futas futas in Futasok)
            {
                Console.WriteLine($"{futas.Datum} | {futas.Tavolsag} | {futas.Idotartam} | {futas.Maxpulzus}");
            }
        }
    }
}
