using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.BusinessObjects
{
    public class ConsoleProxy1 : IProxy1
    {
        public DataFacade Facade { get; set; }
        public ConsoleProxy1(Func<String, String> Request, Action<String> Message, Action<UserData> SetUser)
        {
            Facade = new(Request, Message, SetUser);
        }

        public void DoAction(int answer, UserData user)
        {
            if (Facade.ProcessAnswer(answer, user) == false)
            {
                Facade.Delegates.MessageDelegate("\n denied operation! ");
                return;
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
