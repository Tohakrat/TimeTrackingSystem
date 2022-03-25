using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ConsoleProxy : IProxy
    {
                
        public ConsoleProxy()
            
        {
            DataFacade.Instance.PopulateData();                      
        }

        public void DoAction(int answer, int user)
        {
            if (DataFacade.Instance.ProcessAnswer(answer, user) == false)
            {
                DataFacade.Instance.Delegates.MessageDelegate("\n denied operation! ");
                return;
            }
        }

        public string GetOperations(int userId)
        { 
            return DataFacade.Instance.GetOperations(userId);
        }
        public void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<Int32> SetUser)
        {
            DataFacade.Instance.SetCallBacks(Request, Message,SetUser);            
        }
    }
}
