using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    internal class Bill
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nip = "";
        List<Book> books = new List<Book>();

        public Bill() 
        {
            Console.WriteLine("Proszę podać imię osoby wypożyczającej.");
            Name = Console.ReadLine();
            Console.WriteLine("Proszę podać nazwisko osoby wypożyczającej.");
            Surname = Console.ReadLine();
        }

        public void AddBook(string id)
        {
            books.Add(book);
        }
    }

    internal class Receipt : Bill
    {

    }

    internal class Invoice : Bill
    {
        public Invoice ()
        {
            Console.WriteLine("Proszę podać NIP.");
            Nip = Console.ReadLine();

        }

    }
}
