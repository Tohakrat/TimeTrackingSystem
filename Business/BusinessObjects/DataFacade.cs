using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using System.Runtime.CompilerServices;


namespace Business
{
    internal delegate void OperationDelegate(UserData? UD=null);
    public sealed class DataFacade
    {
        internal Menu MenuObj; 
        internal DataFacadeDelegates Delegates = new DataFacadeDelegates();
        public UserServices UserServicesObj;
        public ProjectServices ProjectServicesObj;
        private static DataFacade DataFacadeObj;

        internal void PopulateData()
        {
            StubDataPopulater Populater = new StubDataPopulater(UserServicesObj, ProjectServicesObj);
            Populater.Populate();
        }      
 
        private DataFacade()
        {
        }
        public static DataFacade GetDataFacade()
        {
            if (DataFacadeObj == null)
                DataFacadeObj = new DataFacade();
            return DataFacadeObj;
        }
        internal void Initialize(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        {            
            Delegates.RequestDelegate = Request;
            Delegates.MessageDelegate = Message;
            Delegates.ChangeUserDelegate = SetUser;
            UserServicesObj = new UserServices(this);
            ProjectServicesObj = new ProjectServices(this);
            UserServicesObj.SetProjectServices(ProjectServicesObj);
            ProjectServicesObj.SetUserServices(UserServicesObj);            
            MenuObj = new(this);
        }

        internal void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        {
            Delegates.RequestDelegate = Request;
            Delegates.MessageDelegate = Message;
            Delegates.ChangeUserDelegate = SetUser;
        }
        internal bool ProcessAnswer(int answer, UserData user)
        {
            return MenuObj.ProcessAnswer(answer, user);                  
        }       

        internal void Login(UserData user)
        {
            UserServicesObj.Login(user);            
        }       

        internal void LogOut(UserData user)
        {
            UserServicesObj.LogOut(user, Delegates.ChangeUserDelegate);
        }        

        internal void SubmitTime(UserData user)
        {
            UserServicesObj.SubmitTime(user);            
        }       
        internal void ReportActiveUsers(UserData user=null)
        {
            Delegates.MessageDelegate(UserServicesObj.ReportActiveUsers(ProjectServicesObj.FindIdByName));
        }

        internal void AddUser(UserData user = null)
        {
            bool Result = UserServicesObj.Add();
        }

        internal void DeleteUser(UserData Me)
        {
            bool deleted = UserServicesObj.DeleteUser(Me);         
        }

        internal void AddProject(UserData user = null)
        {
            bool result = ProjectServicesObj.AddProject(GetUserIdByName);
        }
        internal void DeleteProject(UserData user = null)
        {
            ProjectServicesObj.DeleteProject(null);            
        }          
       
        internal int GetUserIdByName(string UserName,AccessRole role)
        {
            return UserServicesObj.GetUserIdByName(UserName,role);
        }
        public void ViewSubmittedTime(UserData user)
        {
            Delegates.MessageDelegate(UserServicesObj.ViewSubmittedTime(user,ProjectServicesObj.FindNameById));
        }
                
        public List<Project> GetProjectsOfUser(UserData user)
        {
            return null;
        }
        public void GetProjectsString(UserData user = null)
        {
            Delegates.MessageDelegate(ProjectServicesObj.GetProjectsString());
        }

        public string GetOperations(UserData user)
        {
            return MenuObj.GetAvailableOperations(user); 
        }
        public void ViewAllActiveUsers(UserData user = null)
        {
            Delegates.MessageDelegate(UserServicesObj.GetAllActiveUsers());
        }
        public void ViewAllUsers(UserData user = null)
        {
            Delegates.MessageDelegate(UserServicesObj.GetAllUsersString());
        }
        public void Quit(UserData user = null)
        {
            Environment.Exit(0);
        }                     
    }
}
