using System.Collections.Generic;

namespace ContactsManager
{
    public class Repository : IRepository
    {
        private static List<Contact> _contacts = new List<Contact>();

        public IEnumerable<Contact> GetAllContacts()
        {
            return _contacts;
        }

        public void SaveNewContact(Contact contact)
        {
            _contacts.Add(contact); //ToDo don't worry about duplicates for now
        }
    }
}
