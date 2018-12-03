using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMethodExtention
{
    class Program
    {

        static void Main(string[] args)
        {
            //Test01();
            //Test02();
            Test03();


            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();
        }

        private static void Test03()
        {
            Avion a = new Avion();
            Oiseau o = new Oiseau();
            a.Atterir();
            o.Atterir();
            /*
             J'attéris
            J'attéris
            Fin du test, tapez sur une touche pour sortir
             * */
        }





        private static void Test02()
        {
            // apres avoir ajouté this devant les métnodes de la classe statique
            string chaineNormale = "Bonjour à tous";
            string chainCryptee = chaineNormale.Crypte();
            Console.WriteLine("chaineNormale : " + chaineNormale);
            Console.WriteLine("chainCryptee : " + chainCryptee);

            string chaineDecrypte = chainCryptee.Decrypte();
            Console.WriteLine("chaineDecrypte :" + chaineDecrypte);
            /*
            chaineNormale : Bonjour à tous
            chainCryptee : Qm9uam91ciDgIHRvdXM=
            chaineDecrypte :Bonjour à tous
            Fin du test, tapez sur une touche pour sortir

             * */
        }

        private static void Test01()
        {
            string chaineNormale = "Bonjour à tous";
            string chainCryptee = Encodage.Crypte(chaineNormale);
            Console.WriteLine("chaineNormale : " + chaineNormale);
            Console.WriteLine("chainCryptee : " + chainCryptee);

            string chaineDecryptee = Encodage.Decrypte(chainCryptee);
            Console.WriteLine("chaineDecryptee : " + chaineDecryptee);
            /*
            chaineNormale : Bonjour à tous
            chainCryptee : Qm9uam91ciDgIHRvdXM=
            chaineDecryptee : Bonjour à tous
            Fin du test, tapez sur une touche pour sortir

             * */
        }
    }

    public static class Extensions
    {
        public static void Atterir(this IVolant volant)
        {
            Console.WriteLine("J'attéris");
        }
    }


    public interface IVolant
    {
        void Voler();
    }

    public class Oiseau : IVolant
    {
        public void Voler()
        {
            Console.WriteLine("Je vole");
        }
    }

    public class Avion : IVolant
    {
        public void Voler()
        {
            Console.WriteLine("Je vole");
        }
    }

    public static class Encodage
    {
        public static string Crypte(this string chaine)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(chaine));
        }

        public static string Decrypte(this string chaine)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(chaine));
        }
    }
}
