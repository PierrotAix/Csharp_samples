using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerics
{
    class Program
    {
        /// <summary>
        /// Illustre https://openclassrooms.com/fr/courses/2818931-programmez-en-oriente-objet-avec-c/2819081-les-generiques
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //TestDictionnaire();
            //TestAfficheRepresentationPremiereVersion()
            //TestAfficheRepresentationSecondeVersion();
            TestEchanger();


            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();
        }

        private static void TestEchanger()
        {
            int i = 5;
            int j = 10;
            Console.WriteLine($" Avant Echanger i vaut {i} et j vaut {j}");
            Echanger(ref i, ref j);
            Console.WriteLine($" Après Echanger i vaut {i} et j vaut {j}");
            /*
            Avant Echanger i vaut 5 et j vaut 10
            Après Echanger i vaut 10 et j vaut 5
            */

            Voiture v1 = new Voiture { Couleur = "verte" };
            Voiture v2 = new Voiture { Couleur = "rouge" };
            Console.WriteLine($" Avant Echanger v1.Couleur vaut {v1.Couleur} et v2.Couleur vaut {v2.Couleur}");
            Echanger(ref v1, ref v2);
            Console.WriteLine($" Après Echanger v1.Couleur vaut {v1.Couleur} et v2.Couleur vaut {v2.Couleur}");
            /*
             *  Avant Echanger v1.Couleur vaut verte et v2.Couleur vaut rouge
                Après Echanger v1.Couleur vaut rouge et v2.Couleur vaut verte
             */

        }

        private static void Echanger<T>(ref T t1, ref T t2)
        {
            T temp = t1;
            t1 = t2;
            t2 = temp;
        }

        private static void TestAfficheRepresentationSecondeVersion()
        {
            int i = 5;
            double d = 9.5;
            string s = "abcd";
            Voiture v = new Voiture();

            Afficheur.Affiche(i);
            Afficheur.Affiche(d);
            Afficheur.Affiche(s);
            Afficheur.Affiche(v);
            /*
            Afficheur d'objet :
                    Type: System.Int32
                   Représentation : 5
            Afficheur d'objet :
                    Type: System.Double
                   Représentation : 9,5
            Afficheur d'objet :
                    Type: System.String
                   Représentation : abcd
            Afficheur d'objet :
                    Type: TestGenerics.Voiture
                   Représentation : TestGenerics.Voiture
            Fin du test, tapez sur une touche pour sortir
            */

        }

        private static void TestAfficheRepresentationPremiereVersion()
        {
            int i = 5;
            double d = 9.5;
            string s = "abcd";
            Voiture v = new Voiture();

            Afficheur.Affiche(i);
            Afficheur.Affiche(d);
            Afficheur.Affiche(s);
            Afficheur.Affiche(v);
            /*
            Afficheur d'objet :
                    Type: System.Int32
                    Représentation : 5
            Afficheur d'objet :
                    Type: System.Double
                   Représentation : 9,5
            Afficheur d'objet :
                    Type: System.String
                   Représentation : abcd
            Afficheur d'objet :
                    Type: TestGenerics.Voiture
                   Représentation : TestGenerics.Voiture
            Fin du test, tapez sur une touche pour sortir
            */



        }


        private static void TestDictionnaire()
        {
            Dictionary<string, Personne> annuaire = new Dictionary<string, Personne>();
            annuaire.Add("06 01 02 03 04", new Personne { Prenom = "Nicolas" });
            annuaire.Add("06 00 00 00 00", new Personne { Prenom = "Jéremieé" });

            Personne p = annuaire["06 00 00 00 00"];
            Console.WriteLine("p est:" + p.Prenom); // affiche Jéremieé
        }
    }

    internal class Voiture
    {
        public string Couleur { get; set; }
        public string Marque { get; set; }
    }

    public static class Afficheur
    {
        // Première version
        //public static void Affiche(object o)
        //{
        //    Console.WriteLine("Afficheur d'objet :");
        //    Console.WriteLine("\tType : "  + o.GetType() );
        //    Console.WriteLine("\tReprésentation : " + o.ToString() );
        //}

        // seconde version améliorée
        public static void Affiche<T>(T a)
        {
            Console.WriteLine("Afficheur d'objet :");
            Console.WriteLine("\tType : " + a.GetType());
            Console.WriteLine("\tReprésentation : " + a.ToString());
        }
    }

    internal class Personne
    {
        public string Prenom { get; set; }
    }
}
