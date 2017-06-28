using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Data;

namespace RegisterAdmin
{
    static class Program
    {
        static void Main(string[] args)
        {
            string firstName = Prompt("Please enter First Name:");
            string lastName = Prompt("Please enter Last Name:");
            string email = Prompt("Please enter Email Address:");
            string password = Prompt("Please enter Password:");

            AdminRepository repo = new AdminRepository(Properties.Settings.Default.ConStr);
            repo.AddUser(firstName, lastName, email, password);
            Console.WriteLine("Account created, press any key to exit");
            Console.ReadKey(true);
        }
        static string Prompt(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }
    }
}
