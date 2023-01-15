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
            /*Book lotr = new Book("Lord of the Rings;Akcja, kosmici, elfy; 15; 1", "Przygodowa", 1);
            lotr.ShowData();*/

            Catalog Catalog = new Catalog(@"C:\Users\Kajetan\Desktop\Studia CDV\Programowanie obiektowe\11 stycznia\data\books.csv");
            Catalog.ShowCategories();
            // komentarz kajetana
        }
    }
}
