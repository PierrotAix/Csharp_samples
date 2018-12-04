using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSimulateurMeteaoCorrection
{
    class Program
    {
        public struct Personne
        {
            private int age;
            public Personne(int agePersonne)
            {
                age = agePersonne;
            }
            public void AfficheAge()
            {
                Console.WriteLine(age);
            }
        }

        public static class Extensions
        {
            public static void Affiche(string s)
            {
                Console.WriteLine(s);
            }
        }

        //public static void Affiche (T objetGenerique)
        //{
        //    Console.WriteLine();
        //}
        static void Main(string[] args)
        {
            //int a = new int();
            //Console.WriteLine(a);
            //Personne nico = new Personne();
            //nico.AfficheAge();

            Affiche<int>(5);
            Affiche<string>("abc");

            //string s = "abc";
            //s.Affiche();

            //Action<double, double, double> puissance = (x, y) => Math.Pow(x, y);
            //Console.WriteLine(Calcul(puissance, 2,3));


            SimulateurMeteo simulateurMeteo = new SimulateurMeteo(1000);
            Statisticien statisticien = new Statisticien(simulateurMeteo);
            statisticien.DemarrerAnalyse();
            statisticien.AfficherRapport();

            statisticien.DemarrerAnalyse();
            statisticien.AfficherRapport();

            statisticien.DemarrerAnalyse();
            statisticien.AfficherRapport();

            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();
        }

        //private static double Calcul(Action<double, double, double> action, int v1, int v2)
        //{
        //    return action(v1, v2);
        //}

        private static void Affiche<T>(T objetGenerique)
        {
            Console.WriteLine(objetGenerique.ToString());
        }
    }
    public class SimulateurMeteo
    {
        private Temps? ancienTemps;
        private int nombreDeRepetitions;
        private Random random;

        public delegate void IlFaitBeauDelegate(Temps temps);
        //public event IlFaitBeauDelegate QuandLeTempsChange;
        public event Action<Temps> QuandLeTempsChange;

        public SimulateurMeteo(int nombre)
        {
            random = new Random();
            ancienTemps = null;
            nombreDeRepetitions = nombre;
        }

        public void Demarrer()
        {
            for (int i = 0; i < nombreDeRepetitions; i++)
            {
                int valeur = random.Next(0, 100);
                if (valeur < 5)
                    GererTemps(Temps.Soleil);
                else
                {
                    if (valeur < 50)
                        GererTemps(Temps.Nuage);
                    else
                    {
                        if (valeur < 90)
                            GererTemps(Temps.Pluie);
                        else
                            GererTemps(Temps.Orage);
                    }
                }
            }
        }

        private void GererTemps(Temps temps)
        {
            if (ancienTemps.HasValue && ancienTemps.Value != temps && QuandLeTempsChange != null)
                QuandLeTempsChange(temps);
            ancienTemps = temps;
        }
    }

    public enum Temps
    {
        Soleil,
        Nuage,
        Pluie,
        Orage
    }

    public class Statisticien
    {
        private SimulateurMeteo simulateurMeteo;
        private int nombreDeFoisOuLeTempsAChange;
        private int nombreDeFoisOuIlAFaitSoleil;

        public Statisticien(SimulateurMeteo simulateur)
        {
            simulateurMeteo = simulateur;
            nombreDeFoisOuLeTempsAChange = 0;
            nombreDeFoisOuIlAFaitSoleil = 0;
        }

        public void DemarrerAnalyse()
        {
            nombreDeFoisOuLeTempsAChange = 0;
            nombreDeFoisOuIlAFaitSoleil = 0;
            simulateurMeteo.QuandLeTempsChange += simulateurMeteo_QuandLeTempsChange;
            simulateurMeteo.Demarrer();
            simulateurMeteo.QuandLeTempsChange -= simulateurMeteo_QuandLeTempsChange;
        }

        public void AfficherRapport()
        {
            Console.WriteLine("Nombre de fois où le temps a changé : " + nombreDeFoisOuLeTempsAChange);
            Console.WriteLine("Nombre de fois où il a fait soleil: " + nombreDeFoisOuIlAFaitSoleil);
            Console.WriteLine("Pourcentage de beau temps : " + nombreDeFoisOuIlAFaitSoleil  * 100 / nombreDeFoisOuLeTempsAChange + " % ");
        }

        private void simulateurMeteo_QuandLeTempsChange(Temps temps)
        {
            if (temps == Temps.Soleil)
                nombreDeFoisOuIlAFaitSoleil++;
            nombreDeFoisOuLeTempsAChange++;
        }
    }
}
