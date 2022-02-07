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
        public User DoAction(int answer, User user);
        public string GetOperations(User user);
        public DataFacade Facade { get; set; }
        

        
    }
    public class ConsoleProxy : IProxy
    {
        public DataFacade Facade { get; set; }
        public ConsoleProxy()
        {
            Facade = new();
        }
        public User DoAction(int answer, User user)
        {
            
            //Facade.DoAction(int answer, User user)

                switch (answer)
            {
                case 1://Login OPeration
                    string UserName, Password;
                    User TempUser=user;  //delete tempuser if possible!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    Console.WriteLine("Enter login:");
                    UserName = Console.ReadLine();
                    Console.WriteLine("Enter password:");
                    Password = Console.ReadLine();                    
                    bool Logined = Facade.UserServicesObj.LogIn(UserName, Password, out TempUser);
                    return TempUser;
                    //Console.WriteLine(Facade.GetOperations(TempUser));                    
                    //Facade.UserServicesObj.Get
                    break;
                case 2://LogOut OPeration
                    return Facade.UserServicesObj.LogOut(user);
                    break;
                case 3://Get projects
                    Facade.ProjectServicesObj.GetProjectsString();
                    return user;
                    break;
                case 4://SubmitTime
                    Facade.UserServicesObj.SubmitTime(user);
                    break;
                case 5://ViewSubmittedTime
                    Facade.ViewSubmittedTime(user);
                    break;



                    
                case 0://Quit
                    Environment.Exit(0);
                    break;
                    
            }
            return null;
        }

        public string GetOperations(User user)
        {
           // if (user==null)    return Facade.GetOperations(null);     else 
                return Facade.GetOperations(user);

        }
    }
}
