using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;
using Infrastructure;

namespace Solution
{
    public class StubPopulater
    {        
        public StubPopulater ()
        {            
        }
        public void Seed()
        {
            Mediator MediatorObj = Mediator.GetMediator();
            MediatorObj.SubscribeInsert(InsertSubscriber);            
        }
        public void InsertSubscriber(Object Sender, UserStringEventArgs E)
        {
            Console.WriteLine("Populator: "+E.Info);
        }
    }
}
