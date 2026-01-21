using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FutoversenyApp;

namespace menu.Models
{
    internal class CenterEngine
    {
        /// <summary>
        /// Több sor kiírása vízszintesen középre igazítva
        /// </summary>
        public static void Show(params string[] lines)
        {
            // Display multiple lines centered horizontally
            Console.Clear();
            foreach (var line in lines)
            {
                int x = Math.Max(0, (Console.WindowWidth - line.Length) / 2);
                Console.SetCursorPosition(x, Console.CursorTop);
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Bekérő szöveg kiírása és bekérés egyel alatta vízszintesen középre igazítva
        /// </summary>
        public static string ReadCentered(string prompt)
        {
            // Display a prompt centered horizontally, then get input centered

            // Show prompt
            int xPrompt = Math.Max(0, (Console.WindowWidth - prompt.Length) / 2);
            Console.SetCursorPosition(xPrompt, Console.CursorTop);
            Console.WriteLine(prompt);

            // Move cursor to new line, centered
            int inputX = Console.WindowWidth / 2;
            Console.SetCursorPosition(inputX, Console.CursorTop);

            string input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Bekérő szöveg kiírása és bekérés egyel alatta vízszintesen középre igazítva, ESC-el meghív egy függvényt
        /// </summary>
        public static string ReadCenteredC(string prompt)
        {
            int xPrompt = Math.Max(0, (Console.WindowWidth - prompt.Length) / 2);
            Console.SetCursorPosition(xPrompt, Console.CursorTop);
            Console.WriteLine(prompt);

            int inputX = Console.WindowWidth / 2;
            Console.SetCursorPosition(inputX, Console.CursorTop);

            StringBuilder input = new StringBuilder();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.WriteLine();
                        return input.ToString();
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine();
                        Program.Main(); // instantly go back to main menu
                        return null; // this will never be used
                    }
                    else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input.Length--;
                        Console.Write("\b \b"); // erase last char
                    }
                    else if (!char.IsControl(key.KeyChar))
                    {
                        input.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                }
            }
        }

        /// <summary>
        /// Egy sor kiírása vízszintesen középre igazítva
        /// </summary>
        public static void CenterLine(string text)
        {
            int x = (Console.WindowWidth - text.Length) / 2;
            Console.SetCursorPosition(Math.Max(0, x), Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
