// See https://aka.ms/new-console-template for more information


using System;
using DataContracts;
using Business;


namespace Application
{
    class Program
    {
        private static int UserDataId=-1;
        static void Main(string[] args)
        {         
            
            //IProxy Proxy = new ConsoleProxy(Request, Message, ChangeUser);
            IProxy Proxy = new ConsoleProxy();
            Proxy.SetCallBacks(Request, Message, ChangeUser);
            

            while (true)
            {
                Console.WriteLine(Proxy.GetOperations(UserDataId));
                int answer;
                if (int.TryParse(Console.ReadLine(),out answer)==false)
                {
                    Console.WriteLine("Wrong data.");
                    continue;
                };
                Proxy.DoAction(answer, UserDataId);
            }            
        }    
        static void Message(string m)
        {
            Console.WriteLine(m);
        }
        static string Request(string comment)
        {
            Console.WriteLine(comment);
            return (Console.ReadLine())??"no value";            
        }
        static void ChangeUser(Int32 userReceived)
        {
            UserDataId = userReceived;
        }
    }
}