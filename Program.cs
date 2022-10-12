using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Kemia {
    class Program {

        private static readonly List<Felfedezes> felfedezesek = new List<Felfedezes>();

        static void Main(string[] args) {
            foreach (string one in File.ReadAllLines("felfedezesek.csv", Encoding.Default).Skip(1)) {
                felfedezesek.Add(new Felfedezes(one));
            }

            Console.WriteLine("3. feladat: Elemek száma: " + felfedezesek.Count);
            Console.WriteLine("4. feladat: Felfedezések száma az ókorban: " + CountFelfedezesekOkor());

            Console.Write("5. feladat: Kérek egy vegyjelet: ");
            Felfedezes elem = InputVegyjel();

            Console.WriteLine("6. feladat: Keresés");

            if (elem == null) {
                Console.WriteLine("\tNincs ilyen elem az adatforrásban!");
            } else {
                Console.WriteLine("\tAz elem vegyjele: " + elem.Vegyjel);
                Console.WriteLine("\tAz elem neve: " + elem.Elem);
                Console.WriteLine("\tRendszáma: " + elem.Rendszam);
                Console.WriteLine("\tFelfedezés éve: " + elem.Ev);
                Console.WriteLine("\tFelfedező neve: " + elem.Felfedezo);
            }

            Console.WriteLine($"7. feladat: {LeghosszabbEv()} év volt a leghosszabb időszak két elem felfedezése között.");

            Console.WriteLine("8. feladat: Statisztika");

            foreach (var pair in Stats()) {
                Console.WriteLine($"\t{pair.Value}: {pair.Key} db");
            }

            Console.ReadKey();
        }

        private static int CountFelfedezesekOkor() {
            return felfedezesek.Where(felf => "Ókor".Equals(felf.Ev)).Count();
        }

        private static Felfedezes InputVegyjel() {
            Regex regex = new Regex("^[a-zA-Z]+$");
            string input;
            Felfedezes felfedezes = null;

            while ((input = Console.ReadLine()).Length == 0 || input.Length > 2
                || !regex.IsMatch(input)
                || (felfedezes = felfedezesek.Find(felf => felf.Vegyjel.Equals(input, StringComparison.CurrentCultureIgnoreCase))) == null) {
                Console.Write("5. feladat: Kérek egy vegyjelet: ");
            }

            return felfedezes;
        }

        private static int LeghosszabbEv() {
            var withoutOkor = felfedezesek.Where(felf => !"Ókor".Equals(felf.Ev)).Select(felf => int.Parse(felf.Ev));
            int leghosszabb = 0;

            for (int i = 0; i < withoutOkor.Count(); i += 2) {
                int subtr = withoutOkor.ElementAt(i + 1) - withoutOkor.ElementAt(i);

                if (subtr > leghosszabb) {
                    leghosszabb = subtr;
                }
            }

            return leghosszabb;
        }

        private static List<KeyValuePair<int, int>> Stats() {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            var withoutOkor = felfedezesek.Where(felf => !"Ókor".Equals(felf.Ev));
            var ordered = withoutOkor.Select(felf => int.Parse(felf.Ev)).OrderBy(felf => felf);
            int lastEv = 0;

            foreach (Felfedezes one in withoutOkor) {
                int ev = int.Parse(one.Ev);
                int db = ordered.Where(ord => ord == ev).Count();

                if (db > 3 && lastEv != ev) {
                    list.Add(new KeyValuePair<int, int>(db, ev));
                    lastEv = ev;
                }
            }

            return list;
        }
    }
}
