using System.Collections.Generic;
using System.Linq;

namespace ContactsManager
{
    public static class Extensions
    {
        public static IOrderedEnumerable<Contact> OrderContactsByGenderThenLastName(this IEnumerable<Contact> contacts)
        {
            return contacts.OrderBy(x => x.Gender).ThenBy(x => x.LastName);
        }

        public static IOrderedEnumerable<Contact> OrderContactsByDateOfBirthAscending(this IEnumerable<Contact> contacts)
        {
            //A bit ambiguous as to what an ascending date should be in this case
            return contacts.OrderByDescending(x => x.DateOfBirth);
        }

        public static IOrderedEnumerable<Contact> OrderContactsByLastNameDescending(this IEnumerable<Contact> contacts)
        {
            return contacts.OrderByDescending(x => x.LastName);
        }
    }
}
