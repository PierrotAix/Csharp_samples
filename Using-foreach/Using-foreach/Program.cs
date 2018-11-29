using System;
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

            // lecture depuis un fichier texte qui se trouve avec l'executable
            // ce fichier se trouve dans .\Using-foreach\Using-foreach\bin\Debug\Jours.txt
            // encore un changement de commentaire
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
