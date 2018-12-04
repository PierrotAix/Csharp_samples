using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGererLesException
{
    class Program
    {
        static void Main(string[] args)
        {

            //Test01();
            //Test02();
            //Test03();
            //Test04();
            Test05();

            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();
        }

        private static void Test05()
        {
            try
            {
                ChargerProduit("TV HD");
            }
            catch (ProduitNonEnStockException ex)
            {

                Console.WriteLine("Erreur : " + ex.Message);
            }
            /*
             * Erreur : Le produit TV HD n'est pas en stock
             Fin du test, tapez sur une touche pour sortir

             * */
        }

        private static Produit ChargerProduit(string nomProduit)
        {
            Produit produit = new Produit();

            if (produit.Stock <= 0)
                throw new ProduitNonEnStockException(nomProduit);
            return produit;
        }

        private static void Test04()
        {
            try
            {
                Methode1();
            }
            catch (NotImplementedException)
            {

                Console.WriteLine("On intercepte l'exception de la méthode 3");
            }
            /*
             * On intercepte l'exception de la méthode 3
             Fin du test, tapez sur une touche pour sortir

             * */
        }

        private static void Methode1()
        {
            Methode2();
        }

        private static void Methode2()
        {
            Methode3();
        }

        private static void Methode3()
        {
            throw new NotImplementedException();
        }

        private static void Test03()
        {
            try
            {
                double racine = RacineCarree(-5);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Impossible d'effectuer le calcul : " + ex.Message);
            }
            /*
             Impossible d'effectuer le calcul : Le paramètre doit ête positif
            Nom du paramètre : valeur
            Fin du test, tapez sur une touche pour sortir 
             * */
        }

        public static double RacineCarree(double valeur)
        {
            if (valeur <= 0)
                throw new ArgumentOutOfRangeException("valeur", "Le paramètre doit ête positif");
            return Math.Sqrt(valeur);
        }

        private static void Test02()
        {
            try
            {
                Voiture v = null;
                v.Prix = 10000;
            }
            catch (FormatException ex)
            {

                Console.WriteLine("Erreur de format : " + ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Erreur de référence nulle : " + ex);
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Erreur système autres que FormatException et NullReferenceException" + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Toutes les autres exceptions : " + ex);
            }
        }

        private static void Test01()
        {
            string chaine = "dix";
            try
            {
                int valeur = Convert.ToInt32(chaine);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Une erreur s'est produite dans la tentative de conversion, plus d'information ci-dessous:");
                Console.WriteLine();
                Console.WriteLine("Message d'erreur : " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Pile d'apel : " + ex.StackTrace);
                Console.WriteLine();
                Console.WriteLine("Type de l'exception : " + ex.GetType());
                /*
                 * Une erreur s'est produite dans la tentative de conversion, plus d'information ci
                -dessous:

                Message d'erreur : Le format de la chaîne d'entrée est incorrect.

                Pile d'apel :    à System.Number.StringToNumber(String str, NumberStyles options
                , NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
                   à System.Number.ParseInt32(String s, NumberStyles style, NumberFormatInfo inf
                o)
                   à System.Convert.ToInt32(String value)
                   à TestGererLesException.Program.Test01() dans D:\BITBUCKET\demo\Csharp_sample
                _Using-foreach\Using-foreach\TestGererLesException\Program.cs:ligne 25

                Type de l'exception : System.FormatException
                Fin du test, tapez sur une touche pour sortir

                 * */

            }
        }
    }

    internal class Produit
    {
        public int Stock { get; set; }
    }

    public class ChangementDePrixEventArgs : EventArgs
    {
        public decimal Prix { get; set; }
    }


    public class Voiture
    {
        public event EventHandler<ChangementDePrixEventArgs> ChangementDePrix;
        public decimal Prix { get; set; }

        public void PromoSurLePrix()
        {
            Prix = Prix / 2;
            if (ChangementDePrix != null)
                ChangementDePrix(this, new ChangementDePrixEventArgs { Prix = Prix });
        }
    }

    public class ProduitNonEnStockException : Exception
    {
        public ProduitNonEnStockException(string nomProduit) 
            : base("Le produit " + nomProduit + " n'est pas en stock")
        {

        }
    }
}
