using System;
using System.Globalization;
using System.Linq;

namespace ContactsManager
{
    public class Contact
    {
        #region Properties
        public string FirstName { get; protected internal set; }
        public string LastName { get; protected internal set; }
        public string Gender { get; protected internal set; }
        public DateTime DateOfBirth { get; protected internal set; }
        public string FavoriteColor { get; protected internal set; }
        #endregion

        protected Contact() { }

        private static string[] _validDelimeters = new string[] { " | ", ", ", " " };

        public static Contact Parse(string input)
        {
            return Parse(input, out string delimeter);
        }

        public static Contact Parse(string input, out string delimeter)
        {
            //For the sake of simplicity we'll code expecting "conventional" names 
            if (input.Contains("|"))
                delimeter = " | ";
            else if (input.Contains(","))
                delimeter = ", ";
            else if (input.Contains(" "))
                delimeter = " ";
            else
                throw new ArgumentOutOfRangeException(nameof(input));

            return Parse(input, delimeter);
        }

        public static Contact Parse(string input, string delimeter)
        {
            if (!_validDelimeters.Contains(delimeter))
                throw new ArgumentOutOfRangeException(nameof(delimeter));

            var inputArray = input.Split(new[] { delimeter }, StringSplitOptions.None);
            if (inputArray.Length != 5)
                throw new FormatException("input is not in the correct format");

            //We'll keep it simple and go with ISO 8601 date format
            if (!DateTime.TryParseExact(inputArray[4].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                throw new FormatException("Invalid date of birth format.");

            return new Contact()
            {
                LastName = inputArray[0].Trim(),
                FirstName = inputArray[1].Trim(),
                Gender = inputArray[2].Trim(),
                FavoriteColor = inputArray[3].Trim(),
                DateOfBirth =  dob
            };
        }

        public void Save(IRepository repo)
        {
            //ToDo some validation?
            repo.SaveNewContact(this);
        }

        public override string ToString()
        {
            return $"{FirstName.PadRight(15)} {LastName.PadRight(20)} {Gender.PadRight(10)} {FavoriteColor.PadRight(15)} {DateOfBirth.ToString("M/d/yyyy")}";
        }

    }
}
