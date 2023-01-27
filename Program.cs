using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Catalog catalog = new Catalog(@"./data/books.csv");
            catalog.ShowCategories();
            catalog.ShowBooks();

            Console.ReadLine();
        }
    }
}
