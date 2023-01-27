using System;
using System.Collections.Generic;

namespace LibraryManager
{
    internal class Category
    {
        private string name;
        List<Book> books = new List<Book>();
        public Category(string name)
        {
            this.name = name;
        }

        public void ShowCategoryInfo()
        {
            Console.WriteLine("Nazwa: {0}", name);
            Console.WriteLine("-------------------");
            int i = 1;
            foreach (var book in books)
            {
                Console.WriteLine("Ksiazka {0}: {1}", i, book);
                //Console.WriteLine(book.Value);
                i++;
            }

        }

        public void addBook(Book book)
        {
            books.Add(book);
        }

        public void removeBook(Book book)
        {
            books.Remove(book);
        }
    }
}