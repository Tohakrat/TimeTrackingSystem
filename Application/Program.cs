// See https://aka.ms/new-console-template for more information


using System;
using DataContracts;

namespace Business
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("please, log in.");

            }
            Console.WriteLine("Hello, World!");
            User u = new User("User1", "password1", AccessRole.User);
            u.Id = 0;
            Project p1 = new Project("TrackingSystem", DateTime.Now, 200);
            Console.WriteLine("Hello World!");
        }
    }
}