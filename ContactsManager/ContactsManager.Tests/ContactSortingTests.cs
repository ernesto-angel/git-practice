using System;
using NUnit.Framework;
using System.Linq;

namespace ContactsManager.Tests
{
    [TestFixture]
    public class ContactSortingTests
    {
        private IRepository _repo = null;
        private Contact _johnDoe = null;
        private Contact _janeDoe = null;
        private Contact _jillDavis = null;

        [SetUp]
        public void InitializeRepo()
        {
            _repo = new Repository();
            _janeDoe = null;
            _johnDoe = null;
            _jillDavis = null;
        }

        [Test]
        public void ConactsOrderedByGenderThenLastName()
        {
            var allContacts = SetUpContacts();

            var contactsByGenderThenName = allContacts.OrderContactsByGenderThenLastName().ToArray();
            Assert.AreEqual(contactsByGenderThenName[0], _jillDavis, "First contact should be Jill Davis");
            Assert.AreEqual(contactsByGenderThenName[1], _janeDoe, "Second contact should be Jane Doe");
            Assert.AreEqual(contactsByGenderThenName[2], _johnDoe, "Third contact should be John Doe");
        }

        [Test]
        public void ConactsOrderedByDateOfBirthAscending()
        {
            var allContacts = SetUpContacts();

            var contactsByDob = allContacts.OrderContactsByDateOfBirthAscending().ToArray();
            Assert.AreEqual(contactsByDob[0], _jillDavis, "First contact should be Jill Davis");
            Assert.AreEqual(contactsByDob[1], _janeDoe, "Second contact should be Jane Doe");
            Assert.AreEqual(contactsByDob[2], _johnDoe, "Third contact should be John Doe");
        }

        [Test]
        public void ContactsOrderedByLastNameDescending()
        {
            var allContacts = SetUpContacts();

            var contactsByDob = allContacts.OrderContactsByLastNameDescending().ToArray();
            Assert.AreEqual(contactsByDob[0], _johnDoe, "First contact should be John Doe");
            Assert.AreEqual(contactsByDob[1], _janeDoe, "Second contact should be Jane Doe");
            Assert.AreEqual(contactsByDob[2], _jillDavis, "Third contact should be Jill DDavis");
        }

        private Contact[] SetUpContacts()
        {
            _johnDoe = Contact.Parse("Doe John Male Red 1991-01-31");
            Assert.IsTrue(_johnDoe.LastName == "Doe" && _johnDoe.FirstName == "John" && _johnDoe.Gender == "Male" && _johnDoe.FavoriteColor == "Red" && _johnDoe.DateOfBirth.Date == new DateTime(1991, 1, 31).Date, "Ensure correct values for John Doe");
            _johnDoe.Save(_repo);

            _janeDoe = Contact.Parse("Doe Jane Female Pink 1993-12-31");
            Assert.IsTrue(_janeDoe.LastName == "Doe" && _janeDoe.FirstName == "Jane" && _janeDoe.Gender == "Female" && _janeDoe.FavoriteColor == "Pink" && _janeDoe.DateOfBirth.Date == new DateTime(1993, 12, 31).Date, "Ensure correct values for Jane Doe");
            _janeDoe.Save(_repo);

            _jillDavis = Contact.Parse("Davis Jill Female Violet 2016-07-04");
            Assert.IsTrue(_jillDavis.LastName == "Davis" && _jillDavis.FirstName == "Jill" && _jillDavis.Gender == "Female" && _jillDavis.FavoriteColor == "Violet" && _jillDavis.DateOfBirth.Date == new DateTime(2016, 07, 04).Date, "Ensure correct values for Jill Davis");
            _jillDavis.Save(_repo);

            var allContacts = _repo.GetAllContacts().ToArray();

            Assert.AreEqual(allContacts[0], _johnDoe, "First contact should be John Doe");
            Assert.AreEqual(allContacts[1], _janeDoe, "Second contact should be Jane Doe");
            Assert.AreEqual(allContacts[2], _jillDavis, "Third contact should be Jill Davis");

            return allContacts;
        }
    }
}
