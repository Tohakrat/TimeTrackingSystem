using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using DataContracts;

namespace Application
{
    /*
    internal class Menu
    {
        DataFacade Facade = new();
        User user = null;
        internal void Main()
        {
            Facade.UserServicesObj.UserLogined += MenuLoginedUser;
            Facade.UserServicesObj.AdminLogined += MenuLoginedAdmin;
            bool repeat = true;
            bool Logined = false;
            

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
                        //DataContracts.User CurrentUser;
                        Logined = Facade.UserServicesObj.LogIn(UserName, Password, out user);
                        //if (Logined == true)
                            //MenuLoginedUser();
                        //Facade.UserServicesObj.Get
                        break;
                    case 0:
                        repeat = false;
                        break;
                }
            }
        }

            internal void MenuLoginedUser()
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
                        Console.Write("Projects:");
                        
                        Console.WriteLine(Facade.ProjectServicesObj.GetProjectsString());
                        String ProjectName;
                        DateTime Date;
                        Double WorkingTime;
                        Console.WriteLine("Enter the Date. Format: ...");
                        if (DateTime.TryParse(Console.ReadLine(), out Date) == true)
                            Console.WriteLine("Data set successfully: {0}", Date.ToString());
                        else break;
                        Console.WriteLine("Enter the project name");
                        ProjectName = Console.ReadLine();
                        Console.WriteLine("Enter working time in hours:");
                        if (Double.TryParse(Console.ReadLine(), out WorkingTime) == true)
                            Console.WriteLine("Time set successfully: {0}", WorkingTime.ToString());

                        //Facade.UserServicesObj.Get
                        break;
                    case 2://show submitted time
                        //Facade.UserServicesObj.Get
                        break;
                    case 0:
                        repeat = false;
                        break;
                }
            }
            }
        internal void MenuLoginedAdmin()
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
                        Console.Write("Projects:");

                        Console.WriteLine(Facade.ProjectServicesObj.GetProjectsString());
                        String ProjectName;
                        DateTime Date;
                        Double WorkingTime;
                        Console.WriteLine("Enter the Date. Format: ...");
                        if (DateTime.TryParse(Console.ReadLine(), out Date) == true)
                            Console.WriteLine("Data set successfully: {0}", Date.ToString());
                        else break;
                        Console.WriteLine("Enter the project name");
                        ProjectName = Console.ReadLine();
                        Console.WriteLine("Enter working time in hours:");
                        if (Double.TryParse(Console.ReadLine(), out WorkingTime) == true)
                            Console.WriteLine("Time set successfully: {0}", WorkingTime.ToString());

                        //Facade.UserServicesObj.Get
                        break;
                    case 2://show submitted time
                        //Facade.UserServicesObj.Get
                        break;
                    case 0:
                        repeat = false;
                        break;
                }
            }
        }
    }*/
}
