using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Using_foreach
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Début du test");

            List<string> maListe = new List<string> { "lundi", "mardi", "mercredi", "jeudi", "vendredi" };

            foreach (string jour in maListe)
            {
                Console.WriteLine(jour);
            }


            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();


        }
    }
}
