using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    public class ConsoleProxy : IProxy
    {
        public DataFacade Facade { get; set; }
        //public ConsoleProxy(Func<String, String> Request, Action<String> Message)//, Action<UserData> SetUser)
        public ConsoleProxy()//, Action<UserData> SetUser)
            //public ConsoleProxy()
        {
            Facade = DataFacade.GetDataFacade();
            //Facade.Initialize(Request, Message, SetUser);            
            Facade.PopulateData(); 
            
        }

        public void DoAction(int answer, int user)
        {
            if (Facade.ProcessAnswer(answer, user) == false)
            {
                Facade.Delegates.MessageDelegate("\n denied operation! ");
                return;
            }
        }

        public string GetOperations(int userId)
        { 
            return Facade.GetOperations(userId);
        }
        public void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<Int32> SetUser)
        {
            Facade.SetCallBacks(Request, Message,SetUser);//, SetUser);
            //Facade.
        }

    }
}
