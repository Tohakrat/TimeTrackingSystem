using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Repositories.Xml;
using DataContracts;

namespace Solution
{
    abstract class AbstractRepositoryProvider
    {
        public abstract IRepository<User> GetUserRepository() ;
        public abstract IRepository<Project> GetProjectRepository();
        public abstract IRepository<TimeTrackEntry> GetTimeTrackEntryRepository();

    }
    class XmlRepositoryProvider : AbstractRepositoryProvider
    {
        public override IRepository<User> GetUserRepository()
        {
            return new XmlObjectRepository<User>();
        }

        public override IRepository<Project> GetProjectRepository()
        {
            return new XmlObjectRepository<Project>();
        }
        public override IRepository<TimeTrackEntry> GetTimeTrackEntryRepository()
        {
            return new XmlObjectRepository<TimeTrackEntry>();
        }
    }





    //public class Mapper
    //{
    //    //XmlUserRepository<T> Repo;
    //    public IRepository<User> GetUserRepository() { return new XmlObjectRepository<User>(); }
    //}




}
