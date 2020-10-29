using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendelesek
{
    class Rendeles
    {
        public string Nev { get; set; }
        public string Kat { get; set; }
        public int Ar { get; set; }
        public int DB { get; set; }

        public Rendeles(string line)
        {
            string[] megkapottSorFelbontvaStringtombre = line.Split(';');

            this.Nev = megkapottSorFelbontvaStringtombre[0];
            this.Kat = megkapottSorFelbontvaStringtombre[1];
            this.Ar = Convert.ToInt32(megkapottSorFelbontvaStringtombre[2]);
            this.DB = Convert.ToInt32(megkapottSorFelbontvaStringtombre[3]);
        }

        //public string getNev { return Nev; }
        //public string getKat { return Kat; }
        //public int getAr { return Ar; }
        //public int getDB { return DB; }


        public override string ToString()
        {
            return ($"Nev: {Nev}, {Kat}, {Ar}, {DB}");
        }


    }
}
