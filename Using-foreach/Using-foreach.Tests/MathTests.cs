using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Using_foreach.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        public void Factorielle_AvecValeur3_Retourne6()
        {
            // test à faire
            int valeur = 3;
            int resultat = Math.Factorielle(valeur);
            Assert.AreEqual(6, resultat);
        }

        [TestMethod]
        public void Factorielle_AvecValeur10_Retourne1()
        {
            int valeur = 10;
            int resultat = Math.Factorielle(valeur);
            Assert.AreEqual(1, resultat,"La valeur doit être égale à 1");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ToInt32_AvecChaineNonNumerique_LeveUneException()
        {
            Convert.ToInt32("abc");
        }

        [TestMethod]
        public void ObtenirLaMeteoDuJour_AvecUnBouchon_RetourneSoleil()
        {

            Meteo fausseMeteo = new Meteo { Temps = Temps.Soleil, Temperature = 25 };
            IDal fausseDal = Mock.Of<IDal>();
            Mock.Get(fausseDal).Setup(dal => dal.ObtenirLaMeteoDuJour()).Returns(fausseMeteo);

            Meteo meteoDuJour = fausseDal.ObtenirLaMeteoDuJour();
            Assert.AreEqual(25, meteoDuJour.Temperature);
            Assert.AreEqual(Temps.Soleil, meteoDuJour.Temps);
        }

        [TestMethod]
        public void Generateur_AvecUnBouchon_Retourne5()
        {
            IGenerateur generateur = Mock.Of<IGenerateur>();
            Mock.Get(generateur).SetupGet(x => x.Valeur).Returns(5);

            Assert.AreEqual(5, generateur.Valeur);
        }

    }

    public static class Math
    {
        public static int Factorielle(int a)
        {
            if (a <= 1)
                return 1;
            return a * Factorielle(a - 1);
        }
    }

    public class Dal : IDal
    {
        public Meteo ObtenirLaMeteoDuJour()
        {
            // on va bouchonner ici:
            throw new NotImplementedException();

        }
    }

    public interface IDal
    {
        Meteo ObtenirLaMeteoDuJour();
    }

    public class Meteo
    {
        public double Temperature { get; set; }
        public Temps Temps { get; set; }
    }

    public enum Temps
    {
        Soleil,
        Pluie
    }

    public interface IGenerateur
    {
        int Valeur { get; }
    }

    public class Generateur : IGenerateur
    {
        private Random r;
        public Generateur()
        {
            r = new Random();
        }

        public int Valeur
        {
            get
            {
                return r.Next(0, 100);
            }
        }
    }
}
