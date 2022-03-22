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
    //public interface IUserRepository : IRepository<User>
    //{           
    //}
    //public interface IProjectRepository : IRepository<Project>
    //{        
    //}
    //public interface ITimeTrackEntryRepository : IRepository<TimeTrackEntry>
    //{
        
    //}



}
