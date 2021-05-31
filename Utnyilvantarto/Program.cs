using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utnyilvantarto
{
    class Program
    {
        static void Main(string[] args)
        {
            //2.
            List<ÓraÁllás> óraÁllások = new List<ÓraÁllás>();
            foreach (var sor in File.ReadAllLines("januar.txt").Skip(1))
            {
                óraÁllások.Add(new ÓraÁllás(sor));
            }

            //3.
            Console.WriteLine($"3. feladat: Munkanapok száma: {óraÁllások.Where(x => x.Dátum.Month == 1).Count()}");

            //4.
            var list = óraÁllások
                .Take(óraÁllások.Count - 1)
                .Zip(óraÁllások.Skip(1), (first, second) => new { ÓraÁllás = first, MegtettTáv = second.ÓraStart - first.ÓraStart }).ToList();

            var max = list.OrderBy(x => x.MegtettTáv).Last();
            Console.WriteLine($"4. feladat: Az autó legtöbbet {max.ÓraÁllás.Dátum.ToString("yyyy.MM.dd")}-án/én futott, a megtett távolság: {max.MegtettTáv}km");

            //5.
            var MegtettÚt = list
                .Where(x => x.ÓraÁllás.Dátum.DayOfWeek == DayOfWeek.Friday)
                .Sum(x => x.MegtettTáv);
            Console.WriteLine($"5. feladat: Pénteki napokon megtett távolság összege: {MegtettÚt}km");

            //6.
            Console.WriteLine($"6. feladat: Adja meg a következő adatokat");
            Console.Write($"\t Rendszám: ");
            string rendszám = Console.ReadLine();
            Console.Write($"\t Alkalmazhaztó üzemanyagár: ");
            ÓraÁllás.üzemanyagár = int.Parse(Console.ReadLine());
            Console.Write($"\t Kilométerenkénti normaköltség: ");
            ÓraÁllás.normaköltség = int.Parse(Console.ReadLine());
            Console.Write($"\t Gépjármű átlagfogyasztása: ");
            ÓraÁllás.átlagfogyasztás = double.Parse(Console.ReadLine());

            //7.
            List<string> ls = new List<string>();
            ls.Add("Datum;OraStart;OraStop;MegtettTav;Koltseg");
            list.ForEach(x => ls.Add(
                $"{x.ÓraÁllás.Dátum.ToString("yyyy.MM.dd")};" +
                $"{x.ÓraÁllás.ÓraStart};" +
                $"{x.ÓraÁllás.ÓraStart + x.MegtettTáv};" +
                $"{x.MegtettTáv};" +
                $"{ÓraÁllás.Költség(x.MegtettTáv)}"));

            string filename = "2019_januar_" + rendszám + ".txt";
            File.WriteAllLines(filename, ls.ToArray());

            Console.ReadKey();
        }
    }
}
