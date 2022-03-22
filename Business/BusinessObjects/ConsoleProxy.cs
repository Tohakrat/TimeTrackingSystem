using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ConsoleProxy : IProxy
    {
        //public DataFacade Facade { get; set; }
        
        public ConsoleProxy()
            
        {
            DataFacade.GetDataFacade().PopulateData();                      
        }

        public void DoAction(int answer, int user)
        {
            if (DataFacade.GetDataFacade().ProcessAnswer(answer, user) == false)
            {
                DataFacade.GetDataFacade().Delegates.MessageDelegate("\n denied operation! ");
                return;
            }
        }

        public string GetOperations(int userId)
        { 
            return DataFacade.GetDataFacade().GetOperations(userId);
        }
        public void SetCallBacks(Func<String, String> Request, Action<String> Message, Action<Int32> SetUser)
        {
            DataFacade.GetDataFacade().SetCallBacks(Request, Message,SetUser);            
        }
    }
}
