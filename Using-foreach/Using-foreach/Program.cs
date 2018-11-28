﻿using System;
using System.Collections.Generic;
using System.IO;
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

            List<string> maListe = new List<string>();

            // lecture depuis un fichier texte
            string line;
            StreamReader file = new StreamReader("jours.txt", Encoding.ASCII);

            while ((line = file.ReadLine()) != null)
            {
                maListe.Add(line);
            }
            

            foreach (string jour in maListe)
            {
                Console.WriteLine(jour);
            }


            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();


        }
    }
}
