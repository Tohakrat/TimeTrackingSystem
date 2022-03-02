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
        public  void DoAction(int answer, int user);
        public  string GetOperations(int userId);
        public  DataFacade Facade { get; set; }
       
        //public  void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser);    
        public void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<Int32> SetUser);    
    }
   
}
