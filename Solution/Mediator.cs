using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataContracts;

namespace Solution
{
    public class Mediator:IDisposable
    {
        //private List<EventCover> _RepositoryCoverEvent;
        private IRepository<User> _RepositoryUser;
        private IRepository<Project> _RepositoryProject;
        private IRepository<TimeTrackEntry> _RepositoryTimeTrackEntry;
        private List<EventHandler<UserStringEventArgs>> UserInsertedList = new();
        private List<EventHandler<UserStringEventArgs>> UserDeletedList = new();
        private bool _disposed = false;
        private AbstractRepositoryProvider _Provider;
        private BusinessConnector Connector; 
        private event EventHandler<UserStringEventArgs> UserInserted
        {
            add
            {
                UserInsertedList.Add(value);
            }
            remove
            {
                UserInsertedList.Remove(value);
            }
        }
      
    
        private event EventHandler<UserStringEventArgs> UserDeleted;
        private static Mediator s_Mediator;
        private Mediator()
        {
            _Provider = new XmlRepositoryProvider();
            
            _RepositoryUser = _Provider.GetUserRepository();            
            _RepositoryProject = _Provider.GetProjectRepository();     
            _RepositoryTimeTrackEntry = _Provider.GetTimeTrackEntryRepository();
            Connector = new BusinessConnector((Repositories.Xml.XmlObjectRepository<User>)_RepositoryUser);
            
        }
        ~Mediator() => Dispose(false);
        public static Mediator GetMediator()
        {
            if (s_Mediator == null)
                s_Mediator = new Mediator();
            return s_Mediator;
        }
        public void InsertUser(User user)
        {
            _RepositoryUser.Insert(user);
            UserStringEventArgs e = new UserStringEventArgs(new String("UserAdded: "+user.GetFullName()) );
            OnUserInserted(e);
        }
        public void SubscribeInsert(EventHandler<UserStringEventArgs> actionToSubscribe)
        {            
            UserInserted += actionToSubscribe;
        }
        public void UnsubscribeInsert(EventHandler<UserStringEventArgs> actionToSubscribe)
        {
            UserInserted -= actionToSubscribe;            
        }
        public void SubscribeDelete(EventHandler<UserStringEventArgs> actionToSubscribe)
        {
            UserDeleted += actionToSubscribe;            
        }
        public void UnsubscribeDelete(EventHandler<UserStringEventArgs> actionToSubscribe)
        {
            UserDeleted -= actionToSubscribe;            
        }
        internal virtual void OnUserInserted(UserStringEventArgs e)
        {
            foreach (EventHandler<UserStringEventArgs>  Handler in UserInsertedList)
            {
                Handler?.Invoke(this, new UserStringEventArgs(e.Info));
            }            
        }
        public void Dispose()
        {            
            Dispose(true);            
            //GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool itIsSafeToAlsoFreeManagedObjects)
        {
            if (_disposed)
            {
                return;
            }

            if (itIsSafeToAlsoFreeManagedObjects)
            {
                UserInsertedList.Clear();
                UserDeletedList.Clear();
                // TODO: dispose managed state (managed objects).
            }            

            _disposed = true;
        }
    }    
}
