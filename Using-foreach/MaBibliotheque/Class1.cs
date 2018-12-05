using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaBibliotheque
{
    public class Bonjour
    {
        public void DireBonjour()
        {
            Console.WriteLine("Bonjour depuis la bibliothèque MaBibliotheque");
        }
    }

    public class Client
    {
        private string login;
        public string Login
        {
            get { return login; }
        }

        private string motDePasse;

        public string MotDePasse
        {
            get { return motDePasse.Crypte(); }           
        }

        public Client(string loginClient, string motDePasseClient)
        {
            login = loginClient;
            motDePasse = motDePasseClient;
        }

    }

    public static class Generateur
    {
        public static string ObtenirIdentifiantUnique()
        {
            Random r = new Random();
            string chaine = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                chaine += r.Next(0, 100);
            }
            return chaine.Crypte();
        }
    }

    internal static class Encodage
    {
        internal static string Crypte(this string chaine)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(chaine));
        }

        internal static string Decrypte(this string chaine)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(chaine));
        }
    }


}
