using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPSimulateurMeteo
{
    class Program
    {


        static void Main(string[] args)
        {

            Console.WriteLine("---------------------------------------------------");

            DemoMeteo maDemoMeteo =  new DemoMeteo();
            maDemoMeteo.AfficheStatistique();


            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();
        }
    }

    public class DemoMeteo
    {
        public static string LeTempsQuilFaisait = "soleil";

        private int nbChangementDeTemps = 0;

        private int nbBeauTemps = 0;

        private int nbEssais = 1000;

        public DemoMeteo()
        {
            for (int i = 0; i < nbEssais; i++)
            {
                Meteo maMeteo = new Meteo();
                
                // abonnement a l'évènement du beau temps
                maMeteo.IlFaitBeau += meteo_IlFaitBeau;

                maMeteo.LeTempsAChange += meteo_LeTempsAChange;

                maMeteo.MeteoDuJour();

            }
        }

        private void meteo_LeTempsAChange(object sender, LeTempsAChangeEventArgs e)
        {
            Console.WriteLine("Le temps a changé"); // Bizare quand on commente cette phrase ça ne marche plus
            nbChangementDeTemps++;
        }

        public void AfficheStatistique()
        {
            Console.WriteLine($"Le temps a changé {nbChangementDeTemps} fois, soit un pourcentage de {nbChangementDeTemps*100/nbEssais} %  ");
            Console.WriteLine($"Il a fait beau {nbBeauTemps} fois, soit un pourcentage de {nbBeauTemps * 100 / nbEssais} %  ");

        }

        private void meteo_IlFaitBeau(object sender, IlFaitBeauEventArgs e)
        {
            Console.WriteLine("Il fait beau");
            nbBeauTemps++;
        }
    }

    
    public class IlFaitBeauEventArgs : EventArgs
    {
    }

    public class LeTempsAChangeEventArgs : EventArgs
    {
    }

    public class Meteo
    {
        public string TempsQuilFait { get; private set; }

        public event EventHandler<IlFaitBeauEventArgs> IlFaitBeau;

        public event EventHandler<LeTempsAChangeEventArgs> LeTempsAChange;

        public void MeteoDuJour()
        {
            Random rand = new Random();
            int tirage = rand.Next(1, 101);

            if (tirage <= 5) // normalement 5
            {
                TempsQuilFait = "soleil";
                // On lève un évènement
                if (IlFaitBeau != null)
                    IlFaitBeau(this, new IlFaitBeauEventArgs() );
            }
            if (5 < tirage && tirage <=50) // normalement 5
                TempsQuilFait = "nuage";
            if (50 < tirage && tirage <= 90)
                TempsQuilFait = "pluie";
            if (90 < tirage )
                TempsQuilFait = "orage";

            // Le temps a-t-il-changé ?
            if (TempsQuilFait != DemoMeteo.LeTempsQuilFaisait)
            {
                // On lève un évènement
                if (LeTempsAChange != null)
                    LeTempsAChange(this, new LeTempsAChangeEventArgs());
            }
        }
    }
}
