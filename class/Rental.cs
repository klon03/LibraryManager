using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    internal class Rental
    {

        public int id { get; set; }
        public int userID { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public decimal price { get; set; }
        public BookStatus status { get; set; }
        

        public enum BookStatus
        {
            Dostepna,
            Wypozyczona,
        }


        public Rental(int id, string userId, string start, string end, decimal price, BookStatus status)
        {
            this.id = id;
            this.userID = userID;
            this.start = start;
            this.end = end;
            this.price = price;
            this.status = status;
        }

        public void ShowData()
        {
            Console.WriteLine($"Id: {id}");
            Console.WriteLine($"Uzytkownik: {userID}");
            Console.WriteLine($"Data wyp: {start}");
            Console.WriteLine($"Data zw: {end}");
            Console.WriteLine($"Cena wypozyczenia: {price}");
            Console.Write($"Status wyp: {status}")
        }

        public void EditBook(string title, string description, string category, decimal price, BookStatus status)
        {
            this.title = title;
            this.description = description;
            this.category = category;
            this.price = price;
            this.status = status;
        }
    }
}