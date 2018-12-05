using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test01();

            //Test02();

            //Test03(); // exemple de DictionarySectionhandler

            //Test04(); // Exemple de SingleTagSectionHandler

            //Test05(); // exemple de groupe de section

            //Test06(); // exemple de section de configuration personnalisée

            Test07(); //  exemple de section de configuration personnalisée avec une collection

            Console.ReadKey();
        }

        private static void Test07()
        {
            ListeClientSection section = (ListeClientSection)ConfigurationManager.GetSection("ListeClientSection");
            foreach (ClientElement clientElement in section.Listes)
            {
                Console.WriteLine(clientElement.Prenom + " a " + clientElement.Age + " ans ");
            }
            /*
            Nicolas a 30 ans
            Jérémie a 20 ans
             * */
        }

        private static void Test06()
        {
            PersonneSection section = (PersonneSection)ConfigurationManager.GetSection("PersonneSection");
            Console.WriteLine(section.Prenom + " a " + section.Age + " ans ");
            //nico a 30 ans

        }

        private static void Test05()
        {
            Hashtable section1 = (Hashtable)ConfigurationManager.GetSection("Utilisateur/ParametreConnexion");
            Hashtable section2 = (Hashtable)ConfigurationManager.GetSection("Utilisateur/InfoPersos");

            foreach (DictionaryEntry d in section1)
            {
                Console.WriteLine("Attribut : " + d.Key + " / Valeur : " + d.Value);
            }
            foreach (DictionaryEntry d in section2)
            {
                Console.WriteLine("Attribut : " + d.Key + " / Valeur : " + d.Value);
            }
            /*
             Attribut : Login / Valeur : Nico
            Attribut : MotDePasse / Valeur : 12345
            Attribut : Mode / Valeur : Authentification Locale
            Attribut : age / Valeur : 30
            Attribut : prenom / Valeur : Nicolas
             * */

        }

        private static void Test04()
        {
            // Exemple de SingleTagSectionHandler
            Hashtable section = (Hashtable)ConfigurationManager.GetSection("MonUtilisateur");
            foreach (DictionaryEntry d in section)
            {
                Console.WriteLine("Attribut : " + d.Key + " / Valeur : " + d.Value);
            }
            /*
             Attribut : prenom / Valeur : Nico
            Attribut : age / Valeur : 30
            Attribut : adresse / Valeur : 9 rue des bois
             * */

        }

        private static void Test03()
        {
            // exemple de DictionarySectionhandler
            Hashtable section = (Hashtable)ConfigurationManager.GetSection("InformationsUtilisateur");

            Console.WriteLine(" section[\"login\"] : " + section["login"]);
            Console.WriteLine(" section[\"MOTDEPASSE\"] : " + section["MOTDEPASSE"]);
            Console.WriteLine(" section[\"age\"] : " + section["age"]);
            /*
             section["login"] : nico
             section["MOTDEPASSE"] : 12345
             section["age"] : 30             
             * */

            foreach (DictionaryEntry d in section)
            {
                Console.WriteLine("Clé : " + d.Key + " / Valeur : " + d.Value);
            }
            /**
                Clé : login / Valeur : nico
                Clé : age / Valeur : 30
                Clé : motdepasse / Valeur : 12345
             * */

        }

        private static void Test02()
        {
            foreach (ConnectionStringSettings valeur in ConfigurationManager.ConnectionStrings)
            {
                Console.WriteLine(" valeur.ConnectionsString : " + valeur.ConnectionString);
            }
            /*
              valeur.ConnectionsString : data source=.\SQLEXPRESS;Integrated Security=SSPI;At
            tachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true
                valeur.ConnectionsString : Data Source=.\SQLEXPRESS; Initial Catalog=Base1; Int
            egrated Security=true
                valeur.ConnectionsString : Data Source=.\SQLEXPRESS; Initial Catalog=Base2; Int
            egrated Security=true
             * */
        }

        private static void Test01()
        {
            Console.WriteLine("Lecture depuis le fichier App.config.xml");
            string prenom = ConfigurationManager.AppSettings["prenom"];
            string age = ConfigurationManager.AppSettings["age"];
            Console.WriteLine("Prénom : " + prenom + " age : " + age);

            // boucle
            foreach (string cle in ConfigurationManager.AppSettings)
            {
                Console.WriteLine("Clé : " + cle + "  valeur : " + ConfigurationManager.AppSettings[cle]);
            }

            // test de la méthode d'extension
            int ageEntier = ConfigurationManager.AppSettings.ObtenirValeurEntiere("age");
            Console.WriteLine("AgeEntier : " + " valeur : " + ageEntier);

            /*
             * Lecture depuis le fichier App.config.xml
                Prénom : nicolas age : 30
                Clé : prenom  valeur : nicolas
                Clé : age  valeur : 30
                AgeEntier :  valeur : 30
             * */
        }
    }

    public static class ConfigurationManagerExtensions
    {
        public static int ObtenirValeurEntiere(this NameValueCollection appSettings, string cle)
        {
            string valeur = appSettings[cle];
            return Convert.ToInt32(valeur);
        }
    }
    /// <summary>
    /// Test06
    /// </summary>
    public class PersonneSection : ConfigurationSection
    {
        [ConfigurationProperty("age" , IsRequired =true)]
        public int Age
        {
            get { return (int)this["age"]; }
            set { this["age"] = value;  }
        }
        [ConfigurationProperty("prenom", IsRequired = true)]
        public string Prenom
        {
            get { return (string)this["prenom"]; }
            set { this["prenom"] = value; }
        }
    }

    /// <summary>
    /// Test07
    /// </summary>
    public class ClientElement : ConfigurationElement
    {
        private static readonly ConfigurationPropertyCollection _proprietes;
        private static readonly ConfigurationProperty age;
        private static readonly ConfigurationProperty prenom;

        static ClientElement()
        {
            prenom = new ConfigurationProperty("prenom", typeof(string), null, ConfigurationPropertyOptions.IsKey);
            age = new ConfigurationProperty("age", typeof(int), null, ConfigurationPropertyOptions.IsRequired);
            _proprietes = new ConfigurationPropertyCollection { prenom, age };
        }

        public string Prenom
        {
            get { return (string)this["prenom"]; }
            set { this["prenom"] = value; }
        }

        public int Age
        {
            get { return (int)this["age"]; }
            set { this["age"] = value; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return _proprietes; }
        }
    }
    /// <summary>
    /// Test 07
    /// </summary>
    public class ClientElementCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }
        protected override string ElementName
        {
            get { return "Client"; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return new ConfigurationPropertyCollection(); }
        }

        public ClientElement this[int index]
        {
            get { return (ClientElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new ClientElement this[string nom]
        {
            get { return (ClientElement)BaseGet(nom); }
        }

        public void Add(ClientElement item)
        {
            BaseAdd(item);
        }

        public void Remove(ClientElement item)
        {
            BaseRemove(item);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Clear()
        {
            BaseClear();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ClientElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ClientElement)element).Prenom;
        }
    }

    /// <summary>
    /// Test07
    /// </summary>
    public class ListeClientSection : ConfigurationSection
    {
        private static readonly ConfigurationPropertyCollection proprietes;
        private static readonly ConfigurationProperty liste;

        static ListeClientSection()
        {
            liste = new ConfigurationProperty(string.Empty, typeof(ClientElementCollection), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection);
            proprietes = new ConfigurationPropertyCollection { liste };
        }

        public ClientElementCollection Listes
        {
            get { return (ClientElementCollection)base[liste]; }
        }

        public new ClientElement this[string nom]
        {
            get { return Listes[nom]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return proprietes; }
        }
    }

}
