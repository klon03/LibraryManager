using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    internal class Program
    {
        public static Catalog Catalog;
        static void Main(string[] args)
        {
            Catalog = new Catalog(@"data\books.csv");

            Frontend ui = new Frontend();
            while (true)
            {
                ui.ShowActions();
                Console.Write("\nWybór: ");
                ui.input = Console.ReadLine();
                Console.WriteLine("");
                ui.Execute();
            }
        }
    }
}
