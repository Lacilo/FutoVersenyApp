using System;
using System.Collections.Generic;
using FutoversenyApp.Models;
using menu.Models;

namespace FutoversenyApp.Controllers
{
    internal class Controller
    {
        /// <summary>
        /// Bekéri a felhasználó adatait
        /// </summary>
        public static void SzAdatok()
        {
            Console.BackgroundColor = Program.background;
            Console.Clear();
            Console.BackgroundColor = Program.background;
            Console.ForegroundColor = Program.textcolor;


            string magassag;
            while (true)
            {
                magassag = CenterEngine.ReadCentered("Magasság (cm): ");
                try
                {
                    double.Parse(magassag);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen magasság! Kérlek számot adj meg! ");
                }
            }

            string tomeg;
            while (true)
            {
                tomeg = CenterEngine.ReadCentered("Tömeg (kg): ");
                try
                {
                    double.Parse(tomeg);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen tömeg! Kérlek számot adj meg! ");
                }
            }

            string nyugpul;
            while (true)
            {
                nyugpul = CenterEngine.ReadCentered("Nyugalmi Pulzus: ");
                try
                {
                    int.Parse(nyugpul);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen pulzus! Kérlek egész számot adj meg! ");
                }
            }
            
            string celido;
            while (true)
            {
                celido = CenterEngine.ReadCentered("Célidő (perc): ");
                try
                {
                    double.Parse(celido);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen idő! Kérlek számot adj meg! ");
                }
            }

            string szuldat;
            while (true)
            {
                szuldat = CenterEngine.ReadCentered("Születési Dátum (ÉÉÉÉ.HH.NN): ");
                if (szuldat == "")
                {
                    szuldat = DateTime.Now.ToString();
                    break;
                }
                try
                {
                    DateTime.Parse(szuldat);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen dátum! (ÉÉÉÉ.MM.DD) ");
                }
            }

            User ujUser = new User(magassag, tomeg, nyugpul, celido, szuldat);
            User.JsonWriter(ujUser);

            Program.Main();
        }

        /// <summary>
        /// Újra bekéri a felhasználó adatait, majd felülírja a régi adatokat az újjal
        /// </summary>
        public static void SzAdatokSzerk()
        {
            Console.BackgroundColor = Program.background;
            Console.Clear();
            Console.BackgroundColor = Program.background;
            Console.ForegroundColor = Program.textcolor;

            User user = User.UserJsonReader();

            string magassag;
            while (true)
            {
                magassag = CenterEngine.ReadCentered($"Magasság ({user.Magassag}cm): ");
                if (magassag == "")
                {
                    magassag = user.Magassag.ToString();
                    break;
                }
                try
                {
                    double.Parse(magassag);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen magasság! Kérlek számot adj meg! ");
                }
            }
            

            string tomeg;
            while (true)
            {
                tomeg = CenterEngine.ReadCentered($"Tömeg ({user.Tomeg}kg): ");
                if (tomeg == "")
                {
                    tomeg = user.Tomeg.ToString();
                    break;
                }
                try
                {
                    double.Parse(tomeg);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen tömeg! Kérlek számot adj meg! ");
                }
            }

            string nyugpul;
            while (true)
            {
                nyugpul = CenterEngine.ReadCentered($"Nyugalmi Pulzus ({user.Nyugpul}): ");
                if (nyugpul == "")
                {
                    nyugpul = user.Nyugpul.ToString();
                    break;
                }
                try
                {
                    int.Parse(nyugpul);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen pulzus! Kérlek egész számot adj meg! ");
                }
            }

            string celido;
            while (true)
            {
                celido = CenterEngine.ReadCentered($"Célidő ({user.Celido}perc): ");
                if (celido == "")
                {
                    celido = user.Celido.ToString();
                    break;
                }
                try
                {
                    double.Parse(celido);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen idő! Kérlek számot adj meg! ");
                }
            }

            string szuldat;
            while (true)
            {
                szuldat = CenterEngine.ReadCentered($"Születési Dátum ({user.Szuldat}): ");
                if (szuldat == "")
                {
                    szuldat = user.Szuldat.ToString();
                    break;
                }
                try
                {
                    DateTime.Parse(szuldat);
                    break;
                }
                catch
                {
                    CenterEngine.CenterLine("Érvénytelen dátum! (ÉÉÉÉ.MM.DD) ");
                }
            }

            // User ujUser = new User(magassag, tomeg, nyugpul, celido, szuldat);
            // User.JsonWriter(ujUser);

            user.Magassag = int.Parse(magassag);
            user.Tomeg = int.Parse(tomeg);
            user.Nyugpul = int.Parse(nyugpul);
            user.Celido = int.Parse(celido);
            user.Szuldat = DateTime.Parse(szuldat);
            string[] adatok = { DateTime.Now.ToString(), tomeg, nyugpul };
            user.szemelyHistory.Add(adatok);

            User.JsonWriter(user);

            Program.Main();
        }

        /// <summary>
        /// Bekéri az edzés adatait
        /// </summary>
        public static void Edzes(List<Futas> futasok)
        {
            Console.BackgroundColor = Program.background;
            Console.Clear();
            Console.BackgroundColor = Program.background;
            Console.ForegroundColor = Program.textcolor;

            string datum;
            while (true)
            {
                datum = CenterEngine.ReadCenteredC("Dátum: ");
                if (datum == "")
                {
                    datum = DateTime.Now.ToString();
                    break;
                }
                else
                {
                    try
                    {
                        DateTime.Parse(datum);
                        break;
                    }
                    catch (FormatException)
                    {
                        CenterEngine.CenterLine("Érvénytelen formátum! A helyes formátum: ÉÉÉÉ.HH.NN");
                    }
                }
            }

            string tavolsag;
            while (true)
            {
                tavolsag = CenterEngine.ReadCenteredC("Távolság (m): ");
                try
                {
                    int.Parse(tavolsag);
                    break;
                }
                catch (Exception)
                {
                    CenterEngine.CenterLine("Ide számot kell írni! ");
                }
            }

            string idotartam;
            while (true)
            {
                idotartam = CenterEngine.ReadCenteredC("Időtartam (óó:pp:mm): ");
                try
                {
                    int teszt = int.Parse(idotartam.Split(':')[1]) * 1; // Teszt instrukció hogy legyen ami errort ad

                    try
                    {
                        teszt = int.Parse(idotartam.Split(':')[2]) * 1; // Teszt instrukció hogy legyen ami errort ad
                    }
                    catch (Exception)
                    {
                        idotartam += ":00"; // Ha nincs mehadva másodperc akkor ne crash
                        break;
                    }

                    break;
                }
                catch (Exception)
                {
                    CenterEngine.CenterLine("Hibás formátum! Az időtartamot a következő formátumba adja meg: óó:pp:mm");
                }
            }

            string maxpulzus;
            while (true)
            {
                maxpulzus = CenterEngine.ReadCenteredC("Maximális Pulzus: ");
                try
                {
                    int.Parse(maxpulzus);
                    break;
                }
                catch (Exception)
                {
                    CenterEngine.CenterLine("Ide számot kell írni! ");
                }
            }

            Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            futasok.Add(ujFutas);
            Futas.JsonWriter(futasok);

            Program.Main();
        }

        /// <summary>
        /// Újra bekéri az edzés adatait, majd felülírja a régi adatokat az újjal
        /// </summary>
        public static void Szerkesztes(List<Futas> futasok, int kivalasztott)
        {
            string datum;
            while (true)
            {
                datum = CenterEngine.ReadCenteredC("Dátum: ");
                if (datum == "")
                {
                    datum = futasok[kivalasztott].Datum.ToString();
                    break;
                }
                else
                {
                    try
                    {
                        DateTime.Parse(datum);
                        break;
                    }
                    catch (FormatException)
                    {
                        CenterEngine.CenterLine("Érvénytelen formátum! A helyes formátum: ÉÉÉÉ.HH.NN");
                    }
                }
            }

            string tavolsag;
            while (true)
            {
                tavolsag = CenterEngine.ReadCentered("Távolság (m): ");
                if (tavolsag == "")
                {
                    tavolsag = futasok[kivalasztott].Tavolsag.ToString();
                    break;
                }
                else
                {
                    try
                    {
                        int.Parse(tavolsag);
                        break;
                    }
                    catch (Exception)
                    {
                        CenterEngine.CenterLine("Ide számot kell írni! ");
                    }
                }
            }

            string idotartam = CenterEngine.ReadCentered("Időtartam (perc): ");
            if (idotartam == "")
            {
                idotartam = futasok[kivalasztott].Idotartam;
            }
            else
            {
                while (true)
                {
                    idotartam = CenterEngine.ReadCentered("Időtartam (perc): ");
                    try
                    {
                        int teszt = int.Parse(idotartam.Split(':')[1]) * 1; // Teszt instrukció hogy legyen ami errort ad

                        try
                        {
                            teszt = int.Parse(idotartam.Split(':')[2]) * 1; // Teszt instrukció hogy legyen ami errort ad
                        }
                        catch (Exception)
                        {
                            idotartam += ":00"; // Ha nincs mehadva másodperc akkor ne crash
                            break;
                        }

                        break;
                    }
                    catch (Exception)
                    {
                        CenterEngine.CenterLine("Hibás formátum! Az időtartamot a következő formátumba adja meg: óó:pp:mm");
                    }
                }
            }

            string maxpulzus;
            while (true)
            {
                maxpulzus = CenterEngine.ReadCentered("Maximális Pulzus: ");
                if (maxpulzus == "")
                {
                    maxpulzus = futasok[kivalasztott].Maxpulzus.ToString();
                    break;
                }
                else
                {
                    try
                    {
                        int.Parse(maxpulzus);
                        break;
                    }
                    catch (Exception)
                    {
                        CenterEngine.CenterLine("Ide számot kell írni! ");
                    }
                }
            }

            // Futas ujFutas = new Futas(datum, tavolsag, idotartam, maxpulzus);
            // futasok[kivalasztott] = ujFutas;

            // Új megoldás mert az előző felűlírta a pulzust és tömeget mivel új objektumot hoztunk létre
            futasok[kivalasztott].Datum = DateTime.Parse(datum);
            futasok[kivalasztott].Tavolsag = int.Parse(tavolsag);
            futasok[kivalasztott].Idotartam = idotartam;
            futasok[kivalasztott].Maxpulzus = int.Parse(maxpulzus);

            Futas.JsonWriter(futasok);
        }

        /// <summary>
        /// Kitörli a kiválasztott edzést
        /// </summary>
        public static void Torles(List<Futas> futasok, int kivalasztott)
        {
            futasok.RemoveAt(kivalasztott);
            Futas.JsonWriter(futasok);
        }
        public static Futas Min(List<Futas> futasok, char mode)
        {
            Futas min = futasok[0];

            foreach (Futas futas in futasok)
            {
                if (mode == 'd')
                {
                    if (futas.Datum < min.Datum)
                    {
                        min = futas;
                    }
                }

                if (mode == 't')
                {
                    if (futas.Tavolsag < min.Tavolsag)
                    {
                        min = futas;
                    }
                }

                if (mode == 'i')
                {
                    //if(futas.Idotartam < max.Idotartam)
                    //{
                    //    min = futas;
                    //}
                }

                if (mode == 'm')
                {
                    if (futas.Maxpulzus < min.Maxpulzus)
                    {
                        min = futas;
                    }
                }
            }

            return min;
        }

        public static int MegtettOssztav(List<Futas> futasok)
        {
            int ossztav = 0;

            foreach (Futas futas in futasok)
            {
                ossztav += futas.Tavolsag;
            }

            return ossztav;
        }

        public static Futas Max(List<Futas> futasok, char mode)
        {
            Futas max = futasok[0];

            foreach (Futas futas in futasok)
            {
                if(mode == 'd')
                {
                    if (futas.Datum > max.Datum)
                    {
                        max = futas;
                    }
                }

                if (mode == 't')
                {
                    if (futas.Tavolsag > max.Tavolsag)
                    {
                        max = futas;
                    }
                }

                if (mode == 'i')
                {
                    //if(futas.Idotartam > max.Idotartam)
                    //{
                    //    max = futas;
                    //}
                }

                if (mode == 'm')
                {
                    if(futas.Maxpulzus > max.Maxpulzus)
                    {
                        max = futas;
                    }
                }
            }

            return max;
        }

        /// <summary>
        /// sorba rendez egy listát a megadott mód szerint (d - Date, t - Távolság, i - Időtartam, m - Max pulzus) és ha kell csökkenő de alapértelmezetten növekvő sorrendben(asc = true)
        /// </summary>
        /// <param name="futasok"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static List<Futas> Sort(List<Futas> futasokInp, char mode = 'd', bool asc = true)
        {
            List<Futas> futasok = new List<Futas> (futasokInp);

            List<Futas> sortedFutas = new List<Futas>();
            Futas maxFutas = new Futas();

            while (futasok.Count > 0)
            {
                if (!asc)
                {
                    maxFutas = Max(futasok, mode);
                    sortedFutas.Add(maxFutas);
                    futasok.Remove(maxFutas);
                }
                else
                {
                    maxFutas = Min(futasok, mode);
                    sortedFutas.Add(maxFutas);
                    futasok.Remove(maxFutas);
                }
            }

            return sortedFutas;
        }
    }
}
