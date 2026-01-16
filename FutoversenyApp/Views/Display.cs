using System;
using System.Collections.Generic;
using System.Linq;
using FutoversenyApp.Controllers;
using FutoversenyApp;
using System.Runtime.InteropServices;


//TESZTELÉSHEZ:

//Display display = new Display();

//List<Futas> futasok = new List<Futas>();

//for (int i = 0; i < 10; i++)
//{
//    futasok.Add(new Futas());
//}

////foreach (var item in futasok)
////{
////    Console.WriteLine(item);
////}

//display.UpdateFutasok(futasok);

//display.DisplayFutasok();
//display.GetDisplayInput();

namespace FutoversenyApp.Models
{
    internal class Display
    {
        private List<Futas> futasok;
        int cursor;
        int posOnPage;
        int page;
        int currentDisplay = 0;

        internal List<Futas> Futasok { get => futasok; set => futasok = value; }
        public int Cursor { get => cursor; set => cursor = value; }

        public Display(List<Futas> futasok, int cursor, int runSelected)
        {
            Futasok = futasok;
            Cursor = cursor;
        }

        public Display()
        {

        }

        public void UpdateFutasok(List<Futas> ujFutasok)
        {
            Futasok = ujFutasok;
        }

        public void GetDisplayInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            bool exit = false;

            int page = 0;
            int allPage = (int)(Math.Ceiling(Futasok.Count / 10.0f));

            DisplayFutasok(page * 10);

            while (true)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        posOnPage++;
                        if (posOnPage > (Futasok.Count - 1))
                        {
                            posOnPage = (Futasok.Count - 1);
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        posOnPage--;
                        if (posOnPage < 0)
                        {
                            posOnPage = 0;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        page--;
                        posOnPage = 0;
                        if (page < 0)
                        {
                            page = 0;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        page++;
                        posOnPage = 0;
                        if (page > (allPage - 1))
                        {
                            page = allPage - 1;
                        }
                        break;

                    case ConsoleKey.Delete:
                        Controller.Torles(Futasok, currentDisplay);
                        break;

                    case ConsoleKey.E:
                        Controller.Szerkesztes(Futasok, currentDisplay);
                        break;

                    case ConsoleKey.Escape:
                        exit = true;
                        Program.Menu();
                        break;
                }
                currentDisplay = (page * 10) + posOnPage;

                DisplayFutasok(page * 10);
                DisplayDataOfSelectedRun();
                Console.SetCursorPosition(0, 10);
                Console.BackgroundColor = Program.highlight;
                Console.ForegroundColor = Program.highlightText;
                Console.WriteLine($"\n   Oldal: {page + 1} / {allPage} | Jelenlegi elem: {currentDisplay + 1}   ");
                Console.BackgroundColor = Program.background;
                Console.ForegroundColor = Program.textcolor;
                Console.WriteLine("Szerkesztés: e\t Törlés: Delete");
                // Console.WriteLine($"{page}\n{posOnPage}\nettől: {page*10}\njelenlegi: {currentDisplay}");
                key = Console.ReadKey();
            }
        }

        public void DisplayFutasok(int fromThisPos)
        {
            Console.BackgroundColor = Program.background;
            Console.Clear();
            int until = 0;

            until = Math.Min(10, Futasok.Count - fromThisPos);

            if(Futasok.Count() != 0)
            {
                for (int i = fromThisPos; i < fromThisPos + until; i++)
                {
                    if (currentDisplay == i)
                    {
                        Console.ForegroundColor = Program.highlightText;
                        Console.BackgroundColor = Program.highlight;
                        Console.Write("-> ");
                    }
                    else
                    {
                        Console.ForegroundColor = Program.textcolor;
                        Console.BackgroundColor = Program.background;
                        Console.Write("   ");
                    }

                    Console.WriteLine($"{Futasok[i].Datum} | {Futasok[i].Tavolsag} | {Futasok[i].Idotartam} | {Futasok[i].Maxpulzus} ");
                }
            }
            else
            {
                Console.WriteLine("Nincs még adat");
                Console.ReadKey();
                Program.Menu();
            }
        }

        public void DisplayDataOfSelectedRun()
        {
            Console.BackgroundColor = Program.background;
            Console.ForegroundColor = Program.textcolor;

            Console.SetCursorPosition(70, 3);
            Console.Write($"Futás Dátuma: {Futasok[currentDisplay].Datum}");

            Console.SetCursorPosition(70, 4);
            Console.Write($"Lefutott távolság: {Futasok[currentDisplay].Tavolsag}");

            Console.SetCursorPosition(70, 5);
            Console.Write($"Futás Időtartama: {Futasok[currentDisplay].Idotartam}");

            Console.SetCursorPosition(70, 6);
            Console.Write($"Legmagasabb pulzus érték: {Futasok[currentDisplay].Maxpulzus}");
        }
    }
}
