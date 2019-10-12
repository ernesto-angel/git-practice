using System;
using NUnit.Framework;

namespace ContactsManager.Tests
{
    [TestFixture]
    public class ContactParserTests
    {
        private string _lastName = "James";
        private string _firstName = "Jesse";
        private string _gender = "Male";
        private string _favColor = "Red";
        private DateTime _dob = new DateTime(1921, 11, 26);
 
        [Test]
        [TestCase(' ')]
        [TestCase('|')]
        [TestCase(',')]
        public void ParseDelimetedContact(char delimeter)
        {
            var input = string.Join(delimeter.ToString(), _lastName, _firstName, _gender, _favColor, _dob.ToString("yyyy-MM-dd"));

            Contact contact = null;
            char delimeterUsedByParser = 'X';
            Assert.DoesNotThrow(() => { contact = Contact.Parse(input, out delimeterUsedByParser); }, "Parse without providing delimeter");
            Assert.IsNotNull(contact);
            Assert.AreEqual(delimeter, delimeterUsedByParser);
            Assert.AreEqual(contact.LastName, _lastName, $"Last name matches. Delimeter: {delimeter}");
            Assert.AreEqual(contact.FirstName, _firstName, $"First name matches. Delimeter: {delimeter}");
            Assert.AreEqual(contact.Gender, _gender, $"Gender matches. Delimeter: {delimeter}");
            Assert.AreEqual(contact.FavoriteColor, _favColor, $"Favorite color matches. Delimeter: {delimeter}");
            Assert.AreEqual(contact.DateOfBirth.Date, _dob.Date, $"Date of birth matches. Delimeter: {delimeter}");


            contact = null;
            Assert.DoesNotThrow(() => { contact = Contact.Parse(input, delimeter); }, "Parse with specified delimater");
            Assert.IsNotNull(contact);
            Assert.AreEqual(contact.LastName, _lastName, $"Last name matches. Delimeter provided to parser: {delimeter}");
            Assert.AreEqual(contact.FirstName, _firstName, $"First name matches. Delimeter provided to parser: {delimeter}");
            Assert.AreEqual(contact.Gender, _gender, $"Gender matches. Delimeter provided to parser: {delimeter}");
            Assert.AreEqual(contact.FavoriteColor, _favColor, $"Favorite color matches. Delimeter provided to parser: {delimeter}");
            Assert.AreEqual(contact.DateOfBirth.Date, _dob.Date, $"Date of birth matches. Delimeter provided to parser: {delimeter}");
        }

        [Test]
        [TestCase(' ')]
        [TestCase('|')]
        [TestCase(',')]
        public void TryParseWithInvalidInput(char delimeter)
        {
            var input = string.Join(delimeter.ToString(), _lastName, _firstName, _gender, _favColor, _dob.ToString("yyyy-MM-dd"), "OH!NO!");

            Contact contact = null;
            char delimeterUsedByParser = 'X';
            Assert.Throws<FormatException>(() => { contact = Contact.Parse(input, out delimeterUsedByParser); }, "Parse without providing delimeter");

            contact = null;
            Assert.Throws<FormatException>(() => { contact = Contact.Parse(input, delimeter); }, "Parse with specified delimeter");
        }

        [Test]
        [TestCase(' ')]
        [TestCase('|')]
        [TestCase(',')]
        public void TryParseWithInvalidDelimeter(char delimeter)
        {
            var input = string.Join(delimeter.ToString(), _lastName, _firstName, _gender, _favColor, _dob.ToString("yyyy-MM-dd"));

            Assert.Throws<ArgumentOutOfRangeException>(() => Contact.Parse(input, '*'), "Parse with invalid delimeter");
        }

        [Test]
        [TestCase(' ')]
        [TestCase('|')]
        [TestCase(',')]
        public void TryParseWithInvalidDateOfBirthFormat(char delimeter)
        {
            var input = string.Join(delimeter.ToString(), _lastName, _firstName, _gender, _favColor, _dob.ToString("yy-MM-dd"));

            Assert.Throws<ArgumentOutOfRangeException>(() => Contact.Parse(input, delimeter), "Parse with invalid date format");
        }
    }
}
