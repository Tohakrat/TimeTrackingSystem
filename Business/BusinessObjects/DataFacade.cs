using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using System.Runtime.CompilerServices;
using Infrastructure;

namespace Business
{
    internal delegate void OperationDelegate(UserData? UD=null);
    public sealed class DataFacade
    {
        internal Menu Menu; 
        internal DataFacadeDelegates Delegates = new DataFacadeDelegates();
        public UserServices UserServices;
        public ProjectServices ProjectServices;
        private static DataFacade _DataFacade;

        internal void PopulateData()
        {
            StubDataPopulater Populater = new StubDataPopulater();//UserServicesObj, ProjectServicesObj);
            UserServices.AddRange(Populater.Users);
            ProjectServices.AddRange(Populater.Projects);
            UserServices.AddRange(Populater.TimeTrackEntries);
            //Populater.Populate();
            
        }       
        private DataFacade()
        {
            UserServices = new UserServices();
            ProjectServices = new ProjectServices();
            //UserServicesObj.SetProjectServices(ProjectServicesObj);
            //ProjectServicesObj.SetUserServices(UserServicesObj);           
        }
        
        public static DataFacade Instance
        {
            get
            {
                if (_DataFacade == null)
                    _DataFacade = new DataFacade();
                return _DataFacade;
            }
        }

        internal void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<Int32> SetUser)
        {
            Delegates.RequestDelegate = Request;
            Delegates.MessageDelegate = Message;
            Delegates.ChangeUserDelegate = SetUser;
            UserServices.SetEventsDelegates();
            Menu = new();
        }
        internal bool ProcessAnswer(int answer, int user)
        {
            return Menu.ProcessAnswer(answer, user);                  
        }                           
                  
        internal void ReportActiveUsers(Int32 user )
        {
            Delegates.MessageDelegate(UserServices.ReportActiveUsers(ProjectServices.FindIdByName));
        }              
       
        public string GetOperations(Int32 userId)
        {
            return Menu.GetAvailableOperations(userId); 
        }           
        public void Quit(Int32 user )
        {
            Environment.Exit(0);
        }                     
    }
}
