using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestAttributsEtReflexion
{
    class Program
    {
        static void Main(string[] args)
        {

            //Test01();

            //Test02();

            Test03();

            

            Console.ReadKey();
        }

        private static void Test03()
        {
            new DemoAttributs().Demo();
            /*
             Pas de description pour la classe Animal

            Description pour la classe Chien
                    Cette classe correspond à un chien
                    Elle dérive de la classe animal

             * */
        }

        private static void Test02()
        {
            Type type = typeof(string);
            Console.WriteLine(type.IsClass);

            foreach (MethodInfo infos in type.GetMethods())
            {
                Console.WriteLine(infos.Name);
            }
        }

        private static void Test01()
        {
            new Program().Affiche();
        }

        [Obsolete("Utilisez plutôt la méthode ToString() pour avoir une représentation de l'objet")]
        public void Affiche()
        {
            Console.WriteLine("methode obsolete");
        }
    }

    public  class DemoAttributs
    {
        public DemoAttributs()
        {
        }

        public void Demo()
        {
            Animal animal = new Animal();
            Chien chien = new Chien();

            VoirDescription(animal);
            VoirDescription(chien);

        }

        private void VoirDescription<T>(T obj)
        {
            Type type = typeof(T);
            if (!type.IsClass)
                return;
            Attribute[] lesAttributs = Attribute.GetCustomAttributes(type, typeof(DescriptionClasseAttribute));
            if (lesAttributs.Length == 0)
                Console.WriteLine("Pas de description pour la classe " + type.Name + "\n");
            else
            {
                Console.WriteLine("Description pour la classe " + type.Name);
                foreach (DescriptionClasseAttribute attribute in lesAttributs)
                {
                    Console.WriteLine("\t" + attribute.Description);
                }

            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true)]
    public class DescriptionClasseAttribute : Attribute
    {
        public string Description { get; set; }

        public DescriptionClasseAttribute()
        {
        }

        public DescriptionClasseAttribute(string description)
        {
            Description = description;
        }

    }

    [DescriptionClasse(Description = "Cette classe correspond à un chien")]
    [DescriptionClasse("Elle dérive de la classe animal")]
    public class Chien : Animal
    {
        public void Aboyer()
        {

        }
    }

    public class Animal
    {

    }
}
