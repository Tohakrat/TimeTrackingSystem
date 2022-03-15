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
    internal abstract class AbstractRepositoryProvider
    {
        public abstract IRepository<User> GetUserRepository() ;
        public abstract IRepository<Project> GetProjectRepository();
        public abstract IRepository<TimeTrackEntry> GetTimeTrackEntryRepository();

    } 




}
