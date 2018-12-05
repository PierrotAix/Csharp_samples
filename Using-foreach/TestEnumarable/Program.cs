using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumarable
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test01(); // https://openclassrooms.com/fr/courses/2818931-programmez-en-oriente-objet-avec-c/2819176-rendez-une-classe-enumerable

            //Test02();

            //Test03();

            Test04();

            Console.ReadKey();
        }

        private static void Test04()
        {
            Test02();
        }

        public class ChaineEnumerable : IEnumerable<char>
        {
            private string chaine;
            public ChaineEnumerable(string valeur)
            {
                chaine = valeur;
            }

            public IEnumerator<char> GetEnumerator()
            {
                for (int i = 0; i < chaine.Length; i++)
                {
                    yield return chaine[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }


        }

        private static void Test03()
        {
            foreach (string prenom in ObtenirListeDePrenoms())
            {
                Console.WriteLine(prenom);
            }
        }

        private static IEnumerable<string> ObtenirListeDePrenoms()
        {
            yield return "Nicolas";
            yield return "Jérémie";
            yield return "Delphine";
        }

        private static void Test02()
        {
            Console.WriteLine("----------------------------------Utilisation de ma propre liste chainée--------------------------------------------");
            ListeChainee<int> listeChainee = new ListeChainee<int>();
            listeChainee.Ajouter(5);
            listeChainee.Ajouter(10);
            listeChainee.Ajouter(4);
            Console.WriteLine("listeChainee.Premier.Valeur : " + listeChainee.Premier.Valeur);
            Console.WriteLine("listeChainee.Premier.Suivant.Valeur : " + listeChainee.Premier.Suivant.Valeur);
            Console.WriteLine("listeChainee.Premier.Suivant.Suivant.Valeur : " + listeChainee.Premier.Suivant.Suivant.Valeur);
            Console.WriteLine("****************************************");
            // Utilisation d'une boucle foreach
            foreach (var item in listeChainee)
            {
                Console.WriteLine(item);
            }
        }

        private static void Test01()
        {
            Homme homme = new Homme();
            homme.Manger();
            ((Icarnivore)homme).Manger();
            ((IFrugivore)homme).Manger();
        }
    }

    public interface Icarnivore
    {
        void Manger();
    }

    public interface IFrugivore
    {
        void Manger();
    }

    public class Homme : Icarnivore, IFrugivore
    {
        public void Manger()
        {
            Console.WriteLine("Je mange");
        }

        // implementations explicites
        void Icarnivore.Manger()
        {
            Console.WriteLine("Je mande de la viande");
        }

        void IFrugivore.Manger()
        {
            Console.WriteLine("Je mande des fruits");
        }
    }

    public class ListeChaineeEnumerator<T> : IEnumerator<T>
    {
        private Chainage<T> courant;
        private ListeChainee<T> listeChainee;

        public ListeChaineeEnumerator(ListeChainee<T> liste)
        {
            courant = null;
            listeChainee = liste;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (courant == null)
                courant = listeChainee.Premier;
            else
                courant = courant.Suivant;


            return courant != null;
        }

        public T Current
        {
            get
            {
                if (courant == null)
                    return default(T);
                return courant.Valeur;
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }


        public void Reset()
        {
            courant = null;
        }
    }

    public class ListeChainee<T> : IEnumerable<T>
    {

        #region TP precedent
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
            for (int i = 1; i < indice; i++)
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

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            return new ListeChaineeEnumerator<T>(this);
        }

    }

    public class Chainage<T>
    {
        public Chainage<T> Precedent { get; set; }
        public Chainage<T> Suivant { get; set; }
        public T Valeur { get; set; }
    }

}
