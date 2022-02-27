using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Solution
{
    internal class StubPopulater
    {
        private Mediator MediatorObj;
        public StubPopulater (Mediator mediatorObj)
        {
            MediatorObj = mediatorObj;
        }
        internal void Seed()
        {
            MediatorObj.InsertUser(new User(0, "Vasia", "1234", AccessRole.User, "Vasily Ivanovich"));
            MediatorObj.InsertUser(new User(1, "Petia", "1234", AccessRole.User, "Petr Iosifovich"));
            MediatorObj.InsertUser(new User(2, "u", "u", AccessRole.User, "Uriy Nickolaevich"));
            MediatorObj.InsertUser(new User(3, "w", "w", AccessRole.User, "William"));
            MediatorObj.InsertUser(new User(4, "Vlad", "1234", AccessRole.Admin, "Vladimir Ilyich"));
            MediatorObj.InsertUser(new User(5, "a", "a", AccessRole.Admin, "Andrey Mihailovich"));
            MediatorObj.InsertUser(new User(6, "Vania", "1234", AccessRole.ProjectLeader, "Ivan Vladimirovich"));
            MediatorObj.InsertUser(new User(7, "p", "p", AccessRole.ProjectLeader, "Patrick"));
        }
    }
}
