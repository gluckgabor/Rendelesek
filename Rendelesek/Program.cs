using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rendelesek
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string[] osszesSor = File.ReadAllLines("Rendelesek.txt");

            List<string> osszesSorFejlecNelkul = new List<string>();
            
            List<Rendeles> RendelesObjektumLista = new List<Rendeles>();


            for (int i = 1; i < osszesSor.Length; i++)
            {
                osszesSorFejlecNelkul.Add(osszesSor[i]);
            }
            

            foreach (var item in osszesSorFejlecNelkul)
            {
                //peldanyositas
                Rendeles rendeles = new Rendeles(item);

                RendelesObjektumLista.Add(rendeles);
            }

            /*1: hány darab megrendelésünk van?*/
            Console.WriteLine("1: hány darab megrendelésünk van ?");
            Console.WriteLine(hanydarabmegrendeles(RendelesObjektumLista));

            /*2: összesen hány darab ebéd került megrendelésre a nap folyamán ?*/
            Console.WriteLine("2: összesen hány darab ebéd került megrendelésre a nap folyamán ?");
            Console.WriteLine(osszesenhanydarabebed(RendelesObjektumLista));

            /*3 : Mennyi a rendelések összbevétele ? (bizonyos ételekből többet rendeltek!!!)*/
            Console.WriteLine("3 : Mennyi a rendelések összbevétele ? (bizonyos ételekből többet rendeltek!!!)");
            Console.WriteLine(Mennyiarendelesekosszb(RendelesObjektumLista));

            /*4.: van e köret 400 Ft felett?*/
            Console.WriteLine("4.: van e köret 400 Ft felett?");
            vanEKoret400FtFelett(RendelesObjektumLista);

            /*5.: melyik rendelésből(kategóriától függetlenül) adták el a legtöbbet?*/
            Console.WriteLine("/*5.: melyik rendelésből(kategóriától függetlenül) adták el a legtöbbet?*/");
            Console.WriteLine(maxeladottdb(RendelesObjektumLista));

            /*6.: melyik rendelés volt a legdrágább, ha az eladott darabszám és az egységár szorzatát vesszük ?*/
            Console.WriteLine("6.: melyik rendelés volt a legdrágább, ha az eladott darabszám és az egységár szorzatát vesszük ?");
            Console.WriteLine(legdragabb(RendelesObjektumLista));

            //7.: a köretet leszámítva, minden étkezés(reggeli, ebéd, vacsora) 600 Ft feletti volt / 1 darab ?
            Console.WriteLine("7.: a köretet leszámítva, minden étkezés(reggeli, ebéd, vacsora) 600 Ft feletti volt / 1 darab ?");
            minuszkoret600felettivoltE(RendelesObjektumLista);

            //8.: Kérjük be 1 étel nevét!Legalább 3 betű legyen, különben kérjük újra!Ha találunk ilyen rendelést, akkor írjunk ki róla mindent! Különben közöljük, hogy: "nincs ilyen rendelés!"
            etelNeve(RendelesObjektumLista);

            //9.: Milyen kategóriában vannak rendelések?
            Console.WriteLine("9.: Milyen kategóriában vannak rendelések?");
            milyenkategoria(RendelesObjektumLista);

            //10.: Melyik kategóriában hány rendelés történt ? (van, ahol többet darabot rendeltek)
            Console.WriteLine("10.: Melyik kategóriában hány rendelés történt ? (van, ahol többet darabot rendeltek)");
            kategoriankentiRendelesDbSzam(RendelesObjektumLista);

            /*11.: Kérjünk be 1 új rendelést!Név, kategória, egységár és darab tekintetében!
             * Egy új fájlba "ujrendelesek.txt" írjuk bele a legvégére a felvitt értékeket.
             * Ellenőrzés nem kell, feltételezzük, hogy helyesen adták meg az adatokat. 
             * Azonban az eredeti - "rendelesek.txt" tartalma is legyen a fájlban és utána 
             * kerüljön minden új rendelés, amit bekértünk!
            */
            bekeres(RendelesObjektumLista);
            

            Console.ReadLine();
        }

        private static void bekeres(List<Rendeles> rendelesObjektumLista)
        {
            StringBuilder sb = new StringBuilder();

            File.Create("ujrendelesek.txt");
            
            sb.AppendLine(File.ReadAllText("Rendelesek.txt"));
            
            Console.WriteLine("Kérlek add meg az új rendelést:");
            sb.AppendLine(Console.ReadLine());

            File.WriteAllText("ujrendelesek.txt", sb.ToString());
        }

        private static void kategoriankentiRendelesDbSzam(List<Rendeles> rendelesObjektumLista)
        {

            Dictionary<string, int> KategoriankentiRendeles = new Dictionary<string, int>();

            foreach (var item in rendelesObjektumLista)
            {

                if (!(KategoriankentiRendeles.ContainsKey(item.Kat)))
                {
                    KategoriankentiRendeles.Add(item.Kat, item.DB);
                }
                else
                {
                    KategoriankentiRendeles[item.Kat] += item.DB;
                }
            }

            foreach (var item in KategoriankentiRendeles)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }

        }

        private static void milyenkategoria(List<Rendeles> rendelesObjektumLista)
        {
            HashSet<string> kategoriakDuplikatumokNelkul = new HashSet<string>();

            foreach (var item in rendelesObjektumLista)
            {
                kategoriakDuplikatumokNelkul.Add(item.Kat);
            }

            foreach (var item in kategoriakDuplikatumokNelkul)
            {
                Console.WriteLine(item);
            } 
        }

        private static void etelNeve(List<Rendeles> rendelesObjektumLista)
        {
            string bekertNev;

            List<bool> listName = new List<bool>();

            do
            {
                Console.WriteLine("8.: Melyik ételre vagy kiváncsi, kérnék legalább 3 betűt: ");
                bekertNev = Console.ReadLine();
            } while (bekertNev.Length < 3);


            foreach (var item in rendelesObjektumLista)
            {
                if (bekertNev == item.Nev)
                {
                    Console.WriteLine(item.ToString());
                    listName.Add(true);
                }
                else
                {
                    listName.Add(false);
                }
            }

            if (!(listName.Contains(true)))
            {
                Console.WriteLine("Nincs ilyen étel a listában.");
            }
        }

        private static void minuszkoret600felettivoltE(List<Rendeles> rendelesObjektumLista)
        {
            int maxeladottArOsszesen = 0;

            List<bool> minuszkoret600felettivoltE = new List<bool>();


            foreach (var item in rendelesObjektumLista)
            {
                if (item.Kat != "köret")
                {
                    if (item.Ar > 600)
                    {
                        minuszkoret600felettivoltE.Add(true);
                    }
                    else
                    {
                        minuszkoret600felettivoltE.Add(false);
                    } 
                }
            }

            if (minuszkoret600felettivoltE.Contains(false))
            {
                Console.WriteLine("Nem mindegyik volt 600 felett");
            }
            else
            {
                Console.WriteLine("Mindegyik 600 felett volt.");
            }
            
        }

        private static string legdragabb(List<Rendeles> rendelesObjektumLista)
        {
            int maxeladottArOsszesen = 0;
            string maxeladottNeve = "";

            foreach (var item in rendelesObjektumLista)
            {
                if (item.Ar * item.DB > maxeladottArOsszesen)
                {
                    maxeladottArOsszesen = item.Ar * item.DB;
                    maxeladottNeve = item.Nev;
                }
            }

            return maxeladottNeve;
        }

        private static string maxeladottdb(List<Rendeles> rendelesObjektumLista)
        {
            int maxeladottdb = 0;
            string maxeladottNeve = "";

            foreach (var item in rendelesObjektumLista)
            {
                if (item.DB > maxeladottdb)
                {
                    maxeladottdb = item.DB;
                    maxeladottNeve = item.Nev;
                }
            }

            return maxeladottNeve;
        }

        private static void vanEKoret400FtFelett(List<Rendeles> rendelesObjektumLista)
        {
            bool vanEKoret400FtFelett = false;

            List<bool> vanEKoret400FtFelettboollista = new List<bool>();

            foreach (var item in rendelesObjektumLista)
            {
                if (item.Kat == "köret" && item.Ar > 400)
                {
                    vanEKoret400FtFelettboollista.Add(true);
                }
                else
                {
                    vanEKoret400FtFelettboollista.Add(false);
                }
            }

            if (vanEKoret400FtFelettboollista.Contains(true))
            {
                Console.WriteLine("igen, van.");
            }
            else
            {
                Console.WriteLine("nincs");
            }
        }

        private static int Mennyiarendelesekosszb(List<Rendeles> rendelesObjektumLista)
        {
            int countOsszBev = 0;

            foreach (var item in rendelesObjektumLista)
            {
                if (item.Kat == "ebéd")
                {
                    countOsszBev += item.DB * item.Ar;
                }
            }

            return countOsszBev;
        }

        private static int osszesenhanydarabebed(List<Rendeles> RendelesObjektumLista)
        {
            int countEbed = 0;

            foreach (var item in RendelesObjektumLista)
            {
                if (item.Kat == "ebéd")
                {
                    countEbed += item.DB;
                }
            }

            return countEbed;
        }

        private static int hanydarabmegrendeles(List<Rendeles> RendelesObjektumLista)
        {
            int count = 0;

            foreach (var item in RendelesObjektumLista)
            {
                count++;
            }

            return count;
        }
    }
}
