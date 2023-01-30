using ConsoleTables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryManager.Book;
using static System.Net.WebRequestMethods;

namespace LibraryManager
{
    internal class Bill
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nip = "";
        public List<Book> books = new List<Book>();
        public string filePath = "";


        public Bill() 
        {
            Console.WriteLine("Proszę podać imię osoby wypożyczającej.");
            Name = Console.ReadLine();
            Console.WriteLine("Proszę podać nazwisko osoby wypożyczającej.");
            Surname = Console.ReadLine();
        }

        public void AddBook(int id)
        {
            Book wanted = Program.Catalog.GetBook(id);
            if (wanted.status == Book.BookStatus.Dostepna)
            {
                if (books.Contains(wanted))
                {
                    Console.WriteLine("Książka została już dodana do rachunku.");
                    Frontend.Wait();
                }
                else
                {
                    books.Add(wanted);
                }
            }
            else
            {
                Console.WriteLine("Książka nie jest dostępna.");
                Frontend.Wait();
            }
        }

        public void RemoveBook(int id)
        {
            Book book = Program.Catalog.GetBook(id);
            if (book == null)
            {
                Console.WriteLine("Nie ma takiej książki w rachunku.");
                Frontend.Wait();
            }
            else
            {
                books.Remove(book);
            }
        }

        public void ShowBill ()
        {
            Console.WriteLine("Imię: " + Name + "\nNazwisko: " + Surname);
            if (!String.IsNullOrEmpty(Nip))
            {
                Console.WriteLine("NIP: " + Nip);
            }
            Console.WriteLine("Suma: " + Convert.ToString(this.GetPrice()) + " PLN");

            Frontend.PrintBookTable(books);

            
        }

        public decimal GetPrice ()
        {
            decimal price = 0;
            foreach (var book in books)
            {
                price += book.price;           
            }

            return price;
        }

        public bool CloseBill()
        {
            this.ShowBill();
            Console.WriteLine("Czy na pewno chcesz chcesz zamknąć bieżący rachunek? (T/N)");
            string input = Console.ReadLine();
            if (input == "T" || input == "t")
            {
                foreach (var book in books)
                {
                    Book editBook = Program.Catalog.GetBook(book.id);
                    Book.BookStatus status = Book.BookStatus.Wypozyczona;
                    editBook.ChangeStatus(status);
                }
                    

                string text = this.Name + ";" + this.Surname + ";";
                foreach (var book in books)
                {
                    if (books.First() == book)
                    {
                        text = text + book.id;
                    }
                    else
                    {
                        text = text + " " + book.id;
                    }     
                }

                text = text + ";";

                if (this is Invoice)
                {
                    text = text + Nip + ";";
                }
                text = text + Convert.ToString(this.GetPrice()) + ";" + DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");


                try
                {
                    using (StreamWriter write = System.IO.File.AppendText(filePath))
                    {
                        
                        write.WriteLine(text);
                        write.Flush();
                        write.Close();
                    }
                }
                catch
                {
                    Console.WriteLine("Wystąpił błąd podczas zapisu do pliku");
                    Frontend.Wait();
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    internal class Receipt : Bill
    {
        public Receipt()
        {
            filePath = @"./data/receipts.csv";
        }

    }

    internal class Invoice : Bill
    {
        public Invoice ()
        {
            Console.WriteLine("Proszę podać NIP:");
            Nip = Console.ReadLine();
            filePath = @"./data/invoices.csv";

        }
    }
}
