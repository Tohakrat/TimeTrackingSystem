// See https://aka.ms/new-console-template for more information


using System;
using DataContracts;
using Business;

namespace Business
{
    class Program
    {
        static void Main(string[] args)
        {
            bool repeat = true;
            DataFacade Facade = new();
            while (repeat)
            {
                int key = 0;
                Console.WriteLine("1 - log in. 0- Quit");
                if (Int32.TryParse(Console.ReadLine(), out key) == false)
                    continue; 
                switch (key)
                {
                    case 1:
                        string Login, Password;
                        Console.WriteLine("Enter login:");
                        Login = Console.ReadLine();
                        //Facade.UserServicesObj.Get
                        break;
                    case 0:
                        repeat=false;
                        break;
                }


                

                 

            }
            
        }
    }
}