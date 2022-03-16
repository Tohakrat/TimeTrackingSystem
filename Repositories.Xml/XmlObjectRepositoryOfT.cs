using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataContracts;

namespace Repositories.Xml
{
    public class XmlObjectRepository<T> : IRepository<T> where T:BaseEntity
    {
        private List<T> ObjectList { get; set; } = new();
        public IEnumerable<T> GetAll()
        {
            return ObjectList;
        }
        public void Insert (T TObj)
        {
            ObjectList.Add(TObj);
            ObjectInserted?.Invoke(this, " Xml object inserted: " + TObj.ToString());
        }
        public void Delete(T TObj)
        {
            T ObjectToDelete = ObjectList.Single(x => x.Id.Equals( TObj.Id)); //   First( from O in ObjectList where O.Id equals TObj.Id)
            if (ObjectList.Remove(ObjectToDelete))
            {
                //ObjectList.Remove(TObj);                
                ObjectDeleted?.Invoke(this, " Xml object Deleted: " + TObj.ToString());
            }
            
        }
        public event EventHandler<string> ObjectInserted;
        public event EventHandler<string> ObjectDeleted;
    }
    
   




    //public class XmlUserRepository : IUserRepository
    //{
    //    private List<User> ObjectList { get; set; } = new();
    //    IEnumerable<User> IRepository<User>.GetAll()
    //    {
    //        return ObjectList;
    //    }

    //    void IRepository<User>.Insert(User InsertedItem)
    //    {
    //        ObjectList.Add(InsertedItem);
    //        UserInserted?.Invoke(this,"XML User Inserted");
    //    }
    //    internal event EventHandler<string> UserInserted;

    //}
    //public class XmlProjectRepository : IProjectRepository
    //{
    //    private List<Project> ObjectList { get; set; } = new();
    //    IEnumerable<Project> IRepository<Project>.GetAll()
    //    {
    //        return ObjectList;
    //    }

    //    void IRepository<Project>.Insert(Project InsertedItem)
    //    {
    //        ObjectList.Add(InsertedItem);

    //    }

    //}


}
