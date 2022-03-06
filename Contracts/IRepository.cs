using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContracts;

namespace Contracts
{
    public interface IRepository<T> 
    {
        
        public  IEnumerable<T> GetAll();
        public void Insert (T InsertedItem);
        internal event EventHandler<string> ObjectInserted;
        internal event EventHandler<string> ObjectDeleted;
    }
    public interface IUserRepository : IRepository<User>
    {
        //private List<T> RepoList = new List<T>();
        //public IEnumerable<User> GetAll();

        //public void Insert(User InsertedItem);        
    }
    public interface IProjectRepository : IRepository<Project>
    {
        //private List<T> RepoList = new List<T>();
        //public IEnumerable<Project> GetAll();

        //public void Insert(Project InsertedItem);
    }
    public interface ITimeTrackEntryRepository : IRepository<TimeTrackEntry>
    {
        //private List<T> RepoList = new List<T>();
        //public IEnumerable<TimeTrackEntry> GetAll();

        //public void Insert(TimeTrackEntry InsertedItem);
    }



}
