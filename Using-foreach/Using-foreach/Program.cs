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

            foreach (Char c in "Hello world !")
            {
                Console.WriteLine(c);
            }


            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();


        }
    }
}
