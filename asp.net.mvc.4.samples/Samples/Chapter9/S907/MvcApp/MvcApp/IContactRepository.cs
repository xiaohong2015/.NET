using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApp.Models;

namespace MvcApp
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAllContacts();
        Contact GetContact(string id);
    }
}