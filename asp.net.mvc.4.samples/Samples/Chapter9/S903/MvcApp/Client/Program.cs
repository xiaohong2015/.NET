using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("X-HTTP-Method-Override", "DELETE");
            Task.WaitAll(httpClient.GetAsync("http://localhost:3721/api/contacts").ContinueWith(response => Console.WriteLine("删除前：\n{0}\n", response.Result.Content.ReadAsStringAsync().Result)));

            Task.WaitAll(httpClient.PostAsync("http://localhost:3721/api/contacts/001",null));

            httpClient.GetAsync("http://localhost:3721/api/contacts").ContinueWith(response => Console.WriteLine("删除后：\n{0}\n", response.Result.Content.ReadAsStringAsync().Result));
            Console.Read();  
        }
    }
}
