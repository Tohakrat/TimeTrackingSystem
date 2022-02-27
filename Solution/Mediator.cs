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
        private List<Action<string>> ActionList= new();
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
        public void SubscribeToInsert(Object sender,UserEventArgs e)
        {
            UserInserted += action;
            ActionList.Add(action);
        }
        public void UnsubscribeInsert(Object sender, Action<string> action)
        {
            UserAdded -= action;
            ActionList.Remove(action);
        }
        internal virtual void OnUserInserted(UserEventArgs e)
        {
            EventHandler<UserEventArgs> handler = UserInserted;
            handler?.Invoke(this, e);
        }
        public event EventHandler<UserEventArgs> UserInserted;
        public event Action<string> UserDeleted;

    }
    
}
