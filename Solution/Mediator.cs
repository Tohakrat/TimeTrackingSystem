﻿using System;
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
        private List<EventHandler<UserEventArgs>> UserInsertedList = new();
        private List<EventHandler<UserEventArgs>> UserDeletedList = new();
        private bool _disposed = false;
        private AbstractRepositoryProvider _Provider;
        private event EventHandler<UserEventArgs> UserInserted
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
      
    
    private event EventHandler<UserEventArgs> UserDeleted;
        private static Mediator s_Mediator;
        private Mediator()
        {
            _Provider = new XmlRepositoryProvider();
            
            _RepositoryUser = _Provider.GetUserRepository();            
            _RepositoryProject = _Provider.GetProjectRepository();     
            _RepositoryTimeTrackEntry = _Provider.GetTimeTrackEntryRepository();     
            
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
            UserEventArgs e = new UserEventArgs(new String("UserAdded: "+user.GetFullName()) );
            OnUserInserted(e);
        }
        public void SubscribeInsert(EventHandler<UserEventArgs> actionToSubscribe)
        {            
            UserInserted += actionToSubscribe;
        }
        public void UnsubscribeInsert(EventHandler<UserEventArgs> actionToSubscribe)
        {
            UserInserted -= actionToSubscribe;            
        }
        public void SubscribeDelete(EventHandler<UserEventArgs> actionToSubscribe)
        {
            UserDeleted += actionToSubscribe;            
        }
        public void UnsubscribeDelete(EventHandler<UserEventArgs> actionToSubscribe)
        {
            UserDeleted -= actionToSubscribe;            
        }
        internal virtual void OnUserInserted(UserEventArgs e)
        {
            foreach (EventHandler<UserEventArgs>  Handler in UserInsertedList)
            {
                Handler?.Invoke(this, new UserEventArgs(e.Info));
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

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposed = true;
        }

    }
    
}
