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
   
}
