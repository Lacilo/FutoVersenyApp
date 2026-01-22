using FutoversenyApp.Controllers;
using menu.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;


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
        bool dispAsc = true;

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
            bool exit = false;

            int page = 0;
            int allPage = (int)(Math.Ceiling(Futasok.Count / 10.0f));

            DisplayFutasok(page * 10);
            DisplayDataOfSelectedRun();
            Console.SetCursorPosition(0, 10);
            Console.BackgroundColor = Program.highlight;
            Console.ForegroundColor = Program.highlightText;
            Console.WriteLine($"\n   Oldal: {page + 1} / {allPage} | Jelenlegi elem: {currentDisplay + 1}   ");
            Console.BackgroundColor = Program.background;
            Console.ForegroundColor = Program.textcolor;
            Console.WriteLine("Szerkesztés: E\t Törlés: Delete\nLétrehozás: C\t Szortírozás beállítások: S");
            DisplaySeparator();

            ConsoleKeyInfo key = Console.ReadKey();

            while (true)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        posOnPage++;
                        if (posOnPage > 9)
                        {
                            posOnPage = 9;
                        }
                        if ((allPage) / (page + 1) == 1 && Futasok.Count - page * 10 <= posOnPage)
                        {
                            posOnPage--;
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
                        posOnPage = 0;
                        Controller.Torles(Futasok, currentDisplay);
                        break;

                    case ConsoleKey.E:
                        Controller.Szerkesztes(Futasok, currentDisplay);
                        break;

                    case ConsoleKey.C:
                        Controller.Edzes(futasok);
                        break;

                    case ConsoleKey.S:
                        SortMenu();
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
                Console.WriteLine("Szerkesztés: e\t Törlés: Delete\nLétrehozás: c\t Szortírozás beállítások: s");
                DisplaySeparator();
                // Console.WriteLine($"{page}\n{posOnPage}\nettől: {page*10}\njelenlegi: {currentDisplay}");
                key = Console.ReadKey();
            }
        }

        public void DisplayAscEnableMenu()
        {
            dispAsc = !dispAsc;

            Console.SetCursorPosition(95, 15);

            if (!dispAsc)
            {
                Console.WriteLine("Csökkenő sorrend");
            }
            else
            {
                Console.WriteLine("Növekvő sorrend ");
            }
        }

        public void DisplaySortMenu(int cursor)
        {
            Console.SetCursorPosition(0, 15);
            Console.Write(' ');

            Console.SetCursorPosition(80, 14);
            Console.WriteLine("=== Szortírozás beállítások ===");

            string[] menuPontok = { "Dátum", "Távolság", "Időtartam", "Maximum BPM" };

            for (int i = 0; i < menuPontok.Length; i++)
            {
                Console.SetCursorPosition(81, 15 + i);

                if (cursor == i)
                {
                    Console.BackgroundColor = Program.highlight;
                    Console.ForegroundColor = Program.highlightText;
                }

                Console.WriteLine("- " + menuPontok[i]);

                Console.BackgroundColor = Program.background;
                Console.ForegroundColor = Program.textcolor;
            }
        }

        public void SortMenu()
        {
            int sortMenuCursor = 0;

            char[] modes = { 'd', 't', 'i', 'm' };

            DisplaySortMenu(sortMenuCursor);
            DisplayAscEnableMenu();

            ConsoleKeyInfo key = Console.ReadKey();

            while (true)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        sortMenuCursor++;
                        //Console.WriteLine("le");
                        if (sortMenuCursor > (modes.Length - 1))
                        {
                            sortMenuCursor = (modes.Length - 1);
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        sortMenuCursor--;
                        //Console.WriteLine("fel");
                        if (sortMenuCursor < 0)
                        {
                            sortMenuCursor = 0;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        DisplayAscEnableMenu();
                        break;

                    case ConsoleKey.Escape:
                        GetDisplayInput();
                        break;

                    case ConsoleKey.Enter:
                        UpdateFutasok(Controller.Sort(Futasok, modes[sortMenuCursor], dispAsc));
                        //Console.WriteLine($"Szortíroztam ezzel: {modes[sortMenuCursor]}, {dispAsc}");
                        //Console.ReadKey();
                        DisplayFutasok(0);
                        GetDisplayInput();
                        break;
                }

                DisplaySortMenu(sortMenuCursor);
                //Console.WriteLine(sortMenuCursor);
                key = Console.ReadKey();
            }
        }

        public void DisplayFutasok(int fromThisPos)
        {
            Console.BackgroundColor = Program.background;
            Console.Clear();

            int until = 0;

            until = Math.Min(10, Futasok.Count - fromThisPos);

            if (Futasok.Count() != 0)
            {
                for (int i = fromThisPos; i < fromThisPos + until; i++)
                {
                    if (currentDisplay == i)
                    {
                        Console.ForegroundColor = Program.highlightText;
                        Console.BackgroundColor = Program.highlight;
                        Console.Write("--> ");
                    }
                    else
                    {
                        Console.ForegroundColor = Program.textcolor;
                        Console.BackgroundColor = Program.background;
                        Console.Write("    ");
                    }

                    Console.WriteLine($"{Futasok[i].Datum}    {Futasok[i].Tavolsag} méter    {Futasok[i].Idotartam} óra    {Futasok[i].Maxpulzus} bpm ");
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

            Console.SetCursorPosition(80, 3);
            Console.Write($"Futás Dátuma: {Futasok[currentDisplay].Datum}");

            Console.SetCursorPosition(80, 4);
            Console.Write($"Lefutott távolság: {Futasok[currentDisplay].Tavolsag} méter");

            Console.SetCursorPosition(80, 5);
            Console.Write($"Futás Időtartama: {Futasok[currentDisplay].Idotartam}");

            Console.SetCursorPosition(80, 6);
            Console.Write($"Legmagasabb pulzus érték: {Futasok[currentDisplay].Maxpulzus}");

            Console.SetCursorPosition(80, 7);
            Console.WriteLine($"Átlagsebesség: {Futasok[currentDisplay].AtlagSebesseg()} km/h");

            Console.SetCursorPosition(80, 8);
            Console.WriteLine($"Elégetett kalóriák: {Futasok[currentDisplay].ElegetettKaloria()} kcal");
        }

        public void DisplaySeparator()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.SetCursorPosition(75, i);
                Console.Write("||");
            }
        }

        public void DisplayWABPM(User user, int fromThisPos, int cursor, int until)
        {
            Console.Clear();

            List<string[]> userData = user.szemelyHistory;

            for (int i = fromThisPos; i < until; i++)
            {
                if (cursor == i - fromThisPos)
                {
                    Console.BackgroundColor = Program.highlight;
                    Console.ForegroundColor = Program.highlightText;
                }

                CenterEngine.CenterLine($"{userData[i][0]} - {userData[i][1]}kg    {userData[i][2]}BPM");

                Console.ForegroundColor = Program.textcolor;
                Console.BackgroundColor = Program.background;
            }

            //Console.WriteLine($"{cursor}\n{fromThisPos} -> {until}\n{user.szemelyHistory.Count - until}");
        }

        public void DisplayWeightAndBPMChangeMenu(User user)
        {
            if (new FileInfo("Runs.json").Length == 2)
            {
                Console.Clear();
                CenterEngine.CenterLine("Nincs adat");
                CenterEngine.CenterLine("Enterrel tovább");
                Console.ReadKey();
                Program.Main();
            }

            user = User.UserJsonReader();

            int wabpmCursor = 0;
            int start = 0;
            int until = start + 10;
            int hossz = user.szemelyHistory.Count;

            DisplayWABPM(user, start, wabpmCursor, Math.Min(until, hossz));

            if (hossz - until > 0)
            {
                Console.BackgroundColor = Program.highlight;
                Console.ForegroundColor = Program.highlightText;

                CenterEngine.CenterLine($"\n{hossz - until} további");

                Console.BackgroundColor = Program.background;
                Console.ForegroundColor = Program.textcolor;
            }

            ConsoleKeyInfo key = Console.ReadKey();

            while (true)
            {
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        wabpmCursor++;
                        //Console.WriteLine("le");


                        if (wabpmCursor > 9)
                        {
                            wabpmCursor = 9;

                            if (start + 10 <= hossz - 1)
                            {
                                start++;
                                until = start + 10;
                            }
                        }
                        else if (wabpmCursor > hossz - 1) wabpmCursor = hossz - 1;

                        break;

                    case ConsoleKey.UpArrow:
                        wabpmCursor--;
                        //Console.WriteLine("fel");
                        if (wabpmCursor < 0)
                        {
                            wabpmCursor = 0;

                            start--;
                            until = start + 10;
                        }

                        break;

                    case ConsoleKey.Escape:
                        Program.Menu();
                        break;
                }

                if (start < 0)
                {
                    start = 0;
                    until = start + 10;
                }
                if (until > hossz) until = hossz;
                DisplayWABPM(user, start, wabpmCursor, Math.Min(until, hossz));

                if (hossz - until > 0)
                {
                    Console.BackgroundColor = Program.highlight;
                    Console.ForegroundColor = Program.highlightText;

                    CenterEngine.CenterLine($"\n{hossz - (until)} további");

                    Console.BackgroundColor = Program.background;
                    Console.ForegroundColor = Program.textcolor;
                }
                //Console.WriteLine(sortMenuCursor);
                key = Console.ReadKey();
            }
        }
    }
}
