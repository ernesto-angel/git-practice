using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ContactsManager
{
    public class Contact
    {
        #region Properties
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Gender { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public string FavoriteColor { get; protected set; }
        #endregion

        public static Contact Parse(string input, out char delimeter)
        {
            //For the sake of simplicity we'll code expecting "conventional" names 
            var regEx = new Regex("^[A-Za-z0-9']+([ ,\\|])");
            if (!regEx.IsMatch(input))
                throw new ArgumentOutOfRangeException(nameof(input));

            var matches = regEx.Match(input);
            delimeter = matches.Groups[1].Value[0];// ' ';//[0].Value[0];

            return Parse(input, delimeter);
        }

        public static Contact Parse(string input, char delimeter)
        {
            if (delimeter != ' ' && delimeter != '|' && delimeter != ',')
                throw new ArgumentOutOfRangeException(nameof(delimeter));

            var inputArray = input.Split(delimeter);
            if (inputArray.Length > 5)
                throw new FormatException("input is not in the correct format");

            //We'll keep it simple and go with ISO 8601 date format
            if (!DateTime.TryParseExact(inputArray[4], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                throw new FormatException("Invalid date of birth format.");

            return new Contact()
            {
                LastName = inputArray[0],
                FirstName = inputArray[1],
                Gender = inputArray[2],
                FavoriteColor = inputArray[3],
                DateOfBirth =  dob
            };
        }
    }
}
