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
        public  void DoAction(int answer, UserData user);
        public  string GetOperations(UserData user);
        public  DataFacade Facade { get; set; }
        public  void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser);
        //public  IProxy(Func<String, String> Request, Action<String> Message, Action<User> SetUser);

    }
    public class ConsoleProxy : IProxy
    {
        public DataFacade Facade { get; set; }
        public ConsoleProxy(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        {
            Facade = new(Request,Message,SetUser);
        }
        
        public void DoAction(int answer, UserData user)
        {
            if (Facade.CheckAnswer(answer, user) == false)
            {
                Facade.Delegates.MessageDelegate("\n denied operation! ");
                return;
            }
           switch (answer)
            {
                
                case 1:
                    Facade.Login(user);           
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
                case 7://Add User
                    Facade.AddUser();
                    break;
                case 8://Add User
                    Facade.DeleteUser(user);
                    break;
                case 9://Add Project
                    Facade.AddProject();
                    break;
                case 10://Delete Project
                    Facade.DeleteProject();
                    break;             
                case 11://View Users
                    Facade.ViewAllUsers();
                    break;
                case 12://Report. Get users in project with min submitted time
                    Facade.ReportActiveUsers();
                    break;
                case 13://Report. Get all Users who is active
                    Facade.ViewAllActiveUsers();
                    break;



                case 0://Quit
                    Environment.Exit(0);
                    break;
                    
            }
            
        }

        public string GetOperations(UserData user)
        {
           // if (user==null)    return Facade.GetOperations(null);     else 
                return Facade.GetOperations(user);

        }
        public void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        {
            Facade.SetCallBacks(Request, Message, SetUser);
        }
    }
}
