using ConsoleTables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{

    internal class Frontend
    {
        List<string[]> actions = new List<string[]>();
        public string input { get; set; }
        public static Bill bill = null;

        public Frontend()
        {
            AddAction("newReceipt", "Utwórz nowy rachunek.");
            AddAction("showCategories", "Wyświetl wszystkie kategorie.");
            AddAction("showBooks", "Wyświetl wszystkie książki.");
            AddAction("showBook", "Wyświetl dane książki.");

        }

        public void ShowActions()
        {
            Console.Clear();
            Console.WriteLine("### LibraryManager 1.0 ###\n");
            int i = 1;
            actions.Sort((x, y) =>
            {
                if (!x[0].Contains("Receipt") && !y[0].Contains("Receipt"))
                {
                    return 1;
                }
                else if (!x[0].Contains("Receipt") && y[0].Contains("Receipt"))
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });

            bool flag = false;

            foreach (var item in actions.Select((value, index) => new { value, index }))
            {
                Console.WriteLine(i.ToString() + ". " + item.value[1]);

                if (actions.Count != item.index && flag == false && actions[item.index + 1][0].Contains("Receipt"))
                {
                    Console.WriteLine("==================================");
                    flag = true;
                }

                item.value[2] = i.ToString();
                i++;
            }
        }

        public void AddAction(string key, string text)
        {
            actions.Add(new string[] { key, text, "0" });
        }

        public void RemoveAction(string key)
        {
            string[] itemToRemove = { };
            foreach (string[] action in this.actions)
            {
                if (action[0] == key)
                {
                    itemToRemove = action;
                }
            }
            actions.Remove(itemToRemove);
        }

        public static void Wait()
        {
            Console.WriteLine("\nNaciśnij dowolny przycisk aby kontynuować...");
            Console.ReadKey();
        }

        public static void PrintBookTable(List<Book> List)
        {
            var table = new ConsoleTable("ID", "Tytuł", "Opis", "Kategoria", "Cena", "Status");
            foreach (var x in List)
            {
                var formattedDescription = x.description.Length > 30 ? x.description.Substring(0, 30) + "..." : x.description;
                table.AddRow(x.id, x.title, formattedDescription, x.category, x.price, x.status);
            }

            table.Write(Format.Alternative);
        }

        public List<string> GetActionsInputs()
        {
            List<string> keys = new List<string>();
            foreach (string[] action in actions)
            {
                keys.Add(action[2]);
            }
            return keys;
        }



        public void Execute()
        {
            List<string[]> staticActions = new List<string[]>(actions);
            foreach (string[] action in staticActions)
            {
                if (GetActionsInputs().Contains(input))
                {
                    if (input == action[2])
                    {
                        if (action[0] == "newReceipt")
                        {
                            Console.WriteLine("Wybierz rodzaj rachunku:\n1. Paragon\n2. Faktura");
                            string billType = Console.ReadLine();

                            if (billType == "1")
                            {
                                bill = new Receipt();

                            }
                            else if (billType == "2")
                            {
                                bill = new Invoice();
                            }
                            else { Console.WriteLine("Niepoprawne dane."); }

                            RemoveAction("newReceipt");
                            AddAction("endReceipt", "Zamknij rachunek.");
                            AddAction("cancelReceipt", "Anuluj rachunek.");
                            AddAction("showReceipt", "Wyświetl rachunek.");
                            AddAction("addReceipt", "Dodaj pozycję do rachunku.");
                            AddAction("deleteBookReceipt", "Usuń pozycję z rachunku.");
                        }

                        if (action[0] == "addReceipt")
                        {
                            Console.WriteLine("Podaj ID pozycji dodawanej do rachunku:");
                            string addId = Console.ReadLine();
                            if (int.TryParse(addId, out _)) bill.AddBook(Convert.ToInt32(addId));
                        }

                        if (action[0] == "showReceipt")
                        {
                            bill.ShowBill();
                            Wait();
                        }

                        if (action[0] == "deleteBookReceipt")
                        {
                            Console.WriteLine("Podaj ID pozycji usuwanej z rachunku:");
                            string deleteId = Console.ReadLine();
                            if (int.TryParse(deleteId, out _)) bill.RemoveBook(Convert.ToInt32(deleteId));
                        }

                        if (action[0] == "cancelReceipt")
                        {
                            Console.WriteLine("Czy na pewno chcesz chcesz anulować bieżący rachunek? (T/N)");
                            string input = Console.ReadLine();
                            if (input == "T" || input == "t")
                            {
                                bill = null;
                                RemoveAction("endReceipt");
                                RemoveAction("cancelReceipt");
                                RemoveAction("addReceipt");
                                RemoveAction("showReceipt");
                                RemoveAction("deleteBookReceipt");
                                AddAction("newReceipt", "Utwórz nowy rachunek.");
                            }
                        }

                        if (action[0] == "endReceipt")
                        {
                            if (bill.CloseBill())
                            {
                                bill = null;
                                RemoveAction("endReceipt");
                                RemoveAction("cancelReceipt");
                                RemoveAction("addReceipt");
                                RemoveAction("showReceipt");
                                RemoveAction("deleteBookReceipt");
                                AddAction("newReceipt", "Utwórz nowy rachunek.");
                            }
                        }

                        if (action[0] == "showCategories")
                        {
                            Console.WriteLine("Wszystkie kategorie:");
                            Program.Catalog.ShowCategories();
                            Wait();
                        }

                        if (action[0] == "showBooks")
                        {
                            Console.WriteLine("\nWszystkie książki w bilbiotece");
                            Program.Catalog.ShowBooks();
                            Wait();
                        }

                        if (action[0] == "showBook")
                        {
                            Console.WriteLine("Podaj ID książki:");
                            string bookId = Console.ReadLine();

                            if (int.TryParse(bookId, out _)) Program.Catalog.ShowBook(Convert.ToInt32(bookId));
                            Wait();
                        }

                    }
                }
            }
        }
    }
}
