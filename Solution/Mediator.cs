using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataContracts;

namespace Solution
{
    public class Mediator
    {
        private IRepository<User> RepositoryUser;
        private List<EventHandler<UserEventArgs>> ActionList= new();
        public Mediator()
        {
            Mapper Map= new Mapper();
            RepositoryUser = Map.GetUserRepository();
            StubPopulater Populater = new StubPopulater(this);
            Populater.Seed();
        }
        public void InsertUser(User user)
        {
            RepositoryUser.Insert(user);
            UserEventArgs e = new UserEventArgs("XmlUserAdded");
            OnUserInserted(e);
        }
        public void SubscribeInsert(EventHandler<UserEventArgs> actionToSubscribe)
        {
            UserInserted += actionToSubscribe;
            ActionList.Add(actionToSubscribe);
        }
        public void UnsubscribeInsert(EventHandler<UserEventArgs> actionToSubscribe)
        {
            UserInserted -= actionToSubscribe;
            ActionList.Remove(actionToSubscribe);
        }
        public void SubscribeDelete(EventHandler<UserEventArgs> actionToSubscribe)
        {
            UserDeleted += actionToSubscribe;
            ActionList.Add(actionToSubscribe);
        }
        public void UnsubscribeDelete(EventHandler<UserEventArgs> actionToSubscribe)
        {
            UserDeleted -= actionToSubscribe;
            ActionList.Remove(actionToSubscribe);
        }
        internal virtual void OnUserInserted(UserEventArgs e)
        {
            EventHandler<UserEventArgs> handler = UserInserted;
            handler?.Invoke(this, e);
        }
        private event EventHandler<UserEventArgs> UserInserted;
        private event EventHandler<UserEventArgs> UserDeleted;

    }
    
}
