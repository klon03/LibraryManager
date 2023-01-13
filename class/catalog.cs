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
        List<string> categories = new List<string>();
        public Catalog(string filePath)
        {
            StreamReader reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                List<string> listA = new List<string>();

                var lineHead = reader.ReadLine();
                tableHead = lineHead.Split(';');
                var categoryIndex = Array.IndexOf(tableHead, "Kategoria");

                while (!reader.EndOfStream)
                {

                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if (categories.Contains(values[categoryIndex]))
                    {
                        //dodawanie do kategorii
                    }
                    else
                    {
                        //tworzenie nowej kategorii
                        categories.Add(values[categoryIndex]);
                    }


                    /*foreach (var item in values)
                    {
                        listA.Add(item);
                    }
                    foreach (var coloumn1 in listA)
                    {
                        Console.WriteLine(coloumn1);
                    }*/
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
            categories.ForEach(p => Console.WriteLine("-" + p));
            Console.WriteLine("\n");
        }
    }
}
