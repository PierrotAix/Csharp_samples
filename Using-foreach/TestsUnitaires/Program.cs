using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsUnitaires
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test01();

            //Test02();

            Test03();

            Console.ReadKey();
        }

        private static void Test03()
        {
            int entier = 123;
            string chaine = "abc";
            DateTime date = new DateTime(2012, 4, 28);
            Console.WriteLine("Résultat {1}, {{0}}, {2}", entier, chaine, date.ToString("Le dd-MMMM-yyyy"));

        }

        private static void Test02()
        {
            int valeur = 0;
            int resultat = Factorielle(valeur);
            if (resultat != 1)
                Console.WriteLine("Le test a raté");

            valeur = 1;
            resultat = Factorielle(valeur);
            if (resultat != 1)
                Console.WriteLine("Le test a raté");

            valeur = 2;
            resultat = Factorielle(valeur);
            if (resultat != 2)
                Console.WriteLine("Le test a raté");

            valeur = 3;
            resultat = Factorielle(valeur);
            if (resultat != 6)
                Console.WriteLine("Le test a raté");

            valeur = 4;
            resultat = Factorielle(valeur);
            if (resultat != 24)
                Console.WriteLine("Le test a raté");
        }

        private static int Factorielle(int a)
        {
            /*
            int total = 1;
            for (int i = 1; i <= a; i++)
            {
                total *= i;
            }
            return total;
            */

            // recursivité
            if (a <= 1)
                return 1;
            return a * Factorielle(a - 1);
        }

        private static void Test01()
        {
            // arranger
            int a = 1;
            int b = 2;

            //agir
            int resultat = Addition(a, b);

            // auditer
            if (resultat != 3)
            {
                Console.WriteLine("Le test a raté");
            }
        }

        public static int Addition(int a, int b)
        {
            return a + b;
        }
    }
}
