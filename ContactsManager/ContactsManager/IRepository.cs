using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager
{
    public interface IRepository
    {
        IEnumerable<Contact> GetAllContacts();
        void SaveNewContact(Contact contact);
    }
}
