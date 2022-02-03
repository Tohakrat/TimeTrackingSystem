﻿// See https://aka.ms/new-console-template for more information


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
            bool Logined = false;
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
                        string UserName, Password;
                        Console.WriteLine("Enter login:");
                        UserName = Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        Password = Console.ReadLine();
                        DataContracts.User CurrentUser;
                        Logined = Facade.UserServicesObj.LogIn(UserName, Password, out CurrentUser);
                        if (Logined == true)
                            MenuLoginedUser();

                        //Facade.UserServicesObj.Get
                        break;
                    case 0:
                        repeat=false;
                        break;
                }   

            }
            
        }
        static void MenuLoginedUser()
        {
            int KeyMenuLoginedUser;
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("1 - Add Time, 2- Submitted time, 3-Log Out,0- Quit");
                if (Int32.TryParse(Console.ReadLine(), out KeyMenuLoginedUser) == false)
                    continue;
                switch (KeyMenuLoginedUser)
                {
                    case 1:
                        

                        //Facade.UserServicesObj.Get
                        break;
                    case 2:


                        //Facade.UserServicesObj.Get
                        break;
                    case 0:
                        repeat = false;
                        break;
                }

            }
        }
    }
}