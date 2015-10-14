using System.Collections.Generic;
using System.Web.Http;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class ContactsController : ApiController
    {
        public IContactRepository Repository { get; private set; }

        public ContactsController(IContactRepository repository)
        {
            this.Repository = repository;
        }

        public IEnumerable<Contact> Get()
        {
            return this.Repository.GetAllContacts();
        }

        public Contact Get(string id)
        {
            return this.Repository.GetContact(id);
        }
    }
}
