using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    internal class Catalog
    {
        private string[] tableHead;
        List<Category> CategoryList = new List<Category>();
        List<Book> BookList = new List<Book>();

        public Catalog(string filePath)
        {
            StreamReader reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));


                var lineHead = reader.ReadLine();
                tableHead = lineHead.Split(';');
                var categoryIndex = Array.IndexOf(tableHead, "Kategoria");

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    // Tworze obiekt book i dodaje obiekt do ogolej listy z ksiazkami
                    Book newBook = new Book(Convert.ToInt32(values[0]), values[1], values[2], values[3], Convert.ToDecimal(values[4]), values[5] == "1" ? Book.BookStatus.Dostepna : Book.BookStatus.Wypozyczona);
                    BookList.Add(newBook);

                    // jeżeli lista z kategoriami zawiera już daną kategorię
                    if (CategoryList.Any(item => item.name == values[3]))
                    {
                        // przechodzenie po liscie i sprawdzanie do ktorej kategorii dodac ksiazke
                        foreach (var cat in CategoryList)
                        {
                            if (cat.name == values[3]) {
                                cat.addBook(newBook);
                            }
                        }
                    } else
                    {
                        // tworzenie nowej kategorii i dodawanie do niej ksiazki
                        CategoryList.Add(new Category(values[3]));
                        // Jesli utworze nowa kategoria to wiem ze ta do ktorej chce dodać ksiazke jest na koncu
                        CategoryList[CategoryList.Count - 1].addBook(newBook);
                    }
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }
        }

        public void ShowCategories()
        {
            Console.WriteLine("Kategorie w katalogu:");
            CategoryList.ForEach(p => Console.WriteLine("-" + p.name));
            Console.WriteLine("\n");
        }

        public void ShowBooks()
        {
            foreach(var book in BookList)
            {
                Console.WriteLine(book.id + " " + book.title);
            }
        }

        public void showCategory(string category)
        {
            foreach (var cat in CategoryList)
            {
                if (cat.name == category)
                {
                    cat.ShowCategoryInfo();
                    break;
                }
            }
        }

        public void DeleteBook()
        {
            int delId;
            bool found = false;

            this.ShowBooks();
            while (!found)
            {
                Console.Write("Podaj id książki, którą chcesz usunąć: ");
                delId = Convert.ToInt32(Console.ReadLine());
                string delCat;

                // Usuwanie z globalnej listy ksiazek
                foreach (var book in BookList)
                {
                    if (book.id == delId)
                    {
                        delCat = book.category;
                        BookList.Remove(book);
                        found = true;

                        foreach (var cat in CategoryList)
                        {
                            if (cat.name == delCat)
                            {
                                cat.removeBook(book);
                            }
                        }
                        break;
                    }
                }
                
            }
            

        }
    }
}
