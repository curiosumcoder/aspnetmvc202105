using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEP.Model;

namespace CA1
{
    class Program
    {
        static void Main(string[] args)
        {
            MEP.Model.Estudiante e1 = new MEP.Model.Estudiante();
            e1.Id = 123;
            e1.Nombre = "Juan";
            Console.WriteLine($"{e1.Id} {e1.Nombre}");

            Estudiante e2 = new Estudiante() { Id = 456, Nombre = "María" };
            Console.WriteLine($"{e2.Id} - {e2.Nombre}");

            Console.WriteLine("Hello World!");
            Console.ReadLine();

            //var p = new CA1.Program();
        }
    }
}
