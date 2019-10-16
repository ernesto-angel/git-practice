using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace ContactsManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            var repo = new Repository();
            ImportContacts(repo);
            var allContacts = repo.GetAllContacts();
            
            Console.WriteLine("----Contacts ordered by gender (females before males), then by last name ascending");
            DumpContacts(allContacts.OrderContactsByGenderThenLastName());
            Console.WriteLine();

            Console.WriteLine("----Contacts ordered by date of birth ascending"); //A bit ambiguous as to what an ascending date should be in this case
            DumpContacts(allContacts.OrderContactsByDateOfBirthAscending());
            Console.WriteLine();

            Console.WriteLine("----Contacts ordered by last name descending");
            DumpContacts(allContacts.OrderContactsByLastNameDescending());
            Console.WriteLine();

            Console.WriteLine("--------This is the end of the road pal.... Press any key to exit");
            Console.ReadKey();
        }

        private static void ImportContacts(IRepository repo)
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var indexOfBin = currentDirectory.LastIndexOf("\\bin");
            
            if (indexOfBin > 0)
            {
                currentDirectory = currentDirectory.Substring(0, indexOfBin);
            }

            Debug.WriteLine("Importing from location");
            Debug.WriteLine(currentDirectory);

            var delimeterTypes = new Dictionary<string, string> {
                {"comma",   ", "    },
                {"pipe",    " | "   },
                {"space",   " "     },
            };
            foreach (var delimeterType in delimeterTypes )
            {
                var fileName = Path.Combine(currentDirectory, $"contacts_{delimeterType.Key}_delimeted.txt");
                if (File.Exists(fileName))
                {
                    string delimeter = delimeterType.Value;
                    foreach (var line in File.ReadLines(fileName))
                    {
                        var contact = Contact.Parse(line, delimeter);
                        contact.Save(repo);
                    }
                }
            }
        }

        private static void DumpContacts(IEnumerable<Contact> contacts)
        {
            foreach(var contact in contacts)
            {
                Console.WriteLine(contact.ToString());
            }
        }
    }
}
