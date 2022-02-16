using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using DataContracts;

namespace Repositories.Xml
{
    public class XmlUserRepository : IUserRepository
    {
        private List<User> ObjectList { get; set; } = new();
        IEnumerable<User> IRepository<User>.GetAll()
        {
            return ObjectList;
        }

        void IRepository<User>.Insert(User InsertedItem)
        {
            ObjectList.Add(InsertedItem);
        }
    }
    public class XmlProjectRepository : IProjectRepository
    {
        private List<Project> ObjectList { get; set; } = new();
        IEnumerable<Project> IRepository<Project>.GetAll()
        {
            return ObjectList;
        }

        void IRepository<Project>.Insert(Project InsertedItem)
        {
            ObjectList.Add(InsertedItem);
        }
    }


}
