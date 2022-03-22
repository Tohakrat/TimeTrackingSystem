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
        internal Menu MenuObj; 
        internal DataFacadeDelegates Delegates = new DataFacadeDelegates();
        public UserServices UserServicesObj;
        public ProjectServices ProjectServicesObj;
        private static DataFacade _s_DataFacadeObj;

        internal void PopulateData()
        {
            StubDataPopulater Populater = new StubDataPopulater();//UserServicesObj, ProjectServicesObj);
            Populater.Populate();
        }       
        private DataFacade()
        {
            UserServicesObj = new UserServices();
            ProjectServicesObj = new ProjectServices();
            //UserServicesObj.SetProjectServices(ProjectServicesObj);
            //ProjectServicesObj.SetUserServices(UserServicesObj);           
        }
        public static DataFacade GetDataFacade()
        {
            if (_s_DataFacadeObj == null)
                _s_DataFacadeObj = new DataFacade();
            return _s_DataFacadeObj;
        }
       
        internal void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<Int32> SetUser)
        {
            Delegates.RequestDelegate = Request;
            Delegates.MessageDelegate = Message;
            Delegates.ChangeUserDelegate = SetUser;
            UserServicesObj.SetEventsDelegates();
            MenuObj = new();
        }
        internal bool ProcessAnswer(int answer, int user)
        {
            return MenuObj.ProcessAnswer(answer, user);                  
        }                           
                  
        internal void ReportActiveUsers(Int32 user )
        {
            Delegates.MessageDelegate(UserServicesObj.ReportActiveUsers(ProjectServicesObj.FindIdByName));
        }              
       
        public string GetOperations(Int32 userId)
        {
            return MenuObj.GetAvailableOperations(userId); 
        }           
        public void Quit(Int32 user )
        {
            Environment.Exit(0);
        }                     
    }
}
