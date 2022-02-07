// See https://aka.ms/new-console-template for more information


using System;
using DataContracts;
using Business;

namespace Application
{
    class Program
    {
        private static User user1;
        static void Main(string[] args)
        {            
            User user = null;
            ConsoleProxy proxy = new ConsoleProxy();
            proxy.Facade.UserServicesObj.AdminLogined += Message;
            proxy.Facade.UserServicesObj.UserLogined += Message;
            proxy.Facade.UserServicesObj.ProjectLeaderLogined += Message;
            proxy.Facade.UserServicesObj.LoginFailed += Message;
            proxy.Facade.UserServicesObj.LogOutResult += Message;
            proxy.Facade.ProjectServicesObj.ProjectListTransmitted += Message;
            proxy.Facade.UserServicesObj.Request += Request;


            while (true)
            {
                Console.WriteLine(proxy.GetOperations(user));
                int answer;
                int.TryParse(Console.ReadLine(),out answer);
                user = proxy.DoAction(answer, user);
            }            
        }    
        static void Message(string m)
        {
            Console.WriteLine(m);
        }
        static string Request(string comment)
        {
            Console.WriteLine(comment);
            return (Console.ReadLine());            
        }
        static void ChangeUser(User user)
        {
            user1 = user;
        }
    }
}