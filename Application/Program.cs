// See https://aka.ms/new-console-template for more information


using System;
using DataContracts;
using Business;
using Solution;


namespace Application
{
    class Program
    {
        private static int _s_UserDataId=-1;
        static void Main(string[] args)
        {                     
            //IProxy Proxy = new ConsoleProxy(Request, Message, ChangeUser);
            IProxy Proxy = new ConsoleProxy();
            Proxy.SetCallBacks(Request, Message, ChangeUser);
            StubPopulater SP = new StubPopulater();
            SP.Seed();

            using (Mediator MediObj = Mediator.GetMediator())
            {
                MediObj.SubscribeInsert(InsertUserSubscriber);
                MediObj.SubscribeDelete(DeleteUserSubscriber);
                
                while (true)
                {
                    Console.WriteLine(Proxy.GetOperations(_s_UserDataId));
                    int answer;
                    if (int.TryParse(Console.ReadLine(), out answer) == false)
                    {
                        Console.WriteLine("Wrong data.");
                        continue;
                    };
                    Proxy.DoAction(answer, _s_UserDataId);
                }
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
            _s_UserDataId = userReceived;
        }
        static public void InsertUserSubscriber(Object Sender, UserStringEventArgs E)
        {
            Console.WriteLine("Program: " + E.Info);
        }
        static public void DeleteUserSubscriber(Object Sender, UserStringEventArgs E)
        {
            Console.WriteLine("Program: " + E.Info);
        }
    }
}