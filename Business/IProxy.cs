using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;


namespace Business
{
    public interface IProxy
    {
        public void DoAction(int answer, User user);
        public string GetOperations(User user);
        public DataFacade Facade { get; set; }
        public void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<User> SetUser);

    }
    public class ConsoleProxy : IProxy
    {
        public DataFacade Facade { get; set; }
        public ConsoleProxy()
        {
            Facade = new();
        }
        
        public void DoAction(int answer, User user)
        {
            
            //Facade.DoAction(int answer, User user)

                switch (answer)
            {
                case 1:
                    Facade.Login(user);
                    //Login OPeration
                    //string UserName, Password;
                   // User TempUser=user; 
                    //Console.WriteLine("Enter login:");
                    //UserName = Console.ReadLine();
                   // Console.WriteLine("Enter password:");
                   // Password = Console.ReadLine();                    
                   // bool Logined = Facade.UserServicesObj.LogIn(UserName, Password, out TempUser);
                   // return TempUser;
                    //Console.WriteLine(Facade.GetOperations(TempUser));                    
                    //Facade.UserServicesObj.Get
                    break;
                case 2://LogOut OPeration
                    Facade.LogOut(user);
                    break;
                case 3://Get projects
                    Facade.GetProjects();                    
                    break;
                case 4://SubmitTime
                    Facade.SubmitTime(user);
                    break;
                case 5://ViewSubmittedTime
                    Facade.ViewSubmittedTime(user);
                    break;



                    
                case 0://Quit
                    Environment.Exit(0);
                    break;
                    
            }
            
        }

        public string GetOperations(User user)
        {
           // if (user==null)    return Facade.GetOperations(null);     else 
                return Facade.GetOperations(user);

        }
        public void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<User> SetUser)
        {
            Facade.SetCallBacks(Request, Message, SetUser);
        }
    }
}
