// See https://aka.ms/new-console-template for more information


using System;
using DataContracts;
using Business;

namespace Application
{
    class Program
    {
        private static User UserObj= null;
        static void Main(string[] args)
        {            
            //User user = null;
            IProxy Proxy = new ConsoleProxy();
            Proxy.SetCallBacks(Request, Message, ChangeUser);

            Proxy.Facade.UserServicesObj.AdminLogined += Message;
            Proxy.Facade.UserServicesObj.UserLogined += Message;
            Proxy.Facade.UserServicesObj.ProjectLeaderLogined += Message;
            Proxy.Facade.UserServicesObj.LoginFailed += Message;
            Proxy.Facade.UserServicesObj.LogOutResult += Message;
            Proxy.Facade.ProjectServicesObj.ProjectListTransmitted += Message;
            Proxy.Facade.UserServicesObj.Request += Request;


            while (true)
            {
                Console.WriteLine(Proxy.GetOperations(UserObj));
                int answer;
                int.TryParse(Console.ReadLine(),out answer);
                Proxy.DoAction(answer, UserObj);
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
        static void ChangeUser(User userReceived)
        {
            UserObj = userReceived;
        }
    }
}