using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ContactsManager
{
    [RoutePrefix("records")]
    public class RecordsController : ApiController
    {
        private IRepository Repo = new Repository();


        /*Having trouble getting string param using [FromBody] with this architecture
         * Will resort to using a Dto for the sake of completing this exercise
         * 
        */
        [Route(""), HttpPost]
        public string Create(NewContactDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Input))
                return "No data provided";

            try
            {
                var contact = Contact.Parse(dto.Input);
                Repo.SaveNewContact(contact);
                return $"Contact created: {contact.FirstName} {contact.LastName}";
            }
            catch (Exception exc)
            {
                return $"Unable to create contact : {exc.Message}";
            }
        }

        [Route("gender"), HttpGet]
        public IEnumerable<Contact> ContactsByGender()
        {
            return Repo.GetAllContacts().OrderContactsByGenderThenLastName();
        }

        [Route("dateofbirth"), HttpGet]
        public IEnumerable<Contact> ContactsByDateOfBirth()
        {
            return Repo.GetAllContacts().OrderContactsByDateOfBirthAscending();
        }

        [Route("name"), HttpGet]
        public IEnumerable<Contact> ContactsByLastName()
        {
            return Repo.GetAllContacts().OrderContactsByLastNameDescending();
        }


        public class NewContactDto
        {
            public string Input { get; set; }
        }
    }
}
