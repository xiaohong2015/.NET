using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Common;
using System.Net.Http.Formatting;

namespace Client
{
class Program
{
    static void Main(string[] args)
    {
        Uri baseAddress = new Uri("http://ncs-sz-jinnan:3721");
        HttpClient httpClient = new HttpClient { BaseAddress = baseAddress };
        IEnumerable<Contact> contacts = httpClient.GetAsync("/api/contacts").Result.Content.ReadAsAsync<IEnumerable<Contact>>().Result;
        Console.WriteLine("当前联系人列表");
        ListContacts(contacts);

        Contact contact = new Contact { Id = "003", Name = "王五", EmailAddress = "wangwu@gmail.com", PhoneNo = "789" };
        Console.WriteLine("\n添加联系人003");
        httpClient.PutAsync<Contact>("/api/contacts", contact, new JsonMediaTypeFormatter()).Wait();
        contacts = httpClient.GetAsync("/api/contacts").Result.Content.ReadAsAsync<IEnumerable<Contact>>().Result;            
        ListContacts(contacts);

        contact = new Contact { Id = "003", Name = "王五", EmailAddress = "zhaoliu@outlook.com", PhoneNo = "987" };
        Console.WriteLine("\n修改联系人003");
        httpClient.PostAsync<Contact>("/api/contacts", contact, new XmlMediaTypeFormatter()).Wait();
        contacts = httpClient.GetAsync("/api/contacts").Result.Content.ReadAsAsync<IEnumerable<Contact>>().Result;
        ListContacts(contacts);

        Console.WriteLine("\n删除联系人003");
        httpClient.DeleteAsync("/api/contacts/003").Wait();
        contacts = httpClient.GetAsync("/api/contacts").Result.Content.ReadAsAsync<IEnumerable<Contact>>().Result;
        ListContacts(contacts);

            
        Console.Read();
    }

    private static void ListContacts(IEnumerable<Contact> contacts)
    {
        foreach (Contact contact in contacts)
        {
            Console.WriteLine("{0, -6}{1, -6}{2, -20}{3, -10}", contact.Id, contact.Name, contact.EmailAddress, contact.PhoneNo);
        }
    }        
}
}