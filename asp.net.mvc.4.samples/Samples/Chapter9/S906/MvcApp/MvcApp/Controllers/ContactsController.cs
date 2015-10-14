using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    public class ContactsController : ApiController
    {
        [Dependency]
        public IContactRepository Repository { get; set; }

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
