using System;
using System.Collections.Generic;

namespace LibraryManager
{
    internal class Category
    {
        public string name;
        public List<Book> books = new List<Book>();
        public Category(string name)
        {
            this.name = name;
        }

        public void ShowCategoryInfo()
        {
            Console.WriteLine("Nazwa: {0}", name);
            Console.WriteLine("-------------------");
            foreach (var book in books)
            {
                Console.WriteLine(book.id + " " + book.title + " " + book.price + " " + book.status);
            }

        }

        public void addBook(Book book)
        {
            books.Add(book);
        }

        public void editName(string name)
        {
            this.name = name;
        }

        public void removeBook(Book book)
        {
            books.Remove(book);
        }


    }
}