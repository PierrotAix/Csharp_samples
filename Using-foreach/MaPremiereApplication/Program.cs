using MaBibliotheque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaPremiereApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MaBibliotheque.Bonjour bonjour = new MaBibliotheque.Bonjour();
            bonjour.DireBonjour();

            Console.WriteLine("----------------------------------------------------");
            Client client = new Client("Nico", "12345");
            Console.WriteLine(client.MotDePasse);

            Console.WriteLine(Generateur.ObtenirIdentifiantUnique());
            /*
             * Bonjour depuis la bibliothèque MaBibliotheque
                ----------------------------------------------------
                MTIzNDU=
                NTk3NzU4NTA5ODAxNTgzMjY4NQ==

             * */

            //string chaine = "1234565".Crypte();

            for (int i = 0; i < 10000; i++)
            {
                if (AForge.Math.Tools.IsPowerOf2(i))
                {
                    Console.WriteLine(i + " est une puissance de 2");
                }
            }
            /*
            1 est une puissance de 2
            2 est une puissance de 2
            4 est une puissance de 2
            8 est une puissance de 2
            16 est une puissance de 2
            32 est une puissance de 2
            64 est une puissance de 2
            128 est une puissance de 2
            256 est une puissance de 2
            512 est une puissance de 2
            1024 est une puissance de 2
            2048 est une puissance de 2
            4096 est une puissance de 2
            8192 est une puissance de 2

             * */

            // Culture

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            Affiche();


            Console.ReadKey();
        }

        private static void Affiche()
        {
            Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentCulture); //fr-FR
            decimal dec = 5.5M;
            double dou = 4.8;
            DateTime date = new DateTime(2011, 12, 25);
            Console.WriteLine("Décimal : {0}", dec);
            Console.WriteLine("Double : {0}", dou);
            Console.WriteLine("Date : {0}", date);
            /*
             * fr-FR
             Décimal : 5,5
            Double : 4,8
            Date : 25/12/2011 00:00:00
             * */


        }
    }
}
