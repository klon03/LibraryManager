using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    internal class Frontend
    {
        Dictionary<string, string[]> actions = new Dictionary<string, string[]>();
        public string input { get; set; }

        public Frontend()
        {
            actions.Add("newReceipt", new string[] { "Utwórz nowy rachunek.", "0" });
            
        }

        public void ShowActions()
        {
            Console.Clear();
            Console.WriteLine("### LibraryManager 1.0 ###\n");
            int i = 1;
            foreach (KeyValuePair<string, string[]> entry in actions)
            {
                // do something with entry.Value or entry.Key
                Console.WriteLine(i.ToString() +". " + entry.Value[0]);
                actions[entry.Key][1] = i.ToString();
                i++;
            }
        }

        public void AddAction(string key, string text)
        {
            actions.Add(key, new string[] { text, "0" });
        }

        public void RemoveAction (string key)
        {
            actions.Remove(key);
        }



        public void Execute()
        {
            Dictionary<string, string[]> staticActions = new Dictionary<string, string[]>(actions);
            foreach (KeyValuePair<string, string[]> entry in staticActions)
            {
                if (actions.ContainsKey(entry.Key))
                {
                    if (input == entry.Value[1])
                    {
                        if(entry.Key == "newReceipt")
                        {
                            Console.WriteLine("\nWybierz rodzaj rachunku:\n1. Paragon\n2. Faktura");
                            string billType = Console.ReadLine();

                            if (billType == "1")
                            {
                                Receipt bill = new Receipt();
                                
                            }
                            else if (billType == "2")
                            {
                                Invoice bill = new Invoice();
                            }
                            else { Console.WriteLine("Niepoprawne dane."); }

                            this.RemoveAction(entry.Key);
                            this.AddAction("endReceipt", "Zamknij rachunek.");
                            this.AddAction("addReceipt", "Dodaj pozycję do rachunku.");
                        }
                    }
                }
            }
                
        }
        
        
    }
}
