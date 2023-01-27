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
            Catalog Catalog = new Catalog(@"./data/books.csv");
            Catalog.ShowCategories();
            Console.WriteLine("ABC");
            Console.ReadLine();
        }
    }
}
