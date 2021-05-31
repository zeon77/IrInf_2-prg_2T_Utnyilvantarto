using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utnyilvantarto
{
    class ÓraÁllás
    {
        public static int üzemanyagár = 0;
        public static int normaköltség = 0;
        public static double átlagfogyasztás = 0;

        public DateTime Dátum { get; set; }
        public int ÓraStart { get; set; }
        public ÓraÁllás(string sor)
        {
            string[] s = sor.Split(';');
            Dátum = DateTime.Parse(s[0]);
            ÓraStart = int.Parse(s[1]);
        }
        public static double Költség(int megtettTáv)
        {
            return Math.Round(megtettTáv / 100.0 * átlagfogyasztás * üzemanyagár + megtettTáv * normaköltség, 0);
        }
    }
}
