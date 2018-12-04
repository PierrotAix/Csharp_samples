using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDeleguesEvenementsExpressionsLambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test01();
            //Test02();
            //Test03();
            //Test04(); // DemoTriMulticast
            //Test05();
            //Test06(); // TrieurDeTableauAction
            //Test07();
            //Test08();
            Test09();

            Console.WriteLine("Fin du test, tapez sur une touche pour sortir");
            Console.ReadKey();
        }

        private static void Test09()
        {
            new DemoEvenementEvent().Demo();
            // Le nouveau prix est de: 5000
            //Fin du test, tapez sur une touche pour sortir

        }

        public class DemoEvenementEvent
        {
            public void Demo()
            {
                VoitureEvent voiture = new VoitureEvent { Prix = 10000 };

                voiture.ChangementDePrix += voiture_ChangementDePrix;

                voiture.PromoSurLePrix();
            }

            private void voiture_ChangementDePrix(object sender, ChangementDePrixEventArgs e)
            {
                Console.WriteLine("Le nouveau prix est de  : " + e.Prix);
            }
        }


        private static void Test08()
        {
            new DemoEvenement().Demo();
            /*
             Le nouveau prix est de  : 5000
            Fin du test, tapez sur une touche pour sortir         
             * */
        }

        public class DemoEvenement
        {
            public void Demo()
            {
                Voiture voiture = new Voiture { Prix = 10000 };

                Voiture.DelegateDeChangementDePrix delegateDeChangementDePrix = voiture_ChangementDePrix;
                voiture.ChangementDePrix += delegateDeChangementDePrix;

                voiture.PromoSurLePrix();
            }

            private void voiture_ChangementDePrix(decimal nouveauPrix)
            {
                Console.WriteLine("Le nouveau prix est de  : " + nouveauPrix);
            }
        }

        private static void Test07()
        {
            new Operations().DemoOperations();
            //Division: 0,8
            //Puissance: 1024
            //Fin du test, tapez sur une touche pour sortir

        }

        private static void Test06()
        {
            int[] tableau = new int[] { 4, 1, 6, 10, 8, 5 };
            new TrieurDeTableauAction().DemoTri(tableau);
            /*
            1
            4
            5
            6
            8
            10

            10
            8
            6
            5
            4
            1
            Fin du test, tapez sur une touche pour sortir

             * */
        }

        private static void Test05()
        {
            int[] tableau = new int[] { 4, 1, 6, 10, 8, 5 };
            new TrieurDeTableau().DemoTriMulticastAnomyne(tableau);
            /*             
            1
            4
            5
            6
            8
            10
            10
            8
            6
            5
            4
            1
            Fin du test, tapez sur une touche pour sortir
            * 
             * */
        }

        private static void Test04()
        {
            // DemoTriMulticast
            int[] tableau = new int[] { 4, 1, 6, 10, 8, 5 };
            new TrieurDeTableau().DemoTriMulticast(tableau);
            /*
             1
            4
            5
            6
            8
            10
            10
            8
            6
            5
            4
            1
            Fin du test, tapez sur une touche pour sortir

             * */

        }

        private static void Test03()
        {
            int[] tableau = new int[] { 4, 1, 6, 10, 8, 5 };
            new TrieurDeTableau().DemoTriFactorise(tableau);
            /*
            1
            4
            5
            6
            8
            10
            10
            8
            6
            5
            4
            1
            Fin du test, tapez sur une touche pour sortir
             * */
        }

        private static void Test02()
        {
            int[] tableau = new int[] { 4, 1, 6, 10, 8, 5 };
            new TrieurDeTableau().DemoTriSimplifie(tableau);
            /*
            1
            4
            5
            6
            8
            10
            10
            8
            6
            5
            4
            1
            Fin du test, tapez sur une touche pour sortir

             * */
        }

        private static void Test01()
        {
            int[] tableau = new int[] { 4, 1, 6, 10, 8, 5};
            new TrieurDeTableau().DemoTri(tableau);
            /*
             * Tri ascendant
            1
            4
            5
            6
            8
            10
            Tri descendant
            10
            8
            6
            5
            4
            1
            Fin du test, tapez sur une touche pour sortir
             * */
        }
    }



    public class TrieurDeTableau
    {
        private delegate void DelegateTri(int[] tableau);

        private void TriAscendant(int[] tableau)
        {
            Array.Sort(tableau);
            foreach (int i in tableau)
            {
                Console.WriteLine(i);
            }
        }

        private void TriDescendant(int[] tableau)
        {
            Array.Sort(tableau);
            Array.Reverse(tableau);
            foreach (int i in tableau)
            {
                Console.WriteLine(i);
            }
        }

        public void DemoTri(int[] tableau)
        {
            DelegateTri tri = TriAscendant;
            tri(tableau);
            Console.WriteLine("Tri ascendant");
            foreach (int i in tableau)
            {
                Console.WriteLine(i);
            }

            tri = TriDescendant;
            tri(tableau);
            Console.WriteLine("Tri descendant");
            foreach (int i in tableau)
            {
                Console.WriteLine(i);
            }
        }

        private void TrierEtAfficher(int[] tableau, DelegateTri methodeDeTri)
        {
            methodeDeTri(tableau);
            foreach (int i in tableau)
            {
                Console.WriteLine(i);
            }
        }

        public void DemoTriSimplifie(int[] tableau)
        {
            TrierEtAfficher(tableau, TriAscendant);
            TrierEtAfficher(tableau, TriDescendant);
        }

        public void DemoTriFactorise(int[] tableau)
        {
            TrierEtAfficher(tableau, delegate (int[] leTableau)
                {
                    Array.Sort(leTableau);
                }
            );

            TrierEtAfficher(tableau, delegate (int[] leTableau)
                {
                    Array.Sort(leTableau);
                    Array.Reverse(leTableau);
                }
            );
        }

        public void DemoTriMulticast(int[] tableau)
        {
            DelegateTri tri = TriAscendant;
            tri += TriDescendant;
            tri(tableau);
        }

        public void DemoTriMulticastAnomyne(int[] tableau)
        {
            DelegateTri tri = delegate (int[] leTableau)
            {
                Array.Sort(leTableau);
                foreach (int i in tableau)
                {
                    Console.WriteLine(i);
                }
            };
            Console.WriteLine();
            tri += delegate (int[] leTableau)
            {
                Array.Sort(leTableau);
                Array.Reverse(leTableau);
                foreach (int i in tableau)
                {
                    Console.WriteLine(i);
                }
            };
            tri(tableau);
        }
    }

    public class TrieurDeTableauAction
    {
        private void TrierEtAfficher(int[] tableau, Action<int[]> methodeDeTri)
        {
            methodeDeTri(tableau);
            foreach (int i in tableau)
            {
                Console.WriteLine(i);
            }
        }

        public void DemoTri(int[] tableau)
        {
            //TrierEtAfficher(tableau, delegate (int[] leTableau)
            //{
            //    Array.Sort(leTableau);
            //});
            TrierEtAfficher(tableau, (leTableau) => Array.Sort(leTableau));
            Console.WriteLine();
            TrierEtAfficher(tableau, delegate (int[] leTableau)
            {
                Array.Sort(leTableau);
                Array.Reverse(leTableau);
            });
        }
    }

    public class Operations
    {
        public void DemoOperations()
        {
            //double division = Calcul(delegate (int a, int b)
            //{
            //    return (double)a / (double)b;
            //}, 4 , 5
            //);
            double division = Calcul((a, b) => (double)a / (double)b, 4, 5);

            //double puissance = Calcul(delegate (int a, int b)
            //{
            //    return Math.Pow((double)a, (double)b);
            //}, 4, 5
            //);
            double puissance = Calcul((a, b) => Math.Pow((double)a, (double)b), 4,5);

            Console.WriteLine("Division : "+ division);
            Console.WriteLine("Puissance : " + puissance);

        }

        private double Calcul(Func<int, int, double> methodeDeCalcul, int a, int b)
        {
            return methodeDeCalcul(a, b);
        }
    }


    public class Voiture
    {
        public delegate void DelegateDeChangementDePrix(decimal nouveauPrix);
        public event DelegateDeChangementDePrix ChangementDePrix;
        public decimal Prix { get; set; }

        public void PromoSurLePrix()
        {
            Prix = Prix / 2;
            if (ChangementDePrix != null)
                ChangementDePrix(Prix);
        }
    }

    public class ChangementDePrixEventArgs : EventArgs
    {
        public decimal Prix { get; set; }
    }

    public class VoitureEvent
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
}
