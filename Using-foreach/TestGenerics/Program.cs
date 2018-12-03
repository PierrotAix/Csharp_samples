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
            //TestEchanger();
            //TestClassGenerique(); //Créer une classe générique
            //TestRestrictions(); // Restriction sur une métode
            //TestRestrictionClasse(); // Restriction sur une classe
            TestTypeNullable();


            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();
        }

        private static void TestTypeNullable()
        {
            //Nullable<int> entier = null;
            int? entier = null; //simplification d'écriture
            if (!entier.HasValue)
            {
                Console.WriteLine("L'entier n'a pas de valeur");
            }
            entier = 5;
            if (entier.HasValue)
            {
                Console.WriteLine("Valeur de l'entier : " + entier);
            }
        }

        public class TypeValeurNull<T> where T : struct
        {
            private bool aUneValeur;
            public bool AUneValeur
            {
                get { return aUneValeur; }
            }
            private T valeur;

            public T Valeur
            {
                get
                {
                    if (aUneValeur)
                        return valeur;
                    throw new InvalidOperationException();
                }
                set
                {
                    aUneValeur = true;
                    valeur = value;
                }
            }

        }

        private static void TestRestrictionClasse()
        {
            TypeValeurNull<int> entier = new TypeValeurNull<int>();
            if (!entier.AUneValeur)
            {
                Console.WriteLine("L'entier n'a pas de valeur");
            }
            entier.Valeur = 5;
            if (entier.AUneValeur)
            {
                Console.WriteLine("Valeur de l'entier : "+ entier.Valeur);
            }
        }

        public interface IVolant
        {
            void DeplierLesAiles();
            void Voler();
        }

        public class Avion : IVolant
        {
            public void DeplierLesAiles()
            {
                Console.WriteLine("Je déploie mes ailes mécaniques");
            }

            public void Voler()
            {
                Console.WriteLine("J'allume le moteur");
            }
        }

        public class Oiseau : IVolant
        {
            public void DeplierLesAiles()
            {
                Console.WriteLine("Je déploie mes ailes d'oiseau");
            }

            public void Voler()
            {
                Console.WriteLine("Je bats des ailes");                
            }
        }


        public static T Creer<T>() where T : IVolant, new()
        {
            T t = new T();
            t.DeplierLesAiles();
            t.Voler();
            return t;
        }


        private static void TestRestrictions()
        {
            Oiseau oiseau = Creer<Oiseau>();
            Avion a380 = Creer<Avion>();
            /*
            Je déploie mes ailes d'oiseau
            Je bats des ailes
            Je déploie mes ailes mécaniques
            J'allume le moteur
            */
            //Voiture v = Creer<Voiture>();
        }


        private static void TestClassGenerique()
        {
            MalIsteGenerique<int> maListe = new MalIsteGenerique<int>();
            maListe.Ajouter(25);
            maListe.Ajouter(20);
            maListe.Ajouter(30);
            maListe.Ajouter(7);
            maListe.Ajouter(55);

            Console.WriteLine("maListe.ObtenirElement(0) : " + maListe.ObtenirElement(0) );
            Console.WriteLine("maListe.ObtenirElement(1) : " + maListe.ObtenirElement(1));
            Console.WriteLine("maListe.ObtenirElement(2) : " + maListe.ObtenirElement(2));
            Console.WriteLine("maListe.ObtenirElement(3) : " + maListe.ObtenirElement(3));
            Console.WriteLine("maListe.ObtenirElement(4) : " + maListe.ObtenirElement(4));
            Console.WriteLine("maListe.ObtenirElement(5) : " + maListe.ObtenirElement(5));

            for (int i = 0; i < 30; i++)
            {
                maListe.Ajouter(i);
            }
        }

        public class MalIsteGenerique<T>
        {
            private int capacite;
            private int nbElements;
            private T[] tableau;

            public MalIsteGenerique()
            {
                capacite = 10;
                nbElements = 0;
                tableau = new T[capacite];
            }

            public void Ajouter(T element)
            {
                if (nbElements >= capacite)
                {
                    capacite *= 2; // arbitrairement, on double la capacité
                    T[] copierTableau = new T[capacite];
                    for (int i = 0; i < tableau.Length; i++)
                    {
                        copierTableau[i] = tableau[i];
                    }
                    tableau = copierTableau;
                }
                tableau[nbElements] = element;
                nbElements++;
            }

            public T ObtenirElement(int indice)
            {
                if (indice < 0 || indice >= nbElements)
                {
                    Console.WriteLine("L'indice n'est pas bon");
                    return default(T);
                }
                return tableau[indice];
            }
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

    internal class Voiture : IComparable<Voiture> //Interface générique
    {
        public string Couleur { get; set; }
        public string Marque { get; set; }
        public int Vitesse { get; set; }

        public int CompareTo(Voiture obj)
        {
            return Vitesse.CompareTo(obj.Vitesse);
        }
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
