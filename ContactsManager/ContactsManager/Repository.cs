using System.Collections.Generic;

namespace ContactsManager
{
    public class Repository : IRepository //This could be a static class, but unlikely in a real life app
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

        public void DeleteAllContacts()
        {
            _contacts.Clear();
        }
    }
}
