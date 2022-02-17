// See https://aka.ms/new-console-template for more information


using System;
using DataContracts;
using Business;

namespace Application
{
    class Program
    {
        private static UserData UserDataObj= null;
        static void Main(string[] args)
        {         
            
            IProxy Proxy = new ConsoleProxy(Request, Message, ChangeUser);
            Proxy.SetCallBacks(Request, Message, ChangeUser);
            

            while (true)
            {
                Console.WriteLine(Proxy.GetOperations(UserDataObj));
                int answer;
                if (int.TryParse(Console.ReadLine(),out answer)==false)
                {
                    Console.WriteLine("Wrong data.");
                    continue;
                };
                Proxy.DoAction(answer, UserDataObj);
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
        static void ChangeUser(UserData userReceived)
        {
            UserDataObj = userReceived;
        }
    }
}