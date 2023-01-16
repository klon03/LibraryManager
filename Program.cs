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
            Dictionary<string, string> actions = new Dictionary<string, string>();

            Book lotr = new Book("Lord of the Rings;Akcja, kosmici, elfy; 15; 1", "Przygodowa", 1);
            //lotr.ShowData();

            Catalog Catalog = new Catalog(@"C:\Users\Kajetan\Desktop\Studia CDV\Programowanie obiektowe\11 stycznia\data\books.csv");
            //Catalog.ShowCategories();
            
            Console.WriteLine("### Witaj w systemie LibraryManager 1.0 ###\n");
            while (true)
            {
                
                Console.WriteLine("Możliwe działania:\n1. Utwórz nowy rachunek");
                string menuInput = Console.ReadLine();

                if (menuInput == "1")
                {
                    Console.WriteLine("\nWybierz rodzaj rachunku:\n1. Paragon\n2. Faktura");
                    string billType = Console.ReadLine();

                    if (billType == "1") 
                    {
                        Receipt receipt = new Receipt();
                        Console.WriteLine(receipt.Name);
                    }
                }
            }
        }
    }
}
