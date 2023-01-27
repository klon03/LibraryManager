using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class Book
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public BookStatus status { get; set; }

        public enum BookStatus
        {
            Dostepna,
            Wypozyczona,
        }


        public Book(string title, int id, string description, decimal price, BookStatus status)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.price = price;
            this.status = status;
        }

        public void ShowData()
        {
            Console.WriteLine($"Id: {id}");
            Console.WriteLine($"Tytul: {title}");
            Console.WriteLine($"Opis: {description}");
            Console.WriteLine($"Cena: {price}");
            Console.WriteLine($"Status: {status}");
        }

        public void EditBook(string title, string description, decimal price, BookStatus status)
        {
            this.title = title;
            this.description = description;
            this.price = price;
            this.status = status;
        }
    }
}