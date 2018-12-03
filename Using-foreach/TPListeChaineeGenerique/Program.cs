using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPListeChaineeGenerique
{
    class Program
    {
        static void Main(string[] args)
        {


            // instancier notre liste chainée avec les entiers 5, 10 et 4.
            int[] mesEntiers = { 5, 10, 4 };
            LinkedList<int> maListeEntiers = new LinkedList<int>(mesEntiers);
            foreach (int item in maListeEntiers)
            {
                Console.WriteLine(item);
            }

            // afficher les éléments de la liste chainée en utilisant la propriété qui accède au premier élément


            // afficher les éléments de la liste chainée en utilisant la propriété qui accède à un élément par son indice
            for (int i = 0; i <maListeEntiers.Count; i++)
            {
                Console.WriteLine(maListeEntiers.ElementAt(i));
            }

            Console.WriteLine("Insertion de deux éléments");
            // inserer 99 à la première position
            maListeEntiers.AddFirst(99);

            // inserer 33 à la seconde position
            maListeEntiers.AddAfter(maListeEntiers.Find(99), 33);

            // inserer 30 à la seconde position
            maListeEntiers.AddAfter(maListeEntiers.Find(99), 30);

            // afficher tout
            for (int i = 0; i < maListeEntiers.Count; i++)
            {
                Console.WriteLine(maListeEntiers.ElementAt(i));
            }

            Console.WriteLine("----------------------------------Utilisation de ma propre liste chainée--------------------------------------------");
            ListeChainee<int> listeChainee = new ListeChainee<int>();
            listeChainee.Ajouter(5);
            listeChainee.Ajouter(10);
            listeChainee.Ajouter(4);
            Console.WriteLine("listeChainee.Premier.Valeur : " + listeChainee.Premier.Valeur);
            Console.WriteLine("listeChainee.Premier.Suivant.Valeur : " + listeChainee.Premier.Suivant.Valeur);
            Console.WriteLine("listeChainee.Premier.Suivant.Suivant.Valeur : " + listeChainee.Premier.Suivant.Suivant.Valeur);
            Console.WriteLine("****************************************");
            Console.WriteLine(listeChainee.ObtenirElement(0).Valeur);
            Console.WriteLine(listeChainee.ObtenirElement(1).Valeur);
            Console.WriteLine(listeChainee.ObtenirElement(2).Valeur);
            Console.WriteLine("*************");
            listeChainee.Inserer(99, 0);
            listeChainee.Inserer(33, 2);
            listeChainee.Inserer(30, 2);
            Console.WriteLine(listeChainee.ObtenirElement(0).Valeur);
            Console.WriteLine(listeChainee.ObtenirElement(1).Valeur);
            Console.WriteLine(listeChainee.ObtenirElement(2).Valeur);
            Console.WriteLine(listeChainee.ObtenirElement(3).Valeur);
            Console.WriteLine(listeChainee.ObtenirElement(4).Valeur);
            Console.WriteLine(listeChainee.ObtenirElement(5).Valeur);
            /*
             ----------------------------------Utilisation de ma propre liste chainée--------
            ------------------------------------
            listeChainee.Premier.Valeur : 5
            listeChainee.Premier.Suivant.Valeur : 10
            listeChainee.Premier.Suivant.Suivant.Valeur : 4
            ****************************************
            5
            5
            10
            *************
            99
            99
            30
            33
            5
            10
             * */



            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();
        }

        public class Chainage<T>
        {
            public Chainage<T> Precedent { get; set; }
            public Chainage<T> Suivant { get; set; }
            public T Valeur { get; set; }
        }

        public class ListeChainee<T>
        {
            public Chainage<T> Premier { get; private set; }

            public Chainage<T> Dernier
            {
                get
                {
                    if (Premier == null)
                        return null;
                    Chainage<T> dernier = Premier;
                    while (dernier.Suivant != null)
                    {
                        dernier = dernier.Suivant;
                    }
                    return dernier;
                }
            }

            public void Ajouter(T element)
            {
                if (Premier == null)
                {
                    Premier = new Chainage<T> { Valeur = element };
                }
                else
                {
                    Chainage<T> dernier = Dernier;
                    dernier.Suivant = new Chainage<T> { Valeur = element, Precedent = dernier };
                }
            }

            public Chainage<T> ObtenirElement(int indice)
            {
                Chainage<T> temp = Premier;
                for (int  i=1; i < indice; i++)
                {
                    if (temp == null)
                        return null;
                    temp = temp.Suivant;
                }
                return temp;
            }

            public void Inserer(T element, int indice)
            {
                if (indice == 0)
                {
                    if (Premier == null)
                        Premier = new Chainage<T> { Valeur = element };
                    else
                    {
                        Chainage<T> temp = Premier;
                        Premier = new Chainage<T> { Suivant = temp, Valeur = element };
                        temp.Precedent = Premier;
                    }
                }
                else
                {
                    Chainage<T> elementAIndice = ObtenirElement(indice);
                    if (elementAIndice == null)
                        Ajouter(element);
                    else
                    {
                        Chainage<T> precedent = elementAIndice.Precedent;
                        Chainage<T> temp = precedent.Suivant;
                        precedent.Suivant = new Chainage<T> { Valeur = element, Precedent = precedent, Suivant = temp };
                        temp.Precedent = precedent.Suivant;
                    }
                }
            }

        }

        /// <summary>
        /// Classe générique possédant 3 propriétés (Précedent, SUivant et Valeur).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Element<T>
        {
            public T valeur;
            public Element<T> Suivant;
            public Element<T> Precedent;
        }


        public class MaMisteChainee<T>
        {
            private int capacite;
            private int nbElements;
            private T[] tableau;

            public MaMisteChainee()
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
    }
}
