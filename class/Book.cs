using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    internal class Book
    {
        private string category, title, description;
        private int id;
        private byte status;
        private double price;

        public Book(string row, string category, int id)
        {
            var values = row.Split(';');
            this.category = category;
            this.id = id;
            this.title = values[0];
            this.description = values[1];
            this.price = Convert.ToDouble(values[2]);
            this.status = Convert.ToByte(values[3]);

            IDictionary<int, string> numberNames = new Dictionary<int, string>();
            numberNames.Add(1, "One"); //adding a key/value using the Add() method
            numberNames.Add(2, "Two");
            numberNames.Add(3, "Three");

        }

        public void ShowData()
        {
            Console.WriteLine("ID: {0}", id);
            Console.WriteLine("Kategoria: {0}", category);
            Console.WriteLine("Tytuł: {0}", category);
        }
    }
}
