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
            Dictionary<string, string> actions = new Dictionary<string, string>();

            //Book lotr = new Book("Lord of the Rings;Akcja, kosmici, elfy; 15; 1", "Przygodowa", 1);
            //lotr.ShowData();

            Catalog = new Catalog(@"data\books.csv");
            //Catalog.ShowCategories();


            Frontend ui = new Frontend();

            while (true)
            {

                ui.ShowActions();
                ui.input = Console.ReadLine();
                Console.WriteLine("");
                ui.Execute();
            }
        }
    }
}
