using ConsoleTables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryManager
{
    internal class Catalog
    {
        private string[] tableHead;
        List<Category> CategoryList = new List<Category>();
        public List<Book> BookList = new List<Book>();

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
                            if (cat.name == values[3])
                            {
                                cat.addBook(newBook);
                            }
                        }
                    }
                    else
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
            reader.Close();
        }

        public void ShowCategories()
        {
            CategoryList.ForEach(p => Console.WriteLine("-" + p.name));
        }

        public void ShowBooks()
        {
            Frontend.PrintBookTable(BookList);
        }

        public void ShowBook(int id)
        {
            foreach (var book in BookList)
            {
                if (book.id == id)
                {
                    Console.WriteLine("ID: " + book.id + "\nTytuł: " + book.title + "\nKategoria: " + book.category + "\nCena: " +
                        book.price + " PLN\nStatus: " + book.status + "\nOpis:\n" + book.description);
                    break;
                }

            }
        }

        public void showCategory(string category)
        {
            bool flag = false;
            foreach (var cat in CategoryList)
            {
                if (cat.name == category)
                {
                    cat.ShowCategoryInfo();
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("Nie odnaleziono kategorii.");
            }
        }

        public void DeleteBook()
        {
            int delId;
            bool found = false;

            // Tego while'a i showBooks mozna w sumie usunac bo ma to robic front, ktory przekaze do metody id ksiazki i reszta podziala juz normalnie
            while (!found)
            {
                Console.Write("Podaj ID książki, którą chcesz usunąć: ");
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
                        // Usuwanie z kategorii
                        foreach (var cat in CategoryList)
                        {
                            if (cat.name == delCat)
                            {
                                cat.removeBook(book);
                                // Usuniecie kategorii jesli po usunieciu ksiazki lista pozostaje pusta
                                if (cat.books.Count == 0)
                                {
                                    CategoryList.Remove(cat);
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

            }

            UpdateLists();
            SaveToFile();
        }

        // Wyciąganie obiektu książki na zewnątrz
        public Book GetBook(int id)
        {
            foreach (var book in BookList)
            {
                if (book.id == id)
                {
                    return book;
                }
            }

            return null;
        }

        // Zapisywanie książek po edycji do pliku
        public void SaveToFile()
        {
            var newCsv = new StringBuilder();
            newCsv.Append("ID;Title;Description;Price;Status\n");
            foreach (var b in BookList)
            {
                newCsv.Append(b.exportBookData());
            }
            File.WriteAllText("./data/books.csv", newCsv.ToString());
        }

        public void EditBook (int id)
        {
            Book book = GetBook(id);

            Console.WriteLine("Podaj nowy tytuł (ENTER aby pozostawić " +book.title+"):");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) title = book.title;

            Console.WriteLine("Podaj nowy opis (ENTER aby pozostawić domyślny):");
            string description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description)) description = book.description;

            Console.WriteLine("Podaj nową kategorię (ENTER aby pozostawić " + book.category + "):");
            string category = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(category)) category = book.category;

            Console.WriteLine("Podaj nową cenę (ENTER aby pozostawić " + Convert.ToString(book.price) + "):");
            string price = Console.ReadLine();
            decimal priceDec = 0;
            
            if (string.IsNullOrWhiteSpace(price)) priceDec = book.price; else if (!decimal.TryParse(price, out _)) priceDec = Convert.ToDecimal(price);

            Console.WriteLine("Podaj nowy status (ENTER aby pozostawić " + Convert.ToString(book.status) + ", 1 - wypożycznona, 2 - dostępna):");
            string status = Console.ReadLine();
            Book.BookStatus statusFormatted = book.status;

            if (string.IsNullOrWhiteSpace(status))
            {
                statusFormatted = book.status;
            }
            else
            {
                if (status == "1")
                {
                    statusFormatted = Book.BookStatus.Wypozyczona;
                }
                else if (status == "2")
                {
                    statusFormatted = Book.BookStatus.Dostepna;
                }
            }
  

            book.EditBook(title, description, category, priceDec, statusFormatted);
            UpdateLists();
            SaveToFile();
        }

        public void AddNewBook()
        {
            string title = "", description = "", category = "", price = "";
            decimal priceDec = 0;
            
            Console.WriteLine("Podaj tytuł książki:");
            while (string.IsNullOrWhiteSpace(title))
            {
                title = Console.ReadLine();
            }

            Console.WriteLine("Podaj opis książki (może być pusty):");
            description = Console.ReadLine();

            Console.WriteLine("Podaj kategorię książki:");
            while (string.IsNullOrWhiteSpace(category))
            {
                category = Console.ReadLine();
            }

            bool flag = true;
            Console.WriteLine("Podaj cenę książki:");
            while (flag)
            {
                price = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(price) && decimal.TryParse(price, out _))
                {
                    flag = false;
                    priceDec = Convert.ToDecimal(price);
                }
                else { Console.WriteLine("Nieprawidłowa wartość"); }
            }

            Book newBook = new Book(BookList.Last().id+1, title, description, category, priceDec, Book.BookStatus.Dostepna);
            BookList.Add(newBook);

            UpdateLists();
            SaveToFile();
        }

        public void UpdateLists()
        {
            CategoryList.Clear();

            foreach (var book in BookList)
            {
                if (CategoryList.Any(item => item.name == book.category))
                {
                    foreach (var cat in CategoryList)
                    {
                        if (cat.name == book.category)
                        {
                            cat.addBook(book);
                        }
                    }
                }
                else
                {
                    CategoryList.Add(new Category(book.category));
                    CategoryList[CategoryList.Count - 1].addBook(book);
                }
            }
        }

        public void ChangeCategoryName(string from, string to)
        {
            foreach (var book in BookList)
            {
                if (book.category== from)
                {
                    book.category = to;
                }
            }
            UpdateLists();
            SaveToFile();
        }
    }
}
